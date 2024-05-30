using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;
public class CompareTotalSpdCondition : SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalSpd = myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralizator.Spd
                               + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralizator.Spd;
        int opponentsTotalSpd =
            opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizator.Spd
                              + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizator.Spd;
        
        if (myTotalSpd > opponentsTotalSpd) return true;
        return false;
    }
}