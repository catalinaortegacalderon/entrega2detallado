using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

public class ChangeStatsInEffect : Effect
{
    private readonly StatType _stat;

    public ChangeStatsInEffect(StatType stat, int amount)
    {
        Amount = amount;
        _stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)

    {
        if (_stat == StatType.Atk)
        {
            if (Amount > 0) myUnit.ActiveBonus.Atk = myUnit.ActiveBonus.Atk + Amount;
            if (Amount < 0) myUnit.ActivePenalties.Atk = myUnit.ActivePenalties.Atk + Amount;
        }
        else if (_stat == StatType.Def)
        {
            if (Amount > 0) myUnit.ActiveBonus.Def = myUnit.ActiveBonus.Def + Amount;
            if (Amount < 0) myUnit.ActivePenalties.Def = myUnit.ActivePenalties.Def + Amount;
        }
        else if (_stat == StatType.Res)
        {
            if (Amount > 0) myUnit.ActiveBonus.Res = myUnit.ActiveBonus.Res + Amount;
            if (Amount < 0) myUnit.ActivePenalties.Res = myUnit.ActivePenalties.Res + Amount;
        }
        else if (_stat == StatType.Spd)
        {
            if (Amount > 0) myUnit.ActiveBonus.Spd = myUnit.ActiveBonus.Spd + Amount;
            if (Amount < 0) myUnit.ActivePenalties.Spd = myUnit.ActivePenalties.Spd + Amount;
        }
    }
}