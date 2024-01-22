using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Bo_Mong
{
    public class BoMong_Task
    {
        
        public static Message BoMongDAO(Character character)
        {
            Load(character);
            var msg = new Message(-76);
            msg.Writer.WriteByte(0);
            msg.Writer.WriteByte(Cache.Gi().TASK_BO_MONG.Count);
            for (int i = 0; i < Cache.Gi().TASK_BO_MONG.Count; i++)
            {
                var task = Cache.Gi().TASK_BO_MONG.Values.FirstOrDefault(a => a.Id == i);
                msg.Writer.WriteUTF(task.TaskName);
                msg.Writer.WriteUTF(string.Format(task.TaskDescription, ServerUtils.GetMoney(character.DataBoMong.Count[i]), ServerUtils.GetMoney(task.Count)));
                msg.Writer.WriteShort(task.GemCollect);
                msg.Writer.WriteBoolean(character.DataBoMong.isFinish[i]);
                msg.Writer.WriteBoolean(character.DataBoMong.isCollect[i]);
               }
            return msg;
        }
       
        public static void Load(Character character)
        {
            var booleanClan = ClanManager.Get(character.ClanId) != null;
            character.DataBoMong.Count[0] = character.InfoChar.Power;
            character.DataBoMong.Count[1] = character.InfoChar.Power;
            character.DataBoMong.Count[2] = MagicTreeManager.Get(character.Id).Level;
            character.DataBoMong.Count[10] = (booleanClan ? ClanManager.Get(character.ClanId).ClanHandler.GetMember(character.Id).Cho_đậu : 0);
            for (int i = 0; i < Cache.Gi().TASK_BO_MONG.Count; i++)
            {
                var task = Cache.Gi().TASK_BO_MONG.Values.FirstOrDefault(a => a.Id == i);
                if (character.DataBoMong.Count[i] >= task.Count && character.DataBoMong.isCollect[i] == false)
                {
                    character.DataBoMong.isFinish[i] = true;
                }
            }
            
        }
           
    }
}
