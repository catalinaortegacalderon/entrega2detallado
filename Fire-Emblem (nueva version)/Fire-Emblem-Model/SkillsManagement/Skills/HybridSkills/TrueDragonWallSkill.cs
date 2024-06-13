using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.CombatEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class TrueDragonWallSkill : Skill
{
    public TrueDragonWallSkill()
    {
        Conditions = new Condition[1];
        Conditions[0] = new CompareTotalResCondition();

        Effects = new Effect[1];
        Effects[0] = new PercentualDamageReductionDeterminedByResDifferenceEffect(
             6, 0.6, DamageEffectCategory.All);
        Effects[0] = new PercentualDamageReductionDeterminedByResDifferenceEffect(
            4, 0.4, DamageEffectCategory.FollowUp );
    }
    
}