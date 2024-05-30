using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class WardingBlowSkill : Skill
{
    public WardingBlowSkill() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Res, 8);
    }
}