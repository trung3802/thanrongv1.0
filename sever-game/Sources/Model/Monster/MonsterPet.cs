
using System.Security.Policy;
using TienKiemV2Remastered.Application.Handlers.Monster;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Model.ModelBase;
using Zone = TienKiemV2Remastered.Model.Map.Zone;

namespace TienKiemV2Remastered.Model.Monster
{
    public class MonsterPet : MonsterBase
    {
        public MonsterPet(ICharacter character, Map.Zone zone, short template, long hp, int damage)
        {
            IdMap = (short)character.Id;
            Id = template;
            X = character.InfoChar.X;
            Y = character.InfoChar.Y;
            Zone = zone;
            Character = character;
            IsMobMe = true;
            HpMax = hp;
            OriginalHp = hp;
            Hp = hp;
            Damage = damage;
            IsDie = false;
            MonsterHandler = new MonsterPetHandler(this);
        }
    }
}