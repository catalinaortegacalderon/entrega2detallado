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
    private double reductionPercentaje;
    public ReduceRivalsDefInPercentajeForFirstAttack(double reduction) : base()
    {
        this.reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.Def * 0.5));
        opponentsUnit.ActivePenalties.DefFirstAttack  -= reduction;
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
            if ( Amount > 0) opponentsUnit.ActiveBonus.Attk  = opponentsUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Attk  = opponentsUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (stat == "Def")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Def  = opponentsUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Def  = opponentsUnit.ActivePenalties.Def + this.Amount;
        }
        else if (stat == "Res")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Res  = opponentsUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Res  = opponentsUnit.ActivePenalties.Res + this.Amount;
        }
        else if (stat == "Spd")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Spd  = opponentsUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Spd  = opponentsUnit.ActivePenalties.Spd + this.Amount;
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
        if (stat=="Atk" ) opponentsUnit.ActiveBonusNeutralization.Attk  = 0;
        else if (stat == "Def" ) opponentsUnit.ActiveBonusNeutralization.Def = 0;
        else if (stat == "Res" ) opponentsUnit.ActiveBonusNeutralization.Res = 0;
        else if (stat == "Spd" ) opponentsUnit.ActiveBonusNeutralization.Spd = 0;
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
            myUnit.ActiveBonus.Attk  += Amount;
            
        }
        else if (stat == "Def")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Def * this.percentaje));
            myUnit.ActiveBonus.Def += Amount;
        }
        else if (stat == "Res")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Res * this.percentaje));
            myUnit.ActiveBonus.Res += Amount;
        }
        else if (stat == "Spd")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Spd * this.percentaje));
            myUnit.ActiveBonus.Spd += Amount;
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





