using System;
using Microsoft.Extensions.Configuration;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Logging;
using TienKiemV2Remastered.DatabaseManager;

namespace TienKiemV2Remastered.Application.Main
{
    public partial class DragonBall
    {
        private static bool keepRunning = true;
        public static bool findErr = false;
        static void Main(string[] args)
        {

            IServerLogger logger = new ServerLogger();
              var configBuilder = new ConfigurationBuilder().SetBasePath(ServerUtils.ProjectDir("")).AddJsonFile("config.json");
            var configurationRoot = configBuilder.Build();
            DatabaseManager.ConfigManager.CreateManager(configurationRoot);

            Server.Gi().StartServer(true, logger, configurationRoot, false);

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                e.Cancel = true;
                DragonBall.keepRunning = false;
            };
            while (keepRunning)
            {

                var type = Console.ReadLine();
                if (type != null && type.Contains("baotri"))
                {
                    var time = 1;
                    try
                    {
                        time = Int32.Parse(type.Replace("baotri", ""));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    if (Maintenance.Gi().IsStart)
                    {
                        logger.Print($"Server is Maintained, time Left: {Maintenance.Gi().TimeCount} minutes...");
                    }
                    else
                    {
                        Maintenance.Gi().Start(time);
                        logger.Print($"Server will be under Maintenance Later: {time} minutes...");
                    }

                }
                else if (type == "khoidong")
                {
                    logger.Print("Server restarting...");
                    configBuilder = new ConfigurationBuilder().SetBasePath(ServerUtils.ProjectDir(""))
                        .AddJsonFile("config.json");
                    configurationRoot = configBuilder.Build();
                    DatabaseManager.ConfigManager.CreateManager(configurationRoot);
                    Server.Gi().RestartServer();
                }
                else if (type == "tat")
                {
                    logger.Print("Server stopping...");
                    Server.Gi().StopServer();
                    break;
                }else if (type == "timloi")
                {
                    findErr = !findErr;
                    logger.Print("Find Error: " + findErr);
                }
                else
                {
                    Console.WriteLine("Not Found Action...");
                }
            }
            logger.Print("Server stopping...");
            Server.Gi().StopServer();

        }
    }
}