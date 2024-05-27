namespace Fire_Emblem_Model;

public class DragonsWrathSecondEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    { 
        int unitsAtk = myUnit.Atk + myUnit.ActiveBonus.Attk 
            * myUnit.ActiveBonusNeutralization.Attk + myUnit.ActivePenalties.Attk 
            * myUnit.ActivePenaltiesNeutralization.Attk;
        
        int rivalsRes = opponentsUnit.Res + opponentsUnit.ActiveBonus.Res 
            * opponentsUnit.ActiveBonusNeutralization.Res + opponentsUnit.ActivePenalties.Res 
            * opponentsUnit.ActivePenaltiesNeutralization.Res;
        
        int amount = Convert.ToInt32(Math.Truncate((unitsAtk - rivalsRes) * 0.25));
        myUnit.DamageEffects.ExtraDamageFirstAttack +=  amount;
        Console.WriteLine(amount);
    }
}

