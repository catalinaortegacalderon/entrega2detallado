using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class PercentualDamageReductionDeterminedByStatDifferenceEffect : Effect
{
    private StatType stat;
    private int multiplicator;
    
    public PercentualDamageReductionDeterminedByStatDifferenceEffect(StatType stat, int multiplicator) : base()
    {
        this.stat = stat;
        this.multiplicator = multiplicator;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    { 
        //poner el que queda no el reducido. ej: si se reduce en 10% el amount es 0.9
        double redutionPercentage = 1;
        if (this.stat == StatType.Spd)
        {
            double myTotalSpd = 
                myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralizer.Spd 
                           + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralizer.Spd;
            double opponentsTotalSpd =
                opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizer.Spd
                                  + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizer.Spd;
            redutionPercentage =  1 - (((myTotalSpd - opponentsTotalSpd) * this.multiplicator)/100);
        }
        else if (this.stat == StatType.Res)
        {
            double myTotalRes =
                myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralizer.Res
                           + myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralizer.Res;
            double opponentsTotalRes =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizer.Res
                                  + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizer.Res;
            redutionPercentage =  1 - (((myTotalRes - opponentsTotalRes) * this.multiplicator)/100);
        }
        if (redutionPercentage < 0.6)
        {
            redutionPercentage = 0.6;
        }
        myUnit.DamageEffects.PercentageReduction = myUnit.DamageEffects.PercentageReduction * redutionPercentage;
    }
    
}