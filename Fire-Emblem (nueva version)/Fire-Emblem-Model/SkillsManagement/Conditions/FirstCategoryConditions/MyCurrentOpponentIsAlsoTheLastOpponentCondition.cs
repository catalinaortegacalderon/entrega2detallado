namespace Fire_Emblem_Model;

public class MyCurrentOpponentIsAlsoTheLastOpponentCondition: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.Name == myUnit.LastOpponentName) return true;
        return false;
    }
    
}