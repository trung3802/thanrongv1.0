using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Interfaces.Character;

namespace TienKiemV2Remastered.Application.MainTasks
{
    public class TaskHandler
    {
        // public void ContentInfo()
        //{
        //    if (text.StartsWith("#")) when next index
        //    {
        //        text = NinjaUtil.replace(text, "#", string.Empty);
        //        Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[myCharz().cgender][2]);
        //        npc.cx = (npc.cy = -100);
        //        npc.avatar = GameScr.info1.charId[myCharz().cgender][2];
        //        npc.charID = 5;
        //        if (GameCanvas.currentScreen == GameScr.instance)
        //        {
        //            ChatPopup.addNextPopUpMultiLine(text, npc);
        //        }
        //    }
        //    else if (isNextStep) when next task id
        //    {
        //        GameScr.info1.addInfo(text, 0);
        //    }
        //}
        public static TaskHandler taskHandler;
        public static TaskHandler gI()
        {
            if (taskHandler == null) taskHandler = new TaskHandler();
            return taskHandler;
        }
        public Message UpdateIndex(ICharacter character)
        {
            var msg = new Message(41);
            character.CharacterHandler.SendMessage(msg);
            return msg;
        }
        public void UpdateTask(ICharacter character) // load lại nhiệm vụ 
        {
            character.CharacterHandler.SendMessage(Service.SendTask(character));
        }

        public Message UpdateCount(ICharacter character)
        {
            var msg = new Message(43);
            msg.Writer.WriteShort(character.InfoTask.Count);
            character.CharacterHandler.SendMessage(msg);
            return msg;
        }
        public void RewardDoneTask(ICharacter character)
        {

        }

