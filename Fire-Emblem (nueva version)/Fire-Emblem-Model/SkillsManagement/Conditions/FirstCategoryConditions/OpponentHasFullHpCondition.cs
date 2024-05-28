using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class OpponentHasFullHpCondition: Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.CurrentHp == opponentsUnit.HpMax) return true;
        return false;
    }
    
}