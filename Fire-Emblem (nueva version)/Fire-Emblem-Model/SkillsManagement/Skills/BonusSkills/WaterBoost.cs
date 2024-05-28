using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class WaterBoost : Boost
{
    public WaterBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Res, 6); 
    }
}