using System;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Option
{
    public class OptionItem : OptionBase
    {
        public OptionItem()
        {
            
        }

        public override object Clone()
        {
            return new OptionItem()
            {
                Id = Id,
                Param = Param
            };
        }
     
    }
}