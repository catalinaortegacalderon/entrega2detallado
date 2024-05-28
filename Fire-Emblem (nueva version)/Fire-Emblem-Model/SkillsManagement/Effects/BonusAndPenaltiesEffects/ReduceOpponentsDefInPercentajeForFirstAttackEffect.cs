using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

public class ReduceOpponentsDefInPercentajeForFirstAttackEffect : Effect
{
    private double _reductionPercentaje;
    public ReduceOpponentsDefInPercentajeForFirstAttackEffect(double reduction) : base()
    {
        this._reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Def * _reductionPercentaje));
        opponentsUnit.ActivePenalties.DefFirstAttack  -= reduction;
    }
}