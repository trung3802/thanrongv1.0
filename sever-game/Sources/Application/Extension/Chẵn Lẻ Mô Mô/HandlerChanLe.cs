using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Chẵn_Lẻ_Momo
{
    public class HandlerChanLe
    {
        public static List<int> listChan = new List<int> { };
        public static List<int> listLe = new List<int> { };
        public static long AllGoldChan = 0;
        public static long AllGoldLe = 0;
        public static long Time = 300000 + ServerUtils.CurrentTimeMillis();
        public static Boolean Chan = false;
        public static Boolean Le = false;
        public static Boolean isStop = false;
        public static String ChanOrLe = "[Chưa có]";
        public static long TimeFinish = 300000;
        public static void PickChan(Character character)
        {
            if (character.DataMiniGame.pickLe)
            {
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã đặt cược lẻ rồii !"));
                return;
            }
            var inputChan = new List<InputBox>();
            var inputThoiVang = new InputBox()
            {
                Name = "Nhập số thỏi vàng bạn muốn đặt cược",
                Type = 1,
            };
            inputChan.Add(inputThoiVang);
           
           
            character.CharacterHandler.SendMessage(Service.ShowInput("Được ăn cả, Ngã về mo",inputChan));
            character.TypeInput = 17;
        }
        public static void PickLe(Character character)
        {
            if (character.DataMiniGame.pickChan)
            {
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã đặt cược chẵn rồii !"));
                return;
            }
            var inputLe = new List<InputBox>();
            var inputThoiVang = new InputBox()
            {
                Name = "Nhập số thỏi vàng bạn muốn đặt cược",
                Type = 1,
            };
            inputLe.Add(inputThoiVang);
            
           
            character.CharacterHandler.SendMessage(Service.ShowInput("Được ăn cả, Ngã về mo", inputLe));
            character.TypeInput = 18;
            
        }
        public static String Menu(Character character)  
        {
            var seconds = (TimeFinish - ServerUtils.CurrentTimeMillis()) / 1000;
            if (character.DataMiniGame.pickChan) {
                return "Mini game Cho các con nghiện Chẵn Lẽ\nCòn " + ServerUtils.GetTimeAgo((int)seconds) + " nữa sẽ chốt kết quả, Kết quả trước đó là " + ChanOrLe + ".\n Bạn đã đặt cược " + character.DataMiniGame.thoivang + " thỏi vàng vào Chẵn\nTổng giao dịch của Nhà cái JR là " + (AllGoldChan + AllGoldLe) + " thỏi vàng\nThắng lụm x1.6 lượng thỏi vàng đặt được" ;
            }
            if (character.DataMiniGame.pickLe)
            {
                return "Mini game Cho các con nghiện Chẵn Lẽ\nCòn " + ServerUtils.GetTimeAgo((int)seconds) + " nữa sẽ chốt kết quả, Kết quả trước đó là " + ChanOrLe + ".\n Bạn đã đặt cược " + character.DataMiniGame.thoivang + " thỏi vàng vào Lẻ\nTổng giao dịch của Nhà cái JR là " + (AllGoldChan + AllGoldLe) + " thỏi vàng\nThắng lụm x1.6 lượng thỏi vàng đặt được";
            }
            return "Mini game Cho các con nghiện Chẵn Lẽ\nCòn " + ServerUtils.GetTimeAgo((int)seconds) + " nữa sẽ chốt kết quả, Kết quả trước đó là " + ChanOrLe + ".\n[Bạn chưa đặt cược]\nTổng giao dịch của Nhà cái JR là " + (AllGoldChan + AllGoldLe) + " thỏi vàng\nThắng lụm x1.6 lượng thỏi vàng đặt cược";
        }
        
        public static void Update()
        {
            
            new Thread(new ThreadStart(() =>
            {
                while (Server.Gi().IsRunning)
                {
                    var time = ServerUtils.CurrentTimeMillis();
                    var randomChanOrLe = ServerUtils.RandomNumber(10); 
                    if (time >= Time)
                    {
                        if (AllGoldChan > AllGoldLe)
                        {
                            randomChanOrLe = 1;
                        }
                        if (AllGoldLe > AllGoldChan)
                        {
                            randomChanOrLe = 0;
                        }
                        Time = 300000 + time;
                        if (randomChanOrLe == 0 || randomChanOrLe == 3 || randomChanOrLe == 7 || randomChanOrLe == 9) // Chan
                        {
                            if (listChan != null && listChan.Count >= 1)
                            {
                                for (int i = 0; i < listChan.Count; i++)
                            {
                                
                                    var getChar = ClientManager.Gi().GetCharacter(listChan[i]);
                                    if (getChar != null)
                                    {
                                        if (getChar != null && getChar.DataMiniGame.pickChan)
                                        {
                                            getChar.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Đặt Cược Thông Minh"));
                                            var thoivang = ItemCache.GetItemDefault(457);
                                            thoivang.Quantity = getChar.DataMiniGame.thoivang*10/6;
                                            getChar.CharacterHandler.AddItemToBag(true, thoivang, "Chan Le: Chan");
                                            getChar.CharacterHandler.SendMessage(Service.SendBag(getChar));
                                            Chan = true;
                                            Le = false;
                                            getChar.DataMiniGame.pickChan = false;
                                            getChar.DataMiniGame.thoivang = 0;
                                            ChanOrLe = "Chẵn";
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                
                            }
                            ClientManager.Gi().SendMessageCharacter(Service.ServerChat("Kết quả của Chẵn Lẽ là: Chẵn"));
                            Server.Gi().Logger.Print("Odd Even Result: Odd", "yellow");
                            AllGoldChan = 0;
                            listChan.Clear();
                        }
                        else  // Le
                        {
                            if (listLe != null && listLe.Count >= 1)
                            {
                                for (int i = 0; i < listLe.Count; i++)
                                {
                                    var getChar = ClientManager.Gi().GetCharacter(listLe[i]);
                                    if (getChar != null)
                                    {
                                        if (getChar.DataMiniGame.pickLe)
                                        {
                                            getChar.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Đặt Cược Thông Minh"));
                                            var thoivang = ItemCache.GetItemDefault(457);
                                            thoivang.Quantity = getChar.DataMiniGame.thoivang*10/6;
                                            getChar.CharacterHandler.AddItemToBag(true, thoivang, "Chan Le: Le");
                                            getChar.DataMiniGame.pickLe = false;
                                            getChar.CharacterHandler.SendMessage(Service.SendBag(getChar));
                                            Le = true;
                                            Chan = false;
                                            getChar.DataMiniGame.thoivang = 0;
                                            ChanOrLe = "Lẻ";
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                
                               
                            }
                            ClientManager.Gi().SendMessageCharacter(Service.ServerChat("Kết quả của Chẵn Lẽ là: Lẻ"));
                            Server.Gi().Logger.Print("Odd Even Result: Even", "yellow");
                            AllGoldLe = 0;
                            listLe.Clear();
                        }
                    }
                    Thread.Sleep(10000);

                }
                Server.Gi().Logger.Print("Odd Even Close !", "red");
                isStop = true;
            })).Start();
        }
    }
}
