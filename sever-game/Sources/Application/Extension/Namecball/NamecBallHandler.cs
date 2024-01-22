using TienKiemV2Remastered.Application.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Main;

namespace TienKiemV2Remastered.Application.Extension.Namecball
{
    public class NamecBallHandler
    {
        public class DataNgocRongNamek
        {
            public long DelayWish = 600000 + ServerUtils.CurrentTimeMillis();
            public long DelayAction = 60000 + ServerUtils.CurrentTimeMillis();
            public int IdNamekBall { get; set; }
            public DataNgocRongNamek()
            {
                IdNamekBall = -1;
            }
            public Boolean AlreadyPick(Character character)
            {
                return character.DataNgocRongNamek.IdNamekBall != -1;
            }
            public Boolean CanWish(Character character)
            {
                return (ServerUtils.CurrentTimeMillis() - character.DataNgocRongNamek.DelayWish) <= 0; 
                
            }
        }
        
    }
    
}
