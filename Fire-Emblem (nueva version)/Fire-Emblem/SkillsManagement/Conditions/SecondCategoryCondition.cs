namespace Fire_Emblem;

public class SecondCategoryCondition: Condition
{
    public SecondCategoryCondition() : base()
    {
        this.Priority = 2;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

public class CertainSelfUnitStatBiggerThanCertainRivalStat: SecondCategoryCondition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

