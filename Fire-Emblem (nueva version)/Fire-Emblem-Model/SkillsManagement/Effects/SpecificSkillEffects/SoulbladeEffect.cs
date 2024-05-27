namespace Fire_Emblem_Model;

public class SoulbladeEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        double refDesAverage = (opponentsUnit.Def + opponentsUnit.Res) / 2;
        int refDesAverageInt = Convert.ToInt32(Math.Truncate(refDesAverage));
        int defChange = refDesAverageInt - opponentsUnit.Def;
        int resChange = refDesAverageInt - opponentsUnit.Res;
        if (defChange < 0) opponentsUnit.ActivePenalties.Def += defChange;
        else
        {
            opponentsUnit.ActiveBonus.Def += defChange;
        }    
        if (resChange < 0) opponentsUnit.ActivePenalties.Res += resChange;
        else
        {
            opponentsUnit.ActiveBonus.Res += resChange;
        }
    }
}