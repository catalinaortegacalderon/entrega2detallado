using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

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