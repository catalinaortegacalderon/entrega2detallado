using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1;

public class AttackCalculator
{
    private readonly Unit _currentAttackingUnit;
    private readonly Unit _currentDefensiveUnit;
    private readonly AttackType _typeOfThisRoundsCurrentAttack;
    // PONER WTB

    public AttackCalculator(Unit attackingUnit, Unit defensiveUnit, AttackType attackType)
    {
        this._currentAttackingUnit = attackingUnit;
        this._currentDefensiveUnit = defensiveUnit;
        this._typeOfThisRoundsCurrentAttack = attackType;
    }
    
    public int CalculateAttack()
    {
        var initialDamage = CalculateInitialDamage();
        Console.WriteLine("initial damage");
        Console.WriteLine(initialDamage);
        double finalDamage = CalculateFinalDamage(initialDamage);
        finalDamage = Convert.ToInt32(Math.Floor(finalDamage));
        // ACA
        Console.WriteLine("final damage");
        Console.WriteLine(finalDamage);
        
        
        if ((finalDamage) < 0) 
            return 0;
        return Convert.ToInt32(Math.Truncate(finalDamage));
    }
    
    public int CalculateAttackForDivineRecreation()
    {
        var initialDamage = CalculateInitialDamage();
        double finalDamage = CalculateFinalDamageForDivineRecreation(initialDamage);
        if ((finalDamage) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(finalDamage));
    }
    
    private int CalculateInitialDamage()
    {
        int rivalsDefOrRes = CalculateOpponentsDefOrRes();
        double wtb = CalculateWtb();
        int unitsAtk = CalculateUnitsAtk();
        //TRUNCAR TODO PARA ABAJO
        var initialDamage = Convert.ToInt32(Math.Floor(unitsAtk * wtb - rivalsDefOrRes));
        Console.WriteLine("calculando initial damage dentro de calculadora");
        Console.WriteLine("def or res" + rivalsDefOrRes);
        Console.WriteLine("wtb" + wtb);
        Console.WriteLine("units atack" + unitsAtk);
        return initialDamage;
    }

