using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;
public class CompareTotalSpdCondition : SecondCategoryCondition
{
    // todo: funcion
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        int myTotalSpd = myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralizer.Spd
                               + myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralizer.Spd;
        int opponentsTotalSpd =
            opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizer.Spd
                              + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizer.Spd;
        
        if (myTotalSpd > opponentsTotalSpd) return true;
        return false;
    }
}