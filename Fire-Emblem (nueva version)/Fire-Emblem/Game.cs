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
        
        Juego juego_actual = Funciones.Construir_Juego(archivos[input], _view);
        while (juego_actual.terminado == false)
        {
            // si pierde el jugador 0
            string nombre_perdedor1 = "";
            // si pierde el jugador 1
            string nombre_perdedor2 = "";
            if (juego_actual.jugador_actual == 0)
            {
                // esta atacando el primer jugador
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[0].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 + ": " + unidad.nombre);
                        contador1++;
                    }
                }

                int valor1 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[1].unidades)
                {
                    //Console.WriteLine("pase por 3");
                    //Console.WriteLine("imprimiendo nombre de las unidades");
                    //Console.WriteLine(unidad.nombre);
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " + unidad.nombre);
                        contador2++;
                    }
                }

                int valor2 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Round " + juego_actual.ronda_actual + ": " +
                                juego_actual.jugadores[0].unidades[valor1].nombre
                                + " (Player 1) comienza");
                //ataque
                nombre_perdedor2 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.jugador_actual = 1;
                //contraataque
                nombre_perdedor1 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.jugadores[1].unidades[valor2].spd >=
                    5 + juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.jugadores[1].unidades[valor2].spd + 5 <=
                         juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }

                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ") : " + juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ")");
                }
                else if (nombre_perdedor1 != "")
                {
                    _view.WriteLine(nombre_perdedor1 +
                                    " (0) : " + juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ")");
                }
                else
                {
                    _view.WriteLine(juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ") : " + nombre_perdedor2 +
                                    " (0)");
                }

                juego_actual.jugador_actual = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[1].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " + unidad.nombre);
                        contador2++;
                    }
                }

                int valor2 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[0].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 + ": " + unidad.nombre);
                        contador1++;
                    }
                }

                int valor1 = Convert.ToInt32(_view.ReadLine());

                _view.WriteLine("Round " + juego_actual.ronda_actual + ": " +
                                juego_actual.jugadores[1].unidades[valor2].nombre
                                + " (Player 2) comienza");
                //ataque
                nombre_perdedor1 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.jugador_actual = 0;
                //contraataque
                nombre_perdedor2 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                    juego_actual.jugadores[1].unidades[valor2].spd + juego_actual.jugadores[1].unidades[valor2].BonusActivos.spd >=
                    5 + juego_actual.jugadores[0].unidades[valor1].spd + juego_actual.jugadores[0].unidades[valor1].BonusActivos.spd)
                {
                    juego_actual.jugador_actual = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" &&
                         juego_actual.jugadores[1].unidades[valor2].spd + 5 <=
                         juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                // resetear bonus
                juego_actual.jugadores[0].unidades[valor1].BonusActivos.ReestablecerBonusACero();
                juego_actual.jugadores[1].unidades[valor2].BonusActivos.ReestablecerBonusACero();
                                

                

                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ") : " + juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ")");
                }
                else if (nombre_perdedor1 != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ") : " + nombre_perdedor1 +
                                    " (0)");
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(nombre_perdedor2 +
                                    " (0) : " + juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ")");
                }

                juego_actual.jugador_actual = 0;
            }

            juego_actual.ronda_actual++;
            juego_actual.ronda_terminada = false;
        }

        _view.WriteLine("Player " + (juego_actual.ganador + 1) + " ganó");
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
        if (Funciones.Juego_Valido(archivos[input]) == false)
        {
            _view.WriteLine("Archivo de equipos no válido");
            return true;
        }
        return false;
    }

    // ojo que hay harto codigo repetido en juego valido y consruir juego tal vez dejar solo una funcion

    
}