using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Application.Constants;

namespace TienKiemV2Remastered.Application.Interfaces.Map
{
    public interface IClientManager
    {
        int CurrentPlayers { get; set; }
        Player GetPlayer(int id);
        ICharacter GetCharacter(int id);
        ICharacter GetCharacter(string name);
        void SendMessage(Message message);
        void Add(ISession_ME session);
        void Add(Player player);
        void Add(ICharacter character);
        void Remove(ISession_ME session);
        void Remove(Player player);
        void Remove(ICharacter character);
        void KickSession(ISession_ME session);
        void Clear();
    }
}