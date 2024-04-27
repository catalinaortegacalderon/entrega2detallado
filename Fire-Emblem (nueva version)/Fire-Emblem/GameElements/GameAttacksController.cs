namespace Fire_Emblem;
using Fire_Emblem_View;

public class GameAttacksController
{
    public Player[] Players = new Player[2];
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
        this.Players[0] = firstPlayer;
        this.Players[1] = secondPlayer;
        this._gameIsTerminated = false;
    }

    public string Attack(int numberOfCurrentAttack, View view, int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (this._gameIsTerminated || this._roundIsTerminated) return "";
        SetAttacksParameters(numberOfCurrentAttack, firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (_numberOfThisRoundsCurrentAttack == 1) PrintStartingParameters(view);
        _attackValue = CalculateAttack();
        PrintWhoAttacksWho(view);
        if (_currentAttacker == 0) return Player1Attacks(_attackValue);
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
        if (_attackValue >= Players[0].Units[_firstPlayersCurrentUnitNumber].CurrentHp)
        {
            loosersName = Players[0].Units[_firstPlayersCurrentUnitNumber].Name;
            this._roundIsTerminated = true;
            EliminateLooserUnitOfPlayer1();
            return loosersName;
        }
        else
        {
            Players[0].Units[_firstPlayersCurrentUnitNumber].CurrentHp = Players[0].Units[_firstPlayersCurrentUnitNumber].CurrentHp - _attackValue;
        }
        return "";
    }

    private string Player1Attacks(int attackValue)
    {
        string loosersName;
        if (attackValue >= Players[1].Units[_secondPlayersCurrentUnitNumber].CurrentHp)
        {
            loosersName = Players[1].Units[_secondPlayersCurrentUnitNumber].Name;
            this._roundIsTerminated = true;
            EliminateLooserUnitOfPlayer2(_secondPlayersCurrentUnitNumber);
            return loosersName;
        }
        else
        {
            SetDefendorsNewHp(_secondPlayersCurrentUnitNumber, attackValue);
            return "";
        }
    }

    private void ActivateSkills()
    {
        ActivateAttackersUnitSkills();
        ActivateDefensorsUnitSkills();
    }

    private void EliminateLooserUnitOfPlayer1()
    {
        Players[0].Units.RemoveAt(_firstPlayersCurrentUnitNumber);
        Players[0].AmountOfUnits = Players[0].AmountOfUnits - 1;
        if (Players[0].AmountOfUnits == 0)
        {
            this._gameIsTerminated = true;
            this._winner = 1;
        }
    }

    private void SetAttackingAndDefensiveUnits()
    {
        if (_currentAttacker == 0)
        {
            _currentAttackingUnit = Players[0].Units[_firstPlayersCurrentUnitNumber];
            _currentDefensiveUnit = Players[1].Units[_secondPlayersCurrentUnitNumber];
        }
        else
        {
            _currentAttackingUnit = Players[1].Units[_secondPlayersCurrentUnitNumber];
            _currentDefensiveUnit = Players[0].Units[_firstPlayersCurrentUnitNumber];

        }
    }

    private void SetDefendorsNewHp(int secondPlayersCurrentUnitNumber, int attackValue)
    {
        _currentDefensiveUnit.CurrentHp -= attackValue;
    }

    private void EliminateLooserUnitOfPlayer2(int secondPlayersCurrentUnitNumber)
    {
        Players[1].Units.RemoveAt(_secondPlayersCurrentUnitNumber);
        Players[1].AmountOfUnits = Players[1].AmountOfUnits - 1;
        if (Players[1].AmountOfUnits == 0)
        {
            this._gameIsTerminated = true;
            this._winner = 0;
        }
    }

    private void ActivateAttackersUnitSkills()
    {
        foreach (Skill habilidad in _currentAttackingUnit.Skills)
        {
            habilidad.ApplySkills(_currentAttackingUnit, _currentDefensiveUnit, true);
        }
    }
    private void ActivateDefensorsUnitSkills()
    {
        foreach (Skill habilidad in _currentDefensiveUnit.Skills)
        {
            habilidad.ApplySkills(_currentDefensiveUnit, _currentAttackingUnit, false);
        }
    }

    private void PrintSkillsInfo(View view)
    {
        SkillsPrinter.PrintBonus(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenalties(view, _currentAttackingUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentAttackingUnit);

        SkillsPrinter.PrintBonus(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenalties(view, _currentDefensiveUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentDefensiveUnit);
    }

    public int CalculateAttack()
    {
        string attackingWeapon = _currentAttackingUnit.Weapon;
        string defensiveWeapon = _currentDefensiveUnit.Weapon;
        int rivalsDefOrRes = CalculateRivalsDefOrRes(attackingWeapon);
        double wtb = CalculateWtb(defensiveWeapon, attackingWeapon);
        int unitsAtk = CalculateUnitsAtk();
        _currentAttackingUnit.GameLogs.AmountOfAttacks++;
        if ((unitsAtk * wtb - rivalsDefOrRes) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(unitsAtk * wtb - rivalsDefOrRes));
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

    private static double CalculateWtb(string defensiveWeapon, string attackingWeapon)
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

    private int CalculateRivalsDefOrRes(string attackingWeapon)
    {
        int rivalsDefOrRes;
        if (attackingWeapon == "Magic")
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

    public void PrintAdvantages(View view)
    {
        string attackingWeapon = _currentAttackingUnit.Weapon;
        string defensiveWeapon = _currentDefensiveUnit.Weapon;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) view.WriteLine(_currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ") tiene ventaja con respecto a " + _currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ")");
        else
        {
            view.WriteLine(_currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ") tiene ventaja con respecto a " + _currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ")");
        }
    }

    private static bool AttackerHasAdvantage(string attackingWeapon, string defensiveWeapon)
    {
        return (attackingWeapon == "Sword" & defensiveWeapon == "Axe") || (attackingWeapon == "Lance" & defensiveWeapon == "Sword") || (attackingWeapon == "Axe" & defensiveWeapon == "Lance");
    }

    private static bool ThereIsNoAdvantage(string defensiveWeapon, string attackingWeapon)
    {
        return defensiveWeapon == attackingWeapon || attackingWeapon == "Magic" || defensiveWeapon == "Magic" || defensiveWeapon == "Bow" || attackingWeapon == "Bow";
    }

    public void ResetAllSkills()
    {
        ResetAttackersSkills();
        ResetDefensorsSkills();
    }

    private void ResetDefensorsSkills()
    {
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActiveBonus, 0);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActivePenalties, 0);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActiveBonusNeutralization, 1);
        DataStructuresFunctions.SetStructureTo(_currentAttackingUnit.ActivePenaltiesNeutralization, 1);
    }

    private void ResetAttackersSkills()
    {
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActiveBonus, 0);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActivePenalties, 0);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActiveBonusNeutralization, 1);
        DataStructuresFunctions.SetStructureTo(_currentDefensiveUnit.ActivePenaltiesNeutralization, 1);
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
        return this.Players;
    }
    
}