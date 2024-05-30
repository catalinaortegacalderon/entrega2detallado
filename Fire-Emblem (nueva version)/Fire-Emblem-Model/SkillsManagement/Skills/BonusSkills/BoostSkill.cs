using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class BoostSkill : Skill
{
    public BoostSkill() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyHpIsLessThanOpponentsHpPlusCondition(3); 
            
        this.Effects = new Effect[1];
    }
}