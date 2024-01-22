using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Menu;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Main;

namespace TienKiemV2Remastered.Application.Extension.Transfaction.Thách_Đấu
{
    public class Thách_Đấu
    {
        public int Gold { get; set; }
        public Boolean isChallenge { get; set; }
        public int PlayerChallengeID { get; set; }
        public long CurrTimeChallenge { get; set; }
        public Thách_Đấu()
        {
            Gold = -1;
            isChallenge = false;
            PlayerChallengeID = -1;
            CurrTimeChallenge = 300000 + ServerUtils.CurrentTimeMillis();
        }
        public void SetTypeCombat(Character character,int typePK){
            character.InfoChar.TypePk = (sbyte)typePK;
            character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, typePK));
        }
        public void SetStatusEnd(Character character){
            SetTypeCombat(character, 0);
            PlayerChallengeID = -1;
            isChallenge = false;
            Gold = -1;
        }
        public void SetStatusChallenge(Character character, Character charChallenge, int gold){
            SetTypeCombat(character, 3);
            SetTypeCombat(charChallenge, 3);
            PlayerChallengeID = charChallenge.Id;
            charChallenge.Challenge.PlayerChallengeID = character.Id;
            isChallenge = true;
            charChallenge.Challenge.isChallenge = true;
            charChallenge.Challenge.Gold = gold;
            character.Challenge.Gold = gold;
        }
        public void Challenge(){

        }
        public void Runtime(Character character, long timeserver){
            if (timeserver > CurrTimeChallenge){
                var player = (Character)ClientManager.Gi().GetCharacter(PlayerChallengeID);
                if (character.InfoChar.Hp >= player.InfoChar.Hp){
                    var gold = (player.Challenge.Gold - (character.Challenge.Gold / 100)) + (player.Challenge.Gold - (character.Challenge.Gold / 100));
                    character.CharacterHandler.SendMessage(Service.ServerMessage($"Đối thủ đã kiệt sức,bạn đã nhận được {gold} vàng"));
                    character.PlusGold(gold);
                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                    SetStatusEnd(character);
                    SetStatusEnd(player);
                }
            }
        }
    }
}
