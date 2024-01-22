using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Extensions.Configuration;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Logging;
using TienKiemV2Remastered.Model.BangXepHang;
using InitData = TienKiemV2Remastered.DatabaseManager.InitData;
using Task = System.Threading.Tasks.Task;
using TienKiemV2Remastered.Application;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.ChampionShip;
using TienKiemV2Remastered.Application.Extension.Yardat;
using TienKiemV2Remastered.Application.Extension.Event;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu2Gio;

using TienKiemV2Remastered.Application.Extension.Namecball;
using System.Text;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Extension.Bosses.BigBoss;
using TienKiemV2Remastered.Application.Extension.Ký_gửi;
using TienKiemV2Remastered.Application.Extension.NamecballWar;
using System.Runtime.InteropServices;

namespace TienKiemV2Remastered.Application.Threading
{
    public class Server
    {
        private static Server Instance { get; set; } = null;
        public static readonly object SQLLOCK = new object();
        public static readonly object IPLOCK = new object();
        private IPAddress IpAddress { get; set; }
        private TcpListener Listener { get; set; }
        public bool IsRunning { get; set; }
        public bool IsSaving { get; set; }
        private Thread RunServer { get; set; }
        public IServerLogger Logger { get; set; }
        public IConfiguration Config { get; set; }

        private DatabaseManager.InitData _initData;
        private Thread _serverRun;
        private ClanRunTime _clanRun;
        private MagicTreeRunTime _magicTreeRun;
        private BossRunTime _bossRun;

        public ABXH BangXepHang;

        public ABoss ABoss;
        public long DelayLogin { get; set; }

        public long StartServerTime { get; set; }
        public int CountLogin { get; set; }
        public bool LockCloneGiaoDich { get; set; }

        public readonly string DROP_KEY = "dropsuperdrop";

        public static Server Gi()
        {
            return Instance ??= new Server();
        }

        public Server()
        {
            IpAddress = IPAddress.Parse(DatabaseManager.ConfigManager.gI().ServerHost);
            Listener = new TcpListener(IpAddress, DatabaseManager.ConfigManager.gI().ServerPort);
            RunServer = null;
        }
        public Task Runtime { get; set; }
        public void StartRunTime()
        {
            Runtime = new Task(AnotherRunTime);
            Runtime.Start();
        }
        public async void AnotherRunTime()
        {
            _serverRun.Start();
            _clanRun.StartClan();
            _magicTreeRun.StartMagicTree();

            _bossRun.StartBossRunTime();

            
            Yardat.Init();
            BangXepHang.Start();
            ThiefBear.gI().Refesh();
            while (IsRunning)
            {
                var timeserver = ServerUtils.CurrentTimeMillis();
                var currentNow = ServerUtils.TimeNow();
                Hirudegarn.gI().InitHirudegarn(currentNow.Hour);
                ABoss.gI().AutoBoss(timeserver);
                NamecballWar.gI().Update(currentNow);
                //   EventRuntime.Runtime(timeserver);
                ChampionShip.gI().InitDaiHoiVoThuat(timeserver);
                BlackBallRuntime.CurrentRunTime();
                Mabu12h.gI().AutoInit(timeserver);
                Mabu2h.gI().AutoInit(timeserver);
                Init.AutoInit(timeserver);
                ThiefBear.gI().Init(timeserver);
                await Task.Delay(1000);
            }
        }

        public void InitServerRuntime()
        {
            _serverRun.Start();
            _clanRun.StartClan();
            _magicTreeRun.StartMagicTree();
            Yardat.Init();
            BangXepHang.Start();
            new Thread(new ThreadStart(() =>
                {
                    while (IsRunning)
                    {
                        var timeserver = ServerUtils.CurrentTimeMillis();
                        var currentNow = ServerUtils.TimeNow();
                        Hirudegarn.gI().InitHirudegarn(currentNow.Hour);
                        ABoss.gI().AutoBoss(timeserver);
                        NamecballWar.gI().Update(currentNow);
                        //   EventRuntime.Runtime(timeserver);
                        ChampionShip.gI().InitDaiHoiVoThuat(timeserver);
                        BlackBallRuntime.CurrentRunTime();
                        Mabu12h.gI().AutoInit(timeserver);
                        Mabu2h.gI().AutoInit(timeserver);
                        Init.AutoInit(timeserver);
                        ThiefBear.gI().Init(timeserver);
                        Thread.Sleep(1000);
                    }
                })).Start();
            ThiefBear.gI().Refesh();

        }
        private void InitServer()
        {
            _initData = new DatabaseManager.InitData();
            if (_clanRun == null)
            {
                _clanRun = new ClanRunTime();
            }

            if (_magicTreeRun == null)
            {
                _magicTreeRun = new MagicTreeRunTime();
            }


            if (BangXepHang == null)
            {
                BangXepHang = new ABXH();
            }

            if (ABoss == null)
            {
                ABoss = new ABoss();
            }

            if (_bossRun == null)
            {
                _bossRun = new BossRunTime();
            }
        }
       
