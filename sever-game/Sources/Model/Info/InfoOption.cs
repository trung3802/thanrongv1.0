namespace TienKiemV2Remastered.Model.Info
{
    public class InfoOption
    {
        public int PhanPercentSatThuong { get; set; }
        public int PhanTramXuyenGiapChuong { get; set; }
        public int PhanTramXuyenGiapCanChien { get; set; }
        public int PhanTramNeDon { get; set; }
        public int PhanTramVangTuQuai { get; set; }
        public int PhanTramTNSM { get; set; }
        public int PhanTramHutHP { get; set; }
        public int PhanTramHutKI { get; set; }
        public int PercentChinhXac { get; set; }
        public int PhanTramSatThuongChiMang { get; set; }
        public int VoHieuHoaChuong { get; set; }
        public bool HieuUngLua { get; set; }
        public bool QuanBoi { get; set; }
        public bool X2TiemNang { get; set; }
        public int PhanTramTangSatThuongDam { get; set; }
        public InfoOption()
        {
            PhanPercentSatThuong = 0;
            PhanTramXuyenGiapChuong = 0;
            PhanTramXuyenGiapCanChien = 0;
            PhanTramNeDon = 0;
            PhanTramVangTuQuai = 0;
            PhanTramTNSM = 0;
            PhanTramHutHP = 0;
            PhanTramHutKI   = 0;
            PercentChinhXac = 0;
            PhanTramSatThuongChiMang = 0;
            VoHieuHoaChuong = 0;
            HieuUngLua = false;
            QuanBoi = false;
            X2TiemNang = false;
            PhanTramTangSatThuongDam = 0;
        }

        public void Reset()
        {
            PhanTramTangSatThuongDam = 0;
            X2TiemNang = false;
            HieuUngLua = false;
            PhanPercentSatThuong = 0;
            PhanTramXuyenGiapChuong = 0;
            PhanTramXuyenGiapCanChien = 0;
            PhanTramNeDon = 0;
            PhanTramVangTuQuai = 0;
            PhanTramTNSM = 0;
            PhanTramHutKI  = 0;
            PhanTramHutHP= 0;
            PercentChinhXac = 0;
            PhanTramSatThuongChiMang = 0;
            VoHieuHoaChuong = 0;
            QuanBoi = false;

        }
    }
}