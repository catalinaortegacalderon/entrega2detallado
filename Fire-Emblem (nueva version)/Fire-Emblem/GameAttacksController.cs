using ConsoleApp1.DataTypes;
using Fire_Emblem_Model.GameDataStructures.Lists;

namespace Fire_Emblem;
using Fire_Emblem_View;
using Fire_Emblem_Model;

public class GameAttacksController
{
    private readonly Player[] _players = new Player[2];
    private int _currentAttacker;
    private bool _gameIsTerminated;
    private int _winner;
    private bool _roundIsTerminated = false;
    private Unit _currentAttackingUnit;
    private Unit _currentDefensiveUnit;
    private int _numberOfThisRoundsCurrentAttack;
    private int _firstPlayersCurrentUnitNumber;
    private int _secondPlayersCurrentUnitNumber;
    private int _attackValue;

    public GameAttacksController(Player firstPlayer, Player secondPlayer)
    {
        this._currentAttacker = 0;
        this._players[0] = firstPlayer;
        this._players[1] = secondPlayer;
        this._gameIsTerminated = false;
    }

    public string Attack(int numberOfCurrentAttack, View view, int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (this._gameIsTerminated || this._roundIsTerminated) 
            return "";
        SetAttacksParameters(numberOfCurrentAttack, firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (_numberOfThisRoundsCurrentAttack == 1) 
            PrintStartingParameters(view);
        _attackValue = CalculateAttack();
        PrintWhoAttacksWho(view);
        if (_currentAttacker == 0) 
            return Player1Attacks();
        else { return Player2Attacks(); }
    }

    private void PrintStartingParameters(View view)
    {
        PrintAdvantages(view);
        ActivateSkills();
        PrintSkillsInfo(view);
    }

    private void PrintWhoAttacksWho(View view)
    {
        view.WriteLine(_currentAttackingUnit.Name + " ataca a " + _currentDefensiveUnit.Name + " con " + _attackValue + " de daño");
    }

    private void SetAttacksParameters(int numberOfCurrentAttack, int firstPlayersCurrentUnitNumber,
        int secondPlayersCurrentUnitNumber)
    {
        _numberOfThisRoundsCurrentAttack = numberOfCurrentAttack;
        this._firstPlayersCurrentUnitNumber = firstPlayersCurrentUnitNumber;
        this._secondPlayersCurrentUnitNumber = secondPlayersCurrentUnitNumber;
        SetAttackingAndDefensiveUnits();
    }

    private string Player2Attacks()
    {
        string loosersName;
        if (_attackValue >= _players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber).CurrentHp)
        {
            loosersName = _players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber).Name;
            this._roundIsTerminated = true;
            SetDefensorsNewHp();
            EliminateLooserUnit(_players[0], _firstPlayersCurrentUnitNumber);
            return loosersName;
        }
        else
        {
            SetDefensorsNewHp();
            //_players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber).CurrentHp = _players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber).CurrentHp - _attackValue;
        }
        return "";
    }

    private string Player1Attacks()
    {
        string loosersName;
        if (_attackValue >= _players[1].Units.GetUnitByIndex(_secondPlayersCurrentUnitNumber).CurrentHp)
        {
            loosersName = _players[1].Units.GetUnitByIndex(_secondPlayersCurrentUnitNumber).Name;
            this._roundIsTerminated = true;
            SetDefensorsNewHp();
            EliminateLooserUnit(_players[1], _secondPlayersCurrentUnitNumber);
            return loosersName;
        }
        else
        {
            SetDefensorsNewHp();
            return "";
        }
    }

    private void ActivateSkills()
    {
        ActivateOnePlayersUnitSkills(_currentAttackingUnit, _currentDefensiveUnit);
        ActivateOnePlayersUnitSkills(_currentDefensiveUnit, _currentAttackingUnit);
    }

    private void EliminateLooserUnit(Player player, int unitIndex)
    {
        // NO LA ELIMINARE ACA
        //player.Units.EliminateUnit(unitIndex);
        
        //CAMBIAR ESTE NOMBRE
        player.AmountOfUnits = player.AmountOfUnits - 1;
        if (player.AmountOfUnits == 0)
        {
            if (player.PlayerNumber == 0)
            {
                this._winner = 1;
            }
            else
            {
                this._winner = 0;
            }
            this._gameIsTerminated = true;
        }
    }

    private void SetAttackingAndDefensiveUnits()
    {
        if (_currentAttacker == 0)
        {
            _currentAttackingUnit = _players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber);
            _currentDefensiveUnit = _players[1].Units.GetUnitByIndex(_secondPlayersCurrentUnitNumber);
        }
        else
        {
            _currentAttackingUnit = _players[1].Units.GetUnitByIndex(_secondPlayersCurrentUnitNumber);
            _currentDefensiveUnit = _players[0].Units.GetUnitByIndex(_firstPlayersCurrentUnitNumber);

        }
        _currentAttackingUnit.IsAttacking = true;
        _currentDefensiveUnit.IsAttacking = false;
    }

    private void SetDefensorsNewHp()
    {
        Console.WriteLine("paso por set defensors new hp");
        if (_currentDefensiveUnit.CurrentHp < _attackValue)
        {
            Console.WriteLine("paso por donde quiero, set hp a 0");
            _currentDefensiveUnit.CurrentHp = 0;
            return;
        }
        _currentDefensiveUnit.CurrentHp -= _attackValue;
    }

    private void ActivateOnePlayersUnitSkills(Unit targetUnit, Unit opponentsUnit)
    {
        foreach (Skill skill in targetUnit.Skills)
        {
            skill.ApplyFirstCategorySkills(targetUnit, opponentsUnit);
        }
        foreach (Skill skill in targetUnit.Skills)
        {
            skill.ApplySecondCategorySkills(targetUnit, opponentsUnit);
        }
    }

    private void PrintSkillsInfo(View view)
    {
        SkillsPrinter.PrintBonus(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenalties(view, _currentAttackingUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentAttackingUnit);
        SkillsPrinter.PrintDamageEffects(view, _currentAttackingUnit);

        SkillsPrinter.PrintBonus(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenalties(view, _currentDefensiveUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentDefensiveUnit);
        SkillsPrinter.PrintDamageEffects(view, _currentDefensiveUnit);
    }
    
    // CALCULAR ATAQUE SERA UNA CLASE DISTINTA

    public int CalculateAttack()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        int rivalsDefOrRes = CalculateRivalsDefOrRes(attackingWeapon);
        double wtb = CalculateWtb(defensiveWeapon, attackingWeapon);
        int unitsAtk = CalculateUnitsAtk();
        _currentAttackingUnit.GameLogs.AmountOfAttacks++;
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
            //finalDamage =
            //    (initialDamage + _currentAttackingUnit.DamageEffects.ExtraDamage) *
            //    _currentDefensiveUnit.DamageEffects.PorcentualReduction +
            //    _currentDefensiveUnit.DamageEffects.AbsolutDamageReduction;
            //finalDamage  = initialDamage;
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

    public void PrintAdvantages(View view)
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) view.WriteLine(_currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ") tiene ventaja con respecto a " + _currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ")");
        else
        {
            view.WriteLine(_currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ") tiene ventaja con respecto a " + _currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ")");
        }
    }

    private static bool AttackerHasAdvantage(Weapon attackingWeapon, Weapon defensiveWeapon)
    {
        return (attackingWeapon == Weapon.Sword & defensiveWeapon == Weapon.Axe) || (attackingWeapon == Weapon.Lance & defensiveWeapon == Weapon.Sword) || (attackingWeapon == Weapon.Axe & defensiveWeapon == Weapon.Lance);
    }

    private static bool ThereIsNoAdvantage(Weapon defensiveWeapon, Weapon attackingWeapon)
    {
        return defensiveWeapon == attackingWeapon || attackingWeapon == Weapon.Magic || defensiveWeapon == Weapon.Magic || defensiveWeapon == Weapon.Bow || attackingWeapon == Weapon.Bow;
    }

    public void ResetAllSkills()
    {
        ResetAttackersSkills();
        ResetDefensorsSkills();
    }

    private void ResetAttackersSkills()
    {
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActiveBonus, 0);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActivePenalties, 0);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActiveBonusNeutralization, 1);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActivePenaltiesNeutralization, 1);
        DataStructuresFunctions.ResetDamageStructure(_currentAttackingUnit.DamageEffects);
    }

    private void ResetDefensorsSkills()
    {
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActiveBonus, 0);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActivePenalties, 0);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActiveBonusNeutralization, 1);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActivePenaltiesNeutralization, 1);
        DataStructuresFunctions.ResetDamageStructure(_currentDefensiveUnit.DamageEffects);
    }

    public bool IsGameTerminated()
    {
        return this._gameIsTerminated;
    }

    public void RestartRound()
    {
        this._roundIsTerminated = false;
    }
    
    public int GetWinner()
    {
        return this._winner + 1;
    }

    public int GetCurrentAttacker()
    {
        return this._currentAttacker;
    }
    
    public void SetCurrentAttacker(int value)
    {
        this._currentAttacker = value;
    }
    public void ChangeAttacker()
    {
        if (this._currentAttacker == 0)
        {
            this._currentAttacker = 1;
        }
        else
        {
            this._currentAttacker = 0;
        }
    }
    

    public void UpdateAttacks()
    {
        _currentAttackingUnit.GameLogs.AmountOfAttacks = 0;
        _currentDefensiveUnit.GameLogs.AmountOfAttacks = 0;
    }
    
    public void UpdateLastOpponents()
    {
        _currentAttackingUnit.GameLogs.LastOpponentName = _currentDefensiveUnit.Name;
        _currentDefensiveUnit.GameLogs.LastOpponentName = _currentAttackingUnit.Name;
    }
    
    public string GetAttackersName()
    {
        return _currentAttackingUnit.Name;
    }

    public Player[] GetPlayers()
    {
        return this._players;
    }
    
}