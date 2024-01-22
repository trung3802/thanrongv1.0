using System;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;

namespace TienKiemV2Remastered.Model.Info
{
    public class ClanMember
    {
        public int Id { get; set; }
        public short Head { get; set; }
       
        public short Body { get; set; }
        public short Leg { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public long Power { get; set; }
        public int Cho_đậu { get; set; }
        public int Nhận_đậu { get; set; }
        public int Capsule_Bang { get; set; }
        public int Capsule_Cá_Nhân { get; set; }
        public int JoinTime { get; set; } 
        public DateTime DateJoin { get; set; }
        public int LastRequest { get; set; }  
        public long DelayPea { get; set; }  

        public ClanMember()
        {
            Name = "";
        }

        public ClanMember(Character.Character character)
        {
            Id = character.Id;
            Head = character.GetHead(false);
            Body = character.GetBody(false);
            Leg = character.GetLeg(false);
            Name = character.Name;
            Role = 0;
            Power = character.InfoChar.Power;
            Cho_đậu = 0;
            Nhận_đậu = 0;
            Capsule_Bang = 0;
            Capsule_Cá_Nhân = 0;
            LastRequest = 0;
            JoinTime = (int)ServerUtils.CurrentTimeMillis()/10000;

        }
        public ICharacter GetMember(int id){
            if (Id == id){
                var ICharacter = ClientManager.Gi().GetCharacter(id);
                return ICharacter;
            }
            return null;
        }
    }
}