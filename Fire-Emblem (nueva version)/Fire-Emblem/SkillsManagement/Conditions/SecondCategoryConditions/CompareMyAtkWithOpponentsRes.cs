namespace Fire_Emblem;

public class CompareMyAtkWithOpponentsRes : SecondCategoryCondition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        int myTotalAtk =
            myUnit.Atk + myUnit.ActiveBonus.Attk * myUnit.ActiveBonusNeutralization.Attk
                       + myUnit.ActivePenalties.Attk * myUnit.ActivePenaltiesNeutralization.Attk;
        int opponentsTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;

        if (myTotalAtk > opponentsTotalRes)
        {
            return true;
        }
        return false;
    }
}