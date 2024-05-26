namespace Fire_Emblem_Model;

public class ReduceOpponentsResInPercentageForFirstAttackEffect : Effect
{
    private double _reductionPercentage;
    public ReduceOpponentsResInPercentageForFirstAttackEffect(double reduction) : base()
    {
        this._reductionPercentage = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Res * _reductionPercentage));
        opponentsUnit.ActivePenalties.ResFirstAttack  -= reduction;
    }
}