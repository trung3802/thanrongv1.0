using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Model.Option;
using TienKiemV2Remastered.Model.Item;

namespace TienKiemV2Remastered.Model.ModelBase
{
    public class ItemBase
    {
        public short Id { get; set; }
        public int Vang
        { get; set; } = 0;
        public int Ngoc { get; set; } = 0;
        public int Quantity { get; set; } = 1;
        public string Reason { get; set; }
        public List<OptionItem> Options { get; set; }

        public ItemBase()
        {
            Reason = "";
            Options = new List<OptionItem>();
        }

        public int GetParamOption(int id)
        {
            var option = Options.FirstOrDefault(op => op.Id == id);
            return option != null ? option.Param : 0;
        }
        public bool isHaveOption(int id)
        {
            var option = Options.FirstOrDefault(op => op.Id == id);
            return option != null ? true : false;
        }
    }
}