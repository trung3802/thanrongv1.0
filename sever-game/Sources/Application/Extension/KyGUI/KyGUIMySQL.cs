using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Application.Extension.Ký_gửi
{
    

    public class KyGUIMySQL{
        public static bool UpdateAllItem()
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return false;
                    command.CommandText = "TRUNCATE TABLE `shop kí gửi`";
                    command.ExecuteNonQuery();
                    var listItem = CollectionsMarshal.AsSpan(Cache.Gi().kyGUIItems.ToList());
                    for (int i = 0; i < listItem.Length; i++)
                    {
                        var item = listItem[i].Value;
                        command.CommandText = $"insert into `shop kí gửi` (`id`, `player_id`, `tab`, `item_id`, `buyType`, `Cost`, `quantity`, `itemOptions`, `isUpTop`, `isBuy`) values ('{item.ItemId}', '{item.IdPlayerSell}', '{item.Tab}', '{item.Id}', '{item.BuyType}', '{item.Cost}', '{item.quantity}', '{JsonConvert.SerializeObject(item.Options)}', '0', '{item.isBuy}');";
                        command.ExecuteNonQuery();
                    }
                    return true;
                }catch(Exception e)
                {
                    Server.Gi().Logger.Error($"Updateallitem ki gui error: {e.Message}\n{e.StackTrace}");
                    return false;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static void DelItem(int id)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText =
                        $"DELETE FROM `shop kí gửi` WHERE `id` = {id};";
                    command.ExecuteNonQuery();
                    Server.Gi().Logger.Print("rid: " + id, "red");
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
        public static void DelItem2(int id)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText =
                        $"DELETE FROM `shop kí gửi 2` WHERE `id` = {id};";
                    command.ExecuteNonQuery();
                    Server.Gi().Logger.Print("rid2: " + id, "red");

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
        public static void InsertNewItem(short id, int plId, byte tab, short itemid, int gold, int gem, int quantity, List<OptionItem> optionItems){
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText = $"insert into `shop kí gửi` (`id`, `player_id`, `tab`, `item_id`, `gold`, `gem`, `quantity`, `itemOptions`, `isUpTop`, `isBuy`) values ('{id}', '{plId}', '{tab}', '{itemid}', '{gold}', '{gem}', '{quantity}', '{JsonConvert.SerializeObject(optionItems)}', '0', '0');";
                    command.ExecuteNonQuery();
                    return;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"InsertNewItem error: {e.Message}\n{e.StackTrace}");
                    return;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static void InsertNewItem2(short id, int plId, byte tab, short itemid, int gold, int gem, int quantity, List<OptionItem> optionItems)
        {
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText = $"insert into `shop kí gửi 2` (`id`, `player_id`, `tab`, `item_id`, `gold`, `gem`, `quantity`, `itemOptions`, `isUpTop`, `isBuy`) values ('{id}', '{plId}', '{tab}', '{itemid}', '{gold}', '{gem}', '{quantity}', '{JsonConvert.SerializeObject(optionItems)}', '0', '0');";
                    command.ExecuteNonQuery();//'{JsonConvert.SerializeObject(optionItems)}'
                    return;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"InsertNewItem error: {e.Message}\n{e.StackTrace}");
                    return;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
    }
}