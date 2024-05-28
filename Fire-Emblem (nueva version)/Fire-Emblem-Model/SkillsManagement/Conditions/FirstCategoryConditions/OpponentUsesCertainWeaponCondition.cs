using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

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