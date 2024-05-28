using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class DefensePlus5 : Skill
{
    public DefensePlus5() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Def, 5) };
    }
}