using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Application.Extension.Bosses;
using TienKiemV2Remastered.Sources.Base.Info;
using TienKiemV2Remastered.Application.Extension.Namecball;
using static System.GC;
using TienKiemV2Remastered.Model.Task;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using TienKiemV2Remastered.Application.Extension.Bo_Mong;
using static TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip;
using TienKiemV2Remastered.Application.Extension.ChampionShip;
using TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23;
using TienKiemV2Remastered.Application.Extension.Ký_gửi;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.Transfaction.Thách_Đấu;
using TienKiemV2Remastered.Application.Extension.Chẵn_Lẻ_Momo;
using TienKiemV2Remastered.Application.MapPrivate;
using TienKiemV2Remastered.Application.Train;
using static TienKiemV2Remastered.Application.Extension.Super_Champion.SieuHang;
using static TienKiemV2Remastered.Application.Extension.Namecball.NamecBallHandler;

namespace TienKiemV2Remastered.Model.ModelBase
{
    public class CharacterBase : ICharacter
    {

        public ICharacterHandler CharacterHandler { get; set; }
        public Zone Zone { get; set; }
        public int Id { get; set; }
        public int ClanId { get; set; }
        public Player Player { get; set; }
        public InfoFriend Info { get; set; }
        public string Name { get; set; }
        public InfoChar InfoChar { get; set; }
        public TaskInfo InfoTask { get; set; }
        public long DameBossBangHoi { get; set; }
        public int TypeTeleport { get; set; }
        public int TypeDragon { get; set; }
        public sbyte Flag { get; set; }
        public long HpFull { get; set; }
        public long MpFull { get; set; }
        public int DamageFull { get; set; }
        public int DefenceFull { get; set; }
        public int CritFull { get; set; }
        public int HpPlusFromDamage { get; set; }
        public int MpPlusFromDamage { get; set; }
        public int HpPlusFromDamageMonster { get; set; }
        public bool IsGetHpFull { get; set; }
        public bool IsGetMpFull { get; set; }
        public bool IsGetDamageFull { get; set; }
        public bool IsGetDefenceFull { get; set; }
        public bool IsGetCritFull { get; set; }
        public bool IsHpPlusFromDamage { get; set; }
        public bool IsMpPlusFromDamage { get; set; }
        public int DiemSuKien { get; set; }
        public List<SkillCharacter.SkillCharacter> Skills { get; set; }
        public List<Item.Item> ItemBody { get; set; }
        public List<Item.Item> ItemBag { get; set; }
        public List<Item.Item> ItemBox { get; set; }
        public InfoSkill InfoSkill { get; set; }
        public Effect.Effect Effect { get; set; }
        public InfoOption InfoOption { get; set; }
        public InfoSet InfoSet { get; set; }
        public ChanLe DataMiniGame { get; set; }
        
        public Enchant DataEnchant { get; set; }
        public BoMong DataBoMong { get; set; }
        public DataCharacter DataDaiHoiVoThuat { get; set; }
        public DataDaiHoiVoThuat23 DataDaiHoiVoThuat23 { get; set; }
        public Thách_Đấu Challenge { get; set; }
        public Died_Ring DataVoDaiSinhTu { get; set; }
        public Clone Clone { get; set; }
        public MapPrivate MapPrivate { get; set; }
        public NamecBallHandler.DataNgocRongNamek DataNgocRongNamek { get; set; }
        public DataTraining DataTraining{get;set;}      
        public InfoPlayer DataSieuHang {get;set;}
        public BlackBallHandler.ForPlayer Blackball { get; set; }

        public bool IsInvisible()
        {    
            return ItemBody[5] != null && ItemBody[5].Options.FirstOrDefault(i => i.Id == 105) != null;
        }

        public CharacterBase()
        {
            DameBossBangHoi = 0;
            DataSieuHang = new InfoPlayer();
            Blackball = new BlackBallHandler.ForPlayer();
            DataTraining = new DataTraining();
            ClanId = -1;
            Flag = 0;
            InfoChar = new InfoChar();
            HpFull = InfoChar.OriginalHp;
            MpFull = InfoChar.OriginalMp;
            DamageFull = InfoChar.OriginalDamage;
            DefenceFull = InfoChar.OriginalDefence;
            CritFull = InfoChar.OriginalCrit;
            Skills = new List<SkillCharacter.SkillCharacter>();
            ItemBody = new List<Item.Item>(BodyLength());
            for(var i = 0; i < BodyLength(); i++) ItemBody.Add(null);
            ItemBag = new List<Item.Item>();
            ItemBox = new List<Item.Item>();
            InfoSkill = new InfoSkill();
            Effect = new Effect.Effect();
            InfoOption = new InfoOption();
            InfoSet = new InfoSet();
            SetGetFull(true);
            DataMiniGame = new ChanLe();
            DataEnchant = new Enchant();
            DataBoMong = new BoMong();
            for (var i = 0; i < Cache.Gi().TASK_BO_MONG.Count; i++)
            {
                DataBoMong.isFinish.Add(false);
                DataBoMong.isCollect.Add(false);
                DataBoMong.Count.Add(0);
            }
            Clone = new Clone();
            DataDaiHoiVoThuat23 = new DataDaiHoiVoThuat23();
            DataDaiHoiVoThuat = new DataCharacter();
            DataVoDaiSinhTu = new Died_Ring();
            DataNgocRongNamek = new DataNgocRongNamek();
            InfoTask = new TaskInfo();
        }
        public void PlusGold(int gold)
        {
            
        }
        public virtual short GetHead(bool isMonkey = true)
        {
                if (InfoSkill.MaPhongBa.isMaPhongBa) return 1221;
            if (InfoSkill.HoaBang.isHoaBang) return 1210;
            if(isMonkey && InfoSkill.Socola.IsCarot)  return 406;
            if(isMonkey && InfoSkill.Socola.IsSocola) return 412;
            if (isMonkey && InfoSkill.HoaDa.IsHoaDa) return 454;
            if (isMonkey && InfoSkill.Monkey.HeadMonkey != -1) return InfoSkill.Monkey.HeadMonkey;

            if (InfoChar.Fusion.IsFusion) {
                if (InfoChar.Gender == 1 && !InfoChar.Fusion.IsPorata2) {
                    return 391;
                }

                if (InfoChar.Fusion.IsPorata2)
                {
                    switch (InfoChar.Gender)
                    {
                        case 0:
                        {
                            return 870;
                        }
                        case 1:
                        {
                            return 873;
                        }
                        case 2:
                        {
                            return 867;
                        }
                    }
                }
                else if (InfoChar.Fusion.IsPorata)
                {
                    return 383;
                }
                else 
                {
                    return 380;
                }
            }
                   
            var item = ItemBody[5];
            if (item == null) return InfoChar.Hair;

            if (ItemCache.GetCaiTrangById(item.Id))
            {
                return ItemCache.GetHeadByCaiTrangid(item.Id);
            }
            return ItemCache.GetAvatarById(item.Id) ? ItemCache.GetHeadByCaiTrangid(item.Id) : InfoChar.Hair;
        }

