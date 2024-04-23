namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Game
{
    private View _view;
    private string _teamsFolder;
    
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        if (CargarEquipos(out var archivos, out var input)) return;
        GameController juego_actual = Functions.Construir_Juego(archivos[input], _view);
        while (juego_actual.gameIsTerminated == false)
        {
            //juego_actual.MakeAnotherRound();
            //juego_actual.MakeAnotherRound();
            // si pierde el jugador 0
            string nombre_perdedor1 = "";
            // si pierde el jugador 1
            string nombre_perdedor2 = "";
            if (juego_actual.currentPlayer == 0)
            {
                // esta atacando el primer jugador
                int numberUnitOfPlayer1 = AskPlayer1ForTheUnitNumber(juego_actual);
                int numberUnitOfPlayer2 = AskPlayer2ForTheUnitNumber(juego_actual);
                _view.WriteLine("Round " + juego_actual.currentRound + ": " +
                                juego_actual.players[0].units[numberUnitOfPlayer1].nombre
                                + " (Player 1) comienza");
                //ataque
                nombre_perdedor2 = juego_actual.atacar(1, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                juego_actual.currentPlayer = 1;
                //contraataque
                nombre_perdedor1 = juego_actual.atacar(2, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                // followup
                // seteando bonus followup
                juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.attk +=
                    juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.atkFollowup;
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.players[1].units[numberUnitOfPlayer2].spd + juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd >=
                    5 + juego_actual.players[0].units[numberUnitOfPlayer1].spd + juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.players[1].units[numberUnitOfPlayer2].spd + juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd + 5 <=
                         juego_actual.players[0].units[numberUnitOfPlayer1].spd + juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus (tambien se resetea el followup aqui)
                juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.ReestablecerBonusACero();
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + juego_actual.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ") : " + juego_actual.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + juego_actual.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[numberUnitOfPlayer1].nombre;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[numberUnitOfPlayer1].nombre;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    _view.WriteLine(nombre_perdedor1 +
                                    " (0) : " + juego_actual.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + juego_actual.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = nombre_perdedor1;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                    
                }
                else
                {
                    _view.WriteLine(juego_actual.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + juego_actual.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ") : " + nombre_perdedor2 +
                                    " (0)");
                    // setear ultimo contrincante
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.LastOponentName = nombre_perdedor2;
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
                }

                juego_actual.currentPlayer = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int numberUnitOfPlayer2 = AskPlayer2ForTheUnitNumber(juego_actual);
                int numberUnitOfPlayer1 = AskPlayer1ForTheUnitNumber(juego_actual);

                _view.WriteLine("Round " + juego_actual.currentRound + ": " +
                                juego_actual.players[1].units[numberUnitOfPlayer2].nombre
                                + " (Player 2) comienza");
                
                //ataque
                //ImprimirVentajas();
                nombre_perdedor1 = juego_actual.atacar(1, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                juego_actual.currentPlayer = 0;
                //contraataque
                nombre_perdedor2 = juego_actual.atacar(2, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                // followup
                // SETEANDO BONUS FOLLOW UP
                juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.attk +=
                    juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.atkFollowup;
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.players[1].units[numberUnitOfPlayer2].spd + juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd >=
                    5 + juego_actual.players[0].units[numberUnitOfPlayer1].spd + juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.players[1].units[numberUnitOfPlayer2].spd + juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.spd + 5 <=
                         juego_actual.players[0].units[numberUnitOfPlayer1].spd + juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, numberUnitOfPlayer1, numberUnitOfPlayer2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus, tambien se resetea bonus followup de sandstorm
                juego_actual.players[0].units[numberUnitOfPlayer1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                juego_actual.players[1].units[numberUnitOfPlayer2].ActiveBonusAndPenalties.ReestablecerBonusACero();
                
                // logs
                                
                
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + juego_actual.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ") : " + juego_actual.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + juego_actual.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[numberUnitOfPlayer1].nombre;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[numberUnitOfPlayer1].nombre;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(juego_actual.players[1].units[numberUnitOfPlayer2].nombre +
                                    " (" + juego_actual.players[1].units[numberUnitOfPlayer2].hp_actual +
                                    ") : " + nombre_perdedor1 +
                                    " (0)");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.LastOponentName = nombre_perdedor1;
                    juego_actual.players[1].units[numberUnitOfPlayer2].gameLogs.amountOfAttacks++;
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(nombre_perdedor2 +
                                    " (0) : " + juego_actual.players[0].units[numberUnitOfPlayer1].nombre +
                                    " (" + juego_actual.players[0].units[numberUnitOfPlayer1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.LastOponentName = nombre_perdedor2;
                    juego_actual.players[0].units[numberUnitOfPlayer1].gameLogs.amountOfAttacks++;
                }

                juego_actual.currentPlayer = 0;
            }

            juego_actual.currentRound++;
            juego_actual.roundIsTerminated = false;
        }
        _view.WriteLine("Player " + (juego_actual.winner + 1) + " ganó");
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

    private int AskPlayer1ForTheUnitNumber(GameController juego_actual)
    {
        int contador1 = 0;
        _view.WriteLine("Player 1 selecciona una opción");
        foreach (Unit unidad in juego_actual.players[0].units)
        {
            if (unidad.nombre != "")
            {
                _view.WriteLine(contador1 + ": " + unidad.nombre);
                contador1++;
            }
        }
        int valor1 = Convert.ToInt32(_view.ReadLine());
        return valor1;
    }

    private int AskPlayer2ForTheUnitNumber(GameController juego_actual)
    {
        int contador2 = 0;
        _view.WriteLine("Player 2 selecciona una opción");
        foreach (Unit unidad in juego_actual.players[1].units)
        {
            if (unidad.nombre != "")
            {
                _view.WriteLine(contador2 + ": " + unidad.nombre);
                contador2++;
            }
        }
        int valor2 = Convert.ToInt32(_view.ReadLine());
        return valor2;
    }
}