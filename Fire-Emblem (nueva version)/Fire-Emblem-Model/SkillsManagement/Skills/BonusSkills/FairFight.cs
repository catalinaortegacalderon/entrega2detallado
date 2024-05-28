using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class FairFight : Skill
{
    public FairFight() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
        this.Conditions[1] = new MyUnitStartsCombatCondition();
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk,6);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Atk, 6);
    }
}