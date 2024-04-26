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
        if (myUnit.CurrentHp <= myUnit.HpMax * this.cantidad)
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
        if (myUnit.Weapon == this.usedWeapon) return true;
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
        if (myUnit.Weapon == this.usedWeapon && iAmAttacking) return true;
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
        if (myUnit.CurrentHp >= oponentsUnit.CurrentHp + this.valorAAumentar) return true;
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
                if (myUnit.Weapon == "Magic" && (oponentsUnit.Weapon == "Bow" || oponentsUnit.Weapon == "Axe" || oponentsUnit.Weapon == "Sword" || oponentsUnit.Weapon == "Lance"))
                {
                    return true;
                }
                if (oponentsUnit.Weapon == "Magic" && (myUnit.Weapon== "Bow" || myUnit.Weapon=="Axe" || myUnit.Weapon== "Sword" || myUnit.Weapon=="Lance")){
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
        if (oponentsUnit.Gender == "Male") return true;
        return false;
    }
    
}

public class CurrentOponentIsAlsoTheLastOponent: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (oponentsUnit.Name == myUnit.GameLogs.LastOponentName) return true;
        return false;
    }
    
}

public class FirstAtack: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        if (myUnit.GameLogs.AmountOfAttacks == 0) return true;
        return false;
    }
    
}

public class RivalStartCombatOrFullHP: Condition
{
    public override bool Verify(Unit myUnit, Unit oponentsUnit, bool iAmAttacking)
    {
        Console.WriteLine("pase por verify de statrtcombat or full hp, belief in love");
        if (myUnit.CurrentHp == myUnit.HpMax || iAmAttacking == false) return true;
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
        if (!iAmAttacking && this.weapons.Contains(oponentsUnit.Weapon)) return true;
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
        if (oponentsUnit.CurrentHp >= oponentsUnit.HpMax * this.percentaje)
        {
            return true;
        }
        return false;
    }
}




