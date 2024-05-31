using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyHpIsLessThanCondition : Condition
{
    private double _amount;
    public MyHpIsLessThanCondition(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        return myUnit.CurrentHp <= myUnit.HpMax * this._amount;
    }
}