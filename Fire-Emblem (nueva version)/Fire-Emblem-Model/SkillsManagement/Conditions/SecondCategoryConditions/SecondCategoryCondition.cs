namespace Fire_Emblem_Model;

public class SecondCategoryCondition: Condition
{
    public SecondCategoryCondition() : base()
    {
        this.Priority = 2;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        return true;
    }
}

