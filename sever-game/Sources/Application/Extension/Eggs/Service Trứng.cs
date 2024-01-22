using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Ấp_trứng
{
    public class Service_Trứng
    {
        public static Message Trứng_rồng_hắc_long(ICharacter character)
        {
           
                var charReal = (Character)character;
                var seconds = (character.InfoChar.ThoiGianTrungMaBu - ServerUtils.CurrentTimeMillis()) / 1000;
                if (seconds < 0)
                {
                    seconds = 0;
                }
                try
                {
                    var message = new Message(-122);
                    message.Writer.WriteShort(50); //id quả trứng
                    message.Writer.WriteByte(1); // số lượng trứng
                    message.Writer.WriteShort(15073); //ảnh của trứng
                    message.Writer.WriteByte(0);// index qủa trứng
                    message.Writer.WriteInt((int)seconds);//thời gian trứng nở
                    return message;
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error TrungMaBu in Service.cs: {e.Message} \n {e.StackTrace}", e);
                    return null;
                }
            
        }
        public static Message Trứng_rồng_thanh_long(ICharacter character)
        {

            var charReal = (Character)character;
            var seconds = (character.InfoChar.ThoiGianTrungMaBu - ServerUtils.CurrentTimeMillis()) / 1000;
            if (seconds < 0)
            {
                seconds = 0;
            }
            try
            {
                var message = new Message(-122);
                message.Writer.WriteShort(79); //id quả trứng
                message.Writer.WriteByte(1); // số lượng trứng
                message.Writer.WriteShort(15074); //ảnh của trứng
                message.Writer.WriteByte(0);// index qủa trứng
                message.Writer.WriteInt((int)seconds);//thời gian trứng nở
                return message;
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error TrungMaBu in Service.cs: {e.Message} \n {e.StackTrace}", e);
                return null;
            }

        }
    }
}
