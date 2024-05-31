using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyHpIsLessThanOpponentsHpPlusCondition: Condition
{
    private int _increaseAmountIn;
    public MyHpIsLessThanOpponentsHpPlusCondition(int increaseAmountIn) : base()
    {
        this._increaseAmountIn = increaseAmountIn;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        return myUnit.CurrentHp >= opponentsUnit.CurrentHp + this._increaseAmountIn;
    }
}