namespace Fire_Emblem;

public class CompareTotalRes : SecondCategoryCondition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        int myTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;
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