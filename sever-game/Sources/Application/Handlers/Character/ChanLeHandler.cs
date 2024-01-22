using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using System.Collections.Generic;

namespace TienKiemV2Remastered.Source.Application.Handlers
{
    class ChanLeHandler
    {
        private static long timeDelay = 5 + ServerUtils.CurrentTimeSecond();
        private static long timeStart = ServerUtils.CurrentTimeSecond();
        public static bool isStartGame;
        public static int numberRandom = -1;
        public static List<int> IdWinCuoc = new List<int>();
        public static List<int> IdCuocChan = new List<int>();
        public static List<int> IdCuocLe = new List<int>();
        public static List<int> DatTienCuocChan = new List<int>();
        public static List<int> DatTienCuocLe = new List<int>();

        public static void StartNewGame()
        {
            var timeServer = ServerUtils.CurrentTimeSecond();
            var timeDelayLeft = timeDelay - timeServer;
            if (timeDelayLeft > 0)
            {
                return;
            }
            if (!isStartGame)
            {
                IdCuocChan.Clear();
                IdCuocLe.Clear();
                DatTienCuocChan.Clear();
                DatTienCuocLe.Clear();
                timeStart = 60 + timeServer;
                isStartGame = true;
            }
            if (timeStart - timeServer <= 0)
            {
                GetKetQua();
            }

        }
        public static bool Check(int idchar)
        {
            if (IdCuocChan.Contains(idchar) || IdCuocLe.Contains(idchar))
            {
                return true;
            }
            return false;
        }
        public static bool AddCuocChan(int idChar, int soLuongCuoc)
        {
            if (!isStartGame) return false;
            if (Check(idChar))
            {
                return false;
            }
            if (!IdCuocChan.Contains(idChar))
            {
                IdCuocChan.Add(idChar);
                DatTienCuocChan.Add(soLuongCuoc);
                return true; //Cược mới thành công;
            }
            else if (IdCuocChan.Contains(idChar))
            {
                for (int i = 0; i < IdCuocChan.Count; i++)
                {
                    if (IdCuocChan[i] == idChar)
                    {
                        var tienNew = DatTienCuocChan[i] + soLuongCuoc;
                        DatTienCuocChan.RemoveAt(i);
                        DatTienCuocChan.Insert(i, tienNew);
                        return true; //Cược thêm thành công
                    }
                }
            }
            return false; //Lỗi cược
        }

