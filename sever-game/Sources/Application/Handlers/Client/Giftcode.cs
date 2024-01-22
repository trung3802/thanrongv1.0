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
    public static class Giftcode
    {
        public static void HandleUseGiftcode(Model.Character.Character character, string code)
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
            var codeType = GiftcodeDataBase.CheckCodeValidType(code);
            if (codeType == -1)
            {
                character.Delay.UseGiftCode = timeServer + 5000;
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Giftcode đã hết hạn hoặc hết lượt sử dụng."));
                return;
            }

            var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCode(code, character.Name, codeType);

            if (isUsedThisCode)
            {
                character.Delay.UseGiftCode = timeServer + 5000;
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
                return;
            }
            // Sử dụng gift code
            character.Delay.UseGiftCode = timeServer + 5000;
            UseCode(character, code, codeType);

        }

        private static void UseCode(Model.Character.Character character, string code, int codeType)
        {
            if (codeType == 0)//tanthu
            {
                //Kiểm tra giftcode đã sử dụng chưa
                var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCode(code, character.Name, codeType);

                if (isUsedThisCode)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
                    return;
                }

                // Cải trang mị nương
                //var thoivang = ItemCache.GetItemDefault((short)860);
                //thoivang.Quantity = 1; //thỏi vàng
                //character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
                // 15 viên 2 sao
                var nr3s = ItemCache.GetItemDefault((short)14);
                nr3s.Quantity = 10;
                var nr31s = ItemCache.GetItemDefault((short)15);
                nr31s.Quantity = 10;
                var nr32s = ItemCache.GetItemDefault((short)16);
                nr32s.Quantity = 10;
                var nr33s = ItemCache.GetItemDefault((short)17);
                nr33s.Quantity = 10;
                var nr34s = ItemCache.GetItemDefault((short)18);
                nr34s.Quantity = 10;
                var nr36s = ItemCache.GetItemDefault((short)19);
                nr36s.Quantity = 10;
                var nr37s = ItemCache.GetItemDefault((short)20);
                nr37s.Quantity = 10;

                character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công Gitcode tân thủ !"));
            }
            else if (codeType == 1)//thoivang
            {
                //Kiểm tra giftcode đã sử dụng chưa
                var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCode(code, character.Name, codeType);

                if (isUsedThisCode)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
                    return;
                }
                var thoivang = ItemCache.GetItemDefault((short)457);
                thoivang.Quantity = 50;
                character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công Gitcode thỏi vàng!"));
            }
            else if (codeType == 2)//giftcode tân thủ, nhận cải trang hit ngẫu nhiên chỉ số, ngẫu nhiên hạn
            {
                //Kiểm tra giftcode đã sử dụng chưa
                var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCode(code, character.Name, codeType);

                if (isUsedThisCode)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
                    return;
                }

                // Cải trang hit
                var caitranghit = ItemCache.GetItemDefault((short)884);
                caitranghit.Quantity = 1;
                var caitranghit1 = ItemCache.GetItemDefault((short)730);
                caitranghit1.Quantity = 1;

                //Ramdom chỉ số từ 20-120%
                var chisomoi = 0;
                var randomRate = ServerUtils.RandomNumber(0.0, 100.0);
                if (randomRate <= 0.8) //0.8% ra 111-120
                {
                    //0.8%
                    chisomoi = ServerUtils.RandomNumber(110, 120);
                }
                else if (randomRate <= 1.6) //1.6% ra 101-110
                {
                    //0.8%
                    chisomoi = ServerUtils.RandomNumber(101, 110);
                }
                else if (randomRate <= 3.2) //3.2% ra 91-100
                {
                    // 1.6%
                    chisomoi = ServerUtils.RandomNumber(91, 100);
                }
                else if (randomRate <= 5.4) //5.4% ra 81-90
                {
                    //2.2%
                    chisomoi = ServerUtils.RandomNumber(81, 90);
                }
                else if (randomRate <= 8.4) //8.4% ra 71-80
                {
                    // 3%
                    chisomoi = ServerUtils.RandomNumber(71, 80);
                }
                else if (randomRate <= 18.4) //18.4% ra 61-70
                {
                    // 10%
                    chisomoi = ServerUtils.RandomNumber(61, 70);
                }
                else if (randomRate <= 38.4) //38.4% ra 51-60
                {
                    //20%
                    chisomoi = ServerUtils.RandomNumber(51, 60);
                }
                else if (randomRate <= 82.4) //82.4% ra 41-50
                {
                    // 44%
                    chisomoi = ServerUtils.RandomNumber(41, 50);
                }
                else
                {
                    // 17%
                    chisomoi = ServerUtils.RandomNumber(20, 40);
                }
                foreach (var option in caitranghit.Options)
                {
                    if (option.Id == 5)
                    {
                        option.Param = chisomoi;
                    }
                }



                // cải trang lv2
                var chisomoi1 = 0;
                var randomRate1 = ServerUtils.RandomNumber(0.0, 100.0);
                if (randomRate1 <= 0.8) //0.8% ra 111-120
                {
                    //0.8%
                    chisomoi1 = ServerUtils.RandomNumber(35, 50);
                }
                
                else if (randomRate1 <= 38.4) //38.4% ra 51-60
                {
                    //20%
                    chisomoi1 = ServerUtils.RandomNumber(29, 34);
                }
                else if (randomRate1 <= 82.4) //82.4% ra 41-50
                {
                    // 44%
                    chisomoi1 = ServerUtils.RandomNumber(20, 28);
                }
                else
                {
                    // 17%
                    chisomoi1 = ServerUtils.RandomNumber(15, 19);
                }
                foreach (var option in caitranghit1.Options)
                {
                    if (option.Id == 50)
                    {
                        option.Param = chisomoi1;
                    }
                }


                //Ramdom hạn sử dụng từ 5-20 ngày
                var hansudung = 0;
                var timeServer = ServerUtils.CurrentTimeSecond();
                var ramdom = ServerUtils.RandomNumber(0, 100);

                if (ramdom <= 0.8) //0.8% ra 20 ngày
                {
                    //0.8%
                    hansudung = 20;
                }
                else if (ramdom <= 1.6) //1.6% ra 19 ngày
                {
                    //0.8%
                    hansudung = ServerUtils.RandomNumber(19);
                }
                else if (ramdom <= 3.2) //3.2% ra 15-17
                {
                    // 1.6%
                    hansudung = ServerUtils.RandomNumber(17, 18);
                }
                else if (ramdom <= 5.4) //5.4% ra 14-15
                {
                    //2.2%
                    hansudung = ServerUtils.RandomNumber(15, 16);
                }
                else if (ramdom <= 8.4) //8.4% ra 71-80
                {
                    // 3%
                    hansudung = ServerUtils.RandomNumber(13, 14);
                }
                else if (ramdom <= 18.4) //18.4% ra 7-9 ngày
                {
                    // 10%
                    hansudung = ServerUtils.RandomNumber(7, 9);
                }
                else if (ramdom <= 38.4) //38.4% ra 11-13 ngày
                {
                    //20%
                    hansudung = ServerUtils.RandomNumber(11, 12);
                }
                else if (ramdom <= 82.4) //82.4% ra 10 ngày
                {
                    // 44%
                    hansudung = 10;
                }
                else
                {
                    // 17%
                    hansudung = ServerUtils.RandomNumber(5, 7);
                }

                caitranghit.Options.Add(new OptionItem()
                {
                    Id = 93,
                    Param = hansudung
                });
                caitranghit1.Options.Add(new OptionItem()
                {
                    Id = 93,
                    Param = hansudung
                });

                character.CharacterHandler.AddItemToBag(true, caitranghit, "Giftcode tân thủ"); 
                character.CharacterHandler.AddItemToBag(true, caitranghit1, "Giftcode tân thủ");
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công giftcode tân thủ hồng ngọc!"));
            }

            else if (codeType == 3)//pet
            {
                var thoivang = ItemCache.GetItemDefault((short)943);
                thoivang.Quantity = 1;
                character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");                
                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode pet !"));

            }
            if (codeType == 4)//ngọc rồng
            {
                //Kiểm tra giftcode đã sử dụng chưa
                var isUsedThisCode = GiftcodeDataBase.CheckCharacterAlreadyUsedCode(code, character.Name, codeType);

                if (isUsedThisCode)
                {
                    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Bạn đã dùng Giftcode này rồi."));
                    return;
                }               
                
                //// 15 viên 2 sao
                //var nr3s = ItemCache.GetItemDefault((short)14);
                //nr3s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
                ////
                //var nr31s = ItemCache.GetItemDefault((short)15);
                //nr31s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr31s, "Giftcode");
                ////
                //var nr32s = ItemCache.GetItemDefault((short)16);
                //nr32s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr32s, "Giftcode");
                ////
                //var nr33s = ItemCache.GetItemDefault((short)17);
                //nr33s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr33s, "Giftcode");
                ////
                //var nr34s = ItemCache.GetItemDefault((short)18);
                //nr34s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr34s, "Giftcode");
                ////
                //var nr36s = ItemCache.GetItemDefault((short)19);
                //nr36s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr36s, "Giftcode");
                ////
                //var nr37s = ItemCache.GetItemDefault((short)20);
                //nr37s.Quantity = 4;
                //character.CharacterHandler.AddItemToBag(true, nr37s, "Giftcode");
                ////
                //var thoivang = ItemCache.GetItemDefault((short)457);
                //thoivang.Quantity = 50;
                //character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");

                var mbtai = ItemCache.GetItemDefault((short)933);
                mbtai.Quantity = 200;
                character.CharacterHandler.AddItemToBag(true, mbtai, "Giftcode");

                character.CharacterHandler.SendMessage(Service.SendBag(character));
                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công Gitcode tưng bừng !"));
            }
            //else if (codeType == 4)//top2
            //{
            //    // thỏ bunma 160%
            //    var thoivang11 = ItemCache.GetItemDefault((short)1414);
            //    thoivang11.Quantity = 1;
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 4;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 2;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 2"));

            //}
            //else if (codeType == 5)//top3
            //{
            //    // thỏ bunma 180%
            //    var thoivang11 = ItemCache.GetItemDefault((short)1415);
            //    thoivang11.Quantity = 1;
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 3;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 2;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 4;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 3"));

            //}
            //else if (codeType == 6)//top4
            //{
            //    // thỏ bunma 180%
            //    var thoivang11 = ItemCache.GetItemDefault((short)1416);
            //    thoivang11.Quantity = 1;
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 4"));

            //}
            //else if (codeType == 7)//top5
            //{

            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 5"));

            //}
            //else if (codeType == 8)//top4 -->10
            //{
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 6"));

            //}
            //else if (codeType == 9)//top4 -->10
            //{
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 7"));

            //}
            //else if (codeType == 10)//top4 -->10
            //{
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 8"));

            //}
            //else if (codeType == 11)//top4 -->10
            //{
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 9"));

            //}
            //else if (codeType == 12)//top4 -->10
            //{
            //    // 5 phieu
            //    var thoivang = ItemCache.GetItemDefault((short)1419);
            //    thoivang.Quantity = 1;
            //    // 2 bùa vô cực
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var thoivang1 = ItemCache.GetItemDefault((short)1176);
            //    thoivang1.Quantity = 1;
            //    // 4 bộ nro bí
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    var nr1s = ItemCache.GetItemDefault((short)702);
            //    nr1s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr1s, "Giftcode");
            //    var nr2s = ItemCache.GetItemDefault((short)703);
            //    nr2s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr2s, "Giftcode");
            //    var nr3s = ItemCache.GetItemDefault((short)704);
            //    nr3s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr3s, "Giftcode");
            //    var nr4s = ItemCache.GetItemDefault((short)705);
            //    nr4s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr4s, "Giftcode");
            //    var nr5s = ItemCache.GetItemDefault((short)706);
            //    nr5s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr5s, "Giftcode");
            //    var nr6s = ItemCache.GetItemDefault((short)707);
            //    nr6s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr6s, "Giftcode");
            //    var nr7s = ItemCache.GetItemDefault((short)708);
            //    nr7s.Quantity = 1;
            //    character.CharacterHandler.AddItemToBag(true, nr7s, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @"Nhận thành công gitcode TOP 10"));

            //}
            //else if (codeType == 13)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 13)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 14)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 15)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 16)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 17)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 18)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 19)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 20)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 21)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            //else if (codeType == 22)
            //{
            //    var thoivang = ItemCache.GetItemDefault((short)1137);
            //    thoivang.Quantity = 200;
            //    character.CharacterHandler.AddItemToBag(true, thoivang, "Giftcode");
            //    character.CharacterHandler.SendMessage(Service.SendBag(character));
            //    character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, @" Nhận thành công gitcode Live !"));

            //}
            GiftcodeDataBase.UsedCode(code, character.Name, codeType);
        }
    }
}