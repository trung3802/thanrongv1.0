using System;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using System.Data.Common;
using Newtonsoft.Json;
using System.Threading;
using TienKiemV2Remastered.DatabaseManager.Player;

namespace TienKiemV2Remastered.Application.Extension.BlackballWar
{
    public class Blackball
    {
        public string Status = "END";
        public readonly List<int> ListNRSD = new List<int> { 372,373,374,375,376,377,378};
        public readonly List<int> ListMapNRSD = new List<int> { 85, 86, 87, 88, 89, 90, 91 };
        public readonly String textWaitForPick = "Chưa thể nhặt lúc này,hãy đợi {0} giây nữa" ;
        public readonly String textWaitToWin = "Cố giữ ngọc thêm {0} giây nữa sẽ thắng";
        public readonly String textClanReward = "Chúc mừng bạn đã dành được Ngọc Rồng {0} sao đen cho Bang";
        public readonly String textEnd = "Trò chơi đã kết thúc.Hẹn gặp lại vào 20h ngày mai ";
        public readonly List<int> CostToPlusHp = new List<int>(){10,20,30};
        public readonly List<int> PercentPlusHp = new List<int>(){3,5,7};
        public long currTimeEndBlackBall = 21;
        public long currTimeStartBlackBall = 20;
        public long currMiliTimeStartBlackBall = -1;
        public static Blackball instance;
        public static Blackball gI(){
            if (instance == null) instance = new Blackball();
            return instance;
        }
        public Blackball()
        {

        }
    }
    public class BlackBallRuntime{
        public static void CurrentRunTime(){
            var timeNow = ServerUtils.TimeNow().Hour;
            if (timeNow == Blackball.gI().currTimeEndBlackBall && Blackball.gI().Status == "START"){
                Blackball.gI().Status = "END";
            }else if (timeNow == Blackball.gI().currTimeStartBlackBall && Blackball.gI().Status == "END"){
                Blackball.gI().Status = "START";
                BlackBallHandler.ForServer.gI().InitBlackball();
                Blackball.gI().currMiliTimeStartBlackBall = ServerUtils.CurrentTimeMillis();
            }
        }
    }

    public class BlackBallHandler
    {
        public class ForPlayer2
        {

        }
        public class ForPlayer
        {

