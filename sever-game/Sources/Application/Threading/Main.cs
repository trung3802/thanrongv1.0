using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;

namespace TienKiemV2Remastered.Application.Threading
{
    public class Maintenance
    {
        private static Maintenance _instance;
        public int TimeCount { get; set; }
        public bool IsStart { get; set; }

        private Maintenance()
        {
            TimeCount = 3;
            IsStart = false;
        }

        public static Maintenance Gi()
        {
            return _instance ??= new Maintenance();
        }

        public void Start(int time)
        {
            TimeCount = time;
            IsStart = true;
            var task = new Task(Action);
            task.Start();
        }

        private async void Action()
        {
            while (IsStart)
            {
                var text = string.Format(TextServer.gI().MAINTENANCE, TimeCount);
                //ClientManager.Gi().SendMessageCharacter(Service.WorldChat(null, text, 0));
                ClientManager.Gi().SendMessageCharacter(Service.ServerChat(text));
                ClientManager.Gi().SendMessageCharacter(Service.ServerMessage(text));
                TimeCount--;
                if (TimeCount <= 0) IsStart = false;
                await Task.Delay(60000);
            }

            Server.Gi().StopServer();
        }
    }
}