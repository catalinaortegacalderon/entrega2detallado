namespace Fire_Emblem;

public class AbsolutDamageReduction : Effect
{
    public AbsolutDamageReduction(int amount) : base()
    {
        this.Amount = amount;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        myUnit.DamageEffects.AbsolutDamageReduction -= this.Amount;
    }
}