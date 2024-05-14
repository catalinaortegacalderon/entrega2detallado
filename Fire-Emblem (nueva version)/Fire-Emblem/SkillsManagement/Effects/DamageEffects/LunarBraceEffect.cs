namespace Fire_Emblem;

public class LunarBraceEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = opponentsUnit.Def + opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralization.Def
                                       + opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenalties.Def;
        myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + (int)Math.Truncate(0.3 * amount);
    }
}