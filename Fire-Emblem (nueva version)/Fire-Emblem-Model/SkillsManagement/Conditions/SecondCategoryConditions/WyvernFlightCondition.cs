using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using Fire_Emblem;

namespace ConsoleApp1.SkillsManagement.Conditions.SecondCategoryConditions;

public class WyvernFlightCondition: SecondCategoryCondition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {

        var myTotalSpd = TotalStatGetter.GetTotal(StatType.Spd, myUnit);
        var opponentsTotalSpd = TotalStatGetter.GetTotal(StatType.Spd, opponentsUnit);
        
        int  myAmountToAdd = TotalStatGetter.GetTotal(StatType.Def, myUnit);
        int  opponentsAmountToAdd = TotalStatGetter.GetTotal(StatType.Def, opponentsUnit);
        
        return (myTotalSpd + myAmountToAdd > opponentsTotalSpd + opponentsAmountToAdd);
    }
}