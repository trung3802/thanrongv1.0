using TienKiemV2Remastered.Application.IO;

namespace TienKiemV2Remastered.Model.Info
{
    public class InfoDelayBoss
    {
        public long LeaveDead { get; set; }
        public long TTNL { get; set; }
        public long AutoMove { get; set; }
        public long AutoChat { get; set; }

        public long AutoChangeMap { get; set; }
        public long AutoRotHopQua { get; set; }
        public long AutoPlusHP { get; set; }
        public long AutoSpawnXenCon { get; set; }
        public long AutoDie { get; set; }
        public long ChangeMode { get; set; }
        public long DelayRemove { get; set; }
        public InfoDelayBoss()
        {
            LeaveDead = -1;
            AutoMove = ServerUtils.CurrentTimeMillis() + 1500;
            AutoChat = ServerUtils.CurrentTimeMillis() + 5000;
            TTNL = ServerUtils.CurrentTimeMillis() + 1500;
            ChangeMode = ServerUtils.CurrentTimeMillis() + 10000;
            AutoChangeMap = ServerUtils.CurrentTimeMillis() + 500000;
            AutoRotHopQua = ServerUtils.CurrentTimeMillis() + 15000;
            AutoPlusHP = ServerUtils.CurrentTimeMillis() + 5000;
            AutoSpawnXenCon = ServerUtils.CurrentTimeMillis() + 15000;
            AutoDie = ServerUtils.CurrentTimeMillis() + 180000;
            DelayRemove = ServerUtils.CurrentTimeMillis() + 600000;
        }
    }
}