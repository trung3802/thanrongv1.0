using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Constants;
using Org.BouncyCastle.Math.Field;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model.Map;
using K4os.Hash.xxHash;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography;
using System.Xml.Linq;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu2Gio;
using TienKiemV2Remastered.Application.Extension.Super_Champion;
/// Code by baodubai dep trai vai lon
namespace TienKiemV2Remastered.Application.Threading
{
   
    public class ABoss
    {
        private static ABoss Instance { get; set; } = null;
        public bool IsStop = false;
        public static ABoss gI()
        {
            return Instance ??= new ABoss();
        }
        public int CountColer = 1;
        public Boss Mabu2 = null;
        public int IdMabu = -1;
        public bool isMabuSpawn = false;
        public string TileMapMabu = "";
        public int OldMapMabu = -1;
        public int OldZoneMabu = -1;
        public long DelayMabu = 16000 + ServerUtils.CurrentTimeMillis();
        #region XY
        public int XKuKu = 0;
        public int YKuku = 0;
        public int XRambo = 0;
        public int YRambo = 0;
        public int XMapDauDinh = 0;
        public int YMapDauDinh = 0;
        public Zone zoneKuku;
        #endregion
        #region CountBoss
        public int countOngGiaNoel = 0;
        public int countSo4 = 0;
        public int countSo3 = 0;
        public int countSo1 = 0;
        public int countTieuDoiTruong = 0;
        public int countKuku = 0;
        public int countMapDauDinh = 0;
        public int countRambo = 0;
        public int countXen1 = 0;
        public int countXen2 = 0;
        public int countXenHoanThien = 0;
        public int countAnd19 = 0;
        public int countAnd20 = 0;
        public int countPic = 0;
        public int countPoc = 0;
        public int countKingKong = 0;
        public int countAnd13 = 0;
        public int countAnd14 = 0;
        public int countAnd15 = 0;
        public int countChilled = 0;
        public int countChilled2 = 0;
        public int countBlackGoku = 0;
        public int countSuperBlackGoku = 0;
        public int countBroly = 0;
        public int countSuperBroly = 0;
        public int countNoONo = 0;
        #endregion

        #region IdBoss
        public int idBroly = -1;
        public int idOngGiaNoel = -1;
        public int idSo4 = -1;
        public int idSo2 = -1;
        public int idSo3 = -1;
        public int idSo1 = -1;
        public int idTieuDoiTruong = -1;
        public int idKuku = -1;
        public int idMapDauDinh = -1;
        public int idRambo = -1;
        public int idXen1 = -1;
        public int idXen2 = -1;
        public int idXenHoanThien = -1;
        public int idXenHoanThien2 = -1;
        public int idAnd19 = -1;
        public int idAnd20 = -1;
        public int idPic = -1;
        public int idPoc = -1;
        public int idKingKong = -1;
        public int idAnd13 = -1;
        public int idAnd14 = -1;
        public int idAnd15 = -1;
        public int idChilled = -1;
        public int idChilled2 = -1;
        public int idBlackGoku = -1;
        public int idSuperBlackGoku = -1;
        public int idFide1 = -1;
        public int idFide2 = -1;
        public int idFide3 = -1;
        public int idColer1 = -1;
        public int idColer2 = -1;
        #endregion

        #region Spawn
        public bool So4Spawn = false;
        public bool So3Spawn = false;

        public bool So2Spawn = false;
        public bool So1Spawn = false;
        public bool TieuDoiTruongSpawn = false;
        public bool KukuSpawn = false;
        public bool MapDauDinhSpawn = false;
        public bool RamboSpawn = false;
        public bool Xen1Spawn = false;
        public bool Xen2Spawn = false;
        public bool XenHoanThienSpawn = false;
        public bool XenHoanThienSpawn2 = false;
        public bool And19Spawn = false;
        public bool And20Spawn = false;
        public bool PicSpawn = false;
        public bool PocSpawn = false;
        public bool KingkongSpawn = false;
        public bool And13Spawn = false;
        public bool And14Spawn = false;
        public bool And15Spawn = false;
        public bool ChilledSpawn = false;
        public bool Chilled2Spawn = false;
        public bool BlackGokuSpawn = false;
        public bool SuperBlackGokuSpawn = false;
        public bool Fide1Spawn = false;
        public bool Fide2Spawn = false;
        public bool Fide3Spawn = false;
        public bool Coler1Spawn = false;
        public bool Coler2Spawn = false;
        #endregion

        #region mapSpawm
        public List<int> CoolerMaps = new List<int> { 107, 108, 110 };
        public List<int> NappaMaps = new List<int> { 68, 69, 70, 71, 72 };
        public List<int> BlackGokuMaps = new List<int> { 92, 93, 94 };
        public List<int> TDSTMaps = new List<int> { 82, 83, 79 };
        public List<int> FideMaps = new List<int> { 79, 80 };
        public List<int> ChilledMaps = new List<int> { 161 };
        public List<int> BaMapDauTuongLai = new List<int> { 92, 93, 94 };
        public List<int> PicPocKKMaps = new List<int> { 97, 98, 99 };
        public List<int> CellMaps = new List<int> { 100 };
        public List<int> PerfectCellMaps = new List<int> { 103 };
        public List<int> SanSauSieuThi = new List<int> { 104 };
        public List<int> mapTraiDat = new List<int> { 3, 4, 27, 28, 29, 30, 6, 10 };
        public List<List<int>> PosistionBroly = new List<List<int>> { new List<int> { 777, 408 }, new List<int> { 701, 360 }, new List<int> { 836, 336 }, new List<int> { 669, 312 }, new List<int> { 488, 288 }, new List<int> { 484, 336 }, new List<int> { 326, 288 }, new List<int> { 570, 360 } };
        #endregion

        #region Notify
        public bool So4Notify = false;
        public bool So3Notify = false;
        public bool So1Notify = false;
        public bool TieuDoiTruongNotify = false;
        public bool KukuNotify = false;
        public bool MapDauDinhNotify = false;
        public bool RamboNotify = false;
        public bool Xen1Notify = false;
        public bool Xen2Notify = false;
        public bool XenHoanThienNotify = false;
        public bool And19Notify = false;
        public bool And20Notify = false;
        public bool PicNotify = false;
        public bool PocNotify = false;
        public bool KingkongNotify = false;
        public bool And13Notify = false;
        public bool And14Notify = false;
        public bool And15Notify = false;
        public bool ChilledNotify = false;
        public bool Chilled2Notify = false;
        public bool BlackGokuNotify = false;
        public bool SuperBlackGokuNotify = false;
        public bool Fide1Notify = false;
        public bool Fide2Notify = false;
        public bool Fide3Notify = false;
        #endregion

        #region oldMap
        public int oldMapOngGiaNoel = -1;
        public int oldMapSo4 = -1;
        public int oldMapSo3 = -1;
        public int oldMapSo2 = -1;

        public int oldMapSo1 = -1;
        public int oldMapTieuDoiTruong = -1;
        public int oldMapKuku = -1;
        public int oldMapMapDauDinh = -1;
        public int oldMapRambo = -1;
        public int oldMapXen1 = -1;
        public int oldMapXen2 = -1;
        public int oldMapXenHoanThien = -1;
        public int oldMapXenHoanThien2 = -1;
        public int oldMapAnd19 = -1;
        public int oldMapAnd20 = -1;
        public int oldMapPic = -1;
        public int oldMapPoc = -1;
        public int oldMapKingKong = -1;
        public int oldMapAnd13 = -1;
        public int oldMapAnd14 = -1;
        public int oldMapAnd15 = -1;
        public int oldMapChilled = -1;
        public int oldMapChilled2 = -1;
        public int oldMapBlackGoku = -1;
        public int oldMapSuperBlackGoku = -1;
        public int oldMapFide1 = -1;
        public int oldMapFide2 = -1;
        public int oldMapFide3 = -1;
        public int oldMapBroly = -1;
        public int oldMapColer1 = -1;
        public int oldMapColer2 = -1;

        #endregion

        #region oldZone
        public int oldZoneOngGiaNoel = -1;
        public int oldZoneSo4 = -1;
        public int oldZoneSo2 = -1;
        public int oldZoneSo3 = -1;
        public int oldZoneSo1 = -1;
        public int oldZoneTieuDoiTruong = -1;
        public int oldZoneKuku = -1;
        public int oldZoneMapDauDinh = -1;
        public int oldZoneRambo = -1;
        public int oldZoneXen1 = -1;
        public int oldZoneXen2 = -1;
        public int oldZoneXenHoanThien = -1;
        public int oldZoneXenHoanThien2 = -1;
        public int oldZoneAnd19 = -1;
        public int oldZoneAnd20 = -1;
        public int oldZonePic = -1;
        public int oldZonePoc = -1;
        public int oldZoneKingKong = -1;
        public int oldZoneAnd13 = -1;
        public int oldZoneAnd14 = -1;
        public int oldZoneAnd15 = -1;
        public int oldZoneChilled = -1;
        public int oldZoneChilled2 = -1;
        public int oldZoneBlackGoku = -1;
        public int oldZoneSuperBlackGoku = -1;
        public int oldZoneFide1 = -1;
        public int oldZoneFide2 = -1;
        public int oldZoneFide3 = -1;
        public int oldZoneBroly = -1;
        #endregion

        #region Boss
        public Boss NoONo = null;
        public Boss Broly = null;
        public List<Boss> ListBroly = new List<Boss>();

        public Boss OngGiaNoel = null;
        public Boss So4 = null;
        public Boss So3 = null;
        public Boss So2 = null;
        public Boss So1 = null;
        public Boss TieuDoiTruong = null;
        public Boss Kuku = null;
        public Boss MapDauDinh = null;
        public Boss Rambo = null;
        public Boss Xen1 = null;
        public Boss Xen2 = null;
        public Boss XenHoanThien = null;
        public Boss XenHoanThien2 = null;
        public Boss And19 = null;
        public Boss And20 = null;
        public Boss Pic = null;
        public Boss Poc = null;
        public Boss KingKong = null;
        public Boss And13 = null;
        public Boss And14 = null;
        public Boss And15 = null;
        public Boss Chilled = null;
        public Boss Chilled2 = null;
        public Boss BlackGoku = null;
        public Boss SuperBlackGoku = null;
        public Boss Fide1 = null;
        public Boss Fide2 = null;
        public Boss Fide3 = null;
        public Boss SuperBroly = null;
        public Boss Mabu = null;
        public Boss Kidbu = null;
        public Boss BuTenk = null;
        public Boss GohanBu = null;

