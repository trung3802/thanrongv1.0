using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Constants;
using Org.BouncyCastle.Math.Field;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.DatabaseManager;
using System.Security.Cryptography;
namespace TienKiemV2Remastered.Application.Threading
{
    public class BossRunTime
{
        private static BossRunTime Instance { get; set; } = null;

        public static bool IsStop = false;
        //boss sự kiện
        #region vocuc
        private static bool IsvocucSpawn = false;
        private static List<int> vocucMaps = new List<int> { 3, 8, 18,28,31,36 };
        private static Boss vocuc = null;
        private static int vocucId = -1;
        private static bool IsvocucNotify = false;
        private static long vocucSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion
        #region sieuvocuc
        private static bool IssieuvocucSpawn = false;
        private static List<int> sieuvocucMaps = new List<int> { 3, 8, 18, 28, 31, 36 };
        private static Boss sieuvocuc = null;
        private static int sieuvocucId = -1;
        private static bool IssieuvocucNotify = false;
        private static long sieuvocucSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion
        #region supervocuc
        private static bool IssupervocucSpawn = false;
        private static List<int> supervocucMaps = new List<int> { 3, 8, 18, 28, 31, 36 };
        private static Boss supervocuc = null;
        private static int supervocucId = -1;
        private static bool IssupervocucNotify = false;
        private static long supervocucSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion
        //--------------------------------------------------------------------------------------
        //boss type a1 kimono
        #region Boss type a1 kimono   
        //110
        private static List<int> kimonoMaps = new List<int> { 155 };
        private static Boss kimono = null;
        private static int kimonoId = -1;
        private static bool IskimonoSpawn = false;
        private static bool IskimonoNotify = false;
        private static long kimonoSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a2 broly fake 111
        #region Boss type a2 broly
        private static List<int> brolyMaps = new List<int> { 92,93,94,97,98 };
        private static Boss broly = null;
        private static int brolyId = -1;
        private static bool IsbrolySpawn = false;
        private static bool IsbrolyNotify = false;
        private static long brolySpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a3 chaien   112
        #region Boss type a3 chaien
        private static List<int> chaienMaps = new List<int> { 92, 93, 94 };
        private static Boss chaien = null;
        private static int chaienId = -1;
        private static bool IschaienSpawn = false;
        private static bool IschaienNotify = false;
        private static long chaienSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a4 pocolo  113
        #region Boss type a4 pocolo
        private static List<int> pocoloMaps = new List<int> { 160 };
        private static Boss pocolo = null;
        private static int pocoloId = -1;
        private static bool IspocoloSpawn = false;
        private static bool IspocoloNotify = false;
        private static long pocoloSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a5 gohan  
        #region Boss type a5 gohan
        private static List<int> gohanMaps = new List<int> { 5 };
        private static Boss gohan = null;
        private static int gohanId = -1;
        private static bool IsgohanSpawn = false;
        private static bool IsgohanNotify = false;
        private static long gohanSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a6 ironman
        #region Boss type a6 ironman
        private static List<int> ironmanMaps = new List<int> { 6 };
        private static Boss ironman = null;
        private static int ironmanId = -1;
        private static bool IsironmanSpawn = false;
        private static bool IsironmanNotify = false;
        private static long ironmanSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a7 siêu thần trái đất
        #region Boss type a7 sieuthantraidat
        private static List<int> sieuthantraidatMaps = new List<int> { 7 };
        private static Boss sieuthantraidat = null;
        private static int sieuthantraidatId = -1;
        private static bool IssieuthantraidatSpawn = false;
        private static bool IssieuthantraidatNotify = false;
        private static long sieuthantraidatSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a8 ninja
        #region Boss type a8 ninja
        private static List<int> ninjaMaps = new List<int> { 8 };
        private static Boss ninja = null;
        private static int ninjaId = -1;
        private static bool IsninjaSpawn = false;
        private static bool IsninjaNotify = false;
        private static long ninjaSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a9 khỉ vô cực
        #region Boss type a9 khivocuc
        private static List<int> khivocucMaps = new List<int> { 9 };
        private static Boss khivocuc = null;
        private static int khivocucId = -1;
        private static bool IskhivocucSpawn = false;
        private static bool IskhivocucNotify = false;
        private static long khivocucSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion

