﻿namespace Fire_Emblem;
using Fire_Emblem_View;
using Fire_Emblem_Model;
using System.Text.Json;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private GameAttacksController _gameAttacksController;
    private string _currentRoundsPlayer1LooserUnitsName;
    private string _currentRoundsPlayer2LooserUnitsName;
    private int _currentUnitNumberOfPlayer1;
    private int _currentUnitNumberOfPlayer2;
    private int _currentRound;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _currentRound = 1;
    }

    public void Play()
    {
        if (VerifyIfTeamsAreValid(out var files, out var fileNumberInput))
        {
            return;
        }
        _gameAttacksController = UsefulFunctions.BuildGameController(files[fileNumberInput], _view);
        while (_gameAttacksController.IsGameTerminated() == false)
        {
            PlayOneRound();
        }
        _view.WriteLine("Player " + (_gameAttacksController.GetWinner()) + " ganó");
    }

    private void PlayOneRound()
    {
        SetRoundsParameters();
        if (_gameAttacksController.GetCurrentAttacker() == 0) Player1StartsRound();
        else if (_gameAttacksController.GetCurrentAttacker() == 1) Player2StartsRound();
        _currentRound++;
    }

    private void SetRoundsParameters()
    {
        _gameAttacksController.RestartRound();
        _currentRoundsPlayer1LooserUnitsName = "";
        _currentRoundsPlayer2LooserUnitsName = "";
    }

    private void Player1StartsRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameAttacksController.SetCurrentAttacker(1);
        _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer1First();
        UpdateGameLogs();
        _gameAttacksController.SetCurrentAttacker(1);
    }
    
    private void Player2StartsRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameAttacksController.SetCurrentAttacker(0);
        _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer2First();
        UpdateGameLogs();
        _gameAttacksController.SetCurrentAttacker(0);
    }

    private void AskBothPlayersForTheChosenUnit()
    {
        if (_gameAttacksController.GetCurrentAttacker() == 0)
        {
            _currentUnitNumberOfPlayer1 = AskAPlayerForTheChosenUnit(0);
            _currentUnitNumberOfPlayer2 = AskAPlayerForTheChosenUnit(1);
        }
        else
        {
            _currentUnitNumberOfPlayer2 = AskAPlayerForTheChosenUnit(1);
            _currentUnitNumberOfPlayer1 = AskAPlayerForTheChosenUnit(0);
        }
    }

    private bool VerifyIfTeamsAreValid(out string[] files, out int fileNumberInput)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        files = Directory.GetFiles(_teamsFolder);
        int filesCounter = 0;
        foreach (string archivo in files)
        {
            _view.WriteLine(filesCounter + ": " + Path.GetFileName(archivo));
            filesCounter++;
        }
        fileNumberInput = Convert.ToInt32(_view.ReadLine());
        if (UsefulFunctions.CheckIfGameIsValid(files[fileNumberInput]) == false)
        {
            _view.WriteLine("Archivo de equipos no válido");
            return true;
        }
        return false;
    }

    private void PrintRound()
    {
        string playerNumberString  = (_gameAttacksController.GetCurrentAttacker() == 0) ? "1" :  "2";
        int numberOfThePlayersUnit  = (_gameAttacksController.GetCurrentAttacker() == 0) ? _currentUnitNumberOfPlayer1 :  _currentUnitNumberOfPlayer2;
        _view.WriteLine("Round " + _currentRound + ": " + _gameAttacksController.GetPlayers()[_gameAttacksController.GetCurrentAttacker()].Units[numberOfThePlayersUnit].Name + " (Player " + playerNumberString + ") comienza");
    }

    private int AskAPlayerForTheChosenUnit(int playerNumber)
    {
        PrintUnitOptions(playerNumber);
        int chosenUnitNumber = Convert.ToInt32(_view.ReadLine());
        return chosenUnitNumber;
    }
    private void PrintUnitOptions(int playerNumber)
    {
        int unitNumberCounter = 0;
        string playerNumberString  = (playerNumber == 0) ? "1" :  "2";
        _view.WriteLine("Player "+ playerNumberString+ " selecciona una opción");
        foreach (Unit unit in _gameAttacksController.GetPlayers()[playerNumber].Units)
        {
            if (unit.Name != "") _view.WriteLine(unitNumberCounter + ": " + unit.Name);
            unitNumberCounter++;
        }
    }
    
    private void Followup()
    {
        if (SecondPlayerCanDoAFollowup())
        {
            _gameAttacksController.SetCurrentAttacker(1);
            _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (FirstPlayerCanDoAFollowup())
        {
            _gameAttacksController.SetCurrentAttacker(0);
            _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }

    private bool FirstPlayerCanDoAFollowup()
    {
        return ThereAreNoLoosers() &&
               _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].Spd 
               + _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActiveBonus.Spd * _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActiveBonusNeutralization.Spd
               + _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActivePenalties.Spd * _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActivePenaltiesNeutralization.Spd + 5 <=
               _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].Spd 
               + _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActiveBonus.Spd * _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActiveBonusNeutralization.Spd
               +_gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActivePenalties.Spd * _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActivePenaltiesNeutralization.Spd;
    }

    private bool SecondPlayerCanDoAFollowup()
    {
        return ThereAreNoLoosers() &&
               _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].Spd 
               + _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActiveBonus.Spd * _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActiveBonusNeutralization.Spd
               + _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActivePenalties.Spd * _gameAttacksController.GetPlayers()[1].Units[_currentUnitNumberOfPlayer2].ActivePenaltiesNeutralization.Spd >=
               5 + _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].Spd 
                 + _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActiveBonus.Spd * _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActiveBonusNeutralization.Spd
                 +_gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActivePenalties.Spd * _gameAttacksController.GetPlayers()[0].Units[_currentUnitNumberOfPlayer1].ActivePenaltiesNeutralization.Spd;
    }

    private void ResetUnitsBonus()
    {
        _gameAttacksController.ResetAllSkills();
    }
    
    private void UpdateGameLogs()
    {
        UpdateLastOponent();
        UpdateAttacks();
    }

    private void UpdateAttacks()
    {
        _gameAttacksController.UpdateAttacks();
    }

    private bool Player2LoosesRound()
    {
        return _currentRoundsPlayer2LooserUnitsName != "";
    }

    private bool Player1LoosesRound()
    {
        return _currentRoundsPlayer1LooserUnitsName != "";
    }

    private bool ThereAreNoLoosers()
    {
        return _currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "";
    }

    private void UpdateLastOponent()
    {
        _gameAttacksController.UpdateLastOpponents();
    }

    private void ShowLeftoverHpPrintingPlayer1First()
    {
        if (ThereAreNoLoosers())
        {
            _view.WriteLine(GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ") : " + GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ")");
        }
        else if (Player1LoosesRound())
        {
            _view.WriteLine(_currentRoundsPlayer1LooserUnitsName +
                            " (0) : " + GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ")");
        }
        else
        {
            _view.WriteLine(GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ") : " + _currentRoundsPlayer2LooserUnitsName +
                            " (0)");
        }
    }
    
        private void ShowLeftoverHpPrintingPlayer2First()
    {
        if (ThereAreNoLoosers())
        {
            _view.WriteLine(GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ") : " + GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ")");
        }
        else if (Player1LoosesRound())
        {
            _view.WriteLine(GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ") : " + _currentRoundsPlayer1LooserUnitsName +
                            " (0)");
        }
        else
        {
            _view.WriteLine(_currentRoundsPlayer2LooserUnitsName +
                            " (0) : " + GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ")");
        }
    }

    private string GetPlayersName(int player)
    {
        int unitNumber;
        if (player == 0) unitNumber = _currentUnitNumberOfPlayer1;
        else
        {
            unitNumber = _currentUnitNumberOfPlayer2;
        }
        return _gameAttacksController.GetPlayers()[player].Units[unitNumber].Name;
    }
    
    private int GetPlayersHp(int player)
    {
        int unitNumber;
        if (player == 0) unitNumber = _currentUnitNumberOfPlayer1;
        else
        {
            unitNumber = _currentUnitNumberOfPlayer2;
        }
        return _gameAttacksController.GetPlayers()[player].Units[unitNumber].CurrentHp;
    }
    
}