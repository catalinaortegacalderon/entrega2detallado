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

    public virtual void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}

public class EmptyEffect : Effect
{
    public EmptyEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
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

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.hp_actual = unidadPropia.hp_actual + this.cantidad;
    }
}

public class ChangeSpdIn : Effect
{
    public ChangeSpdIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.BonusActivos.spd = unidadPropia.BonusActivos.spd + this.cantidad;
        Console.WriteLine("Paso por donde quiero");
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(unidadPropia.nombre + " obtiene Spd" + signo + this.cantidad);
    }
}

public class ChangeDefIn : Effect
{
    public ChangeDefIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unidadPropia.BonusActivos.def = unidadPropia.BonusActivos.def + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Def" + signo + this.cantidad);
    }
}

public class ChangeResIn : Effect
{
    public ChangeResIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unidadPropia.BonusActivos.res = unidadPropia.BonusActivos.res + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Res" + signo + this.cantidad);
    }
}

public class ChangeAtkIn : Effect
{
    public ChangeAtkIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        string signo = (this.cantidad > 0) ? "+" :  "";
        unidadPropia.BonusActivos.attk = unidadPropia.BonusActivos.attk + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}

public class ChangeRivalsAtkIn : Effect
{
    public ChangeRivalsAtkIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadRival.BonusActivos.attk  = unidadRival.BonusActivos.attk + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}

public class ChangeRivalsSpdIn : Effect
{
    public ChangeRivalsSpdIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadRival.BonusActivos.spd  = unidadRival.BonusActivos.spd + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Spd" + signo + this.cantidad);
    }
}

public class ChangeRivalsDefIn : Effect
{
    public ChangeRivalsDefIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadRival.BonusActivos.def  = unidadRival.BonusActivos.def + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Def" + signo + this.cantidad);
    }
}

public class ReduceRivalsSpdInPercentaje : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsSpdInPercentaje(View view, double reduction) : base(view)
    {
        this.reductionPercentaje = reduction;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int reduction = Convert.ToInt32(Math.Truncate(unidadRival.spd * 0.5));
        unidadRival.BonusActivos.spd  = unidadRival.BonusActivos.spd - reduction;
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Spd-" + reduction);
    }
}


public class ReduceRivalsDefInPercentaje : Effect
{
    private double reductionPercentaje;
    public ReduceRivalsDefInPercentaje(View view, double reduction) : base(view)
    {
        this.reductionPercentaje = reduction;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int reduction = Convert.ToInt32(Math.Truncate(unidadRival.spd * 0.5));
        unidadRival.BonusActivos.spd  = unidadRival.BonusActivos.spd - reduction;
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Def-" + reduction);
    }
}


public class NeutralizeOponentsBonus : Effect
{
    
    public NeutralizeOponentsBonus(View view) : base(view)
    {
    }
    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadRival.BonusActivos.attk > 0 )
        {
            unidadRival.BonusActivos.attk = 0;
        }
        if (unidadRival.BonusActivos.spd > 0 )
        {
            unidadRival.BonusActivos.spd = 0;
        }
        if (unidadRival.BonusActivos.def > 0 )
        {
            unidadRival.BonusActivos.def = 0;
        }
        if (unidadRival.BonusActivos.res > 0 )
        {
            unidadRival.BonusActivos.res = 0;
        }
        this.view.WriteLine("Los bonus de Atk de " + unidadRival.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Spd de " + unidadRival.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Def de " + unidadRival.nombre + " fueron neutralizados");
        this.view.WriteLine("Los bonus de Res de " + unidadRival.nombre + " fueron neutralizados");
    }
}

// TAL VEZ HACER UNO DE BONUS Y UNO DE PENALTIES PARA QUE NO SE MEZCLEN
public class NeutralizePenalties : Effect
{
    
    public NeutralizePenalties(View view) : base(view)
    {
    }
    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.BonusActivos.attk < 0 )
        {
            unidadPropia.BonusActivos.attk = 0;
        }
        if (unidadPropia.BonusActivos.spd < 0 )
        {
            unidadPropia.BonusActivos.spd = 0;
        }
        if (unidadPropia.BonusActivos.def < 0 )
        {
            unidadPropia.BonusActivos.def = 0;
        }
        if (unidadPropia.BonusActivos.res < 0 )
        {
            unidadPropia.BonusActivos.res = 0;
        }
        this.view.WriteLine("Los penalties de Atk de " + unidadPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Spd de " + unidadPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Def de " + unidadPropia.nombre + " fueron neutralizados");
        this.view.WriteLine("Los penalties de Res de " + unidadPropia.nombre + " fueron neutralizados");
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

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (stat=="Atk") unidadPropia.BonusActivos.attk  = unidadPropia.BonusActivos.attk + this.cantidad;
        else if (stat == "Def") unidadPropia.BonusActivos.def = unidadPropia.BonusActivos.def + this.cantidad;
        else if (stat == "Res") unidadPropia.BonusActivos.res = unidadPropia.BonusActivos.res + this.cantidad;
        else if (stat == "Spd") unidadPropia.BonusActivos.spd = unidadPropia.BonusActivos.spd + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(unidadPropia.nombre + " obtiene " + stat + signo + this.cantidad);
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

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (stat=="Atk") unidadRival.BonusActivos.attk  = unidadRival.BonusActivos.attk + this.cantidad;
        else if (stat == "Def") unidadRival.BonusActivos.def = unidadRival.BonusActivos.def + this.cantidad;
        else if (stat == "Res") unidadRival.BonusActivos.res = unidadRival.BonusActivos.res + this.cantidad;
        else if (stat == "Spd") unidadRival.BonusActivos.spd = unidadRival.BonusActivos.spd + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine(unidadRival.nombre + " obtiene " + stat + signo + this.cantidad);
    }
}

