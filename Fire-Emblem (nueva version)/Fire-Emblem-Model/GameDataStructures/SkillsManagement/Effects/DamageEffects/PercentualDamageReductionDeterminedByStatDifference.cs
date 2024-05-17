using System.Diagnostics;

namespace Fire_Emblem_Model;

public class PercentualDamageReductionDeterminedByStatDifference : Effect
{
    private string stat;
    private int multiplicator;
    
    public PercentualDamageReductionDeterminedByStatDifference(string stat, int multiplicator) : base()
    {
        this.stat = stat;
        this.multiplicator = multiplicator;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    { 
        //poner el que queda no el reducido. ej: si se reduce en 10% el amount es 0.9
        double redutionPercentage = 1;
        if (this.stat == "Spd")
        {
            double myTotalSpd = 
                myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralization.Spd 
                           + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralization.Spd;
            double opponentsTotalSpd =
                opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralization.Spd
                                  + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralization.Spd;
            redutionPercentage =  1 - (((myTotalSpd - opponentsTotalSpd) * this.multiplicator)/100);
        }
        else if (this.stat == "Res")
        {
            double myTotalRes =
                myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralization.Res
                           + myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralization.Res;
            double opponentsTotalRes =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                                  + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;
            redutionPercentage =  1 - (((myTotalRes - opponentsTotalRes) * this.multiplicator)/100);
        }
        if (redutionPercentage < 0.6)
        {
            redutionPercentage = 0.6;
        }
        myUnit.DamageEffects.PorcentualReduction = myUnit.DamageEffects.PorcentualReduction * redutionPercentage;
    }
    
}