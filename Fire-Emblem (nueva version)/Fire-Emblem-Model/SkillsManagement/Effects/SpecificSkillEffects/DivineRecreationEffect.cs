using Fire_Emblem;

namespace Fire_Emblem_Model;

public class DivineRecreationEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        //AttackCalculator calculator = new AttackCalculator();
        //int attack = calculator.CalculateAttack();
        myUnit.DamageEffects.ExtraDamageFollowup = myUnit.DamageEffects.ExtraDamageFollowup + 9;
        
    }
    
}