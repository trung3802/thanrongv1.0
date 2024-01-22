using System;
using System.Data.Common;
using Newtonsoft.Json;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Clan;

namespace TienKiemV2Remastered.DatabaseManager.Player
{
    public class ClanDB
    {
        public static int Create(Clan clan)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command != null)
                    {
                        command.CommandText =
                            $"INSERT INTO `clan` (`Name`, `Khẩu hiệu`, `ImgId`, `Điểm thành tích`, `LeaderName`, `Thành viên hiện tại`, `Thành viên tối đa`, `Thời gian tạo bang`, `Thành viên`, `Messages`, `CharacterPeas`, `DataBlackBall`, `Leader`, `ClanBox`,`Điểm Danh Vọng`, `KhiGas`,`DateTime`,`shortName`) VALUES ('{clan.Name}', '{clan.Khẩu_hiệu}', {clan.ImgId}, {clan.Điểm_thành_tích}, '{clan.LeaderName}', {clan.Thành_viên_hiện_tại}, {clan.Tối_đa_thành_viên}, {clan.Thời_gian_tạo_bang}, '{JsonConvert.SerializeObject(clan.Thành_viên)}', '{JsonConvert.SerializeObject(clan.Messages)}' , '{JsonConvert.SerializeObject(clan.CharacterPeas)}','{JsonConvert.SerializeObject(clan.DataBlackBall)}', '{JsonConvert.SerializeObject(clan.Leader)}','[]','{clan.Điểm_Danh_Vọng}', '{JsonConvert.SerializeObject(clan.Gas)}', '{clan.TimeClanCreate:yyyy-MM-dd HH:mm:ss}', ''); SELECT LAST_INSERT_ID();";
                        var reader = int.Parse(command.ExecuteScalar()?.ToString() ?? "0"); 
                        return reader;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Create new character error: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
                return 0;
            }
        }

        public static void Delete(int clanId)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText =
                        $"DELETE FROM `clan` WHERE `id` = {clanId};";
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Create new character error: {e.Message}\n{e.StackTrace}");
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }

        public static void Update(Clan clan)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    var text = $"`Name` = '{clan.Name}'";
                    text += $", `Khẩu hiệu` = '{clan.Khẩu_hiệu}'";
                    text += $", `ImgId` = {clan.ImgId}";
                    text += $", `Điểm thành tích` = {clan.Điểm_thành_tích}";
                    text += $", `LeaderName` = '{clan.LeaderName}'";
                    text += $", `thành viên hiện tại` = {clan.Thành_viên_hiện_tại}";
                    text += $", `thành viên tối đa` = {clan.Tối_đa_thành_viên}";
                    text += $", `Thời gian tạo bang` = {clan.Thời_gian_tạo_bang}";
                    text += $", `Cấp độ` = {clan.Cấp_Độ}";
                    text += $", `Capsule Bang` = {clan.Capsule_Bang}";
                    text += $", `Thành viên` = '{JsonConvert.SerializeObject(clan.Thành_viên)}'";
                    text += $", `Messages` = '{JsonConvert.SerializeObject(clan.Messages)}'";
                    text += $", `CharacterPeas` = '{JsonConvert.SerializeObject(clan.CharacterPeas)}'";
                    text += $", `DataBlackBall` = '{JsonConvert.SerializeObject(clan.DataBlackBall)}'";
                    text += $", `Leader` = '{JsonConvert.SerializeObject(clan.Leader)}'";    
                    text += $", `ClanBox` = '{JsonConvert.SerializeObject(clan.ClanBox)}'";
                    text += $", `Điểm Danh Vọng` = '{clan.Điểm_Danh_Vọng}'";
                    text += $", `KhiGas` = '{JsonConvert.SerializeObject(clan.Gas)}'";
                    text += $", `shortName` = '{clan.shortName}'";
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText = $"UPDATE `clan` SET {text}  WHERE `id` = {clan.Id};";
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Update character error: {e.Message}\n{e.StackTrace}");
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
    }
}