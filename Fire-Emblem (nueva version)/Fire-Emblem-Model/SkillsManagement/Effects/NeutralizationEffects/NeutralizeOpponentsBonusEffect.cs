using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizeOpponentsBonusEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        opponentsUnit.ActiveBonusNeutralization.Attk = 0;
        opponentsUnit.ActiveBonusNeutralization.AtkFollowup = 0; 
        opponentsUnit.ActiveBonusNeutralization.AtkFirstAttack = 0; 
        opponentsUnit.ActiveBonusNeutralization.Spd = 0; 
        opponentsUnit.ActiveBonusNeutralization.Def = 0; 
        opponentsUnit.ActiveBonusNeutralization.Res = 0;
    }
}