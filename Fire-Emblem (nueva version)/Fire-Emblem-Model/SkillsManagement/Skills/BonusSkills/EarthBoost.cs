using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class EarthBoost : Boost
{
    public EarthBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Def, 6); 
    }
}