        public virtual short GetBody(bool isMonkey = true)
        {
            if (InfoSkill.MaPhongBa.isMaPhongBa) return 1222;
            if (InfoSkill.HoaBang.isHoaBang) return 1211;
            if (isMonkey && InfoSkill.Socola.IsCarot)  return 407;
            if(isMonkey && InfoSkill.Socola.IsSocola)  return 413;
            if (isMonkey && InfoSkill.HoaDa.IsHoaDa) return 455;
            if (isMonkey && InfoSkill.Monkey.BodyMonkey != -1) return InfoSkill.Monkey.BodyMonkey;
            var headPart = GetHead();
            if (InfoChar.Fusion.IsFusion) 
            {
                return (short)(headPart + 1);
            }
            var item = ItemBody[5];
            if (item != null)
            {
                if (ItemCache.GetCaiTrangById(item.Id))
                {
                    return ItemCache.GetBodyByCaiTrangid(item.Id);
                }
            }

            item = ItemBody[0];
            if (item != null)
            {
                return ItemCache.ItemTemplate(item.Id).Part;
            }
            return InfoChar.Gender == 1 ? (short)59 : (short)57;
        }

        public virtual short GetLeg(bool isMonkey = true)
        {
            if (InfoSkill.MaPhongBa.isMaPhongBa) return 1223;
            if (InfoSkill.HoaBang.isHoaBang) return 1212;
            if (isMonkey && InfoSkill.Socola.IsCarot)  return 408;
            if(isMonkey && InfoSkill.Socola.IsSocola)  return 414;
            if (isMonkey && InfoSkill.HoaDa.IsHoaDa) return 456;
            if (isMonkey && InfoSkill.Monkey.LegMonkey != -1) return InfoSkill.Monkey.LegMonkey;        
            var headPart = GetHead();
            if (InfoChar.Fusion.IsFusion) 
            {
                return (short)(headPart + 2);
            }
            var item = ItemBody[5];
            
            if (item != null)
            {
                if (ItemCache.GetCaiTrangById(item.Id))
                {
                    return ItemCache.GetLegByCaiTrangid(item.Id);
                }                
            }
            item = ItemBody[1];
            if (item != null)
            {
                return ItemCache.ItemTemplate(item.Id).Part;
            }
            return InfoChar.Gender == 1 ? (short)60 : (short)58;
        }

        public short GetBag()
        {
            return InfoChar.PhukienPart > 0 ? InfoChar.PhukienPart : InfoChar.Bag;
        }

        public void SetGetFull(bool isGet)
        {
            IsGetHpFull = isGet;
            IsGetMpFull = isGet;
            IsGetDamageFull = isGet;
            IsGetDefenceFull = isGet;
            IsGetCritFull = isGet;
            IsHpPlusFromDamage = isGet;
            IsMpPlusFromDamage = isGet;
        }

        public virtual int LengthBagNull()
        {
            return 20 - ItemBag.Count;
        }

        public virtual int LengthBoxNull()
        {
            return 20 - ItemBox.Count;
        }

        public virtual int BagLength()
        {
            return 20;
        }

        public virtual int BoxLength()
        {
            return 20;
        }
        
        public virtual int BodyLength()
        {
            return 12;
        }

        public bool CheckLockInventory()
        {
            if (InfoChar.LockInventory.IsLock)
            {
                CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().LOCK_INVENTORY));
                return false;
            }
            return true;
        }

        public bool IsDontMove()
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            if (InfoChar.IsDie || InfoSkill.ThaiDuongHanSan.IsStun || InfoSkill.DichChuyen.IsStun
                || InfoSkill.PlayerTroi.IsPlayerTroi
                || InfoSkill.ThoiMien.IsThoiMien
                || InfoSkill.HoaDa.IsHoaDa
                || InfoSkill.HoaBang.isHoaBang
                || InfoSkill.Monkey.IsStart || InfoSkill.TuSat.Delay > timeServer || InfoSkill.Laze.Time > timeServer || InfoSkill.Qckk.Time > timeServer) return true;
            return false;
        }

        public void Clear() => SuppressFinalize(this);

        public void Dispose()
        {
            SuppressFinalize(this);
        }
    }
}