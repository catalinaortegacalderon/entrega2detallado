using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

public class LunaSkill : Skill
{
    public LunaSkill() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ReduceOpponentsDefInPercentajeForFirstAttackEffect( 0.5); 
        this.Effects[1] = new ReduceOpponentsResInPercentageForFirstAttackEffect( 0.5); 
    }
}