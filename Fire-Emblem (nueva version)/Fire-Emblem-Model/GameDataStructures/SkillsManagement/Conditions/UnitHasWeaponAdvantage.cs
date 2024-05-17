namespace Fire_Emblem_Model;

public class UnitHasWeaponAdvantage : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        string attackingWeapon = myUnit.Weapon;
        string defensiveWeapon = opponentsUnit.Weapon;
        return (attackingWeapon == "Sword" & defensiveWeapon == "Axe") || (attackingWeapon == "Lance" & defensiveWeapon == "Sword") || (attackingWeapon == "Axe" & defensiveWeapon == "Lance");
    }
}