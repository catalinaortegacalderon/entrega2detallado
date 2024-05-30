using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class LullSkill : Skill
{
    public LullSkill(StatType firstStat, StatType secondStat) : base()
    {
        this.Conditions = new Condition[4];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
        this.Conditions[3] = new AlwaysTrueCondition();
            
        this.Effects = new Effect[4];
        this.Effects[0] = new ChangeOpponentsStatsInEffect( firstStat, -3); 
        this.Effects[1] = new ChangeOpponentsStatsInEffect( secondStat, -3); 
        this.Effects[2] = new NeutralizeOneOfOpponentsBonusEffect( firstStat); 
        this.Effects[3] = new NeutralizeOneOfOpponentsBonusEffect( secondStat); 
    }
}