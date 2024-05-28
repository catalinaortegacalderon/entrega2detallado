using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyUnitUsesCertainWeaponsCondition : Condition
{
    private Weapon[] _usedWeapon;
    public MyUnitUsesCertainWeaponsCondition(Weapon[] weapon) : base()
    {
        this._usedWeapon = weapon;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        if (this._usedWeapon.Contains(myUnit.Weapon)) return true;
        return false;
    }
}