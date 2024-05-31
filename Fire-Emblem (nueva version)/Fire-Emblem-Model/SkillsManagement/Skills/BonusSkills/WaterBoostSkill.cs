using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class WaterBoostSkill : BoostSkill
{
    public WaterBoostSkill() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Res, 6); 
    }
}