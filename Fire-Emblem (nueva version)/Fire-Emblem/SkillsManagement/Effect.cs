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
        myUnit.currentHp = myUnit.currentHp + this.Amount;
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
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.def * 0.5));
        opponentsUnit.activePenalties.defFirstAttack  -= reduction;
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
        int reduction = Convert.ToInt32(Math.Truncate(opponentsUnit.res * 0.5));
        opponentsUnit.activePenalties.resFirstAttack  -= reduction;
    }
}


public class NeutralizeOponentsBonus : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        opponentsUnit.activeBonusNeutralization.attk = 0;
        opponentsUnit.activeBonusNeutralization.atkFollowup = 0; 
        opponentsUnit.activeBonusNeutralization.atkFirstAttack = 0; 
        opponentsUnit.activeBonusNeutralization.spd = 0; 
        opponentsUnit.activeBonusNeutralization.def = 0; 
        opponentsUnit.activeBonusNeutralization.res = 0;
    }
}


public class NeutralizePenalties : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        myUnit.activePenaltiesNeutralization.attk = 0;
        myUnit.activePenaltiesNeutralization.spd = 0;
        myUnit.activePenaltiesNeutralization.def = 0;
        myUnit.activePenaltiesNeutralization.res = 0;
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
            if ( Amount > 0) myUnit.activeBonus.attk  = myUnit.activeBonus.attk + this.Amount;
            if ( Amount < 0) myUnit.activePenalties.attk  = myUnit.activePenalties.attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) myUnit.activeBonus.def  = myUnit.activeBonus.def + this.Amount;
            if ( Amount < 0) myUnit.activePenalties.def  = myUnit.activePenalties.def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) myUnit.activeBonus.res  = myUnit.activeBonus.res + this.Amount;
            if ( Amount < 0) myUnit.activePenalties.res  = myUnit.activePenalties.res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) myUnit.activeBonus.spd  = myUnit.activeBonus.spd + this.Amount;
            if ( Amount < 0) myUnit.activePenalties.spd  = myUnit.activePenalties.spd + this.Amount;
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
            if ( Amount > 0) opponentsUnit.activeBonus.attk  = opponentsUnit.activeBonus.attk + this.Amount;
            if ( Amount < 0) opponentsUnit.activePenalties.attk  = opponentsUnit.activePenalties.attk + this.Amount;
        }
        else if (stat == "Def")
        {
            if ( Amount > 0) opponentsUnit.activeBonus.def  = opponentsUnit.activeBonus.def + this.Amount;
            if ( Amount < 0) opponentsUnit.activePenalties.def  = opponentsUnit.activePenalties.def + this.Amount;
        }
        else if (stat == "Res")
        {
            if ( Amount > 0) opponentsUnit.activeBonus.res  = opponentsUnit.activeBonus.res + this.Amount;
            if ( Amount < 0) opponentsUnit.activePenalties.res  = opponentsUnit.activePenalties.res + this.Amount;
        }
        else if (stat == "Spd")
        {
            if ( Amount > 0) opponentsUnit.activeBonus.spd  = opponentsUnit.activeBonus.spd + this.Amount;
            if ( Amount < 0) opponentsUnit.activePenalties.spd  = opponentsUnit.activePenalties.spd + this.Amount;
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
        if (stat=="Atk" ) opponentsUnit.activeBonusNeutralization.attk  = 0;
        else if (stat == "Def" ) opponentsUnit.activeBonusNeutralization.def = 0;
        else if (stat == "Res" ) opponentsUnit.activeBonusNeutralization.res = 0;
        else if (stat == "Spd" ) opponentsUnit.activeBonusNeutralization.spd = 0;
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
            Amount = Convert.ToInt32(Math.Truncate(myUnit.attk * this.percentaje));
            myUnit.activeBonus.attk  += Amount;
            
        }
        else if (stat == "Def")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.def * this.percentaje));
            myUnit.activeBonus.def += Amount;
        }
        else if (stat == "Res")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.res * this.percentaje));
            myUnit.activeBonus.res += Amount;
        }
        else if (stat == "Spd")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.spd * this.percentaje));
            myUnit.activeBonus.spd += Amount;
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
            Amount = Convert.ToInt32(Math.Truncate(myUnit.attk * this._percentage));
            myUnit.activeBonus.atkFirstAttack  += Amount;
            
        }
        else if (Stat == "Def")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.def * this._percentage));
            myUnit.activeBonus.def += Amount;
        }
        else if (Stat == "Res")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.res * this._percentage));
            myUnit.activeBonus.res += Amount;
        }
        else if (Stat == "Spd")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.spd * this._percentage));
            myUnit.activeBonus.spd += Amount;
        }
    }
}

public class ChangeStatsInBasePlusOnePointForEvery : Effect
{
    private String stat;
    private int _baseIncrease;
    public ChangeStatsInBasePlusOnePointForEvery(String stat, int baseIncrease, int amount) : base()
    {
        this.stat = stat;
        this.Amount = amount;
        this._baseIncrease = baseIncrease;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        //int Amount = 0;
        if (stat == "Atk")
        {
            double division = myUnit.attk / Amount;
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.attk  += (Amount + _baseIncrease);
            
        }
        else if (stat == "Def")
        {
            double division = myUnit.def / Amount;
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.def += (Amount + _baseIncrease);
        }
        else if (stat == "Res")
        {
            double division = myUnit.res / Amount;
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.res += (Amount + _baseIncrease);
        }
        else if (stat == "Spd")
        {
            double division = myUnit.spd / Amount;
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.spd += (Amount + _baseIncrease);
        }
        int amountFinal = Amount + _baseIncrease;
    }
}

public class WrathEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = myUnit.hpMax - myUnit.currentHp;
        if (amount > 30) amount = 30;
        myUnit.activeBonus.attk += amount;
        myUnit.activeBonus.spd += amount;
    }
}

public class SoulbladeEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        double refDesAverage = (opponentsUnit.def + opponentsUnit.res) / 2;
        int refDesAverageInt = Convert.ToInt32(Math.Truncate(refDesAverage));
        int defChange = refDesAverageInt - opponentsUnit.def;
        int resChange = refDesAverageInt - opponentsUnit.res;
        if (defChange < 0) opponentsUnit.activePenalties.def += defChange;
        else
        {
            opponentsUnit.activeBonus.def += defChange;
        }    
        if (resChange < 0) opponentsUnit.activePenalties.res += resChange;
        else
        {
            opponentsUnit.activeBonus.res += resChange;
        }
        
    }
}

public class SandstormEffect : Effect
{

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = Convert.ToInt32(Math.Truncate(1.5 * myUnit.def)) - ((myUnit.attk));
        if (amount < 0) myUnit.activePenalties.atkFollowup += amount;
        else
        {
            myUnit.activeBonus.atkFollowup += amount;
        }
    }
}





