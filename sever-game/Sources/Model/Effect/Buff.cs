namespace TienKiemV2Remastered.Model.Effect
{
    public class Buff
    {
        public int Value { get; set; }
        public long Time { get; set; }

        public Buff()
        {
            Value = 0;
            Time = -1;
        }
    }
}