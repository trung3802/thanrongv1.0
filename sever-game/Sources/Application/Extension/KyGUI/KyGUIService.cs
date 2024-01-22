using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.Extension.Ký_gửi
{
    public class KyGUIService{
        public static List<string> TabsName = new List<string>{"Trang bị","Phụ kiện", "Hỗ trợ", "Linh tinh", "Hành trang nhân vật"};
        public static List<KyGUIItem> getItemKyGui(byte tab, byte page = 0, sbyte gender = 3){
            List<KyGUIItem> kyGUIItems = new List<KyGUIItem>();
            var item = Cache.Gi().kyGUIItems.Values.Where(i => i.Tab == tab && i.Page == page && !i.isBuy &&(ItemCache.ItemTemplate(i.Id).Gender == gender || ItemCache.ItemTemplate(i.Id).Gender is 3 || gender is 3)).ToList();
            Span<KyGUIItem> listAsSpan = CollectionsMarshal.AsSpan(item);
            for (int i = 0; i < listAsSpan.Length;i++)
            {
                kyGUIItems.Add(listAsSpan[i]);
            }

            return kyGUIItems;
        }
        public static List<KyGUIItem> getItemCanKyGui(Character character, byte tab, byte page = 0)
        {
            List<KyGUIItem> kyGUIItems = new List<KyGUIItem>();
            character.ItemBag.Where(i => i != null || (i.isHaveOption(86) || i.isHaveOption(87)) || ItemCache.ItemTemplate(i.Id).Type is (29 or 31 or 12 or 6 or 27) || ItemCache.ItemTemplate(i.Id).Level is (13 or 14) ).ToList().ForEach(item =>
            {
                kyGUIItems.Add(new KyGUIItem()
                {
                    Id = item.Id,
                    ItemId = item.IndexUI,
                    quantity = item.Quantity,
                    isBuy = false,
                    Cost = 0,
                    Tab = 0,
                    BuyType = 0,
                    IdPlayerSell = character.Id,
                    Options = item.Options,
                    IsUpTop = false,
                    Page = 0,
                }) ;
            });
            var item = Cache.Gi().kyGUIItems.Values.Where(i => i.IdPlayerSell == character.Id).ToList();
            Span<KyGUIItem> listAsSpan = CollectionsMarshal.AsSpan(item);
            for (int i = 0; i < listAsSpan.Length; i++)
            {
                kyGUIItems.Add(listAsSpan[i]);
            }
            return kyGUIItems;
        }
        public static Message OpenShopKiGui(Character character) // 0 = ki gui trang bi || 1 = ki gui vp su kien
        {
            var items = getItemCanKyGui(character, 0);
            var version = int.Parse(character.Player.Session.Version.Replace(".", ""));
            var msg = new Message(-44);
            msg.Writer.WriteByte(2); // type shop (2 == shop ki gui) (true)
            msg.Writer.WriteByte(5); // so tab (true)
            for (int i = 0; i < 5; i++)
            {
                if (i == 4)
                {
                    msg.Writer.WriteUTF(TabsName[i]);
                    msg.Writer.WriteByte(0); // max page  (true)
                    msg.Writer.WriteByte(items.Count); // count item
                    items.ForEach(item =>
                    {
                        msg.Writer.WriteShort(item.Id);
                        msg.Writer.WriteShort(item.ItemId);
                        msg.Writer.WriteInt(item.BuyType == 0 ? item.Cost : -1);
                        msg.Writer.WriteInt(item.BuyType == 1 ? item.Cost : -1);
                        msg.Writer.WriteByte(item.isBuy ? 2 : (item.Cost > 0) ? 1 : 0);
                        if (version >= 222)
                        {
                            msg.Writer.WriteInt(item.quantity);
                        }
                        else
                        {
                            msg.Writer.WriteByte(item.quantity);
                        }
                        msg.Writer.WriteByte(1); // isMe
                        msg.Writer.WriteByte(item.Options.Count);
                        item.Options.ForEach(opt =>
                        {
                            msg.Writer.WriteByte(opt.Id);
                            msg.Writer.WriteShort(opt.Param);
                        });
                        msg.Writer.WriteByte(0);
                        msg.Writer.WriteByte(0);

                    });
                }
                else
                {
                    var temp = getItemKyGui((byte)i, 0, character.InfoChar.Gender);

                    msg.Writer.WriteUTF(TabsName[i]);
                    msg.Writer.WriteByte(1); // max page  (true)
                    msg.Writer.WriteByte(temp.Count); // count item (true)
                    temp.ForEach(item =>
                    {
                        msg.Writer.WriteShort(item.Id); // id item
                        msg.Writer.WriteShort(item.ItemId);//count
                        msg.Writer.WriteInt(item.BuyType == 0 ? item.Cost : -1); // vàng
                        msg.Writer.WriteInt(item.BuyType == 1 ? item.Cost : -1); // ngọc
                        msg.Writer.WriteByte(0);
                        if (version >= 222)
                        {
                            msg.Writer.WriteInt(1);
                        }
                        else
                        {
                            msg.Writer.WriteByte(1);
                        }
                        msg.Writer.WriteByte(item.IdPlayerSell == character.Id ? 1 : 0); // isMe
                        msg.Writer.WriteByte(item.Options.Count);
                        item.Options.ForEach(opt =>
                        {
                            msg.Writer.WriteByte(opt.Id);
                            msg.Writer.WriteShort(opt.Param);
                        });
                        msg.Writer.WriteByte(0);
                        msg.Writer.WriteByte(0);
                    });
                }
            }
            return msg;
        }
    }
}