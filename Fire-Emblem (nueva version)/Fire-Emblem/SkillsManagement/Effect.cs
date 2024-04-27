using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Effect
{
    protected int Amount;
    protected Effect()
    {
    }
    public virtual void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        return;
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
        if (!myUnit.ActiveBonus.hpBonusActivated)
        {
            myUnit.CurrentHp = myUnit.CurrentHp + this.Amount; 
            myUnit.HpMax = myUnit.HpMax + this.Amount;
            myUnit.ActiveBonus.hpBonusActivated = true;
        }
    }
}

public class ReduceRivalsDefInPercentajeForFirstAttack : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsDefInPercentajeForFirstAttack(double reduction) : base()
    {
        this.reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Def * 0.5));
        opponentsUnit.ActivePenalties.defFirstAttack  -= reduction;
    }
}

public class ReduceRivalsResInPercentajeForFirstAttack : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsResInPercentajeForFirstAttack(double reduction) : base()
    {
        this.reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Res * 0.5));
        opponentsUnit.ActivePenalties.resFirstAttack  -= reduction;
    }
}


public class NeutralizeOponentsBonus : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        opponentsUnit.ActiveBonusNeutralization.attk = 0;
        opponentsUnit.ActiveBonusNeutralization.atkFollowup = 0; 
        opponentsUnit.ActiveBonusNeutralization.atkFirstAttack = 0; 
        opponentsUnit.ActiveBonusNeutralization.spd = 0; 
        opponentsUnit.ActiveBonusNeutralization.def = 0; 
        opponentsUnit.ActiveBonusNeutralization.res = 0;
    }
}


public class NeutralizePenalties : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        myUnit.ActivePenaltiesNeutralization.attk = 0;
        myUnit.ActivePenaltiesNeutralization.spd = 0;
        myUnit.ActivePenaltiesNeutralization.def = 0;
        myUnit.ActivePenaltiesNeutralization.res = 0;
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
            if ( Amount > 0) myUnit.ActiveBonus.attk  = myUnit.ActiveBonus.attk + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.attk  = myUnit.ActivePenalties.attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) myUnit.ActiveBonus.def  = myUnit.ActiveBonus.def + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.def  = myUnit.ActivePenalties.def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) myUnit.ActiveBonus.res  = myUnit.ActiveBonus.res + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.res  = myUnit.ActivePenalties.res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) myUnit.ActiveBonus.spd  = myUnit.ActiveBonus.spd + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.spd  = myUnit.ActivePenalties.spd + this.Amount;
        }
    }
}


public class ChangeRivalsStatsIn : Effect
{
    private String stat;
    public ChangeRivalsStatsIn(String stat, int amount) : base()
    {
        this.Amount = amount;
        this.stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (stat == "Atk")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.attk  = opponentsUnit.ActiveBonus.attk + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.attk  = opponentsUnit.ActivePenalties.attk + this.Amount;
        }
        else if (stat == "Def")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.def  = opponentsUnit.ActiveBonus.def + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.def  = opponentsUnit.ActivePenalties.def + this.Amount;
        }
        else if (stat == "Res")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.res  = opponentsUnit.ActiveBonus.res + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.res  = opponentsUnit.ActivePenalties.res + this.Amount;
        }
        else if (stat == "Spd")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.spd  = opponentsUnit.ActiveBonus.spd + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.spd  = opponentsUnit.ActivePenalties.spd + this.Amount;
        }
    }
}

public class NeutralizeOneOfOponentsBonus : Effect
{
    private String stat;
    public NeutralizeOneOfOponentsBonus(String stat) : base()
    {
        this.stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        if (stat=="Atk" ) opponentsUnit.ActiveBonusNeutralization.attk  = 0;
        else if (stat == "Def" ) opponentsUnit.ActiveBonusNeutralization.def = 0;
        else if (stat == "Res" ) opponentsUnit.ActiveBonusNeutralization.res = 0;
        else if (stat == "Spd" ) opponentsUnit.ActiveBonusNeutralization.spd = 0;
    }
}

public class ChangeStatInPercentaje : Effect
{
    private String stat;
    private double percentaje;
    public ChangeStatInPercentaje(String stat, Double percentaje) : base()
    {
        this.stat = stat;
        this.percentaje = percentaje;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        Amount = 0;
        if (stat == "Atk")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Atk * this.percentaje));
            myUnit.ActiveBonus.attk  += Amount;
            
        }
        else if (stat == "Def")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Def * this.percentaje));
            myUnit.ActiveBonus.def += Amount;
        }
        else if (stat == "Res")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Res * this.percentaje));
            myUnit.ActiveBonus.res += Amount;
        }
        else if (stat == "Spd")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Spd * this.percentaje));
            myUnit.ActiveBonus.spd += Amount;
        }
    }
}

public class ChangeStatInPercentageOnlyForFirstAttack : Effect
{
    private String Stat;
    private double _percentage;
    // arreglar esto, solo funciona para ataque
    public ChangeStatInPercentageOnlyForFirstAttack(String stat, Double percentage) : base()
    {
        this.Stat = stat;
        this._percentage = percentage;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        Amount = 0;
        if (Stat == "Atk")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Atk * this._percentage));
            myUnit.ActiveBonus.atkFirstAttack  += Amount;
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
            myUnit.ActiveBonus.spd += (Amount + _baseIncrease);
        }
    }
}

public class WrathEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = myUnit.HpMax - myUnit.CurrentHp;
        if (amount > 30) amount = 30;
        myUnit.ActiveBonus.attk += amount;
        myUnit.ActiveBonus.spd += amount;
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
        if (defChange < 0) opponentsUnit.ActivePenalties.def += defChange;
        else
        {
            opponentsUnit.ActiveBonus.def += defChange;
        }    
        if (resChange < 0) opponentsUnit.ActivePenalties.res += resChange;
        else
        {
            opponentsUnit.ActiveBonus.res += resChange;
        }
        
    }
}

public class SandstormEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = Convert.ToInt32(Math.Truncate(1.5 * myUnit.Def)) - ((myUnit.Atk));
        if (amount < 0) myUnit.ActivePenalties.atkFollowup += amount;
        else
        {
            myUnit.ActiveBonus.atkFollowup += amount;
        }
    }
}





