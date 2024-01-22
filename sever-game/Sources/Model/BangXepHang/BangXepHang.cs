namespace TienKiemV2Remastered.Model.BangXepHang
{
    public class BangXepHang
    {
        public class TopPower{
        public int I { get; set; }
        public string Name { get; set; }
        public long Power { get; set; }
            public long TotalPotential { get; set; }

            public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
        public int Id{get;set;}
        }
        public class TopNap{
public int I { get; set; }
        public string Name { get; set; }
        public int TongNap { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
            public int Id { get; set; }
        }
        public class TopDisciple
        {
            public int I { get; set; }
            public string Name { get; set; }
            public long Power { get; set; }
            public int Head { get; set; }
            public int Body { get; set; }
            public int Leg { get; set; }
            public int Id { get; set; }
            public string MasterName { get; set; }
        }
        public class TopTask{
            public int I { get; set; }
            public int Id{get;set;}
        public string Name { get; set; }
        public short TaskId { get; set; }
        public short Index { get; set; }
            public short Count { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
            public long Time { get; set; }
        }
        public class TopClanCDRD{
        public int Top { get; set; }
        public string ClanName { get; set; }
        public int Level { get; set; }
        public int Second{get;set;}
        public string LeaderName{get;set;}
        public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
        }
        public class TopClanKhiGas{
        public int Top { get; set; }
        public string ClanName { get; set; }
        public int Level { get; set; }
        public int Second{get;set;}
        public string LeaderName{get;set;}
        public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
        }
        public class TopEvent
        {
            public int Top { get; set; }
            public string Name { get; set; }
            public int Point { get; set; }
            public int Head { get; set; }
            public int Body { get; set; }
            public int Leg { get; set; }
            public int PlId { get; set; }
        }
        public class TopQuayThuong
        {
            public int Top { get; set; }
            public string Name { get; set; }
            public int Point { get; set; }
            public int Head { get; set; }
            public int Body { get; set; }
            public int Leg { get; set; }
            public int PlId { get; set; }
        }
        public class TopSanBoss
        {
            public int Top { get; set; }
            public string Name { get; set; }
            public int Point { get; set; }
            public int Head { get; set; }
            public int Body { get; set; }
            public int Leg { get; set; }
            public int PlId { get; set; }
        }
    }
}