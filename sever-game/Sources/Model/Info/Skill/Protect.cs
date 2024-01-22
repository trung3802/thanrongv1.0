namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class Protect
    {
        public bool IsProtect { get; set; }
        public long Time { get; set; }

        public Protect()
        {
            IsProtect = false;
            Time = -1;
        }
    }
}