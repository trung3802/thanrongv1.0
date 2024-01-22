using TienKiemV2Remastered.Model.Monster;

namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class Egg
    {
        public MonsterPet Monster{ get; set; }
        public long Time { get; set; }

        public Egg()
        {
            Monster = null;
            Time = -1;
        }
    }
}