using System.Collections.Concurrent;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using System;

namespace TienKiemV2Remastered.Model.Clan
{
    public class Clan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Khẩu_hiệu { get; set; }
        public int ImgId { get; set; }
        public long Điểm_thành_tích { get; set; }
        public string LeaderName { get; set; }
        public int Điểm_Danh_Vọng { get; set; }
        public ClanLeader Leader {get;set;}
        public int Thành_viên_hiện_tại { get; set; }
        public int Tối_đa_thành_viên { get; set; }
        public int Cấp_Độ { get; set; }
        public int Capsule_Bang { get; set; }
        public int Thời_gian_tạo_bang { get; set; }
        public DateTime TimeClanCreate { get; set; }

        public List<ClanMember> Thành_viên { get; set; }
        public List<ClanMessage> Messages { get; set; }
        public IClanHandler ClanHandler { get; set; }
        public List<Item.Item> ClanBox { get; set; }
        public List<CharacterPea> CharacterPeas { get; set; }
        public long DelayUpdate { get; set; }
        public bool IsSave { get; set; }

        
        public Treasure bdkb;
        public CDRC cdrd;
        public Reddot Reddot { get; set; }
        public Gas Gas;
        public BlackBallHandler.ForClan.ClanManagerr DataBlackBall{get;set;}
        public ClanZone ClanZone { get; set; }
        public ClanBoss ClanBoss { get; set; }
        public string shortName { get; set; }
        public Clan()
        {
            shortName = "";
            ClanBoss = new ClanBoss();
            ClanZone = new ClanZone();
            DataBlackBall = new BlackBallHandler.ForClan.ClanManagerr();
            Khẩu_hiệu = "";
            Cấp_Độ = 1;
            Capsule_Bang = 0;
            Thành_viên = new List<ClanMember>();
            Messages = new List<ClanMessage>();
            CharacterPeas = new List<CharacterPea>();
            ClanHandler = new ClanHandler(this);
            DelayUpdate = 300000 + ServerUtils.CurrentTimeMillis();
            IsSave = true;
            
            Reddot = new Reddot();
            Gas = new Gas();
            bdkb = new Treasure();
            ClanBox = new List<Item.Item>();
            bdkb = new Treasure();
            cdrd = new CDRC();
            //doanhtrai = new DoanhTrai();
            //khigas = new Gas();
            //bdkb.MapBDKB = new List<Application.Threading.Map>(1);

            //   cdrd.MapCDRD = new List<Application.Threading.Map>(1);
            //   doanhtrai.ReddotMaps = new List<Application.Threading.Map>(1);
            //   khigas.MapKhiGas = new List<Application.Threading.Map>(1);

        }
    }
}