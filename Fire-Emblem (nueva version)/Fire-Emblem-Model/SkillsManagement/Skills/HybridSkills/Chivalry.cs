using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class Chivalry : Skill
{
    public Chivalry() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AndCondition([new MyUnitStartsCombatCondition(), new OpponentHasFullHpCondition()]);
        this.Conditions[1] = new AndCondition([new MyUnitStartsCombatCondition(), new OpponentHasFullHpCondition()]);
        this.Effects = new Effect[2];
        this.Effects[0] = new AbsolutDamageReductionEffect(2);
        this.Effects[1] = new ExtraDamageReductionEffect(2, DamageEffectCategory.All);
    }
    
}