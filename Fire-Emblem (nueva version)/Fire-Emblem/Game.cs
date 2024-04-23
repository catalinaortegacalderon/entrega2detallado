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
        
        GameContainer currentGameContainer = Functions.Construir_Juego(archivos[input], _view);
        while (currentGameContainer.gameIsTerminated == false)
        {
            // si pierde el jugador 0
            string nombre_perdedor1 = "";
            // si pierde el jugador 1
            string nombre_perdedor2 = "";
            if (currentGameContainer.currentPlayer == 0)
            {
                // esta atacando el primer jugador
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unit unidad in currentGameContainer.players[0].units)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 + ": " + unidad.nombre);
                        contador1++;
                    }
                }
                int valor1 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unit unidad in currentGameContainer.players[1].units)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " + unidad.nombre);
                        contador2++;
                    }
                }
                int valor2 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Round " + currentGameContainer.currentRound + ": " +
                                currentGameContainer.players[0].units[valor1].nombre
                                + " (Player 1) comienza");
                //ataque
                nombre_perdedor2 = currentGameContainer.atacar(1, _view, valor1, valor2);
                currentGameContainer.currentPlayer = 1;
                //contraataque
                nombre_perdedor1 = currentGameContainer.atacar(2, _view, valor1, valor2);
                // followup
                // seteando bonus followup
                currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.attk +=
                    currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.atkFollowup;
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    currentGameContainer.players[1].units[valor2].spd + currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.spd >=
                    5 + currentGameContainer.players[0].units[valor1].spd + currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    currentGameContainer.currentPlayer = 1;
                    nombre_perdedor1 = currentGameContainer.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         currentGameContainer.players[1].units[valor2].spd + currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.spd + 5 <=
                         currentGameContainer.players[0].units[valor1].spd + currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    currentGameContainer.currentPlayer = 0;
                    nombre_perdedor2 = currentGameContainer.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus (tambien se resetea el followup aqui)
                currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.ReestablecerBonusACero();

                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(currentGameContainer.players[0].units[valor1].nombre +
                                    " (" + currentGameContainer.players[0].units[valor1].hp_actual +
                                    ") : " + currentGameContainer.players[1].units[valor2].nombre +
                                    " (" + currentGameContainer.players[1].units[valor2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName =
                        currentGameContainer.players[0].units[valor1].nombre;
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName =
                        currentGameContainer.players[0].units[valor1].nombre;
                    currentGameContainer.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    currentGameContainer.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    _view.WriteLine(nombre_perdedor1 +
                                    " (0) : " + currentGameContainer.players[1].units[valor2].nombre +
                                    " (" + currentGameContainer.players[1].units[valor2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName = nombre_perdedor1;
                    currentGameContainer.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    
                }
                else
                {
                    _view.WriteLine(currentGameContainer.players[0].units[valor1].nombre +
                                    " (" + currentGameContainer.players[0].units[valor1].hp_actual +
                                    ") : " + nombre_perdedor2 +
                                    " (0)");
                    // setear ultimo contrincante
                    currentGameContainer.players[0].units[valor1].gameLogs.LastOponentName = nombre_perdedor2;
                    currentGameContainer.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }

                currentGameContainer.currentPlayer = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unit unidad in currentGameContainer.players[1].units)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " + unidad.nombre);
                        contador2++;
                    }
                }

                int valor2 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unit unidad in currentGameContainer.players[0].units)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 + ": " + unidad.nombre);
                        contador1++;
                    }
                }

                int valor1 = Convert.ToInt32(_view.ReadLine());

                _view.WriteLine("Round " + currentGameContainer.currentRound + ": " +
                                currentGameContainer.players[1].units[valor2].nombre
                                + " (Player 2) comienza");
                
                //ataque
                nombre_perdedor1 = currentGameContainer.atacar(1, _view, valor1, valor2);
                currentGameContainer.currentPlayer = 0;
                //contraataque
                nombre_perdedor2 = currentGameContainer.atacar(2, _view, valor1, valor2);
                // followup
                // setear bonus followup
                
                // SETEANDO BONUS FOLLOW UP

                currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.attk +=
                    currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.atkFollowup;
                
                Console.WriteLine("IMPRIMIENDO ANTES DEL FOLLOWUP");
                Console.WriteLine(          "UNO" +           currentGameContainer.players[1].units[valor2].spd + "DOS" + currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.spd + "TRES" +
                                                       + currentGameContainer.players[0].units[valor1].spd + "CUATRO" + currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.spd);
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    currentGameContainer.players[1].units[valor2].spd + currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.spd >=
                    5 + currentGameContainer.players[0].units[valor1].spd + currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    currentGameContainer.currentPlayer = 1;
                    nombre_perdedor1 = currentGameContainer.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         currentGameContainer.players[1].units[valor2].spd + currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.spd + 5 <=
                         currentGameContainer.players[0].units[valor1].spd + currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    currentGameContainer.currentPlayer = 0;
                    nombre_perdedor2 = currentGameContainer.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus, tambien se resetea bonus followup de sandstorm
                currentGameContainer.players[0].units[valor1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                currentGameContainer.players[1].units[valor2].ActiveBonusAndPenalties.ReestablecerBonusACero();
                
                // logs
                                
                
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(currentGameContainer.players[1].units[valor2].nombre +
                                    " (" + currentGameContainer.players[1].units[valor2].hp_actual +
                                    ") : " + currentGameContainer.players[0].units[valor1].nombre +
                                    " (" + currentGameContainer.players[0].units[valor1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName =
                        currentGameContainer.players[0].units[valor1].nombre;
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName =
                        currentGameContainer.players[0].units[valor1].nombre;
                    currentGameContainer.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    currentGameContainer.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(currentGameContainer.players[1].units[valor2].nombre +
                                    " (" + currentGameContainer.players[1].units[valor2].hp_actual +
                                    ") : " + nombre_perdedor1 +
                                    " (0)");
                    // setear ultimo contrincante
                    currentGameContainer.players[1].units[valor2].gameLogs.LastOponentName = nombre_perdedor1;
                    currentGameContainer.players[1].units[valor2].gameLogs.amountOfAttacks++;
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(nombre_perdedor2 +
                                    " (0) : " + currentGameContainer.players[0].units[valor1].nombre +
                                    " (" + currentGameContainer.players[0].units[valor1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    currentGameContainer.players[0].units[valor1].gameLogs.LastOponentName = nombre_perdedor2;
                    currentGameContainer.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }

                currentGameContainer.currentPlayer = 0;
            }

            currentGameContainer.currentRound++;
            currentGameContainer.roundIsTerminated = false;
        }

        _view.WriteLine("Player " + (currentGameContainer.winner + 1) + " ganó");
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

    // ojo que hay harto codigo repetido en juego valido y consruir juego tal vez dejar solo una funcion

    
}