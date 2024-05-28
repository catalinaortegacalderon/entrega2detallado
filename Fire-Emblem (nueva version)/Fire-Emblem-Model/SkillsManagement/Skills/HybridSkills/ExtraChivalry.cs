using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class ExtraChivalry : Skill
{
    public ExtraChivalry() : base()
    {
        this.Conditions = new Condition[4];
        this.Conditions[0] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[1] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[2] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[3] = new AlwaysTrueCondition();
        this.Effects = new Effect[4];
        this.Effects[0] = new ChangeOpponentsStatsInEffect(StatType.Atk, -5);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Def, -5);
        this.Effects[2] = new ChangeOpponentsStatsInEffect(StatType.Spd, -5);
        this.Effects[3] = new PercentualDamageReductionConsideringOpponentsHpEffect(DamageEffectCategory.All);
    }
}