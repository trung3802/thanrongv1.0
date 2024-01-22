using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Item;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.ModelBase;
using TienKiemV2Remastered.Model.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sources.Model.Clan
{
    public class ClanBox
    {
        public List<Item> ItemBoxClan { get; set; }
        public ClanBox()
        {
            ItemBoxClan = new List<Item>(2);
        }
        public int BoxLength()
        {
            return 20;
        }

    }
}
