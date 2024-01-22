namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class ThoiMien
    {
        public bool IsThoiMien { get; set; }
        public long Time { get; set; }
        public long TimePercent { get; set; }
        public int Percent { get; set; }

        public ThoiMien()
        {
            IsThoiMien = false;
            Time = -1;
            TimePercent = -1;
            Percent = 0;
        }

    }
}