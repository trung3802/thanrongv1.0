using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Extras;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.ModelBase;
using TienKiemV2Remastered.Model.Data;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;

namespace TienKiemV2Remastered.Model.Character
{
    public class Boss : CharacterBase
    {
        public int Status { get; set; }
        public int Type { get; set; }
        public ICharacter CharacterFocus { get; set; }
        public InfoDelayBoss InfoDelayBoss { get; set; }
        public short Hair { get; set; }
        public short Body { get; set; }
        public short Leg { get; set; }
        public short BasePositionX { get; set; }
        public short BasePositionY { get; set; }
        public short RangeMove { get; set; }
        public int KillerId { get; set; }
        public List<int> CharacterAttack { get; set; }
        public bool KhangTroi { get; set; }
        public bool isSpawnXenCon { get; set; }
        public bool isPhanThan { get; set; }
        public bool isClone { get; set; }
        public bool isYardat { get; set; }

        public long HpPst { get; set; }
        public Boss()
        {
            Status = 0;
            InfoChar.Power = 2000;
            Name = "Boss";
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            CharacterFocus = null;
            InfoChar.Stamina = 12050;
            InfoChar.MaxStamina = 12050;
            InfoChar.Pk = 1;
            InfoChar.TypePk = 3;
            isSpawnXenCon = false;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isPhanThan = false;
            isClone = false;
        }
public Boss(int type, short x = 0, short y = 0, sbyte typePk = 5)
        {

            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;
            if (type == 43) ClanId = -100;
            else ClanId = -1;
            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = Type == 82 ? 6 : 0;
            Name = bossTemplate.Name;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = typePk;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
            isYardat = false;
        }
        public void CreateBoss(int type, short x = 0, short y = 0, bool isYardatt = false)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;
            if (type == 43) ClanId = -100;
            else ClanId = -1;
            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = Type == 82 ? 6 : 0;
            Name = bossTemplate.Name;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = (sbyte)(Type == 82 ? 0 : 5);
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
            if (isYardatt)
            {
                isYardat = true;
                
            }
        }
        public void CreateSonTinhThuyTinh(int type, short x = 0, short y = 0, Boss charFocus = null, int IdFocus = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name + " " + ServerUtils.RandomNumber(1, 99);
            ClanId = -1; 
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 0;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            CharacterFocus = charFocus;
            CharacterAttack.Add(IdFocus);
            Flag = (sbyte)(Type == 83 ? 1 : 2); 
        }
        public void CreateTrongTai(short x = 0, short y = 0)
        {
           

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 6;
            Name = "Trọng Tài";
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = 82;
            InfoChar.Stamina = 0;
            InfoChar.MaxStamina = 1000;
            InfoChar.OriginalHp = InfoChar.Hp = 500;
            InfoChar.OriginalMp = InfoChar.Mp = 500;
            InfoChar.OriginalDamage = 0;
            InfoChar.OriginalDefence = 0;
            InfoChar.OriginalCrit = 0;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 0;
            Skills = null;
            Hair = 533;
            KhangTroi = false;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
        }
        public void CreateBossYardat(int type, string name,short x = 0, short y = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name+name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
           
                isYardat = true;

        }
        public void CreateBossHasCharFocus(int type, ICharacter character,short x = 0, short y = 0, int id=0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 0;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
            CharacterAttack.Add(id);
            CharacterFocus = character;
        }
        public void CreateBossDoanhTrai(int type, short x = 0, short y = 0, int hpFull = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = 3000000 * (hpFull / 5000);
            InfoChar.OriginalMp = InfoChar.Mp = 3000000 * (hpFull / 5000);
            InfoChar.OriginalDamage = bossTemplate.Damage * (hpFull / 10000);
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
        }
        public void CreateBossSetHp(int type, short x = 0, short y = 0, int level = 0, int typePk = 5)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp * level;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Hp * level;
            InfoChar.OriginalDamage = bossTemplate.Damage * level;
            InfoChar.OriginalDefence = bossTemplate.Defence * level;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = (sbyte)typePk;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = false;
        }
        public void CreateBossPhanThan(int type, short x = 0, short y = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
            isPhanThan = true;
        }
        public void CreateBossClone(Model.Character.Character character, long hp, long mp, long damage, long def)
        {
            InfoChar.Gender = character.InfoChar.Gender;
            InfoChar.Power = character.InfoChar.Power;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = character.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            Type = 65;
            InfoChar.Stamina = character.InfoChar.Stamina;
            InfoChar.MaxStamina = character.InfoChar.MaxStamina;
            InfoChar.OriginalHp = InfoChar.Hp = hp;
            InfoChar.OriginalMp = InfoChar.Mp = mp;
            InfoChar.OriginalDamage = (int)damage;
            InfoChar.OriginalDefence = (int)def;
            InfoChar.OriginalCrit = 12;
            InfoChar.X = character.InfoChar.X;
            InfoChar.Y = character.InfoChar.Y;
            BasePositionX = character.InfoChar.X;
            BasePositionY = character.InfoChar.Y;
            InfoChar.TypePk = 5;
            Skills = character.Skills;
            Hair = character.GetHead();
            Body = character.GetBody();
            Leg = character.GetLeg();
            KhangTroi = false;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isClone = true;
        }
        public void InitMabu(int type, short x = 0, short y = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = 10000;
            InfoChar.MaxStamina = 10000;
            InfoChar.OriginalHp = InfoChar.Hp = 500000000;
            InfoChar.OriginalMp = InfoChar.Mp = 500000000;
            InfoChar.OriginalDamage = 100000;
            InfoChar.OriginalDefence = 100000;
            InfoChar.OriginalCrit = 12;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
        }
        public void CreateBossNoAttack(int type, short x = 0, short y = 0,bool isSethp = false ,long hp = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == type);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = type;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = isSethp?hp : bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 0;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
            isSpawnXenCon = false;
        }
        public List<int> hpBroly = new List<int> { 16000000, 18000000, 19500000, 19244992 };
        public void CreateBossSuperBroly(short x = 0, short y = 0)
        {
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == 1);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = 1;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = hpBroly[ServerUtils.RandomNumber(hpBroly.Count)];
            InfoChar.OriginalMp = InfoChar.Mp = hpBroly[ServerUtils.RandomNumber(hpBroly.Count)];
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = x;
            InfoChar.Y = y;
            BasePositionX = x;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);
        }
        public void CreateXenCon(int id = 0, short x = 0, short y = 0)
        {
            int r = (short)ServerUtils.RandomNumber(10, 50) + x;
            var bossTemplate = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(boss => boss.Type == 42);
            if (bossTemplate == null) return;

            InfoChar.Gender = 3;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = bossTemplate.Name + " " + id;
            ClanId = -1;
            KillerId = -1;
            CharacterAttack = new List<int>();
            Id = DataCache.CURRENT_BOSS_ID;
            DataCache.CURRENT_BOSS_ID += 1;
            Type = 42;
            InfoChar.Stamina = bossTemplate.Stamina;
            InfoChar.MaxStamina = bossTemplate.Stamina;
            InfoChar.OriginalHp = InfoChar.Hp = bossTemplate.Hp;
            InfoChar.OriginalMp = InfoChar.Mp = bossTemplate.Mp;
            InfoChar.OriginalDamage = bossTemplate.Damage;
            InfoChar.OriginalDefence = bossTemplate.Defence;
            InfoChar.OriginalCrit = bossTemplate.CritChance;
            InfoChar.X = (short)r;
            InfoChar.Y = y;
            BasePositionX = (short)r;
            BasePositionY = y;
            InfoChar.TypePk = 5;
            Skills = bossTemplate.Skills;
            Hair = bossTemplate.Hair;
            KhangTroi = bossTemplate.KhangTroi;
            InfoDelayBoss = new InfoDelayBoss();
            CharacterHandler = new BossHandler(this);

        }
        public override short GetHead(bool isMonkey = true)
        {
            if (InfoSkill.MaPhongBa.isMaPhongBa) return 1221;
            return Hair;
        }

        public override short GetBody(bool isMonkey = true)
        {
            if (InfoSkill.MaPhongBa.isMaPhongBa) return 1222;
            if (Type == DataCache.BOSS_SUPER_BLACK_GOKU_TYPE)
            {
                return 551;
            }
            if (Type == 64 || Type == 80 || Type == 81)
            {
                return 523;
            }
            //if (Type == DataCache.BOSS_GOKU_THIEN_SU_TYPE)
            //{
            //    return (short)1105;
            //}
            if (isClone)
            {
                return Body;
            }
            return (short)(Hair + 1);
        }

        public override short GetLeg(bool isMonkey = true)
        {
            
            if (Type == DataCache.BOSS_SUPER_BLACK_GOKU_TYPE)
            {
                return 552;
            }
            //if (Type == DataCache.BOSS_GOKU_THIEN_SU_TYPE)
            //{
            //    return (short)1106;
            //}
            if (Type == 64 || Type == 80 || Type == 81)
            {
                return 524;
            }
            if (isClone)
            {
                return Leg;
            }
            return (short)(Hair + 2);
        }
    }
}