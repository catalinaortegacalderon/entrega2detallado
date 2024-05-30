using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

public class BeliefInLoveSkill : Skill
{
    public BeliefInLoveSkill() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new OrCondition([new OpponentHasFullHpCondition(),
            new OpponentStartsCombatCondition()] );
        this.Conditions[1] = new OrCondition([new OpponentHasFullHpCondition(), 
            new OpponentStartsCombatCondition()] );
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -5); 
        this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Def, -5); 
    }
}