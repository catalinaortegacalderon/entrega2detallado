using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.ExtraDamageSkills;

public class BraverySkill : Skill
{ 
    public BraverySkill() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrueCondition(); 
        
        this.Effects = new Effect[1];
        this.Effects[0] = new ExtraDamageReductionEffect(5, DamageEffectCategory.All); 
    }
}