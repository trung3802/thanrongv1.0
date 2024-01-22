using TienKiemV2Remastered.Application.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension
{
    public class EffectCharacter
    {
        public static Message addEff(short id, short x, short y, byte layer, byte loop, short loopCount)
        {
           
                var msg = new Message(113);
                msg.Writer.WriteByte(loop); // vòng lặp | -1 vô hạn
                msg.Writer.WriteByte(layer); // 0 hiện sau nv - 1 trước nv
                msg.Writer.WriteByte(id); // id eff
                msg.Writer.WriteShort(x); // tọa độ x
                msg.Writer.WriteShort(y); // tọa độ y
                msg.Writer.WriteShort(loopCount); // thời gian lặp 
                return msg;
               //sendMessAllPlayerInMap(player.map, msg);
        }

        public static Message sendInfoEffChar(short charId,short id, byte layer, int loop, short loopCount, byte isStand)
        {
           
                var msg = new Message(-128);
                msg.Writer.WriteByte(0); // 0 là add effect
                msg.Writer.WriteInt((int)charId);
                msg.Writer.WriteShort(id); // id effect
                msg.Writer.WriteByte(layer); // 0 hiện sau nv - 1 hiện trước nv
                msg.Writer.WriteByte(loop); // để là -1 sẽ lặp vô thời hạn
                msg.Writer.WriteShort(loopCount); // thời gian lặp
                msg.Writer.WriteByte(isStand); // 0 là hiện khi đứng im , 1 là luôn luôn hiện
                return msg;
              //  sendMessAllPlayerInMap(player.map, msg);
        }

        public static Message removeEffChar(short charId,short id)
        {
            
                var msg = new Message(-128);
                msg.Writer.WriteByte(1);
                msg.Writer.WriteInt((int)charId);
                msg.Writer.WriteShort(id);
                return msg;
             //   sendMessAllPlayerInMap(player.map, msg);          
        }
    }
}
