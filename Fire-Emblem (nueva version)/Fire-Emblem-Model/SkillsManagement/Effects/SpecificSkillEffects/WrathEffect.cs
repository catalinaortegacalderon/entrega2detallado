namespace Fire_Emblem_Model;

public class WrathEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int amount = myUnit.HpMax - myUnit.CurrentHp;
        if (amount > 30) amount = 30;
        myUnit.ActiveBonus.Attk += amount;
        myUnit.ActiveBonus.Spd += amount;
    }
}