        public void StartServer(bool running, IServerLogger logger, IConfiguration config, bool isRestart)
        {
            Logger = logger;
            Config = config;
            IsRunning = running;
           // Socket_Client.CreateSocket();
            DelayLogin = ServerUtils.CurrentTimeMillis();
            StartServerTime = ServerUtils.CurrentTimeMillis();
            LockCloneGiaoDich = true;
            CountLogin = 0;
            //Console.WriteLine("color a");
            //List<List<long>> Object = new List<List<long>>();
            //for (int i = 0; i <10; i++)
            //{
            //    List<long> Object2 = new List<long>();
            //   // var clanMember = ClientManager.Gi().GetCharacter(clan.Thành_viên[i].Id);
            //    Object2.Add(10000 * i);
            //    Object2.Add(1000*i);
            //    Object.Add(Object2);
            //}
            //Object.Sort((g1, g2) => g2[1].CompareTo(g1[1]));
            //foreach(var item in Object)
            //{
            //    Console.WriteLine(" " + item[1]);
            //}
                Console.ForegroundColor = ConsoleColor.DarkRed;
            
           
            Console.ResetColor();
            if (!IsRunning) return;
            InitServer();




            Logger.Print($"LOADING DATA FROM DATABASE [STATUS: HOAN TAT]");
            Logger.Print($"START SEVER ON PORT : [{DatabaseManager.ConfigManager.gI().ServerPort}]");
            Listener.Start();

            //new Thread(new ThreadStart(AutoBossHandler.AFide)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AColer)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ACell)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ATieuDoiSatThu)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AChilled)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid1)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid2)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid3)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AOngGiaNoel)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ADanEmFide)).Start();
            // new Thread(new ThreadStart(AutoBossHandler.ABoss)).Start();
            //_bossRun.SpawnBroly();
            //Start Magic tree
            //new Thread(new ThreadStart(AutoBossHandler.ABlackGoku)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ABroly)).Start();

            _serverRun = new Thread(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                       // lock (IPLOCK)
                        //{
                        var client = Listener.AcceptTcpClient();
                      //  _TpcClient.Add(client);
                      //  var clientMarshal = CollectionsMarshal.AsSpan(_TpcClient);
                      //  for (int i = 0; i < clientMarshal.Length; i++)
                      //  {
                        //    client = _TpcClient[i];
                            if (!client.Connected) continue;
                            var ipv4 = client.Client.RemoteEndPoint?.ToString()?.Split(':')[0];
                            var session = new Session_ME(client, ipv4);

                            session.StartSession();
                            ClientManager.Gi().Add(session);
                            Logger.Info($"Accpet Session: {session.Id} | {ipv4} Successful | size {client.SendTimeout} ms");
                            //}
                        //}
                        //Logger.Info("Accecpt ClientMarshal Length: "+clientMarshal.Length);
                        //_TpcClient.Clear();
                    }
                    catch (Exception)
                    {
                        IsRunning = false;
                    }
                }
                SaveData();
                IsSaving = false;
                Task.Run(() =>
                {
                    while (!MagicTreeRunTime.IsStop || !ClanRunTime.IsStop || !ABoss.gI().IsStop || !BossRunTime.IsStop)
                    {
                        //Ignore
                    }
                    Logger.Print("Server Shutdown Success!");
                });
            });
            /// _serverRun.Start();
            // _clanRun.StartClan();
            // _magicTreeRun.StartMagicTree();
            // _bossRun.StartBossRunTime();

            StartRunTime();

            //InitServerRuntime();
            

            //AutoBossHandler.ABoss();
            //ABoss.gI().StartBossRunTime();
            // ChampionShip.gI().InitDaiHoiVoThuat();
            //TienKiemV2Remastered.Sources.Application.Extension.Chẵn_Lẻ_Momo.HandlerChanLe.Update();
            //TienKiemV2Remastered.Sources.Application.Extension.Ngọc_Zồng_Bi_Đen.Init.gI().InitBlackBall();
            //TienKiemV2Remastered.Sources.Application.Extension.Ngọc_Zồng_Bi_Đen.BlackBallHandler.gI().Runtime();
            //TienKiemV2Remastered.Sources.Application.Extension.Bosses.Mabư_12_giờ.Mabu12h.gI().AutoInit();
            //TienKiemV2Remastered.Sources.Application.Extension.Bosses.Mabư_2_giờ.Mabu2h.gI().AutoInit();
            // Yardat.Init();
            //TienKiemV2Remastered.Sources.Application.Extension.BlackballWar.BlackBallRuntime.CurrentRunTime();
            //EventRuntime.InitSuKienRuntime();
            //TienKiemV2Remastered.Sources.Application.Extension.Namecball.Init.AutoInit();
            //TienKiemV2Remastered.Sources.Application.Extension.Bosses.BigBoss.ThiefBear.gI().Init();
            // BangXepHang.Start();
        }

        public void StopServer()
        {
            Listener.Stop();
            IsSaving = true;
        }

        private void SaveData()
        {
            ClientManager.Gi().Clear();
            Logger.Print("Save DATA Player Server Sucess!!!");
            KyGUIMySQL.UpdateAllItem();
            Logger.Print("Save DATA ITEM KY GUI Server Sucess!!!");
        }

        public void RestartServer()
        {

            StopServer();
            Task.Run(() =>
            {
                while (IsSaving || !MagicTreeRunTime.IsStop || !ClanRunTime.IsStop)
                {
                    continue;
                }
                StartServer(true, Logger, Config, true);
                Logger.Print("Server Restart Success!");
            });
        }
    }
}