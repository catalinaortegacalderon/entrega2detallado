using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class CloseDefSkill : Skill
{
    public CloseDefSkill() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
            Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
        this.Conditions[1] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
            Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
        this.Conditions[2] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
            Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
            
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Def,8); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 8); 
        this.Effects[2] = new NeutralizeOpponentsBonusEffect(); 
    }
}