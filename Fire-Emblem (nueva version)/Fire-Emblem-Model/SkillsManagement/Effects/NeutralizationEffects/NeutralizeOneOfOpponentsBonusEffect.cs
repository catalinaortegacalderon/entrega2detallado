using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class NeutralizeOneOfOpponentsBonusEffect : Effect
{
    private StatType _stat;
    public NeutralizeOneOfOpponentsBonusEffect(StatType stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat== StatType.Atk ) opponentsUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == StatType.Def ) opponentsUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == StatType.Res ) opponentsUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == StatType.Spd ) opponentsUnit.ActiveBonusNeutralization.Spd = 0;
    }
}