    private int CalculateUnitsAtk()
    {
        int unitsAtk = _currentAttackingUnit.Atk + _currentAttackingUnit.ActiveBonus.Attk 
            * _currentAttackingUnit.ActiveBonusNeutralizator.Attk 
            + _currentAttackingUnit.ActivePenalties.Attk 
            * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        
        if (_typeOfThisRoundsCurrentAttack is AttackType.FirstAttack or AttackType.SecondAttack)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFirstAttack 
                        * _currentAttackingUnit.ActiveBonusNeutralizator.Attk 
                        + _currentAttackingUnit.ActivePenalties.AtkFirstAttack 
                        * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        }
        if (_typeOfThisRoundsCurrentAttack == AttackType.FollowUp)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFollowup 
                        * _currentAttackingUnit.ActiveBonusNeutralizator.Attk
                        + _currentAttackingUnit.ActivePenalties.AtkFollowup 
                        * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        }
        return unitsAtk;
    }

    private double CalculateWtb()
    {
        // todo: wtb 
        double wtb;
        if (ThereIsNoAdvantage()) wtb = 1;
        else if (AttackerHasAdvantage()) wtb = 1.2;
        else
        {
            wtb = 0.8;
        }
        return wtb;
    }

    private int CalculateOpponentsDefOrRes()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        
        int rivalsDefOrRes;
        if (attackingWeapon == Weapon.Magic)
        {
            rivalsDefOrRes = _currentDefensiveUnit.Res + _currentDefensiveUnit.ActiveBonus.Res 
                * _currentDefensiveUnit.ActiveBonusNeutralizator.Res 
                + _currentDefensiveUnit.ActivePenalties.Res 
                *_currentDefensiveUnit.ActivePenaltiesNeutralizator.Res;
            if (_typeOfThisRoundsCurrentAttack is AttackType.FirstAttack or AttackType.SecondAttack)
            {
                rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.ResFirstAttack 
                    * _currentDefensiveUnit.ActiveBonusNeutralizator.Res 
                    + _currentDefensiveUnit.ActivePenalties.ResFirstAttack 
                    *_currentDefensiveUnit.ActivePenaltiesNeutralizator.Res;
            }
        }
        else
        {
            rivalsDefOrRes = _currentDefensiveUnit.Def + _currentDefensiveUnit.ActiveBonus.Def 
                * _currentDefensiveUnit.ActiveBonusNeutralizator.Def
                + _currentDefensiveUnit.ActivePenalties.Def 
                *_currentDefensiveUnit.ActivePenaltiesNeutralizator.Def;
            if (_typeOfThisRoundsCurrentAttack is AttackType.FirstAttack or AttackType.SecondAttack)
            {
                rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.DefFirstAttack 
                    * _currentDefensiveUnit.ActiveBonusNeutralizator.Def 
                    + _currentDefensiveUnit.ActivePenalties.DefFirstAttack 
                    *_currentDefensiveUnit.ActivePenaltiesNeutralizator.Def;
            }
        }
        
        return rivalsDefOrRes;
    }
    
    private int CalculateFinalDamage(double initialDamage)
    {
        double finalDamage  = initialDamage;
        if (_typeOfThisRoundsCurrentAttack == AttackType.FirstAttack)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage 
                               + _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack) 
                * _currentDefensiveUnit.DamageEffects.PercentageReduction 
                *  _currentDefensiveUnit.DamageEffects.PercentageReductionOpponentsFirstAttack 
                + _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
            
        }
        else if (_typeOfThisRoundsCurrentAttack == AttackType.SecondAttack)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage 
                               + _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack) 
                * _currentDefensiveUnit.DamageEffects.PercentageReduction 
                *  _currentDefensiveUnit.DamageEffects.PercentageReductionOpponentsFirstAttack 
                + _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
            
        }
        else if (_typeOfThisRoundsCurrentAttack == AttackType.FollowUp)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage 
                               + _currentAttackingUnit.DamageEffects.ExtraDamageFollowup) 
                * _currentDefensiveUnit.DamageEffects.PercentageReduction 
                * _currentDefensiveUnit.DamageEffects.PercentageReductionOpponentsFollowup 
                + _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
        }
        var newDamage = Math . Round ( finalDamage , 9) ; // redondeamos al noveno decimal
        var damage = Convert . ToInt32 ( Math . Floor ( newDamage ) ); // trun
        return damage;
    }
    
    private int CalculateFinalDamageForDivineRecreation(double initialDamage)
    {
        
        // todo: ES SIN LA ABSOLUTA NI PORCENTUAL     si extra
        double finalDamage  = initialDamage;
        if (_typeOfThisRoundsCurrentAttack == AttackType.FirstAttack)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage +
                 _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack);

        }
        else if (_typeOfThisRoundsCurrentAttack == AttackType.SecondAttack)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage +
                 _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack);

        }
        else if (_typeOfThisRoundsCurrentAttack == AttackType.FollowUp)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage +
                 _currentAttackingUnit.DamageEffects.ExtraDamageFollowup);

        }
        var newDamage = Math . Round ( finalDamage , 9) ; // redondeamos al noveno decimal
        var damage = Convert . ToInt32 ( Math . Floor ( newDamage ) ); // trun
        return damage;
    }
    
    // todo: ESTOS DOS METODOS LOS REPITO EN GAMES ATTACK CONTROLLER
    
    private bool AttackerHasAdvantage()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        return (attackingWeapon == Weapon.Sword & defensiveWeapon == Weapon.Axe) || 
               (attackingWeapon == Weapon.Lance & defensiveWeapon == Weapon.Sword) || 
               (attackingWeapon == Weapon.Axe & defensiveWeapon == Weapon.Lance);
    }

    private bool ThereIsNoAdvantage()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        return defensiveWeapon == attackingWeapon 
               || attackingWeapon == Weapon.Magic 
               || defensiveWeapon == Weapon.Magic 
               || defensiveWeapon == Weapon.Bow 
               || attackingWeapon == Weapon.Bow;
    }

}