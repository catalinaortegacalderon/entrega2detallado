using ConsoleApp1;
using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;
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
    private DamageCalculator _damageCalculator;
    private GameView _view;

    public GameAttacksController(Player firstPlayer, Player secondPlayer, GameView view)
    {
        this._currentAttacker = 0;
        this._players[0] = firstPlayer;
        this._players[1] = secondPlayer;
        this._gameIsTerminated = false;
        this._view = view;
    }

    public void GenerateAnAttackBetweenTwoUnits(AttackType typeOfCurrentAttack, 
        int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (RoundIsTerminated()) 
            return;
        SetAttacksParameters(firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (typeOfCurrentAttack == AttackType.FirstAttack)
        {
            _currentAttackingUnit.StartedTheRound = true;
            _currentDefensiveUnit.StartedTheRound = false;
            ActivateSkills();
            PrintStartingParameters();
        }
        this._damageCalculator = new DamageCalculator(_currentAttackingUnit, 
            _currentDefensiveUnit, typeOfCurrentAttack);
        _attackValue = this._damageCalculator.CalculateAttack();
        ShowWhoAttacksWho();
        MakeTheDamage();
    }

    private bool RoundIsTerminated()
    {
        return this._gameIsTerminated || this._roundIsTerminated;
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
            ReduceUnitAmount();
            CheckIfGameIsTerminated();
            //CheckOfThereIsAWinner();
        }
        else
        {
            SetDefensorsNewHp();
        }
    }

    private void ActivateSkills()
    {
        var conditionEffectPairs = GetAllConditionEffectPairs();
        var prioritizedList = PiorizeConditionSkillPairs(conditionEffectPairs);
        ApplyAllValidEffects(prioritizedList);
    }
    
    private List<ConditionEffectPair> GetAllConditionEffectPairs(){
        List<ConditionEffectPair> conditionEffectPairs = new List<ConditionEffectPair> {};
        foreach (Skill skill in _currentAttackingUnit.Skills)
        {
            for (int i = 0; i < skill.GetConditionLength(); i++)
            {
                conditionEffectPairs.Add(new ConditionEffectPair(_currentAttackingUnit,
                    _currentDefensiveUnit, skill, i));
            }
        }
        foreach (Skill skill in _currentDefensiveUnit.Skills)
        {
            for (int i = 0; i < skill.GetConditionLength(); i++)
            {
                conditionEffectPairs.Add(new ConditionEffectPair(_currentDefensiveUnit, 
                    _currentAttackingUnit, skill, i));
            }
        }
        return conditionEffectPairs;
    }
    
    private List<ConditionEffectPair> PiorizeConditionSkillPairs(List<ConditionEffectPair> conditionEffectPairs){
        List<ConditionEffectPair> prioritizedList = conditionEffectPairs
            .OrderBy(pair => pair.Condition.GetPriority())
            .ToList();
        return prioritizedList;
    }
    
    private void ApplyAllValidEffects(List<ConditionEffectPair> prioritizedList){
        foreach (ConditionEffectPair conditionEffectPair in prioritizedList)
        {
            if (conditionEffectPair.Condition.DoesItHold(conditionEffectPair.UnitThatHasThePair, 
                    conditionEffectPair.OpponentsUnit))
            {
                conditionEffectPair.Effect.ApplyEffect(conditionEffectPair.UnitThatHasThePair, 
                    conditionEffectPair.OpponentsUnit);
            }
        }
    }

    private void ReduceUnitAmount()
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
        player.AmountOfUnits -= 1;
    }

    private void CheckIfGameIsTerminated()
    {
        if (_players[0].AmountOfUnits == 0){
            this._winner = 1;
            this._gameIsTerminated = true;
        }
        if (_players[1].AmountOfUnits == 0)
        {
            this._winner = 0;
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
    
    private void PrintSkillsInfo()
    {
        _view.ShowAllSkills( _currentAttackingUnit);
        _view.ShowAllSkills( _currentDefensiveUnit);
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
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizatorsToASpecificValue(unit.ActiveBonus,
            0);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizatorsToASpecificValue(unit.ActivePenalties,
            0);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizatorsToASpecificValue(unit.ActiveBonusNeutralizator, 
            1);
        DataStructuresResetter.ResetBonusPenaltiesAndNeutralizatorsToASpecificValue(unit.ActivePenaltiesNeutralizator,
            1);
        DataStructuresResetter.ResetDamageGameStructure(unit.DamageEffects);
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