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

public class CambiarSpdEn : Efecto
{
    public CambiarSpdEn(View view, int cantidad) : base(view)
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

public class CambiarDefEn : Efecto
{
    public CambiarDefEn(View view, int cantidad) : base(view)
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

public class CambiarResEn : Efecto
{
    public CambiarResEn(View view, int cantidad) : base(view)
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

public class CambiarAtkEn : Efecto
{
    public CambiarAtkEn(View view, int cantidad) : base(view)
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
        this.view.WriteLine(unidadRival.nombre + " obtiene Atk+" + this.cantidad);
    }
}