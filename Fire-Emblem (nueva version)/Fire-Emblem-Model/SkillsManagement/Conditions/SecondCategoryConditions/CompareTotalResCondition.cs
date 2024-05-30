using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;

public class CompareTotalResCondition : SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalRes =
            myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralizator.Res
                              + myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralizator.Res;
        int opponentsTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizator.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizator.Res;

        if (myTotalRes > opponentsTotalRes)
        {
            return true;
        }
        return false;
    }
}