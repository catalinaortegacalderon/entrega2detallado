namespace Fire_Emblem_Model;

public class MyUnitStartsCombatCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.IsAttacking) return true;
        return false;
    }
}