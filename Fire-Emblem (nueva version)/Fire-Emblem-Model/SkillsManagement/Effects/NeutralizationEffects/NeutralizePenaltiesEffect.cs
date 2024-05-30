using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizePenaltiesEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        myUnit.ActivePenaltiesNeutralizator.Attk = 0;
        myUnit.ActivePenaltiesNeutralizator.Spd = 0;
        myUnit.ActivePenaltiesNeutralizator.Def = 0;
        myUnit.ActivePenaltiesNeutralizator.Res = 0;
    }
}