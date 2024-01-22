using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.DatabaseManager.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sources.Database;

namespace TienKiemV2Remastered.Application.Extension
{
    public class MenuAdminRecode
    {
        public String NameCharSelect = "Unknow";
        public int IdCharSelect;
        public Character CharSelect;
        public static MenuAdminRecode instance;
        public static MenuAdminRecode gI()
        {
            if (instance == null)
            {
                instance = new MenuAdminRecode();
            }
            return instance;
        }
        public void CheckGiftcode(Character character,string giftcode)
        {
            ////character.CharacterHandler.SendMeMessage(Service.OpenUiConfirm(64, "|0|Giftcode Text: [" + giftcode + "]\n|7|Còn : " + checkCount + 
            ////   " lượt nhập nữa !\nTime Expire: [Không Khả Dụng]\nSTATUS: {OFF}", MenuNpc.gI().MenuGokuVoThan[2], character.InfoChar.Gender));
            //if (!GiftcodeDataBase.GetCode(giftcode))
            //{
            //    character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy !"));
            //    return;
            //}
            //var checkCount = GiftcodeDataBase.GetCount(giftcode);
            //var checkTimeExpire = GiftcodeDataBase.GetTimeExpire(giftcode);
            //var item = GiftcodeDataBase.GetItem(giftcode);
            //var gold = GiftcodeDataBase.GetThoiVang(giftcode);
            //var gem = GiftcodeDataBase.GetGem(giftcode);
            //var ruby = GiftcodeDataBase.GetRuby(giftcode);
            //var text = "";
            //text += "|0|Info Giftcode: " + giftcode + "\n";
            //text += "|7|Lượt nhập: " + checkCount + "\n";
            //text += "|7|HSD: " + checkTimeExpire + "\n";
            //for (int i = 0; i < item.Count; i++)
            //{
            //    var ItemDefault = ItemCache.GetItemDefault(item[i].Id, item[i].Quantity);
            //    var template = ItemCache.ItemTemplate(ItemDefault.Id);
            //    var isTypeBody = template.IsTypeBody();
            //    text += $"|2|{(isTypeBody ? "" : item[i].Quantity + " ")}{template.Name}\n";
                
            //}
            //if (gold != -1 || gold != 0)
            //{
                
            //    text += $"|1|{gold} thỏi vàng\n";
            //}
            //if (gem != -1 || gem != 0)
            //{
            //    text += $"|1|{gem} Ngọc xanh\n";
            //}
            //if (ruby != -1 || ruby != 0)
            //{
            //    text += $"|1|{ruby} Hồng ngọc\n";
            //}
            //character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, text, new List<string> { "OK" }, 3));
            //character.TypeMenu = 11;
        }
        public Character GetCharSelectById()
        {
            if (ClientManager.Gi().GetPlayer(IdCharSelect) == null) return null;
            return (Character)ClientManager.Gi().GetPlayer(IdCharSelect).Character;
        }
        public Character GetCharSelectByUserName()
        {
            if (ClientManager.Gi().GetPlayerByUserName(NameCharSelect) == null) return null;
            return (Character)ClientManager.Gi().GetPlayerByUserName(NameCharSelect).Character;
        }
        public void TeleportCharSelectToMe(Character me)
        {
            var getMapMe = me.InfoChar.MapId;
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            getCharSelect.InfoChar.X = me.InfoChar.X;
            getCharSelect.InfoChar.Y = me.InfoChar.Y;
            MapManager.JoinMap((Character)getCharSelect, getMapMe, me.Zone.Id, false, false, 0);
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("YOU HAVE BEEN TELEPORT TO ADMIN !"));
        }
        public void TeleportMeToCharSelect(Character me)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            MapManager.JoinMap(me, getCharSelect.InfoChar.MapId, getCharSelect.Zone.Id, false, false, 0);
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("TELEPORT TO "+getCharSelect.Name+""));
        }
        public void BuffTask(int id = 0,int index=0,int count=0)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            getCharSelect.InfoTask.Id = (short)id;
            getCharSelect.InfoTask.Index = (sbyte)index;
            getCharSelect.InfoTask.Count = (short)count;
            getCharSelect.CharacterHandler.SendMessage(Service.SendTask(getCharSelect));
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("CHANGE TASK [ID: "+id+" - INDEX: " + index+" - COUNT: " + count +"]"));
        }
        public void BuffItem(int id,int quantity)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            var item = ItemCache.GetItemDefault((short)id);
            var isUpToUp = false;
            item.Quantity = quantity;
            if (quantity > 99)
            {
                isUpToUp = true;
            }
            getCharSelect.CharacterHandler.AddItemToBag(isUpToUp, item);
            getCharSelect.CharacterHandler.SendMessage(Service.SendBag(getCharSelect));
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được x"+quantity+" " + ItemCache.ItemTemplate(item.Id).Name));
        }
        public void CallBoss(int id)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            var boss = new Boss();
            var bossTemp = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(i => i.Id == id);
            boss.CreateBoss(id, getCharSelect.InfoChar.X, getCharSelect.InfoChar.Y);
            boss.CharacterHandler.SetUpInfo();
            getCharSelect.Zone.ZoneHandler.AddBoss(boss);
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("ADD BOSSES [ID:"+id+" - NAME: "+ bossTemp.Name));
        }
        public void BuffMoney(int money)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            UserDB.PlusVND(GetCharSelectById().Player, money);
            getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("PLUS MONEY SUCCESS [VALUE: "+money+"]"));
        }
        public void HandlerBanAccount(string reason)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            UserDB.BanUser(getCharSelect.Player.Id);
            ClientManager.Gi().SendMessageCharacter(Service.ServerChat("Nhân vật " + NameCharSelect + " đã bị khóa tài khoản với lý do: " + reason));
            ClientManager.Gi().KickSession(getCharSelect.Player.Session);
        }
        public void HandlerPlusOrignalPoint(int type,long hp, long mp, long damage, long amor, long crit)
        {
            var getCharSelect = ClientManager.Gi().GetPlayerByUserName(NameCharSelect).Character;
            if (type == 0) // set
            {
                getCharSelect.InfoChar.Hp = hp;
                getCharSelect.InfoChar.Mp = mp;
                getCharSelect.InfoChar.OriginalDamage = (short)damage;
                getCharSelect.InfoChar.OriginalDefence = (short)amor;
                getCharSelect.InfoChar.OriginalCrit = (short)crit;
            }
            if (type == 1) // plus
            {
                getCharSelect.InfoChar.Hp += hp;
                getCharSelect.InfoChar.Mp += mp;
                getCharSelect.InfoChar.OriginalDamage += (short)damage;
                getCharSelect.InfoChar.OriginalDefence += (short)amor;
                getCharSelect.InfoChar.OriginalCrit += (short)crit;
            }
        }
        public void BuffPotenial(int type,long Value)
        {
            var getCharSelect = ClientManager.Gi().GetCharacter(NameCharSelect);
            var tiemnang = Value;
            if (type == 0) // buff suc manh
            {
                getCharSelect.CharacterHandler.PlusPower(tiemnang);
                getCharSelect.CharacterHandler.SendMessage(Service.UpdateExp(0, tiemnang));
                getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("BUFF POWER SUCCESS [VALUE: " + Value + "]"));
            }
            if (type == 1) // buff tiem nang
            {
                getCharSelect.CharacterHandler.PlusPotential(tiemnang);
                getCharSelect.CharacterHandler.SendMessage(Service.UpdateExp(1, tiemnang));
                getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("BUFF POTENIAL SUCCESS [VALUE: " + Value + "]"));
            }
            if (type == 2)
            {
                getCharSelect.CharacterHandler.PlusPotential(tiemnang);
                getCharSelect.CharacterHandler.PlusPower(tiemnang);
                getCharSelect.CharacterHandler.SendMessage(Service.UpdateExp(2, tiemnang));
                getCharSelect.CharacterHandler.SendMessage(Service.ServerMessage("BUFF POTENIAL + POWER SUCCESS [VALUE: " + Value + "]"));
            }
        }
    }
}
