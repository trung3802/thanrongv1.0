﻿using System;
using TienKiemV2Remastered.Application.Handlers.Item;
using TienKiemV2Remastered.Application.Interfaces.Item;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Constants;

namespace TienKiemV2Remastered.Model.Item
{
    public class ItemMap : IDisposable
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public short X { get; set; }
        public short Y { get; set; }     
        public short R { get; set; }     
        public Item Item { get; set; }
        public long LeftTime { get; set; }

        // 
        public bool IsAuraItem { get; set; }
        public int AuraPlayerId { get; set; }
        public long AuraDelayTime { get; set; }

        public IItemMapHandler ItemMapHandler { get; set; }

        private bool _disposedValue;

        public ItemMap()
        {
            LeftTime = 60000 + ServerUtils.CurrentTimeMillis();
            R = -1;
            ItemMapHandler = new ItemMapHandler(this);
        }

        public ItemMap(int time)
        {
            LeftTime = time;
            R = -1;
            ItemMapHandler = new ItemMapHandler(this);
        }

        public ItemMap(int charId, Item item)
        {
            var itemTemplate = ItemCache.ItemTemplate(item.Id);
            var timeServer = ServerUtils.CurrentTimeMillis();
            PlayerId = charId;
            Item = item;
            R = -1;
            ItemMapHandler = new ItemMapHandler(this);

            if (itemTemplate.Type == 22) //aura 
            {
                
                LeftTime = 1800000 + timeServer;
                IsAuraItem = true;
            }else if (itemTemplate.Type == 28)
            {
                LeftTime = 3600000 + timeServer;
            }else if (itemTemplate.Id >= 353 && itemTemplate.Id <= 359)
            {
                LeftTime = 300000 + timeServer;
            }
            else 
            {
                LeftTime = 60000 + timeServer;
                IsAuraItem = false;
            }

            AuraDelayTime = 5000 + timeServer;
        }

        ~ItemMap() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
           
            if (disposing)
            {
                Item.Dispose();
                ItemMapHandler.Dispose();
            }
            _disposedValue = true;
        }
    }
}