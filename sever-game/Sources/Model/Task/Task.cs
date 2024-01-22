using System.Collections.Generic;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Task
{
    public class Task  : TaskBase
    {
        public int Index { get; set; }
        public int Max { get; set; }
        public List<string> Details { get; set; }
        public List<string> Names { get; set; }
        public short Count { get; set; }

    }
}