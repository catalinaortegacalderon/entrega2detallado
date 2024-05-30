using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;

public class CompareMyAtkWithOpponentsResCondition : SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalAtk =
            myUnit.Atk + myUnit.ActiveBonus.Attk * myUnit.ActiveBonusNeutralizator.Attk
                       + myUnit.ActivePenalties.Attk * myUnit.ActivePenaltiesNeutralizator.Attk;
        int opponentsTotalRes =
            opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizator.Res
                              + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizator.Res;

        if (myTotalAtk > opponentsTotalRes)
        {
            return true;
        }
        return false;
    }
}