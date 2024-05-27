using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class AttackBetweenSpecificWeaponsCondition : Condition
{
    private string _firstWeaponType;
    private string _secondWeaponType;
        
    public AttackBetweenSpecificWeaponsCondition(string firstWeaponType, string secondWeaponType) : base()
    {
        this._firstWeaponType = firstWeaponType;
        this._secondWeaponType = secondWeaponType;
    }
        
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        
        if( myUnit.IsAttacking && ((_firstWeaponType == "magia" && _secondWeaponType == "fisica")|| (_secondWeaponType == "magia" && _firstWeaponType == "fisica"))) 
        { 
            if (myUnit.Weapon == Weapon.Magic && (opponentsUnit.Weapon == Weapon.Bow || opponentsUnit.Weapon == Weapon.Axe || opponentsUnit.Weapon == Weapon.Sword || opponentsUnit.Weapon == Weapon.Lance))
            {
                return true;
            }
            if (opponentsUnit.Weapon == Weapon.Magic && (myUnit.Weapon== "Bow" || myUnit.Weapon=="Axe" || myUnit.Weapon== "Sword" || myUnit.Weapon=="Lance")){
                return true;
            }
        }
        return false;
    }
}