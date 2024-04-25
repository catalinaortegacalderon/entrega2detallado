namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private GameController _gameController;
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
        _gameController = Functions.BuildGameController(files[fileNumberInput], _view);
        while (_gameController.gameIsTerminated == false) PlayOneRound();
        _view.WriteLine("Player " + (_gameController.winner + 1) + " ganó");
    }

    private void PlayOneRound()
    {
        _gameController.roundIsTerminated = false;
        _currentRoundsPlayer1LooserUnitsName = "";
        _currentRoundsPlayer2LooserUnitsName = "";
        if (_gameController.currentAttacker == 0) Player1StartsRound();
        else if (_gameController.currentAttacker == 1) Player2StartsRound();
        _gameController.currentRound++;
    }

    private void Player1StartsRound()
    {
        _currentUnitNumberOfPlayer1 = AskPlayerForTheUnitNumber(0);
        _currentUnitNumberOfPlayer2 = AskPlayerForTheUnitNumber(1);
        PrintRound(_gameController.currentAttacker);
        //ataque
        _currentRoundsPlayer2LooserUnitsName = _gameController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameController.currentAttacker = 1;
        //contraataque
        _currentRoundsPlayer1LooserUnitsName = _gameController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        //ActivateFollowupBonusOfPlayer(0);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer1First();
        UpdateGameLogs();
        _gameController.currentAttacker = 1;
    }
    
    private void Player2StartsRound()
    {
        _currentUnitNumberOfPlayer2 = AskPlayerForTheUnitNumber(1);
        _currentUnitNumberOfPlayer1 = AskPlayerForTheUnitNumber(0);
        _view.WriteLine("Round " + _gameController.currentRound + ": " + _gameController.players[1].units[_currentUnitNumberOfPlayer2].name + " (Player 2) comienza");
        //ataque
        _currentRoundsPlayer1LooserUnitsName = _gameController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _gameController.currentAttacker = 0;
        //contraataque
        _currentRoundsPlayer2LooserUnitsName = _gameController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        //ActivateFollowupBonusOfPlayer(1);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHpPrintingPlayer2First();
        UpdateGameLogs();
        _gameController.currentAttacker = 0;
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
        if (Functions.Juego_Valido(files[fileNumberInput]) == false)
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
        _view.WriteLine("Round " + _gameController.currentRound + ": " + _gameController.players[playerNumber].units[numberOfThePlayersUnit].name + " (Player " + playerNumberString + ") comienza");
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
        foreach (Unit unit in _gameController.players[playerNumber].units)
        {
            if (unit.name != "") _view.WriteLine(unitNumberCounter + ": " + unit.name);
            unitNumberCounter++;
        }
    }
    
    //private void ActivateFollowupBonusOfPlayer(int playerNumber)
    //{
    //    int numberOfThePlayersUnit  = (playerNumber == 0) ? _currentUnitNumberOfPlayer1 :  _currentUnitNumberOfPlayer2;
    //    _gameController.players[playerNumber].units[numberOfThePlayersUnit].activeBonus.attk += _gameController.players[playerNumber].units[numberOfThePlayersUnit].activeBonus.atkFollowup;
    //}

    private void Followup()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "" &&
            _gameController.players[1].units[_currentUnitNumberOfPlayer2].spd 
            + _gameController.players[1].units[_currentUnitNumberOfPlayer2].activeBonus.spd * _gameController.players[1].units[_currentUnitNumberOfPlayer2].activeBonusNeutralization.spd
            + _gameController.players[1].units[_currentUnitNumberOfPlayer2].activePenalties.spd * _gameController.players[1].units[_currentUnitNumberOfPlayer2].activePenaltiesNeutralization.spd >=
            5 + _gameController.players[0].units[_currentUnitNumberOfPlayer1].spd 
              + _gameController.players[0].units[_currentUnitNumberOfPlayer1].activeBonus.spd * _gameController.players[0].units[_currentUnitNumberOfPlayer1].activeBonusNeutralization.spd
              +_gameController.players[0].units[_currentUnitNumberOfPlayer1].activePenalties.spd * _gameController.players[0].units[_currentUnitNumberOfPlayer1].activePenaltiesNeutralization.spd)
        {
            _gameController.currentAttacker = 1;
            _currentRoundsPlayer1LooserUnitsName = _gameController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "" &&
                 _gameController.players[1].units[_currentUnitNumberOfPlayer2].spd 
                 + _gameController.players[1].units[_currentUnitNumberOfPlayer2].activeBonus.spd * _gameController.players[1].units[_currentUnitNumberOfPlayer2].activeBonusNeutralization.spd
                 + _gameController.players[1].units[_currentUnitNumberOfPlayer2].activePenalties.spd * _gameController.players[1].units[_currentUnitNumberOfPlayer2].activePenaltiesNeutralization.spd + 5 <=
                 _gameController.players[0].units[_currentUnitNumberOfPlayer1].spd 
                 + _gameController.players[0].units[_currentUnitNumberOfPlayer1].activeBonus.spd * _gameController.players[0].units[_currentUnitNumberOfPlayer1].activeBonusNeutralization.spd
                 +_gameController.players[0].units[_currentUnitNumberOfPlayer1].activePenalties.spd * _gameController.players[0].units[_currentUnitNumberOfPlayer1].activePenaltiesNeutralization.spd)
        {
            _gameController.currentAttacker = 0;
            _currentRoundsPlayer2LooserUnitsName = _gameController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }
    
    private void ResetUnitsBonus()
    {
        _gameController.resetAllSkills();
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
            _gameController.players[1].units[_currentUnitNumberOfPlayer2].gameLogs.amountOfAttacks++;
            _gameController.players[0].units[_currentUnitNumberOfPlayer1].gameLogs.amountOfAttacks++;
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "")
            _gameController.players[1].units[_currentUnitNumberOfPlayer2].gameLogs.amountOfAttacks++;
        else if (_currentRoundsPlayer2LooserUnitsName != "") _gameController.players[0].units[_currentUnitNumberOfPlayer1].gameLogs.amountOfAttacks++;
    }

    private void UpdateLastOponent()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _gameController.players[1].units[_currentUnitNumberOfPlayer2].gameLogs.LastOponentName = _gameController.players[0].units[_currentUnitNumberOfPlayer1].name;
            _gameController.players[0].units[_currentUnitNumberOfPlayer1].gameLogs.LastOponentName = _gameController.players[1].units[_currentUnitNumberOfPlayer2].name;
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "") _gameController.players[1].units[_currentUnitNumberOfPlayer2].gameLogs.LastOponentName = _currentRoundsPlayer1LooserUnitsName;
        else if (_currentRoundsPlayer2LooserUnitsName != "") _gameController.players[0].units[_currentUnitNumberOfPlayer1].gameLogs.LastOponentName = _currentRoundsPlayer2LooserUnitsName;
    }
    
    private void ShowLeftoverHpPrintingPlayer1First()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine(_gameController.players[0].units[_currentUnitNumberOfPlayer1].name +
                            " (" + _gameController.players[0].units[_currentUnitNumberOfPlayer1].currentHp +
                            ") : " + _gameController.players[1].units[_currentUnitNumberOfPlayer2].name +
                            " (" + _gameController.players[1].units[_currentUnitNumberOfPlayer2].currentHp +
                            ")");
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "")
        {
            _view.WriteLine(_currentRoundsPlayer1LooserUnitsName +
                            " (0) : " + _gameController.players[1].units[_currentUnitNumberOfPlayer2].name +
                            " (" + _gameController.players[1].units[_currentUnitNumberOfPlayer2].currentHp +
                            ")");
        }
        else
        {
            _view.WriteLine(_gameController.players[0].units[_currentUnitNumberOfPlayer1].name +
                            " (" + _gameController.players[0].units[_currentUnitNumberOfPlayer1].currentHp +
                            ") : " + _currentRoundsPlayer2LooserUnitsName +
                            " (0)");
        }
    }
    
        private void ShowLeftoverHpPrintingPlayer2First()
    {
        if (_currentRoundsPlayer1LooserUnitsName == "" && _currentRoundsPlayer2LooserUnitsName == "")
        {
            _view.WriteLine(_gameController.players[1].units[_currentUnitNumberOfPlayer2].name +
                            " (" + _gameController.players[1].units[_currentUnitNumberOfPlayer2].currentHp +
                            ") : " + _gameController.players[0].units[_currentUnitNumberOfPlayer1].name +
                            " (" + _gameController.players[0].units[_currentUnitNumberOfPlayer1].currentHp +
                            ")");
        }
        else if (_currentRoundsPlayer1LooserUnitsName != "")
        {
            _view.WriteLine(_gameController.players[1].units[_currentUnitNumberOfPlayer2].name +
                            " (" + _gameController.players[1].units[_currentUnitNumberOfPlayer2].currentHp +
                            ") : " + _currentRoundsPlayer1LooserUnitsName +
                            " (0)");
        }
        else
        {
            _view.WriteLine(_currentRoundsPlayer2LooserUnitsName +
                            " (0) : " + _gameController.players[0].units[_currentUnitNumberOfPlayer1].name +
                            " (" + _gameController.players[0].units[_currentUnitNumberOfPlayer1].currentHp +
                            ")");
        }
    }
}