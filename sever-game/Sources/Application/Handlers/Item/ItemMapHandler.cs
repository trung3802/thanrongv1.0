﻿using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Interfaces.Item;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Model;
using System.Security.Cryptography.X509Certificates;

using TienKiemV2Remastered.Application.Extension.Namecball;

namespace TienKiemV2Remastered.Application.Handlers.Item
{
    public class ItemMapHandler : IItemMapHandler
    {
        private readonly ItemMap _itemMap;

        public ItemMapHandler(ItemMap itemMap)
        {
            _itemMap = itemMap;
        }
        public void Update(Zone zone)
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
           
            if (_itemMap.LeftTime == -1) return;
            if(_itemMap.LeftTime <= timeServer)
            {
                if (_itemMap.Item.Id >= 353 && _itemMap.Item.Id <= 359)
                {
                    Init.countNamecBall--;
                    Server.Gi().Logger.Print("Count Namec Ball: " + Init.countNamecBall);
                }
                zone.ZoneHandler.RemoveItemMap(_itemMap.Id);                
            }
            else if (_itemMap.LeftTime - timeServer <= 15000 && !_itemMap.IsAuraItem)
            {
                _itemMap.PlayerId = -1;
            }
            // Update vệ tinh ở đây
            try
            {
                if (_itemMap.IsAuraItem && _itemMap.AuraDelayTime < timeServer)
                {
                    var playerOwner = zone.ZoneHandler.GetCharacter(_itemMap.AuraPlayerId);

                    if (playerOwner != null)
                    {
                        if (Math.Abs(playerOwner.InfoChar.X - _itemMap.X) <= 200)
                        {
                            var playerReal = (Model.Character.Character) playerOwner;
                            if (_itemMap.Item.Id == 342)
                            {
                                playerReal.InfoMore.IsNearAuraTriLucItem = true;
                            }
                            else if (_itemMap.Item.Id == 343)
                            {
                                playerReal.InfoMore.IsNearAuraTriTueItem = true;
                                playerReal.InfoMore.AuraTriTueTime = timeServer + 10000;
                            }
                            else if (_itemMap.Item.Id == 344)
                            {
                                playerReal.InfoMore.IsNearAuraPhongThuItem = true;
                                playerReal.InfoMore.AuraPhongThuTime = timeServer + 10000;
                            }
                            else if (_itemMap.Item.Id == 345)
                            {
                                playerReal.InfoMore.IsNearAuraSinhLucItem = true;
                            }
                        }

                        if (playerOwner.ClanId != -1)
                        {
                            foreach (var clanChar in zone.Characters.Values.ToList().Where(c => c.ClanId != -1 && c.ClanId == playerOwner.ClanId && c.Id != playerOwner.Id))
                            {
                                // kiểm tra vị trí
                                if (clanChar != null && Math.Abs(clanChar.InfoChar.X - _itemMap.X) <= 200)
                                {
                                    var clanCharReal = (Model.Character.Character) clanChar;
                                    if (_itemMap.Item.Id == 342)
                                    {
                                        clanCharReal.InfoMore.IsNearAuraTriLucItem = true;
                                    }
                                    else if (_itemMap.Item.Id == 343)
                                    {
                                        clanCharReal.InfoMore.IsNearAuraTriTueItem = true;
                                        clanCharReal.InfoMore.AuraTriTueTime = timeServer + 10000;
                                    }
                                    else if (_itemMap.Item.Id == 344)
                                    {
                                        clanCharReal.InfoMore.IsNearAuraPhongThuItem = true;
                                        clanCharReal.InfoMore.AuraPhongThuTime = timeServer + 10000;
                                    }
                                    else if (_itemMap.Item.Id == 345)
                                    {
                                        clanCharReal.InfoMore.IsNearAuraSinhLucItem = true;
                                    }
                                }
                            }
                        }
                    }
                    // Kiểm tra chủ nhân có ở gần không
                    _itemMap.AuraDelayTime = timeServer + 10000;
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error Update Item Map Ve Tinh in ItemMapHandler.cs: {e.Message}\n{e.StackTrace}", e);
            }
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}