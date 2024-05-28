using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class MyUnitUsesCertainWeaponsCondition : Condition
{
    private Weapon[] _usedWeapon;
    public MyUnitUsesCertainWeaponsCondition(Weapon[] weapon) : base()
    {
        this._usedWeapon = weapon;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (this._usedWeapon.Contains(myUnit.Weapon)) return true;
        return false;
    }
}