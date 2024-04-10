using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Fire_Emblem;

// clase base, las otras heredaran de esta
public class Habilidades
{
    public Unidad unidadDueñaDeLaHabilidad;
    public virtual bool ChequearCondiciones(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        // si no hay condiciones se retorna true y no se sobreescribe este método
        return true;
    }

    public virtual void aplicar_cambios(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        //sobreescribir
        return;
    }
}

public class FairFight : Habilidades
{
    
    public virtual void aplicar_cambios(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        if (duenoHabilidadAtacando)
        {
            
        }
        return;
    }
}

public class Resolve : Habilidades
{
    public override bool ChequearCondiciones(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        // si no hay condiciones se retorna true y no se sobreescribe este método
        return true;
    }
    
    public virtual void aplicar_cambios(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        //sobreescribir
        return;
    }
}

public class SpeedMas5 : Habilidades
{
    public override bool ChequearCondiciones(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        // si no hay condiciones se retorna true y no se sobreescribe este método
        return true;
    }
    
    public virtual void aplicar_cambios(Unidad unidadAtacante, Unidad unidadDefensora, bool duenoHabilidadAtacando)
    {
        //sobreescribir
        return;
    }
}

