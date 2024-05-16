using System.Runtime.CompilerServices;

namespace Fire_Emblem;

// cambiar rival por oponent, ser consistente
//hay oponent y opponent con dos p

public class Condition
{
    protected int Priority;
    
    public Condition() : base()
    {
        this.Priority = 1;
    }
    public virtual bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        return true;
    }

    public int GetPriority()
    {
        return this.Priority;
    }
    
    public void ChangePriorityBecauseOfSecondCategoryEffect(int priority)
    {
        this.Priority = priority;
    }
}

public class AlwaysTrue : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        return true;
    }
}

public class OwnHpLessThan : Condition
{
    private double _amount;
    public OwnHpLessThan(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (myUnit.CurrentHp <= myUnit.HpMax * this._amount)
        {
            return true;
        }
        return false;
    }
}

public class OwnHpBiggerThan : Condition
{
    private double _amount;
    public OwnHpBiggerThan(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (Math.Round((double)myUnit.CurrentHp / myUnit.HpMax,2) >= this._amount)
        {
            return true;
        }
        return false;
    }
}

public class UnitStartsCombat : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking) return true;
        return false;
    }
}

public class OpponentStartsCombat : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (iAmAttacking == false) return true;
        return false;
    }
}

public class UseCertainWeapon : Condition
{
    private string _usedWeapon;
    public UseCertainWeapon(string weapon) : base()
    {
        this._usedWeapon = weapon;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (myUnit.Weapon == this._usedWeapon) return true;
        return false;
    }
}

public class UseCertainWeaponAndStartCombat : Condition
{
    private string[] _weapons;
    public UseCertainWeaponAndStartCombat(string[] weapon) : base()
    {
        this._weapons = weapon;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (this._weapons.Contains(myUnit.Weapon) && iAmAttacking) return true;
        return false;
    }
}

public class MyHpIsLessThanRivalsHpPlus: Condition
{
    private int _increaseAmountIn;
    public MyHpIsLessThanRivalsHpPlus(int increaseAmountIn) : base()
    {
        this._increaseAmountIn = increaseAmountIn;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (myUnit.CurrentHp >= opponentsUnit.CurrentHp + this._increaseAmountIn) return true;
        return false;
    }
}

public class AttackBetweenSpecificWeapons : Condition
{
    
    private string _firstWeaponType;
    private string _secondWeaponType;
        
    public AttackBetweenSpecificWeapons(string firstWeaponType, string secondWeaponType) : base()
    {
        this._firstWeaponType = firstWeaponType;
        this._secondWeaponType = secondWeaponType;
    }
        
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        
        if( iAmAttacking && ((_firstWeaponType == "magia" && _secondWeaponType == "fisica")|| (_secondWeaponType == "magia" && _firstWeaponType == "fisica"))) 
        { 
                if (myUnit.Weapon == "Magic" && (opponentsUnit.Weapon == "Bow" || opponentsUnit.Weapon == "Axe" || opponentsUnit.Weapon == "Sword" || opponentsUnit.Weapon == "Lance"))
                {
                    return true;
                }
                if (opponentsUnit.Weapon == "Magic" && (myUnit.Weapon== "Bow" || myUnit.Weapon=="Axe" || myUnit.Weapon== "Sword" || myUnit.Weapon=="Lance")){
                    return true;
                }
        }
        return false;
    }
}

public class OponentIsAMan: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (opponentsUnit.Gender == "Male") return true;
        return false;
    }
    
}

public class CurrentOponentIsAlsoTheLastOponent: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (opponentsUnit.Name == myUnit.GameLogs.LastOpponentName) return true;
        return false;
    }
    
}

public class RivalHasFullHP: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (opponentsUnit.CurrentHp == opponentsUnit.HpMax) return true;
        return false;
    }
    
}

public class OpponentStartsCombatWithCertainWeapon: Condition
{
    private String[] _weapons;
    public OpponentStartsCombatWithCertainWeapon(String[] weapons) : base()
    {
        this._weapons = weapons;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (!iAmAttacking && this._weapons.Contains(opponentsUnit.Weapon)) return true;
        return false;
    }
    
}

public class OpponentUsesCertainWeapon: Condition
{
    private String[] _weapons;
    public OpponentUsesCertainWeapon(String[] weapons) : base()
    {
        this._weapons = weapons;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        Console.WriteLine("paso por el verify");
        if (this._weapons.Contains(opponentsUnit.Weapon)) return true;
        Console.WriteLine("false");
        return false;
    }
}

public class RivalStartsAttackOrHasHpGreaterThan : Condition
{
    private double _percentage;
    public RivalStartsAttackOrHasHpGreaterThan(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (opponentsUnit.CurrentHp >= opponentsUnit.HpMax * this._percentage || !iAmAttacking)
        {
            return true;
        }
        return false;
    }
}