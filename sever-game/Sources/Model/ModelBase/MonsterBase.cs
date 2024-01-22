using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;

namespace TienKiemV2Remastered.Model.ModelBase
{
    public class MonsterBase : IMonster
    {
        public IMonsterHandler MonsterHandler { get; set; }
        public short Id { get; set; }
        public ICharacter Character { get; set; }
        public int IdMap { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public byte Status { get; set; }
        public int Level { get; set; }
        public byte LvBoss { get; set; }
        public bool IsBoss { get; set; }
        public Zone Zone { get; set; }
        public long OriginalHp { get; set; }
        public long Hp { get; set; }
        public long HpMax { get; set; }
        public bool IsMobMe { get; set; }
        public bool IsFire { get; set; }
        public bool IsIce { get; set; }
        public bool IsWind { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDontMove { get; set; }
        public bool IsDie { get; set; }
        public bool IsRefresh { get; set; }
        public long TimeRefresh { get; set; }
        public long DelayFight { get; set; }
        public long TimeAttack { get; set; }
        public long TimeHp { get; set; }
        public List<int> CharacterAttack { get; set; }
        public Dictionary<int, int> SessionAttack { get; set; }
        public InfoSkill InfoSkill { get; set; }
        public sbyte Sys { get; set; }
        public int MaxExp { get; set; }
        public int Damage { get; set; }
        public int LeaveItemType { get; set; }
        public bool isHide { get; set; }
        public int TypeHidru { get; set; }
        public long DelayHidru { get; set; }
        public MonsterBase()
        {
            TypeHidru = 0;
            isHide = false;
            DelayHidru = 3000 + ServerUtils.CurrentTimeMillis();
            Character = null;
            IsMobMe = false;
            Status = 5;
            Level = 1;
            LvBoss = 0;
            IsBoss = false;
            IsFire = false;
            IsWind = false;
            IsIce = false;
            IsDie = false;
            IsDisable = false;
            IsDontMove = false;
            IsRefresh = true;
            TimeRefresh = 0;
            TimeHp = 15000 + ServerUtils.CurrentTimeMillis();
            DelayFight = 1500 + ServerUtils.CurrentTimeMillis();
            TimeAttack = 10000 + ServerUtils.CurrentTimeMillis();
            CharacterAttack = new List<int>();
            SessionAttack = new Dictionary<int, int>();
            InfoSkill = new InfoSkill();
            LeaveItemType = 1;
            // this.isSocola = false;
            // this.isBlind = false;
            // this.isSleep = false;
            // this.idCharSocola = -1;
            // this.timeSocola = -1L;
        }
        
    }
}