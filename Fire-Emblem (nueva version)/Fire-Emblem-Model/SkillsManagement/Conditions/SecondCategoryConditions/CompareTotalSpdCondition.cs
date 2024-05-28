namespace Fire_Emblem_Model;
public class CompareTotalSpdCondition : SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalSpd = myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralization.Spd
                               + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralization.Spd;
        int opponentsTotalSpd =
            opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralization.Spd
                              + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralization.Spd;
        
        if (myTotalSpd > opponentsTotalSpd) return true;
        return false;
    }
}