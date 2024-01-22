using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.Extension
{

    public class Died_Ring
    {
        public class Runtime
        {
            public bool HandlerAuto = false;
            public bool isFinishMatch = false;
            public List<int> ListIdCharacter = new List<int>();
            public List<int> ListTypeBoss = new List<int> { 68, 69, 70, 71, 72 }; // bong bang,vua quy sa tang , tho dau bac  hp goc + (round * 20)
            public static Runtime runtime;
            public static Runtime gI()
            {
                if (runtime == null) runtime = new Runtime();
                return runtime;
            }
            public Boolean CheckBeforeRegister(Character character)
            {
                if (ListIdCharacter.Contains(character.Id)) return true;
                return false;
            }
            public void Register(Character character)
            {
                if (!CheckBeforeRegister(character))
                {
                    ListIdCharacter.Add(character.Id);
                    Auto();
                    character.Zone.ZoneHandler.SendMessage(Service.NpcChat(21, "Số thứ tự của con là: " + ListIdCharacter.Count + ".Hãy chuẩn bị sẵn sàng nhé !"));
                }
                else
                {

                    // cannot reg because has reg
                }
            }
          
            public void Auto()
            {
                new Thread(new ThreadStart(() =>
                {
                    while (ListIdCharacter.Count >= 1)
                    {
                        if (!isFinishMatch && ClientManager.Gi().GetCharacter(ListIdCharacter[0]) != null &&!ClientManager.Gi().GetCharacter(ListIdCharacter[0]).DataVoDaiSinhTu.isStart) {
                            Died_Ring.gI().Fight((Character)ClientManager.Gi().GetCharacter(ListIdCharacter[0]));
                        }else{
                            ListIdCharacter.Remove(ListIdCharacter[0]);
                        }
                    }
                    Thread.Sleep(1000);
                })).Start();
            }
        }

        public int Round { get; set; }
        public bool isStart { get; set; }
        public bool Reward { get; set; }
        public static Died_Ring instance;
        public static Died_Ring gI()
        {
            if (instance == null) instance = new Died_Ring();
            return instance;
        }
        public Died_Ring()
        {
            Round = 0;
            isStart = false;
            Reward = false;
        }
        // 401 - 521 // 336
        public void Fight(Character character)
        {
            character.InfoChar.X = 401;
            character.InfoChar.Y = 336;
            character.CharacterHandler.SendMessage(Service.SendPos(character, 1));
            Boss(character);
            character.InfoChar.TypePk = 3;
            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 3));
            character.DataVoDaiSinhTu.isStart = true;
        }
        public void Lose(Character character)
        {
            character.InfoChar.X = 467;
            character.InfoChar.Y = 408;
            character.CharacterHandler.SendMessage(Service.SendPos(character, 1));
            character.InfoChar.TypePk = 0;
            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
            RemoveBoss(character, Runtime.gI().ListTypeBoss[character.DataVoDaiSinhTu.Round]);
            Died_Ring.Runtime.gI().isFinishMatch = true;
            Died_Ring.Runtime.gI().ListIdCharacter.Remove(character.Id);
        }
        public void Join(Character character)
        {
            MapManager.JoinMap(character, 112, ServerUtils.RandomNumber(20), false, false, 3);
        }
        public void Boss(Character character)
        {
            var boss = new Boss();
            boss.CreateBossNoAttack(Runtime.gI().ListTypeBoss[character.DataVoDaiSinhTu.Round], 521, 336, true, character.HpFull + (20*character.DataVoDaiSinhTu.Round));
            boss.CharacterHandler.SetUpInfo();
            character.Zone.ZoneHandler.AddBoss(boss);
        }
        public bool BossExist(Character character, int type)
        {
            if (character.Zone.ZoneHandler.GetBossInMap(type) != null) return true;
            return false;
        }
        public void RemoveBoss(Character character, int type)
        {
            if (BossExist(character, type))
            {
                var boss = character.Zone.ZoneHandler.GetBoss(type);
                character.Zone.ZoneHandler.RemoveBoss((Boss)boss);
            }
        }
        public void Kill(Character character)
        {
            character.DataVoDaiSinhTu.Round++;
            Boss(character);
        }
        public class Cache{
            public static int READY = 0;
            public static int DURING_MATCH = 1;
            public static int FINISH_MATCH = 2;
            public static int REGISTER = 3;
        }
        //public static string BaHatMitChat(int type)
        //{
        //    switch(type){
        //        case Cache.READY:
        //        case Cache.DURING_MATCH:
        //        case Cache.FINISH_MATCH:
        //        case Cache.REGISTER:
        //        return "";
        //    }
        //    return "";
        //}
    }
}
