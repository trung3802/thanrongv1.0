using System;
using Microsoft.Extensions.Configuration;
using TienKiemV2Remastered.Application.Threading;

namespace TienKiemV2Remastered.DatabaseManager
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private readonly IConfiguration _configuration;
        public int Os { get; set; }
        public string Link { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public bool IsDebug { get; set; }
        public string MySqlHost { get; set; }
        public int MySqlPort { get; set; }
        public string MySqlUsername { get; set; }
        public string MySqlPassword { get; set; }
        public string MySqlDBData { get; set; }
        public string MySqlDBAccount { get; set; }
        public int MaxConnectSocket { get; set; }
        public int ExpUp { get; set; }
        public long LimitPowerExpUp { get; set; }
        public short ItemOld { get; set; }
        public short ItemNew { get; set; }
        public int DownloadVersion { get; set; }
        public sbyte DataVersion { get; set; }
        public sbyte MapVersion { get; set; }
        public sbyte SkillVersion { get; set; }
        public sbyte ItemVersion { get; set; }
        public sbyte EventVersion { get; set; }
        public bool IsDir { get; set; }
        public string NR_Dart { get; set; }
        public string NR_Arrow { get; set; }
        public string NR_Effect { get; set; }
        public string NR_Image { get; set; }
        public string NR_Part { get; set; }
        public string NR_Skill{ get; set; }
        public string NRskill{ get; set; }
        public string NRmap{ get; set; }
        public string MapResource { get; set; }
        public string BackgroundImg { get; set; }
        public string IconImg { get; set; }
        public string EffectData { get; set; }
        public string EffectImg { get; set; }
        public string Download { get; set; }
        public string TileMap { get; set; }
        public string NewBG { get; set; }
        public string Monster { get; set; }
        public string Mount { get; set; }
        public bool IsDropAll { get; set; }
        public bool IsDownloadServer { get; set; }
        public bool IsPlayServer { get; set; }
        public bool IsDevServer { get; set; }
        public bool IsVIPServer { get; set; }

        public bool SuKienTrungThu { get; set; }
        public bool SuKienWorldCup { get; set; }
        public bool SuKienHe { get; set; }


        public bool SuKienNoel { get; set; }
        public int costDoiTien { get; set; }
        public int MaxPlayers { get; set; }
        
        public int Percent_Đồ_Thần_Linh { get; set; }
        public int Percent_Đồ_Kích_Hoạt { get; set; }
        public int Percent_Ngọc_Rồng { get; set; }
        public int Percent_Sao_Pha_Lê { get; set; }
        public int Percent_Đá_Nâng_Cấp { get; set; }
        public int Percent_Thức_Ăn { get; set; }
        public int Percent_Mảnh_Thiên_Sứ { get; set; }
        public int Percent_Truyện_Doremon { get; set; }
        public int Percent_Đùi_Gà { get; set; }
        public int Percent_Ngọc_Rồng_7_Sao_Nhiệm_Vụ { get; set; }

        public static ConfigManager gI()
        {
            return _instance;
        }

        public static void CreateManager(IConfiguration configuration)
        {
            _instance = null;
            _instance = new ConfigManager(configuration);
          //  Extension.gI().Logger.Print("LOAD CONFIG SUCCESS");
        }

        public ConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
            Init();
         //   Extension.gI().Logger.Print("LOAD CONFIG SUCCESS");
        }


        public void Init()
        {
            try
            {
                Os = int.Parse(_configuration.GetSection("os").Value);
                Link = _configuration.GetSection("server").GetSection("link").Value;
                ServerHost = _configuration.GetSection("server").GetSection("host").Value;
                ServerPort = int.Parse(_configuration.GetSection("server").GetSection("port").Value);
                IsDebug = Boolean.Parse(_configuration.GetSection("server").GetSection("debug").Value);
                IsDropAll = Boolean.Parse(_configuration.GetSection("server").GetSection("drop-all-item").Value);
                IsDownloadServer = Boolean.Parse(_configuration.GetSection("server").GetSection("download-server").Value);
                IsPlayServer = Boolean.Parse(_configuration.GetSection("server").GetSection("play-server").Value);
                IsDevServer = Boolean.Parse(_configuration.GetSection("server").GetSection("dev-server").Value);
                IsVIPServer = Boolean.Parse(_configuration.GetSection("server").GetSection("vip-server").Value);
                SuKienTrungThu = Boolean.Parse(_configuration.GetSection("server").GetSection("event-trungthu").Value);
                SuKienWorldCup = Boolean.Parse(_configuration.GetSection("server").GetSection("event-worldcup").Value);
                SuKienNoel = Boolean.Parse(_configuration.GetSection("server").GetSection("event-noel").Value);
                MaxPlayers = int.Parse(_configuration.GetSection("server").GetSection("max-players").Value);
                SuKienHe = Boolean.Parse(_configuration.GetSection("server").GetSection("event-he").Value);
            }
            catch (Exception e)
            {
                Os = 1;
                Link = "Localhost:127.0.0.1:14445:0,0,0";
                ServerHost = "127.0.0.1";
                ServerPort = 14445;
                IsDebug = false;
                IsDropAll = false;
                IsDownloadServer = false;
                IsPlayServer = true;
                IsDevServer = true;
                MaxPlayers = 60;
                IsVIPServer = false;
                SuKienTrungThu = false;
                SuKienWorldCup = false;
                SuKienHe = false;
                SuKienNoel = false;
                Console.WriteLine($"Check ---------------------------------- Error {e.Message}:\n{e.StackTrace}");
            }
            try
            {
                MySqlHost = _configuration.GetSection("database").GetSection("mysql").GetSection("host").Value;
                MySqlPort = int.Parse(_configuration.GetSection("database").GetSection("mysql").GetSection("port").Value);
                MySqlUsername = _configuration.GetSection("database").GetSection("mysql").GetSection("user").Value;
                MySqlPassword = _configuration.GetSection("database").GetSection("mysql").GetSection("password").Value;
                MySqlDBData = _configuration.GetSection("database").GetSection("mysql").GetSection("database").GetSection("data").Value;
                MySqlDBAccount = _configuration.GetSection("database").GetSection("mysql").GetSection("database").GetSection("account").Value;
            }
            catch (Exception e)
            {
                MySqlHost = "127.0.0.1";
                MySqlPort = 3306;
                MySqlUsername = "root";
                MySqlPassword = "";
                MySqlDBData = "nro_data";
                MySqlDBAccount = "nro_acc";
                Console.WriteLine($"Check ---------------------------------- Error {e.Message}:\n{e.StackTrace}");
            }
            try
            {
                MaxConnectSocket = int.Parse(_configuration.GetSection("socket").GetSection("max-connect").Value);
                //Percent_Đồ_Thần_Linh = int.Parse(_configuration.GetSection("percent").GetSection("Đồ Thần Linh").Value);
                //Percent_Đồ_Kích_Hoạt = int.Parse(_configuration.GetSection("percent").GetSection("Đồ Kích Hoạt").Value);
                //Percent_Ngọc_Rồng = int.Parse(_configuration.GetSection("percent").GetSection("Ngọc Rồng").Value);
                //Percent_Sao_Pha_Lê = int.Parse(_configuration.GetSection("percent").GetSection("Sao Pha Lê").Value);
                //Percent_Đá_Nâng_Cấp = int.Parse(_configuration.GetSection("percent").GetSection("Đá Nâng Cấp").Value);
                //Percent_Thức_Ăn = int.Parse(_configuration.GetSection("percent").GetSection("Thức Ăn").Value);
                //Percent_Mảnh_Thiên_Sứ = int.Parse(_configuration.GetSection("percent").GetSection("Mảnh Thiên Sứ").Value);
                //Percent_Truyện_Doremon = int.Parse(_configuration.GetSection("percent").GetSection("Truyện Doremon").Value);
                //Percent_Đùi_Gà = int.Parse(_configuration.GetSection("percent").GetSection("Đùi Gà").Value);
                //Percent_Ngọc_Rồng_7_Sao_Nhiệm_Vụ = int.Parse(_configuration.GetSection("percent").GetSection("Ngọc rồng 7 sao nhiệm vụ").Value);
                ExpUp = int.Parse(_configuration.GetSection("exp-up").Value);
                costDoiTien = int.Parse(_configuration.GetSection("cost").Value);
                LimitPowerExpUp = long.Parse(_configuration.GetSection("limit-power-exp-up").Value);
                ItemOld= short.Parse(_configuration.GetSection("item-old").Value);
                ItemNew= short.Parse(_configuration.GetSection("item-new").Value);
                DownloadVersion = int.Parse(_configuration.GetSection("version").GetSection("download").Value);
                DataVersion = SByte.Parse(_configuration.GetSection("version").GetSection("data").Value);
                MapVersion = SByte.Parse(_configuration.GetSection("version").GetSection("map").Value);
                SkillVersion = SByte.Parse(_configuration.GetSection("version").GetSection("skill").Value);
                ItemVersion = SByte.Parse(_configuration.GetSection("version").GetSection("item").Value);
                EventVersion = SByte.Parse(_configuration.GetSection("version").GetSection("event").Value);
            }
            catch (Exception e)
            {
                MaxConnectSocket = 10;
                ExpUp = 1;
                costDoiTien= 1;
                LimitPowerExpUp = 80000000000;
                ItemOld = 954;
                ItemNew = 954;
                DownloadVersion = 5714013;
                DataVersion = 1;
                MapVersion = 7;
                SkillVersion = 6;
                ItemVersion = 1;
                EventVersion = 1;
                Console.WriteLine($"Check ---------------------------------- Error {e.Message}:\n{e.StackTrace}");
            }

            try
            {
                IsDir = Boolean.Parse(_configuration.GetSection("server").GetSection("cache").GetSection("isDir").Value);
                NR_Dart = _configuration.GetSection("server").GetSection("cache").GetSection("nr_dart").Value;
                NR_Arrow = _configuration.GetSection("server").GetSection("cache").GetSection("nr_arrow").Value;
                NR_Effect = _configuration.GetSection("server").GetSection("cache").GetSection("nr_effect").Value;
                NR_Image = _configuration.GetSection("server").GetSection("cache").GetSection("nr_image").Value;
                NR_Part = _configuration.GetSection("server").GetSection("cache").GetSection("nr_part").Value;
                NR_Skill = _configuration.GetSection("server").GetSection("cache").GetSection("nr_skill").Value;
                NRskill = _configuration.GetSection("server").GetSection("cache").GetSection("nrskill").Value;
                NRmap = _configuration.GetSection("server").GetSection("cache").GetSection("nrmap").Value;
            }
            catch (Exception e)
            {
                IsDir = false;
                NR_Dart = "C:\\";
                NR_Arrow = "C:\\";
                NR_Effect = "C:\\";
                NR_Image = "C:\\";
                NR_Part = "C:\\";
                NR_Skill = "C:\\";
                NRskill = "C:\\";
                NRmap = "C:\\";
                Console.WriteLine($"Check ---------------------------------- Error {e.Message}:\n{e.StackTrace}");
            } 
            
            MapResource = _configuration.GetSection("server").GetSection("resource").GetSection("map").Value;
            Monster = _configuration.GetSection("server").GetSection("resource").GetSection("monster").Value;
            TileMap = _configuration.GetSection("server").GetSection("resource").GetSection("tile").Value;
            NewBG = _configuration.GetSection("server").GetSection("resource").GetSection("new-bg").Value;
            Download = _configuration.GetSection("server").GetSection("resource").GetSection("download").Value;
            BackgroundImg = _configuration.GetSection("server").GetSection("resource").GetSection("background").Value;
            IconImg = _configuration.GetSection("server").GetSection("resource").GetSection("icon").Value;
            Mount = _configuration.GetSection("server").GetSection("resource").GetSection("mount").Value;
            EffectData = _configuration.GetSection("server").GetSection("resource").GetSection("effect").GetSection("data").Value;
            EffectImg = _configuration.GetSection("server").GetSection("resource").GetSection("effect").GetSection("image").Value;
        }
    }
}