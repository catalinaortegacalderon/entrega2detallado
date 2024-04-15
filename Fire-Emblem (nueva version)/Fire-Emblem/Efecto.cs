using System.Runtime.CompilerServices;

namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


public class Efecto
{
    protected int cantidad; // Este parámetro será accedido por algunas clases solamente, no repetir código
    protected View view; // Permitir acceso desde clases hijas

    public Efecto(View view)
    {
        this.view = view;
    }

    public virtual void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}

public class EfectoVacio : Efecto
{
    public EfectoVacio(View view) : base(view)
    {
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}

public class AumentarSpd : Efecto
{
    public AumentarSpd(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.spd = unidadPropia.BonusActivos.spd + this.cantidad;
        Console.WriteLine("Paso por donde quiero");
        this.view.WriteLine(unidadPropia.nombre + " obtiene Spd+" + this.cantidad);
    }
}

public class AumentarDef : Efecto
{
    public AumentarDef(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.BonusActivos.def = unidadPropia.BonusActivos.def + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Def+" + this.cantidad);
    }
}

public class AumentarRes : Efecto
{
    public AumentarRes(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.BonusActivos.res = unidadPropia.BonusActivos.res + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Res+" + this.cantidad);
    }
}

public class AumentarAtk : Efecto
{
    public AumentarAtk(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.BonusActivos.res = unidadPropia.BonusActivos.attk + this.cantidad;
        this.view.WriteLine(unidadPropia.nombre + " obtiene Attk+" + this.cantidad);
    }
}

public class AumentarAtkRival : Efecto
{
    public AumentarAtkRival(View view, int cantidad) : base(view)
    {
        this.cantidad = cantidad;
    }

    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadRival.BonusActivos.attk  = unidadRival.BonusActivos.attk + this.cantidad;
        // ver si se imprime aca
        this.view.WriteLine(unidadRival.nombre + " obtiene Attk+" + this.cantidad);
    }
}