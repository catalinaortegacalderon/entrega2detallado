using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class WindBoost : Boost
{
    public WindBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Spd, 6); 
    }
}