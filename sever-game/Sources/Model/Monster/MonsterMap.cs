
using TienKiemV2Remastered.Application.Handlers.Monster;
using TienKiemV2Remastered.Model.ModelBase;

namespace TienKiemV2Remastered.Model.Monster
{
    public class MonsterMap : MonsterBase
    {
        public MonsterMap()
        {
            IsMobMe = false;
            IsDie = false;
            MonsterHandler = new MonsterMapHandler(this);
        }
    }
}