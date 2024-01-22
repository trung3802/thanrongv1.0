using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Model.ModelBase;
using TienKiemV2Remastered.Model.SkillCharacter;
using TienKiemV2Remastered.Application.Extension.Chẵn_Lẻ_Momo;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using TienKiemV2Remastered.Sources.Base.Info;
using static TienKiemV2Remastered.Application.Extension.Super_Champion.SieuHang;
using TienKiemV2Remastered.Application.Extension.Transfaction.Thách_Đấu;

namespace TienKiemV2Remastered.Model.Character
{
    public class Character : CharacterBase
    {
      
        public int TypeDoiThuong { get; set; }
        public int TypeInput { get; set; }
        public int TypeMenu { get; set; }
        public int TypeShop { get; set; }
        public int MenuPage { get; set; }
        public int ShopId { get; set; }
        public int PlusBag { get; set; }
        public int PlusBox { get; set; }
        public Test Test { get; set; }
        public List<int> BoughtSkill { get; set; }
        public List<MapTranspot> MapTranspots { get; set; }

        public MapTranspot MapOld{ get; set; }
        public int MapIdOld{ get; set; }
        public List<int> CombinneIndex { get; set; }
        public InfoDelay Delay { get; set; }
        public List<InfoFriend> Friends { get; set; }
        public List<InfoFriend> Enemies { get; set; }
        public InfoFriend Me { get; set; }
        public InfoFriend FriendTemp { get; set; }
        public InfoFriend EnemyTemp { get; set; }
        public InfoMore InfoMore { get; set; }
        public InfoNapThe NapTheTemp { get; set; }
        public Trade Trade { get; set; }
        public int CodeInviteClan { get; set; }
        public List<Item.Item> LuckyBox { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsItem193 { get; set; }
        public bool IsItem194 { get; set; }
        public bool isNRSD { get; set; }
        public bool IsNextMap { get; set; }
        public Position TrungMaBuPosition { get; set; }
        public Position DuaHauPosition { get; set; }
        public SpecialSkill SpecialSkill { get; set; }
        public InfoBuff InfoBuff { get; set; }
        public Disciple Disciple { get; set; }
        public Pet Pet { get; set; }
        public CDRD ConDuongRanDoc { get; set; }
        public Boolean CollectEveryDay { get; set; }
        public bool isColer { get; set; }
        public bool IsRevenge { get; set; }
        public int PPower { get; set; }
        public bool BeforeDispose { get; set; }
        public List<int> ListCollectBlackBall { get; set; }
        //  public Chẵn_Lẻ_Momo DataMiniGame { get; set; }
        public class CDRD
        {
            public bool isCDRD { get; set; }
            public bool TalkWithKarinCat { get; set; }
            public CDRD()
            {
                isCDRD = false;
                TalkWithKarinCat = false;
            }
        }
        public Character()
        {
            TypeShop = -1;
            //DameBossBangHoi = 0;
            ListCollectBlackBall = new List<int>();
            BeforeDispose = false;
            CollectEveryDay = false;
            isColer = false;
            IsRevenge = false;
            PPower = 0;
            TypeDoiThuong = -1;
            Disciple = null;
            Pet = null;
            ClanId = -1;
            TypeInput = -1;
            TypeMenu = -1;
            MenuPage = -1;
            ShopId = -1;
            PlusBag = 0;
            PlusBox = 0;
            IsItem193 = false;
            IsItem194 = false;
            Delay = new InfoDelay();
            BoughtSkill = new List<int>();
            Friends = new List<InfoFriend>();
            Enemies = new List<InfoFriend>();
            Me = new InfoFriend(this);
            Trade = new Trade();
            LuckyBox = new List<Item.Item>();
            InfoMore = new InfoMore();
            InfoBuff = new InfoBuff();
            NapTheTemp = new InfoNapThe();
            CharacterHandler = new CharacterHandler(this);
            LastLogin = ServerUtils.TimeNow();
            IsNextMap = false;
            TrungMaBuPosition = new Position();
            DuaHauPosition = new Position();
            SpecialSkill = new SpecialSkill();
            Challenge = new Thách_Đấu();
            ConDuongRanDoc = new CDRD();
            DataSieuHang = new InfoPlayer();
            DataMiniGame = new ChanLe();
            //DataMiniGame = new Chẵn_Lẻ_Momo();
            //DataEnchant = new Sources.Model.Info.DataEnchant();
        }

        public Character(Player player)
        {
            TypeShop = -1;
            IsRevenge = false;
            PPower = 0;
            Disciple = null;
            Pet = null;
            ClanId = -1;
            Player = player;
            TypeInput = -1;
            TypeMenu = -1;
            MenuPage = -1;
            ShopId = -1;
            PlusBag = 0;
            PlusBox = 0;
            isNRSD = false;
            IsItem193 = false;
            IsItem194 = false;
            Delay = new InfoDelay();
            BoughtSkill = new List<int>();
            Friends = new List<InfoFriend>();
            Enemies = new List<InfoFriend>();
            Me = new InfoFriend(this);
            Trade = new Trade();
            LuckyBox = new List<Item.Item>();
            InfoMore = new InfoMore();
            InfoBuff = new InfoBuff();
            NapTheTemp = new InfoNapThe();
            CharacterHandler = new CharacterHandler(this);
            LastLogin = ServerUtils.TimeNow();
            IsNextMap = false;
            Blackball = new BlackBallHandler.ForPlayer();
            TrungMaBuPosition = new Position();
            DuaHauPosition = new Position();
            SpecialSkill = new SpecialSkill();
            Challenge = new Thách_Đấu();
            DataSieuHang = new InfoPlayer();

            //DataMiniGame = new Chẵn_Lẻ_Momo();
            //DataEnchant = new Sources.Model.Info.DataEnchant();
        }

        public override int BagLength()
        {
            return 20 + PlusBag;
        }
        
        public override int BoxLength()
        {
            return 20 + PlusBox;
        }

        public override int LengthBagNull()
        {
            return BagLength() - ItemBag.Count;
        }
        
        public override int LengthBoxNull()
        {
            return BoxLength() - ItemBox.Count;
        }

        //public long AllDiamond()
        //{
        //    return InfoChar.Diamond + InfoChar.DiamondLock;
        //}
        public long AllDiamond()
        {
            return InfoChar.Diamond;
        }
        public long AllDiamondLock()
        {
            return InfoChar.DiamondLock;
        }
        public void PlusGold(long gold)
        {
            lock (InfoChar)
            {
                InfoChar.Gold += gold;
                if (InfoChar.Gold > InfoChar.LimitGold) InfoChar.Gold = InfoChar.LimitGold;
            }
        }

        public void PlusLimitGold(long gold)
        {
            lock (InfoChar)
            {
                InfoChar.LimitGold += gold;
            }
        }

        public void PlusDiamond(long diamond)
        {
            lock (InfoChar)
            {
                InfoChar.Diamond += diamond;
                if (InfoChar.Diamond > InfoChar.LimitDiamond) InfoChar.Diamond = InfoChar.LimitDiamond;
            }
            
        }

        public void PlusLimitDiamond(long diamond)
        {
            lock (InfoChar)
            {
                InfoChar.LimitDiamond += diamond;
            }
            
        }

        public void PlusDiamondLock(int diamond)
        {
            lock (InfoChar)
            {
                InfoChar.DiamondLock += diamond;
                if (InfoChar.DiamondLock > InfoChar.LimitDiamondLock) InfoChar.DiamondLock = InfoChar.LimitDiamondLock;
            }
            
        }

        public void PlusLimitDiamondLock(int diamond)
        {
            lock (InfoChar)
            {
                InfoChar.LimitDiamondLock += diamond;
            }
            
        }
        
        public void MineGold(long gold)
        {
            // if (!CheckLockInventory()) return;
            lock (InfoChar)
            {
                InfoChar.Gold -= gold;
            }

        }
        
        public void MineDiamond(int diamond, int type = 1)
        {
            // if (!CheckLockInventory()) return;
            lock (InfoChar)
            {
                switch (type)
                {
                    case 0:
                    {
                        if (AllDiamond() >= diamond)
                        {
                            InfoChar.DiamondLock -= diamond;
                            if (InfoChar.DiamondLock >= 0) return;
                            InfoChar.Diamond += InfoChar.DiamondLock;
                            InfoChar.DiamondLock = 0;
                        }
                        break;
                    }
                    //Mine Diamond
                    case 1:
                    {
                        if (InfoChar.Diamond >= diamond)
                        {
                            InfoChar.Diamond -= diamond;
                        }
                        break;
                    }
                    //Mine Dimond Lock
                    case 2:
                    {
                        if (InfoChar.DiamondLock >= diamond)
                        {
                            InfoChar.DiamondLock -= diamond;
                        }
                        break;
                    }
                    case 3:
                        
                        break;
                }
            }

        }

        public void UpdateOldMap()
        {
            MapTranspots ??= new List<MapTranspot>();
            MapTranspots.Clear();
           
                MapTranspots.AddRange(DataCache.GMapTranspots);
                MapTranspots[0] ??= new MapTranspot()
                { Id = InfoChar.Gender + 21, Info = "Về nhà", Name = TextTask.NameHanhTinh[InfoChar.Gender] };
                MapTranspots[9] ??= new MapTranspot() { Id = InfoChar.Gender + 24, Info = "Trạm tàu vũ trụ", Name = TextTask.NameHanhTinh[InfoChar.Gender] };
                if (MapOld == null)
                {
                    MapTranspots.RemoveAll(map => map.Id == InfoChar.MapId);
                }
                else
                {
                    MapTranspots.RemoveAll(map => map.Id == InfoChar.MapId || (map.Id == MapIdOld && MapIdOld - 21 != InfoChar.Gender));
                    if (MapIdOld - 21 != InfoChar.Gender)
                        MapTranspots.Insert(0, MapOld);
                }
            
        }
        public void SetMapNRSD()
        {
            MapTranspots ??= new List<MapTranspot>();
            MapTranspots.Clear();
            MapTranspots.AddRange(DataCache.NMapTranspots);            
        }
        public void SetOldMap()
        {
            if (MapIdOld - 21 != InfoChar.Gender && !DataCache.IdMapCustom.Contains(MapIdOld) && !DataCache.IdMapSpecial.Contains(MapIdOld))
            {
                MapOld = new MapTranspot()
                {
                    Id = MapIdOld,
                    Info = $"Về chỗ cũ {Cache.Gi().TILE_MAPS[MapIdOld].Name}",
                    Name = TextTask.NameHanhTinh[Cache.Gi().TILE_MAPS[MapIdOld].PlanetID]
                };
            }
        }

        public int GetTotalDauThanBag()
        {
            return ItemBag.Where(item => DataCache.IdDauThan.Contains(item.Id)).Sum(item => item.Quantity);
        }

        public int GetTotalDauThanBox()
        {
            return ItemBox.Where(item => DataCache.IdDauThan.Contains(item.Id)).Sum(item => item.Quantity);
        }

        public void CloseTrade(bool isClose)
        {
            Trade.IsTrade = false;
            Trade.IsHold = false;
            Trade.IsLock = false;
            Trade.CharacterId = -1;
            Trade.Items.Clear();
            Delay.Trade = 30000 + ServerUtils.CurrentTimeMillis();
            if(isClose) CharacterHandler.SendMessage(Service.ClosePanel());
        }

        public void SetUpTrungMaBuPosition()
        {
            switch (InfoChar.Gender)
           {
               case 0:
               {
                   TrungMaBuPosition.X = 683;
                   DuaHauPosition.X = 401;
                   DuaHauPosition.Y = 336;
                   TrungMaBuPosition.Y = 336;
                   break;
               }
               case 1:
               {
                   TrungMaBuPosition.X = 697;
                   DuaHauPosition.X = 401;
                   DuaHauPosition.Y = 336;
                   TrungMaBuPosition.Y = 336;
                   break;
               }
               case 2:
               {
                   DuaHauPosition.X = 401;
                   DuaHauPosition.Y = 336;
                   TrungMaBuPosition.X = 688;
                   TrungMaBuPosition.Y = 336;
                   break;
               }
           }
        }

        public void SetupAmulet()
        {
            lock(InfoMore)
            {
                var timeServer = ServerUtils.CurrentTimeMillis();
                short buaTriTue = 213;
                if (InfoChar.ItemAmulet.ContainsKey(buaTriTue))
                {
                    // Kiểm tra thời gian của bùa
                    // Tiềm năng và sức mạnh của bạn sẽ nhận được gấp đôi trong 1 khoảng thời gian, kể từ lúc mua
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaTriTue).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaTriTue = true;
                        InfoMore.BuaTriTueTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaTriTue = false;
                    }
                }

                // Bùa Trí Tuệ x3
                short buaTriTueX3 = 671;
                if (InfoChar.ItemAmulet.ContainsKey(buaTriTueX3))
                {
                    // Kiểm tra thời gian của bùa
                    // Tiềm năng và sức mạnh của bạn sẽ nhận được gấp đôi trong 1 khoảng thời gian, kể từ lúc mua
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaTriTueX3).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaTriTueX3 = true;
                        InfoMore.BuaTriTueX3Time = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaTriTueX3 = false;
                    }
                }