        public void CheckTaskDoneKillMob(ICharacter character, MonsterMap monster)
        {
            int mobId = monster.Id;
            switch (character.InfoChar.Gender)
            {
                case 0:
                    switch (mobId, character.InfoTask.Id, character.InfoTask.Index)
                    {
                        #region RequestMemberClan
                        case (16, 14, 0):
                        case (17, 14, 1):
                        case (18, 14, 2):// heo rung
                        case (22, 16, 1):
                        case (23, 16, 2):
                        case (24, 16, 3): // bulon
                        case (60, 26, 3): // xen 3
                        case (62, 28, 4)://xen 5
                        case (65, 29, 4):// xen 8

                            if (character.Zone.ZoneHandler.GetCharacterClanInMap(character.ClanId).Count >= TaskData.N_PLAYERCLAN)
                            {
                                PlusSubTask(character, 2);
                            }
                            else
                            {
                                PlusSubTask(character, 1);
                            }
                            break;
                        #endregion

                        #region NotRequestMemberClan
                        case (0, 1, 0):
                        case (4, 4, 0):
                        case (5, 4, 1):
                        case (6, 4, 2):
                        case (7, 8, 1):
                        case (27, 18, 1):
                        case (25, 18, 2):
                        case (26, 18, 3):
                        case (39, 21, 1):
                        case (40, 21, 2):
                        case (41, 21, 3):
                        case (42, 21, 4):
                        case (43, 21, 5)://nappa
                        case (58, 25, 4): // xen 1
                        case (80, 32, 3) or (80, 32, 8):
                        case (81, 32, 3) or (81, 32, 8):
                            PlusSubTask(character, 1);
                            break;
                            #endregion
                    }
                    break;
                case 1:
                    switch (mobId, character.InfoTask.Id, character.InfoTask.Index)
                    {
                        #region RequestMemberClan
                        case (16, 14, 0):
                        case (17, 14, 1):
                        case (18, 14, 2):// heo rung
                        case (22, 16, 1):
                        case (23, 16, 2):
                        case (24, 16, 3): // bulon
                        case (60, 26, 3): // xen 3
                        case (62, 28, 4)://xen 5
                        case (65, 29, 4):// xen 8
                            if (character.Zone.ZoneHandler.GetCharacterClanInMap(character.ClanId).Count >= TaskData.N_PLAYERCLAN)
                            {
                                PlusSubTask(character, 2);
                            }
                            else
                            {
                                PlusSubTask(character, 1);
                            }
                            break;
                        #endregion

                        #region NotRequestMemberClan
                        case (0, 1, 0):
                        case (5, 4, 0):
                        case (6, 4, 1):
                        case (4, 4, 2):
                        case (8, 8, 1):
                        case (27, 18, 1):
                        case (25, 18, 2):
                        case (26, 18, 3):
                        case (39, 21, 1):
                        case (40, 21, 2):
                        case (41, 21, 3):
                        case (42, 21, 4):
                        case (43, 21, 5)://nappa
                        case (58, 25, 4): // xen 1
                        case (80, 32, 3) or (80, 32, 8):
                        case (81, 32, 3) or (81, 32, 8):
                            PlusSubTask(character, 1);
                            break;
                            #endregion
                    }
                    break;
                case 2:
                    switch (mobId, character.InfoTask.Id, character.InfoTask.Index)
                    {
                        #region RequestMemberClan
                        case (16, 14, 0):
                        case (17, 14, 1):
                        case (18, 14, 2):// heo rung
                        case (22, 16, 1):
                        case (23, 16, 2):
                        case (24, 16, 3): // bulon
                        case (60, 26, 3): // xen 3
                        case (62, 28, 4)://xen 5
                        case (65, 29, 4):// xen 8
                            if (character.Zone.ZoneHandler.GetCharacterClanInMap(character.ClanId).Count >= TaskData.N_PLAYERCLAN)
                            {
                                PlusSubTask(character, 2);
                            }
                            else
                            {
                                PlusSubTask(character, 1);
                            }
                            break;
                        #endregion

                        #region NotRequestMemberClan
                        case (0, 1, 0):
                        case (6, 4, 0):
                        case (4, 4, 1):
                        case (5, 4, 2):
                        case (9, 8, 1):
                        case (27, 18, 1):
                        case (25, 18, 2):
                        case (26, 18, 3):
                        case (39, 21, 1):
                        case (40, 21, 2):
                        case (41, 21, 3):
                        case (42, 21, 4):
                        case (43, 21, 5)://nappa
                        case (58, 25, 4): // xen 1
                        case (80, 32, 3) or (80, 32, 8):
                        case (81, 32, 3) or (81, 32, 8):
                            PlusSubTask(character, 1);
                            break;
                            #endregion
                    }   
                    break;
            }
        }
        public string Replace(ICharacter character, string text)
        {
            switch (text)
            {
                case "#vachnui":
                    switch (character.InfoChar.Gender)
                    {
                        case 0: return "Vách Núi Aru";
                        case 1: return "Vách Núi Mori";
                        case 2: return "Vách Núi Kakarot";
                    }
                    break;
                case "#lang":
                    switch (character.InfoChar.Gender)
                    {
                        case 0: return "Làng Aru";
                        case 1: return "Làng Mori";
                        case 2: return "Làng Kakarot";
                    }
                    break;
                case "#onggia":
                    switch (character.InfoChar.Gender)
                    {
                        case 0:return "Ông Gohan";
                        case 1: return "Ông Morri";
                        case 2: return "Ông Paragus";
                    }
                    break;
                case "#banhang":
                    switch (character.InfoChar.Gender)
                    {
                        case 0: return "Bunma";
                        case 1: return "Dende";
                        case 2: return "Appule";
                    }
                    break;
                case "#truonglao":
                    switch (character.InfoChar.Gender)
                    {
                        case 0: return "Quy Lão Kame";
                        case 1: return "Trưởng Lão Guru";
                        case 2: return "Vua Vegeta";
                    }
                    break;

            }
            return text;
        }
        public void Message(ICharacter character)
        {
            switch(character.InfoTask.Id, character.InfoTask.Index)
            {
                case (0, 0):
                    
                    break;
                case (0, 1):
                   
                    break;
                case (0, 2):
                    DoUISay(character,"Con vừa đi đâu về đó?\n"
                            + "Con hãy đến rương đồ để lấy rađa..\n"
                            + "..sau đó thu hoạch hết đậu trên cây đậu thần đằng kia!");
                    break;
                case (0, 3):
                    DoSendMessage(character, "Bạn có thể thu hoạch đậu thần ở cây đậu đằng kia");
                    break;
                case (0, 4):
                    DoSendMessage(character, $"Nào, bây giờ bạn có thể gặp {Replace(character, "$onggia")} để báo cáo rồi!");
                    break;
                case (0, 5):
                    DoUISay(character,"Tốt lắm, rađa sẽ giúp con thấy được lượng máu và thể lực ở bên góc trái\n"
                            + "Bây giờ con hãy đi luyện tập\n"
                            + $"Con hãy ra {Replace(character, "#lang")}, ở đó có những con mộc nhân cho con luyện tập dó\n"
                            + "Hãy đốn ngã 5 con mộc nhân cho ông");
                    break;
                case (1, 0):
                    DoSendMessage(character, $"Bạn đã đánh được {character.InfoTask.Count}/5 mộc nhân");
                    break;
                case (1, 1):
                    DoUISay(character, "Thể lực của con cũng khá tốt\n"
                            + "Con à, dạo gần đây dân làng của chúng ta gặp phải vài chuyện\n"
                            + "Bên cạnh làng ta đột nhiên xuất hiện lũ quái vật\n"
                            + "Nó tàn sát dân làng và phá hoại nông sản làng ta\n"
                            + "Con hãy tìm đánh chúng và đem về đây 10 cái đùi gà, 2 ông cháu mình sẽ để dành ăn dần\n"
                            + "Đây là tấm bản đồ của vùng này, con hãy xem để tìm đến "+ Replace(character, "#vachnui") +"\n"
                            + "Con có thể sử dụng đậu thần khi hết HP hoặc KI, bằng cách nhấn vào nút có hình trái tim "
                            + "bên góc phải dưới màn hình\n"
                            + "Nhanh lên, ông đói lắm rồi");
                    break;
                case (2, 0):
                    DoSendMessage(character, $"Bạn đã nhặt được {character.InfoTask.Count}/10 đùi gà");
                    break;
                case (2, 1):
                    DoUISay(character, "Tốt lắm, đùi gà đây rồi, haha. Ông sẽ nướng tại đống lửa gần kia con có thể ăn bất cứ lúc nào nếu muốn\n"
                            + $"À cháu này, vừa nãy ông có nghe thấy 1 tiếng động lớn, hình như có 1 vật thể rơi tại {Replace(character, "$vachnui")}, con hãy đến kiểm tra xem\n"
                            + "Con cũng có thể dùng tiềm năng bản thân để nâng HP, KI hoặc sức đánh");
                    break;
                case (3, 0):
                case (3, 1):
                    break;
                case (3, 2):
                    DoUISay(character,"Có em bé trong phi thuyền rơi xuống à, ông cứ tưởng là sao băng chứ\n"
                            + "Ông sẽ đặt tên cho em nó là Goku, từ giờ nó sẽ là thành viên trong gia đình ta\n"
                            + "Nãy ông mới nhận được tin có bầy mãnh thú xuất hiện tại Trạm phi thuyền\n"
                            + "Bọn chúng vừa đổ bộ xuống trái đất để trả thù việc con sát hại con chúng\n"
                            + "Con hãy đi tiêu diệt chúng để giúp dân làng tại đó luôn nhé");
                    break;
                case (4, 0):

                    break;
                case (4, 1):
                    break;
                    case (7, 0):
                    DoSendMessage(character, "Hình như có người đang tới đây");
                    DoSendMessage(character, "Đây có phải tên Tàu Pảy Pảy mà Bò Mộng nhắc đến?, hãy thử sức xem nào");
                    break;
            }
        }
        public Boolean ReportTask(Character character, int npcId)
        {
            
            switch(character.InfoTask.Id, character.InfoTask.Index, npcId)
            {
                case (0, 2 or 5, 0 or 1 or 2):
                case (1,    1, 0 or 1 or 2):
                case (2, 1, 0 or 1 or 2):
                case (3, 2, 0 or 1 or 2):

                case (8, 2, 7 or 8 or 9) or (8, 3, 0 or 1 or 2):
                case (9, 2, 0 or 1 or 2):
                case (11, 2, 17) or (11, 3, 0 or 1 or 2):
                case (12, 1, 0 or 1 or 2):
                case (12, 0, 13 or 14 or 15):

                case (13, 1, 13 or 14 or 15):
                case (14, 3, 13 or 14 or 15):
                case (15, 2, 13 or 14 or 15):
                case (16, 4, 13 or 14 or 15):
                case (17, 1, 13 or 14 or 15):
                case (18, 4, 13 or 14 or 15):
                case (19, 2, 13 or 14 or 15):
                case (20, 2, 13 or 14 or 15):
                case (21, 6, 13 or 14 or 15):
                case (22, 3, 13 or 14 or 15):
                case (23, 5, 13 or 14 or 15):
                case (24, 3, 13 or 14 or 15):
                case (25, 3 or 5, 37) or (25, 0, 0 or 1 or 2) or (25, 1, 38) or (25, 2, 13):
                case (26 or 27, 4, 37):
                case (28 or 29 or 30, 5, 37):
                    PlusSubTask(character, 1);
                    LoadInfo(character);
                    return true;
                case (31, 7, 44):
                    DoSetTask(character, 34);
                    LoadInfo(character);
                    return true;
                case (10, 0, 17):
                DoUISay(character, "Hắn sắp đến đây, hãy giúp ta tiêu diệt hắn");
                PlusSubTask(character, 1);
                LoadInfo(character);
                var tau77 = new Boss(94, character.InfoChar.X, character.InfoChar.Y, 0);
                tau77.InfoDelayBoss.ChangeMode = 1000 + ServerUtils.CurrentTimeMillis();
                tau77.CharacterHandler.SetUpInfo();
                character.MapPrivate.Maps[2].Zones[0].ZoneHandler.AddBoss(tau77);
                async void DungDoTau77()
                    {
                        await Task.Delay(500);
                        PlusSubTask(character, 1);
                        DoSendMessage(character, "Mau chóng bay lên Tháp Karin !");
                    }
                    var task = new Task(DungDoTau77);
                    task.Start();
                    return true;
                case (10, 3, 18):
                DoUISay(character, "Có phải ngươi vừa chiến đáu với Tàu Pảy Pảy không?"
                + "\nTa tuy mù nhưng có thể đọc được ý nghĩ của ngươi"
                + "\nNgươi chưa phải là đối thử của hắn đâu"
                + "\nTìm ta là đúng rồi, để ta dạy cho ngươi vài chiêu, nhưng phải chăm chỉ mới học được đấy nhé"
                + "\nNgươi đã sẵn sàng luyện tập chưa", 18);
                DoUISay(character, "Để luyện tập, hãy đến gặp Thần mèo Karin, chọn mục luyện tập"
                + "\nBạn cũng có thể luyện tập khi đang Offline bằng cách thoát game sau khi chọn mục luyện tập"
                + "\nKhi đủ khả năng này, Hãy chọn mục thách đấu, nếu chiến thắng bạn có thể luyện tập với các đối thử khác mạnh hơn"
                + "\nNgoài ra bạn cũng có thể tặng ngọc để chiến thắng"
                + "\nLuyện tập sẽ giúp tăng tiềm năng và sức mạnh, đối thủ càng mạnh sẽ tăng càng nhanh");
                PlusSubTask(character, 1);
                LoadInfo(character);
                return true;
                case (4, 3, 0 or 1 or 2):
                    DoSetTask(character, 8);
                    LoadInfo(character);

                return true;
                default:
                    return false;
            }
        }
        public void PlusSubTask(ICharacter character, int plusCount)
        {
            character.InfoTask.Count+= (short)plusCount;
            Message(character);
            var task = Cache.Gi().TASK_TEMPLATES_0.Values.FirstOrDefault(i=>i.Id == character.InfoTask.Id);
            switch (character.InfoChar.Gender)
            {
                case 1:
                    task = Cache.Gi().TASK_TEMPLATES_1.Values.FirstOrDefault(i => i.Id == character.InfoTask.Id);
                    break;
                case 2:
                    task = Cache.Gi().TASK_TEMPLATES_2.Values.FirstOrDefault(i => i.Id == character.InfoTask.Id);
                    break;
            }
            switch (character.InfoTask.Count >= task.Counts[character.InfoTask.Index])
            {
                case true:
                    character.InfoTask.Count = 0;
                    character.InfoTask.Index++;
                    character.InfoTask.Time = ServerUtils.CurrentTimeMillis();
                    switch (character.InfoTask.Index >= task.SubNames.Count)
                    {
                        case true:
                            CongTiemNangSucManh(character, task.Reward[0], task.Reward[1],task.Reward[2]);
                            SendNextTask(character);
                            break;
                        case false:
                            UpdateTask(character);
                            break;
                    }
                    break;
                case false:
                    UpdateCount(character);
                    break;
            }
        }
         public void DoSetTask(Character character, int taskid)
        {
            Message(character);
            SetTask(character, taskid);
            var task = Cache.Gi().TASK_TEMPLATES_0.Values.FirstOrDefault(i=>i.Id == character.InfoTask.Id);
            switch (character.InfoChar.Gender)
            {
                case 1:
                    task = Cache.Gi().TASK_TEMPLATES_1.Values.FirstOrDefault(i => i.Id == character.InfoTask.Id);
                    break;
                case 2:
                    task = Cache.Gi().TASK_TEMPLATES_2.Values.FirstOrDefault(i => i.Id == character.InfoTask.Id);
                    break;
            }
           
             CongTiemNangSucManh(character, task.Reward[0], task.Reward[1],task.Reward[2]);
             SendNextTask(character);
                          
        }
        public void SendNextTask(ICharacter character)
        {
            character.InfoTask.Id++;
            character.InfoTask.Index = 0;
            character.InfoTask.Count = 0;
            UpdateTask(character);
            RewardDoneTask(character);
        }
        public void SetTask(Character character, int taskid)
        {
            character.InfoTask.Id = (short)taskid;
            character.InfoTask.Index = 0;
            character.InfoTask.Count = 0;
            UpdateTask(character);
            RewardDoneTask(character);
        }
        public void CheckTaskDoneKillBoss(Character character, int bossType)
        {
            switch (bossType, character.InfoTask.Id, character.InfoTask.Index)
            {
                case (87, 11, 0) or (85, 11, 1):
                case (24,22, 0) or (25, 22, 1) or (26, 22, 2):
                case (16, 23, 0) or (17, 23, 1) or (93, 23, 2) or (18, 23, 3) or(19, 23, 4):
                case (4, 24, 0) or (5, 24, 1) or (6, 24, 2):
                case (30, 26, 1) or (31, 26, 2):
                case (34, 27, 1) or (33, 27, 2) or (32, 27, 3):
                case (28, 28, 1) or (27, 28, 2) or (29, 28, 3):
                case (7, 29, 1) or (8, 29, 2) or (9, 29, 3):
                case (42, 30, 3) or (63,30, 4):
                case (36, 31, 1) or (37, 31, 2) or (37, 31, 3) or (38, 31, 4) or (36, 31, 5) or (39, 31, 6):
                case (14, 33, 5) or (15, 33, 6) or (14 or 15, 33, 7):
                    PlusSubTask(character, 1);
                    break;
            }
        }
        public void CheckTaskMoveInMap(ICharacter character)
        {
            switch(character.InfoChar.MapId, character.InfoTask.Id, character.InfoTask.Index)
            {
                case (21 or 22 or 23, 0, 0):

                    break;
            }
        }
        public void CheckTaskDoneGoToMap(ICharacter character)
        {
            switch(character.InfoChar.MapId, character.InfoTask.Id, character.InfoTask.Index)
            {
                case (21 or 22 or 23, 0, 1):
                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, $"{Replace(character, "#onggia")} đang đứng đợi kìa\n"
                           + "Hãy nhấn 2 lần vào để nói chuyện"));
                    break;
                case (46, 10, 2):
                                    PlusSubTask(character, 1);
                                    DoSendMessage(character, "Hãy đến nói chuyện với Thần Mèo Karin");
                                    break;
                case (47, 9, 3):
                case (93, 26, 0):
                case (104, 27, 0):
                case (103, 28, 3):
                case (100, 29, 0):
                case (103,30, 2):
                    PlusSubTask(character, 1);
                    break;
                case (97 or 98 or 99, 28, 0):
                    if (character.Zone.ZoneHandler.BossInMap().Count > 0)
                    {
                        PlusSubTask(character, 1);
                        break;
                    }
                    break;
            }
        }
        public void CheckTaskDonePower(ICharacter character)
        {
            switch(character.InfoTask.Id, character.InfoTask.Index, character.InfoChar.Power)
            {   
                case (8, 0, >= 16000)://16k suc manh
                case (9, 0, >= 40000):// 40k suc manh
                case (15, 0, >= 200000)://200k suc manh
                case (16, 0, >= 500000):// 500k suc manh
                case (18, 0, >= 1500000):// 1tr5 suc manh
                case (19, 0, >= 5000000):// 5tr suc manh
                case (20, 0, >= 15000000):// 15tr suc manh
                case (21, 0, >= 50000000):// 50tr suc manh
                    PlusSubTask(character, 1);
                    break;
            }
        }
        public void CheckTaskDonePlusPoint()
        {

        }
        public void DoneTask()
        {

        }
        public static Boolean CheckTask(Character character, int id, int index) // check Nhiệm Vụ Theo Id và Index || TaskHandler.CheckTask(character, 1 la id,1 la index )
        {
            return character.InfoTask.Id == id && character.InfoTask.Index == index;
            
        }
        public static Boolean CheckTask(ICharacter character, int id, int index) // check Nhiệm Vụ Theo Id và Index || TaskHandler.CheckTask(character, 1 la id,1 la index )
        {
            return character.InfoTask.Id == id && character.InfoTask.Index == index;

        }
        public void DoSendMessage(ICharacter character, string message) // Gửi Thông Báo Extension Message
        {
            character.CharacterHandler.SendMessage(Service.ServerMessage(message));
        }
        public void DoUISay(ICharacter character, string say, short npcId = 5) // mở Menu Ui Con Mèo
        {
            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, $"{say}"));
        }

        public void LoadInfo(ICharacter character) // send lại info nhân vật như vàng , ngọc
        {
            character.CharacterHandler.SendMessage(Service.BuyItem(character));
        }

        public void CongTiemNangSucManh(ICharacter character, long gold, long gem, long tnsm)
        {
            if (!character.InfoChar.IsPower) return;
                switch (gold > 0)
            {
                case true:
                    DoSendMessage(character, $"Bạn vừa nhận được {ServerUtils.GetMoney(gold)} vàng");
                    break;
            }
            switch (gem > 0)
            {
                case true:
                    DoSendMessage(character, $"Bạn vừa nhận được {ServerUtils.GetMoney(gold)} ngọc");
                    break;
            }
            switch (tnsm > 0)
            {
                case true:
                    character.CharacterHandler.PlusPotential(tnsm);
                    character.CharacterHandler.PlusPower(tnsm);
                    character.CharacterHandler.SendMessage(Service.UpdateExp(2, tnsm));
                    DoSendMessage(character, $"Bạn vừa được thưởng {tnsm} sức mạnh");
                    DoSendMessage(character, $"Bạn vừa được thưởng {tnsm} tiềm năng nữa");
                    break;
            }
            LoadInfo(character);

        }
    }
}
