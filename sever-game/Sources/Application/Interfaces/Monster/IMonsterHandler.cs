using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;

namespace TienKiemV2Remastered.Application.Interfaces.Monster
{
    public interface IMonsterHandler
    {
        IMonster Monster { get; set; }
        void SetUpMonster();
        void Recovery();
        int UpdateHp(long damage, int charId, bool isMaxHp = false);  
        void LeaveItem(ICharacter character);
        int PetAttackMonster(IMonster monster);  
        void PetAttackPlayer(ICharacter character);  
        void MonsterAttack(ICharacter temp, ICharacter character);
        void Update();
        Message Hidru(byte type);
        void AddPlayerAttack(ICharacter character, int damage);
        void RemoveTroi(int charId);
        void RemoveEffect(long timeServer, bool globalReset = false);
    }
}