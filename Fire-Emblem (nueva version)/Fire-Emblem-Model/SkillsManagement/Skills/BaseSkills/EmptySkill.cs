using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BaseEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.BaseSkills;

public class EmptySkill : Skill
{
    public EmptySkill() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
        this.Effects = new Effect[] { new EmptyEffect() };
    }
}