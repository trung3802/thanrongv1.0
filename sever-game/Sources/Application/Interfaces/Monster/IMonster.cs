using System.Collections.Generic;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;

namespace TienKiemV2Remastered.Application.Interfaces.Monster
{
    public interface IMonster
    {
        IMonsterHandler MonsterHandler { get; set; }
        short Id { get; set; }
        ICharacter Character { get; set; }
        int IdMap { get; set; }
        short X { get; set; }
        short Y { get; set; }
        byte Status { get; set; }
        int Level { get; set; }
        byte LvBoss { get; set; }
        bool IsBoss { get; set; }
        Zone Zone { get; set; } 
        long OriginalHp { get; set; }
        long Hp { get; set; }
        long HpMax { get; set; }
        bool IsFire { get; set; }
        bool IsMobMe { get; set; }
        bool IsIce { get; set; }
        bool IsWind { get; set; }
        bool IsDisable { get; set; }
        bool IsDontMove { get; set; }
        bool IsDie { get; set; }
        bool IsRefresh { get; set; }
        long TimeRefresh { get; set; }
        sbyte Sys { get; set; }
        int MaxExp { get; set; }
        int Damage { get; set; }
        long DelayFight { get; set; }
        long TimeAttack { get; set; }
        long TimeHp { get; set; }
        List<int> CharacterAttack { get; set; }
        Dictionary<int, int> SessionAttack { get; set; }
        int TypeHidru { get; set; }
        long DelayHidru { get; set; }
        InfoSkill InfoSkill { get; set; }
        int LeaveItemType { get; set; }
    }
}