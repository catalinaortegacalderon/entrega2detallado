namespace Fire_Emblem_Model;

public class OpponentStartsCombatCondition : Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.IsAttacking == false) return true;
        return false;
    }
}