using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

public class DivineRecreationEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        // OJO ES SIN PENALTIES NI BONUS
        
        // ES SIN LA ABSOLUTA NI PORCENTUAL     si extra
        
        // rival te va a atacar, vas a reducir el daño, y esa diferencia la haras en el ataque siguuiente
        
        // se calcula igual en el primer o segundo ataque, puse primero
        AttackCalculator calculator = new AttackCalculator(opponentsUnit, myUnit, AttackType.FirstAttack);
        
        //double initialDamage = calculator.CalculateInitialDamage(); 
        //double initialDamage = calculator.CalculateInitialDamageWithoutBonusAndPenalties();
        double initialDamage = calculator.CalculateAttackForDivineRecreation();
        int finalDamage = calculator.CalculateAttack();

        int amount = (int)(initialDamage - finalDamage);
        
        Console.WriteLine("aplicando efecto divine recreation");
        Console.WriteLine("ataque inicial");
        Console.WriteLine(initialDamage);

        //double initialDamageAlternative1 = calculator.CalculateInitialDamageWithoutBonusAndPenaltiesWhitDef();
        //double initialDamageAlternative2 = calculator.CalculateInitialDamageWithoutBonusAndPenaltiesWhithRes();
        //Console.WriteLine("ataque inicial alternativo");
        //Console.WriteLine(initialDamageAlternative1);
        //Console.WriteLine("ataque inicial alternativo2");
        //Console.WriteLine(initialDamageAlternative2);
        
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