using System.Collections.Generic;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Template
{
    public class TaskTemplate : TaskBase
    {
        public string Name { get; set; }
        public string Detail { get; set; }

        public TaskTemplate() : base()
        {
        }
    }
}