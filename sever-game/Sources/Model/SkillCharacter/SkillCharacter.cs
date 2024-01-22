namespace TienKiemV2Remastered.Model.SkillCharacter
{
    public class SkillCharacter
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public long CoolDown { get; set; }
        public int Point { get; set; }
        public short CurrExp{get;set;}

        public SkillCharacter()
        {
            CoolDown = 0;
            Point = 1;
            CurrExp = 0;
        }

        public SkillCharacter(int id, int skillId)
        {
            Id = id;
            SkillId = skillId;
            CoolDown = 0;
            Point = 1;
            CurrExp = 0;
        }
    }
}