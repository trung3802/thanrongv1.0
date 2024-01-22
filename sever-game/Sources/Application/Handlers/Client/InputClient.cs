using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Handlers.Menu;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Source.Application.Handlers;

namespace TienKiemV2Remastered.Application.Handlers.Client
{
    public static class InputClient
    {
        public static void JoinHome(Model.Character.Character character,bool isDefaul = true, bool isTeleport = false, int typeTeleport = 0)
        {
            var home = new Home(character.InfoChar.Gender);
            home.Maps[0].JoinZone(character, 0, isDefaul, isTeleport, typeTeleport);
        }


        public static  void JoinKarin(Model.Character.Character character,int mapId, bool isDefaul = false, bool isTeleport = false, int typeTeleport = 0)
        {
            var karin = new Karin();
            karin.GetMapById(mapId)
                .JoinZone(character, 0, isDefaul, isTeleport, typeTeleport);
        }
        public static void HanleInputClient(Model.Character.Character character, Message message)
        {
            if(message == null) return;
            try
            {
                var lengthInput = message.Reader.ReadByte();
                var listInput = new List<string>();
                for (var i = 0; i < lengthInput; i++)
                {
                    listInput.Add(message.Reader.ReadUTF());
                }
                if(listInput.Count <= 0) return;
                switch (character.TypeInput)
                {
                    case 0://Nạp thẻ
                    {
                        var soSeriText = listInput[0];
                        var maPinText = listInput[1];

                        Console.WriteLine("Loai the " + character.NapTheTemp.LoaiThe + " menh gia " + character.NapTheTemp.MenhGia + " So Seri " + soSeriText + " ma pin " + maPinText);
                        GachThe.SendCard(character, character.NapTheTemp.LoaiThe, character.NapTheTemp.MenhGia, soSeriText, maPinText);
                        break;
                    }
                    case 1://Gift code 
                    {
                        var codeInput = listInput[0];
                        Giftcode.HandleUseGiftcode(character, listInput[0]);
                        break;
                    }
                    case 41://Gift code tân thủ
                    {
                        var codeInput = listInput[0];
                        GiftcodeTT.HandleUseGiftcodeTT(character, listInput[0]);
                        break;
                    }
                    case 42://Gift code tân thủ
                        {
                            var codeInput = listInput[0];
                            GiftcodeHT.HandleUseGiftcodeHT(character, listInput[0]);
                            break;
                        }
                    case 2://đổi mật khẩu
                    {
                        var timeServer = ServerUtils.CurrentTimeMillis();
                        var oldPass = listInput[0];
                        var newPass = listInput[1];
                        // var sdt = listInput[2];
                        var checkData = UserDB.CheckBeforeChangePass(character.Player.Id, oldPass);
                        if (!checkData)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Thông tin tài khoản không chính xác, vui lòng nhập lại."));
                            return;
                        }
                        UserDB.DoiMatKhau(character.Player.Id, newPass);
                        character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Đổi mật khẩu thành công, vui lòng thoát game và đăng nhập lại"));
                        break;
                    }
                    case 3: //khoa tai khoan
                    {
                        var banReason = listInput[0];
                        MenuAdminRecode.gI().HandlerBanAccount(banReason);
                        break;
                    }
                    case 4:
                        var id = Int32.Parse(listInput[0]);
                        var quantity = int.Parse(listInput[1]);
                        MenuAdminRecode.gI().BuffItem(id, quantity);
                        break;
                    case 5:
                        MenuAdminRecode.gI().CallBoss(int.Parse(listInput[0]));
                        break;
                    case 6:
                        var map = MapManager.Get(character.InfoChar.MapId);
                        if (map == null) return;
                        Threading.Map mapJoin = null;
                        mapJoin = MapManager.Get(Int32.Parse(listInput[0]));
                                if (mapJoin == null) return;
                                var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
                                if (zoneJoin != null)
                                {
                                    switch (zoneJoin.Map.Id)
                                    {
                                
                                            case 21:
                                            case 22:
                                            case 23:
                                            {
                                                JoinHome(character,true, true, character.InfoChar.Teleport);
                                                return;
                                            }
                                            case 47:
                                            {
                                                JoinKarin(character, 47, true, true, character.InfoChar.Teleport);
                                                return;
                                            }
                                            case 45:
                                            {
                                                JoinKarin(character, 45, true, true, character.InfoChar.Teleport);
                                                return;
                                            }
                                            case 48:
                                            {
                                                JoinKarin(character, 48, true, true, character.InfoChar.Teleport);
                                                return;
                                            }
                                            case 111:
                                            {
                                                JoinKarin(character, 111, true, true, character.InfoChar.Teleport);
                                                return;
                                            } default:
                                    {
                                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                                        map.OutZone(character, mapJoin.Id);
                                        zoneJoin.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
                                    }
                                    break;
                                    }
                                    
                                }
                        break;
                    case 112:
                        {
                            var username = listInput[0];
                            var thoivan = int.Parse(listInput[1]);
                            if (UserDB.PlusThoiVang(username, thoivan))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã cộng thành công cho tài khoản " + username  + " " + thoivan +" thỏi vàng"));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không tìm thấy tài khoản"));

                            }
                            break;
                        }
                    case 113:
                        {
                            var username = listInput[0];
                            var mone = int.Parse(listInput[1]);
                            if (UserDB.PlusVND(username, mone))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã cộng thành công cho tài khoản " + username + " " + mone ));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không tìm thấy tài khoản"));

                            }
                            break;
                        }
                    case 7:
                        MenuAdminRecode.gI().BuffPotenial(int.Parse(listInput[0]), long.Parse(listInput[1]));
                        break;
                    case 8:
                        
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, "Would You Want? SET OR PLUS ?", MenuNpc.Gi().MenuAdmin[3], character.InfoChar.Gender));
                        character.TypeMenu = 3;
                        break;
                    case 9:
                        var idTask = Int32.Parse(listInput[0]);
                        var index = Int32.Parse(listInput[1]);
                        var count = Int32.Parse(listInput[2]);
                        MenuAdminRecode.gI().BuffTask(idTask, index, count);
                        break;
                   case 10:
                        var getCharSelect = ClientManager.Gi().GetPlayerByUserName(MenuAdminRecode.gI().NameCharSelect).Character;
                        
                        break;
                    case 11:
                        //ingored teleport to nrsd
                        break;
                    case 12:
                        var code = listInput[0];
                        MenuAdminRecode.gI().CheckGiftcode(character, code);

                        break;
                    case 13:
                        var money = Int32.Parse(listInput[0 ]);
                        MenuAdminRecode.gI().BuffMoney(money);
                        break;
                    case 14:
                        // ingored input level dungeon
                        var levele = Int32.Parse(listInput[0]);
                        int l2;
                        var isNumberr = Int32.TryParse(listInput[0], out l2);

                        if (levele > 110 && !isNumberr && levele <= 0)
                        //if (levele > 110 || levele <= 0 || !isNumberr)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerChat("Level không hợp lệ, vui lòng chọn lại !"));
                            //character.CharacterHandler.RemoveItemBagById(611, 1, reason: "BDKB");
                            return;
                        }
                        var clan = ClanManager.Get(character.ClanId);

                        if (character.InfoChar.MapId == 48)
                        {
                            if (clan.cdrd.Count <= 0 && !clan.cdrd.Open){
                            character.CharacterHandler.SendMessage(Service.ServerChat("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                            clan.cdrd.Init(levele);
                            MapManager.OutMap(character, clan.cdrd.MapCDRD[0].Id);
                            character.InfoChar.X = 1103;
                            character.InfoChar.Y = 336;
                            clan.cdrd.MapCDRD[0].JoinZone(character, 0);
                        }
                        else
                        {
                            if (clan.bdkb.Count <= 0){
                            character.CharacterHandler.SendMessage(Service.ServerChat("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                            clan.bdkb.Init(levele);
                            MapManager.OutMap(character, clan.bdkb.MapBDKB[0].Id);
                            character.InfoChar.X = 78;
                            character.InfoChar.Y = 336;
                            clan.bdkb.MapBDKB[0].JoinZone(character, 0, false, false);
                        }
                        break;
                    case 15:
                        var ok = listInput[0];
                        if (ok == "ok" || ok == "OK")
                        {
                            var disciple = character.Disciple;
                            if (disciple == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DONT_FIND_DISCIPLE));
                                return;
                            }

                            var itemDiscipleBody = disciple.ItemBody.FirstOrDefault(item => item != null);

                            if (itemDiscipleBody != null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_EMPTY_DISCIPLE_BODY));
                                return;
                            }

                            var oldStatus = disciple.Status;

                            if (oldStatus < 3)
                            {
                                character.Zone.ZoneHandler.RemoveDisciple(character.Disciple);
                            }

                            disciple = new Disciple();
                            if (character.Disciple.InfoChar.Gender == 0)
                            {
                                disciple.CreateNewDisciple(character, 1);
                            }else if (character.Disciple.InfoChar.Gender == 1)
                            {
                                disciple.CreateNewDisciple(character, 2);
                            }
                            else
                            {
                                disciple.CreateNewDisciple(character, 0);
                            }
                            disciple.Player = character.Player;
                            disciple.Zone = character.Zone;
                            disciple.CharacterHandler.SetUpInfo();
                            character.Disciple = disciple;

                            if (!character.InfoChar.Fusion.IsFusion && oldStatus < 3)
                            {
                                character.Zone.ZoneHandler.AddDisciple(disciple);
                            }
                            else
                            {
                                character.CharacterHandler.SetUpInfo();
                                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                                character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                                character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                            }
                            character.CharacterHandler.RemoveItemBagById(401, 1, reason: "Dùng đổi đệ tử");
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            DiscipleDB.Update(disciple);
                        }
                        break;
                    case 16:
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            if (inputValue > UserDB.GetVND(character.Player))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            UserDB.MineVND(character.Player, inputValue);
                            character.InfoChar.DiamondLock += (inputValue / 10);
                            character.CharacterHandler.SendMessage(Service.SendBag(character));

                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa quy đổi {inputValue} hồng ngọc"));
                        }
                        break;
                    case 111:
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var thoivan = UserDB.GetThoiVang(character.Player);
                            if (thoivan > 0 && inputValue <= thoivan)
                            {
                                UserDB.MineThoiVang(character.Player, inputValue);
                                var item2 = ItemCache.GetItemDefault(457);
                                item2.Quantity = thoivan;
                                character.CharacterHandler.AddItemToBag(true, item2);
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa quy đổi {inputValue} thỏi vàng"));
                                return;
                            }
                            if (inputValue > UserDB.GetVND(character.Player))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                           
                            UserDB.MineVND(character.Player,inputValue);
                            var item = ItemCache.GetItemDefault(457);
                            item.Quantity = (inputValue/1000)*2;
                            character.CharacterHandler.AddItemToBag(true,item);
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa quy đổi {inputValue} thỏi vàng"));
                        }
                        break;
                    case 17:
                        int c;
                        bool nb = int.TryParse(listInput[0], out c);
                        
                        if (!nb)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                       
                        int thoivang = int.Parse(listInput[0]);

                        if (thoivang > character.CharacterHandler.GetThoiVangInBag() || character.CharacterHandler.GetItemBagById(457) == null || character.CharacterHandler.GetItemBagById(457).Quantity <= 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có đủ thỏi vàng"));
                            return;
                        }
                        character.DataMiniGame.thoivang += thoivang;
                        character.DataMiniGame.pickChan = true;
                        character.CharacterHandler.RemoveItemBagById(457, thoivang);
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Tổng số thỏi vàng đã đập vào Chẵn: " + thoivang));
                        break;
                    case 18:
                        int d;
                        bool nc = int.TryParse(listInput[0], out d);
                        if (!nc)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        thoivang = int.Parse(listInput[0]);
                        if (thoivang > character.CharacterHandler.GetThoiVangInBag() || character.CharacterHandler.GetItemBagById(457) == null || character.CharacterHandler.GetItemBagById(457).Quantity <= 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có đủ thỏi vàng"));
                            return;
                        }
                        character.DataMiniGame.thoivang += thoivang;
                        character.DataMiniGame.pickLe = true;
                        character.CharacterHandler.RemoveItemBagById(457, thoivang);
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Tổng số thỏi vàng đã đập vào Lẻ: " + thoivang));
                        break;
                    case 19:
                        string Name = listInput[0];
                        character.Name = Name;
                        character.CharacterHandler.SendMessage(Service.MeLoadAll(character));
                        break;
                    case 20:
                        Name = listInput[0];
                        character.Disciple.Name = Name;
                        character.Disciple.CharacterHandler.SendMessage(Service.MeLoadAll(character.Disciple));
               
                        break;
                    case 21:
                        var passNow = listInput[0];
                        var passChange = listInput[1];
                        var confirmPassChange = listInput[2];
                        if (UserDB.GetPassword(character.Player).Contains(passNow))
                        {
                            // dung pass thuc hien change
                            if (confirmPassChange == passChange)
                            {

                                UserDB.ChangePassword(character.Player, passChange);
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5,"Bạn đã đổi mật khẩu thành: " + passChange));
                            }
                            else
                            {
                                // ko confirm pass doi dung
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Nhập lại mật khẩu cần thay đổi phải đúng !"));
                            }
                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Sai mật khẩu hiện tại!"));
                            // sai mk hien tai
                        }
                        break;
                    case 22:
                        var clan2 = ClanManager.Get(character.ClanId);
                        int m;
                        bool isNumer = int.TryParse(listInput[0], out m);
                        if (!isNumer)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        if (clan2 == null)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có bang hội?"));
                            return;
                        }
                        var level = Int32.Parse(listInput[0]);
                        if (level <= 0 || level > 110)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chỉ có thể nhập từ cấp 0 -> cấp 110"));
                            return;
                        }
                        if (clan2.Gas.Count <= 0){
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                        clan2.Gas.Level = level;
                        clan2.Gas.initMapKhiGas();
                        clan2.Gas.InitMob(level);
                        var mapOld = MapManager.Get(character.InfoChar.MapId);
                        mapOld.OutZone(character, 149);
                        character.InfoChar.X = 121;
                        character.InfoChar.Y = 336;
                        clan2.Gas.GasMaps[0].JoinZone(character, 0);
                        //foreach (var CharacterSameClan in character.Zone.Characters.Values.ToList().Where(c => c.ClanId == character.ClanId))
                        //{
                        //    mapOld.OutZone(character, 149);
                        //    clan2.Gas.GasMaps[0].JoinZone(CharacterSameClan, 0);
                        //  //  character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Doanh trại độc nhãn", 0, (int)(ServerUtils.CurrentTimeMillis() - clan.Reddot.timeDoanhTrai)));
                        //}
                        break;
                    case 23:
                        var pl = listInput[0];
                        var allPlayerInSever = "\nALL SESSION : " + ServerUtils.GetMoneys(ClientManager.Gi().Sessions.Count) + " | ALL PLAYER: " + ServerUtils.GetMoneys(ClientManager.Gi().Characters.Count);
                        if (ClientManager.Gi().GetCharacter(pl) == null)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("NOT FOUND PLAYER !"));
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, "Xin Chào " + character.Name + ", " + allPlayerInSever + "\n|0|Sever Status: [ON]", new List<string> { "ME", "FIND PLAYER:\n" + MenuAdminRecode.gI().NameCharSelect }, character.InfoChar.Gender));
                            character.TypeMenu = 2;
                            return;
                        }
                        MenuAdminRecode.gI().NameCharSelect = pl;
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, "Xin Chào "+character.Name+", " + allPlayerInSever + "\n|0|Sever Status: [ON]", MenuNpc.Gi().MenuAdmin[0], character.InfoChar.Gender));
                        character.TypeMenu = 0;
                        break;
                    case 24: // set point
                        var hp = Int32.Parse(listInput[0]);
                        var mp = Int32.Parse(listInput[1]);
                        var sd = Int32.Parse(listInput[2]);
                        var crit = Int32.Parse(listInput[3]);
                        var amor = Int32.Parse(listInput[4]);
                        MenuAdminRecode.gI().HandlerPlusOrignalPoint(0, hp, mp, sd, amor, crit);
                        break;
                    case 25: // plus point
                        var hp2 = Int32.Parse(listInput[0]);
                        var mp2 = Int32.Parse(listInput[1]);
                        var sd2 = Int32.Parse(listInput[2]);
                        var crit2 = Int32.Parse(listInput[3]);
                        var amor2 = Int32.Parse(listInput[4]);
                        MenuAdminRecode.gI().HandlerPlusOrignalPoint(1, hp2, mp2, sd2, amor2, crit2);
                        break;
                    case 31:
                        if (listInput[0].Length > 10)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Tối đa 10 kí tự"));
                            return;
                        }
                        ClanManager.Get(character.ClanId).shortName = listInput[0];
                        break;
                    case 26:
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            // Kiểm tra có đủ VNĐ không
                            if (character.CharacterHandler.GetItemBagById(457).Quantity < inputValue || character.CharacterHandler.GetItemBagById(457).Quantity < 0 || character.CharacterHandler.GetItemBagById(457) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ thỏi vàng !"));
                                return;
                            }
                            character.CharacterHandler.RemoveItemBagById(457, inputValue);
                            long GoldGet = (long)((long)inputValue * 500000000);
                            if (GoldGet + character.InfoChar.Gold >= character.InfoChar.LimitGold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã Max giới hạn vàng, vui lòng mở rộng giới hạn vàng để bán !"));
                                return;
                            }
                            character.InfoChar.Gold += GoldGet;
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ServerUtils.GetMoneys(GoldGet) + " vàng"));
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            break;
                        }
                    case 37://chan
                        {
                            int tiencuoc = int.Parse(listInput[0]);
                            if (ChanLeHandler.GetTimeLeft() > 10)
                            {
                                if (character.CharacterHandler.GetItemBagById(457).Quantity < tiencuoc)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn không đủ thỏi vàng để cược !"));
                                    return;
                                }
                                if (!ChanLeHandler.Check(character.Id))
                                {
                                    if (tiencuoc < 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage($"Lỗi đặt cược ! Vui lòng chờ ván sau !!"));
                                        return;
                                    }
                                    ChanLeHandler.AddCuocChan(character.Id, tiencuoc);
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã đặt thành công {tiencuoc} thỏi vàng vào CHẴN"));
                                    character.CharacterHandler.RemoveItemBagById(457, tiencuoc, reason: "Con Số May Mắn");
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Lỗi đặt cược ! Vui lòng chờ ván sau !!"));
                                    return;
                                }
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage($"Sắp hết thời gian rùi không thể cược thêm nữa !"));
                                return;
                            }
                            break;
                        }
                    case 38://le
                        {
                            int tiencuoc = int.Parse(listInput[0]);
                            if (ChanLeHandler.GetTimeLeft() > 10)
                            {
                                if (character.CharacterHandler.GetItemBagById(457).Quantity < tiencuoc)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn không đủ thỏi vàng để cược !"));
                                    return;
                                }
                                if (!ChanLeHandler.Check(character.Id))
                                {
                                    if (tiencuoc < 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage($"Lỗi đặt cược ! Vui lòng chờ ván sau !!"));
                                        return;
                                    }
                                    ChanLeHandler.AddCuocLe(character.Id, tiencuoc);
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã đặt thành công {tiencuoc} thỏi vàng vào LẺ"));
                                    character.CharacterHandler.RemoveItemBagById(457, tiencuoc, reason: "Con Số May Mắn");
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Lỗi đặt cược ! Vui lòng chờ ván sau !!"));
                                    return;
                                }
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage($"Sắp hết thời gian rùi không thể cược thêm nữa !"));
                                return;
                            }

                            break;
                        }
                    case 40:
                        {
                            var @char = ClientManager.Gi().GetPlayerByUserName(listInput[0]);
                            var item = ItemCache.GetItemDefault((short)(int.Parse(listInput[1])), int.Parse(listInput[2]));
                            @char.Character.CharacterHandler.AddItemToBag(true, item);
                            @char.Character.CharacterHandler.SendMessage(Service.SendBag(character));
                            @char.Character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được x" + listInput[2] + " " + ItemCache.ItemTemplate(item.Id).Name + " từ Admin"));
                        }
                        break;
                    case 1999: //đổi vnd sang vàng
                    {
                        // kiểm tra có phải là số không
                        int n;
                        bool isNumeric = int.TryParse(listInput[0], out n);
                        if (!isNumeric) 
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        var inputValue = Int32.Parse(listInput[0]);

                        if (inputValue < 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        // Kiểm tra có đủ VNĐ không
                        int vnd = UserDB.GetVND(character.Player);
                        if (vnd < inputValue)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_VND));
                            return;
                        }
                        // Kiểm tra giới hạn vàng trên người
                        long quyDoi = inputValue*550;
                        if (character.InfoChar.Gold + quyDoi > character.InfoChar.LimitGold)
                        {
                            var quyDoiToiDa = (character.InfoChar.LimitGold - character.InfoChar.Gold)/550;
                            character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().VND_TO_GOLD_LIMIT, ServerUtils.GetMoneys(quyDoiToiDa))));
                            return;
                        }
                        // Oke hết thì trừ VNĐ và cộng vàng
                        if (UserDB.MineVND(character.Player, inputValue))
                        {
                            character.PlusGold(quyDoi);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));

                            if (inputValue >= 20000 && !character.InfoChar.IsPremium)
                            {
                                character.InfoChar.IsPremium = true;
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().UPGRADE_TO_PREMIUM));
                            }
                        }
                        character.TypeInput = 0;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HanleInputClient in Service.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        
        public static void HandleNapThe(Model.Character.Character character, Message message)
        {
            var gender = character.InfoChar.Gender;
            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, string.Format("Hãy đến gặp {0} để nạp thẻ bạn nhé.", TextTask.NameNpc[gender]), false, gender));
        }
    }
}