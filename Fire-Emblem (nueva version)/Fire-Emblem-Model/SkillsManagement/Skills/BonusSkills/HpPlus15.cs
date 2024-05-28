using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class HpPlus15 : Skill
{
    public HpPlus15() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeHpInEffect(15);
    }
}