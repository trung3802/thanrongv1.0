namespace TienKiemV2Remastered.Model.Info
{
    public class InfoBuff
    {
        public short ThucAnId { get; set; }
        public long ThucAnTime { get; set; }

        public bool CuongNo { get; set; }
        public long CuongNoTime { get; set; }

        public bool BinhChuaCommeson { get; set; }
        public long BinhChuaCommesonTime { get; set; }

        public bool BoHuyet { get; set; }
        public long BoHuyetTime { get; set; }

        public bool XiMuoiHoaDao { get; set; }
        public long XiMuoiHoaDaoTime { get; set; }
        
        public bool XiMuoiHoaMai { get; set; }
        public long XiMuoiHoaMaiTime { get; set; }
        public bool BoKhi { get; set; }
        public long BoKhiTime { get; set; }

        public bool effRongXuong { get; set; }
        public long effRongXuongTime { get; set; }
        public bool GiapXen { get; set; }
        public long GiapXenTime { get; set; }

        public bool AnDanh { get; set; }
        public long AnDanhTime { get; set; }
        public bool CuongNo2 { get; set; }
        public long CuongNoTime2{ get; set; }

        public bool BoHuyet2 { get; set; }
        public long BoHuyetTime2 { get; set; }

        public bool BoKhi2 { get; set; }
        public long BoKhiTime2 { get; set; }

        public bool GiapXen2 { get; set; }
        public long GiapXenTime2 { get; set; }

        public bool AnDanh2 { get; set; }
        public long AnDanhTime2 { get; set; }

        public bool MayDoCSKB { get; set; }
        public long MayDoCSKBTime { get; set; }

        public bool CuCarot { get; set; }
        public long CuCarotTime { get; set; }

        public short BanhTrungThuId { get; set; }
        public long BanhTrungThuTime { get; set; }

        public bool isActiveCrit { get; set; }
        public bool isEnchantCrit { get; set; }
        public long delayEnchantCrit { get; set; }
        public long timeEnchantCrit { get; set; }

        public bool isActiveGiap { get; set; }
        public bool isEnchantGiap { get; set; }
        public long delayEnchantGiap { get; set; }
        public long timeEnchantGiap { get; set; }

        public bool KichDucX2 { get; set; }
        public long KichDucX2Time { get; set; }
        public bool KichDucX5 { get; set; }
        public long KichDucX5Time { get; set; }
        public bool KichDucX7 { get; set; }
        public long KichDucX7Time { get; set; }
        public InfoBuff()
        {
            KichDucX2 = false;
            KichDucX2Time = -1;
            KichDucX5 = false;
            KichDucX5Time = -1;
            KichDucX7 = false;
            KichDucX7Time = -1;

            isEnchantCrit = false;
            delayEnchantCrit = -1;
            timeEnchantCrit = -1;
            isActiveCrit = false;

            isActiveGiap = false;
            isEnchantGiap = false;
            delayEnchantGiap = -1;
            timeEnchantGiap = -1;

            BinhChuaCommeson = false;
            BinhChuaCommesonTime = 0;

            ThucAnId = -1;
            ThucAnTime = 0;

            CuongNo = false;
            CuongNoTime = 0;

            BoHuyet = false;
            BoHuyetTime = 0;

            BoKhi = false;
            BoKhiTime = 0;

            GiapXen = false;
            GiapXenTime = 0;

            effRongXuong = false;
            effRongXuongTime = 0;

            AnDanh = false;
            AnDanhTime = 0;

            CuongNo2 = false;
            CuongNoTime2 = 0;

            BoHuyet2 = false;
            BoHuyetTime2 = 0;

            BoKhi2 = false;
            BoKhiTime2 = 0;

            GiapXen2 = false;
            GiapXenTime2 = 0;

            AnDanh2 = false;
            AnDanhTime2 = 0;

            CuCarot = false;
            CuCarotTime = 0;

            BanhTrungThuId = -1;
            BanhTrungThuTime = 0;
        }
    }
}