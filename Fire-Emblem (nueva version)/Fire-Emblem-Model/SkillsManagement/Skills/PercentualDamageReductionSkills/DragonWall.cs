using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.PercentualDamageReductionSkills;

public class DragonWall : Skill
{
    public DragonWall() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new CompareTotalResCondition(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReductionDeterminedByStatDifferenceEffect(StatType.Res, 4); 
    }
}