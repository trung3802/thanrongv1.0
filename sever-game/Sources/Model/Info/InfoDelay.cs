using TienKiemV2Remastered.Application.IO;

namespace TienKiemV2Remastered.Model.Info
{
    public class InfoDelay
    {
        public long UsePea { get; set; }
        public long ChangeZone { get; set; }
        public long ChangeFlag { get; set; }
        public long ChatTheGioi { get; set; }
        public long TeleportToPlayer { get; set; }
        public long Trade { get; set; }
        public long NewLogin { get; set; }
        public long timeDelayXKame { get; set; }
        public long UseGiftCode { get; set; }

        // update auto luyen tap
        public long AutoPlay { get; set; }
        // Giáp tập luyện
        public long GiapLuyenTap { get; set; }
        public long TrainGiapLuyenTap { get; set; }
        // Sử dụng bông tai
        public long BongTaiPorata { get; set; }
        // Delay nhận ngọc
        public long GetGem { get; set; }
        // Save Data
        public long SaveInvData { get; set; }
        public long SaveData { get; set; }
        public bool NeedToSaveBag { get; set; }
        public bool NeedToSaveBody { get; set; }
        public bool NeedToSaveBox { get; set; }
        public bool NeedToSaveLucky { get; set; }
        // Cải trang đẹp
        public long BeautifulTalk { get; set; }
        public long DelayHupHp5s { get; set; }
        public long DelayHoaDa { get; set; }
        public long DelayHoaBang { get; set; }

        // Delay skill
        public long DelaySkillZone { get; set; }
        public long InvAction { get; set; }
        public long Delay180Giay { get; set; }
        public long Delay10Giay { get; set; }
        public bool DoiMayChu { get; set; }
        public bool StartLogout { get; set; }
        public long Delay30Phut { get; set; }
        public long Delay1Phut { get; set; }
        public long Delay300Giay { get; set; }
        public bool IsSavingInventory { get; set; }
        public long DelayBossBangHoi { get; set; }
        public InfoDelay()
        {
            DelayBossBangHoi = 5000 + ServerUtils.CurrentTimeMillis(); 
            UsePea = -1;
            ChangeZone = 10000 + ServerUtils.CurrentTimeMillis();
            ChangeFlag = -1;
            Delay300Giay = 300000 + ServerUtils.CurrentTimeMillis();
            ChatTheGioi = -1;
            TeleportToPlayer = -1;
            Trade = -1;
            AutoPlay = 60000 + ServerUtils.CurrentTimeMillis();
            GiapLuyenTap = 60000 + ServerUtils.CurrentTimeMillis();
            TrainGiapLuyenTap = 60000 + ServerUtils.CurrentTimeMillis();
            BongTaiPorata = 5000 + ServerUtils.CurrentTimeMillis();
            GetGem = ServerUtils.CurrentTimeMillis();
            SaveInvData = 2000 + ServerUtils.CurrentTimeMillis();
            SaveData = 300000 + ServerUtils.CurrentTimeMillis();
            Delay10Giay = 10000 + ServerUtils.CurrentTimeMillis();
            UseGiftCode = 60000 + ServerUtils.CurrentTimeMillis();
            DelayHupHp5s = 5000 + ServerUtils.CurrentTimeMillis();
            BeautifulTalk = 10000 + ServerUtils.CurrentTimeMillis();
            Delay30Phut = 1800000 + ServerUtils.CurrentTimeMillis();
            Delay1Phut = 60000 + ServerUtils.CurrentTimeMillis();
            DelaySkillZone = 1000 + ServerUtils.CurrentTimeMillis();
            InvAction = ServerUtils.CurrentTimeMillis();
            Delay180Giay = 190000 + ServerUtils.CurrentTimeMillis();
            DelayHoaDa = 15000 + ServerUtils.CurrentTimeMillis();
            DelayHoaBang = 15000 + ServerUtils.CurrentTimeMillis();
            timeDelayXKame = 60000 + ServerUtils.CurrentTimeMillis();
            NeedToSaveBag = false;
            NeedToSaveBody = false;
            NeedToSaveBox = false;
            NeedToSaveLucky = false;

            DoiMayChu = false;
            StartLogout = false;
            IsSavingInventory = false;
        }
    }
}