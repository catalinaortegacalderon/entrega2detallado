using Fire_Emblem;
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class DivineRecreationEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        
        // se calcula igual en el primer o segundo ataque, puse primero
        AttackCalculator calculator = new AttackCalculator(opponentsUnit, myUnit, AttackType.FirstAttack);
        
        double initialDamage = calculator.CalculateInitialDamage(); 
        int finalDamage = calculator.CalculateAttack();

        int amount = (int)(initialDamage - finalDamage);
        
        Console.WriteLine("aplicando efecto divine recreation");
        Console.WriteLine("ataque inicial");
        Console.WriteLine(initialDamage);
        Console.Write("ataque final");
        Console.WriteLine(finalDamage);
        Console.Write("amount");
        Console.WriteLine(amount);
        
        if (myUnit.StartedTheRound)
        {
            Console.WriteLine("paso por a");
            myUnit.DamageEffects.ExtraDamageFollowup += amount;
        }
        else
        {
            Console.WriteLine("paso por b");
            myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
        }
        
    }
    
}