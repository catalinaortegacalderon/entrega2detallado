namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Player _attackingPlayer;
    private Player _defensivePlayer;
    private int _numberOfAttackingUnit;
    private int _numberOfDefensiveUnit;
    private int _attackingPlayerNumber;
    private int _defensivePlayerNumber;
    public Player[] players = new Player[2];
    public int currentPlayer;
    public bool gameIsTerminated;
    public int currentRound = 1;
    public int winner = -1;
    public bool roundIsTerminated = false;
    
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
            // si pierde el jugador 0
            string nombre_perdedor1 = "";
            // si pierde el jugador 1
            string nombre_perdedor2 = "";
            if (juego_actual.currentPlayer == 0)
            {
                // esta atacando el primer jugador
                int contador1 = 0;
                int contador2 = 0;
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
                _view.WriteLine("Round " + juego_actual.currentRound + ": " +
                                juego_actual.players[0].units[valor1].nombre
                                + " (Player 1) comienza");
                //ataque
                nombre_perdedor2 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.currentPlayer = 1;
                //contraataque
                nombre_perdedor1 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                // seteando bonus followup
                juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.attk +=
                    juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.atkFollowup;
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.players[1].units[valor2].spd + juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.spd >=
                    5 + juego_actual.players[0].units[valor1].spd + juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.players[1].units[valor2].spd + juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.spd + 5 <=
                         juego_actual.players[0].units[valor1].spd + juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus (tambien se resetea el followup aqui)
                juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.ReestablecerBonusACero();

                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.players[0].units[valor1].nombre +
                                    " (" + juego_actual.players[0].units[valor1].hp_actual +
                                    ") : " + juego_actual.players[1].units[valor2].nombre +
                                    " (" + juego_actual.players[1].units[valor2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[valor1].nombre;
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[valor1].nombre;
                    juego_actual.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    juego_actual.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    _view.WriteLine(nombre_perdedor1 +
                                    " (0) : " + juego_actual.players[1].units[valor2].nombre +
                                    " (" + juego_actual.players[1].units[valor2].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName = nombre_perdedor1;
                    juego_actual.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    
                }
                else
                {
                    _view.WriteLine(juego_actual.players[0].units[valor1].nombre +
                                    " (" + juego_actual.players[0].units[valor1].hp_actual +
                                    ") : " + nombre_perdedor2 +
                                    " (0)");
                    // setear ultimo contrincante
                    juego_actual.players[0].units[valor1].gameLogs.LastOponentName = nombre_perdedor2;
                    juego_actual.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }

                juego_actual.currentPlayer = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int contador1 = 0;
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

                _view.WriteLine("Round " + juego_actual.currentRound + ": " +
                                juego_actual.players[1].units[valor2].nombre
                                + " (Player 2) comienza");
                
                //ataque
                //ImprimirVentajas();
                nombre_perdedor1 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.currentPlayer = 0;
                //contraataque
                nombre_perdedor2 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                // setear bonus followup
                
                // SETEANDO BONUS FOLLOW UP

                juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.attk +=
                    juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.atkFollowup;
                
                Console.WriteLine("IMPRIMIENDO ANTES DEL FOLLOWUP");
                Console.WriteLine(          "UNO" +           juego_actual.players[1].units[valor2].spd + "DOS" + juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.spd + "TRES" +
                                                       + juego_actual.players[0].units[valor1].spd + "CUATRO" + juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.spd);
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.players[1].units[valor2].spd + juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.spd >=
                    5 + juego_actual.players[0].units[valor1].spd + juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.players[1].units[valor2].spd + juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.spd + 5 <=
                         juego_actual.players[0].units[valor1].spd + juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.spd)
                {
                    juego_actual.currentPlayer = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus, tambien se resetea bonus followup de sandstorm
                juego_actual.players[0].units[valor1].ActiveBonusAndPenalties.ReestablecerBonusACero();
                juego_actual.players[1].units[valor2].ActiveBonusAndPenalties.ReestablecerBonusACero();
                
                // logs
                                
                
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.players[1].units[valor2].nombre +
                                    " (" + juego_actual.players[1].units[valor2].hp_actual +
                                    ") : " + juego_actual.players[0].units[valor1].nombre +
                                    " (" + juego_actual.players[0].units[valor1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[valor1].nombre;
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName =
                        juego_actual.players[0].units[valor1].nombre;
                    juego_actual.players[1].units[valor2].gameLogs.amountOfAttacks++;
                    juego_actual.players[0].units[valor1].gameLogs.amountOfAttacks++;
                }
                else if (nombre_perdedor1 != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(juego_actual.players[1].units[valor2].nombre +
                                    " (" + juego_actual.players[1].units[valor2].hp_actual +
                                    ") : " + nombre_perdedor1 +
                                    " (0)");
                    // setear ultimo contrincante
                    juego_actual.players[1].units[valor2].gameLogs.LastOponentName = nombre_perdedor1;
                    juego_actual.players[1].units[valor2].gameLogs.amountOfAttacks++;
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(nombre_perdedor2 +
                                    " (0) : " + juego_actual.players[0].units[valor1].nombre +
                                    " (" + juego_actual.players[0].units[valor1].hp_actual +
                                    ")");
                    // setear ultimo contrincante
                    juego_actual.players[0].units[valor1].gameLogs.LastOponentName = nombre_perdedor2;
                    juego_actual.players[0].units[valor1].gameLogs.amountOfAttacks++;
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
    
       public string atacar(int numero_ataque, View view, int unidad1, int unidad2)
    {
        string nombre_perdedor = "";
        //VOY A RETORNAR EL NOMBRE DE UNA UNIDAD SI ESQUE MUERE, SINO STRING VACIO
        if (this.gameIsTerminated || this.roundIsTerminated)
        {
            return "";
        }

        // EL NUMERO DE ATAQUE PUEDE SER 1-2-3 DEPENDIENDO SI ES ATAQUE, CONTRAATAQUE O FOLLOWUP
        // follow up lo hace el que tiene 5 puntos mas de speed que el otro
        bool imprimir = false;
        int ataque;
        if (numero_ataque == 1)
        {
            //imrpimo ventaja y luego activo habilidades
            imprimir = true;

        }

        if (currentPlayer == 0)
        {
            // PRIMER JUGADOR ATACA AL SEGUNDO
            // aca imprimo, tal vez pasarlo al true de antes    
            //int ataque = jugadores[0].calcular_atque(imprimir, view, unidad1, jugadores[1].unidades[unidad2]);
            //primero activar habilidades
            // revisar si son ambas, o solo el jugador atacante

            ataque = players[0].calcular_atque(imprimir, view, unidad1, players[1].units[unidad2]);
            if (numero_ataque == 1)
            {
                // activando habilidades atacante (primer jugador)
                foreach (Skill habilidad in players[0].units[unidad1].habilidades)
                {
                    habilidad.AplicarHabilidades(players[0].units[unidad1], players[1].units[unidad2], true);
                }

                //activando habilidades defensor (segundo jugador)
                foreach (Skill habilidad in players[1].units[unidad2].habilidades)
                {
                    habilidad.AplicarHabilidades(players[1].units[unidad2], players[0].units[unidad1], false);
                }
            }

            //me faltan las anulaciones
            // recalcular ataque con los cambios    
            ataque = players[0].calcular_atque(false, view, unidad1, players[1].units[unidad2]);
            view.WriteLine(players[0].units[unidad1].nombre +
                           " ataca a " +
                           players[1].units[unidad2].nombre + " con " +
                           ataque + " de daño");
            if (ataque >= players[1].units[unidad2].hp_actual)
            {
                //muere esta unidad
                nombre_perdedor = players[1].units[unidad2].nombre;
                this.roundIsTerminated = true;
                // ERROR: LISTAS ES COPIA POR REFERENCIA
                if (unidad2 == 0)
                {
                    players[1].units[0] = players[1].units[1];
                    players[1].units[1] = players[1].units[2];
                    players[1].units[2] = new Unit();
                }
                else if (unidad2 == 1)
                {
                    players[1].units[1] = players[1].units[2];
                    players[1].units[2] = new Unit();
                }
                else
                {
                    players[1].units[2] = new Unit();
                }

                players[1].amountOfUnits = players[1].amountOfUnits - 1;
                if (players[1].amountOfUnits == 0)
                {
                    this.gameIsTerminated = true;
                    this.winner = 0;
                    return nombre_perdedor;
                }

                return nombre_perdedor;
            }
            else
            {
                players[1].units[unidad2].hp_actual = players[1].units[unidad2].hp_actual - ataque;
            }

            return "";
        }
        // si el jugador que ataca es el 2
        else
        {
            // imrpimir
            ataque = players[1].calcular_atque(imprimir, view, unidad2, players[0].units[unidad1]);
            // activando habilidades atacante (jugador 2)
            if (numero_ataque == 1)
            {
                // aplicar habilidades
                //activando habilidades atacante (segundo jugador)
                foreach (Skill habilidad in players[1].units[unidad2].habilidades)
                {
                    habilidad.AplicarHabilidades(players[1].units[unidad2], players[0].units[unidad1], true);
                }

                // activando habilidades defensor (primer jugador)
                foreach (Skill habilidad in players[0].units[unidad1].habilidades)
                {
                    habilidad.AplicarHabilidades(players[0].units[unidad1], players[1].units[unidad2], false);
                }

            }
        }

        // recalcular ataque sin imprimir
        ataque = players[1].calcular_atque(false, view, unidad2, players[0].units[unidad1]);

        view.WriteLine(players[1].units[unidad2].nombre +
                       " ataca a " +
                       players[0].units[unidad1].nombre + " con " +
                       ataque + " de daño");
        if (ataque >= players[0].units[unidad1].hp_actual)
        {
            nombre_perdedor = players[0].units[unidad1].nombre;
            //muere esta unidad
            this.roundIsTerminated = true;
            if (unidad1 == 0)
            {
                players[0].units[0] = players[0].units[1];
                players[0].units[1] = players[0].units[2];
                players[0].units[2] = new Unit();
            }
            else if (unidad1 == 1)
            {
                players[0].units[1] = players[0].units[2];
                players[0].units[2] = new Unit();
            }
            else
            {
                players[0].units[2] = new Unit();
            }

            players[0].amountOfUnits = players[0].amountOfUnits - 1;
            if (players[0].amountOfUnits == 0)
            {
                this.gameIsTerminated = true;
                this.winner = 1;
                return nombre_perdedor;
            }

            return nombre_perdedor;
        }
        else
        {
            players[0].units[unidad1].hp_actual = players[0].units[unidad1].hp_actual - ataque;
        }

        return "";
    }
    
}