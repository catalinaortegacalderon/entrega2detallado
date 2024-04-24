namespace Fire_Emblem;
using Fire_Emblem_View;

public class GameController
{
    public Player[] players = new Player[2];
    public int currentAttacker;
    public bool gameIsTerminated;
    public int currentRound = 1;
    public int winner = -1;
    public bool roundIsTerminated = false;
    // arreglos: hacer variables privadas (setter y getter)
    //  ver si dejo lineas largas o con enter
    // ver si dejo view como atributo o solo como param de funcion
    // IDEA: SOLO ATACKING PLAYER Y DEFENSE PLAYER, SACAR CURRENT PLAYER

    public GameController(Player jugador1, Player jugador2)
    {
        this.currentAttacker = 0;
        this.players[0] = jugador1;
        this.players[1] = jugador2;
        this.gameIsTerminated = false;
    }

    public string Attack(int numero_ataque, View view, int unidad1, int unidad2)
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
        int attackValue;
        if (numero_ataque == 1)
        {
            
            //imrpimo ventaja y luego activo habilidades
            imprimir = true;

        }

        if (currentAttacker == 0)
        {
            // PRIMER JUGADOR ATACA AL SEGUNDO
            // aca imprimo, tal vez pasarlo al true de antes    
            //int ataque = jugadores[0].calcular_atque(imprimir, view, unidad1, jugadores[1].unidades[unidad2]);
            //primero activar habilidades
            // revisar si son ambas, o solo el jugador atacante

            //ataque = players[0].calcular_atque(imprimir, view, unidad1, players[1].units[unidad2]);
            //ataque = CalcularAtaque(players[0].units[unidad1], players[1].units[unidad2]);
            if (numero_ataque == 1)
            {
                ImprimirVentajas(view, players[0].units[unidad1], players[1].units[unidad2]);
                
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
            attackValue = CalcularAtaque(players[0].units[unidad1], players[1].units[unidad2]);
            view.WriteLine(players[0].units[unidad1].nombre +
                           " ataca a " +
                           players[1].units[unidad2].nombre + " con " +
                           attackValue + " de daño");
            if (attackValue >= players[1].units[unidad2].hp_actual)
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
                players[1].units[unidad2].hp_actual = players[1].units[unidad2].hp_actual - attackValue;
            }

            return "";
        }
        // si el jugador que ataca es el 2
        else
        {
            // imrpimir
            //ataque = players[1].calcular_atque(imprimir, view, unidad2, players[0].units[unidad1]);
            // activando habilidades atacante (jugador 2)
            if (numero_ataque == 1)
            {
                ImprimirVentajas(view, players[1].units[unidad2], players[0].units[unidad1]);
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
        //ataque = players[1].calcular_atque(false, view, unidad2, players[0].units[unidad1]);
        attackValue = CalcularAtaque(players[1].units[unidad2], players[0].units[unidad1]);

        view.WriteLine(players[1].units[unidad2].nombre +
                       " ataca a " +
                       players[0].units[unidad1].nombre + " con " +
                       attackValue + " de daño");
        if (attackValue >= players[0].units[unidad1].hp_actual)
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
            players[0].units[unidad1].hp_actual = players[0].units[unidad1].hp_actual - attackValue;
        }

        return "";
    }

    public int CalcularAtaque(Unit atacckingUnit, Unit defenseUnit)
    {
        string arma_atac = atacckingUnit.arma;
        string arma_def = defenseUnit.arma;
        int def_o_res_rival;
        if (arma_atac == "Magic") def_o_res_rival = defenseUnit.res + defenseUnit.ActiveBonusAndPenalties.res;
        else
        {
            def_o_res_rival = defenseUnit.def + defenseUnit.ActiveBonusAndPenalties.def;
        }
        double wtb;
        if (arma_def == arma_atac || arma_atac == "Magic" || arma_def == "Magic" || arma_def == "Bow" || arma_atac == "Bow") wtb = 1;
        else if ((arma_atac == "Sword" & arma_def == "Axe") || (arma_atac == "Lance" & arma_def == "Sword") || (arma_atac == "Axe" & arma_def == "Lance")) wtb = 1.2;
        else
        {
            //como poner esto en 1 sola linea
            wtb = 0.8;
        }
        int atk_unidad = atacckingUnit.attk + atacckingUnit.ActiveBonusAndPenalties.attk;
        if ((atk_unidad * wtb - def_o_res_rival) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(atk_unidad * wtb - def_o_res_rival));
    }

    public void ImprimirVentajas(View view, Unit atacckingUnit, Unit defenseUnit)
    {
        string arma_atac = atacckingUnit.arma;
        string arma_def = defenseUnit.arma;
        if (arma_def == arma_atac || arma_atac == "Magic" || arma_def == "Magic"
            || arma_def == "Bow" || arma_atac == "Bow")
        {
            view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
        else if ((arma_atac == "Sword" & arma_def == "Axe") ||
                 (arma_atac == "Lance" & arma_def == "Sword") ||
                 (arma_atac == "Axe" & arma_def == "Lance"))
        {
            view.WriteLine(atacckingUnit.nombre + " (" +
                           atacckingUnit.arma + ") tiene ventaja con respecto a " +
                           defenseUnit.nombre + " (" +
                           defenseUnit.arma + ")");
        }
        else
        {
            view.WriteLine(defenseUnit.nombre + " (" +
                           defenseUnit.arma + ") tiene ventaja con respecto a " +
                           atacckingUnit.nombre + " (" +
                           atacckingUnit.arma + ")");
        }
    }
}