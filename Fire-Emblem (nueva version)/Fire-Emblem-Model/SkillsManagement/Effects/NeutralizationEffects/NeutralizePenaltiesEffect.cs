using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizePenaltiesEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        myUnit.ActivePenaltiesNeutralization.Attk = 0;
        myUnit.ActivePenaltiesNeutralization.Spd = 0;
        myUnit.ActivePenaltiesNeutralization.Def = 0;
        myUnit.ActivePenaltiesNeutralization.Res = 0;
    }
}