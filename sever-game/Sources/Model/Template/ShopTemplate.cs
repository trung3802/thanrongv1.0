using System.Collections;
using System.Collections.Generic;
using TienKiemV2Remastered.Model.Item;

namespace TienKiemV2Remastered.Model.Template
{
    public class ShopTemplate
    {
        public int Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }
        public List<ItemShop> ItemShops { get; set; }

        public ShopTemplate()
        {
            ItemShops = new List<ItemShop>();
        }
    }
}