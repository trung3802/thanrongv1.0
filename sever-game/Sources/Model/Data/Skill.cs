namespace TienKiemV2Remastered.Model.Data
{
    public class Skill
    {
        public short Id { get; set; }
        public short EffectHappenOnMob { get; set; }
        public byte NumEff { get; set; }
        public short[][] SkillStand { get; set; }
        public short[][] SkillFly { get; set; }
    }
}