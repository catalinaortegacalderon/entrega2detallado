namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private GameController _gameController;
    private string _player1UnitLooserName;
    private string _player2UnitLooserName;
    private int _numberUnitOfPlayer1;
    private int _numberUnitOfPlayer2;
    // arreglos: ENCADENAMIENTO, ACCEDER A MUCHAS COSAS A UN NIVEL MUCHO MAS ABAJO
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
        while (_gameController.gameIsTerminated == false)
        {
            _gameController.roundIsTerminated = false;
            _player1UnitLooserName = "";
            _player2UnitLooserName = "";
            if (_gameController.currentAttacker == 0)
            {
                _numberUnitOfPlayer1 = AskPlayerForTheUnitNumber(0);
                _numberUnitOfPlayer2 = AskPlayerForTheUnitNumber(1);
                PrintRound(_gameController.currentAttacker);
                //ataque
                _player2UnitLooserName = _gameController.Attack(1, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
                _gameController.currentAttacker = 1;
                //contraataque
                _player1UnitLooserName = _gameController.Attack(2, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
                ActivateFollowupBonusOfPlayer(0);
                Followup();
                ResetUnitsBonus();
                ShowLeftoverHpPrintingPlayer1First();
                UpdateGameLogs();
                _gameController.currentAttacker = 1;
            }
            else if (_gameController.currentAttacker == 1)
            {
                _numberUnitOfPlayer2 = AskPlayerForTheUnitNumber(1);
                _numberUnitOfPlayer1 = AskPlayerForTheUnitNumber(0);
                _view.WriteLine("Round " + _gameController.currentRound + ": " + _gameController.players[1].units[_numberUnitOfPlayer2].nombre + " (Player 2) comienza");
                //ataque
                _player1UnitLooserName = _gameController.Attack(1, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
                _gameController.currentAttacker = 0;
                //contraataque
                _player2UnitLooserName = _gameController.Attack(2, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
                ActivateFollowupBonusOfPlayer(1);
                Followup();
                ResetUnitsBonus();
                ShowLeftoverHpPrintingPlayer2First();
                UpdateGameLogs();
                _gameController.currentAttacker = 0;
            }
            _gameController.currentRound++;
        }
        _view.WriteLine("Player " + (_gameController.winner + 1) + " ganó");
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
        int numberOfThePlayersUnit  = (playerNumber == 0) ? _numberUnitOfPlayer1 :  _numberUnitOfPlayer2;
        _view.WriteLine("Round " + _gameController.currentRound + ": " + _gameController.players[playerNumber].units[numberOfThePlayersUnit].nombre + " (Player " + playerNumberString + ") comienza");
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
            if (unit.nombre != "") _view.WriteLine(unitNumberCounter + ": " + unit.nombre);
            unitNumberCounter++;
        }
    }
    
    private void ActivateFollowupBonusOfPlayer(int playerNumber)
    {
        int numberOfThePlayersUnit  = (playerNumber == 0) ? _numberUnitOfPlayer1 :  _numberUnitOfPlayer2;
        _gameController.players[playerNumber].units[numberOfThePlayersUnit].ActiveBonusAndPenalties.attk += _gameController.players[playerNumber].units[numberOfThePlayersUnit].ActiveBonusAndPenalties.atkFollowup;
    }

    private void Followup()
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "" &&
            _gameController.players[1].units[_numberUnitOfPlayer2].spd + _gameController.players[1].units[_numberUnitOfPlayer2].ActiveBonusAndPenalties.spd >=
            5 + _gameController.players[0].units[_numberUnitOfPlayer1].spd + _gameController.players[0].units[_numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
        {
            _gameController.currentAttacker = 1;
            _player1UnitLooserName = _gameController.Attack(3, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
        }
        else if (_player1UnitLooserName == "" && _player2UnitLooserName == "" &&
                 _gameController.players[1].units[_numberUnitOfPlayer2].spd + _gameController.players[1].units[_numberUnitOfPlayer2].ActiveBonusAndPenalties.spd + 5 <=
                 _gameController.players[0].units[_numberUnitOfPlayer1].spd + _gameController.players[0].units[_numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
        {
            _gameController.currentAttacker = 0;
            _player2UnitLooserName = _gameController.Attack(3, _view, _numberUnitOfPlayer1, _numberUnitOfPlayer2);
        }
        else if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }
    
    private void ResetUnitsBonus()
    {
        _gameController.players[0].units[_numberUnitOfPlayer1].ActiveBonusAndPenalties.ResetBonusToZero();
        _gameController.players[1].units[_numberUnitOfPlayer2].ActiveBonusAndPenalties.ResetBonusToZero();
    }
    
    private void UpdateGameLogs()
    {
        UpdateLastOponent();
        UpdateAttacks();
    }

    private void UpdateAttacks()
    {
        _gameController.players[1].units[_numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
        _gameController.players[0].units[_numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
    }

    private void UpdateLastOponent()
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _gameController.players[1].units[_numberUnitOfPlayer2].gameLogs.LastOponentName = _gameController.players[0].units[_numberUnitOfPlayer1].nombre;
            _gameController.players[1].units[_numberUnitOfPlayer2].gameLogs.LastOponentName = _gameController.players[0].units[_numberUnitOfPlayer1].nombre;
        }
        else if (_player1UnitLooserName != "") _gameController.players[1].units[_numberUnitOfPlayer2].gameLogs.LastOponentName = _player1UnitLooserName;
        else if (_player2UnitLooserName != "") _gameController.players[0].units[_numberUnitOfPlayer1].gameLogs.LastOponentName = _player2UnitLooserName;
    }
    
    private void ShowLeftoverHpPrintingPlayer1First()
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _view.WriteLine(_gameController.players[0].units[_numberUnitOfPlayer1].nombre +
                            " (" + _gameController.players[0].units[_numberUnitOfPlayer1].hp_actual +
                            ") : " + _gameController.players[1].units[_numberUnitOfPlayer2].nombre +
                            " (" + _gameController.players[1].units[_numberUnitOfPlayer2].hp_actual +
                            ")");
        }
        else if (_player1UnitLooserName != "")
        {
            _view.WriteLine(_player1UnitLooserName +
                            " (0) : " + _gameController.players[1].units[_numberUnitOfPlayer2].nombre +
                            " (" + _gameController.players[1].units[_numberUnitOfPlayer2].hp_actual +
                            ")");
        }
        else
        {
            _view.WriteLine(_gameController.players[0].units[_numberUnitOfPlayer1].nombre +
                            " (" + _gameController.players[0].units[_numberUnitOfPlayer1].hp_actual +
                            ") : " + _player2UnitLooserName +
                            " (0)");
        }
    }
    
        private void ShowLeftoverHpPrintingPlayer2First()
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _view.WriteLine(_gameController.players[1].units[_numberUnitOfPlayer2].nombre +
                            " (" + _gameController.players[1].units[_numberUnitOfPlayer2].hp_actual +
                            ") : " + _gameController.players[0].units[_numberUnitOfPlayer1].nombre +
                            " (" + _gameController.players[0].units[_numberUnitOfPlayer1].hp_actual +
                            ")");
        }
        else if (_player1UnitLooserName != "")
        {
            _view.WriteLine(_gameController.players[1].units[_numberUnitOfPlayer2].nombre +
                            " (" + _gameController.players[1].units[_numberUnitOfPlayer2].hp_actual +
                            ") : " + _player1UnitLooserName +
                            " (0)");
        }
        else
        {
            _view.WriteLine(_player2UnitLooserName +
                            " (0) : " + _gameController.players[0].units[_numberUnitOfPlayer1].nombre +
                            " (" + _gameController.players[0].units[_numberUnitOfPlayer1].hp_actual +
                            ")");
        }
    }
}