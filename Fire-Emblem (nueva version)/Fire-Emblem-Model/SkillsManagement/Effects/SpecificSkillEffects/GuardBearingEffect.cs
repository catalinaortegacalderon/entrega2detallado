namespace Fire_Emblem_Model;

public class GuardBearingEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        double percentage = 0.7;
        if (!myUnit.HasStartedACombat || !myUnit.HasBeenBeenInACombatStartedByTheOpponent )
        {
            percentage = 0.4;
        }
        myUnit.DamageEffects.PorcentualReduction = myUnit.DamageEffects.PorcentualReduction * percentage;
    }
}