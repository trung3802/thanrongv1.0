using System.Collections.Generic;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
namespace TienKiemV2Remastered.Application.Train{
    public class TraningCache{
        public static long Time = 1800000;
    }
    public class ThachDau{
        public static ThachDau instance;
        public static ThachDau gI(){
            if (instance == null) instance = new ThachDau();
            return instance;
        }
        public void ThachDauWhis(Character character, int level)
        {
          //  character.DataTraining.DataWhis.Count++;
            var boss = new Boss();
            boss.CreateBoss(107, 339, 560);
            boss.CharacterHandler.SetUpInfo();
            character.MapPrivate.GetMapById(154).Zones[0].ZoneHandler.AddBoss(boss);
            character.InfoChar.X = 493;
            character.InfoChar.Y = 360;
            character.CharacterHandler.SendMessage(Service.SendPos(character, 0));
        }
        public void Start(Character character,int level){
            switch(level){
                case 0: // than meo karin
                character.CharacterHandler.SendMessage(Service.HideNpc(18, true));
                var meokarin = new Boss();
                    meokarin.CreateBoss(87, 204, 408);
                    meokarin.CharacterHandler.SetUpInfo();
                character.MapPrivate.Maps[1].Zones[0].ZoneHandler.AddBoss(meokarin);
                break;
                case 1:// yajiro

                break;
                case 2:// mr popo
                case 3: // thuong de
                case 4: // khi bullble
                case 5: // kaio
                case 6: // to su kaio
                case 7: // whis

                break;
            }
        }
    }
    public class TrainingHandler{
        public static TrainingHandler instance;
        public static TrainingHandler gI(){
            if (instance == null) instance = new TrainingHandler();
            return instance;
        }
        public void RegisterAutoTrain(Character character, long Potenial){
            character.DataTraining.isTraining = true;
            character.DataTraining.Potenial = Potenial;
            character.DataTraining.MapTraning = character.InfoChar.MapId;
        }
        public void Goback(Character character){
            var oldMapId = character.DataTraining.OldMap.Map.Id;
            var oldZone = character.DataTraining.OldMap;
            character.MapPrivate.Maps[character.MapPrivate.GetIndexMap(character.InfoChar.MapId)].OutZone(character, oldMapId);
            MapManager.Get(oldMapId).JoinZone(character, oldZone.Map.GetZoneNotMaxPlayer().Id, false, true, character.TypeTeleport);

        }
        public void Training(Character character){
            var time = (ServerUtils.CurrentTimeMillis() - character.DataTraining.lastTimeLogout) / 60000;
            switch (time)
            {
                case >= 30: // nếu off hơn 30 phút
                    var tnsm = DataTraining.GetPotenial(character);
                    character.MineDiamond(1);
                    switch (character.InfoChar.MapId)
                    {
                        case 45 or 46 or 47 or 50 or 111 or 116 or 48 or 49:
                            character.CharacterHandler.PlusPotential(tnsm);
                            character.CharacterHandler.PlusPower(tnsm);
                            character.CharacterHandler.SendMessage(Service.UpdateExp(2, tnsm));
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, $"Bạn tăng được {ServerUtils.GetMoneys(((time - 30) <= 0 ? 1 : (time - 30)) * tnsm)} sức mạnh trong thời gian {time} tập luyện Offline"));
                            break;
                        default:
                           // character.DataTraining.OldMap = character.Zone;
                         //   MapManager.OutMap(character, character.DataTraining.MapTraning);
                          //  character.MapPrivate.GetMapById(character.DataTraining.MapTraning).JoinZone(character, 0);
                            character.CharacterHandler.PlusPotential(tnsm);
                            character.CharacterHandler.PlusPower(tnsm);
                            character.CharacterHandler.SendMessage(Service.UpdateExp(2, tnsm));
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(19, $"Bạn tăng được {ServerUtils.GetMoneys(((time - 30) <= 0 ? 1 : (time - 30)) * tnsm)} sức mạnh trong thời gian {time} tập luyện Offline", new List<string>{"Ở\nLại đây", "Về\nChỗ cũ"}, character.InfoChar.Gender));
                        //    character.TypeMenu = 5;
                            break;
                    }
                    break;

            }
        }
    }
}