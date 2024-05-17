namespace Fire_Emblem_Model;

public class DragonsWrathSecondEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        Console.WriteLine("paso por donde quiero");
        int amount = Convert.ToInt32(Math.Truncate((myUnit.HpMax-myUnit.CurrentHp) * 0.5));
        myUnit.DamageEffects.ExtraDamageFirstAttack +=  + amount;
    }
}

