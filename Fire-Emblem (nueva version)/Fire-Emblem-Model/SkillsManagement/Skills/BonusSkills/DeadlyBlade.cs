using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class DeadlyBlade : Skill
{
    public DeadlyBlade() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AndCondition([new MyUnitStartsCombatCondition(), 
            new MyUnitUsesCertainWeaponsCondition([Weapon.Sword])]);
        this.Conditions[1] = new AndCondition([new MyUnitStartsCombatCondition(), 
            new MyUnitUsesCertainWeaponsCondition([Weapon.Sword])]);
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 8); 
        this.Effects[1] = new ChangeStatsInEffect(StatType.Spd, 8); 
    }
}