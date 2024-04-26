using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class Condition
{
    protected string usedWeapon;
    public virtual bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

public class SiempreVerdad : Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

public class HpPropioMenorAUnValor : Condition
{
    private double cantidad;
    public HpPropioMenorAUnValor(double cantidad) : base()
    {
        this.cantidad = cantidad;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.currentHp <= myUnit.hpMax * this.cantidad)
        {
            return true;
        }
        return false;
    }
    
}

public class UnidadIniciaCombate : Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking) return true;
        return false;
    }
}

public class RivalIniciaCombate : Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking == false) return true;
        return false;
    }
}

public class UseCertainWeapon : Condition
{
    public UseCertainWeapon(string arma) : base()
    {
        this.usedWeapon = arma;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.weapon == this.usedWeapon) return true;
        return false;
    }
}

public class UseCertainWeaponAndStartCombat : Condition
{
    public UseCertainWeaponAndStartCombat(string arma) : base()
    {
        this.usedWeapon = arma;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.weapon == this.usedWeapon && iAmAttacking) return true;
        return false;
    }
}

public class TenerHpPropioMayorAlDelRivalAumentadoEn: Condition
{
    private int valorAAumentar;
    public TenerHpPropioMayorAlDelRivalAumentadoEn(int valorAAumentar) : base()
    {
        this.valorAAumentar = valorAAumentar;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.currentHp >= oponentsUnit.currentHp + this.valorAAumentar) return true;
        return false;
    }
}

public class AtaqueEntreArmasEspecificas : Condition
{
    
    private string tipoDeArma1;
    private string tipoDeArma2;
        
    public AtaqueEntreArmasEspecificas(string tipoDeArma1, string tipoDeArma2) : base()
    {
        this.tipoDeArma1 = tipoDeArma1;
        this.tipoDeArma2 = tipoDeArma2;
    }
        
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        
        if( iAmAttacking && ((tipoDeArma1 == "magia" && tipoDeArma2 == "fisica")|| (tipoDeArma2 == "magia" && tipoDeArma1 == "fisica"))) 
        { 
                if (myUnit.weapon == "Magic" && (oponentsUnit.weapon == "Bow" || oponentsUnit.weapon == "Axe" || oponentsUnit.weapon == "Sword" || oponentsUnit.weapon == "Lance"))
                {
                    return true;
                }
                if (oponentsUnit.weapon == "Magic" && (myUnit.weapon== "Bow" || myUnit.weapon=="Axe" || myUnit.weapon== "Sword" || myUnit.weapon=="Lance")){
                    return true;
                }
        }
        return false;
    }
}

public class OponentIsAMan: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.gender == "Male") return true;
        return false;
    }
    
}

public class CurrentOponentIsAlsoTheLastOponent: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.name == myUnit.gameLogs.LastOponentName) return true;
        return false;
    }
    
}

public class FirstAtack: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.gameLogs.AmountOfAttacks == 0) return true;
        return false;
    }
    
}

public class RivalStartCombatOrFullHP: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        Console.WriteLine("pase por verify de statrtcombat or full hp, belief in love");
        if (myUnit.currentHp == myUnit.hpMax || iAmAttacking == false) return true;
        Console.WriteLine("falso");
        return false;
    }
    
}

public class OponentStartsCombatWhithWeapon: Condition
{
    private String[] weapons;
    public OponentStartsCombatWhithWeapon(String[] weapons) : base()
    {
        this.weapons = weapons;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (!iAmAttacking && this.weapons.Contains(oponentsUnit.weapon)) return true;
        return false;
    }
    
}

public class RivalStartsAttackOrHasHpGreaterThan : Condition
{
    private double percentaje;
    public RivalStartsAttackOrHasHpGreaterThan(double percentage) : base()
    {
        this.percentaje = percentage;
    }
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.currentHp >= oponentsUnit.hpMax * this.percentaje)
        {
            return true;
        }
        return false;
    }
}




