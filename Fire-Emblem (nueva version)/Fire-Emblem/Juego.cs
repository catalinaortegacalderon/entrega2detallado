namespace Fire_Emblem;
using Fire_Emblem_View;

public class Juego
{
    public Jugador[] jugadores = new Jugador[2];
    public int jugador_actual;
    public bool terminado;
    public int ronda_actual = 1;
    public int ganador = -1;
    public string ultima_unidad_perdida = "";
    public bool ronda_terminada = false;
    public Juego(Jugador jugador1, Jugador jugador2)
    {
        this.jugador_actual = 0;
        this.jugadores[0] = jugador1;
        this.jugadores[1] = jugador2;
        this.terminado = false;
    }
    public string atacar(int numero_ataque, View view, int unidad1, int unidad2)
    {
        string nombre_perdedor = "";
        //VOY A RETORNAR EL NOMBRE DE UNA UNIDAD SI ESQUE MUERE, SINO STRING VACIO
        if (this.terminado || this.ronda_terminada)
        {
            return "";
        }
        //view es para hacer los outputs
        // EL NUMERO DE ATAQUE PUEDE SER 1-2-3 DEPENDIENDO SI ES ATAQUE, CONTRAATAQUE O FOLLOWUP
        // follow up lo hace el que tiene 5 puntos mas de speed que el otro
        bool imprimir = false;
        if (numero_ataque == 1)
        {
            imprimir = true;
        }
        if (jugador_actual == 0)
        {
            // PRIMER JUGADOR ATACA AL SEGUNDO
            // aca imprimo, tal vez pasarlo al true de antes    
            int ataque = jugadores[0].calcular_atque(imprimir, view, unidad1, jugadores[1].unidades[unidad2]);
            //primero activar habilidades
            // revisar si son ambas, o solo el jugador atacante
            
            // activando habilidades atacante (primer jugador)
            //foreach ( Habilidades habilidad in jugadores[0].unidades[unidad1].habilidades)
            //{
            //    if (habilidad.ChequearCondiciones(jugadores[0].unidades[unidad1],jugadores[1].unidades[unidad2],true))
            //    {
            //        habilidad.aplicar_cambios(jugadores[0].unidades[unidad1],jugadores[1].unidades[unidad2],true);
            //    }
            //        
            //}
            
            // activando habilidades defensor (segundo jugador)
            //foreach ( Habilidades habilidad in jugadores[1].unidades[unidad2].habilidades)
            //{
            //    if (habilidad.ChequearCondiciones(jugadores[1].unidades[unidad2],jugadores[0].unidades[unidad1],false))
            //    {
            //        habilidad.aplicar_cambios(jugadores[1].unidades[unidad2],jugadores[0].unidades[unidad1],false);
            //    }
                    
            //}
            
            //me faltan las anulaciones
                
            // recalcular ataque con los cambios    
            ataque = jugadores[0].calcular_atque(false, view, unidad1, jugadores[1].unidades[unidad2]);
            view.WriteLine(jugadores[0].unidades[unidad1].nombre +
                           " ataca a " +
                           jugadores[1].unidades[unidad2].nombre + " con "+
                           ataque + " de daño");
            if (ataque >= jugadores[1].unidades[unidad2].hp_actual)
            {
                //muere esta unidad
                nombre_perdedor = jugadores[1].unidades[unidad2].nombre;
                this.ronda_terminada = true;
                // ERROR: LISTAS ES COPIA POR REFERENCIA
                if (unidad2 == 0)
                {
                    jugadores[1].unidades[0] = jugadores[1].unidades[1];
                    jugadores[1].unidades[1] = jugadores[1].unidades[2];
                    jugadores[1].unidades[2] = new Unidad();
                }
                else if (unidad2 == 1)
                {
                    jugadores[1].unidades[1] = jugadores[1].unidades[2];
                    jugadores[1].unidades[2] = new Unidad();
                }
                else
                {
                    jugadores[1].unidades[2] = new Unidad();
                }
                jugadores[1].cantidad_unidades = jugadores[1].cantidad_unidades -1;
                if(jugadores[1].cantidad_unidades == 0)
                {
                    this.terminado = true;
                    this.ganador = 0;
                    return nombre_perdedor;
                }
                return nombre_perdedor;
            }
            else
            {
                jugadores[1].unidades[unidad2].hp_actual = jugadores[1].unidades[unidad2].hp_actual - ataque;
            }
            return "";
        }
        // si el jugador que ataca es el 2
        else
        {
            int ataque = jugadores[1].calcular_atque(imprimir, view, unidad2, jugadores[0].unidades[unidad1]);
            view.WriteLine(jugadores[1].unidades[unidad2].nombre +
                           " ataca a " +
                           jugadores[0].unidades[unidad1].nombre + " con "+
                           ataque + " de daño");
            if (ataque >= jugadores[0].unidades[unidad1].hp_actual)
            {
                nombre_perdedor = jugadores[0].unidades[unidad1].nombre;
                //muere esta unidad
                this.ronda_terminada = true;
                if (unidad1 == 0)
                {
                    jugadores[0].unidades[0] = jugadores[0].unidades[1];
                    jugadores[0].unidades[1] = jugadores[0].unidades[2];
                    jugadores[0].unidades[2] = new Unidad();
                }
                else if (unidad1 == 1)
                {
                    jugadores[0].unidades[1] = jugadores[0].unidades[2];
                    jugadores[0].unidades[2] = new Unidad();
                }
                else
                {
                    jugadores[0].unidades[2] = new Unidad();
                }
                jugadores[0].cantidad_unidades = jugadores[0].cantidad_unidades -1;
                if(jugadores[0].cantidad_unidades == 0)
                {
                    this.terminado = true;
                    this.ganador = 1;
                    return nombre_perdedor;
                }
                return nombre_perdedor;
            }
            else
            {
                jugadores[0].unidades[unidad1].hp_actual = jugadores[0].unidades[unidad1].hp_actual - ataque;
            }
            return "";
        }
        
    }
}