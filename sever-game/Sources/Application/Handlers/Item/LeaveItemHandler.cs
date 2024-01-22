    using System;
using System.Linq;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Option;
using Org.BouncyCastle.Math.Field;
using System.Net.WebSockets;
using TienKiemV2Remastered.Application.MainTasks;
using TienKiemV2Remastered.DatabaseManager;

namespace TienKiemV2Remastered.Application.Handlers.Item
{
    public static class LeaveItemHandler
    {
        public static ItemMap LeaveGold(int charId, int quantity)
        {
            if (ServerUtils.RandomNumber(100) > 25) return null;
            var item = ItemCache.GetItemDefault(76);
            switch (quantity)
            {
                case >= 350 and < 5500:
                    item = ItemCache.GetItemDefault(188);
                    break;
                case >= 5500 and < 15000:
                    item = ItemCache.GetItemDefault(189);
                    break;
                case > 15000:
                    item = ItemCache.GetItemDefault(190);
                    break;
            }

            item.Quantity = quantity;
            return new ItemMap(charId, item);
        }

        public static ItemMap LeaveGoldPlayer(int charId, int quantity)
        {
            if (quantity == 0) return null;
            var item = ItemCache.GetItemDefault(76);
            item.Quantity = quantity;
            return new ItemMap(charId, item);
        }
        public static ItemMap LeaveManhBongTai(int charId)
        {
            var item = ItemCache.GetItemDefault(933, 935);
            item.Quantity = 1;
            return new ItemMap(charId, item);
        }
        public static ItemMap LeaveSuKienHe(int charId)
        {
            List<int> ListItem = new List<int>() { 695, 696, 697, 698 };
            var item = ItemCache.GetItemDefault((short)(ListItem[ServerUtils.RandomNumber(ListItem.Count)]));
            return new ItemMap(charId, item);
        }
        public static ItemMap LeaveBuaNguHanhSon(int charId)
        {
                var item = ItemCache.GetItemDefault((short)DataCache.ListBuaNguHanhSon[ServerUtils.RandomNumber(DataCache.ListBuaNguHanhSon.Count)]);
                item.Quantity = 1;
                return new ItemMap(charId, item);
            
        }
        public static ItemMap LeaveMonsterItemRecode(ICharacter character, int leaveItemType, int goldPlusPercent = 0, int mapId = 0, short monsterId = 0)
        {
            var charId = Math.Abs(character.Id);
            var item = ItemCache.GetItemDefault(1);
            var percentSuccess = ServerUtils.RandomNumber(100);
            switch (leaveItemType)
            {
                case 0:
                    if (character.InfoOption.QuanBoi)
                    {
                        
                        item = ItemCache.GetItemDefault(1244);
                        item.Quantity = 1;
                        return new ItemMap(charId, item);

                    }
                    else
                    {
                        return LeaveGold(character, ServerUtils.RandomNumber(2000, 3000), goldPlusPercent);
                    }
                //case 1:
                //    if (TaskHandler.CheckTask((Model.Character.Character)character,2, 0))
                //    {
                //        var percentDropDuiGa = ServerUtils.RandomNumber(100) < 50;
                //        if (percentDropDuiGa)
                //        {
                //            item = ItemCache.GetItemDefault(74);
                //            item.Quantity = 1;
                //            return new ItemMap(charId, item);
                //        }
                //        else
                //            {
                //                return LeaveGold(character, ServerUtils.RandomNumber(2000, 3000), goldPlusPercent);
                //            }
                //        //else
                //        //{
                //        //    if (percentSuccess < 30)
                //        //    {
                //        //        var ListDragonBall = new List<int> { 19, 20 };
                //        //        short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                //        //        item = ItemCache.GetItemDefault(randomDragonBall);
                //        //        item.Quantity = 1;
                //        //        return new ItemMap(charId, item);
                //        //    }
                //        //    else if (percentSuccess < 50)
                //        //    {
                //        //        int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                //        //        short idDaNangCap = DataCache.ListDaNangCap[index];
                //        //        item = ItemCache.GetItemDefault(idDaNangCap);
                //        //        item.Quantity = 1;
                //        //        return new ItemMap(charId, item);
                //        //    }
                //        //    else if (percentSuccess < 70)
                //        //    {
                //        //        var CaiTrangDSPL = character.ItemBody[5];
                //        //        if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                //        //        int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                //        //        short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                //        //        item = ItemCache.GetItemDefault(idSaoPhaLe);
                //        //        item.Quantity = 1;
                //        //        return new ItemMap(charId, item);
                //        //    }
                //        //    else
                //        //    {
                //        //        return LeaveGold(character, ServerUtils.RandomNumber(2000, 3000), goldPlusPercent);
                //        //    }
                //        //}
                //    }
                //    else
                //    {
                //        var percentDropSetKichHoat = ServerUtils.RandomNumber(0.000, 100.0) < 0.013;
                //        //if (percentDropSetKichHoat)
                //        //{
                //        //    return LeaveSKH(character, mapId);
                //        //}
                //        if (percentSuccess < 45)// up ra skh
                //        {
                //            var percentSKH = ServerUtils.RandomNumber(0.0, 100.0);
                //            if (percentSKH < 25.0)
                //            {
                //                return LeaveSKH(character, mapId, rare: 2);
                //            }
                //            else if (percentSKH < 25.0)
                //            {
                //                return LeaveSKH(character, mapId, rare: 3);
                //            }
                //            else if (percentSKH < 25.0)
                //            {
                //                return LeaveSKH(character, mapId, rare: 2);
                //            }
                //            else if (percentSKH < 25.0)
                //            {
                //                return LeaveSKH(character, mapId, rare: 3);
                //            }
                //        }
                //        else
                //        {
                //            if (percentSuccess < 55)
                //            {
                //                var ListDragonBall = new List<int> { 18,19, 20 };
                //                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                //                item = ItemCache.GetItemDefault(randomDragonBall);
                //                item.Quantity = 1;
                //                return new ItemMap(charId, item);
                //            }
                //            else if (percentSuccess < 50)
                //            {
                //                int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                //                short idDaNangCap = DataCache.ListDaNangCap[index];
                //                item = ItemCache.GetItemDefault(idDaNangCap);
                //                item.Quantity = 1;
                //                return new ItemMap(charId, item);
                //            }
                //            else if (percentSuccess < 70)
                //            {
                //                var CaiTrangDSPL = character.ItemBody[5];
                //                if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                //                int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                //                short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                //                item = ItemCache.GetItemDefault(idSaoPhaLe);
                //                item.Quantity = 1;
                //                return new ItemMap(charId, item);
                //            }
                //            else
                //            {
                //                return LeaveGold(character, ServerUtils.RandomNumber(2000, 3500), goldPlusPercent);
                //            }
                //        }
                //        break;
                //    }
                case 1:
                    if (TaskHandler.CheckTask((Model.Character.Character)character, 2, 0))
                    {
                        var percentDropDuiGa = ServerUtils.RandomNumber(100) < 50;
                        if (percentDropDuiGa)
                        {
                            item = ItemCache.GetItemDefault(74);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }
                        else
                        {
                            return LeaveGold(character, ServerUtils.RandomNumber(2000, 3000), goldPlusPercent);
                        }
                        //else
                        //{
                        //    if (percentSuccess < 30)
                        //    {
                        //        var ListDragonBall = new List<int> { 19, 20 };
                        //        short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                        //        item = ItemCache.GetItemDefault(randomDragonBall);
                        //        item.Quantity = 1;
                        //        return new ItemMap(charId, item);
                        //    }
                        //    else if (percentSuccess < 50)
                        //    {
                        //        int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                        //        short idDaNangCap = DataCache.ListDaNangCap[index];
                        //        item = ItemCache.GetItemDefault(idDaNangCap);
                        //        item.Quantity = 1;
                        //        return new ItemMap(charId, item);
                        //    }
                        //    else if (percentSuccess < 70)
                        //    {
                        //        var CaiTrangDSPL = character.ItemBody[5];
                        //        if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                        //        int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                        //        short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                        //        item = ItemCache.GetItemDefault(idSaoPhaLe);
                        //        item.Quantity = 1;
                        //        return new ItemMap(charId, item);
                        //    }
                        //    else
                        //    {
                        //        return LeaveGold(character, ServerUtils.RandomNumber(2000, 3000), goldPlusPercent);
                        //    }
                        //}
                    }
                    else
                    {
                        var percentDropSetKichHoat = ServerUtils.RandomNumber(0.000, 100.0) < 0.013;
                        if (percentDropSetKichHoat)
                        {
                            return LeaveSKH(character, mapId);
                        }
                        else
                        {
                            if (percentSuccess < 30)
                            {
                                var ListDragonBall = new List<int> { 19, 20 };
                                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                item = ItemCache.GetItemDefault(randomDragonBall);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percentSuccess < 50)
                            {
                                int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                short idDaNangCap = DataCache.ListDaNangCap[index];
                                item = ItemCache.GetItemDefault(idDaNangCap);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percentSuccess < 70)
                            {
                                var CaiTrangDSPL = character.ItemBody[5];
                                if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                item = ItemCache.GetItemDefault(idSaoPhaLe);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                return LeaveGold(character, ServerUtils.RandomNumber(2000, 3500), goldPlusPercent);
                            }
                        }
                    }
                case 2:
                    if (TaskHandler.CheckTask(character,8, 1))
                    {
                        var percentDrop7sao = ServerUtils.RandomNumber(100) < 50;
                        if (monsterId == 10 || monsterId == 11 || monsterId == 12)
                        {
                            if (percentDrop7sao)
                            {
                                item = ItemCache.GetItemDefault(20);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                if (percentSuccess < 30)
                                {
                                    var ListDragonBall = new List<int> { 19, 20 };
                                    short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                    item = ItemCache.GetItemDefault(randomDragonBall);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                                else if (percentSuccess < 50)
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                    short idDaNangCap = DataCache.ListDaNangCap[index];
                                    item = ItemCache.GetItemDefault(idDaNangCap);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                                else if (percentSuccess < 70)
                                {
                                    var CaiTrangDSPL = character.ItemBody[5];
                                    if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                    int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                    short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                    item = ItemCache.GetItemDefault(idSaoPhaLe);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                                else
                                {
                                    return LeaveGold(character, ServerUtils.RandomNumber(2100, 3600), goldPlusPercent);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (percentSuccess < 50)
                        {
                            var ListDragonBall = new List<int> { 19, 20 };
                            short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                            item = ItemCache.GetItemDefault(randomDragonBall);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }
                        else if (percentSuccess < 50)
                        {
                            int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                            short idDaNangCap = DataCache.ListDaNangCap[index];
                            item = ItemCache.GetItemDefault(idDaNangCap);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }
                        else if (percentSuccess < 70)
                        {
                            var CaiTrangDSPL = character.ItemBody[5];
                            if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                            int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                            short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                            item = ItemCache.GetItemDefault(idSaoPhaLe);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }

                        else
                        {
                            return LeaveGold(character, ServerUtils.RandomNumber(2200, 3700), goldPlusPercent);
                        }
                    }
                    break;
                case 3:
                    if (TaskHandler.CheckTask(character, 15, 1))
                    {
                        var percentDropTruyenDoremon = ServerUtils.RandomNumber(100) < 30;
                        if (monsterId == 13 || monsterId == 14 || monsterId == 15)
                        {
                            if (percentDropTruyenDoremon)
                            {
                                item = ItemCache.GetItemDefault(85);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                        }
                        else
                        {
                            if (percentSuccess < 50)
                            {
                                var ListDragonBall = new List<int> { 19, 20 };
                                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                item = ItemCache.GetItemDefault(randomDragonBall);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percentSuccess < 50)
                            {
                                int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                short idDaNangCap = DataCache.ListDaNangCap[index];
                                item = ItemCache.GetItemDefault(idDaNangCap);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percentSuccess < 70)
                            {
                                var CaiTrangDSPL = character.ItemBody[5];
                                if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                item = ItemCache.GetItemDefault(idSaoPhaLe);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                return LeaveGold(character, ServerUtils.RandomNumber(2400, 3800), goldPlusPercent);
                            }
                        }
                    }
                    else
                    {
                        if (percentSuccess < 30)
                        {
                            var ListDragonBall = new List<int> { 19, 20 };
                            short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                            item = ItemCache.GetItemDefault(randomDragonBall);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }
                        else if (percentSuccess < 50)
                        {
                            int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                            short idDaNangCap = DataCache.ListDaNangCap[index];
                            item = ItemCache.GetItemDefault(idDaNangCap);
                            item.Quantity = 1;
                            return new ItemMap(charId, item);
                        }
                        else if (percentSuccess < 70)
                        {
                            if (character.InfoSet.IsQuanBoi && ConfigManager.gI().SuKienHe)
                            {
                                var percentt = ServerUtils.RandomNumber(100);
                                if (percentt < 55)
                                {
                                   
                                    item = ItemCache.GetItemDefault(1245);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                                else
                                {
                                    var CaiTrangDSPL = character.ItemBody[5];
                                    if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                    int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                    short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                    item = ItemCache.GetItemDefault(idSaoPhaLe);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                            }
                            else
                            {
                                var CaiTrangDSPL = character.ItemBody[5];
                                if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                item = ItemCache.GetItemDefault(idSaoPhaLe);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                        }
                        else
                        {
                           return LeaveGold(character, ServerUtils.RandomNumber(2500, 3900), goldPlusPercent);
                        }
                    }
                    break;
                case 9:
                    {
                        var charReal = (TienKiemV2Remastered.Model.Character.Character)character;
                        var percent = ServerUtils.RandomNumber(100);
                        if (charReal.InfoBuff.MayDoCSKB)
                        {
                            var percentDropCapsuleKiBi = ServerUtils.RandomNumber(100);
                            if (percent < 60)
                            {
                                item = ItemCache.GetItemDefault(1235);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percent < 70)
                            {
                                item = ItemCache.GetItemDefault(380);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }else if (percent < 80)
                            {
                                item = ItemCache.GetItemDefault((short)ServerUtils.RandomNumber(933,934));
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percent < 90)
                            {
                                int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                short idDaNangCap = DataCache.ListDaNangCap[index];
                                item = ItemCache.GetItemDefault(idDaNangCap);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                // drop vang
                                return LeaveGold(character, ServerUtils.RandomNumber(13000, 15000), goldPlusPercent);
                            }
                        }
                        else
                        {
                            if (percent < 30)
                            {
                                item = ItemCache.GetItemDefault(1235);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            if (percent < 50)
                            {
                                // drop spl
                                var CaiTrangDSPL = character.ItemBody[5];
                                if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                item = ItemCache.GetItemDefault(idSaoPhaLe);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else if (percent < 70)
                            {
                                // drop ngoc rong
                                var ListDragonBall = new List<int> { 19, 20 };
                                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                item = ItemCache.GetItemDefault(randomDragonBall);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                // drop vang
                                return LeaveGold(character, ServerUtils.RandomNumber(13000, 15000), goldPlusPercent);
                            }

                        }
                    }
                case 10:
                    {
                        if (DataCache.IdMapCold.Contains(mapId))
                        {
                            var percent = ServerUtils.RandomNumber(0.000, 150.000);
                            // drop thuc an
                            var charReal = (Model.Character.Character)character;
                            if (charReal.InfoSet.IsFullSetThanLinh)
                            {
                                if (percent <= 30)
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListThucAn.Count);
                                    short idThucHan = DataCache.ListThucAn[index];
                                    item = ItemCache.GetItemDefault(idThucHan);
                                    item.Quantity = 1;

                                    item.Options.Add(new OptionItem()
                                    {
                                        Id = 30,
                                        Param = 1
                                    });
                                    return new ItemMap(charId, item);
                                }else if (percent <= 60)
                                {
                                    item = ItemCache.GetItemDefault(1246);
                                    return new ItemMap(charId, item);
                                }
                                else
                                {
                                    return LeaveGold(character.Id,ServerUtils.RandomNumber(2000,5000));
                                }
                            }
                            else
                            {
                                if (percent < 0.016)
                                {
                                    return LeaveGodItem(character);
                                }
                                else if (percent < 0.032)
                                {
                                    return LeaveDoII(character);
                                }
                                else if (percent < 50)
                                {


                                    int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                    short idDaNangCap = DataCache.ListDaNangCap[index];
                                    item = ItemCache.GetItemDefault(idDaNangCap);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);

                                }
                                else if (percent < 50)
                                {
                                    // drop spl
                                    var CaiTrangDSPL = character.ItemBody[5];
                                    if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                                    int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                                    short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                                    item = ItemCache.GetItemDefault(idSaoPhaLe);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                                else if (percent < 70)
                                {
                                    // drop ngoc rong
                                    if (character.InfoSet.IsQuanBoi && ConfigManager.gI().SuKienHe)
                                    {
                                        var random = ServerUtils.RandomNumber(100);
                                        if (random < 55)
                                        {
                                            item = ItemCache.GetItemDefault(1246);
                                            item.Quantity = 1;
                                            return new ItemMap(charId, item);
                                        }
                                        else
                                        {
                                            var ListDragonBall = new List<int> { 19, 20 };
                                            short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                            item = ItemCache.GetItemDefault(randomDragonBall);
                                            item.Quantity = 1;
                                            return new ItemMap(charId, item);
                                        }
                                    }
                                    else
                                    {
                                        var ListDragonBall = new List<int> { 19, 20 };
                                        short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                        item = ItemCache.GetItemDefault(randomDragonBall);
                                        item.Quantity = 1;
                                        return new ItemMap(charId, item);
                                    }
                                }
                                else
                                {
                                    // drop vang
                                    return LeaveGold(character, ServerUtils.RandomNumber(13000, 15000), goldPlusPercent);
                                }
                            }
                        }else if (mapId == 155)
                        {
                            var percentManhThienSu = ServerUtils.RandomNumber(100);
                            if (percentManhThienSu <= 70)
                            {
                                var charReal = (Model.Character.Character)character;
                                if (charReal.InfoSet.IsFullSetHuyDiet)
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListManhAngel.Count);
                                    short idManhAngel = DataCache.ListManhAngel[index];
                                    item = ItemCache.GetItemDefault(idManhAngel);
                                    item.Quantity = 2;

                                    item.Options.Add(new OptionItem()
                                    {
                                        Id = 30,
                                        Param = 1
                                    });
                                    return new ItemMap(charId, item);
                                }
                                else
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                    short idDaNangCap = DataCache.ListDaNangCap[index];
                                    item = ItemCache.GetItemDefault(idDaNangCap);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                            }
                            else if (percentManhThienSu <= 80)
                            {
                                var ListDragonBall = new List<int> { 19, 20 };
                                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                item = ItemCache.GetItemDefault(randomDragonBall);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                return LeaveGold(character, ServerUtils.RandomNumber(13000, 15000), goldPlusPercent);
                            }
                        }
                        else if (DataCache.IdMapThucVat.Contains(mapId))
                        {
                            var percentManhThienSu = ServerUtils.RandomNumber(100);
                            if (percentManhThienSu <= 50)
                            {
                                var charReal = (Model.Character.Character)character;
                                if (charReal.InfoSet.IsFullSetHuyDiet)
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListManhAngel.Count);
                                    short idManhAngel = DataCache.ListManhAngel[index];
                                    item = ItemCache.GetItemDefault(idManhAngel);
                                    item.Quantity = 1;

                                    item.Options.Add(new OptionItem()
                                    {
                                        Id = 30,
                                        Param = 1
                                    });
                                    return new ItemMap(charId, item);
                                }
                                else
                                {
                                    int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                                    short idDaNangCap = DataCache.ListDaNangCap[index];
                                    item = ItemCache.GetItemDefault(idDaNangCap);
                                    item.Quantity = 1;
                                    return new ItemMap(charId, item);
                                }
                            }
                            else if (percentManhThienSu <= 60)
                            {
                                var ListDragonBall = new List<int> { 19, 20 };
                                short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                                item = ItemCache.GetItemDefault(randomDragonBall);
                                item.Quantity = 1;
                                return new ItemMap(charId, item);
                            }
                            else
                            {
                                return LeaveGold(character, ServerUtils.RandomNumber(13000, 15000), goldPlusPercent);
                            }
                        }
                    }
                    break;
                default:
                    if (percentSuccess < 50)
                    {
                        var ListDragonBall = new List<int> { 19, 20 };
                        short randomDragonBall = (short)ListDragonBall[ServerUtils.RandomNumber(ListDragonBall.Count)];
                        item = ItemCache.GetItemDefault(randomDragonBall);
                        item.Quantity = 1;
                        return new ItemMap(charId, item);
                    }
                    else if (percentSuccess < 50)
                    {
                        int index = ServerUtils.RandomNumber(DataCache.ListDaNangCap.Count);
                        short idDaNangCap = DataCache.ListDaNangCap[index];
                        item = ItemCache.GetItemDefault(idDaNangCap);
                        item.Quantity = 1;
                        return new ItemMap(charId, item);
                    } 
                    else if (percentSuccess < 70)
                    {
                        var CaiTrangDSPL = character.ItemBody[5];
                        if (CaiTrangDSPL == null || CaiTrangDSPL?.Options?.FirstOrDefault(option => option.Id == 110) == null) return null;
                        int index = ServerUtils.RandomNumber(DataCache.ListSaoPhaLe.Count);
                        short idSaoPhaLe = DataCache.ListSaoPhaLe[index];
                        item = ItemCache.GetItemDefault(idSaoPhaLe);
                        item.Quantity = 1;
                        return new ItemMap(charId, item);   
                    }
                    else
                    {
                        return LeaveGold(character, ServerUtils.RandomNumber(8000, 9000), goldPlusPercent);
                    }
            }
            return new ItemMap(charId, item);
        }
         
        public static ItemMap LeaveGold(ICharacter character, int gold, int percentPlusGold)
        {
            var vang = ItemCache.GetItemDefault(189);
            if (gold > 30000)
            {
                vang = ItemCache.GetItemDefault(190);
            }
            vang.Quantity = gold;
            if (percentPlusGold > 0)
            {
                vang.Quantity += vang.Quantity * percentPlusGold / 100;
            }
            return new ItemMap(character.Id, vang);
        }
        public static ItemMap LeaveGodItem(ICharacter character)
        {
            var index = ServerUtils.RandomNumber(DataCache.ListDoThanLinh.Count);
            short idDoHuyDiet = DataCache.ListDoThanLinh[index];
            var item = ItemCache.GetItemDefault(idDoHuyDiet);
            var ListOption = new List<int> { 47, 6, 0, 7, 14 };
            var minParam = new List<int> { 730, 36000, 3600, 36000, 12 };
            var maxParam = new List<int> { 1200, 69999, 7000, 59000, 18 };
            var typeItem = ItemCache.ItemTemplate(item.Id).Type;
            var option = item.Options.FirstOrDefault(i => i.Id == ListOption[ItemCache.ItemTemplate(item.Id).Type]);
            if (option == null) return null;
            option.Param = ServerUtils.RandomNumber(minParam[typeItem], maxParam[typeItem]);
            if (ServerUtils.RandomNumber(50) < 20)
            {
                item.Options.Add(new OptionItem()
                {
                    Id = 107,
                    Param = ServerUtils.RandomNumber(1, 3),
                });
            }
            item.Quantity = 1;

            return new ItemMap(character.Id, item);
        }
        public static ItemMap LeaveDoII(ICharacter character)
        {
            var random = new Random();
            int index = random.Next(DataCache.ListDoHiem.Count);
            short idDoHuyDiet = DataCache.ListDoHiem[index];
            var item = ItemCache.GetItemDefault(idDoHuyDiet);
            var ListOption = new List<int> { 47, 6, 0, 7, 12 };
            var typeItem = ItemCache.ItemTemplate(item.Id).Type;
            var option = item.Options.FirstOrDefault(i => i.Id == ListOption[ItemCache.ItemTemplate(item.Id).Type]);
            option.Param = ServerUtils.RandomNumber(option.Param/((int)1.2), option.Param*((int)1.5));
            if (ServerUtils.RandomNumber(50) < 20)
            {
                item.Options.Add(new OptionItem()
                {
                    Id = 107,
                    Param = ServerUtils.RandomNumber(1, 3),
                });
            }
            item.Quantity = 1;

            return new ItemMap(character.Id, item);
        }
        public static ItemMap LeaveItemStar(ICharacter character, List<short> listIdItem, int maxStar)
        {
            var item = ItemCache.GetItemDefault(listIdItem[ServerUtils.RandomNumber(listIdItem.Count)]);
            item.Options.Add(new OptionItem()
            {
                Id = 107,
                Param = ServerUtils.RandomNumber(maxStar)
            });
            item.Quantity = 1;
            return new ItemMap(character.Id, item);
        }
        public static ItemMap LeaveSKH(ICharacter character, int mapId = 0, sbyte rare = 0)
        {
            if (character.Id < 0) return null;
            if (mapId != 1 && mapId != 2 && mapId != 3 && mapId != 8 && mapId != 9 && mapId != 10 
            && mapId != 15 && mapId != 16 && mapId != 17) return null;
            var gender = character.InfoChar.Gender;          
            var listItem = new List<short>(){0,6,12,21,27};
            var listSKH =    new List<int>(){127,128,129};

            //



            switch (rare)
            {
                case 0: //độ hiếm thường
                    {
                        if (gender == 1)
                        {
                            // NM hiếm Giầy và Radar 28
                            listItem = new List<short>() { 0 };
                            // bộ picolo hiếm
                            listSKH = new List<int>() { 131, 132 };
                        }
                        else if (gender == 2)
                        {
                            //XD hiếm quần và Radar 8
                            listItem = new List<short>() { 0 };
                            // bộ nappa hiếm 135
                            listSKH = new List<int>() { 133, 134 };
                        }
                        break;
                    }
                case 1: //độ hiếm cơ bản
                    {
                        listItem = new List<short>() { 0, 6, 12, 21, 27 };
                        listSKH = new List<int>() { 127, 128 };

                        if (gender == 1)
                        {
                            listItem = new List<short>() { 1, 7, 12, 22, 28 };
                            listSKH = new List<int>() { 131, 132 };
                        }
                        else if (gender == 2)
                        {
                            listItem = new List<short>() { 2, 8, 12, 23, 29 };
                            listSKH = new List<int>() { 133, 134 };
                        }
                        break;
                    }
                case 2: //độ hiếm tốt
                    {
                        // TD hiếm Găng và RADAR
                        listItem = new List<short>() { 0, 6, 12, 21, 27 };
                        listSKH = new List<int>() { 127, 128, 129 };

                        if (gender == 1)
                        {
                            listItem = new List<short>() { 1, 7, 12, 22, 28 };
                            listSKH = new List<int>() { 130, 131, 132 };
                        }
                        else if (gender == 2)
                        {
                            listItem = new List<short>() { 2, 8, 12, 23, 29 };
                            listSKH = new List<int>() { 133, 134, 135 };
                        }
                        break;
                    }
                case 3: //độ hiếm cực
                    {
                        // TD hiếm Găng và RADAR
                        listItem = new List<short>() { 0 };
                        listSKH = new List<int>() { 127, 128, 129 };

                        if (gender == 1)
                        {
                            listItem = new List<short>() { 0 };
                            listSKH = new List<int>() { 130, 131, 132 };
                        }
                        else if (gender == 2)
                        {
                            listItem = new List<short>() { 0 };
                            listSKH = new List<int>() { 133, 134, 135 };
                        }
                        break;
                    }
            }
            //
            if (gender == 1)
            {
                listItem = new List<short>() { 7, 12, 22, 28 ,1};
                listSKH = new List<int>() { 131, 132, 213, 130 };
            }
            else if (gender == 2)
            {
                listItem = new List<short>() { 2, 8, 12, 23, 29 };
                listSKH = new List<int>() { 133, 134, 135 };
            }
            var item = ItemCache.GetItemDefault(listItem[ServerUtils.RandomNumber(listItem.Count)]);
            item.Quantity = 1;
            if (ServerUtils.RandomNumber(100) < 30)
            {
                item.Options.Add(new OptionItem()
                {
                    Id = 107,
                    Param = ServerUtils.RandomNumber(3)
                });
            }
            else
            {
                var idSKH = listSKH[ServerUtils.RandomNumber(listSKH.Count)];
                item.Options.Add(new OptionItem()
                {
                    Id = idSKH,
                    Param = 0,
                });
                item.Options.Add(new OptionItem()
                {
                    Id = GetSKHDescOption(idSKH),
                    Param = 0,
                });
                item.Options.Add(new OptionItem()
                {
                    Id = 30,
                    Param = 0,
                });
            }
            return new ItemMap(character.Id, item);
        }
    
        public static int GetSKHDescOption(int skhId)
        {
            switch (skhId)
            {
                case 127: return 139;
                case 128: return 140;
                case 129: return 141;
                case 130: return 142;
                case 131: return 143;
                case 132: return 144;
                case 133: return 136;
                case 134: return 137;
                case 135: return 138;
                case 213: return 214;   
            }
            return 73;
        }
    }
}