        public static bool AddCuocLe(int idChar, int soLuongCuoc)
        {
            if (!isStartGame) return false;
            if (Check(idChar))
            {
                return false;
            }
            if (!IdCuocLe.Contains(idChar))
            {
                IdCuocLe.Add(idChar);
                DatTienCuocLe.Add(soLuongCuoc);
                return true; //Cược mới thành công;
            }
            else if (IdCuocLe.Contains(idChar))
            {
                for (int i = 0; i < IdCuocLe.Count; i++)
                {
                    if (IdCuocLe[i] == idChar)
                    {
                        var tienNew = DatTienCuocLe[i] + soLuongCuoc;
                        DatTienCuocLe.RemoveAt(i);
                        DatTienCuocLe.Insert(i, tienNew);
                        return false; //Cược thêm thành công
                    }
                }
            }
            return false; //Lỗi cược
        }
        public static void GetKetQua()
        {
            var timeServer = ServerUtils.CurrentTimeSecond();
            if (isStartGame && timeStart - timeServer <= 0)
            {
                var RandomNumber = ServerUtils.RandomNumber(10000, 90000);
                numberRandom = RandomNumber;
                timeStart = timeServer;
                timeDelay = 10 + timeServer;
                isStartGame = false;
                IdWinCuoc.Clear();
            }
        }
        private static int Calculator(int numberRandom)
        {
            if (numberRandom == -1) return -1;
            if (numberRandom % 2 == 0) //Chan win
            {
                return 0;
            }
            else //Le win
            {
                return 1;
            }
        }
        public static void PhatPhanThuong(int idChar)
        {
            thongBaoThua(idChar);
            if (!isStartGame && Calculator(numberRandom) == 0)
            {
                for (int i = 0; i < IdCuocChan.Count; i++)
                {
                    if (IdCuocChan[i] == idChar)
                    {
                        var getChar = ClientManager.Gi().GetCharacter(idChar);
                        var a = DatTienCuocChan[i] * 17 / 10;
                        var thoivang = ItemCache.GetItemDefault((short)457);
                        thoivang.Quantity = a; //thỏi vàng
                        getChar.CharacterHandler.AddItemToBag(true, thoivang, "CSMM");
                        getChar.CharacterHandler.SendMessage(Service.SendBag(getChar));
                        getChar.CharacterHandler.SendMessage(Service.MeLoadInfo(getChar));
                        getChar.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã thắng cược {a} Chẵn"));
                        IdWinCuoc.Add(idChar);
                        DatTienCuocChan.RemoveAt(i);
                        IdCuocChan.RemoveAt(i);
                    }

                }
            }
            if (!isStartGame && Calculator(numberRandom) == 1)
            {
                for (int i = 0; i < IdCuocLe.Count; i++)
                {
                    if (IdCuocLe[i] == idChar)
                    {
                        var getChar = ClientManager.Gi().GetCharacter(idChar);
                        var a = DatTienCuocLe[i] * 17 / 10;
                        var thoivang = ItemCache.GetItemDefault((short)457);
                        thoivang.Quantity = a; //thỏi vàng
                        getChar.CharacterHandler.AddItemToBag(true, thoivang, "CSMM");
                        getChar.CharacterHandler.SendMessage(Service.SendBag(getChar));
                        getChar.CharacterHandler.SendMessage(Service.MeLoadInfo(getChar));
                        getChar.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn đã thắng cược {a} Lẻ"));
                        IdWinCuoc.Add(idChar);
                        DatTienCuocLe.RemoveAt(i);
                        IdCuocLe.RemoveAt(i);
                    }
                }
            }
        }
        public static void thongBaoThua(int idChar)
        {
            if (!isStartGame && Calculator(numberRandom) == 0)
            {
                if (CheckDatCuoc(idChar) == 1)
                {
                    var getChar = ClientManager.Gi().GetCharacter(idChar);
                    getChar.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thua cược mất rồi ! Kết quả cược là Chẵn !!"));
                    IdCuocLe.Clear();
                    DatTienCuocLe.Clear();
                }
            }
            if (!isStartGame && Calculator(numberRandom) == 1)
            {
                if (CheckDatCuoc(idChar) == 0)
                {
                    var getChar = ClientManager.Gi().GetCharacter(idChar);
                    getChar.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã thua cược mất rồi ! Kết quả cược là Lẻ !!"));
                    IdCuocChan.Clear();
                    DatTienCuocChan.Clear();
                }
            }
        }
        public static long GetTimeLeft()
        {
            var timeServer = ServerUtils.CurrentTimeSecond();
            if (isStartGame)
            {
                var timeleft = timeStart - timeServer;
                return timeleft;
            }
            return 0;
        }
        public static long GetTimeDelayLeft()
        {
            var timeServer = ServerUtils.CurrentTimeSecond();
            if (!isStartGame)
            {
                var timeleft = timeDelay - timeServer;
                return timeleft;
            }
            return 0;
        }
        public static int CheckDatCuoc(int idChar)
        {
            if (IdCuocChan.Contains(idChar))
            {
                return 0;
            }
            if (IdCuocLe.Contains(idChar))
            {
                return 1;
            }
            return -1;
        }
        public static int TongTienCuoc()
        {
            var tongtien = 0;
            if (DatTienCuocChan != null && DatTienCuocLe != null)
            {
                if (DatTienCuocChan.Count > 0)
                {
                    for (int i = 0; i < DatTienCuocChan.Count; i++)
                    {
                        tongtien += DatTienCuocChan[i];
                    }
                }
                if (DatTienCuocLe.Count > 0)
                {
                    for (int i = 0; i < DatTienCuocLe.Count; i++)
                    {
                        tongtien += DatTienCuocLe[i];
                    }
                }
            }
            return tongtien;
        }
    }
}