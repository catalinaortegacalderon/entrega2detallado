using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;

public class PercentualDamageReduction : Effect
{
    private string Type;
    private double percentaje;
    
    public PercentualDamageReduction(double amount, string type) : base()
    {
        this.percentaje = amount;
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    { //poner el que queda no el reducido. ej: si se reduce en 10% el amount es 0.9
        if (this.Type == "All")
        {
            myUnit.DamageEffects.PorcentualReduction = myUnit.DamageEffects.PorcentualReduction * this.percentaje;
        }
        else if (this.Type == "First Attack")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFirstAttack = myUnit.DamageEffects.PorcentualReductionRivalsFirstAttack * this.percentaje;
            Console.WriteLine("paso por donde quiero");
        }
        else if (this.Type == "Followup")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFollowup = myUnit.DamageEffects.PorcentualReductionRivalsFollowup * this.percentaje;
        }
    }
}