            public int CurrentBlackball { get; set; }
            public List<int> CurrentListBuff{get;set;}
            public int CurrentPercentPlusHp { get; set; }
            public long DelayCollectBlackball { get; set; }
            public long DelaySendMessage { get; set; }
            public static ForPlayer instance;
            public static ForPlayer gI()
            {
                if (instance == null) instance = new ForPlayer();
                return instance;
            }
            public ForPlayer()
            {
                DelaySendMessage = 10000 + ServerUtils.CurrentTimeMillis();
                DelayCollectBlackball = -1;
                CurrentBlackball = -1;
                CurrentListBuff = new List<int>();
                
                CurrentPercentPlusHp = -1;
            }
            public Boolean AlreadyPick(Character character)
            {
                return character.Blackball.CurrentBlackball != -1;
            }
            public Boolean AlreadyPlusHp(Character character)
            {
                return character.Blackball.CurrentPercentPlusHp != -1;
            }
            
           
            public void PickBlackball(Character character, int itemId){
                var timeserver = ServerUtils.CurrentTimeMillis();
                
                character.Blackball.CurrentBlackball = itemId - 371;
                character.Blackball.DelayCollectBlackball = timeserver + 300000;
                character.Blackball.DelaySendMessage = timeserver + 10000;
                var second = (character.Blackball.DelayCollectBlackball - timeserver) / 1000;
                character.CharacterHandler.SendMessage(Service.ServerMessage($"{string.Format(Blackball.gI().textWaitToWin, second)}"));
                character.InfoChar.Bag = 107;
                character.CharacterHandler.UpdatePhukien();
                character.InfoChar.TypePk = 5;
                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 5));
            }
            public void ExitMapOrDie(Character character)
            {
                character.Blackball.CurrentBlackball = -1;
                character.Blackball.CurrentPercentPlusHp = -1;
                character.InfoChar.TypePk = 0;
                if (AlreadyPick(character))
                {
                    var Blackball2 = ItemCache.GetItemDefault((short)Blackball.gI().ListNRSD[character.Blackball.CurrentBlackball - 1]);
                    var blackball = new ItemMap(-1, Blackball2);
                    blackball.X = character.InfoChar.X;
                    blackball.Y = character.InfoChar.Y;
                    character.Zone.ZoneHandler.LeaveItemMap(blackball);
                    character.InfoChar.Bag = (sbyte)(ClanManager.Get(character.ClanId) != null ? character.InfoChar.Bag : -1);
                   
                    character.CharacterHandler.UpdatePhukien();
                     character.CharacterHandler.SendZoneMessage(character.ClanId == -1 || character.ClanId != -100
                    ? Service.SendImageBag(character.Id, -1)
                    : Service.SendImageBag(character.Id, character.InfoChar.Bag));
                }
                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
            }
            public void Runtime(Character character, long timeserver)
            {
                if (Blackball.gI().Status == "END")
                {
                    if (character.Blackball.AlreadyPick(character))
                    {
                        ForClan.ClanManagerr.gI().Insert(character, character.Blackball.CurrentBlackball);
                    }
                    character.CharacterHandler.SendMessage(Service.ServerMessage(Blackball.gI().textEnd));
                    ExitMapOrDie(character);
                    MapManager.JoinMap(character, 24 + character.InfoChar.Gender, ServerUtils.RandomNumber(19), true, true, character.TypeTeleport);
                    Server.Gi().Logger.Print("blackball status: end");
                }
                else
                {
                    if (AlreadyPick(character))
                    {
                    if (character.Blackball.DelayCollectBlackball < timeserver)
                    {
                          
                        character.CharacterHandler.SendMessage(Service.ServerMessage($"{string.Format(Blackball.gI().textClanReward, character.Blackball.CurrentBlackball)}"));
                        ForClan.ClanManagerr.gI().Insert(character, character.Blackball.CurrentBlackball);
                        MapManager.JoinMap(character, 24 + character.InfoChar.Gender, ServerUtils.RandomNumber(19), true, true, character.TypeTeleport);
                        ExitMapOrDie(character);
                    }
                    if (character.Blackball.DelaySendMessage < timeserver)
                        {
                            var second = (character.Blackball.DelayCollectBlackball - timeserver) / 1000;
                            character.CharacterHandler.SendMessage(Service.ServerMessage($"{string.Format(Blackball.gI().textWaitToWin, second)}"));
                            character.Blackball.DelaySendMessage = 10000 + timeserver;

                        }
                    }
                }
            }
        }
        public class ForServer
        {
            public static ForServer instance;
            public static ForServer gI(){
                if (instance == null) instance = new ForServer();
                return instance;
            }
            public void InitBlackball(){
                Application.Threading.Map mapInit;
                var item = ItemCache.GetItemDefault(372);
                for (int i = 0; i < 7; i++)
                    {
                        mapInit = MapManager.Get(85 + i);
                        for (int zone = 0; zone < 12; zone++)
                            {
                                var zoneInit = mapInit.GetZoneById(zone);
                                item = ItemCache.GetItemDefault((short)(372 + i));
                                if (zoneInit.ZoneHandler.GetItemMapsByID(item.Id).Count>=1){
                                    for (int nrsd = 0; nrsd < zoneInit.ZoneHandler.GetItemMapsByID(item.Id).Count;nrsd++){
                                    zoneInit.ZoneHandler.RemoveItemMap(zoneInit.ZoneHandler.GetItemMapsByID(item.Id)[nrsd].Id);
                                    Thread.Sleep(50);
                                    }
                                }
                                if (i == 3)
                                {
                                    zoneInit.ItemMaps.TryAdd(0, new ItemMap(-1)
                                    {
                                        Id = 0,
                                        PlayerId = -1,
                                        Item = item,
                                        X = 1031,
                                        Y = 336,
                                    });
                                }
                                else
                                {
                                    zoneInit.ItemMaps.TryAdd(0, new ItemMap(-1)
                                    {
                                        Id = 0,
                                        PlayerId = -1,
                                        Item = item,
                                        X = 1031,
                                        Y = 360,
                                    });
                                }
                            }
                    }
            }
        }
        public class ForOther
        {

        }
        public class ForClan
        {

            public class ClanDatabase
            {
                public static ClanDatabase instance;
                public static ClanDatabase gI()
                {
                    if (instance == null) instance = new ClanDatabase();
                    return instance;
                }
                public static void Update(int ClanId)
                {
                    lock (Server.SQLLOCK)
                    {

                        var clan = Application.Manager.ClanManager.Get(ClanId);
                        var text = $"`DataBlackball` = '{JsonConvert.SerializeObject(clan.DataBlackBall)}'";
                        DbContext.gI()?.ConnectToAccount();
                        using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                        if (command == null) return;
                        command.CommandText = $"UPDATE `clan` SET {text}  WHERE `id` = {ClanId};";
                        command.ExecuteNonQuery();
                        DbContext.gI()?.CloseConnect();
                    }
                }
            }
            public class ClanManagerr
            {
                public List<int> ListCurrentBlackball { get; set; }
                public static ClanManagerr instance;
                public static ClanManagerr gI()
                {
                    if (instance == null) instance = new ClanManagerr();
                    return instance;
                }
                public ClanManagerr()
                {
                    ListCurrentBlackball = new List<int>();
                   
                }
                public void Insert(Character character,int ball)
                {
                    var clan = Manager.ClanManager.Get(character.ClanId);
                        if (ball == -1) return;
                        clan.DataBlackBall.ListCurrentBlackball.Add(ball);
                    ClanDatabase.Update(clan.Id);
                }
            

            }
        }
            public class ForNpc
            {
                public class Omega_Dragon
                {
                public static List<List<String>> Menus = new List<List<String>>()
                {
                    new List<String> { "Hướng dẫn\nthêm", "Tham gia", "Từ chối" },
                    new List<String> { "Hướng dẫn\nthêm", "Từ chối" },
                };
                public static String Tutorial = "Mỗi ngày từ 20h -> 21h các hành tinh có Ngọc Rồng Sao Đen sẽ xảy ra 1 cuộc đại chiến\nNgười nào tìm thấy và giữ được Ngọc Rồng Sao Đen sẽ mang phần thưởng về cho bang của mình trong 1 ngày"
                                        + "\nLưu ý mỗi bang có thể chiếm hữu nhiều viên khác nhau nhưng nếu cùng loại cũng chỉ nhận được 1 lần phần thưởng đó.\nCó 2 cách để thắng:\n1) Giữ ngọc sao đen trong 5 phút\n2)Sau 30 phút tham gia tàu sẽ đón về và đang giữ ngọc sao đen trên người\n"
                                        + "Các phần thưởng như sau:\n1 sao đen : +20% sức đánh toàn bang\n2 sao đen: +20% Hp toàn bang\n3 sao đen: +20% Ki toàn bang\n4 sao đen: +20% giáp cho toàn bang\n 5 sao đen: + 14% né cho toàn bang\n6 sao đen: +35% tiềm năng sức mạnh nhận được cho toàn bang\n7 sao đen: +35% tiềm năng sức mạnh nhận được cho toàn bang";
                public static void OpenMenuOmega_Dragon(Character character, int npcId)
                {
                    var now = ServerUtils.TimeNow().Hour;
                    var @char = character.CharacterHandler;
                    if (now == Blackball.gI().currTimeStartBlackBall && character.ClanId != -1)
                    {
                        @char.SendMessage(Service.OpenUiConfirm((short)npcId, "Đường đến với ngọc rồng sao đen đã mở, ngươi có muốn tham gia không?", Menus[0] , character.InfoChar.Gender));
                        character.TypeMenu = 0;
                    }
                    else if (now == 21 && character.ClanId != -1)
                    {
                        var clan = Application.Manager.ClanManager.Get(character.ClanId);
                        if (clan.DataBlackBall.ListCurrentBlackball.Count >= 1)
                        {
                            var menu = new List<string>();
                            character.ListCollectBlackBall.Clear();
                            for (int i = 0; i < clan.DataBlackBall.ListCurrentBlackball.Count; i++)
                            {
                                if (!character.Blackball.CurrentListBuff.Contains(clan.DataBlackBall.ListCurrentBlackball[i]))
                                {
                                    menu.Add($"Ngọc rồng\n{clan.DataBlackBall.ListCurrentBlackball[i]} sao");
                                    character.ListCollectBlackBall.Add(clan.DataBlackBall.ListCurrentBlackball[i]);
                                }
                            }
                            @char.SendMessage(Service.OpenUiConfirm((short)npcId, "Bang hội của ngươi có vài phần thưởng này!\nNgươi có muốn nhận không?", menu, character.InfoChar.Gender));
                            character.TypeMenu = 1;
                        }
                        else
                        {
                            @char.SendMessage(Service.OpenUiConfirm((short)npcId, "Ta có thể giúp gì cho ngươi?", Menus[1], character.InfoChar.Gender));
                            character.TypeMenu = 2;
                        }
                    }
                    else
                    {
                        @char.SendMessage(Service.OpenUiConfirm((short)npcId, "Ta có thể giúp gì cho ngươi?", Menus[1], character.InfoChar.Gender));
                        character.TypeMenu = 2;
                    }
                }
                
                public static void Confirm(Character character, int npcId, int select){
                    switch(character.TypeMenu){
                        case 0:
                        switch(select){
                            case 0: // huong dan them
                                    
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)npcId, Tutorial));
                                break;
                            case 1: // open menu capsule 
                                character.SetMapNRSD();
                                character.CharacterHandler.SendMessage(Service.MapTranspot(character.MapTranspots));
                                break;
                        }
                            break;
                        case 1:
                        var clan = Application.Manager.ClanManager.Get(character.ClanId);
                            character.Blackball.CurrentListBuff.Add(character.ListCollectBlackBall[select]);
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được Ngọc Rồng" + character.ListCollectBlackBall[select] + " sao"));
                        break;
                    }
                }
                }
                public class Rong_nSaoDen
                {
                public static List<List<String>> Menus = new List<List<String>>()
                {
                    new List<String> { "Phù hộ", "Từ chối" },
                    new List<String> { "X3 HP\n10 Ngọc", "X5 HP\n20 Ngọc", "X7 HP\n30 Ngọc", "Từ chối" },
                    new List<String> {  "Từ chối" },
                };
                public static void OpenMenuRong_nSaoDen(Character character, int npcId){
                        if (character.Blackball.AlreadyPick(character))
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm((short)npcId, "Ta có thể giúp gì cho ngươi?", Menus[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;

                        }else{
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm((short)npcId, "Ta có thể giúp gì cho ngươi?", Menus[2], character.InfoChar.Gender));
                            character.TypeMenu = 2;
                        }
                    }
                    public static void ConfirmRong_nSaoDen(Character character, int npcId, int select){
                    switch (character.TypeMenu)
                    {
                        case 0:
                            switch (select)
                            {
                                case 0:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm((short)npcId, "Ta sẽ giúp ngươi tăng HP và KI lên mức kinh hoàng,ngươi hãy chọn đi", Menus[1], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                            }
                            break;
                        case 1:
                            switch (select)
                            {
                                case 0:
                                case 1:
                                case 2:
                                    if (character.AllDiamond() < Blackball.gI().CostToPlusHp[select])
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn còn thiếu " + (Blackball.gI().CostToPlusHp[select] - character.AllDiamond()) + " ngọc nữa !"));
                                        return;
                                    }
                                    if (character.Blackball.CurrentPercentPlusHp == Blackball.gI().PercentPlusHp[select]){
                                                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi đã phù hộ hủy diệt rồi !"));
                                        return;
                                    }
                                    character.MineDiamond(Blackball.gI().CostToPlusHp[select]);
                                    character.Blackball.CurrentPercentPlusHp = Blackball.gI().PercentPlusHp[select];
                                    character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                    character.CharacterHandler.SetUpInfo();
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Hủy diệt tụi nó thôi nào ehehe !"));
                                    break;

                            }
                            break;
                    }
                    }
                }
            }
        
    }
}