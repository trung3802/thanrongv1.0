using System;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Item
{
    public class Item : ItemBase, IDisposable
    {
        public int IndexUI { get; set; }
        public int SaleCoin { get; set; } = 1;
        public long BuyPotential { get; set; }

        public Item() : base()
        {
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

       
    }
}