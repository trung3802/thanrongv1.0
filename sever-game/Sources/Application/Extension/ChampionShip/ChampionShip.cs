using TienKiemV2Remastered.Application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using System.Threading;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23;
using TienKiemV2Remastered.Application.MainTasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Application.Extension.ChampionShip
{

    public class ChampionShip
    {
        public class DataCharacter
        {
            public int IdCharacterFight { get; set; }
            public long delayChangeTypePK { get; set; }
            public long delayNextRound { get; set; }
            public bool isWin { get; set; }
            public bool isVoDich { get; set; }
            public bool isFailed { get; set; }
            public int TypeCharacters { get; set; }
            public int Round { get; set; }
            public DataCharacter()
            {
                IdCharacterFight = -1;
                delayChangeTypePK = -1;
                delayNextRound = -1;
                isWin = false;
                isFailed = false;
                TypeCharacters = -1;
                Round = 0;
            }
        }

        public List<int> Characters1 = new List<int> { };
        public List<int> Characters2 = new List<int> { };
        public List<int> CharacterRegister = new List<int> { };
        public List<int> CharactersWon = new List<int> { };
        public long TimeStart = 1800000 + ServerUtils.CurrentTimeMillis();
        public List<List<Boss>> TrongTai = new List<List<Boss>> { new List<Boss> { }, new List<Boss> { } };
        public int typeChampion = 0;
        public bool Init = false;
        public int CurrentTimeInti = -1;
        public int CurrentMinuteInit = -1;
        // 1 = nhi dong && 2 ngoc && (8 gio || 13 gio || 18 gio)
        // 2 = sieu cap 1 && 4 ngoc && (9 gio || 14 gio || 19 gio) 
        // 3 = sieu cap 2 &&  6 ngoc && (10 gio || 15 gio || 20 gio)
        // 4 = sieu cap 3 && 8 ngoc (11 gio || 16 gio || 21 gio)
        // 5 = ngoai hang | 10.000 vang ( 12 gio || 17 gio || 22 gio || 23 gio)
        public int roundNow = 0;
        public static ChampionShip instance;
        public static ChampionShip gI()
        {
            if (instance == null)
            {
                instance = new ChampionShip();
            }
            return instance;
        }
        public void InitTrongTai()
        {
            
        }
        public long timeDelay = 0;
        public void InitDaiHoiVoThuat(long timeserver)
        {

            if (!Init && CheckTime() && CurrentTimeInti != ServerUtils.TimeNow().Hour && ServerUtils.TimeNow().Minute <= 30)
            {
                CurrentTimeInti = ServerUtils.TimeNow().Hour;
                timeDelay = 3600000 + ServerUtils.CurrentTimeMillis();
                IsStartSetCharacters = false;
                Characters1 = new List<int>();
                Characters2 = new List<int>();
                CharacterRegister = new List<int>();
                roundNow = 0;
                EndNow = false;
                typeChampion = SetTypeChampion();
                TimeStart = ((30 - ServerUtils.TimeNow().Minute) * 60000) + ServerUtils.CurrentTimeMillis();//1800000
                Update();

            }

        }
        //public void ClearRegister()
        //{
        //    for (int i = 0; i < CharacterRegister.Count; i++)
        //    {
        //        if (CharacterRegister[i]!=null) CharacterRegister[i] = null;
        //    }
        //}
       
        public Boolean IsFinishSetCharacters = false;
        public Boolean IsStartSetCharacters = false;

        public Boolean IsSetMatch = false;
        public long TimeMatch = 0 + ServerUtils.CurrentTimeMillis();
        public long TimeStartPvp = 15000 + ServerUtils.CurrentTimeMillis();
        public long TimeEnd = 300000 + ServerUtils.CurrentTimeMillis();
        public Boolean EndNow = false;
        public Boolean EndRound = false;
        public int TeamExist = 0;

        public bool EndBecausePlayerKillPlayer = false;
        public void Update()
        {
            new Thread(new ThreadStart(() =>
            {
                while (Server.Gi().IsRunning && !EndNow)
                {
                    var timeserver = ServerUtils.CurrentTimeMillis();
                    if (TimeStart <= timeserver && !IsStartSetCharacters)
                    {
                        
                            if (CharacterRegister.Count <= 0)
                            {
                                Close();
                                return;
                            }
                            SetCharacters();
                            
                    }
                    if (IsFinishSetCharacters && !IsSetMatch && TimeMatch < timeserver)
                    {
                        Match();
                        TimeEnd = 315000 + timeserver;
                    }
                    if ((TimeEnd <= timeserver && IsSetMatch) || EndBecausePlayerKillPlayer)
                    {
                        End();
                    }
                    
                    Thread.Sleep(1000);
                }
            })).Start();
        }
        public void Close()
        {
            EndNow = true;
            CharacterRegister.Clear();
            Characters1.Clear();
            Characters2.Clear();
            CharactersWon.Clear();
            Init = false;
        }
        public void End()
        {
            if (EndBecausePlayerKillPlayer) EndBecausePlayerKillPlayer = false;
            roundNow++;
            EndRound = true;
            IsSetMatch = false;
            TimeMatch = 120000 + ServerUtils.CurrentTimeMillis();
            IsStartSetCharacters = false;
            TimeStart = 60000 + ServerUtils.CurrentTimeMillis();
            Characters1.Clear();
            Characters2.Clear();
            
        }
        public void Verus()
        {
            for (int i = 0; i < GetCharactersCount(); i++)
            {
                var Char1 = GetCharacters1(i);
                var Char2 = GetCharacters2(i);
                if ((Char1 == null) || (Char1.InfoChar.IsDie) || (Char1.InfoChar.Hp <= 0) || (Char1.InfoChar.MapId != 113))
                {
                    CharactersWon.Add(Char2.Id);
                    Characters1.Remove(Char1.Id);
                    Characters2.Remove(Char2.Id);
                    Char2.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thắng vòng này, xin chờ tại đây ít phút để thi tiếp vòng sau"));
                    if (Char1 != null)
                    {
                        Char1.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thua, hẹn gặp lại ở giải sau"));
                    }
                    Dịch_chuyển_tới_map((Character)Char2, 52, i, 3);
                    return;
                }
                if ((Char2 == null) || (Char2.InfoChar.IsDie) || (Char2.InfoChar.Hp <= 0) || (Char2.InfoChar.MapId != 113))
                {
                    CharactersWon.Add(Char1.Id);
                    Characters1.Remove(Char1.Id);
                    Characters2.Remove(Char2.Id);
                    Char1.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thắng vòng này, xin chờ tại đây ít phút để thi tiếp vòng sau"));
                    if (Char2 != null)
                    {
                        Char2.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thua, hẹn gặp lại ở giải sau"));
                    }
                    Dịch_chuyển_tới_map((Character)Char1, 52, i, 3);
                    return;
                }
                
               
            }
        }
        public void Match()
        {
            TimeMatch = 300000 + ServerUtils.CurrentTimeMillis();
            for (int i = 0; i < GetCharactersCount(); i++)
            {
                
                var Char1 = GetCharacters1(i);
                var Char2 = GetCharacters2(i);
                Char1.DataDaiHoiVoThuat.IdCharacterFight = Char2.Id;
                Char2.DataDaiHoiVoThuat.IdCharacterFight = Char1.Id;
                Dịch_chuyển_tới_map((Character)Char1, 113, i, 1);
                Dịch_chuyển_tới_map((Character)Char2, 113, i, 2);
                Char1.DataDaiHoiVoThuat.delayChangeTypePK = 15000 + ServerUtils.CurrentTimeMillis();
                Char2.DataDaiHoiVoThuat.delayChangeTypePK = 15000 + ServerUtils.CurrentTimeMillis();
                async void TrongTaiChat()
                {
                    await Task.Delay(5000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"Trận đấu giữa {Char1.Name} và {Char2.Name} sắp diễn ra"));
                    await Task.Delay(5000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"Xin quý vị khán giả cho 1 tràng pháo tay cỗ vũ cho 2 đớ thủ nào"));
                    await Task.Delay(1000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"Mọi người hãy ổn định chỗ ngồi, trận đấu sẽ bắt đầu sau 3 giây nữa"));
                    await Task.Delay(1000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"3"));
                    await Task.Delay(1000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"2"));
                    await Task.Delay(1000);
                    TrongTai[1][i].CharacterHandler.SendZoneMessage(Service.PublicChat(TrongTai[1][i].Id, $"1"));
                    if (Char1 == null)
                    {
                        Char2.Zone.ZoneHandler.RemoveBoss(TrongTai[1][i]);
                    }else if (Char2 == null)
                    {
                        Char1.Zone.ZoneHandler.RemoveBoss(TrongTai[1][i]);
                    }
                }
                var task = new Task(TrongTaiChat);
                task.Start();
                if (i == GetCharactersCount() - 1)
                {
                    IsSetMatch = true;
                }

            }
        }
        public void SetPos1(Character character)
        {
            character.InfoChar.X = 242;
            character.InfoChar.Y = 264;
            character.CharacterHandler.SendZoneMessage(Service.SendPos(character, 1));
        }
        public void SetPos2(Character character)
        {
            character.InfoChar.X = 500;
            character.InfoChar.Y = 264;
            character.CharacterHandler.SendZoneMessage(Service.SendPos(character, 1));
        }
        public int SetTypeChampion()
            {
            var h = ServerUtils.TimeNow().Hour;
            if (h == 8 || h == 13 || h == 18)
            {
                return 1;
            }else if (h== 9 || h == 14 || h == 19)
            {
                return 2;
            }else if (h== 10 || h == 15 || h == 20)
            {
                return 3;
            }
            else if (h == 11 || h == 16 || h == 21) 
            {
                return 4;
            }else if (h == 12 || h == 17 || h == 22)
            {
                return 5;
            }
            return 0;
        }
        public String TextTypeChampion()
        {
            if (typeChampion == 1)
            {
                return "Nhi đồng";
            }
            else if (typeChampion == 2)
            {
                return "Siêu cấp 1";
            }
            else if (typeChampion == 3)
            {
                return "Siêu cấp 2";
            }
            else if (typeChampion == 4)
            {
                return "Siêu cấp 3";
            }
            else if (typeChampion == 5)
            {
                return "Ngoại hạng";
            }
            else
            {
                return "Null";
            }
        }

        public void SetCharacters()
        {
            IsStartSetCharacters = true;
          //  Server.Gi().Logger.Print("CharactersRegister Count: " + CharacterRegister.Count, "red");
            if (CharacterRegister.Count < 2)
            {
                // CANNOT SET CHARACTERS BECAUSE NOT ENOUGH CHARACTER REGISTER
                if (roundNow == 0)
                {
                    foreach (var id in CharacterRegister)
                    {
                        if (ClientManager.Gi().GetCharacter(id) != null)
                        {
                            ClientManager.Gi().GetCharacter(id).CharacterHandler.SendMessage(Service.ServerMessage("Không thể bắt đầu vì không đủ người !"));
                 //           Server.Gi().Logger.Print("Cannot Match Because Not Enough Player!", "red");
                        }
                    }
                }
                else
                {
                    foreach (var id in CharacterRegister)
                    {
                        if (ClientManager.Gi().GetCharacter(id) != null)
                        {
                            ClientManager.Gi().GetCharacter(id).CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng thèn ku đã Win, khá đếy nhót!"));
                         //   Server.Gi().Logger.Print("Character Win: " + id, "red");
                        }
                    }
                }
                IsFinishSetCharacters = false;
                Close();
                return;
            }
            var SetCharacter1Or2 = 0;
            for (int i = 0; i < CharacterRegister.Count; i++)
            {
                if (roundNow == 2)
                {
                    var ICharacter = (Character)ClientManager.Gi().GetCharacter(CharactersWon[i]);
                    if (TaskHandler.CheckTask(ICharacter, 19, 1))
                    {
                        TaskHandler.gI().PlusSubTask(ICharacter, 1);
                    }
                }
                if (SetCharacter1Or2 == 0)
                {
                    Characters1.Add(CharacterRegister[i]);
                    //CharacterRegister.Remove(CharacterRegister[i]);
                    Server.Gi().Logger.Print("Characters1 Add: " + CharacterRegister[i], "red");
                    SetCharacter1Or2 = 1;
                }
                else
                {
                    Characters2.Add(CharacterRegister[i]);
                    //CharacterRegister.Remove(CharacterRegister[i]);
                    Server.Gi().Logger.Print("Characters2 Add: " + CharacterRegister[i], "red");
                    SetCharacter1Or2 = 0;
                }
                if (i == CharacterRegister.Count - 1)
                {
                    IsFinishSetCharacters = true;
                    TeamExist = i;
                    if (Characters1.Count == Characters2.Count + 1)
                    {
                        var LuckyPlayer = ClientManager.Gi().GetCharacter(Characters1[Characters2.Count]);
                        if (LuckyPlayer!=null){
                        LuckyPlayer.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã được vào thẳng vòng trong !"));
                        }else{
                            CharacterRegister.Remove(Characters1[Characters2.Count]);
                        }
                    }
                    //CharacterRegister.Clear();
                }
                Thread.Sleep(60);
            }

        }
        
        public Boolean CheckIdRegister(int id)
        {
            if (CharacterRegister.Contains(id)) return true;
            return false;
        }
       
        public Boolean CheckIdCharacters(int id)
        {
            if (Characters1.Contains(id) || Characters2.Contains(id)) return true;
            return false;
        }
       
        public ICharacter GetCharacters1(int i)
        {
            return ClientManager.Gi().GetCharacter(Characters1[i]);
        }
        public ICharacter GetCharacters2(int i)
        {
            return ClientManager.Gi().GetCharacter(Characters2[i]);  
        }
        
        public int GetCharactersCount()
        {
            return Characters2.Count;
        }
        public Boolean canRegister(long power)
        {
            if ((typeChampion == 1 && power < 1500000) || (typeChampion == 2 && power < 15000000) || (typeChampion == 3 && power < 150000000) || (typeChampion == 4 && power < 1500000000) || (typeChampion == 5)) return true;
            return false;
        }
        public Boolean CheckTime()
        {
            var h = ServerUtils.TimeNow().Hour;
            if (h >= 8 && h <= 22 && TimeStart > ServerUtils.CurrentTimeMillis())
            {
                return true;
            }
            return false;
        }

        public void OpenMenuGhiDanh(Character character)
        {
            if (character.InfoChar.MapId == 129)
            {
                if (character.DataDaiHoiVoThuat23.ChestLevel == 0 || character.DataDaiHoiVoThuat23.isCollected)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, ChampionShip_23.Menu.Text[0], Application.Extension.ChampionShip.ChampionShip_23.Menu.TextMenu[0], character.InfoChar.Gender));
                }
                else
                {
                    var menu = new List<string>()
                    {
                       "Hướng dẫn thêm",
                       "Thi đấu\n"+1*character.DataDaiHoiVoThuat23.Count+" ngọc",
                       "Thi đấu\n"+50000*character.DataDaiHoiVoThuat23.Count+"\nvàng",
                       "Nhận\nthưởng\nRương cấp "+character.DataDaiHoiVoThuat23.ChestLevel,
                       "Về\nĐại hội\nvõ thuật"
                    };
                    
                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, ChampionShip_23.Menu.Text[0], menu, character.InfoChar.Gender));
                }
                character.TypeMenu = 0;
            }
            else
            {
                if (typeChampion != 0 || !EndNow)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, "Chào mừng bạn đến với Đại Hội Võ Thuật\nGiải " + TextTypeChampion() + " đang có " + CharacterRegister.Count + " người đăng ký thi đấu", new List<string> { "Thông tin\nChi tiết", "Đăng ký", "Giải\nSiêu Hạng", "Đại hội\nVõ Thuật Lần Thứ 23" }, character.InfoChar.Gender));
                }
                else
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, "Đã hết hạn đăng ký thi đấu, xin vui lòng chờ đến giải sau", new List<String> { "Thông tin\nChi tiết", "OK", "Giải\nSiêu hạng", "Đại hội\nVõ thuật\nLần thứ\n23" }, character.InfoChar.Gender));
                }
                character.TypeMenu = 0;
            }
        }
        public void HandlerMenu(Character character, short npcId, int select)
        {

            switch (character.TypeMenu)
            {
                case 6:
                    switch (select)
                    {
                        case 0:
                            if (character.AllDiamond() > 10000)
                            {
                                //TaskHandler.DoNextIndex(character);
                                character.MineDiamond(10000);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn còn thiếu {10000 - character.AllDiamond()} ngọc nữa"));
                            }
                            break;
                    }
                    break;
                case 0:

                    switch (select)
                    {
                        case 0:
                            if (character.InfoChar.MapId == 129)
                            {
                                //character.CharacterHandler.SendMessage(Service.OpenUiSay(23,Menu.textHuongDanThem));
                                
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Lịch thi đấu trong ngày:\nGiải nhi đồng: 8,14,18h\nGiải Siêu cấp 1: 9,13,19h\nGiải Siêu cấp 2: 10,15,20h\nGiải siêu cấp 3: 11,16,21h\nGiải ngoại hạng: 12,17,22,23h"));
                            }
                            character.TypeMenu = 0;
                            break;
                        case 1:
                            if (character.InfoChar.MapId == 129) // mine diamond dhvt 23
                            {
                                if (character.AllDiamond() < 1 * character.DataDaiHoiVoThuat23.Count)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ ngọc để đăng ký !"));
                                    return;
                                }
                                if (character.DataDaiHoiVoThuat23.Round >= 11)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã chiến thắng rồi, hãy quay lại vào ngày mai !"));

                                    return;
                                }
                                character.MineDiamond(1 * character.DataDaiHoiVoThuat23.Count);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.DataDaiHoiVoThuat23.Count++;
                                ChampionShip_23.ChampionShip_23.gI().Đấm_Nhau(character);
                            }
                            else
                            {
                                if (typeChampion != 0)
                                {
                                    if (typeChampion != 5)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, "Hiện đang có giải đấu " + TextTypeChampion() + " bạn có muốn đăng ký không?", new List<String> { "Giải\n" + TextTypeChampion() + "\n(" + Cost() + " ngọc)" }, character.InfoChar.Gender));
                                        character.TypeMenu = 1;
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, "Hiện đang có giải đấu " + TextTypeChampion() + " bạn có muốn đăng ký không?", new List<String> { "Giải\n" + TextTypeChampion() + "\n(" + Cost() + " vàng)" }, character.InfoChar.Gender));
                                        character.TypeMenu = 1;
                                    }
                                }
                            }
                            break;
                        case 2: // giai sieu hang
                            
                            if (character.InfoChar.MapId == 129) // mine gold dhvt 23
                            {
                                if (character.InfoChar.Gold < 50000 * character.DataDaiHoiVoThuat23.Count)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ vàng để đăng ký !"));
                                    return;
                                }
                                if (character.DataDaiHoiVoThuat23.Round >= 11)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã chiến thắng rồi, hãy quay lại vào ngày mai !"));

                                    return;
                                }
                                character.MineGold(50000 * character.DataDaiHoiVoThuat23.Count);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.DataDaiHoiVoThuat23.Count++;
                                ChampionShip_23.ChampionShip_23.gI().Đấm_Nhau(character);
                            }
                            break;
                        case 3: // dhvt lan thu 23
                            if (character.InfoChar.MapId == 129) // get chest dhvt 23
                            {
                                if (character.DataDaiHoiVoThuat23.ChestLevel != 0 && !character.DataDaiHoiVoThuat23.isCollected)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(23, string.Format(ChampionShip_23.Menu.Text[1], character.DataDaiHoiVoThuat23.ChestLevel), ChampionShip_23.Menu.TextMenu[2], character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                }
                                else
                                {
                                    MapManager.JoinMap(character, 52, ServerUtils.RandomNumber(100), false, false, 0);
                                }
                            }
                            else
                            {
                                MapManager.JoinMap(character, 129, ServerUtils.RandomNumber(20), false, false, 0);
                            }
                            break;
                    }
                    break;
                case 1: // confirm dang ky
                    if (typeChampion != 5)
                    {
                        if (character.AllDiamond() < Cost())
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ ngọc để đăng ký !"));
                            return;
                        }
                    }
                    else
                    {
                        if (character.InfoChar.Gold < Cost())
                        { 
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ vàng để đăng ký !"));
                            return;
                        }
                    }
                    Register(character);
                    break;
                case 2: // confirm get ruong thuong
                    if (select == 0)
                    {
                        var ruongGo = ItemCache.GetItemDefault(570);
                        ruongGo.Options.Add(new OptionItem()
                        {
                            Id = 72,
                            Param = character.DataDaiHoiVoThuat23.ChestLevel,
                        });
                        character.DataDaiHoiVoThuat23.isCollected = true;
                        character.CharacterHandler.AddItemToBag(false, ruongGo, "DHVT 23");
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn vừa nhận được Rương Gỗ Cấp " + character.DataDaiHoiVoThuat23.ChestLevel));
                    }
                    break;
            }
          
        }
        public int Cost()
        {
            if (typeChampion == 5)
            {
                return 10000;
            }
            return 2 * typeChampion;
        }
        
        public void Register(Character character)
        {
            if (!CheckTime())
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Đã hết hạn đăng ký thi đấu, xin vui lòng chờ đến giải sau !"));
                return;
            }
            else if (CheckIdRegister(character.Id))
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn đã đăng ký rồi !"));
                return;
            }
            else if (!canRegister(character.InfoChar.Power))
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Sức mạnh của bạn đã vượt ngưỡng giới hạn của giải đấu, không thể đăng ký!"));
                return;
            }
            else
            {
                CharacterRegister.Add(character.Id);                
                if (typeChampion != 5)
                {
                    character.MineDiamond(Cost());
                }
                else
                {
                    character.MineGold(Cost());
                }
                character.DataDaiHoiVoThuat.isWin = false;
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Chúc mừng bạn đã đăng ký thành công!"));
                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
            }
        }

        public void KillPlayer(ICharacter character, ICharacter plKill)
        {
            TeamExist -= 1;
            if (TeamExist == 0)
            {
                EndBecausePlayerKillPlayer = true;
            }
            CharacterRegister.Remove(plKill.Id);
            plKill.DataDaiHoiVoThuat.delayNextRound = 5000 + ServerUtils.CurrentTimeMillis();
            if (CharacterRegister.Count == 1)
            {
                character.DataDaiHoiVoThuat.delayNextRound = 0 + ServerUtils.CurrentTimeMillis();
                character.DataDaiHoiVoThuat.isVoDich = true;
                Close();

            }
            else
            {
                character.DataDaiHoiVoThuat.delayNextRound = 5000 + ServerUtils.CurrentTimeMillis();
                character.DataDaiHoiVoThuat.isWin = true;
            }
            character.Zone.ZoneHandler.SendMessage(Service.HideNpc(59));
            character.CharacterHandler.SendMessage(Service.ServerMessage("Đối thủ đã kiệt sức, bạn đã thắng"));
            character.Zone.ZoneHandler.SendMessage(Service.NpcChat(59, $"Đối thủ kiệt sức, {character.Name} đã thắng"));
            SetTypeCombat(character, 0);
            SetTypeCombat(plKill, 0);
            plKill.DataDaiHoiVoThuat.isFailed = true;
            plKill.DataDaiHoiVoThuat.IdCharacterFight = -1;
            character.DataDaiHoiVoThuat.IdCharacterFight = -1;
            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa nhận thưởng {Cost()} {(typeChampion != 5 ? "ngọc" : "vàng")}"));
            if (typeChampion != 5) character.CharacterHandler.PlusDiamondLock(Cost());
            else character.PlusGold(Cost());
        }
        public void WinRound(Character character)
        {
            var trongtai = TrongTai[1][character.Zone.Id];
            character.DataDaiHoiVoThuat.delayChangeTypePK = 5000 + ServerUtils.CurrentTimeMillis();
            character.DataDaiHoiVoThuat.isWin = true;
            character.Zone.ZoneHandler.AddBoss(trongtai);
            character.CharacterHandler.SendMessage(Service.ServerMessage("Đối thủ đã bỏ trốn, bạn đã thắng"));
            trongtai.CharacterHandler.SendZoneMessage(Service.PublicChat(trongtai.Id, $"Đối thủ bỏ trốn, {character.Name} đã thắng"));
            SetTypeCombat(character, 0);
            character.DataDaiHoiVoThuat.IdCharacterFight = -1;
            CharactersWon.Add(character.Id);
            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa nhận thưởng {Cost()} {(typeChampion != 5 ? "ngọc" : "vàng")}"));
            if (typeChampion != 5) character.CharacterHandler.PlusDiamondLock(Cost());
            else character.PlusGold(Cost());
        }
        public void Dịch_chuyển_tới_map(Character character,int idMap, int zone, int player)
        {

            if (player == 1)
            {
                character.InfoChar.X = 242;
                character.InfoChar.Y = 264;
                MapManager.JoinMap(character, idMap, zone, false, false, 0);
            }
            else if (player == 2)
            {
                character.InfoChar.X = 500;
                character.InfoChar.Y = 264;
                MapManager.JoinMap(character, idMap, zone, false, false, 0);
            }
            else
            {
                MapManager.JoinMap(character, idMap, zone, false, false, 0);
            }
            
        }       
        public void SetTypeCombat(ICharacter character, sbyte typePk)
        {
            character.InfoChar.TypePk = typePk;
            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, typePk));
        }
        
    }
}
