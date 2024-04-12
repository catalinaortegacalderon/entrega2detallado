namespace Fire_Emblem;
using Fire_Emblem_View;

public class Efecto
{
    public View view;

    public Efecto(View view)
    {
        this.view = view;
    }
    public virtual void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}

public class AumentarSpd : Efecto
{
    public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.spd = unidadPropia.BonusActivos.spd + 5;
        Console.WriteLine("paso por donde quiero");
        this.view.WriteLine( unidadPropia.nombre+ " obtiene Spd+5");
    }

    public virtual void aplicar_cambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        //sobreescribir
        unidadPropia.spd = unidadPropia.spd + 5;
        Console.WriteLine("paso por donde quiero");
        this.view.WriteLine( unidadPropia.nombre+ " obtiene Spd+5");

    }
    
}