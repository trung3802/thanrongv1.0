namespace TienKiemV2Remastered.Model.Template
{
    public class InputBox
    {
        public string Name { get; set; }
        public int Type { get; set; }

        public InputBox()
        {
            Name = "";
            Type = 1;
        }
    }
}