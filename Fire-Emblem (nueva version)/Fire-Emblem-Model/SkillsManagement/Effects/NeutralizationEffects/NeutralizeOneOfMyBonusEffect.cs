namespace Fire_Emblem_Model;

public class NeutralizeOneOfMyBonusEffect : Effect
{
    private String _stat;
    public NeutralizeOneOfMyBonusEffect(String stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat=="Atk" ) myUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == "Def" ) myUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == "Res" ) myUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == "Spd" ) myUnit.ActiveBonusNeutralization.Spd = 0;
    }
}