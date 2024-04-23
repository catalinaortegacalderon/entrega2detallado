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

public class ChangeSpdIn : Effect
{
    public ChangeSpdIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        unitPropia.ActiveBonusAndPenalties.spd = unitPropia.ActiveBonusAndPenalties.spd + this.cantidad;
        Console.WriteLine("Paso por donde quiero");
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(unitPropia.nombre + " obtiene Spd" + signo + this.cantidad);
    }
}

public class ChangeDefIn : Effect
{
    public ChangeDefIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unitPropia.ActiveBonusAndPenalties.def = unitPropia.ActiveBonusAndPenalties.def + this.cantidad;
        this.view.WriteLine(unitPropia.nombre + " obtiene Def" + signo + this.cantidad);
    }
}

public class ChangeResIn : Effect
{
    public ChangeResIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unitPropia.ActiveBonusAndPenalties.res = unitPropia.ActiveBonusAndPenalties.res + this.cantidad;
        this.view.WriteLine(unitPropia.nombre + " obtiene Res" + signo + this.cantidad);
    }
}

public class ChangeAtkIn : Effect
{
    public ChangeAtkIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unitPropia.ActiveBonusAndPenalties.attk = unitPropia.ActiveBonusAndPenalties.attk + this.cantidad;
        this.view.WriteLine(unitPropia.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}

public class ChangeRivalsAtkIn : Effect
{
    public ChangeRivalsAtkIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        OponentsUnit.ActiveBonusAndPenalties.attk  = OponentsUnit.ActiveBonusAndPenalties.attk + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}

public class ChangeRivalsSpdIn : Effect
{
    public ChangeRivalsSpdIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        OponentsUnit.ActiveBonusAndPenalties.spd  = OponentsUnit.ActiveBonusAndPenalties.spd + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Spd" + signo + this.cantidad);
    }
}

public class ChangeRivalsDefIn : Effect
{
    public ChangeRivalsDefIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        OponentsUnit.ActiveBonusAndPenalties.def  = OponentsUnit.ActiveBonusAndPenalties.def + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Def" + signo + this.cantidad);
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
        OponentsUnit.ActiveBonusAndPenalties.spd  = OponentsUnit.ActiveBonusAndPenalties.spd - reduction;
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
        OponentsUnit.ActiveBonusAndPenalties.spd  = OponentsUnit.ActiveBonusAndPenalties.spd - reduction;
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
        if (OponentsUnit.ActiveBonusAndPenalties.attk > 0 )
        {
            OponentsUnit.ActiveBonusAndPenalties.attk = 0;
        }
        if (OponentsUnit.ActiveBonusAndPenalties.spd > 0 )
        {
            OponentsUnit.ActiveBonusAndPenalties.spd = 0;
        }
        if (OponentsUnit.ActiveBonusAndPenalties.def > 0 )
        {
            OponentsUnit.ActiveBonusAndPenalties.def = 0;
        }
        if (OponentsUnit.ActiveBonusAndPenalties.res > 0 )
        {
            OponentsUnit.ActiveBonusAndPenalties.res = 0;
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
    public override void Aplicar(Unit unitPropia, Unit OponentsUnit, bool atacando)
    {
        if (unitPropia.ActiveBonusAndPenalties.attk < 0 )
        {
            unitPropia.ActiveBonusAndPenalties.attk = 0;
        }
        if (unitPropia.ActiveBonusAndPenalties.spd < 0 )
        {
            unitPropia.ActiveBonusAndPenalties.spd = 0;
        }
        if (unitPropia.ActiveBonusAndPenalties.def < 0 )
        {
            unitPropia.ActiveBonusAndPenalties.def = 0;
        }
        if (unitPropia.ActiveBonusAndPenalties.res < 0 )
        {
            unitPropia.ActiveBonusAndPenalties.res = 0;
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
        if (stat=="Atk") unitPropia.ActiveBonusAndPenalties.attk  = unitPropia.ActiveBonusAndPenalties.attk + this.cantidad;
        else if (stat == "Def") unitPropia.ActiveBonusAndPenalties.def = unitPropia.ActiveBonusAndPenalties.def + this.cantidad;
        else if (stat == "Res") unitPropia.ActiveBonusAndPenalties.res = unitPropia.ActiveBonusAndPenalties.res + this.cantidad;
        else if (stat == "Spd") unitPropia.ActiveBonusAndPenalties.spd = unitPropia.ActiveBonusAndPenalties.spd + this.cantidad;
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
        if (stat=="Atk") OponentsUnit.ActiveBonusAndPenalties.attk  = OponentsUnit.ActiveBonusAndPenalties.attk + this.cantidad;
        else if (stat == "Def") OponentsUnit.ActiveBonusAndPenalties.def = OponentsUnit.ActiveBonusAndPenalties.def + this.cantidad;
        else if (stat == "Res") OponentsUnit.ActiveBonusAndPenalties.res = OponentsUnit.ActiveBonusAndPenalties.res + this.cantidad;
        else if (stat == "Spd") OponentsUnit.ActiveBonusAndPenalties.spd = OponentsUnit.ActiveBonusAndPenalties.spd + this.cantidad;
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
        if (stat=="Atk" && OponentsUnit.ActiveBonusAndPenalties.attk > 0) OponentsUnit.ActiveBonusAndPenalties.attk  = 0;
        else if (stat == "Def" && OponentsUnit.ActiveBonusAndPenalties.attk > 0) OponentsUnit.ActiveBonusAndPenalties.def = 0;
        else if (stat == "Res" && OponentsUnit.ActiveBonusAndPenalties.attk > 0) OponentsUnit.ActiveBonusAndPenalties.res = 0;
        else if (stat == "Spd"&& OponentsUnit.ActiveBonusAndPenalties.attk > 0) OponentsUnit.ActiveBonusAndPenalties.spd = 0;
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
            unitPropia.ActiveBonusAndPenalties.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.def * this.percentaje));
            unitPropia.ActiveBonusAndPenalties.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.res * this.percentaje));
            unitPropia.ActiveBonusAndPenalties.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unitPropia.spd * this.percentaje));
            unitPropia.ActiveBonusAndPenalties.spd += cantidad;
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
            unitPropia.ActiveBonusAndPenalties.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            double division = unitPropia.def / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.ActiveBonusAndPenalties.def += cantidad;
        }
        else if (stat == "Res")
        {
            double division = unitPropia.res / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.ActiveBonusAndPenalties.res += cantidad;
        }
        else if (stat == "Spd")
        {
            double division = unitPropia.spd / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unitPropia.ActiveBonusAndPenalties.spd += cantidad;
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
        unitPropia.ActiveBonusAndPenalties.attk += cantidad;
        unitPropia.ActiveBonusAndPenalties.spd += cantidad;
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
        OponentsUnit.ActiveBonusAndPenalties.def += cantidadDef;
        OponentsUnit.ActiveBonusAndPenalties.res += cantidadRes;
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
        OponentsUnit.ActiveBonusAndPenalties.atkFollowup += cantidad;
        this.view.WriteLine(OponentsUnit.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}





