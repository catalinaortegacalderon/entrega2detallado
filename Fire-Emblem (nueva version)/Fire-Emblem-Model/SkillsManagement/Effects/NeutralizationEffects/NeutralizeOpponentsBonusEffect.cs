namespace Fire_Emblem_Model;

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