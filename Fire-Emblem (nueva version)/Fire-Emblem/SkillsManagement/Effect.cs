using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Effect
{
    protected int cantidad;
    protected int amount;
    public Effect()
    {
    }
    public virtual void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        return;
    }
}

public class EmptyEffect : Effect
{
    public EmptyEffect() : base()
    {
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        return;
    }
}

public class ChangeHPIn : Effect
{
    public ChangeHPIn(int cantidad) : base()
    {
        this.cantidad = cantidad;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        myUnit.currentHp = myUnit.currentHp + this.cantidad;
    }
}

public class ReduceRivalsDefInPercentajeForFirstAttack : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsDefInPercentajeForFirstAttack(double reduction) : base()
    {
        this.reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int reduction = Convert.ToInt32(Math.Truncate(OponentsUnit.def * 0.5));
        OponentsUnit.activePenalties.defFirstAttack  -= reduction;
    }
}

public class ReduceRivalsResInPercentajeForFirstAttack : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsResInPercentajeForFirstAttack(double reduction) : base()
    {
        this.reductionPercentaje = reduction;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool attacking)
    {
        int reduction = Convert.ToInt32(Math.Truncate(OponentsUnit.res * 0.5));
        OponentsUnit.activePenalties.resFirstAttack  -= reduction;
    }
}


public class NeutralizeOponentsBonus : Effect
{
    
    public NeutralizeOponentsBonus() : base()
    {
    }
    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        OponentsUnit.activeBonusNeutralization.attk = 0;
        OponentsUnit.activeBonusNeutralization.atkFollowup = 0; 
        OponentsUnit.activeBonusNeutralization.atkFirstAttack = 0; 
        OponentsUnit.activeBonusNeutralization.spd = 0; 
        OponentsUnit.activeBonusNeutralization.def = 0; 
        OponentsUnit.activeBonusNeutralization.res = 0;
    }
}


public class NeutralizePenalties : Effect
{
    
    public NeutralizePenalties() : base()
    {
    }
    
    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        myUnit.activePenaltiesNeutralization.attk = 0;
        myUnit.activePenaltiesNeutralization.spd = 0;
        myUnit.activePenaltiesNeutralization.def = 0;
        myUnit.activePenaltiesNeutralization.res = 0;
    }
}

public class ChangeStatsIn : Effect
{
    private String stat;
    public ChangeStatsIn(String stat, int cantidad) : base()
    {
        this.cantidad = cantidad;
        this.stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        if (stat == "Atk")
        {
            if ( cantidad > 0) myUnit.activeBonus.attk  = myUnit.activeBonus.attk + this.cantidad;
            if ( cantidad < 0) myUnit.activePenalties.attk  = myUnit.activePenalties.attk + this.cantidad;
        }
        else if (stat == "Def")
        {
            if ( cantidad > 0) myUnit.activeBonus.def  = myUnit.activeBonus.def + this.cantidad;
            if ( cantidad < 0) myUnit.activePenalties.def  = myUnit.activePenalties.def + this.cantidad;
        }
        else if (stat == "Res")
        {
            if ( cantidad > 0) myUnit.activeBonus.res  = myUnit.activeBonus.res + this.cantidad;
            if ( cantidad < 0) myUnit.activePenalties.res  = myUnit.activePenalties.res + this.cantidad;
        }
        else if (stat == "Spd")
        {
            if ( cantidad > 0) myUnit.activeBonus.spd  = myUnit.activeBonus.spd + this.cantidad;
            if ( cantidad < 0) myUnit.activePenalties.spd  = myUnit.activePenalties.spd + this.cantidad;
        }
    }
}


