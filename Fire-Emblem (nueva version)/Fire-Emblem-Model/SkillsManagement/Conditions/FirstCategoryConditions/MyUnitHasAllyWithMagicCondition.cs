using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyUnitHasAllyWithMagicCondition : Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        Console.WriteLine(myUnit.HasAnAllyWithMagic);
        return myUnit.HasAnAllyWithMagic;
    }
}
