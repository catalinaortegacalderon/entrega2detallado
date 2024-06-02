using ConsoleApp1;
using ConsoleApp1.DataTypes;
using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class GameAttacksController
{
    private readonly PlayersList _players = new PlayersList();

    private readonly GameView _view;
    private int _attackValue;

    private int _currentAttackerId;
    private Unit _currentAttackingUnit;
    private Unit _currentDefensiveUnit;

    private DamageCalculator _damageCalculator;
    private bool _gameIsTerminated;

    private bool _roundIsTerminated;
    private int _winner;

    public GameAttacksController(Player firstPlayer, Player secondPlayer, GameView view)
    {
        _currentAttackerId = 0;
        _players.AddPlayer(firstPlayer);
        _players.AddPlayer(secondPlayer);
        _gameIsTerminated = false;
        _view = view;
    }

    public void GenerateAnAttackBetweenTwoUnits(AttackType typeOfCurrentAttack,
        int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (RoundIsTerminated())
            return;
        SetAttackingAndDefensiveUnits(firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (typeOfCurrentAttack == AttackType.FirstAttack)
        {
            _currentAttackingUnit.StartedTheRound = true;
            _currentDefensiveUnit.StartedTheRound = false;
            ActivateSkills();
            PrintStartingParameters();
        }

        _damageCalculator = new DamageCalculator(_currentAttackingUnit,
            _currentDefensiveUnit, typeOfCurrentAttack);
        _attackValue = _damageCalculator.CalculateAttack();
        ShowWhoAttacksWho();
        MakeTheDamage();
    }

    private bool RoundIsTerminated()
    {
        return _gameIsTerminated || _roundIsTerminated;
    }

    private void PrintStartingParameters()
    {
        ShowAdvantages();
        PrintSkillsInfo();
    }

    private void ShowWhoAttacksWho()
    {
        _view.ShowAttack(_currentAttackingUnit.Name,
            _currentDefensiveUnit.Name, _attackValue);
    }

    private void SetAttackingAndDefensiveUnits(int firstPlayersCurrentUnitNumber,
        int secondPlayersCurrentUnitNumber)
    {
        if (_currentAttackerId == 0)
        {
            // todo: trainvreck
            _currentAttackingUnit = _players.GetPlayerById(0).Units.GetUnitByIndex(firstPlayersCurrentUnitNumber);
            _currentDefensiveUnit = _players.GetPlayerById(1).Units.GetUnitByIndex(secondPlayersCurrentUnitNumber);
        }
        else
        {
            _currentAttackingUnit = _players.GetPlayerById(1).Units.GetUnitByIndex(secondPlayersCurrentUnitNumber);
            _currentDefensiveUnit = _players.GetPlayerById(0).Units.GetUnitByIndex(firstPlayersCurrentUnitNumber);
        }

        _currentAttackingUnit.IsAttacking = true;
        _currentDefensiveUnit.IsAttacking = false;
    }

    private void MakeTheDamage()
    {
        if (_attackValue >= _currentDefensiveUnit.CurrentHp)
        {
            _roundIsTerminated = true;
            SetDefensorsNewHp();
            ReduceUnitAmount();
            CheckIfGameIsTerminated();
        }
        else
        {
            SetDefensorsNewHp();
        }
    }

    private void ActivateSkills()
    {
        SkillsActivator.ActivateSkills(_currentAttackingUnit, _currentDefensiveUnit);
    }

    private void ReduceUnitAmount()
    {
        Player player;
        if (_currentAttackerId == 0)
            player = _players.GetPlayerById(1);
        else
            player = _players.GetPlayerById(0);
        player.AmountOfUnits -= 1;
    }

    private void CheckIfGameIsTerminated()
    {
        // todo: encapsular
        if (_players.GetPlayerById(0).AmountOfUnits == 0)
        {
            _winner = 1;
            _gameIsTerminated = true;
        }

        if (_players.GetPlayerById(1).AmountOfUnits == 0)
        {
            _winner = 0;
            _gameIsTerminated = true;
        }
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

    private void PrintSkillsInfo()
    {
        _view.ShowAllSkills(_currentAttackingUnit);
        _view.ShowAllSkills(_currentDefensiveUnit);
    }

    private void ShowAdvantages()
    {
        var attackingWeapon = _currentAttackingUnit.Weapon;
        var defensiveWeapon = _currentDefensiveUnit.Weapon;

        if (DamageCalculator.IsNoAdvantage(defensiveWeapon, attackingWeapon))
            _view.AnnounceThereIsNoAdvantage();
        else if (DamageCalculator.DoesAttackerHaveAdvantage(attackingWeapon, defensiveWeapon))
            _view.AnnounceAdvantage(_currentAttackingUnit, _currentDefensiveUnit);
        else
            _view.AnnounceAdvantage(_currentDefensiveUnit, _currentAttackingUnit);
    }

    public void ResetAllSkills()
    {
        ResetOnePlayersSkills(_currentAttackingUnit);
        ResetOnePlayersSkills(_currentDefensiveUnit);
    }

    private void ResetOnePlayersSkills(Unit unit)
    {
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizersToASpecificValue(unit.ActiveBonus,
            0);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizersToASpecificValue(unit.ActivePenalties,
            0);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizersToASpecificValue(unit.ActiveBonusNeutralizer,
            1);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizersToASpecificValue(unit.ActivePenaltiesNeutralizer,
            1);
        DataStructuresResetter.ResetDamageGameStructure(unit.DamageEffects);
    }


    public bool IsGameTerminated()
    {
        return _gameIsTerminated;
    }

    public void RestartRound()
    {
        _roundIsTerminated = false;
    }

    public int GetWinner()
    {
        return _winner + 1;
    }

    public int GetCurrentAttacker()
    {
        return _currentAttackerId;
    }

    public void SetCurrentAttacker(int value)
    {
        _currentAttackerId = value;
    }

    public void ChangeAttacker()
    {
        if (_currentAttackerId == 0)
            _currentAttackerId = 1;
        else
            _currentAttackerId = 0;
    }

    public void UpdateLastOpponents()
    {
        _currentAttackingUnit.LastOpponentName = _currentDefensiveUnit.Name;
        _currentDefensiveUnit.LastOpponentName = _currentAttackingUnit.Name;
    }

    public PlayersList GetPlayers()
    {
        return _players;
    }
}