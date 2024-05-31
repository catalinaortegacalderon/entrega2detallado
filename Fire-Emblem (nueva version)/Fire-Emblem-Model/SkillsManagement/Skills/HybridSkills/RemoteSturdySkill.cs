using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class RemoteSturdySkill : Skill
{
    public RemoteSturdySkill() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
        this.Conditions[1] = new MyUnitStartsCombatCondition();
        this.Conditions[2] = new MyUnitStartsCombatCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Atk,7);
        this.Effects[1] = new ChangeStatsInEffect(StatType.Def,10);
        this.Effects[2] = new PercentualDamageReductionEffect(0.7, DamageEffectCategory.FirstAttack);
    }
}