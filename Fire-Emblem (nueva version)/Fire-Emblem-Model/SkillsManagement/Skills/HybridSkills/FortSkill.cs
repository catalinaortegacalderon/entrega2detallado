using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class FortSkill : Skill
{
    public FortSkill(StatType firstStat, StatType secondStat) : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
            
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsInEffect( firstStat, 6); 
        this.Effects[1] = new ChangeStatsInEffect( secondStat, 6); 
        this.Effects[2] = new ChangeStatsInEffect( StatType.Atk, -2); ; 
    }
}