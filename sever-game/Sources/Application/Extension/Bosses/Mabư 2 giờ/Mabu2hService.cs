using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Map;

namespace TienKiemV2Remastered.Application.Extension.Bosses.Mabu2Gio
{
    public class Mabu2hService
    {
		// case 52: -------------------------------------------------------------
			//	sbyte b = msg.reader().readByte();
			//	if (b == 1)
			//	{
			//		int num = msg.reader().readInt();
			//		if (num == Char.myCharz().charID)
			//		{
			//			Char.myCharz().setMabuHold(m: true);
			//			Char.myCharz().cx = msg.reader().readShort();
			//			Char.myCharz().cy = msg.reader().readShort();
			//		}
			//		else
			//		{
			//			Char @char = GameScr.findCharInMap(num);
			//			if (@char != null)
			//			{
			//				@char.setMabuHold(m: true);
			//				@char.cx = msg.reader().readShort();
			//				@char.cy = msg.reader().readShort();
			//			}
			//		}
			//	}
			//	if (b == 0)
			//	{
			//		int num2 = msg.reader().readInt();
			//		if (num2 == Char.myCharz().charID)
			//		{
			//			Char.myCharz().setMabuHold(m: false);
			//		}
			//		else
			//		{
			//			GameScr.findCharInMap(num2)?.setMabuHold(m: false);
			//		}
			//	}
			//	if (b == 2)
			//	{
			//		int charId = msg.reader().readInt();
			//		int id = msg.reader().readInt();
			//		((Mabu)GameScr.findCharInMap(charId)).eat(id);
			//	}
			//	if (b == 3)
			//	{
			//		GameScr.mabuPercent = msg.reader().readByte();
			//	}
			//	break;
			//}
	    public static Message SendSkillMabu(int IdMabu, short skillId, short x, short y, Zone zone)
        {
			var msg = new Message(51);
			msg.Writer.WriteInt(IdMabu);
			msg.Writer.WriteShort(skillId);
			msg.Writer.WriteShort(x);
			msg.Writer.WriteShort(y);
			msg.Writer.WriteByte(zone.Characters.Count);
			for (int i = 0; i < zone.Characters.Count; i++)
			{
                msg.Writer.WriteInt(zone.ZoneHandler.CharacterInMap()[i].Id);
                msg.Writer.WriteInt(1000);
				Server.Gi().Logger.Print("Id Mabu: " + IdMabu + " -SkillId:" + skillId + " -X: " + x + " -Y: " + y + " -char: " + zone.Characters.Count, "red");
			}
			return msg;
        }
        public static Message SendMabu0(int charId) // thoat khoi trung mabu
        {
            var msg = new Message(52);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteInt(charId);
            return msg;
        }
        public static Message SendMabu1(int charId, short x, short y) // bi bat vao trung mabu
        {
            var msg = new Message(52);
            msg.Writer.WriteByte(1);
            msg.Writer.WriteInt(charId);
            msg.Writer.WriteShort(x);
            msg.Writer.WriteShort(y);
            return msg;
        }
        public static Message SendMabu2(int MabuId, int charIdEat) // bi an
        {
            var msg = new Message(52);
            msg.Writer.WriteByte(2);
            msg.Writer.WriteInt(MabuId);
            msg.Writer.WriteInt(charIdEat);
            return msg;
        }
        public static Message SendMabu3(int percentMabu) // mau cua mabu
        {
            var msg = new Message(52);
            msg.Writer.WriteByte(3);
            msg.Writer.WriteByte(percentMabu);
            return msg;
        }
    }
}
