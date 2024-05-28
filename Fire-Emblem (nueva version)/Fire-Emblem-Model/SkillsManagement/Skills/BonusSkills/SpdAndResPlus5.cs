using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class SpdAndResPlus5 : Skill
{
    public SpdAndResPlus5 () : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition(); 
        this.Conditions[1] = new AlwaysTrueCondition(); 
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Spd, 5); 
        this.Effects[1] = new ChangeStatsInEffect(StatType.Res, 5);
            
    }
}