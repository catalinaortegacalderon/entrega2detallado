using ConsoleApp1.DataTypes;
using Fire_Emblem_View;
using Fire_Emblem_Model;

namespace Fire_Emblem;

public class AttackCalculator
{
    private readonly Player[] _players = new Player[2];
    private Unit _currentAttackingUnit;
    private Unit _currentDefensiveUnit;
    private int _numberOfThisRoundsCurrentAttack;
    private int _firstPlayersCurrentUnitNumber;
    private int _secondPlayersCurrentUnitNumber;
    private int _attackValue;

    public AttackCalculator(Unit attackingUnit, Unit defensiveUnit, int attackNumber)
    {
        this._currentAttackingUnit = attackingUnit;
        this._currentDefensiveUnit = defensiveUnit;
        this._numberOfThisRoundsCurrentAttack = attackNumber;
    }
    
    public int CalculateAttack()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        int rivalsDefOrRes = CalculateRivalsDefOrRes(attackingWeapon);
        double wtb = CalculateWtb(defensiveWeapon, attackingWeapon);
        int unitsAtk = CalculateUnitsAtk();
        double finalDamage = CalculateFinalDamage(unitsAtk * wtb - rivalsDefOrRes);
        if ((finalDamage) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(finalDamage));
    }

    private int CalculateUnitsAtk()
    {
        int unitsAtk = _currentAttackingUnit.Atk + _currentAttackingUnit.ActiveBonus.Attk * _currentAttackingUnit.ActiveBonusNeutralization.Attk + _currentAttackingUnit.ActivePenalties.Attk * _currentAttackingUnit.ActivePenaltiesNeutralization.Attk;
        if (_numberOfThisRoundsCurrentAttack == 1 || _numberOfThisRoundsCurrentAttack == 2)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFirstAttack * _currentAttackingUnit.ActiveBonusNeutralization.Attk +
                          _currentAttackingUnit.ActivePenalties.AtkFirstAttack * _currentAttackingUnit.ActivePenaltiesNeutralization.Attk;
        }
        if (_numberOfThisRoundsCurrentAttack == 3)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.AtkFollowup * _currentAttackingUnit.ActiveBonusNeutralization.Attk
                          + _currentAttackingUnit.ActivePenalties.AtkFollowup * _currentAttackingUnit.ActivePenaltiesNeutralization.Attk;
        }
        return unitsAtk;
    }

    private static double CalculateWtb(Weapon defensiveWeapon, Weapon attackingWeapon)
    {
        double wtb;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) wtb = 1;
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) wtb = 1.2;
        else
        {
            wtb = 0.8;
        }
        return wtb;
    }

    private int CalculateRivalsDefOrRes(Weapon attackingWeapon)
    {
        int rivalsDefOrRes;
        if (attackingWeapon == Weapon.Magic)
        {
            rivalsDefOrRes = _currentDefensiveUnit.Res + _currentDefensiveUnit.ActiveBonus.Res * _currentDefensiveUnit.ActiveBonusNeutralization.Res + _currentDefensiveUnit.ActivePenalties.Res *_currentDefensiveUnit.ActivePenaltiesNeutralization.Res;
            if (_numberOfThisRoundsCurrentAttack == 1 || _numberOfThisRoundsCurrentAttack == 2) rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.ResFirstAttack * _currentDefensiveUnit.ActiveBonusNeutralization.Res + _currentDefensiveUnit.ActivePenalties.ResFirstAttack *_currentDefensiveUnit.ActivePenaltiesNeutralization.Res;
        }
        else
        {
            rivalsDefOrRes = _currentDefensiveUnit.Def + _currentDefensiveUnit.ActiveBonus.Def * _currentDefensiveUnit.ActiveBonusNeutralization.Def + _currentDefensiveUnit.ActivePenalties.Def *_currentDefensiveUnit.ActivePenaltiesNeutralization.Def;
            if (_numberOfThisRoundsCurrentAttack == 1 || _numberOfThisRoundsCurrentAttack == 2) rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.DefFirstAttack * _currentDefensiveUnit.ActiveBonusNeutralization.Def + _currentDefensiveUnit.ActivePenalties.DefFirstAttack *_currentDefensiveUnit.ActivePenaltiesNeutralization.Def;
        }

        return rivalsDefOrRes;
    }
    private double CalculateFinalDamage(double initialDamage)
    {
        double finalDamage  = initialDamage;
        if (_numberOfThisRoundsCurrentAttack == 1)
        {
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage + _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack) *
                _currentDefensiveUnit.DamageEffects.PorcentualReduction *  _currentDefensiveUnit.DamageEffects.PorcentualReductionRivalsFirstAttack +
                _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
            
        }
        else if (_numberOfThisRoundsCurrentAttack == 2)
        {
            finalDamage  = initialDamage;
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage + _currentAttackingUnit.DamageEffects.ExtraDamageFirstAttack) *
                _currentDefensiveUnit.DamageEffects.PorcentualReduction *  _currentDefensiveUnit.DamageEffects.PorcentualReductionRivalsFirstAttack +
                _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
            
        }
        else if (_numberOfThisRoundsCurrentAttack == 3)
        {
            finalDamage  = initialDamage;
            finalDamage =
                (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage + _currentAttackingUnit.DamageEffects.ExtraDamageFollowup) *
                _currentDefensiveUnit.DamageEffects.PorcentualReduction * _currentDefensiveUnit.DamageEffects.PorcentualReductionRivalsFollowup +
                _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction + _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
        }
        //TIRAR EXCEPCION TAL VEZ SI EL NUMBER OF ATTACK ES DISTINTO
        return finalDamage;
    }
    
    // ESTOS DOS METODOS LOS REPITO EN GAMES ATTACK CONTROLLER
    
    private static bool AttackerHasAdvantage(Weapon attackingWeapon, Weapon defensiveWeapon)
    {
        return (attackingWeapon == Weapon.Sword & defensiveWeapon == Weapon.Axe) || (attackingWeapon == Weapon.Lance & defensiveWeapon == Weapon.Sword) || (attackingWeapon == Weapon.Axe & defensiveWeapon == Weapon.Lance);
    }

    private static bool ThereIsNoAdvantage(Weapon defensiveWeapon, Weapon attackingWeapon)
    {
        return defensiveWeapon == attackingWeapon || attackingWeapon == Weapon.Magic || defensiveWeapon == Weapon.Magic || defensiveWeapon == Weapon.Bow || attackingWeapon == Weapon.Bow;
    }

}