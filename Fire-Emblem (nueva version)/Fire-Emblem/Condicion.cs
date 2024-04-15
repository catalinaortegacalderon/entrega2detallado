namespace Fire_Emblem;

public class Condicion
{
    // estos son parametros utilizadas por ciertas condiciones
    protected double valorMultiusoCondicion;
    protected string arma_usada;
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

public class UsarCiertaArma : Condicion
{
    public UsarCiertaArma(string arma) : base()
    {
        this.arma_usada = arma;
    }
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.arma == this.arma_usada) return true;
        return false;
    }
    
}

public class UsarCiertaArmaEIniciarCombate : Condicion
{
    public UsarCiertaArmaEIniciarCombate(string arma) : base()
    {
        this.arma_usada = arma;
    }
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.arma == this.arma_usada && atacando) return true;
        return false;
    }
    
}


