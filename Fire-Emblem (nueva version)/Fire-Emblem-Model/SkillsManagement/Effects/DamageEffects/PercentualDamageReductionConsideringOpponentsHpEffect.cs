using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;

public class PercentualDamageReductionConsideringOpponentsHpEffect : Effect
{
    private string Type;
    
    public PercentualDamageReductionConsideringOpponentsHpEffect(string type) : base()
    {
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        double percentualReduction = (double)opponentsUnit.CurrentHp / (double)opponentsUnit.HpMax / 2;
        percentualReduction = Math.Truncate(100.0 * percentualReduction) / 100.0;
        double finalPercentage = 1 - percentualReduction;
        
        if (this.Type == "All")
        {
            Console.WriteLine("paso por all");
            myUnit.DamageEffects.PorcentualReduction *= finalPercentage;
        }
        else if (this.Type == "First Attack")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFirstAttack *= finalPercentage;
        }
        else if (this.Type == "Followup")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFollowup *= finalPercentage;
        }
    }
}