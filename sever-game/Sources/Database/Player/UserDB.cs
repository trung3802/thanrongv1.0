using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.BangXepHang;

namespace TienKiemV2Remastered.DatabaseManager.Player
{
    public class UserDB
    {
        public static Model.Player Login(string username, string password)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT * FROM `user` WHERE (`username` = '{username}' AND `password` = '{password}');";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    int characterId = reader.GetInt32(3);
                                    bool IsActive = reader.GetInt32(4) == 0 ? false : true;
                                    int role = reader.GetInt32(5);
                                    byte ban = reader.GetByte(6);
                                    int tongVND = reader.GetInt32(12);
                                    int tichnap = reader.GetInt32(14);
                                        
                                    Model.Player player = new Model.Player()
                                    {
                                        Id = id,
                                        Username = username,
                                        CharId = characterId,
                                        IsActive = IsActive,
                                        Role = role,
                                        Ban = ban,
                                        TongVND = tongVND,
                                        DiemTichNap = tichnap
                                    };
                                    return player;  
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Login User: {e.Message}\n{e.StackTrace}");
                    return null;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
            return null;
        }

        public static void UpdatePort(int playerId, int port)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        // int port = DatabaseManager.Manager.gI().ServerPort;
                        command.CommandText = $"UPDATE `user` SET `sv_port`= {port} WHERE `id`=" + playerId + " LIMIT 1;";
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Update Port User: {e.Message}\n{e.StackTrace}");
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }

        public static bool Update(Model.Player player, string ipV4 = "", bool isCreateChar = false)
        {
            if (ipV4 != "")
            {
                var timeServer = ServerUtils.CurrentTimeMillis();
                ServerUtils.WriteLog("login", $"Tên tài khoản {player.Username} (ID:{player.Id}) ipV4 {ipV4}");
                // Chỉ có đăng nhập mới có IP V4
                // Kiểm tra thời gian đăng nhập gần nhất
                // Dưới 20 giây là khóa tài khoản
                long thoiGianDangNhap = 0;
                if (ClientManager.Gi().UserLoginTime.TryGetValue(player.Id, out thoiGianDangNhap) && !isCreateChar)
                {
                    var difTime = (timeServer-thoiGianDangNhap);
                    if (difTime < 10000)
                    {
                        //  // ban tài khoản
                        ////  UserDB.BanUser(player.Id);
                        //  ClientManager.Gi().KickSession(player.Session);
                        //  ServerUtils.WriteLog("bandn", $"Tên tài khoản {player.Username} (ID:{player.Id}) Dif time: {difTime} ms");

                        var temp = ClientManager.Gi().GetPlayer(player.Id);
                        if (temp != null)
                        {
                            ClientManager.Gi().KickSession(temp.Session);
                        }
                        return false;
                    }
                    else 
                    {
                        ClientManager.Gi().UserLoginTime.TryUpdate(player.Id, timeServer, thoiGianDangNhap);
                    }
                }
                else 
                {
                    ClientManager.Gi().UserLoginTime.TryAdd(player.Id, timeServer);
                }
            }
            else 
            {
                ServerUtils.WriteLog("login", $"(THOÁT) Tên tài khoản {player.Username} (ID:{player.Id})");
            }
            lock (Server.SQLLOCK)
            {
                try
                {
                    var timeServer = ServerUtils.CurrentTimeSecond() + 30;
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        int online = player.IsOnline ? 1 : 0;
                        if (ipV4 != "")
                        {
                            command.CommandText = $"UPDATE `user` SET `online`= {online}, `logout_time`= {timeServer}, `last_ip` = '{ipV4}', `is_login`=0, `character` = {player.CharId} WHERE `id`=" + player.Id + " LIMIT 1;";
                        }
                        else 
                        {
                            command.CommandText = $"UPDATE `user` SET `online`= {online}, `logout_time`= {timeServer}, `character` = {player.CharId} WHERE `id`=" + player.Id + " LIMIT 1;";
                        }
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Update User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        
        public static bool UpdateLogin(int userId, int login)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `is_login`= {login} WHERE `id`=" + userId + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error UpdateLogin User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
       
        public static bool BanUser(int id)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `ban`= 1 WHERE `id`=" + id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Update User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }

        public static bool updateOnline(int id)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `online`= 0 WHERE `id`=" + id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Update User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool isActive(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT active FROM `user` WHERE `id` = '{player.Id}';";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader.GetInt16(0) == 1;
                                }
                            }
                         
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static bool Active(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        // Kiểm tra trạng thái active của người dùng
                        command.CommandText =
                            $"SELECT `active` FROM `user` WHERE `id` = '{player.Id}';";

                        object result = command.ExecuteScalar();

                        // Nếu active bằng 0, cập nhật thành 1
                        if (result != null && result.ToString() == "0")
                        {
                            command.CommandText =
                                $"UPDATE `user` SET `active` = 1 WHERE `id` = '{player.Id}';";
                            command.ExecuteNonQuery();
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error Active User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static int GetVND(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT vnd FROM `user` WHERE `id` = '{player.Id}' LIMIT 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int vnd = reader.GetInt32(0);
                                    return vnd;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetVND User: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static int GetHongNgoc(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT hongngoc FROM `user` WHERE `id` = '{player.Id}' LIMIT 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int vnd = reader.GetInt32(0);
                                    return vnd;
                                }
                            }
                            else
                            {
                                return 0;
                            }

                        }
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetVND User: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static int GetThoiVang(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT thoivang FROM `user` WHERE `id` = '{player.Id}' LIMIT 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int tv = reader.GetInt32(0);
                                    return tv;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetVND User: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static bool ChangePassword(Model.Player player, string pass)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `password`= {pass} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error ChangePass User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static string GetPassword(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT password FROM `user` WHERE `id` = '{player.Id}' LIMIT 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string pass = reader.GetString(0);
                                    return pass;
                                }
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                    return "";
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetVND User: {e.Message}\n{e.StackTrace}");
                    return "";
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }
        public static int GetTongVND(Model.Player player)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText =
                            $"SELECT tongnap FROM `user` WHERE `id` = '{player.Id}' LIMIT 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int tongnap = reader.GetInt32(0);
                                    return tongnap;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error GetTongVND User: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.ConnectToAccount();
                }
            }
        }

        public static bool MineVND(Model.Player player, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `vnd`= vnd-{value} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool MineHongNgoc(Model.Player player, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `hongngoc`= hongngoc-{value} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool MineThoiVang(Model.Player player, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `thoivang`= thoivang-{value} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool PlusThoiVang(string account, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `thoivang`= thoivang+{value} WHERE `username`='" + account + "' LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool PlusThoiVang(Model.Player player, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `thoivang`= thoivang+{value} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool PlusVND(Model.Player player, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `vnd`= vnd+{value} WHERE `id`=" + player.Id + " LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool PlusVND(string username, int value)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE `user` SET `vnd`= vnd+{value} WHERE `username`='" +username + "' LIMIT 1;";
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error MineVND User: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static bool CheckBeforeChangePass(int userId, string oldPass)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return false;
                    command.CommandText = $"SELECT `id` FROM `user` WHERE `password` = '{oldPass}' AND `id`=" + userId + " LIMIT 1;";
                    using var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"CheckBeforeChangePass: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect(); 
                }
                
            }
        }

        public static bool DoiMatKhau(int userId, string newPass)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return false;
                    command.CommandText = $"UPDATE `user` SET `password`='{newPass}' WHERE `id`=" + userId + " LIMIT 1;";
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"DoiMatKhau: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect(); 
                }
                
            }
        }

       

        public static bool CheckInvalidPortServer(int playerId)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return false;
                    command.CommandText = $"SELECT `sv_port` FROM `user` WHERE `id` = {playerId};";
                    using var reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        var sv_port = reader.GetInt32(0);
                        return (sv_port != DatabaseManager.ConfigManager.gI().ServerPort);
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Update CheckPortBeforeUpdate error: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }

        public static bool CheckInvalidPortServer(string username, ref int thoiGianDangNhap, ref bool isOnline)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return false;
                    command.CommandText = $"SELECT `sv_port`, `logout_time`, `online` FROM `user` WHERE `username` = '{username}';";
                    using var reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        var sv_port = reader.GetInt32(0);
                        thoiGianDangNhap = reader.GetInt32(1);
                        isOnline = reader.GetInt32(2) == 0 ? false : true;
                        return (sv_port != DatabaseManager.ConfigManager.gI().ServerPort);
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Update CheckPortBeforeUpdate name error: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
    }
}