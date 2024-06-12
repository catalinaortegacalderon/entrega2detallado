using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.CombatEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.SkillsThatAffectCombat.CounterattackDenialSkills;

public class HlioskjalfSkill : Skill
{
    public HlioskjalfSkill()
    {
        Conditions = new Condition[1];
        Conditions[0] = new AndCondition([ new MyUnitStartsCombatCondition(), 
            new MyUnitUsesCertainWeaponsCondition([Weapon.Magic]), 
            new OpponentUsesCertainWeaponCondition([Weapon.Magic])]);

        Effects = new Effect[1];
        Effects[0] = new CounterAttackDenialOnOpponentEffect();
    }
}