using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class PercentualDamageReductionConsideringOpponentsHpEffect : Effect
{
    private DamageEffectCategory Type;
    
    public PercentualDamageReductionConsideringOpponentsHpEffect(DamageEffectCategory type) : base()
    {
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        double percentualReduction = (double)opponentsUnit.CurrentHp / (double)opponentsUnit.HpMax / 2;
        percentualReduction = Math.Truncate(100.0 * percentualReduction) / 100.0;
        double finalPercentage = 1 - percentualReduction;
        
        if (this.Type == DamageEffectCategory.All)
        {
            myUnit.DamageEffects.PercentageReduction *= finalPercentage;
        }
        else if (this.Type == DamageEffectCategory.FirstAttack)
        {
            myUnit.DamageEffects.PercentageReductionOpponentsFirstAttack *= finalPercentage;
        }
        else if (this.Type == DamageEffectCategory.FollowUp)
        {
            myUnit.DamageEffects.PercentageReductionOpponentsFollowup *= finalPercentage;
        }
    }
}