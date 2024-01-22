using System;
using System.Linq;
using Linq.Extras;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Info.InfoDiscipler;
using TienKiemV2Remastered.Model.ModelBase;
using TienKiemV2Remastered.Model.Data;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Interfaces.Character;

namespace TienKiemV2Remastered.Model.Character
{
    public class Disciple : CharacterBase
    {
        public Character Character { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public long LevelPercent { get; set; }
        public bool IsFire { get; set; }
        public bool IsBienHinh { get; set; }
        public IMonster MonsterFocus { get; set; }
        public InfoDelayDisciple InfoDelayDisciple { get; set; }
        public PlusPoint PlusPoint { get; set; }
        public ICharacter CharacterFocus { get; set; }
        public Disciple(Character character)
        {
            Status = 0;
            InfoChar.Power = 2000;
            Name = "Đệ tử";
            Type = 1;
            Id = -character.Id;
            Character = character;
            InfoChar.Stamina = 1250;
            InfoChar.MaxStamina = 1250;
            IsFire = true;
            MonsterFocus = null;
            IsBienHinh = false;
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }

        public Disciple()
        {
            Status = 0;
            InfoChar.Power = 2000;
            Name = "Đệ tử";
            Type = 1;
            InfoChar.Stamina = 1250;
            InfoChar.MaxStamina = 1250;
            IsFire = true;
            IsBienHinh = false;
            MonsterFocus = null;
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }

        public void CreateNewDisciple(Character character, int gender)
        {
            InfoChar.Gender = (sbyte)gender;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Đệ tử";
            Type = 1;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 1250;
            InfoChar.MaxStamina = 1250;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(980, 2200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(980, 2200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(20, 60);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(20, 50);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(3, 6) : ServerUtils.RandomNumber(1, 4);

            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
            Info = new InfoFriend(this);
        }
        public void CreatePet(Character character, int type,sbyte gender)
        {
            InfoChar.Gender =gender;
            InfoChar.Power = 2000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            if (type == 1)
            {
                Name = "Đệ tử";
                Type = 1;
            }
            else if (type == 1)
            {
                Name = "Đệ Tử Mabư";
                Type = 2;
            }
            else if (type == 3)
            {
                Name = "Đệ Tử Pic Cô Nương";
                Type = 3;
            }
            else if (type == 4)
            {
                Name = "Đệ Tử Pic Nhạc Hội";
                Type = 4;
            }
            else if (type == 5)
            {
                Name = "Đệ Tử KingKong Mùa hè";
                Type = 5;
            }
            else if (type == 6)
            {
                Name = "Đệ Tử Ngộ Không";
                Type = 6;
            }
            else if (type == 7)
            {
                Name = "Đệ Tử Songoku";
                Type = 7;
            }
            else
            {
                Name = "Đệ Tử Ninja";
                Type = 8;
            }
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 1250;
            InfoChar.MaxStamina = 1250;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(980, 2200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(980, 2200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(20, 60);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(20, 50);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(3, 6) : ServerUtils.RandomNumber(1, 4);

            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }
        public void CreateNewMaBuDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Đệ tử Mabư";
            Type = 2;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(20, 60);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(20, 50);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
            Info = new InfoFriend(this);
        }
        public void CreateDiscipleTemplate(Character character,int id ,sbyte gender)
        {
            var temp = Cache.Gi().DISCIPLE_TEMPLATE.Values.FirstOrDefault(i => i.Id == id);
            if (temp != null)
            {
                InfoChar.Gender = gender;
                InfoChar.Power = 2000;
                InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
                Status = 0;
                Name = temp.Name;
                Type = temp.Type;
                Id = -character.Id;
                Character = character;
                Zone = character.Zone;
                InfoChar.Stamina = 2400;
                InfoChar.MaxStamina = 2400;
                IsFire = true;
                InfoChar.OriginalHp = InfoChar.Hp = temp.Hp;
                InfoChar.OriginalMp = InfoChar.Mp = temp.Mp;
                InfoChar.OriginalDamage = (int)temp.Damage;
                InfoChar.OriginalDefence = (int)temp.Defend;
                InfoChar.OriginalCrit = temp.Critical;
                var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
                Skills.Add(new SkillCharacter.SkillCharacter()
                {
                    Id = randomSkill,
                    SkillId = GetSkillId(randomSkill),
                    Point = 1,
                });
                PlusPoint = new PlusPoint();
                InfoDelayDisciple = new InfoDelayDisciple();
                CharacterHandler = new DiscipleHandler(this);
            }
            else{
                Character.CharacterHandler.SendMessage(Service.DialogMessage("NOT FIND PET WITH TYPE: " + id + "IN TEMPlATE !"));
            }
        }
        public void CreateNewKaminDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Đệ tử Kamin";
            Type = 3;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }

        public void CreateNewOrenDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Đệ tử Oren";
            Type = 4;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }

        public void CreateNewKaminOrenDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Đệ tử KaminOren";
            Type = 5;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }
        public void CreateNewNgoKhongDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Ngộ Không";
            Type = 6;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }
        public void CreateNewBillDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Breus";
            Type = 7;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }
        public void CreateNewDaishinkanDisciple(Character character, sbyte gender)
        {
            InfoChar.Gender = gender;
            InfoChar.Power = 1500000;
            InfoChar.Level = (sbyte)Cache.Gi().EXPS.Count(exp => exp < InfoChar.Power);
            Status = 0;
            Name = "Daishinkan";
            Type = 8;
            Id = -character.Id;
            Character = character;
            Zone = character.Zone;
            InfoChar.Stamina = 2400;
            InfoChar.MaxStamina = 2400;
            IsFire = true;
            InfoChar.OriginalHp = InfoChar.Hp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalMp = InfoChar.Mp = ServerUtils.RandomNumber(2200, 3200);
            InfoChar.OriginalDamage = ServerUtils.RandomNumber(100, 220);
            InfoChar.OriginalDefence = ServerUtils.RandomNumber(90, 100);
            InfoChar.OriginalCrit = ServerUtils.RandomNumber(100) < 10 ? ServerUtils.RandomNumber(4, 8) : ServerUtils.RandomNumber(2, 5);
            var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
            Skills.Add(new SkillCharacter.SkillCharacter()
            {
                Id = randomSkill,
                SkillId = GetSkillId(randomSkill),
                Point = 1,
            });
            PlusPoint = new PlusPoint();
            InfoDelayDisciple = new InfoDelayDisciple();
            CharacterHandler = new DiscipleHandler(this);
        }



