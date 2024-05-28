using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class ChaosStyleCondition : Condition
{
    
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        
        if( myUnit.IsAttacking) 
        { 
            if (myUnit.Weapon == Weapon.Magic && (opponentsUnit.Weapon == Weapon.Bow || opponentsUnit.Weapon == Weapon.Axe || opponentsUnit.Weapon == Weapon.Sword || opponentsUnit.Weapon == Weapon.Lance))
            {
                return true;
            }
            if (opponentsUnit.Weapon == Weapon.Magic && (myUnit.Weapon == Weapon.Bow || myUnit.Weapon == Weapon.Axe || myUnit.Weapon == Weapon.Sword || myUnit.Weapon == Weapon.Lance)){
                return true;
            }
        }
        return false;
    }
}