
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Item;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.Extension.Ký_gửi
{
    public class KyGUIHandler{
        public static void Sort(int target)
        {
            //Cache.Gi().kyGUIItems.Values.Where(i => i.ItemId > itemId).ToList().ForEach(item =>
            //{
            //    item.ItemId--;
            //});
            Span<KyGUIItem> kyGUIItems = CollectionsMarshal.AsSpan(Cache.Gi().kyGUIItems.Values.Where(i => i.ItemId > target).ToList());
            foreach(var item in kyGUIItems)
            {
                item.ItemId--;
            }
        }
        public static int GetMaximumId()
        {
            //List<int> id = new List<int>();
            //Span<KyGUIItem> kyGUIItems = CollectionsMarshal.AsSpan(Cache.Gi().kyGUIItems.Values.ToList());
            //for (int i = 0; i < kyGUIItems.Length; i++)
            //{
            //    id.Add(kyGUIItems[i].ItemId);

            //}
            //return id.Max();
            try
            {
                return Cache.Gi().kyGUIItems.Keys.Max();
            }
            catch
            {
                return 0;
            }
        }
        public static void BuyItem(Character character, int Id)
        {
            var item = Cache.Gi().kyGUIItems[Id];
            if (item.IdPlayerSell.Equals(character.Id))
            {
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không thể mua vật phẩm của chính mình"));
                return;
            }
            if (item.isBuy)
            {
                character.CharacterHandler.SendMessage(Service.ServerMessage("Vật phẩm đã được mua"));
                return;
            }
            item.isBuy = true;
            var itemData = ItemCache.GetItemDefault(item.Id, item.Options, item.quantity);
            character.CharacterHandler.AddItemToBag(item.quantity > 1 ? true : false, itemData);
            switch (item.BuyType)
            {
                case 0:
                   // character.MineGold(item.Cost);
                    character.MineGold(item.Cost);
                    break;
                case 1:
                    character.MineDiamond(item.Cost);
                    break;
            }
          //  async void Action()
         //   {
          //      await Task.Delay(50);
               // Cache.Gi().kyGUIItems.Remove(Id);
                character.CharacterHandler.SendMessage(Service.SendBag2(character));
         //       await Task.Delay(50);
                character.CharacterHandler.SendMessage(Service.BuyItem(character));
           //     await Task.Delay(200);
                character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(itemData.Id).Name));
            //}
            // Cache.Gi().kyGUIItems.Remove(Id);
            //var task = new Task(Action);
            //task.Start();
        }
        public static void KyGui(Character character, int Id, int money, int moneyType, int uquantity)
        {
            switch (moneyType)
            {
                case 0:
                case 1:
                    if (money > int.MaxValue || money < int.MinValue)
                    {
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Giá tiền không hợp lệ"));
                        return;
                    }
                    break;
              
            }
            var item = character.CharacterHandler.GetItemBagByIndex(Id);
            var tab = GetIndexTab(ItemCache.ItemTemplate(item.Id).Type);
            Cache.Gi().kyGUIItems.Add(GetMaximumId() + 1, new KyGUIItem()
            {
                Id = item.Id,
                ItemId = GetMaximumId() + 1,
                quantity = uquantity,
                isBuy = false,
                Cost = money,
                Tab = tab,
                BuyType = (byte)moneyType,
                IdPlayerSell = character.Id,
                Options = item.Options,
                IsUpTop = false,
                Page = 0,
            });
            
          // async void Action()
           // {
            
                character.CharacterHandler.RemoveItemBagByIndex(item.IndexUI, uquantity, false);
              
                character.CharacterHandler.SendMessage(Service.SendBag2(character));
               
                character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                character.CharacterHandler.SendMessage(Service.ServerMessage("Đăng bán thành công"));
            //}
            //var task = new Task(Action);
            //task.Start();
        }
        public static void ClaimMoneyOrDeleteItem(Character character,int action, int Id)
        {
            switch (action)
            {              
                case 1:
                    {
                        var itemRemove = Cache.Gi().kyGUIItems[Id];
                        if (itemRemove.isBuy)
                        { // vat pham chua duoc mua
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Vật phẩm không hợp lệ !"));
                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                            return;
                        }
                        var itemData = ItemCache.GetItemDefault(itemRemove.Id, itemRemove.Options, itemRemove.quantity);
                      //  async void Action()
                      //  {
                       //     await Task.Delay(50);
                            Cache.Gi().kyGUIItems.Remove(Id);
                            character.CharacterHandler.AddItemToBag(itemRemove.quantity > 1 ? true : false, itemData);
                         //   await Task.Delay(50);
                            character.CharacterHandler.SendMessage(Service.SendBag2(character));
                          //  await Task.Delay(200);
                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Hủy ký gửi thành công"));
                        //}
                        //var task = new Task(Action);
                        //task.Start();
                    }
                    break;
                case 2:
                    {
                        var itemGet = Cache.Gi().kyGUIItems[Id];
                        if (!itemGet.isBuy) { // vat pham chua duoc mua
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Vật phẩm không hợp lệ !"));
                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                            return;
                        }
                        switch (itemGet.BuyType)
                        {
                            case 0:
                                character.PlusGold(itemGet.Cost);
                                break;
                            case 1:
                                character.PlusDiamondLock(itemGet.Cost);
                                break;
                        }
                        //async void Action()
                        //{
                          //  await Task.Delay(50);
                            Cache.Gi().kyGUIItems.Remove(Id);
                            //await Task.Delay(50);
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            //await Task.Delay(200);
                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + itemGet.Cost + (itemGet.BuyType == 0 ? " vàng" : " hồng ngọc")));
                        //}
                        //var task = new Task(Action);
                        //task.Start();
                    }
                    break;
             
            }
        }
        public static int GetIndexTab(int typeItem)
        {
            switch (typeItem)
            {
                case >= 0 and <= 1: return 0;
                case >= 2 and <= 4: return 1;
                case 29 or 31: return 2; 
                case 12 or 6: return 3;
            }
            return -1;
        }

      /* public static void ClaimMoney(Character character,int id){
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        var item = Cache.Gi().kyGUIItems.Values.FirstOrDefault(i => i.Id == id);
                        switch (item.GoldSell)
                        {
                            case > 0:
                                var goldCollect = item.GoldSell - item.GoldSell * 5 / 100;
                                character.PlusGold(goldCollect);
                                if (Cache.Gi().kyGUIItems.Remove(item.Id, out item))
                                {
                                    character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã nhận được {ServerUtils.GetMoney(goldCollect)} vàng"));
                                    character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));

                                }
                                break;
                            case < 0:
                                var gemCollect = item.GoldSell - item.GoldSell * 5 / 100;
                                character.PlusDiamond(gemCollect);
                                if (Cache.Gi().kyGUIItems.Remove(item.Id, out item))
                                {
                                    character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã nhận được {gemCollect} ngọc"));
                                    character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));

                                }
                                break;
                        }
                       break;
                    }
                case 1:
                    {
                        var item = Cache.Gi().kyGUIItems2.Values.FirstOrDefault(i => i.Id == id);
                        switch (item.GoldSell)
                        {
                            case > 0:
                                var goldCollect = item.GoldSell - item.GoldSell * 5 / 100;
                                character.PlusGold(goldCollect);
                                if (Cache.Gi().kyGUIItems2.Remove(item.Id, out item))
                                {
                                    character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã nhận được {ServerUtils.GetMoney(goldCollect)} vàng"));
                                    character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));

                                }
                                break;
                            case < 0:
                                var gemCollect = item.GoldSell - item.GoldSell * 5 / 100;
                                character.PlusDiamond(gemCollect);
                                if (Cache.Gi().kyGUIItems2.Remove(item.Id, out item))
                                {
                                    character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã nhận được {gemCollect} ngọc"));
                                    character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));

                                }
                                break;
                        }

                        break;
                    }
            }
       }
       public static void DelItem(Character character, int id){
           
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        var item = Cache.Gi().kyGUIItems.Values.FirstOrDefault(i => i.Id == id);
                        var itemClone = ItemHandler.Clone(ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options));
                        Cache.Gi().kyGUIItems.Remove(item.Id, out item);
                        KyGUIMySQL.DelItem(item.Id);
                        character.CharacterHandler.AddItemToBag(false, ItemCache.GetItemDefault(itemClone.Id));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã hủy bán vật phẩm thành công"));
                        character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                        break;
                    }
                case 1:
                    {
                        var item = Cache.Gi().kyGUIItems2.Values.FirstOrDefault(i => i.Id == id);
                        var itemClone = ItemHandler.Clone(ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options));
                        Cache.Gi().kyGUIItems2.Remove(item.Id, out item);
                        KyGUIMySQL.DelItem2(item.Id);
                        character.CharacterHandler.AddItemToBag(true, ItemCache.GetItemDefault(itemClone.Id));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã hủy bán vật phẩm thành công"));
                        character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                    }
                    break;
            }
            
       }
       public static void BuyItem(Character character, int id, sbyte moneyType, int money ){
            switch (moneyType)
            {
                case 0:
                    switch (money > character.InfoChar.Gold)
                    {
                        case true:
                            character.CharacterHandler.SendServerMessage("Không đủ vàng");
                            break;
                        case false:
                            {
                                switch (character.TypeMenu)
                                {
                                    case 0:
                                        {
                                            var item = Cache.Gi().kyGUIItems.Values.FirstOrDefault(i => i.Id == id);
                                            var itemClone = ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options);
                                            KyGUIMySQL.DelItem(item.Id);
                                            Cache.Gi().kyGUIItems.Remove(item.Id, out item);
                                            character.CharacterHandler.AddItemToBag(false, itemClone);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.MineGold(money);
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(itemClone.Id).Name));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                                            break;
                                        }
                                    case 1:
                                        {
                                            var item = Cache.Gi().kyGUIItems2.Values.FirstOrDefault(i => i.Id == id);
                                            var itemClone =ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options);
                                            KyGUIMySQL.DelItem2(item.Id);
                                            Cache.Gi().kyGUIItems2.Remove(item.Id, out item);
                                            character.CharacterHandler.AddItemToBag(false, itemClone);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.MineGold(money);
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(itemClone.Id).Name));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                                            break;
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case 1:
                    switch(money > character.AllDiamond())
                    {
                        
                        case true:
                            character.CharacterHandler.SendServerMessage("Không đủ ngọc");
                            break;
                        case false:
                            {
                                switch (character.TypeMenu)
                                {
                                    case 0:
                                        {
                                            var item = Cache.Gi().kyGUIItems.Values.FirstOrDefault(i => i.Id == id);
                                            var itemClone = ItemHandler.Clone(ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options));
                                            KyGUIMySQL.DelItem(item.Id);
                                            Cache.Gi().kyGUIItems.Remove(item.Id, out item);
                                            character.CharacterHandler.AddItemToBag(false, itemClone);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.MineDiamond(money);
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(itemClone.Id).Name));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                                            break;
                                        }
                                    case 1:
                                        {
                                            var item = Cache.Gi().kyGUIItems2.Values.FirstOrDefault(i => i.Id == id);
                                            var itemClone = ItemHandler.Clone(ItemCache.GetItemKyGUI(item.ItemId, item.quantity, item.Options));
                                            KyGUIMySQL.DelItem2(item.Id);
                                            Cache.Gi().kyGUIItems2.Remove(item.Id, out item);
                                            character.CharacterHandler.AddItemToBag(false, itemClone);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.MineDiamond(money);
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(itemClone.Id).Name));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                                            break;
                                        }
                                }
                            }
                            break;
                    }
                    break;
            }       
       }
       public static void KyGUI(Character character, short index, byte moneyType, int money, int quantity)
        {
            if (character.AllDiamond() < 5){
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn cần ít nhất 5 ngọc xanh để làm phí đăng bán"));
                return;
            }
            var item = character.ItemBag[index];
            if (money <= 0 || quantity > item.Quantity){    
                character.CharacterHandler.SendMessage(Service.ServerMessage("Có lỗi xảy ra"));
                character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                return;
            }
            if (item.Quantity > 99){
                character.CharacterHandler.SendMessage(Service.ServerMessage("Ký gửi tối đa x99"));
                character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                return;
            }
            
            var Id = Cache.Gi().kyGUIItems.Count;
            var itemkygui = new KyGUIItem((short)Id,(short)(item.Id + 1), item.Quantity, character.Id, (byte)GetTabKyGUI(item.Id, character.TypeMenu), moneyType == 0 ? money : -1, moneyType == 1 ? money : -1, 0, item.Options, false);
            Cache.Gi().kyGUIItems.Add(Id,itemkygui);
            KyGUIMySQL.InsertNewItem(itemkygui.Id, itemkygui.IdPlayerSell, itemkygui.Tab, itemkygui.ItemId, itemkygui.GoldSell, itemkygui.GemSell, itemkygui.quantity, itemkygui.Options);
            character.CharacterHandler.RemoveItemBagByIndex(item.IndexUI, quantity);
            character.CharacterHandler.SendMessage(Service.SendBag(character));
            character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
            character.CharacterHandler.SendMessage(Service.ServerMessage("Đăng bán thành công !"));
        }
        public static int GetTabKyGUI(int id, int TypeKiGUi = 0){
            var type = ItemCache.ItemTemplate((short)id).Type;
            switch (TypeKiGUi)
            {
                case 0:
                    switch (type)
                    {
                        case >= 0 and <= 2:
                            return 0;
                        case >= 3 and <= 4:
                                return 1;
                        case 12 or 33:
                            return 1;
                        case 29:
                            return 2;
                        case 27:
                            return 3;
                    }
                    break;
                   

                case 1:
                    return 0;
            }
            return 0;
        }

public static List<KyGUIItem> GetItemCanKYGUI(Character character, int typeKiGui = 0)
        {
            List<KyGUIItem> ikg = new List<KyGUIItem>();
            //if (character.ItemsKyGui.CharacterKiGui.Count >= 1)
            //{
            //    ikg.AddRange(character.ItemsKyGui.CharacterKiGui);
            //}
            switch (typeKiGui)
            {
                case 0:
                    //foreach (var item in Cache.Gi().kyGUIItems.Values)
                    //{
                    //    if (item.IdPlayerSell == character.Id)
                    //    {
                    //        ikg.Add(item);
                    //    }
                    //}
                    ikg.AddRange(Cache.Gi().kyGUIItems.Values.Where(i => i.IdPlayerSell == character.Id));
                    for (int i = 0; i < character.ItemBag.Count; i++)
                    {
                        var item = character.ItemBag[i];
                        var ItemTemplate = ItemCache.ItemTemplate(item.Id);
                        if ((ItemTemplate.Type < 5 && ItemTemplate.Type >= 0) || ItemTemplate.Type == 12 || ItemTemplate.Type == 33 || ItemTemplate.Type == 29)
                        {
                            ikg.Add(new KyGUIItem((short)item.IndexUI, item.Id, item.Quantity, character.Id, 4, 0, 0, 0, item.Options, false));
                        }
                    }
                    break;
                case 1:
                    //foreach (var item in Cache.Gi().kyGUIItems2.Values)
                    //{
                    //    if (item.IdPlayerSell == character.Id)
                    //    {
                    //        ikg.Add(item);
                    //    }
                    //}
                    ikg.AddRange(Cache.Gi().kyGUIItems2.Values.Where(i => i.IdPlayerSell == character.Id));
                    for (int i = 0; i < character.ItemBag.Count; i++)
                    {
                        var item = character.ItemBag[i];
                        var ItemTemplate = ItemCache.ItemTemplate(item.Id);
                        if (ItemTemplate.Id >= 1252 && ItemTemplate.Id <= 1257)
                        {
                            ikg.Add(new KyGUIItem((short)item.IndexUI, item.Id, item.Quantity, character.Id, 4, 0, 0, 0, item.Options, false));
                        }
                    }
                    break;
            }
            
            return ikg;
        }*/
    }
}