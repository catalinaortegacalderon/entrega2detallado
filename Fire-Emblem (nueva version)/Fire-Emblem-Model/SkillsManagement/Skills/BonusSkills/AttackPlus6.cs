using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class AttackPlus6 : Skill
{
    public AttackPlus6() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Atk, 6) };
    }
}