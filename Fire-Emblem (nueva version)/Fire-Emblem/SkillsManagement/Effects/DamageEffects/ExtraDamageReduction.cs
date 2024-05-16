namespace Fire_Emblem;

public class ExtraDamageReduction: Effect
{
    private string Type;
    private int amount;
    
    public ExtraDamageReduction(int amount, string type) : base()
    {
        this.amount = amount;
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    { 
        if (this.Type == "All")
        {
            myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + this.amount;
        }
        else if (this.Type == "First Attack")
        {
            myUnit.DamageEffects.ExtraDamageFirstAttack= myUnit.DamageEffects.ExtraDamageFirstAttack + this.amount;
        }
        else if (this.Type == "Followup")
        {
            myUnit.DamageEffects.ExtraDamageFollowup = myUnit.DamageEffects.ExtraDamageFollowup + this.amount;
        }
    }
}