        #endregion

        #region Delay
        public long DelayOngGiaNoel = 0 + ServerUtils.CurrentTimeMillis();
        public long DelaySo4 = 300000 + ServerUtils.CurrentTimeMillis();
        public long DelaySo2 = 300000 + ServerUtils.CurrentTimeMillis();
        public long DelaySo3 = 300000 + ServerUtils.CurrentTimeMillis();
        public long DelaySo1 = 300000 + ServerUtils.CurrentTimeMillis();
        public long DelayTieuDoiTruong = 300000 + ServerUtils.CurrentTimeMillis();
        public long DelayKuku = 100000 + ServerUtils.CurrentTimeMillis();
        public long DelayMapDauDinh = 100000 + ServerUtils.CurrentTimeMillis();
        public long DelayRambo = 100000 + ServerUtils.CurrentTimeMillis();
        public long DelayXen1 = 20000 + ServerUtils.CurrentTimeMillis();
        public long DelayXen2 = 400000 + ServerUtils.CurrentTimeMillis();
        public long DelayXenHoanThien = 400000 + ServerUtils.CurrentTimeMillis();
        public long DelayXenHoanThien2 = 21000 + ServerUtils.CurrentTimeMillis();
        public long DelayAnd19 = 120000 + ServerUtils.CurrentTimeMillis();
        public long DelayAnd20 = 120000 + ServerUtils.CurrentTimeMillis();
        public long DelayPic = 21000 + ServerUtils.CurrentTimeMillis();
        public long DelayPoc = 21000 + ServerUtils.CurrentTimeMillis();
        public long DelayKingKong = 21000 + ServerUtils.CurrentTimeMillis();
        public long DelayAnd13 = 0 + ServerUtils.CurrentTimeMillis();
        public long DelayAnd14 = 0 + ServerUtils.CurrentTimeMillis();
        public long DelayAnd15 = 0 + ServerUtils.CurrentTimeMillis();
        public long DelayChilled = 15000 + ServerUtils.CurrentTimeMillis();
        public long DelayChilled2 = 15000 + ServerUtils.CurrentTimeMillis();
        public long DelayBlackGoku = 10000 + ServerUtils.CurrentTimeMillis();
        public long DelaySuperBlackGoku = 150000 + ServerUtils.CurrentTimeMillis();
        public long DelayBroly = 11000 + ServerUtils.CurrentTimeMillis();
        public long DelayFide1 = 60000 + ServerUtils.CurrentTimeMillis();
        public long DelayFide2 = 63000 + ServerUtils.CurrentTimeMillis();
        public long DelayFide3 = 66000 + ServerUtils.CurrentTimeMillis();
        public long DelaySuperBroly = 10000 + ServerUtils.CurrentTimeMillis();
        public long DelayNoONo = 32000 + ServerUtils.CurrentTimeMillis();
        #endregion

        #region oldTileMap
        public string TileMapOngGiaNoel = "";
        public string TileMapSo4 = "";
        public string TileMapSo3 = "";
        public string TileMapSo2 = "";

        public string TileMapSo1 = "";
        public string TileMapTieuDoiTruong = "";
        public string TileMapKuku = "";
        public string TileMapMapDauDinh = "";
        public string TileMapRambo = "";
        public string TileMapXen1 = "";
        public string TileMapXen2 = "";
        public string TileMapXenHoanThien = "";
        public string TileMapAnd19 = "";
        public string TileMapAnd20 = "";
        public string TileMapPic = "";
        public string TileMapPoc = "";
        public string TileMapKingKong = "";
        public string TileMapAnd13 = "";
        public string TileMapAnd14 = "";
        public string TileMapAnd15 = "";
        public string TileMapChilled = "";
        public string TileMapChilled2 = "";
        public string TileMapBlackGoku = "";
        public string TileMapSuperBlackGoku = "";
        public string TileMapFide1 = "";
        public string TileMapFide2 = "";
        public string TileMapFide3 = "";
        public string TileMapBroly = "";
        public string TileMapXenHoanThien2 = "";
        #endregion

     

        public void SendBossServerChat(string thongbao)
        {
            ClientManager.Gi().SendMessageCharacter(Service.ServerChat($"BOSS {thongbao}"));
        }
        public void Output(string output)
        {
            Server.Gi().Logger.Print(output, "green");
        }
        public Zone PickMapSpawn(int mapId, int zoneId = 0)
        {
            if (zoneId == 0) zoneId = ServerUtils.RandomNumber(20);
            return MapManager.Get(mapId).Zones[zoneId];
        }
        public Zone PickMapSpawnNotHasBosses(int mapId)
        {
            return MapManager.Get(mapId).GetZoneNotBoss();
        }
        public Boss SetUpBoss(Boss boss, int type, short x = 0, short y = 0)
        {
            boss = new Boss();
            if (x != 0 || y != 0)
            {
                boss.CreateBoss(type, x, y);
            }
            else
            {
                boss.CreateBoss(type);
            }
            boss.CharacterHandler.SetUpInfo();
            return boss;
        }
        public Boss SetUpBossNoAttack(Boss boss, int type, short x = 0, short y = 0)
        {
            boss = new Boss();
            if (x != 0 || y != 0)
            {
                boss.CreateBoss(type, x, y);
            }
            else
            {
                boss.CreateBossNoAttack(type);
            }
            boss.CharacterHandler.SetUpInfo();
            return boss;
        }
        public readonly long _15MINUTES = 90000;
        public readonly long _5MINUTES = 300000;

        public readonly long _15SECONDS = 15000 ;
        public readonly long _45SECONDS = 45000 ;

        public List<Boss> ListBossTDST = new List<Boss>() { null, null, null, null, null};
        public List<int> IdBossesTDST = new List<int>() { 16, 17,  93, 18,19 };
        public bool SpawnTDST = false;
        public long DelayTDST = 12000 + ServerUtils.CurrentTimeMillis();
        public int oldMapTDST = -1;
        public int oldZoneTDST = -1;
        public string TileMapTDST = "";

        public List<Boss> ListBossSatThu3 = new List<Boss> { null, null, null };
        public List<int> IDBossesSatThu3 = new List<int> { 28, 27, 29};
        public bool SpawnSatThu3 = false;
        public long DelaySatThu3 = 15000 + ServerUtils.CurrentTimeMillis();
        public int oldMapSatThu3 = -1;
        public int oldZoneSatThu3 = -1;
        public string TileMapSatThu3 = "";

        public List<Boss> ListBossSatThu2 = new List<Boss> { null, null, null };
        public List<int> IDBossesSatThu2 = new List<int> { 34,33,32};
        public bool SpawnSatThu2 = false;
        public long DelaySatThu2 = 14000 + ServerUtils.CurrentTimeMillis();
        public int oldMapSatThu2 = -1;
        public int oldZoneSatThu2 = -1;
        public string TileMapSatThu2 = "";

        public List<Boss> ListBossSatThu1 = new List<Boss> { null, null };
        public List<int> IDBossesSatThu1 = new List<int> { 30, 31};
        public bool SpawnSatThu1 = false;
        public long DelaySatThu1 = 13000 + ServerUtils.CurrentTimeMillis();
        public int oldMapSatThu1 = -1;
        public int oldZoneSatThu1 = -1;
        public string TileMapSatThu1 = "";

        public List<Boss> ListBossXenBoHung = new List<Boss> { null, null,null };
        public List<int> IDBossesXenBoHung = new List<int> { 7,8,9 };
        public bool SpawnXenBoHung = false;
        public long DelayXenBoHung = 14000 + ServerUtils.CurrentTimeMillis();
        public int oldMapXenBoHung = -1;
        public int oldZoneXenBoHung = -1;
        public string TileMapXenBoHung = "";

        public Boss Basil = new Boss();
        public Boss Lavender = new Boss();
        public Boss Bergamo = new Boss();
        public bool SpawnBasil = false;
        public long DelayBasil = 15000 + ServerUtils.CurrentTimeMillis();
        public int oldMapBasil = -1;
        public int oldZoneBasil = -1;
        public string TileMapBasil = "";

        public bool SpawnLavender = false;
        public long DelayLavender = 16000 + ServerUtils.CurrentTimeMillis();
        public int oldMapLavender = -1;
        public int oldZoneLavender = -1;
        public string TileMapLavender = "";

        public bool SpawnBergamo = false;
        public long DelayBergamo = 17000 + ServerUtils.CurrentTimeMillis();
        public int oldMapBergamo = -1;
        public int oldZoneBergamo = -1;
        public string TileMapBergamo = "";

        public long DelayColer1 = 18000 + ServerUtils.CurrentTimeMillis();
        public long DelayColer2 = 19000 + ServerUtils.CurrentTimeMillis();
        public Boss Coler1 = null;
        public Boss Coler2 = null;
        public int oldZoneColer1 = -1;
        public int oldZoneColer2 = -1;
        public string TileMapColer1 = "";
        public string TileMapColer2 = "";

