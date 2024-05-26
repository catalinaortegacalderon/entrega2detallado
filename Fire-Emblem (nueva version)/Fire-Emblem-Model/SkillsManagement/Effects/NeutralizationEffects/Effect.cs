using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;
using System; 


public class Effect
{
    protected int Priority;
    protected int Amount;
    public Effect()
    {
        this.Priority = 1;
    }
    public virtual void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        return;
    }

    public virtual int GetPriority()
    {
        return this.Priority;
    }
}

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

public class WrathEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int amount = myUnit.HpMax - myUnit.CurrentHp;
        if (amount > 30) amount = 30;
        myUnit.ActiveBonus.Attk += amount;
        myUnit.ActiveBonus.Spd += amount;
    }
}

public class SoulbladeEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        double refDesAverage = (opponentsUnit.Def + opponentsUnit.Res) / 2;
        int refDesAverageInt = Convert.ToInt32(Math.Truncate(refDesAverage));
        int defChange = refDesAverageInt - opponentsUnit.Def;
        int resChange = refDesAverageInt - opponentsUnit.Res;
        if (defChange < 0) opponentsUnit.ActivePenalties.Def += defChange;
        else
        {
            opponentsUnit.ActiveBonus.Def += defChange;
        }    
        if (resChange < 0) opponentsUnit.ActivePenalties.Res += resChange;
        else
        {
            opponentsUnit.ActiveBonus.Res += resChange;
        }
    }
}

public class SandstormEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        int amount = Convert.ToInt32(Math.Truncate(1.5 * myUnit.Def)) - ((myUnit.Atk));
        if (amount < 0) myUnit.ActivePenalties.AtkFollowup += amount;
        else
        {
            myUnit.ActiveBonus.AtkFollowup += amount;
        }
    }
}






