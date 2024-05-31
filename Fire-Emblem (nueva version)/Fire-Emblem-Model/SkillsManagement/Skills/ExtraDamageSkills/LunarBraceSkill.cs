using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.ExtraDamageSkills;

public class LunarBraceSkill : Skill
{ 
    public LunarBraceSkill() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new  AndCondition([new MyUnitUsesCertainWeaponsCondition([Weapon.Sword, Weapon.Bow, 
            Weapon.Axe, Weapon.Lance]), new MyUnitStartsCombatCondition()]);
        this.Conditions[0].ChangePriorityBecauseEffectPriorityIsBigger(2);
        
        this.Effects = new Effect[1];
        this.Effects[0] = new LunarBraceEffect();
    }
}