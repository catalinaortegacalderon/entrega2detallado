using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;


public class CompareTotalSpdAddingSpdToTheOpponent : SecondCategoryCondition
{
    // todo: funcion
    private int amountToAdd;
        
    public CompareTotalSpdAddingSpdToTheOpponent(int amountToAdd)
    {
        this.amountToAdd = amountToAdd;
    }
    
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        var myTotalSpd = myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralizer.Spd
                                    + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralizer.Spd;
        var opponentsTotalSpd =
            opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizer.Spd
                              + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizer.Spd;

        if (myTotalSpd > opponentsTotalSpd + amountToAdd)
            return true;
        return false;
    }
}