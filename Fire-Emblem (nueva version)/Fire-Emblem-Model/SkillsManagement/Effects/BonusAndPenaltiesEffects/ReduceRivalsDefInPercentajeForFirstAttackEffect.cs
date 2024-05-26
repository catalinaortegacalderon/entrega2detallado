namespace Fire_Emblem_Model;

public class ReduceRivalsDefInPercentajeForFirstAttackEffect : Effect
{
    private double _reductionPercentaje;
    public ReduceRivalsDefInPercentajeForFirstAttackEffect(double reduction) : base()
    {
        this._reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Def * _reductionPercentaje));
        opponentsUnit.ActivePenalties.DefFirstAttack  -= reduction;
    }
}