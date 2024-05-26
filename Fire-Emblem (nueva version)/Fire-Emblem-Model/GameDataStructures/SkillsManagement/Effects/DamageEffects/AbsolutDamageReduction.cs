namespace Fire_Emblem_Model;

public class AbsolutDamageReduction : Effect
{
    public AbsolutDamageReduction(int amount) : base()
    {
        this.Amount = amount;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        myUnit.DamageEffects.AbsolutDamageReduction -= this.Amount;
    }
}