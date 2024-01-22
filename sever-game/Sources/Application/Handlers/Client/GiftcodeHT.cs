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
    public static class GiftcodeHT
    {
        public static void HandleUseGiftcodeHT(Model.Character.Character character, string code)
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
            var codeType = GiftcodeDataBase.CheckCodeValidTypeHT(code);
            if (codeType == -1)
            {
                character.Delay.UseGiftCode = timeServer + 30000;
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Giftcode đã hết hạn hoặc hết lượt sử dụng."));
                return;
            }

            var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCodeHT(code, character.Name, codeType);

            //if (isUsedThisCode)
            //{
            //    character.Delay.UseGiftCode = timeServer + 30000;
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
            //    return;
            //}
            // Sử dụng gift code
            character.Delay.UseGiftCode = timeServer + 30000;
            UseCodeHT(character, code, codeType);

        }

        private static void UseCodeHT(Model.Character.Character character, string code, int codeType)
        {
            if (codeType == 1)//Giftcode hệ thống tặng đồ
            {
                var giftcodedb = ItemCache.GetItemDefault((short)1572);
                giftcodedb.Quantity = 1;

                character.CharacterHandler.AddItemToBag(true, giftcodedb, "Giftcode hệ thống");
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode hệ thống !"));
            }
            //if (codeType == 2)//Giftcode hệ thống tặng hồng ngọc
            //{
            //    var giftcodedb = ItemCache.GetItemDefault((short)1573);// 3 phiếu 200 hồng ngọc
            //    giftcodedb.Quantity = 3;
            //    character.CharacterHandler.AddItemToBag(true, giftcodedb, "Giftcode hệ thống");


            //    var thoivang = ItemCache.GetItemDefault((short)457); // 20 thỏi vàng
            //    thoivang.Quantity = 20;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode hệ thống");

            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode hệ thống !"));
            //}
            //if (codeType == 3)//top1
            //{
            //    // cải trang top1
            //    var cttop1 = ItemCache.GetItemDefault((short)1574);
            //    cttop1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, cttop1, "gittop1");

            //    // 1000 thỏi vàng
            //    var thoivang = ItemCache.GetItemDefault((short)457);
            //    thoivang.Quantity = 100;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "gittop1");

            //    // 2k hồng ngọc
            //    var hongngoct1 = ItemCache.GetItemDefault((short)1573);// 3 phiếu 200 hồng ngọc
            //    hongngoct1.Quantity = 10;
            //    character.CharacterHandler.AddItemToBag(true, hongngoct1, "gittop1");
                
            //    // 5 bộ nro 
            //    var nr1s = ItemCache.GetItemDefault((short)14); //1s
            //    nr1s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "gittop1");

            //    var nr2s = ItemCache.GetItemDefault((short)15);//2s
            //    nr2s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "gittop1");

            //    var nr3s = ItemCache.GetItemDefault((short)16);//3s
            //    nr3s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "gittop1");

            //    var nr4s = ItemCache.GetItemDefault((short)17);//4s
            //    nr4s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "gittop1");

            //    var nr5s = ItemCache.GetItemDefault((short)18);//5s
            //    nr5s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "gittop1");

            //    var nr6s = ItemCache.GetItemDefault((short)19);//6s
            //    nr6s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "gittop1");

            //    var nr7s = ItemCache.GetItemDefault((short)20);//7s
            //    nr7s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "gittop1");

            //    // hòm quà thiên sứ
            //    var honmts = ItemCache.GetItemDefault((short)1561);
            //    honmts.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, honmts, "gittop1");

            //    // hòm quà thần
            //    var homthan = ItemCache.GetItemDefault((short)1558);
            //    homthan.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, homthan, "gittop1");

            //    // danh hiệu độc quyền
            //    var dhdocquyen1 = ItemCache.GetItemDefault((short)1578);
            //    dhdocquyen1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, dhdocquyen1, "gittop1");



            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 1"));

            //}
            //if (codeType == 4)//top2
            //{
            //    // cải trang top1
            //    var cttop1 = ItemCache.GetItemDefault((short)1575);
            //    cttop1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, cttop1, "gittop2");

            //    // 60 thỏi vàng
            //    var thoivang = ItemCache.GetItemDefault((short)457);
            //    thoivang.Quantity = 60;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "gittop2");

            //    // 2k hồng ngọc
            //    var hongngoct1 = ItemCache.GetItemDefault((short)1573);// 3 phiếu 200 hồng ngọc
            //    hongngoct1.Quantity = 8;
            //    character.CharacterHandler.AddItemToBag(true, hongngoct1, "gittop2");

            //    // 5 bộ nro 
            //    var nr1s = ItemCache.GetItemDefault((short)14); //1s
            //    nr1s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "gittop2");

            //    var nr2s = ItemCache.GetItemDefault((short)15);//2s
            //    nr2s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "gittop2");

            //    var nr3s = ItemCache.GetItemDefault((short)16);//3s
            //    nr3s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "gittop2");

            //    var nr4s = ItemCache.GetItemDefault((short)17);//4s
            //    nr4s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "gittop2");

            //    var nr5s = ItemCache.GetItemDefault((short)18);//5s
            //    nr5s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "gittop2");

            //    var nr6s = ItemCache.GetItemDefault((short)19);//6s
            //    nr6s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "gittop2");

            //    var nr7s = ItemCache.GetItemDefault((short)20);//7s
            //    nr7s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "gittop2");

            //    // hòm quà thiên sứ
            //    var honmts = ItemCache.GetItemDefault((short)1561);
            //    honmts.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, honmts, "gittop2");

            //    // hòm quà thần
            //    var homthan = ItemCache.GetItemDefault((short)1558);
            //    homthan.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, homthan, "gittop2");

            //    // danh hiệu độc quyền
            //    var dhdocquyen1 = ItemCache.GetItemDefault((short)1579);
            //    dhdocquyen1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, dhdocquyen1, "gittop2");



            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 2"));

            //}
            //if (codeType == 5)//top3
            //{
            //    // cải trang top1
            //    var cttop1 = ItemCache.GetItemDefault((short)1576);
            //    cttop1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, cttop1, "gittop3");

            //    // 30 thỏi vàng
            //    var thoivang = ItemCache.GetItemDefault((short)457);
            //    thoivang.Quantity = 30;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "gittop3");

            //    // 2k hồng ngọc
            //    var hongngoct1 = ItemCache.GetItemDefault((short)1573);// 3 phiếu 200 hồng ngọc
            //    hongngoct1.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, hongngoct1, "gittop3");

            //    // 5 bộ nro 
            //    var nr1s = ItemCache.GetItemDefault((short)14); //1s
            //    nr1s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "gittop3");

            //    var nr2s = ItemCache.GetItemDefault((short)15);//2s
            //    nr2s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "gittop3");

            //    var nr3s = ItemCache.GetItemDefault((short)16);//3s
            //    nr3s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "gittop3");

            //    var nr4s = ItemCache.GetItemDefault((short)17);//4s
            //    nr4s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "gittop3");

            //    var nr5s = ItemCache.GetItemDefault((short)18);//5s
            //    nr5s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "gittop3");

            //    var nr6s = ItemCache.GetItemDefault((short)19);//6s
            //    nr6s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "gittop3");

            //    var nr7s = ItemCache.GetItemDefault((short)20);//7s
            //    nr7s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "gittop3");

            //    // hòm quà thiên sứ
            //    var honmts = ItemCache.GetItemDefault((short)1561);
            //    honmts.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, honmts, "gittop3");

            //    // hòm quà thần
            //    var homthan = ItemCache.GetItemDefault((short)1558);
            //    homthan.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, homthan, "gittop3");

            //    // danh hiệu độc quyền
            //    var dhdocquyen1 = ItemCache.GetItemDefault((short)1580);
            //    dhdocquyen1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, dhdocquyen1, "gittop3");



            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 3"));

            //}
            //if (codeType == 6)//top4 tới 10
            //{
            //    // cải trang top1
            //    var cttop1 = ItemCache.GetItemDefault((short)1577);
            //    cttop1.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, cttop1, "gittopnguoihung");

            //    // 30 thỏi vàng
            //    var thoivang = ItemCache.GetItemDefault((short)457);
            //    thoivang.Quantity = 20;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "gittopnguoihung");

            //    // 2k hồng ngọc
            //    var hongngoct1 = ItemCache.GetItemDefault((short)1573);// 3 phiếu 200 hồng ngọc
            //    hongngoct1.Quantity = 3;
            //    character.CharacterHandler.AddItemToBag(true, hongngoct1, "gittopnguoihung");

            //    // 5 bộ nro 
            //    var nr1s = ItemCache.GetItemDefault((short)14); //1s
            //    nr1s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "gittopnguoihung");

            //    var nr2s = ItemCache.GetItemDefault((short)15);//2s
            //    nr2s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "gittopnguoihung");

            //    var nr3s = ItemCache.GetItemDefault((short)16);//3s
            //    nr3s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "gittopnguoihung");

            //    var nr4s = ItemCache.GetItemDefault((short)17);//4s
            //    nr4s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "gittopnguoihung");

            //    var nr5s = ItemCache.GetItemDefault((short)18);//5s
            //    nr5s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "gittopnguoihung");

            //    var nr6s = ItemCache.GetItemDefault((short)19);//6s
            //    nr6s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "gittopnguoihung");

            //    var nr7s = ItemCache.GetItemDefault((short)20);//7s
            //    nr7s.Quantity = 5;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "gittopnguoihung");

            //    // hòm quà thiên sứ
            //    var honmts = ItemCache.GetItemDefault((short)1561);
            //    honmts.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, honmts, "gittopnguoihung");

            //    // hòm quà thần
            //    var homthan = ItemCache.GetItemDefault((short)1558);
            //    homthan.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, homthan, "gittopnguoihung");

                



            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 4 tới 10"));

            //}
            GiftcodeDataBase.UsedCodeHT(code, character.Name, codeType);
        }
    }
}