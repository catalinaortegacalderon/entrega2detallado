namespace Fire_Emblem;

public class Condicion
{
    public virtual bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return true;
    }
}

public class SiempreVerdad : Condicion
{
    public virtual bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return true;
    }
}

public class HpPropioMenorAUnValor : Condicion
{
    
    
}


