using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Effect
{
    protected int cantidad; // Este parámetro será accedido por algunas clases solamente, no repetir código
    protected View view; // Permitir acceso desde clases hijas
    // como es protected, con _?

    public Effect(View view)
    {
        this.view = view;
    }

    public virtual void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        return;
    }
}

public class EmptyEffect : Effect
{
    public EmptyEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        return;
    }
}

public class ChangeHPIn : Effect
{
    public ChangeHPIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        unitPropia.currentHp = unitPropia.currentHp + this.cantidad;
    }
}

public class ReduceRivalsSpdInPercentaje : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsSpdInPercentaje(View view, double reduction) : base(view)
    {
        this.reductionPercentaje = reduction;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int reduction = Convert.ToInt32(Math.Truncate(OponentsUnit.spd * 0.5));
        OponentsUnit.activePenalties.spd  = OponentsUnit.activePenalties.spd - reduction;
    }
}
public class ReduceRivalsDefInPercentaje : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsDefInPercentaje(View view, double reduction) : base(view)
    {
        this.reductionPercentaje = reduction;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int reduction = Convert.ToInt32(Math.Truncate(OponentsUnit.spd * 0.5));
        OponentsUnit.activePenalties.spd  = OponentsUnit.activePenalties.spd - reduction;
    }
}


public class NeutralizeOponentsBonus : Effect
{
    
    public NeutralizeOponentsBonus(View view) : base(view)
    {
    }
    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        Console.WriteLine("aplicando efectos");
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
    
    public NeutralizePenalties(View view) : base(view)
    {
    }
    
    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        unitPropia.activePenaltiesNeutralization.attk = 0;
        unitPropia.activePenaltiesNeutralization.spd = 0;
        unitPropia.activePenaltiesNeutralization.def = 0;
        unitPropia.activePenaltiesNeutralization.res = 0;
    }
}

public class ChangeStatsIn : Effect
{
    private String stat;
    public ChangeStatsIn(View view, String stat, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
        this.stat = stat;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        if (stat == "Atk")
        {
            if ( cantidad > 0) unitPropia.activeBonus.attk  = unitPropia.activeBonus.attk + this.cantidad;
            if ( cantidad < 0) unitPropia.activePenalties.attk  = unitPropia.activePenalties.attk + this.cantidad;
        }
        else if (stat == "Def")
        {
            if ( cantidad > 0) unitPropia.activeBonus.def  = unitPropia.activeBonus.def + this.cantidad;
            if ( cantidad < 0) unitPropia.activePenalties.def  = unitPropia.activePenalties.def + this.cantidad;
        }
        else if (stat == "Res")
        {
            if ( cantidad > 0) unitPropia.activeBonus.res  = unitPropia.activeBonus.res + this.cantidad;
            if ( cantidad < 0) unitPropia.activePenalties.res  = unitPropia.activePenalties.res + this.cantidad;
        }
        else if (stat == "Spd")
        {
            if ( cantidad > 0) unitPropia.activeBonus.spd  = unitPropia.activeBonus.spd + this.cantidad;
            if ( cantidad < 0) unitPropia.activePenalties.spd  = unitPropia.activePenalties.spd + this.cantidad;
        }
    }
}


public class ChangeRivalsStatsIn : Effect
{
    private String stat;
    public ChangeRivalsStatsIn(View view, String stat, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
        this.stat = stat;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
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
    public NeutralizeOneOfOponentsBonus(View view, String stat) : base(view)
    {
        this.stat = stat;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
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
    public ChangeStatInPercentaje(View view, String stat, Double percentaje) : base(view)
    {
        this.stat = stat;
        this.percentaje = percentaje;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.attk * this.percentaje));
            unitPropia.activeBonus.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.def * this.percentaje));
            unitPropia.activeBonus.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.res * this.percentaje));
            unitPropia.activeBonus.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.spd * this.percentaje));
            unitPropia.activeBonus.spd += cantidad;
        }
    }
}

public class ChangeStatInPercentageOnlyForFirstAttack : Effect
{
    private String stat;
    private double percentaje;
    // arreglar esto, solo funciona para ataque
    public ChangeStatInPercentageOnlyForFirstAttack(View view, String stat, Double percentaje) : base(view)
    {
        this.stat = stat;
        this.percentaje = percentaje;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.attk * this.percentaje));
            unitPropia.activeBonus.atkFirstAttack  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.def * this.percentaje));
            unitPropia.activeBonus.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.res * this.percentaje));
            unitPropia.activeBonus.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.spd * this.percentaje));
            unitPropia.activeBonus.spd += cantidad;
        }
    }
}

public class ChangeStatsInBasePlusOnePointForEvery : Effect
{
    private String stat;
    private int amount;
    private int _baseIncrease;
    public ChangeStatsInBasePlusOnePointForEvery(View view, String stat, int baseIncrease, int amount) : base(view)
    {
        this.stat = stat;
        this.amount = amount;
        this._baseIncrease = baseIncrease;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            double division = unitPropia.attk / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.attk  += (cantidad + _baseIncrease);
            
        }
        else if (stat == "Def")
        {
            double division = unitPropia.def / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.def += (cantidad + _baseIncrease);
        }
        else if (stat == "Res")
        {
            double division = unitPropia.res / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.res += (cantidad + _baseIncrease);
        }
        else if (stat == "Spd")
        {
            double division = unitPropia.spd / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.spd += (cantidad + _baseIncrease);
        }
        int cantidadFinal = cantidad + _baseIncrease;
    }
}

public class WrathEffect : Effect
{
    public WrathEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = unitPropia.hpMax - unitPropia.currentHp;
        if (cantidad > 30) cantidad = 30;
        unitPropia.activeBonus.attk += cantidad;
        unitPropia.activeBonus.spd += cantidad;
    }
}

public class SoulbladeEffect : Effect
{
    public SoulbladeEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        double promedioDefResDouble = (OponentsUnit.def + OponentsUnit.res) / 2;
        int promedioDefRes = Convert.ToInt32(Math.Truncate(promedioDefResDouble));
        int cantidadDef = promedioDefRes - OponentsUnit.def;
        int cantidadRes = promedioDefRes - OponentsUnit.res;
        if (cantidadDef < 0) OponentsUnit.activePenalties.def += cantidadDef;
        else
        {
            OponentsUnit.activeBonus.def += cantidadDef;
        }    
        if (cantidadRes < 0) OponentsUnit.activePenalties.res += cantidadRes;
        else
        {
            OponentsUnit.activeBonus.res += cantidadRes;
        }
        
    }
}

public class SandstormEffect : Effect
{
    public SandstormEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = Convert.ToInt32(Math.Truncate(1.5 * unitPropia.def - unitPropia.attk));
        OponentsUnit.activeBonus.atkFollowup += cantidad;
    }
}





