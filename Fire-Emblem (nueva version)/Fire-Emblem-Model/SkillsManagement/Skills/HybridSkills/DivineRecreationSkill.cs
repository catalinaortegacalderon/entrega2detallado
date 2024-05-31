using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;
public class DivineRecreationSkill : Skill
{
    public DivineRecreationSkill() : base()
    {
        this.Conditions = new Condition[6];
        this.Conditions[0] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[1] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[2] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[3] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[4] = new OpponentHasHpGreaterThanCondition(0.5);
        this.Conditions[5] = new OpponentHasHpGreaterThanCondition(0.5);
        
        this.Effects = new Effect[6];
        this.Effects[0] = new ChangeOpponentsStatsInEffect(StatType.Spd,-4);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Def,-4);
        this.Effects[2] = new ChangeOpponentsStatsInEffect(StatType.Atk,-4);
        this.Effects[3] = new ChangeOpponentsStatsInEffect(StatType.Res,-4);
        this.Effects[4] = new PercentualDamageReductionEffect(0.7, DamageEffectCategory.FirstAttack);
        this.Effects[5] = new DivineRecreationEffect();
    }
}