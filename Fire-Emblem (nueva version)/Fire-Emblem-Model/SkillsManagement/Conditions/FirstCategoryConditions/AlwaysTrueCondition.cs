namespace Fire_Emblem_Model;

public class AlwaysTrueCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        return true;
    }
}