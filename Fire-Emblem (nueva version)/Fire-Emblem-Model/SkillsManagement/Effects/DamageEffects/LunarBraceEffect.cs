using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class LunarBraceEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        int amount = opponentsUnit.Def + opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralization.Def
                                       + opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenalties.Def;
        Console.WriteLine(amount);
        myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + (int)Math.Truncate(0.3 * amount);
    }
}