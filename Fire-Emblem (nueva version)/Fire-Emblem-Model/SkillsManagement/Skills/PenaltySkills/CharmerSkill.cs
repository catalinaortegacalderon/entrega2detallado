using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

public class CharmerSkill : Skill
{
    public CharmerSkill() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
        this.Conditions[1] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -3); 
        this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Spd, -3); 
    }
}