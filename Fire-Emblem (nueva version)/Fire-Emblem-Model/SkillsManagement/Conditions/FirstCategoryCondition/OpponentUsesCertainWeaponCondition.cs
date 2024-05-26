namespace Fire_Emblem_Model;

public class OpponentUsesCertainWeaponCondition: Condition
{
    private String[] _weapons;
    public OpponentUsesCertainWeaponCondition(String[] weapons) : base()
    {
        this._weapons = weapons;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (this._weapons.Contains(opponentsUnit.Weapon)) return true;
        return false;
    }
}