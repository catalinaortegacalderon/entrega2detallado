using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;

public class NeutralizeOneOfOpponentsBonusEffect : Effect
{
    private StatType _stat;
    public NeutralizeOneOfOpponentsBonusEffect(StatType stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat== StatType.Atk ) opponentsUnit.ActiveBonusNeutralizer.Atk  = 0;
        else if (_stat == StatType.Def ) opponentsUnit.ActiveBonusNeutralizer.Def = 0;
        else if (_stat == StatType.Res ) opponentsUnit.ActiveBonusNeutralizer.Res = 0;
        else if (_stat == StatType.Spd ) opponentsUnit.ActiveBonusNeutralizer.Spd = 0;
    }
}