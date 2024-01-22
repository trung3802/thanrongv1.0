using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Threading;

namespace TienKiemV2Remastered.Application.Extension.Bosses.BigBoss
{
    public class BigBossService
    {
        
		public static Message SendBigBossSkill(IMonster monster,byte action, short x= 0, short y = 0)
        {
			var msg = new Message(101);
            msg.Writer.WriteByte(action);
            if (action == 0 || action == 1 || action == 2 || action == 3 || action == 4)
            {
                if (action == 3)
                {

                    // bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
                    // bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
                    // bigBoss2.setFly();
                    msg.Writer.WriteShort(x);
                    msg.Writer.WriteShort(y);
                    Server.Gi().Logger.Print("SET HIDRU FLYING TO X: " + x + " TO Y: " + y, "cyan");
                    return msg;
                }
                else
                {
                    var vCharInMap = monster.Zone.Characters.Count;
                    msg.Writer.WriteByte(vCharInMap);
                    for (int i = 0; i < vCharInMap; i++)
                    {
                        var @char = monster.Zone.ZoneHandler.CharacterInMap()[i];
                        if (@char != null)
                        {
                            msg.Writer.WriteInt(@char.Id);
                            msg.Writer.WriteInt(monster.Damage + (int)@char.HpFull * 5 / 100);
                        }
                    }
                    Server.Gi().Logger.Print("SEND ATK HIDRU: " + vCharInMap, "cyan");
                    return msg;

                    //sbyte b25 = msg.reader().readByte(); Send Count Nhân vật trong Map
                    //Res.outz("CHUONG nChar= " + b25);
                    //Char[] array8 = new Char[b25];
                    //int[] array9 = new int[b25];
                    //for (int num32 = 0; num32 < b25; num32++)
                    //{
                    //    int num33 = msg.reader().readInt();
                    //    Res.outz("char ID=" + num33);
                    //    array8[num32] = null;
                    //    if (num33 != Char.myCharz().charID)
                    //    {
                    //        array8[num32] = GameScr.findCharInMap(num33);
                    //    }
                    //    else
                    //    {
                    //        array8[num32] = Char.myCharz();
                    //    }
                    //    array9[num32] = msg.reader().readInt();
                    //}
                    //bigBoss2.setAttack(array8, array9, b24);
                }
            }
            // Sys 0: Mặc Định
            // Sys 1: Hóa Vàng
            // Sys 2: Cụt Đầu
            switch (action)
            {
                case 5: // Set Cụt Đầu
                    //  bigBoss2.haftBody = true;
                    //  bigBoss2.status = 2;
                    monster.Sys = 2;
                    monster.MonsterHandler.SetUpMonster();
                    Server.Gi().Logger.Print("SET HIDRU CUT DAU", "cyan");
                    return msg;
                case 6: // Set Hóa Vàng
                    // bigBoss2.getDataB2();
                    // bigBoss2.x = msg.reader().readShort();
                    // bigBoss2.y = msg.reader().readShort();
                    msg.Writer.WriteShort(100);
                    msg.Writer.WriteShort(100);
                    monster.Sys = 1;
                    monster.MonsterHandler.SetUpMonster();
                    Server.Gi().Logger.Print("SET HIDRU HOA VANG", "cyan");
                    return msg;
                case 7: // Update Lại Người Attack
                    // bigBoss2.setAttack(null, null, b24);
                    break;
                case 8: // update vị trí mới 
                    // bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
                    // bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
                    // bigBoss2.status = 2;
                    break;
                case 9: // cho bay ra khỏi map rồi quay lại
                    // bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
                    break;
            }
			return msg;
        }
        public static void sendActionBigBoss2(Message msg,IMonster monster ,byte idBoss, int actionBoss, int x = 0, int y = 0)
        {
            msg.Writer.WriteByte(idBoss);
            if (actionBoss == 10)
            {
                msg.Writer.WriteShort(x);
                msg.Writer.WriteShort(y);
                Server.Gi().Logger.Print("ACTION == 10 && DO MOVE TO X: " + x + " | Y: " + y, "red");
            }
            if (actionBoss >= 11 && actionBoss <= 20)
            {
                var vCharInMap = monster.Zone.Characters.Count;
                msg.Writer.WriteInt(vCharInMap);
                for (int i = 0; i < vCharInMap; i++)
                {
                    var @char = monster.Zone.Characters[i];
                    if (@char!=null)
                    {
                        msg.Writer.WriteInt(@char.Id);
                        msg.Writer.WriteInt(monster.Damage + (int)@char.HpFull * 5 / 100);
                    }
                }
                Server.Gi().Logger.Print("ACTION >= 11 && ACTION <= 20 && DO HIT DAMAGE", "red");
            }
            if (actionBoss == 21)
            {
                //Flying
                msg.Writer.WriteInt(x);
                msg.Writer.WriteInt(y);
                Server.Gi().Logger.Print("ACTION == 21 && DO FLYING", "red");
            }
            if (actionBoss == 22)
            {
                //ingored
            }
            if (actionBoss == 23)
            {
                // boss die
            }
            Server.Gi().Logger.Print("Action: " + actionBoss, "red");
        }
public static Message BachTuoc(short x){
    var msg = new Message(102);
    msg.Writer.WriteByte(5);
    msg.Writer.WriteShort(x);
    return msg;
}
        
