namespace Fire_Emblem;
using Fire_Emblem_View;
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
    // arreglos: ENCADENAMIENTO, ACCEDER A MUCHAS COSAS A UN NIVEL MUCHO MAS ABAJO, revisar largo funcion
    // SACAR QUE FUNCIONES RETORNEN PERDEDOR, tal vez atributo desaparezca
    // eliminar comentario ataque y contraataque, ver como hacer mas evidente
    // ideas: private Printer printer (clase impresora), hacer solo 1 funcion para mostrar hp, hacer funcion para cambiar current player
    // separar play en jugador1jugando y jugador2jugando, ver si se puede hacer esto en 1 funcion, creo q si
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        if (VerifyIfTeamsAreValid(out var files, out var fileNumberInput)) return;
        _gameAttacksController = UsefulFunctions.BuildGameController(files[fileNumberInput], _view);
        while (_gameAttacksController.gameIsTerminated == false) PlayOneRound();
        _view.WriteLine("Player " + (_gameAttacksController.winner + 1) + " ganó");
    }

    private void PlayOneRound()
    {
        SetRoundsParameters();
        if (_gameAttacksController.currentAttacker == 0) Player1StartsRound();
        else if (_gameAttacksController.currentAttacker == 1) Player2StartsRound();
        _gameAttacksController.currentRound++;
    }

    private void SetRoundsParameters()
    {
        _gameAttacksController.roundIsTerminated = false;
        _currentRoundsPlayer1LooserUnitsName = "";
        _currentRoundsPlayer2LooserUnitsName = "";
    }

    private void Player1StartsRound()
    {
        _currentUnitNumberOfPlayer1 = AskPlayerForTheUnitNumber(0);
        _currentUnitNumberOfPlayer2 = AskPlayerForTheUnitNumber(1);
        PrintRound(_gameAttacksController.currentAttacker);
        _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameAttacksController.currentAttacker = 1;
        _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer1First();
        UpdateGameLogs();
        _gameAttacksController.currentAttacker = 1;
    }
    
    private void Player2StartsRound()
    {
        _currentUnitNumberOfPlayer2 = AskPlayerForTheUnitNumber(1);
        _currentUnitNumberOfPlayer1 = AskPlayerForTheUnitNumber(0);
        _view.WriteLine("Round " + _gameAttacksController.currentRound + ": " + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name + " (Player 2) comienza");
        _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameAttacksController.currentAttacker = 0;
        _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer2First();
        UpdateGameLogs();
        _gameAttacksController.currentAttacker = 0;
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

    private void PrintRound(int playerNumber)
    {
        string playerNumberString  = (playerNumber == 0) ? "1" :  "2";
        int numberOfThePlayersUnit  = (playerNumber == 0) ? _currentUnitNumberOfPlayer1 :  _currentUnitNumberOfPlayer2;
        _view.WriteLine("Round " + _gameAttacksController.currentRound + ": " + _gameAttacksController.players[playerNumber].units[numberOfThePlayersUnit].Name + " (Player " + playerNumberString + ") comienza");
    }

    private int AskPlayerForTheUnitNumber(int playerNumber)
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
        foreach (Unit unit in _gameAttacksController.players[playerNumber].units)
        {
            if (unit.Name != "") _view.WriteLine(unitNumberCounter + ": " + unit.Name);
            unitNumberCounter++;
        }
    }
    
    private void Followup()
    {
        if (SecondPlayerCanDoAFollowup())
        {
            _gameAttacksController.currentAttacker = 1;
            _currentRoundsPlayer1LooserUnitsName = _gameAttacksController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (FirstPlayerCanDoAFollowup())
        {
            _gameAttacksController.currentAttacker = 0;
            _currentRoundsPlayer2LooserUnitsName = _gameAttacksController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }

    private bool FirstPlayerCanDoAFollowup()
    {
        return _currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "" &&
               _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Spd 
               + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActiveBonus.spd * _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActiveBonusNeutralization.spd
               + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActivePenalties.spd * _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActivePenaltiesNeutralization.spd + 5 <=
               _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Spd 
               + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActiveBonus.spd * _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActiveBonusNeutralization.spd
               +_gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActivePenalties.spd * _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActivePenaltiesNeutralization.spd;
    }

    private bool SecondPlayerCanDoAFollowup()
    {
        return _currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "" &&
               _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Spd 
               + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActiveBonus.spd * _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActiveBonusNeutralization.spd
               + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActivePenalties.spd * _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].ActivePenaltiesNeutralization.spd >=
               5 + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Spd 
                 + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActiveBonus.spd * _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActiveBonusNeutralization.spd
                 +_gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActivePenalties.spd * _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].ActivePenaltiesNeutralization.spd;
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
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].GameLogs.AmountOfAttacks = 0;
            _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].GameLogs.AmountOfAttacks = 0;
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "") _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].GameLogs.AmountOfAttacks = 0;
        else if (_currentRoundsPlayer2LooserUnitsName != "") _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].GameLogs.AmountOfAttacks = 0;
    }

    private void UpdateLastOponent()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].GameLogs.LastOponentName = _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Name;
            _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].GameLogs.LastOponentName = _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name;
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "") _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].GameLogs.LastOponentName = _currentRoundsPlayer1LooserUnitsName;
        else if (_currentRoundsPlayer2LooserUnitsName != "") _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].GameLogs.LastOponentName = _currentRoundsPlayer2LooserUnitsName;
    }
    
    private void ShowLeftoverHpPrintingPlayer1First()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine(_gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Name +
                            " (" + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].CurrentHp +
                            ") : " + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name +
                            " (" + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].CurrentHp +
                            ")");
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "")
        {
            _view.WriteLine(_currentRoundsPlayer1LooserUnitsName +
                            " (0) : " + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name +
                            " (" + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].CurrentHp +
                            ")");
        }
        else
        {
            _view.WriteLine(_gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Name +
                            " (" + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].CurrentHp +
                            ") : " + _currentRoundsPlayer2LooserUnitsName +
                            " (0)");
        }
    }
    
        private void ShowLeftoverHpPrintingPlayer2First()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine(_gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name +
                            " (" + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].CurrentHp +
                            ") : " + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Name +
                            " (" + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].CurrentHp +
                            ")");
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "")
        {
            _view.WriteLine(_gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].Name +
                            " (" + _gameAttacksController.players[1].units[_currentUnitNumberOfPlayer2].CurrentHp +
                            ") : " + _currentRoundsPlayer1LooserUnitsName +
                            " (0)");
        }
        else
        {
            _view.WriteLine(_currentRoundsPlayer2LooserUnitsName +
                            " (0) : " + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].Name +
                            " (" + _gameAttacksController.players[0].units[_currentUnitNumberOfPlayer1].CurrentHp +
                            ")");
        }
    }
}