        public Boss Cumber = null;
        public Boss SuperCumber = null;
        public bool CumberSpawn = false;
        public long DelayCumber = 20000 + ServerUtils.CurrentTimeMillis();
        public int oldZoneCumber = -1;
        public int oldMapCumber = -1;
        public string TileMapCumber = "";
        public void AutoBoss(long timeserver)
        {
            if (DelayBasil < timeserver)
            {
                DelayBasil = _5MINUTES + timeserver;
                if (!SpawnBasil)
                {
                    SpawnBasil = true;
                    Basil = SetUpBoss(Basil, 100);
                    var zone = PickMapSpawn(BaMapDauTuongLai[ServerUtils.RandomNumber(BaMapDauTuongLai.Count)]);
                    zone.ZoneHandler.AddBoss(Basil);
                    oldMapBasil = zone.Map.Id;
                    oldZoneBasil = zone.Id;
                    TileMapBasil = zone.Map.TileMap.Name;
                    SendBossServerChat("Basil vừa xuất hiện tại " + zone.Map.TileMap.Name);
                    Output($"Basil Wolf Spawn: [Mapid: {oldMapBasil}, Zone: {oldZoneBasil}]");

                }
                else
                {
                    SendBossServerChat("Basil vừa xuất hiện tại " + TileMapBasil);
                    Output($"Basil Wolf Spawn: [Mapid: {oldMapBasil}, Zone: {oldZoneBasil}]");
                }
            }
            if (DelayLavender < timeserver)
            {
                DelayLavender = _5MINUTES + timeserver;
                if (!SpawnLavender)
                {
                    SpawnLavender = true;
                    Lavender = SetUpBoss(Lavender, 101);
                    var zone = PickMapSpawn(BaMapDauTuongLai[ServerUtils.RandomNumber(BaMapDauTuongLai.Count)]);
                    zone.ZoneHandler.AddBoss(Lavender);
                    oldMapLavender = zone.Map.Id;
                    oldZoneLavender = zone.Id;
                    TileMapLavender = zone.Map.TileMap.Name;
                    SendBossServerChat("Lavender vừa xuất hiện tại " + zone.Map.TileMap.Name);
                    Output($"Lavender Wolf Spawn: [Mapid: {oldMapLavender}, Zone: {oldZoneLavender}]");
                }
                else
                {
                    SendBossServerChat("Lavender vừa xuất hiện tại " + TileMapLavender);
                    Output($"Lavender Wolf Spawn: [Mapid: {oldMapLavender}, Zone: {oldZoneLavender}]");
                }
            }
            if (DelayBergamo < timeserver)
            {
                DelayBergamo = _5MINUTES + timeserver;
                if (!SpawnBergamo)
                {
                    SpawnBergamo = true;
                    Bergamo = SetUpBoss(Bergamo, 102);
                    var zone = PickMapSpawn(BaMapDauTuongLai[ServerUtils.RandomNumber(BaMapDauTuongLai.Count)]);
                    zone.ZoneHandler.AddBoss(Bergamo);
                    oldMapBergamo = zone.Map.Id;
                    oldZoneBergamo = zone.Id;
                    TileMapBergamo = zone.Map.TileMap.Name;
                    SendBossServerChat("Bergamo vừa xuất hiện tại " + TileMapBergamo);
                    Output($"Bergamo Wolf Spawn: [Mapid: {oldMapBergamo}, Zone: {oldZoneBergamo}]");
                }
                else
                {
                    SendBossServerChat("Bergamo vừa xuất hiện tại " + TileMapBergamo);
                    Output($"Bergamo Wolf Spawn: [Mapid: {oldMapBergamo}, Zone: {oldZoneBergamo}]");
                }
            }
            if (DelayCumber < timeserver)
            {
                DelayCumber = _5MINUTES + timeserver;
                if (!CumberSpawn)
                {
                    CumberSpawn = true;
                    Cumber = SetUpBoss(Cumber, 105);
                    var zone = PickMapSpawn(155);
                    zone.ZoneHandler.AddBoss(Cumber);
                    oldMapCumber = zone.Map.Id;
                    oldZoneCumber = zone.Id;
                    TileMapCumber = zone.Map.TileMap.Name;
                    Output($"Cumber Spawn: [Mapid: {oldMapCumber}, Zone: {oldZoneCumber}");
                    SendBossServerChat($"Cumber vừa xuất hiện tại {TileMapCumber}");
                }
                else
                {
                    Output($"Cumber Spawn: [Mapid: {oldMapCumber}, Zone: {oldZoneCumber}");
                    SendBossServerChat($"Cumber vừa xuất hiện tại {TileMapCumber}");
                }
            }
            if (DelayColer1 < timeserver)
            {
                DelayColer1 = _5MINUTES + timeserver;
                if (!Coler1Spawn)
                {
                    Coler1Spawn = true;
                    var zone = PickMapSpawn(CoolerMaps[ServerUtils.RandomNumber(CoolerMaps.Count)]);
                    Coler1 = SetUpBoss(Coler1, 10);
                    zone.ZoneHandler.AddBoss(Coler1);
                    oldMapColer1 = zone.Map.Id;
                    oldZoneColer1 = zone.Id;
                    TileMapColer1 = zone.Map.TileMap.Name;
                    Output($"Coler 1 Spawn: [Mapid: {oldMapColer1}, Zone: {oldZoneColer1}");
                    SendBossServerChat($"Coler 1 vưa xuất hiện tại {TileMapColer1}");
                }
                else
                {
                    Output($"Coler 1 Spawn: [Mapid: {oldMapColer1}, Zone: {oldZoneColer1}");
                    SendBossServerChat($"Coler 1 vưa xuất hiện tại {TileMapColer1}");
                }
            }
            if (DelayColer2 < timeserver)
            {
                DelayColer2 = _5MINUTES + timeserver;
                if (!Coler2Spawn)
                {
                    Coler2Spawn = true;
                    var zone = PickMapSpawn(CoolerMaps[ServerUtils.RandomNumber(CoolerMaps.Count)]);
                    Coler2 = SetUpBoss(Coler1, 10);
                    zone.ZoneHandler.AddBoss(Coler2);
                    oldMapColer2 = zone.Map.Id;
                    oldZoneColer2 = zone.Id;
                    TileMapColer2 = zone.Map.TileMap.Name;
                    Output($"Coler 2 Spawn: [Mapid: {oldMapColer2}, Zone: {oldZoneColer2}");
                    SendBossServerChat($"Coler 2 vưa xuất hiện tại {TileMapColer2}");
                }
                else
                {
                    Output($"Coler 2 Spawn: [Mapid: {oldMapColer2}, Zone: {oldZoneColer2}");
                    SendBossServerChat($"Coler 2 vưa xuất hiện tại {TileMapColer2}");
                }
            }
            if (DelayChilled < timeserver)
            {
                DelayChilled = _5MINUTES + timeserver;
                if (!ChilledSpawn)
                {
                    ChilledSpawn = true;
                    var zone = PickMapSpawn(ChilledMaps[ServerUtils.RandomNumber(ChilledMaps.Count)]);
                    Chilled = SetUpBoss(Chilled, 14);
                    zone.ZoneHandler.AddBoss(Chilled);
                    TileMapChilled = zone.Map.TileMap.Name;
                    oldMapChilled = zone.Map.
                        Id;
                    oldZoneChilled = zone.Id;
                    Output($"Chilled 1 Spawn: [Mapid: {oldMapChilled}, Zone: {oldZoneChilled}");
                    SendBossServerChat($"Chilled 1 vừa xuất hiện tại {TileMapChilled}");
                }
                else
                {
                    Output($"Chilled 1 Spawn: [Mapid: {oldMapChilled}, Zone: {oldZoneChilled}");
                    SendBossServerChat($"Chilled 1 vừa xuất hiện tại {TileMapChilled}");
                }
            }
            if (DelayChilled2 < timeserver)
            {
                DelayChilled2 = _5MINUTES + timeserver;
                if (!Chilled2Spawn)
                {
                    Chilled2Spawn = true;
                    var zone = PickMapSpawn(ChilledMaps[ServerUtils.RandomNumber(ChilledMaps.Count)]);
                    Chilled2 = SetUpBoss(Chilled, 15);
                    zone.ZoneHandler.AddBoss(Chilled2);
                    TileMapChilled2 = zone.Map.TileMap.Name;
                    oldMapChilled2= zone.Map.
                        Id;
                    oldZoneChilled2 = zone.Id;
                    Output($"Chilled 2 Spawn: [Mapid: {oldMapChilled2}, Zone: {oldZoneChilled2}");
                    SendBossServerChat($"Chilled 2 vừa xuất hiện tại {TileMapChilled2}");
                }
                else
                {
                    Output($"Chilled 2 Spawn: [Mapid: {oldMapChilled2}, Zone: {oldZoneChilled2}");
                    SendBossServerChat($"Chilled 2 vừa xuất hiện tại {TileMapChilled2}");
                }
            }
            if (DelayXenHoanThien2 < timeserver)
            {
                DelayXenHoanThien2 = _5MINUTES + timeserver;
                if (!XenHoanThienSpawn2)
                {
                    var zone = PickMapSpawn(103);
                    XenHoanThienSpawn2 = true;
                    XenHoanThien2 = SetUpBoss(XenHoanThien2, 63);
                    zone.ZoneHandler.AddBoss(XenHoanThien2);
                    TileMapXenHoanThien2 = zone.Map.TileMap.Name;
                    oldZoneXenHoanThien2 = zone.Id;
                    oldMapXenHoanThien2 = zone.Map.Id;
                    Output($"Super Cell Spawn: [Mapid: {TileMapXenHoanThien2}, Zone: {oldZoneXenHoanThien2}");
                    SendBossServerChat($"Siêu Bọ Hung vừa xuất hiện tại {TileMapXenHoanThien2}");
                }
                else
                {
                    Output($"Super Cell Spawn: [Mapid: {TileMapXenHoanThien2}, Zone: {oldZoneXenHoanThien2}");
                    SendBossServerChat($"Siêu Bọ Hung vừa xuất hiện tại {TileMapXenHoanThien2}");
                }
            }
            if (delayPicHe < timeserver)
            {
                delayPicHe = _5MINUTES + timeserver;
                if (!SpawnPiche)
                {
                    var zone = PickMapSpawn(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    SpawnPiche = true;
                    Piche = SetUpBoss(Piche, 99);
                    zone.ZoneHandler.AddBoss(Piche);
                    tileMapPicHe = zone.Map.TileMap.Name;
                    ZonePicHe = Piche.Zone.Id;
                    MapPicHe = zone.Map.Id;
                    Output($"Summer Pic Spawn: [Mapid: {MapPicHe}, Zone: {ZonePicHe}]");
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic hè vừa xuất hiện tại " + tileMapPicHe));
                }
                else
                {
                    Output($"Summer Pic Spawn: [Mapid: {MapPicHe}, Zone: {ZonePicHe}]");
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic hè vừa xuất hiện tại " + tileMapPicHe));

                }
            }
            if (delayKingKongHe < timeserver)
            {
                delayKingKongHe = _5MINUTES + timeserver;
                if (!SpawnKKHe)
                {
                    SpawnKKHe = true;
                    var zone = PickMapSpawn(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    KingKongHe = SetUpBoss(KingKongHe, 99);
                    zone.ZoneHandler.AddBoss(KingKongHe);
                    ZoneKKhe = KingKongHe.Zone.Id;
                    tileMapKingKongHe = zone.Map.TileMap.Name;
                    MapKKHe = zone.Map.Id;
                    Output($"Summer Kingkong Spawn: [Mapid: {MapKKHe}, Zone: {ZoneKKhe}]");
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong Hè vừa xuất hiện tại " + tileMapKingKongHe));
                }
                else
                {
                    Output($"Summer Kingkong Spawn: [Mapid: {MapKKHe}, Zone: {ZoneKKhe}]");
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong Hè vừa xuất hiện tại " + tileMapKingKongHe));
                }
            }
            if (DelayXenBoHung < timeserver)
            {
                DelayXenBoHung = _5MINUTES + timeserver;
                if (!SpawnXenBoHung)
                {
                    SpawnXenBoHung = true;
                    var zone = PickMapSpawn(CellMaps[ServerUtils.RandomNumber(CellMaps.Count)]);
                    for (int i = 0; i < ListBossXenBoHung.Count; i++)
                    {
                        ListBossXenBoHung[i] = SetUpBoss(ListBossXenBoHung[i], IDBossesXenBoHung[i]);
                    }
                    zone.ZoneHandler.AddBoss(ListBossXenBoHung[0]);
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Xên Bọ Hung 1 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapXenBoHung = zone.Map.Id;
                    oldZoneXenBoHung = zone.Id;
                    TileMapXenBoHung = zone.Map.TileMap.Name;
                    Output($"Cell 1 Spawn: [Mapid: {oldMapXenBoHung}, Zone: {oldZoneXenBoHung}]");

                }
                else
                {
                    Output($"Cell 1 Spawn: [Mapid: {oldMapXenBoHung}, Zone: {oldZoneXenBoHung}]");
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Xên Bọ Hung 1 vừa xuất hiện tại " + TileMapXenBoHung));

                }
            }
            if (DelaySatThu3 < timeserver)
            {
                DelaySatThu3 = _5MINUTES + timeserver;
                if (!SpawnSatThu3)
                {
                    SpawnSatThu3 = true;
                    var zone = PickMapSpawn(PicPocKKMaps[ServerUtils.RandomNumber(PicPocKKMaps.Count)]);
                    for (int i = 0; i < ListBossSatThu3.Count;i++)
                    {
                        if (i == 0) ListBossSatThu3[i] = SetUpBoss(ListBossSatThu3[i], IDBossesSatThu3[i]);
                        else ListBossSatThu3[i] = SetUpBossNoAttack(ListBossSatThu3[i], IDBossesSatThu3[i]);
                        zone.ZoneHandler.AddBoss(ListBossSatThu3[i]);
                    }
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Poc vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapSatThu3 = zone.Map.Id;
                    oldZoneSatThu3 = zone.Id;
                    TileMapSatThu3 = zone.Map.TileMap.Name;
                    Output($"Poc Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                    Output($"Pic Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                    Output($"Kingkong Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Poc vừa xuất hiện tại " + TileMapSatThu3));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic vừa xuất hiện tại " + TileMapSatThu3));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong vừa xuất hiện tại " + TileMapSatThu3));
                    Output($"Poc Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                    Output($"Pic Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                    Output($"Kingkong Spawn: [Mapid: {oldMapSatThu3}, Zone: {oldZoneSatThu3}]");
                }
            }
            if (DelaySatThu2 < timeserver)
            {
                DelaySatThu2 = _5MINUTES + timeserver;
                if (!SpawnSatThu2)
                {
                    SpawnSatThu2 = true;
                    var zone = PickMapSpawn(SanSauSieuThi[0]);
                    for (int i = 0; i < ListBossSatThu2.Count; i++)
                    {
                        if (i == 0) ListBossSatThu2[i] = SetUpBoss(ListBossSatThu2[i], IDBossesSatThu2[i]);
                        else ListBossSatThu2[i] = SetUpBossNoAttack(ListBossSatThu2[i], IDBossesSatThu2[i]);
                        zone.ZoneHandler.AddBoss(ListBossSatThu2[i]);
                    }
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 15 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 14 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 13 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapSatThu2 = zone.Map.Id;
                    oldZoneSatThu2 = zone.Id;
                    TileMapSatThu2 = zone.Map.TileMap.Name;
                    Output($"Android 15 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                    Output($"Android 14 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                    Output($"Android 13 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 15 vừa xuất hiện tại " + TileMapSatThu2));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 14 vừa xuất hiện tại " + TileMapSatThu2));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 13 vừa xuất hiện tại " + TileMapSatThu2));
                    Output($"Android 15 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                    Output($"Android 14 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                    Output($"Android 13 Spawn: [Mapid: {oldMapSatThu2}, Zone: {oldZoneSatThu2}]");
                }
            }
            if (DelaySatThu1 < timeserver)
            {
                DelaySatThu1 = _5MINUTES + timeserver;
                if (!SpawnSatThu1)
                {
                    SpawnSatThu1 = true;
                    var zone = PickMapSpawn(BaMapDauTuongLai[ServerUtils.RandomNumber(BaMapDauTuongLai.Count)]);
                    for (int i = 0; i < ListBossSatThu1.Count; i++)
                    {
                        if (i == 0) ListBossSatThu1[i] = SetUpBoss(ListBossSatThu1[i], IDBossesSatThu1[i]);
                        else ListBossSatThu1[i] = SetUpBossNoAttack(ListBossSatThu1[i], IDBossesSatThu1[i]);
                        zone.ZoneHandler.AddBoss(ListBossSatThu1[i]);
                    }
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 19 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 20 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapSatThu1 = zone.Map.Id;
                    oldZoneSatThu1 = zone.Id;
                    TileMapSatThu1 = zone.Map.TileMap.Name;
                    Output($"Android 19 Spawn: [Mapid: {oldMapSatThu1}, Zone: {oldZoneSatThu1}]");
                    Output($"Android 20 Spawn: [Mapid: {oldMapSatThu1}, Zone: {oldZoneSatThu1}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 19 vừa xuất hiện tại " + TileMapSatThu1));
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Android 20 vừa xuất hiện tại " + TileMapSatThu1));
                    Output($"Android 19 Spawn: [Mapid: {oldMapSatThu1}, Zone: {oldZoneSatThu1}]");
                    Output($"Android 20 Spawn: [Mapid: {oldMapSatThu1}, Zone: {oldZoneSatThu1}]");
                }
            }
            if (DelayMapDauDinh < timeserver)
            {
                DelayMapDauDinh = _5MINUTES + timeserver;
                if (!MapDauDinhSpawn)
                {
                    MapDauDinhSpawn = true;
                    MapDauDinh = SetUpBoss(MapDauDinh, 25);
                    var zone = PickMapSpawn(NappaMaps[ServerUtils.RandomNumber(NappaMaps.Count)]);
                    zone.ZoneHandler.AddBoss(MapDauDinh);
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Mập Đầu Đinh vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapMapDauDinh = zone.Map.Id;
                    oldZoneMapDauDinh = zone.Id;
                    TileMapMapDauDinh = zone.Map.TileMap.Name;
                    Output($"Map Dau Dinh Spawn: [Mapid: {oldMapMapDauDinh}, Zone: {oldZoneMapDauDinh}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Mập Đầu Đinh vừa xuất hiện tại " + TileMapMapDauDinh));
                    Output($"Map Dau Dinh Spawn: [Mapid: {oldMapMapDauDinh}, Zone: {oldZoneMapDauDinh}]");

                }
            }
            if (DelayRambo < timeserver)
            {
                DelayRambo = _5MINUTES + timeserver;
                if (!RamboSpawn)
                {
                    RamboSpawn = true;
                    Rambo = SetUpBoss(Rambo, 26);
                    var zone = PickMapSpawn(NappaMaps[ServerUtils.RandomNumber(NappaMaps.Count)]);
                    zone.ZoneHandler.AddBoss(Rambo);
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Rambo vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapRambo = zone.Map.Id;
                    oldZoneRambo = zone.Id;
                    TileMapRambo = zone.Map.TileMap.Name;
                    Output($"Rambo Spawn: [Mapid: {oldMapRambo}, Zone: {oldZoneRambo}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Rambo vừa xuất hiện tại " + TileMapRambo));
                    Output($"Rambo Spawn: [Mapid: {oldMapRambo}, Zone: {oldZoneRambo}]");

                }
            }
            if (DelayKuku < timeserver)
            {
                DelayKuku = _5MINUTES + timeserver;
                if (!KukuSpawn)
                {
                    KukuSpawn = true;
                    Kuku = SetUpBoss(Kuku,24);
                    var zone = PickMapSpawn(NappaMaps[ServerUtils.RandomNumber(NappaMaps.Count)]);
                    zone.ZoneHandler.AddBoss(Kuku);
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kuku vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapKuku = zone.Map.Id;
                    oldZoneKuku = zone.Id;
                    TileMapKuku = zone.Map.TileMap.Name;
                    Output($"Kuku Spawn: [Mapid: {oldMapKuku}, Zone: {oldZoneKuku}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kuku vừa xuất hiện tại " + TileMapKuku));
                    Output($"Kuku Spawn: [Mapid: {oldMapKuku}, Zone: {oldZoneKuku}]");

                }
            }
            if (DelayFide1 < timeserver)
            {
                DelayFide1 = _5MINUTES + timeserver;
                if (!Fide1Spawn)
                {
                    Fide1Spawn = true;
                    Fide1 = SetUpBoss(Fide1, 4);
                    var zone = PickMapSpawn(FideMaps[ServerUtils.RandomNumber(FideMaps.Count)]);
                    zone.ZoneHandler.AddBoss(Fide1);
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại ca 1 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapFide1 = zone.Map.Id;
                    oldZoneFide1 = zone.Id;
                    TileMapFide1 = zone.Map.TileMap.Name;
                    Output($"Fide Spawn: [Mapid: {oldMapFide1}, Zone: {oldZoneFide1}]");

                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại ca 1 vừa xuất hiện tại " + TileMapFide1));
                    Output($"Fide Spawn: [Mapid: {oldMapFide1}, Zone: {oldZoneFide1}]");
                }
            }
            if (DelayBlackGoku < timeserver)
            {
                DelayBlackGoku = _5MINUTES + timeserver;
                if (!BlackGokuSpawn)
                {
                    BlackGokuSpawn = true;
                    BlackGoku = SetUpBoss(BlackGoku, 2);
                    var zone = PickMapSpawn(BaMapDauTuongLai[ServerUtils.RandomNumber(BaMapDauTuongLai.Count)]);
                    zone.ZoneHandler.AddBoss(BlackGoku);
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Black Goku vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapBlackGoku = zone.Map.Id;
                    oldZoneBlackGoku = zone.Id;
                    TileMapBlackGoku = zone.Map.TileMap.Name;
                    Output($"Black Goku Spawn: [Mapid: {oldMapBlackGoku}, Zone: {oldZoneBlackGoku}]");
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Black Goku vừa xuất hiện tại " + TileMapBlackGoku));
                    Output($"Black Goku Spawn: [Mapid: {oldMapBlackGoku}, Zone: {oldZoneBlackGoku}]");
                }
            }
            if (DelayBroly < timeserver)
            {
                DelayBroly = _45SECONDS + timeserver;
                if (countBroly < 10)
                {
                    Broly = SetUpBoss(Broly, 41);
                    var zone = PickMapSpawnNotHasBosses(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    zone.ZoneHandler.AddBoss(Broly);
                    //ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Broly vừa xuất hiện tại " + zone.Map.TileMap.Name));
                    oldMapBroly = zone.Map.Id;
                    oldZoneBroly = zone.Id;
                    TileMapBroly = zone.Map.TileMap.Name;
                    //Output($"Broly Spawn: [Mapid: {zone.Map.Id}, Zone: {zone.Id}]");
                    countBroly++;
                    ListBroly.Add(Broly);
                }
                else {
                    for (int i = 0; i < ListBroly.Count; i++)
                    {
                        //ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Broly vừa xuất hiện tại " + ListBroly[i].Zone.Map.TileMap.Name));
                        //Output($"Broly Spawn: [Mapid: {ListBroly[i].Zone.Map.Id}, Zone: {ListBroly[i].Zone.Id}]");

                    }
                }
            }
            if (DelayTDST < timeserver)
            {
                DelayTDST = _5MINUTES + timeserver;
                if (!SpawnTDST)
                {
                    var zone = PickMapSpawn(TDSTMaps[ServerUtils.RandomNumber(TDSTMaps.Count)]);
                    SpawnTDST = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 0) ListBossTDST[i] = SetUpBoss(ListBossTDST[i], IdBossesTDST[i]);
                        else ListBossTDST[i] = SetUpBossNoAttack(ListBossTDST[i], IdBossesTDST[i]);
                        zone.ZoneHandler.AddBoss(ListBossTDST[i]);
                    }
                    oldMapTDST = zone.Map.Id;
                    oldZoneTDST = zone.Id;
                    TileMapTDST = zone.Map.TileMap.Name;
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Tiểu đội sát thủ vừa xuất hiện tại " + TileMapTDST));
                    Output($"TDST Spawn: [Mapid: {oldMapTDST}, {oldZoneTDST}]");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Tiểu đội sát thủ vừa xuất hiện tại " + TileMapTDST));
                    Output($"TDST Spawn: [Mapid: {oldMapTDST}, {oldZoneTDST}]");
                }
            }
        }

        public void BossDied(Boss Bossist)
        {
            var timeserver = ServerUtils.CurrentTimeMillis();
            switch (Bossist.Type)
            {
                case 41:
                    if (Bossist.HpFull >= 1500000)
                    {
                        var SuperBroly = new Boss();
                        SuperBroly.CreateBossSuperBroly();
                        SuperBroly.CharacterHandler.SetUpInfo();
                        Bossist.Zone.ZoneHandler.AddBoss(SuperBroly);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Broly vừa xuất hiện tại " + Bossist.Zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Super Broly] Map: " + Bossist.Zone.Map.Id + " | K: " + Bossist.Zone.Id, "yellow");
                        ABoss.gI().countBroly--;
                        ABoss.gI().ListBroly.Remove(Bossist);
                    }
                    else if (ServerUtils.RandomNumber(100) < 15)
                    {
                        var SuperBroly = new Boss();
                        SuperBroly.CreateBossSuperBroly();
                        SuperBroly.CharacterHandler.SetUpInfo();
                        Bossist.Zone.ZoneHandler.AddBoss(SuperBroly);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Broly vừa xuất hiện tại " + Bossist.Zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Super Broly] Map: " + Bossist.Zone.Map.Id + " | K: " + Bossist.Zone.Id, "yellow");
                        ABoss.gI().countBroly--;
                        ABoss.gI().ListBroly.Remove(Bossist);
                    }
                    else
                    {
                        async void ActionRespawn()
                        {
                            await Task.Delay(15000);
                            var broly = ABoss.gI().ListBroly.FirstOrDefault(boss => boss.Id == Bossist.Id);
                            if (broly != null)
                            {
                                broly = new Boss();
                                broly.CreateBoss(41);
                                broly.InfoChar.Hp = broly.HpFull = broly.HpFull + (broly.HpFull * 10 / 100);
                                broly.CharacterHandler.SetUpInfo();
                                Bossist.Zone.ZoneHandler.AddBoss(broly);

                            }
                        }
                        var task = new Task(ActionRespawn);
                        task.Start();
                    }
                    break;
                case 105:
                    Cumber = null;
                    DelayCumber = _5MINUTES + timeserver;
                    SuperCumber = SetUpBoss(SuperCumber, 106);
                    Bossist.Zone.ZoneHandler.AddBoss(SuperCumber);
                    break;
                case 106:
                    CumberSpawn = false;
                    DelayCumber = _5MINUTES + timeserver;
                    break;
                case 10:
                    Coler1 = null;
                    Coler1Spawn = false;
                    break;
                case 11:
                    Coler2 = null;
                    Coler2Spawn = false;
                    break;
                case 14:
                    Chilled = null;
                    ChilledSpawn = false;
                    break;
                case 15:
                    Chilled2 = null;
                    Chilled2Spawn = false;
                    break;
                case 63:
                    XenHoanThien2 = null;
                    XenHoanThienSpawn2 = false;
                    break;
                case 100:
                    Basil = null;
                    DelayBasil = 120000 + timeserver;
                    SpawnBasil = false;
                    break;
                case 101:
                    Lavender = null;
                    DelayLavender = 120000 + timeserver;
                    SpawnLavender = false;
                    break;
                case 102:
                    Bergamo = null;
                    DelayBergamo = 120000 + timeserver;
                    SpawnBergamo = false;
                    break;
                case 24:
                    Kuku = null;
                    KukuSpawn = false;
                    DelayKuku = 20000 + timeserver;
                    break;
                case 26:
                    Rambo = null;

                    RamboSpawn = false;
                    DelayRambo = 20000 + timeserver;
                    break;
                case 25:
                    MapDauDinh = null;
                    MapDauDinhSpawn = false;
                    DelayMapDauDinh = 20000 + timeserver;
                    break;
                case 98:
                    Piche = null;
                    SpawnPiche = false;
                    break;
                case 99:
                    KingKongHe = null;
                    SpawnKKHe = false;
                    break;
               
                case 7:
                    ListBossXenBoHung[0] = null;
                    Bossist.Zone.ZoneHandler.AddBoss(ListBossXenBoHung[1]);
                    break;
                case 8:
                    ListBossXenBoHung[1] = null;
                    Bossist.Zone.ZoneHandler.AddBoss(ListBossXenBoHung[2]);
                    break;
                case 9:
                    ListBossXenBoHung[2] = null;
                    DelayXenBoHung = 129000 + timeserver;
                    SpawnXenBoHung = false;
                    break;
                case 2:
                    DelayBlackGoku = 120000 + timeserver;
                    BlackGokuSpawn = false;
                    BlackGoku = null;
                    SuperBlackGoku = SetUpBoss(SuperBlackGoku, 3, Bossist.InfoChar.X, Bossist.InfoChar.Y);
                    Bossist.Zone.ZoneHandler.AddBoss(SuperBlackGoku);
                    break;
                case 4:
                    Fide1 = null;
                    Fide2 = SetUpBoss(Fide2, 5, Bossist.InfoChar.X, Bossist.InfoChar.Y);
                    Bossist.Zone.ZoneHandler.AddBoss(Fide2);
                    break;
                case 5:
                    Fide2 = null;
                    Fide3 = SetUpBoss(Fide3, 6, Bossist.InfoChar.X, Bossist.InfoChar.Y);
                    Bossist.Zone.ZoneHandler.AddBoss(Fide3);
                    break;
                case 6:
                    Fide3 = null;
                    Fide1Spawn = false;
                    DelayFide1 = 122000 + timeserver;
                    break;
                case 16:
                    for (int i = 0; i < ListBossTDST.Count; i++)
                    {
                        var bosses = Bossist.Zone.ZoneHandler.GetBossInMap(IdBossesTDST[i]);
                        if (bosses.Count > 0)
                        {
                            var @boss = (Boss)bosses[0];
                            boss.InfoDelayBoss.DelayRemove = 350000 + timeserver;
                            DelayTDST = 400000 + timeserver;

                        }
                    }
                    ListBossTDST[0] = null;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossTDST[1].Id, 5));
                    ListBossTDST[1].InfoChar.TypePk = 5;
                    break;
                case 17:
                    ListBossTDST[1] = null;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossTDST[2].Id, 5));
                    ListBossTDST[2].InfoChar.TypePk = 5;
                    break;
                case 93:
                    ListBossTDST[2] = null;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossTDST[3].Id, 5));
                    ListBossTDST[3].InfoChar.TypePk = 5;
                    break;
                case 18:
                    ListBossTDST[3] = null;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossTDST[4].Id, 5));
                    ListBossTDST[4].InfoChar.TypePk = 5;
                    break;
                case 19:
                    ListBossTDST[4] = null;
                    DelayTDST = 15000 + timeserver;
                    SpawnTDST = false;
                    break;
                case 28:
                    ListBossSatThu3[0] = null;
                    ListBossSatThu3[1].InfoChar.TypePk = 5;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossSatThu3[1].Id, 5));
                    break;
                case 27:
                    ListBossSatThu3[1] = null;
                    ListBossSatThu3[2].InfoChar.TypePk = 5;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossSatThu3[2].Id, 5));
                    break;
                case 29:
                    ListBossSatThu3[2] = null;
                    DelaySatThu3 = 126000 + timeserver;
                    SpawnSatThu3 = false;
                    break;
                case 30:
                    ListBossSatThu1[0] = null;
                    ListBossSatThu1[1].InfoChar.TypePk = 5;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossSatThu1[1].Id, 5));
                    break;
                case 31:
                    ListBossSatThu1[1] = null;
                    DelaySatThu1 = 127000 + timeserver;
                    SpawnSatThu1 = false;
                    break;
                case 34:
                    ListBossSatThu2[0] = null;
                    ListBossSatThu2[1].InfoChar.TypePk = 5;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossSatThu2[1].Id, 5));
                    break;
                case 33:
                    ListBossSatThu2[1] = null;
                    ListBossSatThu2[2].InfoChar.TypePk = 5;
                    Bossist.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(ListBossSatThu2[2].Id, 5));
                    break;
                case 32:
                    ListBossSatThu2[2] = null;
                    DelaySatThu2 = 128000 + timeserver;
                    SpawnSatThu2 = false;
                    break;
            }
        }
        public List<int> CountBoss = new List<int> { 0,0};
        public List<string> TileMap1 = new List<string>();
        public List<string> TileMap2 = new List<string>();

        public void StartBossRunTime2(long timeserver)
        {
            for (int i = 0; i < Cache.Gi().AUTOBOSS_TEMPLATE.Count; i++)
            {
                var boss = Cache.Gi().AUTOBOSS_TEMPLATE[i];
                var bossTemp = Cache.Gi().BOSS_TEMPLATES.FirstOrDefault(i2 => i2.Type == boss.Type);
                if (boss.TimeSpawn < timeserver && CountBoss[i] < boss.Count)
                {
                    var bossSummon = new Boss();
                    var randomPosistion = ServerUtils.RandomNumber(boss.Map.Length);
                    bossSummon.CreateBoss(boss.Type, boss.X[randomPosistion], boss.y[randomPosistion]);
                    var map = MapManager.Get(boss.Map[randomPosistion]);
                    bossSummon.CharacterHandler.SetUpInfo();
                    map.Zones[ServerUtils.RandomNumber(20)].ZoneHandler.AddBoss(bossSummon);
                    ClientManager.Gi().SendMessage(Service.ServerChat("Boss " + bossTemp.Name + " vừa xuất hiện tại " + map.TileMap.Name));
                    boss.TimeSpawn += boss.Delay + timeserver;
                }
                
            }
           
        }
        public Boss KingKongHe = null;
        public Boss Piche = null;
        public long delayKingKongHe = -1;
        public long delayPicHe = -1;
        public string tileMapKingKongHe = "";
        public string tileMapPicHe = "";
        public int CountKKHe = 0;
        public int CountPicHe = 0;
        public bool SpawnKKHe = false;
        public bool SpawnPiche = false;
        public int IdKKHe = -1;
        public int IdPicHe = -1;
        public int ZoneKKhe = -1;
        public int ZonePicHe = -1;
        public int MapKKHe = -1;
        public int MapPicHe = -1;
        public void StartBossRunTime(long timeserver)
        {
            if (delayKingKongHe < timeserver)
            {
                delayKingKongHe = 120000 + timeserver;
                if (!SpawnKKHe)
                {
                    var map = MapManager.Get(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    SpawnKKHe = true;
                    KingKongHe = new Boss();
                    KingKongHe.CreateBoss(98, 500, 346);
                    KingKongHe.CharacterHandler.SetUpInfo();
                    var randZone = ServerUtils.RandomNumber(20);
                    map.Zones[randZone].ZoneHandler.AddBoss(KingKongHe);
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong hè vừa xuất hiện tại " + map.TileMap.Name));
                    Server.Gi().Logger.Print("KingKong HE : " + map.Id + " | " + KingKongHe.Zone.Id, "blue");
                    ZoneKKhe = KingKongHe.Zone.Id;
                    tileMapKingKongHe = map.TileMap.Name;
                    MapKKHe = map.Id;  
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Kingkong hè vừa xuất hiện tại " + tileMapKingKongHe));
                    Server.Gi().Logger.Print("KingKong HE : " + MapKKHe + " | " + ZoneKKhe, "blue");

                }
            }
            if (delayPicHe < timeserver)
            {
                delayPicHe = 120000 + timeserver;
                if (!SpawnPiche)
                {
                    var map = MapManager.Get(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    SpawnPiche = true;
                    Piche = new Boss();
                    Piche.CreateBoss(98, 500, 346);
                    Piche.CharacterHandler.SetUpInfo();
                    var randZone = ServerUtils.RandomNumber(20);
                    map.Zones[randZone].ZoneHandler.AddBoss(Piche);

                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic hè vừa xuất hiện tại " + map.TileMap.Name));
                    tileMapPicHe = map.TileMap.Name;
                    ZonePicHe = Piche.Zone.Id;
                    MapPicHe = map.Id;
                    Server.Gi().Logger.Print("PIC HE : " + map.Id + " | " + Piche.Zone.Id, "blue");
                }
                else
                {
                    ClientManager.Gi().SendMessage(Service.ServerChat("BOSS Pic hè vừa xuất hiện tại " + tileMapPicHe));
                    Server.Gi().Logger.Print("PIC HE : " + MapPicHe + " | " + ZonePicHe, "blue");

                }
            }
            if (DelayMabu <= timeserver)
            {
                DelayMabu = 900000 + timeserver;
                if (!isMabuSpawn)
                {
                    isMabuSpawn = true;
                    var Map = MapManager.Get(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    var Zone = Map.Zones[ServerUtils.RandomNumber(1, 9)];
                    Mabu = new Boss();
                    Mabu.CreateBoss(86);
                    Mabu.CharacterHandler.SetUpInfo();
                    Zone.ZoneHandler.AddBoss(Mabu);
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Mabu vừa xuất hiện tại " + Zone.Map.TileMap.Name));
                    Server.Gi().Logger.Print("[Spawn Super Mabu] Map: " + Map.Id + " | K: " + Zone.Id, "blue");
                    TileMapMabu = Zone.Map.TileMap.Name;
                    OldZoneMabu = Zone.Id;
                    OldMapMabu = Map.Id;
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Mabu vừa xuất hiện tại " + TileMapMabu));
                    Server.Gi().Logger.Print("[Spawn Super Mabu] Map: " + OldMapMabu + " | K: " + OldZoneMabu, "blue");
                }
            }
            if (DelayNoONo <= timeserver)
            {
                DelayNoONo = 32000 + timeserver;
                if (countNoONo <= 5)
                {
                    var Map = MapManager.Get(mapTraiDat[ServerUtils.RandomNumber(mapTraiDat.Count)]);
                    var Zone = Map.Zones[ServerUtils.RandomNumber(1, 9)];
                    countNoONo++;
                    NoONo = new Boss();
                    NoONo.CreateBoss(85);
                    NoONo.CharacterHandler.SetUpInfo();
                    Zone.ZoneHandler.AddBoss(NoONo);
                }
            }
            if ( timeserver >= DelayBlackGoku)
            {
                DelayBlackGoku = 900000 + timeserver;
                if (!BlackGokuSpawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(1, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(BlackGokuMaps.Count);
                    int sbRandomMap = BlackGokuMaps[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {

                        BlackGoku = new Boss();
                        idBlackGoku = BlackGoku.Id;
                        BlackGokuSpawn = true;
                        BlackGokuNotify = true;
                        BlackGoku.CreateBoss(2);
                        oldMapBlackGoku = zone.Map.Id;
                        oldZoneBlackGoku = zone.Id;
                        TileMapBlackGoku = zone.Map.TileMap.Name;
                        BlackGoku.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(BlackGoku);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Black Goku vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Black Goku] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Black Goku vừa xuất hiện tại " + TileMapBlackGoku));
                    Server.Gi().Logger.Print("[Spawn Black Goku] Map: " + oldMapBlackGoku + " | K: " + oldZoneBlackGoku, "blue");
                }
            }
            if (timeserver >= DelaySuperBroly && countSuperBroly >= 1)
            {
                DelaySuperBroly = 180000 + timeserver;
                int sbRandomZoneNum = oldZoneBroly;
                int sbRandomMapIndex = ServerUtils.RandomNumber(mapTraiDat.Count);
                int sbRandomMap = oldMapBroly;
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (zone != null)
                {
                   // ---SUPER BROLY
                   countSuperBroly -= 1;
                    SuperBroly = new Boss();
                    SuperBroly.CreateBossSuperBroly();
                    SuperBroly.CharacterHandler.SetUpInfo();
                    zone.ZoneHandler.AddBoss(SuperBroly);
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Broly vừa xuất hiện tại " + TileMapBroly));
                    Server.Gi().Logger.Print("[Spawn Super Broly] Map: " + oldMapBroly + " | K: " + oldZoneBroly, "yellow");

                }
            }

            if (timeserver >= DelayXen1)
            {
                DelayXen1 = 900000 + timeserver;
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(CellMaps.Count);
                int sbRandomMap = CellMaps[sbRandomMapIndex];
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (!Xen1Spawn)
                {
                    if (zone != null)
                    {
                        Xen1Spawn = true;
                        oldMapXen1 = zone.Map.Id;
                        oldZoneXen1 = zone.Id;
                        TileMapXen1 = zone.Map.TileMap.Name;
                        // ---- XEN 1 
                        Xen1 = new Boss();
                        idXen1 = Xen1.Id;
                        Xen1.CreateBoss(7);
                        Xen1.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Xen1);
                        Server.Gi().Logger.Print("[Spawn Cell 1] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        // --- XEN 2
                        Xen2 = new Boss();
                        idXen2 = Xen2.Id;
                        Xen2.CreateBossNoAttack(8);
                        Xen2.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Xen2);
                        Server.Gi().Logger.Print("[Spawn Cell 2] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        //---XEN HOAN THIEN
                        XenHoanThien = new Boss();
                        idXenHoanThien = XenHoanThien.Id;
                        XenHoanThien.CreateBossNoAttack(9);
                        XenHoanThien.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(XenHoanThien);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Xên Bọ Hung 1 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Awesome Cell] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Xên Bọ Hung 1 vừa xuất hiện tại " + TileMapXen1));
                    Server.Gi().Logger.Print("[Spawn Cell 1] Map: " + oldMapXen1 + " | K: " + oldZoneXen1, "blue");
                }
            }

            if (timeserver >= DelayXenHoanThien2)
            {
                DelayXenHoanThien2 = 900000 + timeserver;
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(PerfectCellMaps.Count);
                int sbRandomMap = PerfectCellMaps[sbRandomMapIndex];
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (!XenHoanThienSpawn2)
                {
                    if (zone != null)
                    {
                        // --- XEN HOAN THIEN 2
                        XenHoanThien2 = new Boss();
                        idXenHoanThien2 = XenHoanThien2.Id;
                        XenHoanThien2.CreateBoss(63);
                        XenHoanThien2.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(XenHoanThien2);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu Bọ Hung vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Super Cell] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Siêu Bọ Hung vừa xuất hiện tại " + TileMapXenHoanThien2));
                    Server.Gi().Logger.Print("[Spawn Super Cell] | Map: " + oldMapXenHoanThien2 + " | K: " + oldZoneXenHoanThien2, "blue");
                }
            }
            if (timeserver >= DelayAnd19)
            {
                DelayAnd19 = 900000 + timeserver;
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(BaMapDauTuongLai.Count);
                int sbRandomMap = BaMapDauTuongLai[sbRandomMapIndex];
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (!And19Spawn && !And20Spawn)
                {
                    if (zone != null)
                    {
                        /// --- AND 19
                        And19 = new Boss();
                        idAnd19 = And19.Id;
                        And19Spawn = true;
                        oldMapAnd19 = zone.Map.Id;
                        oldZoneAnd19 = zone.Id;
                        TileMapAnd19 = zone.Map.TileMap.Name;
                        And19.CreateBoss(30);
                        And19.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(And19);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 19 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Android 19] | Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        /// --- AND 20
                        And20 = new Boss();
                        idAnd20 = And20.Id;
                        And20Spawn = true;
                        oldMapAnd20 = zone.Map.Id;
                        oldZoneAnd20 = zone.Id;
                        TileMapAnd20 = zone.Map.TileMap.Name;
                        And20.CreateBossNoAttack(31);
                        And20.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(And20);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 20 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Android 20] | Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Àndroid 19  vừa xuất hiện tại " + TileMapAnd19));
                    Server.Gi().Logger.Print("[Spawn Android 19] Map: " + oldMapAnd19 + " | K: " + oldZoneAnd19, "blue");
                }
            }
            if (timeserver >= DelayPic)
            {
                DelayPic = 900000 + timeserver;
               
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(PicPocKKMaps.Count);
                int sbRandomMap = PicPocKKMaps[sbRandomMapIndex];
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (!PicSpawn && !PocSpawn && !KingkongSpawn)
                {
                    if (zone != null)
                    {
                        /// --- PIC
                        Pic = new Boss();
                        idPic = Pic.Id;
                        PicSpawn = true;
                        oldMapPic = zone.Map.Id;
                        oldZonePic = zone.Id;
                        TileMapPic = zone.Map.TileMap.Name;
                        Pic.CreateBoss(27);
                        Pic.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Pic);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Pic vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Pic] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        /// --- POC
                        Poc = new Boss();
                        idPoc = Poc.Id;
                        PocSpawn = true;
                        oldMapPoc = zone.Map.Id;
                        oldZonePoc = zone.Id;
                        TileMapPoc = zone.Map.TileMap.Name;
                        Poc.CreateBossNoAttack(28);
                        Poc.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Poc);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Poc vừa xuất hiện tại " + zone.Map.TileMap.Name));
                       Server.Gi().Logger.Print("[Spawn Poc] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        // --- KING KONG
                        KingKong = new Boss();
                        idKingKong = KingKong.Id;
                        KingkongSpawn = true;
                        oldMapKingKong = zone.Map.Id;
                        oldZoneKingKong = zone.Id;
                        TileMapKingKong = zone.Map.TileMap.Name;
                        KingKong.CreateBossNoAttack(29);
                        KingKong.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(KingKong);

                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kingkong vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Kingkong] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                    }
                }
                else
                {
                    if (PicSpawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Pic  vừa xuất hiện tại " + TileMapPic));
                        Server.Gi().Logger.Print("[Spawn Pic] Map: " + oldMapPic + " | K: " + oldZonePic, "blue");
                    }
                    if (PocSpawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Poc  vừa xuất hiện tại " + TileMapPoc));
                        Server.Gi().Logger.Print("[Spawn Poc] Map: " + oldMapPoc + " | K: " + oldZonePoc, "blue");
                    }
                    if (KingkongSpawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kingkong  vừa xuất hiện tại " + TileMapKingKong));
                        Server.Gi().Logger.Print("[Spawn Kingkong] Map: " + oldMapKingKong + " | K: " + oldZoneKingKong, "blue");
                    }
                }
            }
            if ((timeserver >= DelayChilled && CountColer == 1) || (timeserver >= DelayChilled2 && CountColer == 2))
            {
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(CoolerMaps.Count);
                int sbRandomMap = 110;
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (timeserver >= DelayChilled && CountColer == 1)
                {
                    DelayChilled = 900000 + timeserver;
                    if (!ChilledSpawn)
                    {
                        Chilled = new Boss();
                        idChilled = Chilled.Id;
                        ChilledSpawn = true;
                        oldMapChilled = zone.Map.Id;
                        oldZoneChilled = zone.Id;
                        TileMapChilled = zone.Map.TileMap.Name;
                        Chilled.CreateBoss(10);
                        Chilled.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Chilled);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Coler 1  vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Coler 1] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        CountColer = 2;
                    }
                    else
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Coler 1  vừa xuất hiện tại " + TileMapChilled));
                        Server.Gi().Logger.Print("[Spawn Coler 1] Map: " + oldMapChilled + " | K: " + oldZoneChilled, "blue");
                        CountColer = 2;
                    }
                }
                if (timeserver >= DelayChilled2 && CountColer == 2)
                {
                    DelayChilled2 = 900000 + timeserver;
                    if (!Chilled2Spawn)
                    {
                        Chilled2 = new Boss();
                        idChilled2 = Chilled2.Id;
                        Chilled2Spawn = true;
                        oldMapChilled2 = zone.Map.Id;
                        oldZoneChilled2 = zone.Id;
                        TileMapChilled2 = zone.Map.TileMap.Name;
                        Chilled2.CreateBoss(11);
                        Chilled2.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Chilled2);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Coler 2  vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Coler 2] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        CountColer = 1;
                    }
                    else
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Coler 1  vừa xuất hiện tại " + TileMapChilled2));
                        Server.Gi().Logger.Print("[Spawn Coler 2] Map: " + oldMapChilled2 + " | K: " + oldZoneChilled2, "blue");
                        CountColer = 1;
                    }
                }
            }
            if (timeserver >= DelayAnd13 )
            {
                DelayAnd13 = 900000 + timeserver;
               
                int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                int sbRandomMapIndex = ServerUtils.RandomNumber(BaMapDauTuongLai.Count);
                int sbRandomMap = BaMapDauTuongLai[sbRandomMapIndex];
                var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                if (!And13Spawn && !And14Spawn && !And15Spawn)
                {
                    if (zone != null)
                    {
                        /// --- ANDROID 13
                        And13 = new Boss();
                        idAnd13 = And13.Id;
                        And13Spawn = true;
                        oldMapAnd13 = zone.Map.Id;
                        oldZoneAnd13 = zone.Id;
                        TileMapAnd13 = zone.Map.TileMap.Name;
                        And13.CreateBossNoAttack(32);
                        And13.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(And13);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 13 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Android 14] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        /// --- ANDROID 14
                        And14 = new Boss();
                        idAnd14 = And14.Id;
                        And14Spawn = true;
                        oldMapAnd14 = zone.Map.Id;
                        oldZoneAnd14 = zone.Id;
                        TileMapAnd14 = zone.Map.TileMap.Name;
                        And14.CreateBossNoAttack(33);
                        And14.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(And14);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 13 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Android 14] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                        // --- ANDROID 15
                        And15 = new Boss();
                        idAnd15 = And15.Id;
                        And15Spawn = true;
                        oldMapAnd15 = zone.Map.Id;
                        oldZoneAnd15 = zone.Id;
                        TileMapAnd15 = zone.Map.TileMap.Name;
                        And15.CreateBoss(34);
                        And15.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(And15);

                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 15 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Android 15] Map: " + zone.Map.Id + " | K: " + zone.Id, "blue");
                    }
                }
                else
                {
                    if (And15Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 15 vừa xuất hiện tại " + TileMapAnd15));
                        Console.WriteLine("[Spawn Android 15] Map: " + oldMapAnd15 + " | K: " + oldZoneAnd15, "blue");
                    }
                    if (And14Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 14 vừa xuất hiện tại " + TileMapAnd14));
                        Console.WriteLine("[Spawn Android 14] Map: " + oldMapAnd14 + " | K: " + oldZoneAnd14, "blue");
                    }
                    if (And13Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Android 13 vừa xuất hiện tại " + TileMapAnd13));
                        Console.WriteLine("[Spawn Android 13] Map: " + oldMapAnd13 + " | K: " + oldZoneAnd13, "blue");
                    }
                }
            }
            if (timeserver >= DelaySuperBlackGoku)
            {
                DelaySuperBlackGoku = 300000 + timeserver;
                int zoneBlack = oldZoneBlackGoku;
                int mapBlack = oldMapBlackGoku;
                var zone = MapManager.Get(mapBlack).GetZoneById(zoneBlack);
                if (!SuperBlackGokuSpawn)
                {
                    if (zone != null)
                    {
                        // --- SUPER GOKU
                        SuperBlackGoku = new Boss();
                        SuperBlackGoku.CreateBoss(3);
                        SuperBlackGoku.CharacterHandler.SetUpInfo();
                        SuperBlackGokuSpawn = true;
                        zone.ZoneHandler.AddBoss(SuperBlackGoku);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Black Goku vừa xuất hiện tại " + TileMapBlackGoku));
                        Server.Gi().Logger.Print("[< ! > Spawn Super Black Goku] Map: " + zoneBlack + " | K: " + mapBlack, "blue");

                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Super Black Goku vừa xuất hiện tại " + TileMapBlackGoku));
                    Server.Gi().Logger.Print("[< Again > Spawn Super Black Goku] Map: " + zoneBlack + " | K: " + mapBlack, "blue");
                }
            }
            if (timeserver >= DelayFide1 )
            {
                DelayFide1 = timeserver + 900000;
               
                if (!Fide1Spawn && !Fide2Spawn && !Fide3Spawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(FideMaps.Count);
                    int sbRandomMap = FideMaps[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {
                        // --- FIDE DAI CA 1
                        Fide1 = new Boss();
                        idFide1 = Fide1.Id;
                        Fide1Spawn = true;
                        Fide1Notify = true;
                        oldMapFide1 = zone.Map.Id;
                        oldZoneFide1 = zone.Id;
                        TileMapFide1 = zone.Map.TileMap.Name;
                        Fide1.CreateBoss(4);
                        Fide1.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Fide1);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 1 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Fide DC1] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        // --- FIDE DAI CA 2
                        Fide2 = new Boss();
                        idFide2 = Fide2.Id;
                        Fide2Spawn = true;
                        Fide2Notify = true;
                        oldMapFide2 = zone.Map.Id;
                        oldZoneFide2 = zone.Id;
                        TileMapFide2 = zone.Map.TileMap.Name;
                        Fide2.CreateBossNoAttack(5);
                        Fide2.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Fide2);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 2 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Fide DC2] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        // --- FIDE DAI CA 3
                        Fide3 = new Boss();
                        idFide3 = Fide3.Id;
                        Fide3Spawn = true;
                        Fide3Notify = true;
                        oldMapFide3 = zone.Map.Id;
                        oldZoneFide3 = zone.Id;
                        TileMapFide3 = zone.Map.TileMap.Name;
                        Fide3.CreateBossNoAttack(6);
                        Fide3.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Fide3);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 3 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Fide DC3] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                    }
                }
                else
                {
                    if (Fide1Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 1 vừa xuất hiện tại " + TileMapFide1));
                        Server.Gi().Logger.Print("[Spawn Fide DC1] Map: " + oldMapFide1 + " | K: " + oldZoneFide1, "blue");
                    }
                    if (Fide2Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 2 vừa xuất hiện tại " + TileMapFide2));
                        Server.Gi().Logger.Print("[Spawn Fide DC2] Map: " + oldMapFide2 + " | K: " + oldZoneFide2, "blue");
                    }
                    if (Fide3Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Fide Đại Ca 3 vừa xuất hiện tại " + TileMapFide3));
                        Server.Gi().Logger.Print("[Spawn Fide DC3] Map: " + oldMapFide3 + " | K: " + oldZoneFide3, "blue");
                    }
                }
            }
           
            if (timeserver >= DelayKuku)
            {
                DelayKuku = timeserver + 300000;

                if (!KukuSpawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(NappaMaps.Count);
                    int sbRandomMap = NappaMaps[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {
                        // --- Kuku

                        Kuku = new Boss();
                        Kuku.CreateBoss(24);
                        idKuku = Kuku.Id;
                        KukuSpawn = true;
                        oldMapKuku = zone.Map.Id;
                        oldZoneKuku = zone.Id;
                        TileMapKuku = zone.Map.TileMap.Name;
                        XKuKu = Kuku.InfoChar.X;
                        YKuku = Kuku.InfoChar.Y;
                        Kuku.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Kuku);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kuku vừa xuất hiện tại: " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Kuku] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");





                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Kuku vừa xuất hiện tại: " + TileMapKuku));
                    Server.Gi().Logger.Print("[Spawn Kuku] Map: " + oldMapKuku + " | K: " + oldZoneKuku, "blue");
                }

            }

            else
            {

            }
            if (timeserver >= DelayRambo )
            {
                DelayRambo = timeserver + 300000;
                if (!RamboSpawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(NappaMaps.Count);
                    int sbRandomMap = NappaMaps[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {
                        // --- Rambo 
                        Rambo = new Boss();
                        Rambo.CreateBoss(26);
                        idRambo = Rambo.Id;
                        RamboSpawn = true;
                        oldMapRambo = zone.Map.Id;
                        oldZoneRambo = zone.Id;
                        TileMapRambo = zone.Map.TileMap.Name;
                        XRambo = Rambo.InfoChar.X;
                        YRambo = Rambo.InfoChar.Y;
                        Rambo.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(Rambo);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Rambo vừa xuất hiện tại: " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Rambo] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                    }
                    else
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Rambo vừa xuất hiện tại: " + TileMapRambo));
                        Server.Gi().Logger.Print("[Spawn Rambo] Map: " + oldMapRambo + " | K: " + oldZoneRambo, "blue");
                    }
                }
            }
            if (timeserver >= DelayMapDauDinh)
            {
                DelayMapDauDinh = timeserver + 300000;
                if (!MapDauDinhSpawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(NappaMaps.Count);
                    int sbRandomMap = NappaMaps[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {
                        // --- Map Dau Dinh
                        MapDauDinh = new Boss();
                        MapDauDinh.CreateBoss(25);
                        idMapDauDinh = MapDauDinh.Id;
                        MapDauDinhSpawn = true;
                        oldMapMapDauDinh = zone.Map.Id;
                        oldZoneMapDauDinh = zone.Id;
                        TileMapMapDauDinh = zone.Map.TileMap.Name;
                        XMapDauDinh = MapDauDinh.InfoChar.X;
                        YMapDauDinh = MapDauDinh.InfoChar.Y;
                        MapDauDinh.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(MapDauDinh);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Mập Đàu Đinh vừa xuất hiện tại: " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Nail-Headed Fat] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                    }
                }
                else
                {
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Mập Đàu Đinh vừa xuất hiện tại: " + TileMapMapDauDinh));
                    Server.Gi().Logger.Print("[Spawn Nail-Headed Fat] Map: " + oldMapMapDauDinh + " | K: " + oldZoneMapDauDinh, "blue");
                }
            }
            if (timeserver >= DelaySo4)
            {
                DelaySo4 = timeserver + 900000;
                
                if (!So4Spawn && !So3Spawn && !So1Spawn && !TieuDoiTruongSpawn)
                {
                    int sbRandomZoneNum = ServerUtils.RandomNumber(0, 15);
                    int sbRandomMapIndex = ServerUtils.RandomNumber(DataCache.IdMapTDST.Count);
                    int sbRandomMap = DataCache.IdMapTDST[sbRandomMapIndex];
                    var zone = MapManager.Get(sbRandomMap)?.GetZoneById(sbRandomZoneNum);
                    if (zone != null)
                    {
                        //--- SO 4
                        So4 = new Boss();
                        idSo4 = So4.Id;
                        So4Spawn = true;
                        So4Notify = true;
                        So4.CreateBoss(16, 150, 360);
                        oldMapSo4 = zone.Map.Id;
                        oldZoneSo4 = zone.Id;
                        TileMapSo4 = zone.Map.TileMap.Name;
                        So4.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(So4);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 4 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Number 4] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        // --- So 3
                        So3 = new Boss();
                        idSo3 = So3.Id;
                        So3Spawn = true;
                        So3Notify = true;
                        So3.CreateBossNoAttack(17, 160, 360);
                        oldMapSo3 = zone.Map.Id;
                        oldZoneSo3 = zone.Id;
                        TileMapSo3 = zone.Map.TileMap.Name;
                        So3.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(So3);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 3 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Number 3] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        // --- So 1
                        So1 = new Boss();
                        idSo1 = So1.Id;
                        So1Spawn = true;
                        So1Notify = true;
                        So1.CreateBossNoAttack(18, 170, 360);
                        oldMapSo1 = zone.Map.Id;
                        oldZoneSo1 = zone.Id;
                        TileMapSo1 = zone.Map.TileMap.Name;
                        So1.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(So1);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 1 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Number 1] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        /// --- SO 2
                        So2 = new Boss();
                        idSo2 = So1.Id;
                        So2Spawn = true;
                        So2.CreateBossNoAttack(93, 180, 360);
                        oldMapSo2 = zone.Map.Id;
                        oldZoneSo2 = zone.Id;
                        TileMapSo2 = zone.Map.TileMap.Name;
                        So2.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(So2);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 2 vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Number 2] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                        // --- Tieu Doi Truong
                        TieuDoiTruong = new Boss();
                        idTieuDoiTruong = TieuDoiTruong.Id;
                        TieuDoiTruongSpawn = true;
                        TieuDoiTruongNotify = true;
                        TieuDoiTruong.CreateBossNoAttack(19, 190, 360);
                        oldMapTieuDoiTruong = zone.Map.Id;
                        oldZoneTieuDoiTruong = zone.Id;
                        TileMapTieuDoiTruong = zone.Map.TileMap.Name;
                        TieuDoiTruong.CharacterHandler.SetUpInfo();
                        zone.ZoneHandler.AddBoss(TieuDoiTruong);
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Tiểu Đội Trưởng vừa xuất hiện tại " + zone.Map.TileMap.Name));
                        Server.Gi().Logger.Print("[Spawn Small Captain] Map: " + sbRandomMap + " | K: " + sbRandomZoneNum, "blue");
                    }
                }
                else
                {
                    if (So4Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 4 vừa xuất hiện tại " + TileMapSo4));
                        Server.Gi().Logger.PrintColor("[Spawn Number 4] Map: " + oldMapSo4 + " | K: " + oldZoneSo4, "blue");
                    }
                    if (So3Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 3 vừa xuất hiện tại " + TileMapSo3));
                        Server.Gi().Logger.PrintColor("[Spawn Number 3] Map: " + oldMapSo3 + " | K: " + oldZoneSo3, "blue");

                    }
                    if (So1Spawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Số 1 vừa xuất hiện tại " + TileMapSo1));
                        Server.Gi().Logger.PrintColor("[Spawn Number 1] Map: " + oldMapSo1 + " | K: " + oldZoneSo1, "blue");

                    }
                    if (TieuDoiTruongSpawn)
                    {
                        ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Tiểu Đội Trưởng vừa xuất hiện tại " + TileMapTieuDoiTruong));
                        Server.Gi().Logger.PrintColor("[Spawn Small Captain] Map: " + oldMapTieuDoiTruong + " | K: " + oldZoneTieuDoiTruong, "blue");

                    //}
                    //else
                    //{
                    //    ClientManager.gI().SendMessageCharacter(Service.ServerChat("BOSS Số 4 vừa xuất hiện tại " + TileMapSo4));
                    //    Server.gI().Logger.Print("![Init So 4] Map: " + oldMapSo4 + " | K: " + oldZoneSo4);
                    //    ClientManager.gI().SendMessageCharacter(Service.ServerChat("BOSS Số 3 vừa xuất hiện tại " + TileMapSo3));
                    //    Server.gI().Logger.Print("![Init So 3] Map: " + oldMapSo3 + " | K: " + oldZoneSo3);
                    //    ClientManager.gI().SendMessageCharacter(Service.ServerChat("BOSS Số 1 vừa xuất hiện tại " + TileMapSo1));
                    //    Server.gI().Logger.Print("![Init So 1] Map: " + oldMapSo1 + " | K: " + oldZoneSo1);
                    //    ClientManager.gI().SendMessageCharacter(Service.ServerChat("BOSS Tiểu Đội Trưởng vừa xuất hiện tại " + TileMapTieuDoiTruong));
                    //    Server.gI().Logger.Print("![Init So 4] Map: " + oldMapTieuDoiTruong + " | K: " + oldZoneTieuDoiTruong);
                    //}
                }
                }
            }
        }

        
    }
}
