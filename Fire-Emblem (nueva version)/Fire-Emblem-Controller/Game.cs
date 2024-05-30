﻿using ConsoleApp1;
using ConsoleApp1.DataTypes;
using ConsoleApp1.Exceptions;
using ConsoleApp1.GameDataStructures;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private readonly GameView _view;
    private readonly string _teamsFolder;
    private GameAttacksController _attackController;
    private int _currentUnitNumberOfPlayer1;
    private int _currentUnitNumberOfPlayer2;
    private Unit _currentUnitOfPlayer1;
    private Unit _currentUnitOfPlayer2;
    private int _currentRound;
    private int _identificatorOfPlayer1 = 0;
    private int _identificatorOfPlayer2 = 1;
    
    public Game(GameView view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _currentRound = 1;
    }

    public void Play()
    {
        try
        {
            TryToPlay();
        }
        catch (InvalidTeamException)
        {
            _view.AnnounceTeamsAreNotValid();
        }
    }

    private void TryToPlay()
    {
        var teamFile = GetTeamFile();
        _attackController = GameAttacksControllerBuilder.BuildGameController(teamFile, _view);
        while (_attackController.IsGameTerminated() == false)
        {
            PlayOneRound();
        }
        _view.AnnounceWinner(_attackController.GetWinner());
    }
    
    private string GetTeamFile()
    {
        var files = ReadTeamsFiles();
        var fileNumInput = _view.AskPlayerForTheChosenFile(files);
        if (!FileChecker.IsGameValid(files[fileNumInput]))
        { 
            throw new InvalidTeamException();
        }
        return files[fileNumInput]; 
    }
    
    private void PlayOneRound()
    {
        _attackController.RestartRound();
        if (IsPlayer1TheRoundStarter())
        {
            _attackController.SetCurrentAttacker(_identificatorOfPlayer1);
            StartRound();
        }
        else 
        {
            _attackController.SetCurrentAttacker(_identificatorOfPlayer2);
            StartRound();
        }
        _currentRound++;
    }

    private void StartRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FirstAttack , 
            _currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        _attackController.ChangeAttacker();
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.SecondAttack, 
            _currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        FollowUp();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        EliminateLooserUnit();
        _attackController.SetCurrentAttacker(_identificatorOfPlayer2);
    }
    

    private void AskBothPlayersForTheChosenUnit()
    {
        // todo: arreglar trainwreck
        if (_attackController.GetCurrentAttacker() == _identificatorOfPlayer1)
        {
            
            _currentUnitNumberOfPlayer1 = _view.AskAPlayerForTheChosenUnit(_identificatorOfPlayer1, 
                _attackController.GetPlayers()[_identificatorOfPlayer1].Units);
            _currentUnitNumberOfPlayer2 = _view.AskAPlayerForTheChosenUnit(_identificatorOfPlayer2, 
                _attackController.GetPlayers()[_identificatorOfPlayer2].Units);
        }
        else
        {
            _currentUnitNumberOfPlayer2 = _view.AskAPlayerForTheChosenUnit(_identificatorOfPlayer2, 
                _attackController.GetPlayers()[_identificatorOfPlayer2].Units);
            _currentUnitNumberOfPlayer1 = _view.AskAPlayerForTheChosenUnit(_identificatorOfPlayer1, 
                _attackController.GetPlayers()[_identificatorOfPlayer1].Units);
        }
        SetUnits();
    }

    private void SetUnits()
    {
        // todo: ARREGLAR ESTE TRAINWRECJ
        _currentUnitOfPlayer1 = _attackController.GetPlayers()[_identificatorOfPlayer1].Units
            .GetUnitByIndex(_currentUnitNumberOfPlayer1);
        _currentUnitOfPlayer2 = _attackController.GetPlayers()[_identificatorOfPlayer2].Units
            .GetUnitByIndex(_currentUnitNumberOfPlayer2);
    }

    private string[] ReadTeamsFiles()
    {
        var files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void PrintRound()
    {
        
        int playersNumber  = 
            (_attackController.GetCurrentAttacker() == _identificatorOfPlayer1) ? 1 :  2;
        _view.ShowRoundInformation(_currentRound, GetCurrentAttackersName(), playersNumber);
    }

    private string GetCurrentAttackersName()
    {
        if (IsPlayer1TheRoundStarter())
        {
            return _currentUnitOfPlayer1.Name;
        }
        else
        {
            return _currentUnitOfPlayer2.Name;
        }
        
    }
    
    private void FollowUp()
    {
        if (CanDoAFollowup( _currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            _attackController.SetCurrentAttacker(_identificatorOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1,
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup( _currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            _attackController.SetCurrentAttacker(_identificatorOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.AnnounceNoUnitCanDoAFollowup();
        }
    }

    private bool CanDoAFollowup( Unit attackingUnit, Unit defensiveUnit)
    {
        const int additionValueForFollowupCondition = 5;
        return ThereAreNoLoosers() &&
               defensiveUnit.Spd 
               + defensiveUnit.ActiveBonus.Spd * defensiveUnit.ActiveBonusNeutralizator.Spd
               + defensiveUnit.ActivePenalties.Spd * defensiveUnit.ActivePenaltiesNeutralizator.Spd 
               + additionValueForFollowupCondition 
               <= attackingUnit.Spd 
               + attackingUnit.ActiveBonus.Spd * attackingUnit.ActiveBonusNeutralizator.Spd
               + attackingUnit.ActivePenalties.Spd * attackingUnit.ActivePenaltiesNeutralizator.Spd;
    }

    private void ResetUnitsBonus()
    {
        _attackController.ResetAllSkills();
    }

    private void EliminateLooserUnit()
    {
        // todo: trainwrecks
        if (_currentUnitOfPlayer1.CurrentHp == 0)
        {
            _attackController.GetPlayers()[_identificatorOfPlayer1].Units.EliminateUnit(_currentUnitNumberOfPlayer1);
        }
        if (_currentUnitOfPlayer2.CurrentHp == 0)
        {
            _attackController.GetPlayers()[_identificatorOfPlayer2].Units.EliminateUnit(_currentUnitNumberOfPlayer2);
        }
    }
    

    private bool ThereAreNoLoosers()
    {
        return _currentUnitOfPlayer2.CurrentHp != 0 && 
               _currentUnitOfPlayer1.CurrentHp != 0;
    }

    private void UpdateGameLogs()
    {
        // todo: PONER ESTO MAS BONITO, CAMBIAR NOMBRE
        _attackController.UpdateLastOpponents();
        if (IsPlayer1TheRoundStarter())
        {
            _currentUnitOfPlayer2.HasBeenBeenInACombatStartedByTheOpponent = true;
            _currentUnitOfPlayer1.HasStartedACombat= true;
        }
        else
        {
            _currentUnitOfPlayer2.HasStartedACombat = true;
            _currentUnitOfPlayer1.HasBeenBeenInACombatStartedByTheOpponent = true;
        }
    }

    private void ShowLeftoverHp()
    {
        if (IsPlayer1TheRoundStarter())
            _view.ShowHp( _currentUnitOfPlayer1, _currentUnitOfPlayer2 );
        else
            _view.ShowHp(_currentUnitOfPlayer2, _currentUnitOfPlayer1 );
    }

    private bool IsPlayer1TheRoundStarter()
    {
        return _currentRound % 2 == 1;
    }
}