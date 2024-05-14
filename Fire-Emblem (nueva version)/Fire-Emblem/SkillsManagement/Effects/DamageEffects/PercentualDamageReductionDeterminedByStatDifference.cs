namespace Fire_Emblem;

public class PercentualDamageReductionDeterminedByStatDifference : Effect
{
    private string stat;
    private string multiplicator;
    
    public PercentualDamageReduction(string stat, int multiplicator) : base()
    {
        this.stat = stat;
        this.multiplicator = this.multiplicator;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    { //poner el que queda no el reducido. ej: si se reduce en 10% el amount es 0.9
        double recutionPercentaje;
        if this.
        if (this.Type == "All")
        {
            myUnit.DamageEffects.PorcentualReduction = myUnit.DamageEffects.PorcentualReduction * this.percentaje;
        }
        else if (this.Type == "First Attack")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFirstAttack = myUnit.DamageEffects.PorcentualReductionRivalsFirstAttack * this.percentaje;
        }
        else if (this.Type == "Followup")
        {
            myUnit.DamageEffects.PorcentualReductionRivalsFollowup = myUnit.DamageEffects.PorcentualReductionRivalsFollowup * this.percentaje;
        }
        myUnit.DamageEffects.PorcentualReduction = myUnit.DamageEffects.PorcentualReduction * this.percentaje;
    }
{
    
}