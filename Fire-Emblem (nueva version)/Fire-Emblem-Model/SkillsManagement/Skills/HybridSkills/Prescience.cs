using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class Prescience : Skill
{
    public Prescience() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new OrCondition([new MyUnitStartsCombatCondition(), new MyUnitUsesCertainWeaponsCondition([Weapon.Magic, Weapon.Bow])]);
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeOpponentsStatsInEffect(StatType.Atk,-5);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Res,-5);
        this.Effects[2] = new PercentualDamageReductionEffect(0.7, DamageEffectCategory.FirstAttack);
    }
}