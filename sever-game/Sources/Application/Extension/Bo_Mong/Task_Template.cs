using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Bo_Mong
{
    public class BoMong_Task_Template
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int GemCollect { get; set; }
        public long Count { get; set; }
    }
}
