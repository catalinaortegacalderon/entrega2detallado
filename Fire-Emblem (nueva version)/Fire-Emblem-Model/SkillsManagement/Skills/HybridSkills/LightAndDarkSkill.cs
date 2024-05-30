using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class LightAndDarkSkill : Skill
{
    public LightAndDarkSkill() : base()
    {
        this.Conditions = new Condition[6];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
        this.Conditions[3] = new AlwaysTrueCondition();
        this.Conditions[4] = new AlwaysTrueCondition();
        this.Conditions[5] = new AlwaysTrueCondition();
            
        this.Effects = new Effect[6];
        this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -5); 
        this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Spd, -5); 
        this.Effects[2] = new ChangeOpponentsStatsInEffect( StatType.Def, -5);
        this.Effects[3] = new ChangeOpponentsStatsInEffect( StatType.Res, -5);
        this.Effects[4] = new NeutralizePenaltiesEffect();
        this.Effects[5] = new NeutralizeOpponentsBonusEffect();
    }
}