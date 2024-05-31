using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class OpponentHasHpGreaterThanCondition : Condition
{
    private double _percentage;
    public OpponentHasHpGreaterThanCondition(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        return opponentsUnit.CurrentHp >= opponentsUnit.HpMax * this._percentage;
    }
}