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
    
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        if (CargarEquipos(out var archivos, out var input)) return;
        _gameController = Functions.Construir_Juego(archivos[input], _view);
        while (_gameController.gameIsTerminated == false)
        {
            _player1UnitLooserName = "";
            _player2UnitLooserName = "";
            if (_gameController.currentPlayer == 0)
            {
                int numberUnitOfPlayer1 = AskPlayerForTheUnitNumber(_gameController, 0);
                int numberUnitOfPlayer2 = AskPlayerForTheUnitNumber(_gameController, 1);
                _view.WriteLine("Round " + _gameController.currentRound + ": " +
                                _gameController.players[0].units[numberUnitOfPlayer1].nombre
                                + " (Player 1) comienza");
                //ataque
                _player2UnitLooserName = _gameController.atacar(1, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                _gameController.currentPlayer = 1;
                //contraataque
                _player1UnitLooserName = _gameController.atacar(2, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                // followup
                // seteando bonus followup
                _gameController.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.attk += _gameController.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.atkFollowup;
                Followup(_gameController, numberUnitOfPlayer2, numberUnitOfPlayer1);
                // resetear bonus (tambien se resetea el followup aqui)
                _gameController.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                _gameController.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.ReestablecerBonusACero();
                //mostrar hp restante de cada unidad
                if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
                {
                    _view.WriteLine(_gameController.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + _gameController.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ") : " + _gameController.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + _gameController.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ")");
                }
                else if (_player1UnitLooserName != "")
                {
                    _view.WriteLine(_player1UnitLooserName +
                                    " (0) : " + _gameController.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + _gameController.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ")");
                }
                else
                {
                    _view.WriteLine(_gameController.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + _gameController.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ") : " + _player2UnitLooserName +
                                    " (0)");
                }
                UpdateGameLogs(_gameController, numberUnitOfPlayer2, numberUnitOfPlayer1);

                _gameController.currentPlayer = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int numberUnitOfPlayer2 = AskPlayerForTheUnitNumber(_gameController, 1);
                int numberUnitOfPlayer1 = AskPlayerForTheUnitNumber(_gameController, 0);
                _view.WriteLine("Round " + _gameController.currentRound + ": " + _gameController.players[1].units[numberUnitOfPlayer2].nombre + " (Player 2) comienza");
                //ataque
                _player1UnitLooserName = _gameController.atacar(1, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                _gameController.currentPlayer = 0;
                //contraataque
                _player2UnitLooserName = _gameController.atacar(2, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                // followup
                // SETEANDO BONUS FOLLOW UP
                _gameController.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.attk += _gameController.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.atkFollowup;
                Followup(_gameController, numberUnitOfPlayer2, numberUnitOfPlayer1);
                // resetear bonus, tambien se resetea bonus followup de sandstorm
                
                ResetUnitsBonus(_gameController, numberUnitOfPlayer1, numberUnitOfPlayer2);

                // logs
                //mostrar hp restante de cada unidad
                if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
                {
                    _view.WriteLine(_gameController.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + _gameController.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ") : " + _gameController.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + _gameController.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        _gameController.players[0].units[numberUnitOfPlayer1].nombre;
                    _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        _gameController.players[0].units[numberUnitOfPlayer1].nombre;
                    _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                    _gameController.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
                }
                else if (_player1UnitLooserName != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(_gameController.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + _gameController.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ") : " + _player1UnitLooserName +
                                    " (0)");
                    // setear ultimo contrincante
                    _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = _player1UnitLooserName;
                    _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(_player2UnitLooserName +
                                    " (0) : " + _gameController.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + _gameController.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ")");
                }
                UpdateGameLogs(_gameController, numberUnitOfPlayer2, numberUnitOfPlayer1);
                _gameController.currentPlayer = 0;
            }

            _gameController.currentRound++;
            _gameController.roundIsTerminated = false;
        }
        _view.WriteLine("Player " + (_gameController.winner + 1) + " ganó");
    }

    private static void ResetUnitsBonus(GameController juego_actual, int numberUnitOfPlayer1, int numberUnitOfPlayer2)
    {
        juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.ReestablecerBonusACero();
        juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.ReestablecerBonusACero();
    }

    private bool CargarEquipos(out string[] archivos, out int input)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        archivos = Directory.GetFiles(_teamsFolder);
        int contador = 0;
        foreach (string archivo in archivos)
        {
            _view.WriteLine(contador + ": " + Path.GetFileName(archivo));
            contador++;
        }

        input = Convert.ToInt32(_view.ReadLine());
        if (Functions.Juego_Valido(archivos[input]) == false)
        {
            _view.WriteLine("Archivo de equipos no válido");
            return true;
        }
        return false;
    }

    private int AskPlayerForTheUnitNumber(GameController gameController, int playerNumber)
    {
        int unitNumberCounter = 0;
        string playerNumberString  = (playerNumber == 0) ? "1" :  "2";
        _view.WriteLine("Player "+ playerNumberString+ " selecciona una opción");
        foreach (Unit unit in gameController.players[playerNumber].units)
        {
            if (unit.nombre != "") _view.WriteLine(unitNumberCounter + ": " + unit.nombre);
            unitNumberCounter++;
        }
        int chosenUnit = Convert.ToInt32(_view.ReadLine());
        return chosenUnit;
    }
    
    private void Followup(int numberUnitOfPlayer2, int numberUnitOfPlayer1)
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "" &&
            _gameController.players[1].units[numberUnitOfPlayer2].spd + _gameController.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd >=
            5 + _gameController.players[0].units[numberUnitOfPlayer1].spd + _gameController.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
        {
            _gameController.currentPlayer = 1;
            _player1UnitLooserName = _gameController.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
        }
        else if (_player1UnitLooserName == "" && _player2UnitLooserName == "" &&
                 _gameController.players[1].units[numberUnitOfPlayer2].spd + _gameController.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd + 5 <=
                 _gameController.players[0].units[numberUnitOfPlayer1].spd + _gameController.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
        {
            _gameController.currentPlayer = 0;
            _player2UnitLooserName = _gameController.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
        }
        else if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }
    
    // ELIMINAR DESPUES ESTOS TRENES QUE ROMPEN CLEAN CODE
    
    private void UpdateGameLogs(int numberUnitOfPlayer2, int numberUnitOfPlayer1)
    {
        if (_player1UnitLooserName == "" && _player2UnitLooserName == "")
        {
            _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = _gameController.players[0].units[numberUnitOfPlayer1].nombre;
            _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = _gameController.players[0].units[numberUnitOfPlayer1].nombre;
        }
        else if (_player1UnitLooserName != "") _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = _player1UnitLooserName;
        else if (_player2UnitLooserName != "") _gameController.players[0].units[numberUnitOfPlayer1].gameLogs.LastOponentName = _player2UnitLooserName;
        _gameController.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
        _gameController.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
    }
}