public class NeutralizeOneOfOponentsBonus : Effect
{
    private String stat;
    public NeutralizeOneOfOponentsBonus(View view, String stat) : base(view)
    {
        this.stat = stat;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (stat=="Atk" && unidadRival.BonusActivos.attk > 0) unidadRival.BonusActivos.attk  = 0;
        else if (stat == "Def" && unidadRival.BonusActivos.attk > 0) unidadRival.BonusActivos.def = 0;
        else if (stat == "Res" && unidadRival.BonusActivos.attk > 0) unidadRival.BonusActivos.res = 0;
        else if (stat == "Spd"&& unidadRival.BonusActivos.attk > 0) unidadRival.BonusActivos.spd = 0;
        string signo = (this.cantidad > 0) ? "+" :  "";
        this.view.WriteLine("Los bonus de " + this.stat + " de " + unidadRival.nombre + " fueron neutralizados");
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

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unidadPropia.attk * this.percentaje));
            unidadPropia.BonusActivos.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unidadPropia.def * this.percentaje));
            unidadPropia.BonusActivos.def += cantidad;
        }
        else if (stat == "Res")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unidadPropia.res * this.percentaje));
            unidadPropia.BonusActivos.res += cantidad;
        }
        else if (stat == "Spd")
        {
            cantidad = Convert.ToInt32(Math.Truncate(unidadPropia.spd * this.percentaje));
            unidadPropia.BonusActivos.spd += cantidad;
        }
        string signo = (this.percentaje > 0) ? "+" :  "";
        this.view.WriteLine(unidadPropia.nombre + " obtiene " + stat + signo + cantidad);
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

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int cantidad = 0;
        if (stat == "Atk")
        {
            double division = unidadPropia.attk / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unidadPropia.BonusActivos.attk  += cantidad;
            
        }
        else if (stat == "Def")
        {
            double division = unidadPropia.def / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unidadPropia.BonusActivos.def += cantidad;
        }
        else if (stat == "Res")
        {
            double division = unidadPropia.res / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unidadPropia.BonusActivos.res += cantidad;
        }
        else if (stat == "Spd")
        {
            double division = unidadPropia.spd / amount;
            cantidad = Convert.ToInt32(Math.Truncate(division));
            unidadPropia.BonusActivos.spd += cantidad;
        }
        this.view.WriteLine(unidadPropia.nombre + " obtiene " + stat + "+" + cantidad);
    }
}

public class WrathEffect : Effect
{
    public WrathEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int cantidad = unidadPropia.hp_max - unidadPropia.hp_actual;
        if (cantidad > 30) cantidad = 30;
        unidadPropia.BonusActivos.attk += cantidad;
        unidadPropia.BonusActivos.spd += cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Atk+" + this.cantidad);
        this.view.WriteLine(unidadPropia.nombre + " obtiene Spd+"+ this.cantidad);
        
    }
}

public class SoulbladeEffect : Effect
{
    public SoulbladeEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        double promedioDefResDouble = (unidadRival.def + unidadRival.res) / 2;
        int promedioDefRes = Convert.ToInt32(Math.Truncate(promedioDefResDouble));
        int cantidadDef = promedioDefRes - unidadRival.def;
        int cantidadRes = promedioDefRes - unidadRival.res;
        string signoDef = (cantidadDef > 0) ? "+" :  "";
        string signoRes = (cantidadRes > 0) ? "+" :  "";
        unidadRival.BonusActivos.def += cantidadDef;
        unidadRival.BonusActivos.res += cantidadRes;
        this.view.WriteLine(unidadRival.nombre + " obtiene Def" + signoDef + this.cantidad);
        this.view.WriteLine(unidadRival.nombre + " obtiene Res"+ signoRes + this.cantidad);
        
    }
}

public class SandstormEffect : Effect
{
    public SandstormEffect(View view) : base(view)
    {
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        int cantidad = Convert.ToInt32(Math.Truncate(1.5 * unidadPropia.def - unidadPropia.attk));
        string signo = (cantidad > 0) ? "+" :  "";
        unidadRival.BonusActivos.atkFollowup += cantidad;
        this.view.WriteLine(unidadRival.nombre + " obtiene Atk" + signo + this.cantidad);
    }
}





