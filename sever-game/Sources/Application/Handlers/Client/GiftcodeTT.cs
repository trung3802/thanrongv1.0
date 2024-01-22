using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Menu;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model.Option;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Model.Character;
using Newtonsoft.Json;
using Sources.Database;

namespace TienKiemV2Remastered.Application.Handlers.Client
{
    public static class GiftcodeTT
    {
        public static void HandleUseGiftcodeTT(Model.Character.Character character, string code)
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            if (character.Delay.UseGiftCode > timeServer)
            {
                var delay = (character.Delay.UseGiftCode - timeServer) / 1000;
                if (delay < 1)
                {
                    delay = 1;
                }

                character.CharacterHandler.SendMessage(Service.DialogMessage(string.Format(TextServer.gI().DELAY_SEC,
                        delay)));
                return;
            }
            // kiểm tra hạn gift code
            // kiểm tra đã dùng gift code chưa
            var codeType = GiftcodeDataBase.CheckCodeValidTypeTT(code);
            if (codeType == -1)
            {
                character.Delay.UseGiftCode = timeServer + 30000;
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Giftcode đã hết hạn hoặc hết lượt sử dụng."));
                return;
            }

            var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCodeTT(code, character.Name, codeType);

            //if (isUsedThisCode)
            //{
            //    character.Delay.UseGiftCode = timeServer + 30000;
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
            //    return;
            //}
            // Sử dụng gift code
            character.Delay.UseGiftCode = timeServer + 30000;
            UseCodeTT(character, code, codeType);

        }

        private static void UseCodeTT(Model.Character.Character character, string code, int codeType)
        {
            if (codeType == 1)//Giftcode đặc biệt
            {
                //Hộp qìa đặc biệt
                var giftcodedb = ItemCache.GetItemDefault((short)1533);
                giftcodedb.Quantity = 1;

                character.CharacterHandler.AddItemToBag(true, giftcodedb, "Giftcode");
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode đặc biệt !"));
            }
            GiftcodeDataBase.UsedCodeTT(code, character.Name, codeType);
        }
    }
}