using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class SpeedPlus5 : Skill
{
    public SpeedPlus5() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Spd, 5) };
    }
}