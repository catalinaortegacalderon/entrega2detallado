using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class Condicion
{
    // estos son parametros utilizadas por ciertas condiciones
    protected double valorMultiusoCondicion;
    // tal vez sacar variable anterior
    protected string arma_usada;
    public virtual bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

public class SiempreVerdad : Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
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
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.hp_actual <= myUnit.hp_max * this.valorMultiusoCondicion)
        {
            return true;
        }
        return false;
    }
    
}

public class UnidadIniciaCombate : Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking) return true;
        return false;
    }
    
}

public class RivalIniciaCombate : Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking == false) return true;
        return false;
    }
    
}

public class UseCertainWeapon : Condicion
{
    public UseCertainWeapon(string arma) : base()
    {
        this.arma_usada = arma;
    }
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.arma == this.arma_usada) return true;
        return false;
    }
    
}

public class UseCertainWeaponAndStartCombat : Condicion
{
    public UseCertainWeaponAndStartCombat(string arma) : base()
    {
        this.arma_usada = arma;
    }
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.arma == this.arma_usada && iAmAttacking) return true;
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
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.hp_actual >= oponentsUnit.hp_actual + this.valorAAumentar) return true;
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
        
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        // ir agregando a medida que lo necesito
        
        Console.WriteLine("verificando");
        
        if(iAmAttacking && ((tipoDeArma1 == "magia" && tipoDeArma2 == "fisica")|| (tipoDeArma2 == "magia" && tipoDeArma1 == "fisica")))
            {
                Console.WriteLine("me meti al prifer if");
                Console.WriteLine(myUnit.arma);
                Console.WriteLine(oponentsUnit.arma);
                
                if (myUnit.arma == "Magic" && (oponentsUnit.arma == "Bow" || oponentsUnit.arma == "Axe" || oponentsUnit.arma == "Sword" || oponentsUnit.arma == "Lance")){
                    Console.WriteLine("true");
                    return true;
                 }
                if (oponentsUnit.arma == "Magic" && (myUnit.arma== "Bow" || myUnit.arma=="Axe" || myUnit.arma== "Sword" || myUnit.arma=="Lance")){
                    Console.WriteLine("true");
                    return true;
                }
            }
        Console.WriteLine("false");
        return false;
    }
}

public class OponentIsAMan: Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.genero == "Male") return true;
        return false;
    }
    
}

public class CurrentOponentIsAlsoTheLastOponent: Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.nombre == myUnit.gameLogs.LastOponentName) return true;
        return false;
    }
    
}

public class FirstAtack: Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.gameLogs.ataquesAcumulados == 0) return true;
        return false;
    }
    
}

public class StartCombatOrFullHP: Condicion
{
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.hp_actual == myUnit.hp_max || iAmAttacking) return true;
        return false;
    }
    
}

public class OponentStartsCombatWhithWeapon: Condicion
{
    private String[] weapons;
    public OponentStartsCombatWhithWeapon(String[] weapons) : base()
    {
        this.weapons = weapons;
    }
    public override bool Verify(Unidad myUnit, Unidad oponentsUnit, bool iAmAttacking)
    {
        if (!iAmAttacking && this.weapons.Contains(oponentsUnit.arma)) return true;
        return false;
    }
    
}




