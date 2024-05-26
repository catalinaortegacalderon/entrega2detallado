namespace Fire_Emblem_Model;

public class AbsolutDamageReductionEffect : Effect
{
    public AbsolutDamageReductionEffect(int amount) : base()
    {
        this.Amount = amount;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        myUnit.DamageEffects.AbsolutDamageReduction -= this.Amount;
    }
}