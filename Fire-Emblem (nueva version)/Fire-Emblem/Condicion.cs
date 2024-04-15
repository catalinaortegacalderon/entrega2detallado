namespace Fire_Emblem;

public class Condicion
{
    protected double valorMultiusoCondicion;
    public virtual bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return true;
    }
}

public class SiempreVerdad : Condicion
{
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        return true;
    }
}

public class HpPropioMenorAUnValor : Condicion
{
    public HpPropioMenorAUnValor(double cantidad) : base()
    {
        this.valorMultiusoCondicion = cantidad;
    }
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.hp_actual <= unidadPropia.hp_max * this.valorMultiusoCondicion)
        {
            return true;
        }
        return false;
    }
    
}

public class UnidadIniciaCombate : Condicion
{
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        Console.WriteLine("paso por el foco, imrpimiendo atacando");
        Console.WriteLine(atacando);
        if (atacando) return true;
        return false;
    }
    
}


