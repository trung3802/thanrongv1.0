using System;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Map;

namespace TienKiemV2Remastered.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsOnline { get; set; }
        public bool IsActive { get; set; }
        public byte Status { get; set; }
        public byte Ban { get; set; }
        public int CharId { get; set; }
        public int TongVND { get; set; }
        public int DiemTichNap { get; set; }
        public ICharacter Character { get; set; }
        public ISession_ME Session { get; set; }
        

        public Player()
        {
            
        }

        public Player(ISession_ME session)
        {
            Session = session;
        }
    }
}