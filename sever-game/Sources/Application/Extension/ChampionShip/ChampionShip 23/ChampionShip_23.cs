using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23
{
    public class WoodChest
    {
        public static WoodChest instance;
        public static WoodChest gI()
        {
            if (instance == null)
            {
                instance = new WoodChest();
            }
            return instance;
        }

        public List<Item> Items { get; set; }
        public int CountItem { get; set; }
       
        public WoodChest()
        {
            Items = new List<Item>();
            CountItem = -1;
        }
        public void Open(Character character, int chestLevel)
        {
            var gold = ServerUtils.RandomNumber(2000000 * chestLevel, 2000000 * chestLevel * 2);
            var geml = ServerUtils.RandomNumber(2 * chestLevel, 2 * chestLevel * 2);
            character.PlusGold(gold);
            character.PlusDiamondLock(geml);
            List<List<int>> ListIdItem = new List<List<int>>();
            switch (chestLevel)
            {
                case 3:
                    CountItem = ServerUtils.RandomNumber(1,2);
                    ListIdItem = new List<List<int>> { new List<int> { 0, 3, 6, 9, 12, 21, 24, 27, 30 }, new List<int> { 1, 4, 7, 10, 12, 22, 25, 28, 31 }, new List<int> { 2, 5, 8, 11, 13, 23, 26, 29, 32 } } ;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    CountItem = ServerUtils.RandomNumber(3,5);
                    ListIdItem = new List<List<int>> { new List<int> { 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 184, 185, 186, 187 }, new List<int> { 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 184, 185, 186, 187 }, new List<int> { 168, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187 } };
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    CountItem = ServerUtils.RandomNumber(7, 9);
                    ListIdItem = new List<List<int>> { new List<int> { 230,231,232,233,242,243,244,245,254,255,256,257,266,267,268,269,278,279,280,281 }, new List<int> {234,235,236,237,246,247,248,249,258,259,260,261,270,271,272,273,278,279,280,281}, new List<int> { 238,239,240,241,250,251,252,253,262,263,264,265,274,275,276,277,278,279,280,281 } };
                    break;
            }
            if (chestLevel >= 3)
            {
                var item = ItemCache.GetItemDefault(-1);
                for (int i = 0; i < CountItem; i++)
                {
                    item = ItemCache.GetItemDefault((short)(ListIdItem[character.InfoChar.Gender][ServerUtils.RandomNumber(ListIdItem[character.InfoChar.Gender].Count)]));
                    character.CharacterHandler.AddItemToBag(true, item);
                }
            }
            character.CharacterHandler.RemoveItemBagById(570, 1);
            character.CharacterHandler.SendMessage(Service.SendBag(character));
            character.CharacterHandler.SendMessage(Service.ServerMessage("Nhan thuong thanh cong !"));
        }
    }
    public class ChampionShip_23
    {
        public int Id_Phần_Thưởng = 570; // id ruong go~
        public int Option_Phần_Thưởng = 72;
        public int Param = 1;
        public static ChampionShip_23 instance;
        public static ChampionShip_23 gI()
        {
            if (instance == null)
            {
                instance = new ChampionShip_23();
            }
            return instance;
        }
        public void Khống_chế_10_giây_đầu(Character character)
        {
            async void Action() {
                await Task.Delay(500);
                character.InfoSkill.ThoiMien.IsThoiMien = true;
                character.InfoSkill.ThoiMien.Time = 9000 + ServerUtils.CurrentTimeMillis();
                character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeThoiMien[0], 9));
                character.CharacterHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 40));
            }
            var task = new Task(Action);
            task.Start();
        }
        public void Hồi_lại_skill(Character character)
        {
            character.DataDaiHoiVoThuat23.Battled = true;
            for (int i = 0; i < character.Skills.Count; i++)
            {
                character.Skills[i].CoolDown = 1000 + ServerUtils.CurrentTimeMillis();
                character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
            }
        }
        public void Thực_hiện_dịch_chuyển(Character character, int x, int y)
        {
            character.InfoChar.X = (short)x;
            character.InfoChar.Y = (short)y;
            character.CharacterHandler.SendZoneMessage(Service.SendPos(character, 1));
        }
        public void Đấm_Nhau(Character character)
        {
           
            //boss 434
            character.Delay.Delay10Giay = 10000 + ServerUtils.CurrentTimeMillis();
            character.Delay.Delay180Giay = 190000 + ServerUtils.CurrentTimeMillis();
            
            // ChampionShip.gI().Dịch_chuyển_tới_map(character, 113, ServerUtils.RandomNumber(0,100), 1);
            Thực_hiện_dịch_chuyển(character, 334, 264);
            Tạo_Boss(character);
            Khống_chế_10_giây_đầu(character);
            Hồi_lại_skill(character);
            character.DataDaiHoiVoThuat23.Battled = true;
           // new Thread(new ThreadStart(() =>
           // {
           async void Update()
            {
                while (character.DataDaiHoiVoThuat23.Battled && character.InfoChar.MapId == 129 && ClientManager.Gi().GetCharacter(character.Id) != null)
                {
                    var timenow = ServerUtils.CurrentTimeMillis();
                    // Console.WriteLine("CURR1: " + (character.Delay.Delay180Giay - ServerUtils.CurrentTimeMillis()) / 1000 + "s");
                    if (ServerUtils.CurrentTimeMillis() >= character.Delay.Delay180Giay)
                    {
                        Thực_hiện_dịch_chuyển(character, 389, 360);
                        character.CharacterHandler.PlusHp((int)character.HpFull);
                        character.CharacterHandler.PlusMp((int)character.MpFull);
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Hết thời gian!"));
                        character.DataDaiHoiVoThuat23.Battled = false;
                        character.InfoChar.TypePk = 0;
                        character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                        Xóa_Boss(character);
                    }
                    else
                    {
                        if ((character.Delay.Delay10Giay - 1000) <= ServerUtils.CurrentTimeMillis() && character.InfoChar.TypePk != 3)
                        {
                            character.Delay.Delay10Giay = 180000 + ServerUtils.CurrentTimeMillis();
                            character.InfoChar.TypePk = 3;
                            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 3));
                            Thực_hiện_dịch_chuyển(character, 334, 264);
                        }
                        if (character.InfoChar.IsDie)
                        {
                            Thực_hiện_dịch_chuyển(character, 389, 360);
                            character.CharacterHandler.PlusHp((int)character.HpFull);
                            character.CharacterHandler.PlusMp((int)character.MpFull);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Gà quá nên thua!"));
                            Xóa_Boss(character);
                            character.DataDaiHoiVoThuat23.Battled = false;
                        }
                        if (character.DataDaiHoiVoThuat23.Battled && character.InfoChar.MapId == 129 && ((character.InfoChar.X >= 0 && character.InfoChar.X <= 157) || (character.InfoChar.X >= 611 && character.InfoChar.X <= 733) || character.InfoChar.Y > 264))
                        {
                            Xóa_Boss(character);
                            ChampionShip_23.gI().Thực_hiện_dịch_chuyển(character, 389, 360);
                            character.CharacterHandler.PlusHp((int)character.HpFull);
                            character.CharacterHandler.PlusMp((int)character.MpFull);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Do ngươi đã ra khỏi phạm vi thi đấu nên bị xử thua!"));
                            character.DataDaiHoiVoThuat23.Battled = false;
                            character.InfoChar.TypePk = 0;
                            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                            ChampionShip_23.gI().Xóa_Boss(character);

                        }
                        if (character.InfoChar.MapId != 129)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Do bạn đã chạy trốn nên bị xử thua!"));
                            Xóa_Boss(character);
                            character.DataDaiHoiVoThuat23.Battled = false;
                        }
                        //if (character.DataDaiHoiVoThuat23.Round == character.DataDaiHoiVoThuat23.RoundNext)
                        //{
                        //    if (character.DataDaiHoiVoThuat23.Round == 11)
                        //    {
                        //        Thực_hiện_dịch_chuyển(character, 389, 360);
                        //        character.CharacterHandler.PlusHp((int)character.HpFull);
                        //        character.CharacterHandler.PlusMp((int)character.MpFull);
                        //        character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã chiến thắng !"));
                        //        character.DataDaiHoiVoThuat23.Battled = false;
                        //        character.InfoChar.TypePk = 0;
                        //        character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                        //        Console.WriteLine("WIN R11");
                        //        Xóa_Boss(character.InfoChar.MapId, character.Zone.Id, 52 + character.DataDaiHoiVoThuat23.Round);
                        //        return;
                        //    }
                        //    //character.Delay.Delay180Giay = 190000 + ServerUtils.CurrentTimeMillis();
                        //    //character.DataDaiHoiVoThuat23.RoundNext++;
                        //    //character.CharacterHandler.PlusHp((int)character.HpFull);
                        //    //character.CharacterHandler.PlusMp((int)character.MpFull);
                        //    //character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        //    //Thực_hiện_dịch_chuyển(character, 334, 264);
                        //    //Khống_chế_10_giây_đầu(character);
                        //    //Hồi_lại_skill(character);
                        //    //Tạo_Boss(character);
                        //    //Console.WriteLine("NEXT ROUND");
                        //}
                    }
                    await Task.Delay(1000);
                }
            }
            var task = new Task(Update);
            task.Start();
           // })).Start();

        }
        public void Kill(Character character,int round)
        {
            if (round < 10)
            {
                character.DataDaiHoiVoThuat23.Battled = false; // true
               
                character.InfoChar.TypePk = 0;//
                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));//3
                character.Delay.Delay10Giay = 10000 + ServerUtils.CurrentTimeMillis();
                //JoinMapAgain(character);
                Thực_hiện_dịch_chuyển(character, 389, 360);
                character.DataDaiHoiVoThuat23.Round++;
                character.DataDaiHoiVoThuat23.ChestLevel = character.DataDaiHoiVoThuat23.Round;
                character.Delay.Delay180Giay = 190000 + ServerUtils.CurrentTimeMillis();
                character.CharacterHandler.PlusHp((int)character.HpFull);
                character.CharacterHandler.PlusMp((int)character.MpFull);
                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                async void After()
                {
                    await Task.Delay(1000);
                    Đấm_Nhau(character);
                   // Thực_hiện_dịch_chuyển(character, 334, 264);
                   // character.DataDaiHoiVoThuat23.Battled = true; // true
                   // if (character.DataDaiHoiVoThuat23.Battled)
                   // {
                   //     Console.WriteLine("@@");
                   // }
                   // Khống_chế_10_giây_đầu(character);
                   // Hồi_lại_skill(character);
                   // Tạo_Boss(character);
                   //// Server.Gi().Logger.PrintColor("DO NEXT ROUND", "red");
                }
                var task = new Task(After);
                task.Start();
            }
            else
            {
                character.DataDaiHoiVoThuat23.Round++;
                Thực_hiện_dịch_chuyển(character, 389, 360);
                character.CharacterHandler.PlusHp((int)character.HpFull);
                character.CharacterHandler.PlusMp((int)character.MpFull);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã chiến thắng !"));
                character.DataDaiHoiVoThuat23.Battled = false;
                character.InfoChar.TypePk = 0;
                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
            }
        }
        public void ResetTimeVS(Character character)
        {            
            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 3));
        }
        public void Cập_nhật_Boss()
        {
            var timenow = ServerUtils.CurrentTimeMillis();

        }
        public void Tạo_Boss(Character character)
        {
            var boss = new Boss();
            boss.CreateBossHasCharFocus(52 + character.DataDaiHoiVoThuat23.Round, character,497, 264, character.Id);
            boss.CharacterHandler.SetUpInfo();
            character.Zone.ZoneHandler.AddBoss(boss);

        }
        public void Xóa_Boss(Character character)
        {
            for (int i = 0; i < character.Zone.Bosses.Count; i++)
            {
                var boss = character.Zone.ZoneHandler.GetBossInMap()[i];
                var @bossFound = (Boss)boss;
                character.Zone.ZoneHandler.RemoveBoss(@bossFound);
                boss.Id = -1;
                System.GC.SuppressFinalize(boss);
                boss = null;
            }
        }
        public void Nhận_Thưởng(Character character)
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
    }
}
