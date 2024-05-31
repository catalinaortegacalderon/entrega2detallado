using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class DragonsWrathSkill : Skill
{
    public DragonsWrathSkill() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new CompareMyAtkWithOpponentsResCondition();
        this.Effects = new Effect[2];
        this.Effects[0] = new PercentualDamageReductionEffect(0.75, DamageEffectCategory.FirstAttack);
        this.Effects[1] = new DragonsWrathSecondEffect();
    }
}