        public static int GetSkillId(int id)
        {
            return id switch
            {
                19 => 121,
                99 => 693,
                _ => id * 7
            };
        }

        private short HeadLevel()
        {
            //var temp = Cache.gI().DISCIPLE_TEMPLATE.Values.FirstOrDefault(i => i.Id == Type);
            //if (temp != null)
            //{
            //    return (short)temp.Head;
            //}
            //else
            //{
                // Ma bư
                if (Type == 2)
                {
                    return 297;
                }
            else if (Type == 3)
            {
                return 1236;
            }

            else if (Type == 4)
            {
                return 1239;
            }
            else if (Type == 5)
            {
                return 1242;
            }
            else if (Type == 6)
            {
                return 462;
            }
            else if (Type == 7)
            {
                return 1291;
            }
            else if (Type == 8)
            {
                return 1306;
            }
            //    if (Type == 3)
            //    {
            //        return 989;
            //    }
            //    if (Type == 4)
            //{
            //    return (short)(InfoChar.Power > 150000000 ? 1266 : 1263);
            //}
            return InfoChar.Gender switch
                {
                    0 => InfoChar.Power <= 1500000 ? (short)285 : (short)304,
                    1 => InfoChar.Power <= 1500000 ? (short)288 : (short)305,
                    _ => InfoChar.Power <= 1500000 ? (short)282 : (short)303
                };

            //}
            //return 0;
        }

        private short BodyLevel()
        {
            //var temp = Cache.gI().DISCIPLE_TEMPLATE.Values.FirstOrDefault(i => i.Id == Type);
            //if (temp != null)
            //{
            //    return (short)temp.Body;
            //}
            //else
            //{
                // Ma bư
                if (Type == 2)
                {
                    return 298;
                }
            else if (Type == 3)
            {
                return 1237;
            }
            else if (Type == 4)
            {
                return 1240;
            }
            else if (Type == 5)
            {
                return 1243;
            }
            else if (Type == 6)
            {
                return 463;
            }
            else if (Type == 7)
            {
                return 1292;
            }
            else if (Type == 8)
            {
                return 1307;
            }
            //    if (Type == 3)
            //    {
            //        return 990;
            //    }
            //if (Type == 4)
            //{
            //    return 1264;
            //}
            return InfoChar.Gender switch
                {
                    0 => 286,
                    1 => 289,
                    _ => 283
                };
           // }
           // return 0;
        }

        private short LegLevel()
        {
            //var temp = Cache.gI().DISCIPLE_TEMPLATE.Values.FirstOrDefault(i => i.Id == Type);
            //if (temp != null)
            //{
            //    return (short)temp.Leg;
            //}
            //else
            //{
                // Ma bư
                if (Type == 2)
                {
                    return 299;
                }
            else if (Type == 3)
            {
                return 1238;
            }
            else if (Type == 4)
            {
                return 1241;
            }
            else if (Type == 5)
            {
                return 1244;
            }
            else if (Type == 6)
            {
                return 464;
            }
            else if (Type == 7)
            {
                return 1293;
            }
            else if (Type == 8)
            {
                return 1308;
            }
            //    if (Type == 3)
            //    {
            //        return 991;
            //    }
            //if (Type == 4)
            //{
            //    return 1265;
            //}
            return InfoChar.Gender switch
                {
                    0 => 287,
                    1 => 290,
                    _ => 284
                };
            //}
            //return 0;
        }

