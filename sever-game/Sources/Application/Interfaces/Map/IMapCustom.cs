using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Interfaces.Character;

namespace TienKiemV2Remastered.Application.Interfaces.Map
{
    public interface IMapCustom
    {
        int Id { get; set; }
        IList<Threading.Map> Maps { get; set; }
        IList<ICharacter> Characters { get; set; }
        bool IsClear { get; set; }
        bool IsOutMap { get; set; }
        long Time { get; set; }
        object LOCK { get; set; }
        void Update();
        void InitMap();

        void Clear();

        void AddCharacter(ICharacter character);

        void RemoveCharacter(ICharacter character);

        bool IsClose();

        Threading.Map GetMapById(int id);

        Threading.Map GetMapByIndex(int index);
    }
}