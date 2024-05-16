using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Effect
{
    protected int Priority;
    protected int Amount;
    public Effect()
    {
        this.Priority = 1;
    }
    public virtual void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        return;
    }

    public virtual int GetPriority()
    {
        return this.Priority;
    }
}

public class EmptyEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        return;
    }
}

public class ChangeHpIn : Effect
{
    public ChangeHpIn(int amount) : base()
    {
        this.Amount = amount;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (!myUnit.ActiveBonus.HpBonusActivated)
        {
            myUnit.CurrentHp = myUnit.CurrentHp + this.Amount; 
            myUnit.HpMax = myUnit.HpMax + this.Amount;
            myUnit.ActiveBonus.HpBonusActivated = true;
        }
    }
}

public class ReduceRivalsDefInPercentajeForFirstAttack : Effect
{
    private double _reductionPercentaje;
    public ReduceRivalsDefInPercentajeForFirstAttack(double reduction) : base()
    {
        this._reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Def * _reductionPercentaje));
        opponentsUnit.ActivePenalties.DefFirstAttack  -= reduction;
    }
}

public class ReduceRivalsResInPercentageForFirstAttack : Effect
{
    private double _reductionPercentage;
    public ReduceRivalsResInPercentageForFirstAttack(double reduction) : base()
    {
        this._reductionPercentage = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Res * _reductionPercentage));
        opponentsUnit.ActivePenalties.ResFirstAttack  -= reduction;
    }
}


public class NeutralizeOponentsBonus : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        opponentsUnit.ActiveBonusNeutralization.Attk = 0;
        opponentsUnit.ActiveBonusNeutralization.AtkFollowup = 0; 
        opponentsUnit.ActiveBonusNeutralization.AtkFirstAttack = 0; 
        opponentsUnit.ActiveBonusNeutralization.Spd = 0; 
        opponentsUnit.ActiveBonusNeutralization.Def = 0; 
        opponentsUnit.ActiveBonusNeutralization.Res = 0;
    }
}


public class NeutralizePenalties : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        myUnit.ActivePenaltiesNeutralization.Attk = 0;
        myUnit.ActivePenaltiesNeutralization.Spd = 0;
        myUnit.ActivePenaltiesNeutralization.Def = 0;
        myUnit.ActivePenaltiesNeutralization.Res = 0;
    }
}

public class ChangeStatsIn : Effect
{
    private String _stat;
    public ChangeStatsIn(String stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat == "Atk")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Attk  = myUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Attk  = myUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Def  = myUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Def  = myUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Res  = myUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Res  = myUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Spd  = myUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Spd  = myUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}

public class ChangeStatsInBasePercentage : Effect
{
    private String _stat;
    private double _percentage;
    public ChangeStatsInBasePercentage(String stat, double percentage) : base()
    {
        this._percentage =  percentage;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat == "Atk")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Attk  += (int)(this._percentage * myUnit.Atk);
            if ( _percentage < 0) myUnit.ActivePenalties.Attk  +=  (int)(this._percentage * myUnit.Atk);
        }
        else if (_stat == "Def")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Def  +=   (int)(this._percentage * myUnit.Def);
            if ( _percentage < 0) myUnit.ActivePenalties.Def  +=  (int)(this._percentage * myUnit.Def);
        }
        else if (_stat == "Res")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Res  +=  (int)(this._percentage * myUnit.Res);
            if ( _percentage < 0) myUnit.ActivePenalties.Res  +=  (int)(this._percentage * myUnit.Res);
        }
        else if (_stat == "Spd")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Spd  +=  (int)(this._percentage * myUnit.Spd);
            if ( _percentage < 0) myUnit.ActivePenalties.Spd  += (int)(this._percentage * myUnit.Spd);
        }
    }
}


public class ChangeRivalsStatsIn : Effect
{
    private String _stat;
    public ChangeRivalsStatsIn(String stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat == "Atk")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Attk  = opponentsUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Attk  = opponentsUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Def  = opponentsUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Def  = opponentsUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Res  = opponentsUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Res  = opponentsUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Spd  = opponentsUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Spd  = opponentsUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}

public class NeutralizeOneOfOpponentsBonus : Effect
{
    private String _stat;
    public NeutralizeOneOfOpponentsBonus(String stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat=="Atk" ) opponentsUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == "Def" ) opponentsUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == "Res" ) opponentsUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == "Spd" ) opponentsUnit.ActiveBonusNeutralization.Spd = 0;
    }
}

public class NeutralizeOneOfMyBonus : Effect
{
    private String _stat;
    public NeutralizeOneOfMyBonus(String stat) : base()
    {
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat=="Atk" ) myUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (_stat == "Def" ) myUnit.ActiveBonusNeutralization.Def = 0;
        else if (_stat == "Res" ) myUnit.ActiveBonusNeutralization.Res = 0;
        else if (_stat == "Spd" ) myUnit.ActiveBonusNeutralization.Spd = 0;
    }
}

public class ChangeStatInPercentageOnlyForFirstAttack : Effect
{
    private String _stat;
    private double _percentage;
    public ChangeStatInPercentageOnlyForFirstAttack(String stat, Double percentage) : base()
    {
        this._stat = stat;
        this._percentage = percentage;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        Amount = 0;
        if (_stat == "Atk")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Atk * this._percentage));
            myUnit.ActiveBonus.AtkFirstAttack  += Amount;
        }
    }
}

public class ChangeStatsInBasePlusOnePointForEvery : Effect
{
    private string _stat;
    private int _baseIncrease;
    private int _divisor;
    public ChangeStatsInBasePlusOnePointForEvery(String stat, int baseIncrease, int divisor) : base()
    {
        this._stat = stat;
        this._divisor = divisor;
        this._baseIncrease = baseIncrease;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (_stat == "Spd")
        {
            double division = (myUnit.Spd / _divisor);
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.ActiveBonus.Spd += (Amount + _baseIncrease);
        }
    }
}

public class WrathEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = myUnit.HpMax - myUnit.CurrentHp;
        if (amount > 30) amount = 30;
        myUnit.ActiveBonus.Attk += amount;
        myUnit.ActiveBonus.Spd += amount;
    }
}

public class SoulbladeEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
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

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = Convert.ToInt32(Math.Truncate(1.5 * myUnit.Def)) - ((myUnit.Atk));
        if (amount < 0) myUnit.ActivePenalties.AtkFollowup += amount;
        else
        {
            myUnit.ActiveBonus.AtkFollowup += amount;
        }
    }
}






