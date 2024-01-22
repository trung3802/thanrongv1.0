using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using Org.BouncyCastle.Math.Field;
using TienKiemV2Remastered.Source.Application.Handlers;

namespace TienKiemV2Remastered.Application.Threading
{
    public class MagicTreeRunTime
    {
        public static bool IsStop = false;
        public static int RunTimeUpdate1 = -1;
        public static bool IsRunTimeSave = true;

        public MagicTreeRunTime()
        {
            
        }
        public Task Runtime { get; set; }
        public void StartMagicTree()
        {
            Runtime = new Task(MagicTree);
            Runtime.Start();
        }
        public async void MagicTree()
        {
            
            while (Server.Gi().IsRunning)
                {
                ChanLeHandler.StartNewGame();
                var now = ServerUtils.TimeNow();

                    if (now.Hour == 1 && now.Minute == 0 && IsRunTimeSave) 
                    {
                        IsRunTimeSave = false;
                        Parallel.ForEach(MagicTreeManager.Entrys.Values.ToList(), tree => tree.MagicTreeHandler.Update(0));
                    }
                    else if(RunTimeUpdate1 != now.Minute)
                    {
                        RunTimeUpdate1 = now.Minute;
                        Parallel.ForEach(MagicTreeManager.Entrys.Values.ToList(), tree => tree.MagicTreeHandler.Update(1));
                        if (now.Hour != 1 && !IsRunTimeSave) IsRunTimeSave = true;
                    }
                    await Task.Delay(1000);
                }
                MagicTreeManager.Entrys.Values.ToList().ForEach(tree => tree.MagicTreeHandler.Flush());
                Server.Gi().Logger.Print("MagicTree Manager is close...","red");
                IsStop = true;
        }
    }
}