namespace Fire_Emblem;

public class CompareTotalRes : SecondCategoryCondition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        Console.WriteLine("paso por verify de dragon wall");
        Console.WriteLine(myUnit.Name);
        Console.WriteLine(opponentsUnit.Name);
        int myTotalRes =
            myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralization.Res
                              + myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralization.Res;
        int opponentsTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;
        Console.WriteLine("my total res", myTotalRes);
        Console.WriteLine(myTotalRes);
        Console.WriteLine("opponents total res", opponentsTotalRes);
        Console.WriteLine(opponentsTotalRes);

        if (myTotalRes > opponentsTotalRes)
        {
            Console.WriteLine("true");
            return true;
        }
        Console.WriteLine("false");
        return false;
    }
}