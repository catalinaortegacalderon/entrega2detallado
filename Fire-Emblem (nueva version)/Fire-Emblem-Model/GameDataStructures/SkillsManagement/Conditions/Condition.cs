using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;

public class Condition
{
    protected int Priority;
    
    public Condition() : base()
    {
        this.Priority = 1;
    }
    public virtual bool Verify(Unit myUnit, Unit opponentsUnit)
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

public class AlwaysTrueCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        return true;
    }
}

public class OwnHpLessThanCondition : Condition
{
    private double _amount;
    public OwnHpLessThanCondition(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.CurrentHp <= myUnit.HpMax * this._amount)
        {
            return true;
        }
        return false;
    }
}

public class OwnHpBiggerThanCondition : Condition
{
    private double _amount;
    public OwnHpBiggerThanCondition(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (Math.Round((double)myUnit.CurrentHp / myUnit.HpMax,2) >= this._amount)
        {
            return true;
        }
        return false;
    }
}

public class UnitStartsCombatCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.IsAttacking) return true;
        return false;
    }
}

public class OpponentStartsCombatCondition : Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.IsAttacking == false) return true;
        return false;
    }
}

public class UseCertainWeaponsCondition : Condition
{
    private string[] _usedWeapon;
    public UseCertainWeaponsCondition(string[] weapon) : base()
    {
        this._usedWeapon = weapon;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (this._usedWeapon.Contains(myUnit.Weapon)) return true;
        return false;
    }
}

public class MyHpIsLessThanOpponentsHpPlusCondition: Condition
{
    private int _increaseAmountIn;
    public MyHpIsLessThanOpponentsHpPlusCondition(int increaseAmountIn) : base()
    {
        this._increaseAmountIn = increaseAmountIn;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.CurrentHp >= opponentsUnit.CurrentHp + this._increaseAmountIn) return true;
        return false;
    }
}

public class AttackBetweenSpecificWeaponsCondition : Condition
{
    
    private string _firstWeaponType;
    private string _secondWeaponType;
        
    public AttackBetweenSpecificWeaponsCondition(string firstWeaponType, string secondWeaponType) : base()
    {
        this._firstWeaponType = firstWeaponType;
        this._secondWeaponType = secondWeaponType;
    }
        
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        
        if( myUnit.IsAttacking && ((_firstWeaponType == "magia" && _secondWeaponType == "fisica")|| (_secondWeaponType == "magia" && _firstWeaponType == "fisica"))) 
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

public class OpponentIsAManCondition: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.Gender == "Male") return true;
        return false;
    }
    
}

public class CurrentOpponentIsAlsoTheLastOpponentCondition: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.Name == myUnit.GameLogs.LastOpponentName) return true;
        return false;
    }
    
}

public class OpponentHasFullHPCondition: Condition
{
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
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
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (!myUnit.IsAttacking && this._weapons.Contains(opponentsUnit.Weapon)) return true;
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
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        Console.WriteLine("paso por el verify");
        if (this._weapons.Contains(opponentsUnit.Weapon)) return true;
        Console.WriteLine("false");
        return false;
    }
}

public class OpponentStartsAttackOrHasHpGreaterThan : Condition
{
    private double _percentage;
    public OpponentStartsAttackOrHasHpGreaterThan(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.CurrentHp >= opponentsUnit.HpMax * this._percentage || !myUnit.IsAttacking)
        {
            return true;
        }
        return false;
    }
}