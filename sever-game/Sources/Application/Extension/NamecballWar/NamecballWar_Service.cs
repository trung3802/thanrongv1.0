using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Clan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Threading;
using System.Runtime.InteropServices;

namespace TienKiemV2Remastered.Application.Extension.NamecballWar
{
    public class NamecballWar_Service
    {
        #region Information
        // win + 10, draw + 5, lose + 1, 1 ball + 1 (max 20) if lose / 2 else if draw / 3
        // effect end : 0 lose, 1 win, 2 draw
        #endregion
        
        #region chientruong_namek
        public static void SendMessageTeam(NamecballWar_Team namecballWar_Team, Message msg)
        {
            switch (namecballWar_Team)
            {
                case NamecballWar_Team.Cadic:
                    {
                        NamecballWar.Cadic.IdMember.ForEach(id =>
                        {
                            var character = ClientManager.Gi().GetCharacter(id);
                            character.CharacterHandler.SendMessage(msg);
                        });
                    }
                    break;
                case NamecballWar_Team.Fide:
                    {
                        NamecballWar.Fide.IdMember.ForEach(id =>
                        {
                            var character = ClientManager.Gi().GetCharacter(id);
                            character.CharacterHandler.SendMessage(msg);
                        });
                    }
                    break;
                case NamecballWar_Team.All:
                    {
                        NamecballWar.Cadic.IdMember.ForEach(id =>
                        {
                            var character = ClientManager.Gi().GetCharacter(id);
                            character.CharacterHandler.SendMessage(msg);
                        });
                        NamecballWar.Fide.IdMember.ForEach(id =>
                        {
                            var character = ClientManager.Gi().GetCharacter(id);
                            character.CharacterHandler.SendMessage(msg);
                        });
                    }
                    break;
            }
        }
        public static void Send_ChienTruong_Namek(Clan clan, int type, params int[] column)
        {
            
            if (type == 0) clan.ClanHandler.SendMessage(newInfoPhuBan(164,"Ca Đíc","Fide", 7,  900, 7));
            else if (type == 1) clan.ClanHandler.SendMessage(updatePoint(column[0], column[1]));
            else if (type == 2) clan.ClanHandler.SendMessage(AddEffectEnd(column[0]));
            else if (type == 5) clan.ClanHandler.SendMessage(updateTime(60));
            else clan.ClanHandler.SendMessage(updateLife((byte)column[0], (byte)column[1]));
            Server.Gi().Logger.Print("Chien truong namec msg: 20 type: " + type);
        }
        public static Message newInfoPhuBan(short idMap, string nameTeam, string nameTeam2, int maxPoint, short second, byte maxLife)
        {
            var msg = new Message(20);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteShort(idMap);
            msg.Writer.WriteUTF(nameTeam);
            msg.Writer.WriteUTF(nameTeam2);
            msg.Writer.WriteInt(maxPoint);
            msg.Writer.WriteShort(second);
            msg.Writer.WriteByte(maxLife);
            return msg;
        }
        public static Message updatePoint(int pointTeam1, int pointTeam2)
        {
            var msg = new Message(20);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(1);
            msg.Writer.WriteInt(pointTeam1);
            msg.Writer.WriteInt(pointTeam2);
            return msg;
        }
        public static Message AddEffectEnd(int type)
        {
            var msg = new Message(20);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(2);
            msg.Writer.WriteByte(type);
            return msg;
        }
        public static Message updateTime(short second)
        {
            var msg = new Message(20);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(5);
            msg.Writer.WriteShort(second);
            return msg;
        }
        public static Message updateLife(byte lifeTeam1, byte lifeTeam2)
        {
            var msg = new Message(20);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(4);
            msg.Writer.WriteByte(lifeTeam1);
            msg.Writer.WriteByte(lifeTeam2);
            return msg;
        }
        #endregion
    }
}
