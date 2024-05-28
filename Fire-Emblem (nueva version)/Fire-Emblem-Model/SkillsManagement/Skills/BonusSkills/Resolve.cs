using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class Resolve : Skill
{
    public Resolve() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyHpIsLessThanCondition(0.75); 
        this.Conditions[1] = new MyHpIsLessThanCondition(0.75);
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Def, 7); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 7); 
    }
}