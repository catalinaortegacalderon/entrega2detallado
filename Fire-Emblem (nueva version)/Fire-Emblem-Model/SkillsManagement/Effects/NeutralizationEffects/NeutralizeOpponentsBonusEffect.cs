using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizeOpponentsBonusEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        opponentsUnit.ActiveBonusNeutralizator.Attk = 0;
        opponentsUnit.ActiveBonusNeutralizator.AtkFollowup = 0; 
        opponentsUnit.ActiveBonusNeutralizator.AtkFirstAttack = 0; 
        opponentsUnit.ActiveBonusNeutralizator.Spd = 0; 
        opponentsUnit.ActiveBonusNeutralizator.Def = 0; 
        opponentsUnit.ActiveBonusNeutralizator.Res = 0;
    }
}