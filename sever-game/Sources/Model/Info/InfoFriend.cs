using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;

namespace TienKiemV2Remastered.Model.Info
{
    public class InfoFriend
    {
        public int Id { get; set; } 
        public short Head { get; set; }
        public short Body { get; set; }
        public short Leg { get; set; }
        public sbyte Bag { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public long Power { get; set; }
        public InfoFriend()
        {
            Id = -1;
            Head = -1;
            Leg = -1;
            Body = -1;
            Bag = -1;
            Name = "";
            IsOnline = true;
            Power = 0;
        }

        public InfoFriend(Character.Character character)
        {
            Id = character.Id;
            Head = character.GetHead(false);
            Leg = character.GetLeg(false);
            Body = character.GetBody(false);
            Bag = character.InfoChar.Bag;
            Name = character.Name;
            IsOnline = true;
            Power = character.InfoChar.Power;
        }
        public InfoFriend(ICharacter character)
        {
            Id = character.Id;
            Head = character.GetHead(false);
            Leg = character.GetLeg(false);
            Body = character.GetBody(false);
            Bag = character.InfoChar.Bag;
            Name = character.Name;
            IsOnline = true;
            Power = character.InfoChar.Power;
        }
    }
}