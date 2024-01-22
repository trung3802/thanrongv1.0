using TienKiemV2Remastered.Application.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio
{
    public class Mabu12hService
    {
        // send the luc mabu , info = text the luc, the luc = the luc cua Char, max The Luc la gioi han the luc, second la tg hien bang the luc
        public static Message sendPowerInfo(string info, short Theluc,short maxTheLuc, short seccond) 
        {
            var msg = new Message(-115);
            msg.Writer.WriteString(info); 
            msg.Writer.WriteShort(Theluc);
            msg.Writer.WriteShort(maxTheLuc);
            msg.Writer.WriteShort(seccond);
            return msg;
        }
        public static Message NoTrungMabu(byte temp)
        {
            var msg = new Message(-117);
            msg.Writer.WriteByte(temp); // 100 là giựt giựt , đùm đùm
            return msg;
        }   
    }
}
