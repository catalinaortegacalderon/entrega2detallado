using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class Condicion
{
    // estos son parametros utilizadas por ciertas condiciones
    protected double valorMultiusoCondicion;
    // tal vez sacar variable anterior
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

public class TenerHpPropioMayorAlDelRivalAumentadoEn: Condicion
{
    private int valorAAumentar;
    public TenerHpPropioMayorAlDelRivalAumentadoEn(int valorAAumentar) : base()
    {
        this.valorAAumentar = valorAAumentar;
    }
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        if (unidadPropia.hp_actual >= unidadRival.hp_actual + this.valorAAumentar) return true;
        return false;
    }
    
}

// arreglar espaciado de aca abajo, tenia el cursor raro

public class AtaqueEntreArmasEspecificas : Condicion
{
    
    private string tipoDeArma1;
    private string tipoDeArma2;
        
    public AtaqueEntreArmasEspecificas(string tipoDeArma1, string tipoDeArma2) : base()
    {
        // REVISAR ESTOS NOMBRES
        this.tipoDeArma1 = tipoDeArma1;
        this.tipoDeArma2 = tipoDeArma2;
    }
        
    public override bool Verificar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
    {
        // ir agregando a medida que lo necesito
        
        if((tipoDeArma1 == "magia" && tipoDeArma2 == "fisica")|| (tipoDeArma2 == "magia" && tipoDeArma1 == "fisica"))
            {
                if (unidadPropia.arma == "magia" && (unidadRival.arma == "Bow" || unidadRival.arma == "Axe" || unidadRival.arma == "Sword" || unidadRival.arma == "Lance")){
                    return true;
                 }
                if (unidadRival.arma == "magia" && (unidadPropia.arma== "Bow" || unidadPropia.arma=="Axe" || unidadPropia.arma== "Sword" || unidadPropia.arma=="Lance")){
                    return true;
                }
            }
        return false;
    }
}




