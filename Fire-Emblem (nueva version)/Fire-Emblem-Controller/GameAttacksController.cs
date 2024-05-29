using ConsoleApp1;
using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using Fire_Emblem_View;

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
    private GameView _view;

    public GameAttacksController(Player firstPlayer, Player secondPlayer, GameView view)
    {
        this._currentAttacker = 0;
        this._players[0] = firstPlayer;
        this._players[1] = secondPlayer;
        this._gameIsTerminated = false;
        this._view = view;
    }

    public void Attack(AttackType typeOfCurrentAttack, 
        int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (this._gameIsTerminated || this._roundIsTerminated) 
            return;
        SetAttacksParameters(firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (typeOfCurrentAttack == AttackType.FirstAttack)
        {
            _currentAttackingUnit.StartedTheRound = true;
            _currentDefensiveUnit.StartedTheRound = false;
            ActivateSkills();
            PrintStartingParameters();
        }
        this._attackCalculator = new AttackCalculator(_currentAttackingUnit, 
            _currentDefensiveUnit, typeOfCurrentAttack);
        _attackValue = this._attackCalculator.CalculateAttack();
        ShowWhoAttacksWho();
        MakeTheDamage();
    }

    private void PrintStartingParameters()
    {
        ShowAdvantages();
        PrintSkillsInfo();
    }

    private void ShowWhoAttacksWho()
    {
        _view.ShowAttack(_currentAttackingUnit.Name, _currentDefensiveUnit.Name, _attackValue);
       
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
        
        // TODO: CAMBIAR TARGET UNIT, NO ES TAN DESCRIPTIVO
        ActivateOnePlayersUnitSkills(_currentAttackingUnit, _currentDefensiveUnit);
        ActivateOnePlayersUnitSkills(_currentDefensiveUnit, _currentAttackingUnit);
        
        foreach (Skill skill in _currentAttackingUnit.Skills)
        {
            skill.ApplySkillsOfACertainPriority(_currentAttackingUnit, _currentDefensiveUnit, 4);
            //skill.ApplyThirdCategorySkills(opponentsUnit, targetUnit);
        }
        
        foreach (Skill skill in _currentDefensiveUnit.Skills)
        {
            skill.ApplySkillsOfACertainPriority(_currentDefensiveUnit, _currentAttackingUnit, 4);
            //skill.ApplyThirdCategorySkills(opponentsUnit, targetUnit);
        }
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
            // todo: arreglar esto
            skill.ApplySkillsOfACertainPriority(targetUnit, opponentsUnit, 1);
            //skill.ApplyFirstCategorySkills(targetUnit, opponentsUnit);
        }
        foreach (Skill skill in targetUnit.Skills)
        {
            skill.ApplySecondCategorySkills(targetUnit, opponentsUnit);
        }
        foreach (Skill skill in targetUnit.Skills)
        {
            skill.ApplyThirdCategorySkills(targetUnit, opponentsUnit);
            //skill.ApplyThirdCategorySkills(opponentsUnit, targetUnit);
        }
        // LA CATEGORIA 4 NECESITA QUE SE APLIQUEN LAS SKILLS DEL RIVAL PRIMERO
    }

    private void PrintSkillsInfo()
    {
        _view.ShowAllSkills( _currentAttackingUnit);
        _view.ShowAllSkills( _currentDefensiveUnit);
    }
    
    public void ShowAdvantages()
    {
        Weapon attackingWeapon = _currentAttackingUnit.Weapon;
        Weapon defensiveWeapon = _currentDefensiveUnit.Weapon;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) 
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) 
            _view.WriteLine(_currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ") tiene ventaja con respecto a " + _currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ")");
        else
        {
            _view.WriteLine(_currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ") tiene ventaja con respecto a " + _currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ")");
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
        ResetOnePlayersSkills(_currentAttackingUnit);
        ResetOnePlayersSkills(_currentDefensiveUnit);
    }

    private void ResetOnePlayersSkills(Unit unit)
    {
        DataStructuresFunctions.SetStructureTo(unit.ActiveBonus, 0);
        DataStructuresFunctions.SetStructureTo(unit.ActivePenalties, 0);
        DataStructuresFunctions.SetStructureTo(unit.ActiveBonusNeutralization, 1);
        DataStructuresFunctions.SetStructureTo(unit.ActivePenaltiesNeutralization, 1);
        DataStructuresFunctions.ResetDamageStructure(unit.DamageEffects);
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
    
    public Unit GetCurrentAttackingUnit()
    {
        return this._currentAttackingUnit;
    }
    public Unit GetCurrentDefensiveUnit()
    {
        return this._currentDefensiveUnit;
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