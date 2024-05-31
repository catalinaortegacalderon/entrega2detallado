using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyUnitUsesCertainWeaponsCondition : Condition
{
    private Weapon[] _usedWeapon;
    public MyUnitUsesCertainWeaponsCondition(Weapon[] weapon) : base()
    {
        _usedWeapon = weapon;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        return _usedWeapon.Contains(myUnit.Weapon);
    }
}