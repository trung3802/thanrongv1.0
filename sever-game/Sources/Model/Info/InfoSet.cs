namespace TienKiemV2Remastered.Model.Info
{
    public class InfoSet
    {
        public bool IsFullSetThanLinh { get; set; }

        public bool IsFullSetKirin { get; set; }
        public bool IsFullSetSongoku { get; set; }
        public bool IsFullSetThienXinHang { get; set; }

        public bool IsFullSetNappa { get; set; }
        public bool IsFullSetKakarot { get; set; }
        public bool IsFullSetCadic { get; set; }

        public bool IsFullSetOcTieu { get; set; }
        public bool IsFullSetPicolo { get; set; }
        public bool IsFullSetPikkoro { get; set; }
        public bool IsFullSetZelot {get;set;}
        public bool IsFullSetTinhAn { get; set; }
        public bool IsFullSetNguyetAn { get; set; }
        public bool IsFullSetNhatAn { get; set; }
        public bool IsFullSetHuyDiet { get; set; }
        public bool IsQuanBoi { get; set; }
        public InfoSet()
        {
            IsFullSetThanLinh = false;
            IsFullSetHuyDiet = false;
            IsFullSetKirin = false;
            IsFullSetSongoku = false;
            IsFullSetThienXinHang = false;

            IsFullSetNappa = false;
            IsFullSetKakarot = false;
            IsFullSetCadic = false;
            IsQuanBoi = false;
            IsFullSetOcTieu = false;
            IsFullSetPicolo = false;
            IsFullSetPikkoro = false;
            IsFullSetZelot = false;
            IsFullSetNhatAn = false;
            IsFullSetTinhAn = false;
            IsFullSetNguyetAn = false;
        }

        public void Reset()
        {
            IsFullSetThanLinh = false;

            IsFullSetKirin = false;
            IsFullSetSongoku = false;
            IsFullSetThienXinHang = false;
            IsQuanBoi = false;
            IsFullSetNappa = false;
            IsFullSetKakarot = false;
            IsFullSetCadic = false;
            IsFullSetHuyDiet = false;
            IsFullSetOcTieu = false;
            IsFullSetPicolo = false;
            IsFullSetPikkoro = false;
            IsFullSetZelot = false;
            IsFullSetNhatAn = false;
            IsFullSetTinhAn = false;
            IsFullSetNguyetAn = false;
        }
    }
}