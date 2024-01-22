using System.Collections.Generic;
using TienKiemV2Remastered.Model.Template;

namespace TienKiemV2Remastered.Model
{
    public class NClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SkillTemplate> SkillTemplates { get; set; }

        public NClass()
        {
            
        }
    }
}