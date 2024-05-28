namespace Fire_Emblem_Model;

public class OpponentHasFullHpCondition: Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.CurrentHp == opponentsUnit.HpMax) return true;
        return false;
    }
    
}