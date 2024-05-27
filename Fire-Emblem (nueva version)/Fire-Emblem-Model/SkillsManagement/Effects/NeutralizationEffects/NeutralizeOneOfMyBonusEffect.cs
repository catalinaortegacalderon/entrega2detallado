using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class NeutralizeOneOfMyBonusEffect : Effect
{
    private StatType _stat;
    public NeutralizeOneOfMyBonusEffect(StatType stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        if (_stat== StatType.Atk ) myUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == StatType.Def ) myUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == StatType.Res ) myUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == StatType.Spd ) myUnit.ActiveBonusNeutralization.Spd = 0;
    }
}