using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.AbsolutDamageReductionSkills;
public class Sympathetic : Skill
{
    public Sympathetic() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AndCondition([new MyHpIsLessThanCondition(0.5), new OpponentStartsCombatCondition()]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReductionEffect(5); 
    }
}