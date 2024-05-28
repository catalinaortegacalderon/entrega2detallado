using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.PercentualDamageReductionSkills;

public class GoldenLotus :  Skill
{
    public GoldenLotus() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentUsesCertainWeaponCondition([Weapon.Sword, Weapon.Axe, Weapon.Lance, Weapon.Bow]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReductionEffect(0.5,DamageEffectCategory.FirstAttack); 
    }
}
