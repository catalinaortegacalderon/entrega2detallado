using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class MyUnitHasWeaponAdvantageCondition : Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        Weapon attackingWeapon = myUnit.Weapon;
        Weapon defensiveWeapon = opponentsUnit.Weapon;
        return HasWeaponAdvantage(attackingWeapon, defensiveWeapon);
    }

    private bool HasWeaponAdvantage(Weapon attackingWeapon, Weapon defensiveWeapon)
    {
        bool hasWeaponAdvantage = (attackingWeapon == Weapon.Sword & defensiveWeapon == Weapon.Axe) || 
                                  (attackingWeapon == Weapon.Lance & defensiveWeapon == Weapon.Sword) || 
                                  (attackingWeapon == Weapon.Axe & defensiveWeapon == Weapon.Lance);
        return hasWeaponAdvantage;
    }
}