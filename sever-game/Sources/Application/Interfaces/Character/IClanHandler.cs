using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Model.Info;

namespace TienKiemV2Remastered.Application.Interfaces.Character
{
    public interface IClanHandler
    {
        Clan Clan { get; set; }
        void Update(int id);
        void Flush();
        bool AddMember(Model.Character.Character character, int role = 0, bool isFlush = true);
        void AddCharacterPea(CharacterPea characterPea);
        bool RemoveMember(int id);
        ClanMember GetMember(int id);
        ClanMessage GetMessage(int id);
        void SendMessage(Message message);
        void UpdateClanId();
        void SendUpdateClan();
        void Chat(ClanMessage message);
    }
}