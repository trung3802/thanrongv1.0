using System;
using System.Collections.Generic;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Application.Extension.Ký_gửi
{
    public class KyGUIItem{
            public short Id { get; set; }
            public int ItemId { get; set; }
            public int quantity { get; set; }
            public int IdPlayerSell { get; set; }
            public Boolean isBuy { get; set; } // check xem đã bán hay chưa 
            public int Cost { get; set; } // gold, gem, ruby
            public byte BuyType { get; set; }
            public List<OptionItem> Options { get; set; }
            public bool IsUpTop{get;set;}
            public int Page { get; set; }
            public int Tab { get; set; }
            public KyGUIItem(){
                
            }
            
    }
}