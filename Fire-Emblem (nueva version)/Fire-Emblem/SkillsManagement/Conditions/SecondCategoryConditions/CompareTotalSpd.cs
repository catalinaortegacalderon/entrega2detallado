namespace Fire_Emblem;

public class CompareTotalSpd : SecondCategoryCondition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
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