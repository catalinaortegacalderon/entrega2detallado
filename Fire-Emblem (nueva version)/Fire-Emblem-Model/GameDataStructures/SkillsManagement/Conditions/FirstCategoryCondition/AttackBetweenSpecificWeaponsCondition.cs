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
            if (myUnit.Weapon == "Magic" && (opponentsUnit.Weapon == "Bow" || opponentsUnit.Weapon == "Axe" || opponentsUnit.Weapon == "Sword" || opponentsUnit.Weapon == "Lance"))
            {
                return true;
            }
            if (opponentsUnit.Weapon == "Magic" && (myUnit.Weapon== "Bow" || myUnit.Weapon=="Axe" || myUnit.Weapon== "Sword" || myUnit.Weapon=="Lance")){
                return true;
            }
        }
        return false;
    }
}