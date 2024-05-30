using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizeOneOfMyBonusEffect : Effect
{
    private StatType _stat;
    public NeutralizeOneOfMyBonusEffect(StatType stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        if (_stat== StatType.Atk ) myUnit.ActiveBonusNeutralizator.Attk  = 0;
        else if (_stat == StatType.Def ) myUnit.ActiveBonusNeutralizator.Def = 0;
        else if (_stat == StatType.Res ) myUnit.ActiveBonusNeutralizator.Res = 0;
        else if (_stat == StatType.Spd ) myUnit.ActiveBonusNeutralizator.Spd = 0;
    }
}