using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1;

public class DamageCalculator
{
    private readonly Unit _currentAttackingUnit;
    private readonly Unit _currentDefensiveUnit;
    private readonly AttackType _typeOfThisRoundsCurrentAttack;
    private readonly double _wtbValueForNoAdvantage = 1;
    private readonly double _wtbValueForAttackersAdvantage = 1.2;
    private readonly double _wtbValueForDefensorsAdvantage = 0.8;

    public DamageCalculator(Unit attackingUnit, Unit defensiveUnit, AttackType attackType)
    {
        this._currentAttackingUnit = attackingUnit;
        this._currentDefensiveUnit = defensiveUnit;
        this._typeOfThisRoundsCurrentAttack = attackType;
    }
    
    public int CalculateAttack()
    {
        var initialDamage = CalculateInitialDamage();
        double finalDamage = CalculateFinalDamage(initialDamage);
        finalDamage = Convert.ToInt32(Math.Floor(finalDamage));
        
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

        var initialDamage = Convert.ToInt32(Math.Floor(unitsAtk * wtb - rivalsDefOrRes));
        if (initialDamage < 0)
            initialDamage = 0;
        return initialDamage;
    }

    private int CalculateUnitsAtk()
    {
        int unitsAtk = _currentAttackingUnit.Atk + _currentAttackingUnit.ActiveBonus.Attk 
            * _currentAttackingUnit.ActiveBonusNeutralizator.Attk 
            + _currentAttackingUnit.ActivePenalties.Attk 
            * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        
        if (IsFirstOrSecondAttack())
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFirstAttack 
                        * _currentAttackingUnit.ActiveBonusNeutralizator.Attk 
                        + _currentAttackingUnit.ActivePenalties.AtkFirstAttack 
                        * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        }
        if (IsFollowUp())
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFollowup 
                        * _currentAttackingUnit.ActiveBonusNeutralizator.Attk
                        + _currentAttackingUnit.ActivePenalties.AtkFollowup 
                        * _currentAttackingUnit.ActivePenaltiesNeutralizator.Attk;
        }
        return unitsAtk;
    }

    private bool IsFollowUp()
    {
        return _typeOfThisRoundsCurrentAttack == AttackType.FollowUp;
    }

    private bool IsFirstOrSecondAttack()
    {
        return _typeOfThisRoundsCurrentAttack is AttackType.FirstAttack or AttackType.SecondAttack;
    }

    private double CalculateWtb()
    {
        double wtb;
        if (IsNoAdvantage(_currentAttackingUnit.Weapon, _currentDefensiveUnit.Weapon)) 
            wtb = _wtbValueForNoAdvantage;
        else if (DoesAttackerHaveAdvantage( _currentAttackingUnit.Weapon, _currentDefensiveUnit.Weapon)) 
            wtb = _wtbValueForAttackersAdvantage;
        else
        {
            wtb = _wtbValueForDefensorsAdvantage;
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
            
            if (IsFirstOrSecondAttack())
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
                * _currentDefensiveUnit.ActivePenaltiesNeutralizator.Def;
            
            if (IsFirstOrSecondAttack())
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
        // todo: arreglar codigo duplicado
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
        var newDamage = Math . Round ( finalDamage , 9) ; 
        var damage = Convert . ToInt32 ( Math . Floor ( newDamage ) ); 
        return damage;
    }
    
    private int CalculateFinalDamageForDivineRecreation(double initialDamage)
    {
        double finalDamage  = initialDamage;
        // todo: codigo duplicado
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
        var newDamage = Math . Round ( finalDamage , 9) ; 
        var damage = Convert . ToInt32 ( Math . Floor ( newDamage ) ); 
        return damage;
    }
    
    public static bool DoesAttackerHaveAdvantage(Weapon attackingWeapon, Weapon defensiveWeapon)
    {
        return (attackingWeapon == Weapon.Sword & defensiveWeapon == Weapon.Axe) || 
               (attackingWeapon == Weapon.Lance & defensiveWeapon == Weapon.Sword) || 
               (attackingWeapon == Weapon.Axe & defensiveWeapon == Weapon.Lance);
    }

    public static bool IsNoAdvantage(Weapon attackingWeapon, Weapon defensiveWeapon)
    {
        return defensiveWeapon == attackingWeapon 
               || attackingWeapon == Weapon.Magic 
               || defensiveWeapon == Weapon.Magic 
               || defensiveWeapon == Weapon.Bow 
               || attackingWeapon == Weapon.Bow;
    }

}