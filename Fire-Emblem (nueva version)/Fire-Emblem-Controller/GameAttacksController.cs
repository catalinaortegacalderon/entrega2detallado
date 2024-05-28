using Fire_Emblem_Model.DataTypes;
using Fire_Emblem_View;
using Fire_Emblem_Model;

namespace Fire_Emblem;

public class GameAttacksController
{
    private readonly Player[] _players = new Player[2];
    private int _currentAttacker;
    private bool _gameIsTerminated;
    private int _winner;
    private bool _roundIsTerminated;
    private Unit _currentAttackingUnit;
    private Unit _currentDefensiveUnit;
    private int _firstPlayersCurrentUnitNumber;
    private int _secondPlayersCurrentUnitNumber;
    private int _attackValue;
    private AttackCalculator _attackCalculator;

    public GameAttacksController(Player firstPlayer, Player secondPlayer)
    {
        this._currentAttacker = 0;
        this._players[0] = firstPlayer;
        this._players[1] = secondPlayer;
        this._gameIsTerminated = false;
    }

    public void Attack(AttackType typeOfCurrentAttack, View view, int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (this._gameIsTerminated || this._roundIsTerminated) 
            return;
        SetAttacksParameters(firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (typeOfCurrentAttack == AttackType.FirstAttack)
        {
            ActivateSkills();
            PrintStartingParameters(view);
        }
        this._attackCalculator = new AttackCalculator(_currentAttackingUnit, _currentDefensiveUnit, typeOfCurrentAttack);
        _attackValue = this._attackCalculator.CalculateAttack();
        PrintWhoAttacksWho(view);
        MakeTheDamage();
    }

    private void PrintStartingParameters(View view)
    {
        PrintAdvantages(view);
        PrintSkillsInfo(view);
    }

    private void PrintWhoAttacksWho(View view)
    {
        view.WriteLine(_currentAttackingUnit.Name + " ataca a " + _currentDefensiveUnit.Name + " con " + _attackValue + " de daño");
    }

    private void SetAttacksParameters(int firstPlayersCurrentUnitNumber,
        int secondPlayersCurrentUnitNumber)
    {
        this._firstPlayersCurrentUnitNumber = firstPlayersCurrentUnitNumber;
        this._secondPlayersCurrentUnitNumber = secondPlayersCurrentUnitNumber;
        SetAttackingAndDefensiveUnits();
    }

    private void MakeTheDamage()
    {
        if (_attackValue >= _currentDefensiveUnit.CurrentHp)
        {
            this._roundIsTerminated = true;
            SetDefensorsNewHp();
            EliminateLooserUnit();
            return;
        }
        else
        {
            SetDefensorsNewHp();
        }
    }

    private void ActivateSkills()
    {
        ActivateOnePlayersUnitSkills(_currentAttackingUnit, _currentDefensiveUnit);
        ActivateOnePlayersUnitSkills(_currentDefensiveUnit, _currentAttackingUnit);
    }

    private void EliminateLooserUnit()
    {
        Player player;
        if (_currentAttacker == 0)
        {
            player = _players[1];
        }
        else
        {
            player = _players[0];
        }
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
        if (_currentDefensiveUnit.CurrentHp < _attackValue)
        {
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
    
    public void UpdateLastOpponents()
    {
        _currentAttackingUnit.LastOpponentName = _currentDefensiveUnit.Name;
        _currentDefensiveUnit.LastOpponentName = _currentAttackingUnit.Name;
    }

    public Player[] GetPlayers()
    {
        return this._players;
    }
    
}