namespace Fire_Emblem_Model;

public class NeutralizeOneOfOpponentsBonusEffect : Effect
{
    private String _stat;
    public NeutralizeOneOfOpponentsBonusEffect(String stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat=="Atk" ) opponentsUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == "Def" ) opponentsUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == "Res" ) opponentsUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == "Spd" ) opponentsUnit.ActiveBonusNeutralization.Spd = 0;
    }
}