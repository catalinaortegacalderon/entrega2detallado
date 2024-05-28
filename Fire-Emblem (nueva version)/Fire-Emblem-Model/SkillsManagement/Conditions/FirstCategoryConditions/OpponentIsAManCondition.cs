using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class OpponentIsAManCondition: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.Gender == Gender.Male) return true;
        return false;
    }
    
}