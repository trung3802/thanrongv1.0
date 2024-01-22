using System;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Option
{
    public class OptionSkill : OptionBase
    {
        public override object Clone()
        {
            return new OptionSkill()
            {
                Id = Id,
                Param = Param
            };
        }
    }
}