        public override short GetHead(bool isMonkey = true)
        {
            switch (isMonkey)
            {
                case true when InfoSkill.Socola.IsSocola:
                    return 609;
                case true when InfoSkill.Monkey.HeadMonkey != -1:
                    return InfoSkill.Monkey.HeadMonkey;
            }

            if (InfoChar.Fusion.IsFusion) return 383;
            var item = ItemBody[5];
            //if (item == null || (Type == 2 && !IsBienHinh)) return HeadLevel();
            if (item == null || (Type == 2 && !IsBienHinh) || (Type == 3 && !IsBienHinh) || (Type == 4 && !IsBienHinh) || (Type == 5 && !IsBienHinh) || (Type == 6 && !IsBienHinh) || (Type == 7 && !IsBienHinh) || (Type == 8 && !IsBienHinh)) return HeadLevel();

            var itemTemplate = ItemCache.ItemTemplate(item.Id);
            var part = itemTemplate.Part;
            //Check part #1
            if (part == -1)
            {
                try
                {
                    part = Cache.Gi().PARTS[itemTemplate.IconId];
                }
                catch (Exception)
                {
                    part = ItemCache.PartNotAvatar(item.Id);
                }
            }
            //Check part #2
            return part == -1 ? HeadLevel() : part;
        }

        public override short GetBody(bool isMonkey = true)
        {
            switch (isMonkey)
            {
                case true when InfoSkill.Socola.IsCarot:
                    return 407;
                case true when InfoSkill.Socola.IsSocola:
                    return 413;
                case true when InfoSkill.Monkey.BodyMonkey != -1:
                    return InfoSkill.Monkey.BodyMonkey;
            }

            if (InfoChar.Fusion.IsFusion) return 384;

            //if (Type == 2 && !IsBienHinh) return BodyLevel();
            if (Type == 2 && Type == 3 && Type == 4 && Type == 5 && Type == 6 && Type == 7 && Type == 8 && !IsBienHinh) return BodyLevel();

            var headPart = GetHead();
            var item = ItemBody[5];
            if (item != null && !ItemCache.IsAvatar(headPart))
            {
                return ItemCache.PartHeadToBody(headPart);
            }

            item = ItemBody[0];
            if (item != null)
            {
                return ItemCache.ItemTemplate(item.Id).Part;
            }
            return BodyLevel();
        }

        public override short GetLeg(bool isMonkey = true)
        {
            switch (isMonkey)
            {
                case true when InfoSkill.Socola.IsSocola:
                    return 611;
                case true when InfoSkill.Monkey.LegMonkey != -1:
                    return InfoSkill.Monkey.LegMonkey;
            }

            if (InfoChar.Fusion.IsFusion) return 385;

            //if (Type == 2 && !IsBienHinh) return LegLevel();
            if (Type == 2 && Type == 3 && Type == 4 && Type == 5 && Type == 6 && Type == 7 && Type == 8 && !IsBienHinh) return LegLevel();

            var headPart = GetHead();
            var item = ItemBody[5];
            if (item != null && !ItemCache.IsAvatar(headPart))
            {
                return ItemCache.PartHeadToLeg(headPart);
            }

            item = ItemBody[1];
            if (item != null)
            {
                return ItemCache.ItemTemplate(item.Id).Part;
            }
            return LegLevel();
        }

        public string CurrStrLevel()
        {
            GetDataLevel();
            var levels = Cache.Gi().LEVELS.Where(x => x.Gender == InfoChar.Gender).ToList();
            return $"{levels[InfoChar.Level].Name} {LevelPercent/100}.{LevelPercent%100}%" ;
        }

        private void GetDataLevel()
        {
            try
            {
                var num = 1L;
                var num2 = 0L;
                var num3 = 0;
                for (var num4 = Cache.Gi().EXPS.Count - 1; num4 >= 0; num4--)
                {
                    if (InfoChar.Power < Cache.Gi().EXPS[num4]) continue;
                    num = ((num4 != Cache.Gi().EXPS.Count - 1) ? (Cache.Gi().EXPS[num4 + 1] - Cache.Gi().EXPS[num4]) : 1);
                    num2 = InfoChar.Power - Cache.Gi().EXPS[num4];
                    num3 = num4;
                    break;
                }
                InfoChar.Level = (sbyte)num3;
                LevelPercent = (int)(num2 * 10000 / num);
            }
            catch (Exception)
            {
                //Ignored
            }
        }
    }
}