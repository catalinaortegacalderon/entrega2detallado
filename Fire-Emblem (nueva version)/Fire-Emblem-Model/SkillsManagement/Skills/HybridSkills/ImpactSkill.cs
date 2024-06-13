using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.CombatEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class ImpactSkill: Skill
{
    public ImpactSkill(StatType firstStat, StatType secondStat)
    {
        Conditions = new Condition[1];
        Conditions[0] = new MyUnitStartsCombatCondition();
        Conditions[0] = new MyUnitStartsCombatCondition();
        Conditions[0] = new MyUnitStartsCombatCondition();

        Effects = new Effect[1];
        Effects[0] = new ChangeStatsInEffect(firstStat, 6);
        Effects[0] = new ChangeStatsInEffect(secondStat, 10);
        Effects[0] = new OpponentFollowUpDenialEffect();
    }
}