        //boss type a10 cadic
        #region Boss type a10 cadic
        private static List<int> cadicMaps = new List<int> { 10 };
        private static Boss cadic = null;
        private static int cadicId = -1;
        private static bool IscadicSpawn = false;
        private static bool IscadicNotify = false;
        private static long cadicSpawnTimeDelay = 5000 + ServerUtils.CurrentTimeMillis();
        #endregion
        public static BossRunTime Gi()
        {
            return Instance ??= new BossRunTime();
        }

        public BossRunTime()
        {

        }
        public void BossDie(int bossId)
        {
            try
            {
                // boss sk
                if (bossId == vocucId && IsvocucSpawn)
                {
                    vocuc = null;
                    IsvocucSpawn = false;
                    vocucId = -1;
                    vocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                }
                else if (bossId == sieuvocucId && IssieuvocucSpawn)
                {
                    sieuvocuc = null;
                    IssieuvocucSpawn = false;
                    sieuvocucId = -1;
                    sieuvocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                }
                else if (bossId == supervocucId && IssupervocucSpawn)
                {
                    supervocuc = null;
                    IssupervocucSpawn = false;
                    supervocucId = -1;
                    supervocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a1 Kimono
                else if (bossId == kimonoId && IskimonoSpawn)
                {
                    kimono = null;
                    IskimonoSpawn = false;
                    kimonoId = -1;
                    kimonoSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a2 Broly
                else if (bossId == brolyId && IsbrolySpawn)
                {
                    broly = null;
                    IsbrolySpawn = false;
                    brolyId = -1;
                    brolySpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a3 Chaien
                else if (bossId == chaienId && IschaienSpawn)
                {
                    chaien = null;
                    IschaienSpawn = false;
                    chaienId = -1;
                    chaienSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a4 pocolo
                else if (bossId == pocoloId && IspocoloSpawn)
                {
                    pocolo = null;
                    IspocoloSpawn = false;
                    pocoloId = -1;
                    pocoloSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a5 gohan
                else if (bossId == gohanId && IsgohanSpawn)
                {
                    gohan = null;
                    IsgohanSpawn = false;
                    gohanId = -1;
                    gohanSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a6 ironman
                else if (bossId == ironmanId && IsironmanSpawn)
                {
                    ironman = null;
                    IsironmanSpawn = false;
                    ironmanId = -1;
                    ironmanSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a7 sieuthantraidat
                else if (bossId == sieuthantraidatId && IssieuthantraidatSpawn)
                {
                    sieuthantraidat = null;
                    IssieuthantraidatSpawn = false;
                    sieuthantraidatId = -1;
                    sieuthantraidatSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a8 ninja
                else if (bossId == ninjaId && IsninjaSpawn)
                {
                    ninja = null;
                    IsninjaSpawn = false;
                    ninjaId = -1;
                    ninjaSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a9 khivocuc
                else if (bossId == khivocucId && IskhivocucSpawn)
                {
                    khivocuc = null;
                    IskhivocucSpawn = false;
                    khivocucId = -1;
                    khivocucSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }

                //Boss a10 cadic
                else if (bossId == cadicId && IscadicSpawn)
                {
                    cadic = null;
                    IscadicSpawn = false;
                    cadicId = -1;
                    cadicSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error BossDie in BossRunTime.cs: {e.Message} \n {e.StackTrace}", e);
            }

        }
        public void StartBossRunTime()
        {
            new Thread(new ThreadStart(() =>
            {
                while (Server.Gi().IsRunning)
                {
                    var now = ServerUtils.TimeNow();
                    try
                    {
                        #region vocuc
                        if ((vocucSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            vocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                            if (!IsvocucSpawn)
                            {
                                IsvocucSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(1, 19);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(vocucMaps.Count);
                                int sbRandomMap = vocucMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    vocuc = new Boss();
                                    vocuc.CreateBoss(DataCache.BOSS_vocuc_TYPE);
                                    vocuc.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(vocuc);
                                    vocucId = vocuc.Id;
                                    IsvocucSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Vô Cực " + vocucId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Vô Cực...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IsvocucNotify = true;
                                }
                            }
                            else if (!IsvocucNotify && vocuc != null && vocucId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Vô Cực " + vocucId + " vừa xuất hiện tại " + vocuc.Zone.Map.TileMap.Name));
                                IsvocucNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IsvocucNotify = false;
                        }


                        #endregion
                        #region sieuvocuc
                        if ((sieuvocucSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            sieuvocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                            if (!IssieuvocucSpawn)
                            {
                                IssieuvocucSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(1, 19);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(sieuvocucMaps.Count);
                                int sbRandomMap = sieuvocucMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    sieuvocuc = new Boss();
                                    sieuvocuc.CreateBoss(DataCache.BOSS_sieuvocuc_TYPE);
                                    sieuvocuc.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(sieuvocuc);
                                    sieuvocucId = sieuvocuc.Id;
                                    IssieuvocucSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu Vô Cực " + sieuvocucId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Siêu Vô Cực...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IssieuvocucNotify = true;
                                }
                            }
                            else if (!IssieuvocucNotify && sieuvocuc != null && sieuvocucId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu Vô Cực " + sieuvocucId + " vừa xuất hiện tại " + sieuvocuc.Zone.Map.TileMap.Name));
                                IssieuvocucNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IssieuvocucNotify = false;
                        }


                        #endregion
                        #region supervocuc
                        if ((supervocucSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            supervocucSpawnTimeDelay = 750000 + ServerUtils.CurrentTimeMillis();
                            if (!IssupervocucSpawn)
                            {
                                IssupervocucSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(1, 19);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(supervocucMaps.Count);
                                int sbRandomMap = supervocucMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    supervocuc = new Boss();
                                    supervocuc.CreateBoss(DataCache.BOSS_supervocuc_TYPE);
                                    supervocuc.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(supervocuc);
                                    supervocucId = supervocuc.Id;
                                    IssupervocucSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Vô Cực " + supervocucId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Super Vô Cực...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IssupervocucNotify = true;
                                }
                            }
                            else if (!IssupervocucNotify && supervocuc != null && supervocucId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Vô Cực " + supervocucId + " vừa xuất hiện tại " + supervocuc.Zone.Map.TileMap.Name));
                                IssupervocucNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IssupervocucNotify = false;
                        }


                        #endregion

                        #region Boss a1 Kimono

                        if ((kimonoSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            kimonoSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IskimonoSpawn)
                            {
                                IskimonoSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(kimonoMaps.Count);
                                int sbRandomMap = kimonoMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    kimono = new Boss();
                                    kimono.CreateBoss(DataCache.BOSS_kimono_TYPE);
                                    kimono.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(kimono);
                                    kimonoId = kimono.Id;
                                    IskimonoSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kimono " + kimonoId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Kimono...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IskimonoNotify = true;
                                }
                            }
                            else if (!IskimonoNotify && kimono != null && kimonoId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kimono " + kimonoId + " vừa xuất hiện tại " + kimono.Zone.Map.TileMap.Name));
                                IskimonoNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IskimonoNotify = false;
                        }
                        #endregion

                        #region Boss a2 Broly

                        if ((brolySpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            brolySpawnTimeDelay = 600000 + ServerUtils.CurrentTimeMillis();
                            if (!IsbrolySpawn)
                            {
                                IsbrolySpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(brolyMaps.Count);
                                int sbRandomMap = brolyMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    broly = new Boss();
                                    broly.CreateBoss(DataCache.BOSS_broly_TYPE);
                                    broly.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(broly);
                                    brolyId = broly.Id;
                                    IsbrolySpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Broly Fake " + brolyId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Broly Fake...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IsbrolyNotify = true;
                                }
                            }
                            else if (!IsbrolyNotify && broly != null && brolyId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Broly Fake " + brolyId + " vừa xuất hiện tại " + broly.Zone.Map.TileMap.Name));
                                IsbrolyNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IsbrolyNotify = false;
                        }
                        #endregion

                        #region Boss a3 Chaien

                        if ((chaienSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            chaienSpawnTimeDelay = 500000 + ServerUtils.CurrentTimeMillis();
                            if (!IschaienSpawn)
                            {
                                IschaienSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(chaienMaps.Count);
                                int sbRandomMap = chaienMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    chaien = new Boss();
                                    chaien.CreateBoss(DataCache.BOSS_chaien_TYPE);
                                    chaien.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(chaien);
                                    chaienId = chaien.Id;
                                    IschaienSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Chaien " + chaienId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Chaien...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IschaienNotify = true;
                                }
                            }
                            else if (!IschaienNotify && chaien != null && chaienId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Chaien " + chaienId + " vừa xuất hiện tại " + chaien.Zone.Map.TileMap.Name));
                                IschaienNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IschaienNotify = false;
                        }
                        #endregion

                        #region Boss a4 Pocolo

                        if ((pocoloSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            pocoloSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IspocoloSpawn)
                            {
                                IspocoloSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(pocoloMaps.Count);
                                int sbRandomMap = pocoloMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    pocolo = new Boss();
                                    pocolo.CreateBoss(DataCache.BOSS_pocolo_TYPE);
                                    pocolo.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(pocolo);
                                    pocoloId = chaien.Id;
                                    IspocoloSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Pocolo " + pocoloId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Pocolo...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IspocoloNotify = true;
                                }
                            }
                            else if (!IspocoloNotify && pocolo != null && pocoloId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Pocolo " + pocoloId + " vừa xuất hiện tại " + pocolo.Zone.Map.TileMap.Name));
                                IspocoloNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IspocoloNotify = false;
                        }
                        #endregion

                        #region Boss a5 Gohan

                        if ((gohanSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            gohanSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IsgohanSpawn)
                            {
                                IsgohanSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(gohanMaps.Count);
                                int sbRandomMap = gohanMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    gohan = new Boss();
                                    gohan.CreateBoss(DataCache.BOSS_gohan_TYPE);
                                    gohan.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(gohan);
                                    gohanId = gohan.Id;
                                    IsgohanSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Gohan " + gohanId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Gohan...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IsgohanNotify = true;
                                }
                            }
                            else if (!IsgohanNotify && gohan != null && gohanId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Gohan " + gohanId + " vừa xuất hiện tại " + gohan.Zone.Map.TileMap.Name));
                                IsgohanNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IsgohanNotify = false;
                        }
                        #endregion

                        #region Boss a6 Iron man

                        if ((ironmanSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            ironmanSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IsironmanSpawn)
                            {
                                IsironmanSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(ironmanMaps.Count);
                                int sbRandomMap = ironmanMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    ironman = new Boss();
                                    ironman.CreateBoss(DataCache.BOSS_ironman_TYPE);
                                    ironman.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(ironman);
                                    ironmanId = ironman.Id;
                                    IsironmanSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Iron man " + ironmanId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Iron man...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IsironmanNotify = true;
                                }
                            }
                            else if (!IsironmanNotify && ironman != null && ironmanId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Iron man " + ironmanId + " vừa xuất hiện tại " + ironman.Zone.Map.TileMap.Name));
                                IsironmanNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IsironmanNotify = false;
                        }
                        #endregion

                        #region Boss a7 Siêu thần trái đất

                        if ((sieuthantraidatSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            sieuthantraidatSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IssieuthantraidatSpawn)
                            {
                                IssieuthantraidatSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(sieuthantraidatMaps.Count);
                                int sbRandomMap = sieuthantraidatMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    sieuthantraidat = new Boss();
                                    sieuthantraidat.CreateBoss(DataCache.BOSS_sieuthantraidat_TYPE);
                                    sieuthantraidat.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(sieuthantraidat);
                                    sieuthantraidatId = sieuthantraidat.Id;
                                    IssieuthantraidatSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu thần trái đất " + sieuthantraidatId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Siêu thần trái đất...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IssieuthantraidatNotify = true;
                                }
                            }
                            else if (!IssieuthantraidatNotify && sieuthantraidat != null && sieuthantraidatId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu thần trái đất " + sieuthantraidatId + " vừa xuất hiện tại " + sieuthantraidat.Zone.Map.TileMap.Name));
                                IssieuthantraidatNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IssieuthantraidatNotify = false;
                        }
                        #endregion

                        #region Boss a8 Ninja

                        if ((ninjaSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            ninjaSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IsninjaSpawn)
                            {
                                IsninjaSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(ninjaMaps.Count);
                                int sbRandomMap = ninjaMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    ninja = new Boss();
                                    ninja.CreateBoss(DataCache.BOSS_ninja_TYPE);
                                    ninja.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(ninja);
                                    ninjaId = ninja.Id;
                                    IsninjaSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Ninja " + ninjaId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Ninja...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IsninjaNotify = true;
                                }
                            }
                            else if (!IsninjaNotify && ninja != null && ninjaId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Ninja " + ninjaId + " vừa xuất hiện tại " + ninja.Zone.Map.TileMap.Name));
                                IsninjaNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IsninjaNotify = false;
                        }
                        #endregion

                        #region Boss a9 Khỉ vô cực

                        if ((khivocucSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            khivocucSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IskhivocucSpawn)
                            {
                                IskhivocucSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(khivocucMaps.Count);
                                int sbRandomMap = khivocucMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    khivocuc = new Boss();
                                    khivocuc.CreateBoss(DataCache.BOSS_khivocuc_TYPE);
                                    khivocuc.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(khivocuc);
                                    khivocucId = khivocuc.Id;
                                    IskhivocucSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Khỉ vô cực " + brolyId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Khỉ vô cực...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IskhivocucNotify = true;
                                }
                            }
                            else if (!IskhivocucNotify && khivocuc != null && khivocucId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Khỉ vô cực " + khivocucId + " vừa xuất hiện tại " + khivocuc.Zone.Map.TileMap.Name));
                                IskhivocucNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IskhivocucNotify = false;
                        }
                        #endregion

                        #region Boss a10 Cadic

                        if ((cadicSpawnTimeDelay < ServerUtils.CurrentTimeMillis()))
                        {
                            cadicSpawnTimeDelay = 300000 + ServerUtils.CurrentTimeMillis();
                            if (!IscadicSpawn)
                            {
                                IscadicSpawn = true;
                                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                                int sbRandomMapIndex = ServerUtils.RandomNumber(cadicMaps.Count);
                                int sbRandomMap = cadicMaps[sbRandomMapIndex];
                                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                                if (zone != null)
                                {
                                    cadic = new Boss();
                                    cadic.CreateBoss(DataCache.BOSS_cadic_TYPE);
                                    cadic.CharacterHandler.SetUpInfo();
                                    zone.ZoneHandler.AddBoss(cadic);
                                    cadicId = cadic.Id;
                                    IscadicSpawn = true;
                                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Cadic " + cadicId + " vừa xuất hiện tại " + zone.Map.TileMap.Name));
                                    Server.Gi().Logger.Print("Boss Spawn Cadic...: Map " + sbRandomMap + " Zone: " + sbRandomZoneNum + " Name: " + zone.Map.TileMap.Name + " DataCache.CURRENT_BOSS_ID: " + DataCache.CURRENT_BOSS_ID);
                                    IscadicNotify = true;
                                }
                            }
                            else if (!IscadicNotify && cadic != null && cadicId != -1)
                            {
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Cadic " + cadicId + " vừa xuất hiện tại " + cadic.Zone.Map.TileMap.Name));
                                IscadicNotify = true;
                            }
                            // Get Random Map
                        }
                        else
                        {
                            IscadicNotify = false;
                        }
                        #endregion

                    }
                    catch (Exception e)
                    {
                        Server.Gi().Logger.Error($"Error StartBossRunTime in BossRunTime.cs: {e.Message} \n {e.StackTrace}", e);
                    }
                    Thread.Sleep(1000);
                }
                Server.Gi().Logger.Print("Boss Runtime is close Success...");
                IsStop = true;
            })).Start();
        }
    }
}
