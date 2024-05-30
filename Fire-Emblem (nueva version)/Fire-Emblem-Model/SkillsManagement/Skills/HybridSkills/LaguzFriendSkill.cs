using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class LaguzFriendSkill : Skill
{
    public LaguzFriendSkill() : base()
    {
        this.Conditions = new Condition[5];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
        this.Conditions[3] = new AlwaysTrueCondition();
        this.Conditions[4] = new AlwaysTrueCondition();
        this.Effects = new Effect[5];
        this.Effects[0] = new PercentualDamageReductionEffect(0.5, DamageEffectCategory.All);
        this.Effects[1] = new ChangeStatsInBasePercentageEffect(StatType.Def, -0.5);
        this.Effects[2] = new ChangeStatsInBasePercentageEffect(StatType.Res, -0.5);
        this.Effects[3] = new NeutralizeOneOfMyBonusEffect(StatType.Def);
        this.Effects[4] = new NeutralizeOneOfMyBonusEffect(StatType.Res);
    }
    
}