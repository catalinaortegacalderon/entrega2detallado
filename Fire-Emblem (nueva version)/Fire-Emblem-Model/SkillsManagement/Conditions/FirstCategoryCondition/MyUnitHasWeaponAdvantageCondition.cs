namespace Fire_Emblem_Model;

public class MyUnitHasWeaponAdvantageCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        string attackingWeapon = myUnit.Weapon;
        string defensiveWeapon = opponentsUnit.Weapon;
        return (attackingWeapon == "Sword" & defensiveWeapon == "Axe") || (attackingWeapon == "Lance" & defensiveWeapon == "Sword") || (attackingWeapon == "Axe" & defensiveWeapon == "Lance");
    }
}