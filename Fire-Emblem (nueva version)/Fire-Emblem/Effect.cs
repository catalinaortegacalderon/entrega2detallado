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

public class ChangeSpdIn : Effect
{
    public ChangeSpdIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.spd = unidadPropia.BonusActivos.spd + this.cantidad;
        Console.WriteLine("Paso por donde quiero");
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
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
        string signo = (this.cantidad > 0) ? "+" :  "-";
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Def" + signo + this.cantidad);
    }
}

public class ChangeRivalsResIn : Effect
{
    public ChangeRivalsResIn(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadRival.BonusActivos.res  = unidadRival.BonusActivos.res + this.cantidad;
        string signo = (this.cantidad > 0) ? "+" :  "-";
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Res" + signo + this.cantidad);
    }
}