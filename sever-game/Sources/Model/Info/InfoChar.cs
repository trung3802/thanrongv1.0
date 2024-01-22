using System.Collections.Concurrent;
using System.Collections.Generic;
using TienKiemV2Remastered.Model.Info.Radar;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Task;
using TienKiemV2Remastered.Application.Constants;
using System;
using TienKiemV2Remastered.Application.Extension.Task_Everyday;

namespace TienKiemV2Remastered.Model.Info
{
    public class InfoChar
    {
        public sbyte NClass { get; set; }
        public sbyte Gender { get; set; }
        public int MapId { get; set; }
        public int MapCustomId { get; set; }
        public int ZoneId { get; set; }
        public short Hair { get; set; }
        public sbyte Bag { get; set; }
        public sbyte Level { get; set; }
        public sbyte Speed { get; set; }
        public sbyte Pk { get; set; }
        public sbyte TypePk { get; set; }
        public long Potential { get; set; }
        public long TotalPotential { get; set; }
        public long Power { get; set; }
        public bool IsDie { get; set; }
        public bool IsPower { get; set; }
        public int LitmitPower { get; set; }
        
        public List<sbyte> KSkill { get; set; }
        public List<sbyte> OSkill { get; set; }
        public short CSkill { get; set; }
        public long CSkillDelay { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public byte HpFrom1000 { get; set; } 
        public byte MpFrom1000 { get; set; } 
        public byte DamageFrom1000 { get; set; } 
        public byte Exp { get; set; } 
        public long OriginalHp { get; set; } 
        public long OriginalMp { get; set; } 
        public int OriginalDamage { get; set; }
        public int OriginalDefence { get; set; } 
        public int OriginalCrit { get; set; } 
        public long Hp { get; set; }
        public long Mp { get; set; }
        public short Stamina { get; set; }
        public int VIP { get; set; }
        public short MaxStamina { get; set; }
        public int NangDong { get; set; }
        public short MountId { get; set; }
        public int Teleport { get; set; }
        public long Gold { get; set; }
        public long Diamond { get; set; }
        public long DiamondLock { get; set; }
        public long LimitGold { get; set; }
        public long LimitDiamond { get; set; }
        public long LimitDiamondLock { get; set; }
        public bool IsNewMember { get; set; }
        public bool IsNhanBua { get; set; }
        public short PhukienPart { get; set; }
        public bool IsHavePet { get; set; }
        public bool IsPremium { get; set; }
        public long ThoiGianTrungMaBu { get; set; } //Thời gian trứng Ma Bư, != 0 là có trứng
        public long ThoiGianDuaHau { get; set; } //Thời gian trứng Ma Bư, != 0 là có trứng

        public long TimeAutoPlay { get; set; }
        public short CountGoiRong { get; set; }
        public Fusion Fusion { get; set; }
        public LockInventory LockInventory { get; set; }
        public LearnSkill LearnSkill { get; set; }
        public LearnSkill LearnSkillTemp { get; set; }
        public ConcurrentDictionary<short, long> ItemAmulet { get; set; }
        public ConcurrentDictionary<short, Card> Cards { get; set; }
        

        // Tain mảnh vỡ bông tai
        public int TrainManhVo { get; set; }
        public int TrainManhHon { get; set; }
        public int SoLanGiaoDich { get; set; }
        public long ThoiGianGiaoDich { get; set; }
        public long ThoiGianChatTheGioi { get; set; }
        public long ThoiGianDoiMayChu { get; set; }

        public bool HieuUngDonDanh { get; set; }
        public short EffectAuraId { get; set; }
        public sbyte idEff_Set_Item { get; set; }
        public short idHat { get; set; }
        public short PetId { get; set; }

        public Mission InfoSideTask { get; set; }
        //public int vip { get; set; }
        //  public int VeTinh { get;set ; }
       

       // public int Linh_Thú_ID { get; set; }

       // public int LinhThuImage { get; set; }
       // public int LinhThuFrame { get; set; }
       public bool isDiemDanh { get; set; }
        public int PointQuayThuong { get; set; }
        public int PointSanBoss { get; set; }
        public bool IsOutZone { get; set; }
        public InfoChar()
        {
            VIP = 0;
            IsOutZone = false;
            PointQuayThuong = 0;
            PointSanBoss = 0;
            isDiemDanh = false;
            IsNewMember = true;
            MapCustomId = -1;
            Hair = 0;
            Level = 1;
            Speed = 5;
            Pk = 0;
            Bag = -1;
            TypePk = 0;
            PhukienPart = -1;
            Potential = 0;
            Power = 1200;
            IsDie = false;
            IsPower = true;
            HpFrom1000 = 20;
            MpFrom1000 = 20;
            DamageFrom1000 = 1;
            Exp = 100;
            OriginalHp = Hp = 100;
            OriginalMp = Mp = 100;
            OriginalDamage = 15;
            OriginalDefence = 0;
            OriginalCrit = 0;
            Stamina = 10000;
            MaxStamina = 10000;
            MountId = -1;
            Teleport = 1;
            LockInventory = new LockInventory();
            KSkill = new List<sbyte>();
            OSkill = new List<sbyte>();
            CSkill = 0;
            CSkillDelay = 500;
            Gold = 0;
            Diamond = 0;
            DiamondLock = 0;
            LimitGold = 50000000000;
            LimitDiamond = 100000;
            LimitDiamondLock = 200000000;
            IsNhanBua = false;
            IsHavePet = false;
            Fusion = new Fusion();
            ItemAmulet = new ConcurrentDictionary<short, long>();
            Cards = new ConcurrentDictionary<short, Card>();
            IsPremium = false;
            ThoiGianTrungMaBu = 0;
            SoLanGiaoDich = 0; 
            ThoiGianGiaoDich = 0;
            LitmitPower = 0;
            TimeAutoPlay = 0;
            CountGoiRong = 0;
            TrainManhVo = 0;
            TrainManhHon = 0;
            ThoiGianChatTheGioi = 0;
            HieuUngDonDanh = true;
            EffectAuraId = -1;
            idEff_Set_Item = -1;
            idHat = -1;
            PetId = -1;
            
            //PetImei = -1;
            ThoiGianDoiMayChu = 0;
            InfoSideTask = new Application.Extension.Task_Everyday.Mission();
           
          //  LinhThuFrame = -1;
          //  LinhThuImage = -1;
          //  Linh_Thú_ID = -1;
            // VeTinh = 0;
        }

    }
}