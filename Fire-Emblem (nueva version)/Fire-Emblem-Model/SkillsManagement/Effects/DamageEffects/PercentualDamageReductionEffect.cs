using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class PercentualDamageReductionEffect : Effect
{
    private DamageEffectCategory Type;
    private double percentaje;
    
    public PercentualDamageReductionEffect(double amount, DamageEffectCategory type) : base()
    {
        this.percentaje = amount;
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        //poner el que queda no el reducido. ej: si se reduce en 10% el amount es 0.9
        if (this.Type == DamageEffectCategory.All)
        {
            myUnit.DamageEffects.PercentageReduction = myUnit.DamageEffects.PercentageReduction * this.percentaje;
        }
        else if (this.Type == DamageEffectCategory.FirstAttack)
        {
            myUnit.DamageEffects.PercentageReductionOpponentsFirstAttack = myUnit.DamageEffects.PercentageReductionOpponentsFirstAttack * this.percentaje;
        }
        else if (this.Type == DamageEffectCategory.FollowUp)
        {
            myUnit.DamageEffects.PercentageReductionOpponentsFollowup = myUnit.DamageEffects.PercentageReductionOpponentsFollowup * this.percentaje;
        }
    }
}