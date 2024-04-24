using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Effect
{
    protected int cantidad; // Este parámetro será accedido por algunas clases solamente, no repetir código
    protected View view; // Permitir acceso desde clases hijas

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
        unitPropia.hp_actual = unitPropia.hp_actual + this.cantidad;
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
        OponentsUnit.activeBonus.spd  = OponentsUnit.activeBonus.spd - reduction;
        // ver si se imprime aca
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Spd-" + reduction);
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
        OponentsUnit.activeBonus.spd  = OponentsUnit.activeBonus.spd - reduction;
        // ver si se imprime aca
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Def-" + reduction);
    }
}


public class NeutralizeOponentsBonus : Effect
{
    
    public NeutralizeOponentsBonus(View view) : base(view)
    {
    }
    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        if (OponentsUnit.activeBonus.attk > 0 )
        {
            OponentsUnit.activeBonus.attk = 0;
        }
        if (OponentsUnit.activeBonus.spd > 0 )
        {
            OponentsUnit.activeBonus.spd = 0;
        }
        if (OponentsUnit.activeBonus.def > 0 )
        {
            OponentsUnit.activeBonus.def = 0;
        }
        if (OponentsUnit.activeBonus.res > 0 )
        {
            OponentsUnit.activeBonus.res = 0;
        }
        this.view.WriteLine("Los bonus de Atk de " + OponentsUnit.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Spd de " + OponentsUnit.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Def de " + OponentsUnit.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Res de " + OponentsUnit.nombre + " fueron neutralizados");
    }
}

// TAL VEZ HACER UNO DE BONUS Y UNO DE PENALTIES PARA QUE NO SE MEZCLEN
public class NeutralizePenalties : Effect
{
    
    public NeutralizePenalties(View view) : base(view)
    {
    }
    
    // VER SI SOLO ES CUANDO BONUS ES POSITIVO O SI ES SIEMPRE Y LO IMPRIMO SOLO CUAND HAT, TAL VEZ NEUTRALIZO ANTES DE APLICAR
    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        if (unitPropia.activePenalties.attk < 0 )
        {
            unitPropia.activeBonusNeutralization.attk = 0;
        }
        if (unitPropia.activePenalties.spd < 0 )
        {
            unitPropia.activeBonusNeutralization.spd = 0;
        }
        if (unitPropia.activePenalties.def < 0 )
        {
            unitPropia.activeBonusNeutralization.def = 0;
        }
        if (unitPropia.activePenalties.res < 0 )
        {
            unitPropia.activeBonusNeutralization.res = 0;
        }
        this.view.WriteLine("Los penalties de Atk de " + unitPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Spd de " + unitPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Def de " + unitPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Res de " + unitPropia.nombre + " fueron neutralizados");
    }
}

//  ELIMINAR CHANGEATK CHANGESPD.. DEJAR SOLO ESTO:

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
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(unitPropia.nombre + " obtiene " + stat + signo + this.cantidad);
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
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(OponentsUnit.nombre + " obtiene " + stat + signo + this.cantidad);
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
        if (stat=="Atk" && OponentsUnit.activeBonus.attk > 0) OponentsUnit.activeBonusNeutralization.attk  = 0;
        else if (stat == "Def" && OponentsUnit.activeBonus.attk > 0) OponentsUnit.activeBonusNeutralization.def = 0;
        else if (stat == "Res" && OponentsUnit.activeBonus.attk > 0) OponentsUnit.activeBonusNeutralization.res = 0;
        else if (stat == "Spd"&& OponentsUnit.activeBonus.attk > 0) OponentsUnit.activeBonusNeutralization.spd = 0;
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine("Los bonus de " + this.stat + " de " + OponentsUnit.nombre + " fueron neutralizados");
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
        string signo = (this.percentaje > 0) ? "+" :  "";
        this.view.WriteLine(unitPropia.nombre + " obtiene " + stat + signo + cantidad);
    }
}

public class ChangeStatsOnePointForEvery : Effect
{
    private String stat;
    private int amount;
    public ChangeStatsOnePointForEvery(View view, String stat, int amount) : base(view)
    {
        this.stat = stat;
        this.amount = amount;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            double division = unitPropia.attk / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            double division = unitPropia.def / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.def += cantidad;
        }
        else if (stat == "Res")
        {
            double division = unitPropia.res / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.res += cantidad;
        }
        else if (stat == "Spd")
        {
            double division = unitPropia.spd / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.activeBonus.spd += cantidad;
        }
        this.view.WriteLine(unitPropia.nombre + " obtiene " + stat + "+" + cantidad);
    }
}

public class WrathEffect : Effect
{
    public WrathEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        int cantidad = unitPropia.hp_max - unitPropia.hp_actual;
        if (cantidad > 30) cantidad = 30;
        unitPropia.activeBonus.attk += cantidad;
        unitPropia.activeBonus.spd += cantidad;
        this.view.WriteLine(unitPropia.nombre + " obtiene Atk+" + this.cantidad);
        this.view.WriteLine(unitPropia.nombre + " obtiene Spd+"+ this.cantidad);
        
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
        string signoDef = (cantidadDef > 0) ? "+" :  "";
        string signoRes = (cantidadRes > 0) ? "+" :  "";
        OponentsUnit.activeBonus.def += cantidadDef;
        OponentsUnit.activeBonus.res += cantidadRes;
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Def" + signoDef + this.cantidad);
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Res"+ signoRes + this.cantidad);
        
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
        string signo = (cantidad > 0) ? "+" :  "";
        OponentsUnit.activeBonus.atkFollowup += cantidad;
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}





