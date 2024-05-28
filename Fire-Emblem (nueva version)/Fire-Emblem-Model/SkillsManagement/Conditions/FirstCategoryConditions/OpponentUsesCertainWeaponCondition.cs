using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class OpponentUsesCertainWeaponCondition: Condition
{
    private Weapon[] _weapons;
    public OpponentUsesCertainWeaponCondition(Weapon[] weapons) : base()
    {
        this._weapons = weapons;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        if (this._weapons.Contains(opponentsUnit.Weapon)) return true;
        return false;
    }
}