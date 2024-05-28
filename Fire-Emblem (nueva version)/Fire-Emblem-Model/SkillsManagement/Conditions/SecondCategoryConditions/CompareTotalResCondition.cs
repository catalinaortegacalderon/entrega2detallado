namespace Fire_Emblem_Model;

public class CompareTotalResCondition : SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalRes =
            myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralization.Res
                              + myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralization.Res;
        int opponentsTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;

        if (myTotalRes > opponentsTotalRes)
        {
            return true;
        }
        return false;
    }
}