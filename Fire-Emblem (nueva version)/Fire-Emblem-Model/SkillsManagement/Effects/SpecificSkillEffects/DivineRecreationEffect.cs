using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

public class DivineRecreationEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        AttackCalculator calculator = new AttackCalculator(opponentsUnit, myUnit, 
            AttackType.FirstAttack);
        
        double initialDamage = calculator.CalculateAttackForDivineRecreation();
        int finalDamage = calculator.CalculateAttack();

        int amount = (int)(initialDamage - finalDamage);
        
        if (myUnit.StartedTheRound)
        {
            myUnit.DamageEffects.ExtraDamageFollowup += amount;
        }
        else
        {
            myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
        }
        
    }
    
}