using System.Net.Mail;
using System.Runtime.CompilerServices;
using Fire_Emblem_View;

namespace Fire_Emblem;

// clase base, las otras heredaran de esta
public abstract class Habilidad
{
    public View view;
    // si se cumple la condicion i se aplica el efecto i
    public Condicion[] condiciones;
    public Efecto[] efectos;

    public Habilidad(View view)
    {
        this.view = view;

    }
    public virtual bool ChequearCondiciones(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        // si no hay condiciones se retorna true y no se sobreescribe este método
        return true;
    }

    public virtual void aplicar_cambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        //sobreescribir
        return;
    }

    public virtual void AplicarHabilidades()
    {
        for (int i = 0; i < this.condiciones.Length; i++)
        {
            if (this.condiciones[i].Verificar())
            {
                this.efectos[i].Aplicar();
            }
        }
    }
}

public class HabilidadVacia : Habilidad
{
    public HabilidadVacia(View view) : base(view)
    {
    }

    public override bool ChequearCondiciones(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return false;
    }
    
    public virtual void aplicar_cambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}



public class FairFight : Habilidad
{
    public FairFight(View view) : base(view)
    {
    }
    
    public virtual void aplicar_cambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return;
    }
}

public class Resolve : Habilidad
{
    public Resolve(View view) : base(view)
    {
    }
    // Si el HP de la unidad est´a al 75% o menos al inicio del combate, otorga Def/Res+7
    public override bool ChequearCondiciones(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.hp_actual <= unidadPropia.hp_max * 0.75)
        {
            return true;
        }
        return false;
    }
    
    public virtual void aplicarCambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        unidadPropia.def = unidadPropia.def + 7;
        unidadPropia.res = unidadPropia.res + 7;
        this.view.WriteLine( unidadPropia.nombre+ " obtiene Def+7");
        this.view.WriteLine( unidadPropia.nombre+ " obtiene Res+7");
    }
}

public class SpeedMas5 : Habilidad
{
    public SpeedMas5(View view) : base(view)
    {
        Console.WriteLine("cree speed mas 5");
    }
    
    public virtual void aplicar_cambios(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        //sobreescribir
        unidadPropia.spd = unidadPropia.spd + 5;
        Console.WriteLine("paso por donde quiero");
        this.view.WriteLine( unidadPropia.nombre+ " obtiene Spd+5");

    }
}