        public static Message ActionBigBoss(IMonster monster, ICharacter character, int type, int xto)
        {
           
             var msg = new Message(102);
             msg.Writer.WriteByte(type);
            /*if (type == 6)
            //{
            //   bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
            //   break;
            }*/
            #region bigboss2
            if (type == 0 || type == 1 || type == 2 || type == 6)
            {
                if (type == 6)
                {
                    Server.Gi().Logger.Print("SEND TELEPORT XY ROBOT BAO VE" + type, "cyan");
                }
                if (monster.Id == 72)
                {
                    msg.Writer.WriteByte(monster.Zone.Characters.Count);
                    for (int i = 0; i < monster.Zone.Characters.Count; i++)
                    {
                        Character @char = monster.Zone.Characters[i];
                        if (@char != null)
                        {
                            msg.Writer.WriteInt(@char.Id);
                            msg.Writer.WriteInt(monster.Damage + (int)@char.HpFull * 5 / 100);
                        }
                    }
                }
            }
            #endregion

            #region Bạch Tuộc
            if (type == 3 || type == 4 || type == 5 || type == 7)
            {
                /*if (type == 7)
                {
                    bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
                    break;
                }*/
                if (type == 3 || type == 4)
                {
                    if (monster.Id == 71)
                    {
                        msg.Writer.WriteByte(monster.Zone.Characters.Count);
                        for (int i = 0; i < monster.Zone.Characters.Count; i++)
                        {
                            Character @char = monster.Zone.Characters[i];
                            if (@char != null)
                            {
                                msg.Writer.WriteInt(@char.Id);
                                msg.Writer.WriteInt(monster.Damage + (int)@char.HpFull * 5 / 100);
                            }
                        }
                    }
                }
                if (type == 5)
                {
                    msg.Writer.WriteShort(xto);
                }
            }
            if (type > 9 && type < 30)
            {
                sendActionBigBoss2(msg, monster, (byte)monster.Id, type, 100);
            }
            Server.Gi().Logger.Print("TYPE: " + type, "red");
            #endregion
            return msg;
           
        }
            //}
            //     case 101:
            //{
            //	Res.outz("big boss--------------------------------------------------");
            //	BigBoss bigBoss = Mob.getBigBoss();
            //	if (bigBoss == null)
            //	{
            //		break;
            //	}
            //	sbyte b6 = msg.reader().readByte();
            //	if (b6 == 0 || b6 == 1 || b6 == 2 || b6 == 4 || b6 == 3)
            //	{
            //		if (b6 == 3)
            //		{
            //			bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
            //			bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
            //			bigBoss.setFly();
            //		}
            //		else
            //		{
            //			sbyte b7 = msg.reader().readByte();
            //			Res.outz("CHUONG nChar= " + b7);
            //			Char[] array2 = new Char[b7];
            //			int[] array3 = new int[b7];
            //			for (int k = 0; k < b7; k++)
            //			{
            //				int num5 = msg.reader().readInt();
            //				Res.outz("char ID=" + num5);
            //				array2[k] = null;
            //				if (num5 != Char.myCharz().charID)
            //				{
            //					array2[k] = GameScr.findCharInMap(num5);
            //				}
            //				else
            //				{
            //					array2[k] = Char.myCharz();
            //				}
            //				array3[k] = msg.reader().readInt();
            //			}
            //			bigBoss.setAttack(array2, array3, b6);
            //		}
            //	}
            //	if (b6 == 5)
            //	{
            //		bigBoss.haftBody = true;
            //		bigBoss.status = 2;
            //	}
            //	if (b6 == 6)
            //	{
            //		bigBoss.getDataB2();
            //		bigBoss.x = msg.reader().readShort();
            //		bigBoss.y = msg.reader().readShort();
            //	}
            //	if (b6 == 7)
            //	{
            //		bigBoss.setAttack(null, null, b6);
            //	}
            //	if (b6 == 8)
            //	{
            //		bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
            //		bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
            //		bigBoss.status = 2;
            //	}
            //	if (b6 == 9)
            //	{
            //		int num6 = -1000;
            //		bigBoss.yFirst = -1000;
            //		num6 = -1000;
            //		bigBoss.xFirst = -1000;
            //		num6 = -1000;
            //		bigBoss.yTo = -1000;
            //		num6 = -1000;
            //		bigBoss.xTo = -1000;
            //		num6 = -1000;
            //		bigBoss.y = -1000;
            //		bigBoss.x = -1000;
            //	}
            //	break;
            //}
            //case 102:
            //{
            //	sbyte b12 = msg.reader().readByte();
            //	if (b12 == 0 || b12 == 1 || b12 == 2 || b12 == 6)
            //	{
            //		BigBoss2 bigBoss2 = Mob.getBigBoss2();
            //		if (bigBoss2 == null)
            //		{
            //			break;
            //		}
            //		if (b12 == 6)
            //		{
            //			int num14 = -1000;
            //			bigBoss2.yFirst = -1000;
            //			num14 = -1000;
            //			bigBoss2.xFirst = -1000;
            //			num14 = -1000;
            //			bigBoss2.yTo = -1000;
            //			num14 = -1000;
            //			bigBoss2.xTo = -1000;
            //			num14 = -1000;
            //			bigBoss2.y = -1000;
            //			bigBoss2.x = -1000;
            //			break;
            //		}
            //		sbyte b13 = msg.reader().readByte();
            //		Char[] array7 = new Char[b13];
            //		int[] array8 = new int[b13];
            //		for (int num15 = 0; num15 < b13; num15++)
            //		{
            //			int num16 = msg.reader().readInt();
            //			array7[num15] = null;
            //			if (num16 != Char.myCharz().charID)
            //			{
            //				array7[num15] = GameScr.findCharInMap(num16);
            //			}
            //			else
            //			{
            //				array7[num15] = Char.myCharz();
            //			}
            //			array8[num15] = msg.reader().readInt();
            //		}
            //		bigBoss2.setAttack(array7, array8, b12);
            //	}
            //	if (b12 == 3 || b12 == 4 || b12 == 5 || b12 == 7)
            //	{
            //		BachTuoc bachTuoc = Mob.getBachTuoc();
            //		if (bachTuoc == null)
            //		{
            //			break;
            //		}
            //		if (b12 == 7)
            //		{
            //			int num17 = -1000;
            //			bachTuoc.yFirst = -1000;
            //			num17 = -1000;
            //			bachTuoc.xFirst = -1000;
            //			num17 = -1000;
            //			bachTuoc.yTo = -1000;
            //			num17 = -1000;
            //			bachTuoc.xTo = -1000;
            //			num17 = -1000;
            //			bachTuoc.y = -1000;
            //			bachTuoc.x = -1000;
            //			break;
            //		}
            //		if (b12 == 3 || b12 == 4)
            //		{
            //			sbyte b14 = msg.reader().readByte();
            //			Char[] array9 = new Char[b14];
            //			int[] array10 = new int[b14];
            //			for (int num18 = 0; num18 < b14; num18++)
            //			{
            //				int num19 = msg.reader().readInt();
            //				array9[num18] = null;
            //				if (num19 != Char.myCharz().charID)
            //				{
            //					array9[num18] = GameScr.findCharInMap(num19);
            //				}
            //				else
            //				{
            //					array9[num18] = Char.myCharz();
            //				}
            //				array10[num18] = msg.reader().readInt();
            //			}
            //			bachTuoc.setAttack(array9, array10, b12);
            //		}
            //		if (b12 == 5)
            //		{
            //			bachTuoc.move(msg.reader().readShort());
            //		}
            //	}
            //	if (b12 > 9 && b12 < 30)
            //	{
            //		readActionBoss(msg, b12);
            //	}
            //	break;
            //}
        }
}