public class ChangeRivalsStatsIn : Effect
{
    private String stat;
    public ChangeRivalsStatsIn(String stat, int cantidad) : base()
    {
        this.cantidad = cantidad;
        this.stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        if (stat == "Atk")
        {
            if ( cantidad > 0) OponentsUnit.activeBonus.attk  = OponentsUnit.activeBonus.attk + this.cantidad;
            if ( cantidad < 0) OponentsUnit.activePenalties.attk  = OponentsUnit.activePenalties.attk + this.cantidad;
        }
        else if (stat == "Def")
        {
            if ( cantidad > 0) OponentsUnit.activeBonus.def  = OponentsUnit.activeBonus.def + this.cantidad;
            if ( cantidad < 0) OponentsUnit.activePenalties.def  = OponentsUnit.activePenalties.def + this.cantidad;
        }
        else if (stat == "Res")
        {
            if ( cantidad > 0) OponentsUnit.activeBonus.res  = OponentsUnit.activeBonus.res + this.cantidad;
            if ( cantidad < 0) OponentsUnit.activePenalties.res  = OponentsUnit.activePenalties.res + this.cantidad;
        }
        else if (stat == "Spd")
        {
            if ( cantidad > 0) OponentsUnit.activeBonus.spd  = OponentsUnit.activeBonus.spd + this.cantidad;
            if ( cantidad < 0) OponentsUnit.activePenalties.spd  = OponentsUnit.activePenalties.spd + this.cantidad;
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

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        if (stat=="Atk" ) OponentsUnit.activeBonusNeutralization.attk  = 0;
        else if (stat == "Def" ) OponentsUnit.activeBonusNeutralization.def = 0;
        else if (stat == "Res" ) OponentsUnit.activeBonusNeutralization.res = 0;
        else if (stat == "Spd" ) OponentsUnit.activeBonusNeutralization.spd = 0;
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

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.attk * this.percentaje));
            myUnit.activeBonus.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.def * this.percentaje));
            myUnit.activeBonus.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.res * this.percentaje));
            myUnit.activeBonus.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.spd * this.percentaje));
            myUnit.activeBonus.spd += cantidad;
        }
    }
}

public class ChangeStatInPercentageOnlyForFirstAttack : Effect
{
    private String stat;
    private double percentaje;
    // arreglar esto, solo funciona para ataque
    public ChangeStatInPercentageOnlyForFirstAttack(String stat, Double percentaje) : base()
    {
        this.stat = stat;
        this.percentaje = percentaje;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.attk * this.percentaje));
            myUnit.activeBonus.atkFirstAttack  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.def * this.percentaje));
            myUnit.activeBonus.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.res * this.percentaje));
            myUnit.activeBonus.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(myUnit.spd * this.percentaje));
            myUnit.activeBonus.spd += cantidad;
        }
    }
}

public class ChangeStatsInBasePlusOnePointForEvery : Effect
{
    private String stat;
    private int amount;
    private int _baseIncrease;
    public ChangeStatsInBasePlusOnePointForEvery(String stat, int baseIncrease, int amount) : base()
    {
        this.stat = stat;
        this.amount = amount;
        this._baseIncrease = baseIncrease;
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            double division = myUnit.attk / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.attk  += (cantidad + _baseIncrease);
            
        }
        else if (stat == "Def")
        {
            double division = myUnit.def / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.def += (cantidad + _baseIncrease);
        }
        else if (stat == "Res")
        {
            double division = myUnit.res / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.res += (cantidad + _baseIncrease);
        }
        else if (stat == "Spd")
        {
            double division = myUnit.spd / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            myUnit.activeBonus.spd += (cantidad + _baseIncrease);
        }
        int cantidadFinal = cantidad + _baseIncrease;
    }
}

public class WrathEffect : Effect
{
    public WrathEffect() : base()
    {
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int cantidad = myUnit.hpMax - myUnit.currentHp;
        if (cantidad > 30) cantidad = 30;
        myUnit.activeBonus.attk += cantidad;
        myUnit.activeBonus.spd += cantidad;
    }
}

public class SoulbladeEffect : Effect
{
    public SoulbladeEffect() : base()
    {
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        double refDesAverage = (OponentsUnit.def + OponentsUnit.res) / 2;
        int refDesAverageInt = Convert.ToInt32(Math.Truncate(refDesAverage));
        int defChange = refDesAverageInt - OponentsUnit.def;
        int resChange = refDesAverageInt - OponentsUnit.res;
        if (defChange < 0) OponentsUnit.activePenalties.def += defChange;
        else
        {
            OponentsUnit.activeBonus.def += defChange;
        }    
        if (resChange < 0) OponentsUnit.activePenalties.res += resChange;
        else
        {
            OponentsUnit.activeBonus.res += resChange;
        }
        
    }
}

public class SandstormEffect : Effect
{
    public SandstormEffect() : base()
    {
    }

    public override void ApplyEffect(Unit myUnit, Unit OponentsUnit, bool atacando)
    {
        int cantidad = Convert.ToInt32(Math.Truncate(1.5 * myUnit.def)) - ((myUnit.attk));
        if (cantidad < 0) myUnit.activePenalties.atkFollowup += cantidad;
        else
        {
            myUnit.activeBonus.atkFollowup += cantidad;
        }
    }
}





