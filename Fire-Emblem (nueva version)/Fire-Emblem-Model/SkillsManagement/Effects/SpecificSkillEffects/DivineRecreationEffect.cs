using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

public class DivineRecreationEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        var calculator = new DamageCalculator(opponentsUnit, myUnit,
            AttackType.FirstAttack);

        double initialDamage = calculator.CalculateAttackForDivineRecreation();
        var finalDamage = calculator.CalculateAttack();

        var amount = (int)(initialDamage - finalDamage);

        if (myUnit.StartedTheRound)
            myUnit.DamageEffects.ExtraDamageFollowup += amount;
        else
            myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
    }
}