namespace TienKiemV2Remastered.Model.Task
{
    public class TaskInfo
    {
       public short Id { get; set; } 
       public sbyte Index { get; set; }     
       public short Count { get; set; }
        public long Time { get; set; }

       public TaskInfo()
       {
           Id = 1;
           Index = 0;
           Count = 0;
            Time = 0;
       }
    }
}