                // Bùa Trí Tuệ x4
                short buaTriTueX4 = 672;
                if (InfoChar.ItemAmulet.ContainsKey(buaTriTueX4))
                {
                    // Kiểm tra thời gian của bùa
                    // Tiềm năng và sức mạnh của bạn sẽ nhận được gấp đôi trong 1 khoảng thời gian, kể từ lúc mua
                    var amuletTime  = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaTriTueX4).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaTriTueX4 = true;
                        InfoMore.BuaTriTueX4Time = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaTriTueX4 = false;
                    }
                }

                short buaDeoDai = 218;
                if (InfoChar.ItemAmulet.ContainsKey(buaDeoDai))
                {
                    // Kiểm tra thời gian của bùa
                    // Thể lực của bạn sẽ không bao giờ giảm khi bùa này có tác dụng. Có tác dụng trong 1 khoảng thời gian, kể từ lúc mua
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaDeoDai).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaDeoDai = true;
                        InfoMore.BuaDeoDaiTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaDeoDai = false;
                    }
                }

                short buaThuHut = 219;
                if (InfoChar.ItemAmulet.ContainsKey(buaThuHut))
                {
                    // Kiểm tra thời gian của bùa
                    // Tăng sức chịu đòn cho bạn. Khi bị quái đánh, máu sẽ mất ít hơn, chỉ còn 50% bình thường. Có tác dụng trong 1 khoảng thời gian kể từ lúc mua
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaThuHut).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaThuHut = true;
                        InfoMore.BuaThuHutTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaThuHut = false;
                    }
                }

                short buaDaTrau = 215;
                if (InfoChar.ItemAmulet.ContainsKey(buaDaTrau))
                {
                    // Kiểm tra thời gian của bùa
                    // Tăng sức chịu đòn cho bạn. Khi bị quái đánh, máu sẽ mất ít hơn, chỉ còn 50% bình thường. Có tác dụng trong 1 khoảng thời gian kể từ lúc mua
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaDaTrau).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaDaTrau = true;
                        InfoMore.BuaDaTrauTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaDaTrau = false;
                    }
                }

                short buaDeTu = 522;
                if (InfoChar.ItemAmulet.ContainsKey(buaDeTu))
                {
                    var amuletTime = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaDeTu).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaDeTu = true;
                        InfoMore.BuaDeTuTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaDeTu = false;
                    }
                }

                short buaManhMe = 214;
                if (InfoChar.ItemAmulet.ContainsKey(buaManhMe))
                {
                    // Kiểm tra thời gian của bùa
                    // Cú đấm của bạn sẽ mạnh hơn. Tăng 150% sức đánh hiện có khi bạn đánh Quái trong 1 khoảng thời gian, kể từ lúc mua
                    var amuletTime  = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaManhMe).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaManhMe = true;
                        InfoMore.BuaManhMeTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaManhMe = false;
                    }
                }

                short buaBatTu = 217;
                if (InfoChar.ItemAmulet.ContainsKey(buaBatTu))
                {
                    // Kiểm tra thời gian của bùa
                    // Bạn sẽ không bao giờ bị quái đánh chết. Thay vào đó chỉ còn 1 máu. Tuy nhiên bạn phải bơm máu mới có thể đánh lại. Có tác dụng trong 1 khoảng thời gian kể từ lúc mua.
                    var amuletTime  = InfoChar.ItemAmulet.FirstOrDefault(i => i.Key == buaBatTu).Value;
                    if (amuletTime > timeServer)
                    {
                        InfoMore.BuaBatTu = true;
                        InfoMore.BuaBatTuTime = amuletTime;
                    }
                    else 
                    {
                        InfoMore.BuaBatTu = false;
                    }
                }
            }
        }
    }
}