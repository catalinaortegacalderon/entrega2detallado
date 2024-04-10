namespace Fire_Emblem;
using Fire_Emblem_View;
public class Jugador
{
    // SI UN UNIDAD MUERE, MOVER LAS UNIDADES HACIA LA IZQUIERDA
    public int cantidad_unidades;
    // el equipo  del jugador esta conformado por unidades (max 3):
    public Unidad[] unidades = new Unidad[3];
    public Jugador(int cant_unidades, Unidad[] unidades)
    {
        this.cantidad_unidades = cant_unidades;
        this.unidades = unidades;
    }
    public int calcular_atque(bool imprimir, View view, int numero_unidad, Unidad unidad_contincante)
    {
        string arma_atac = this.unidades[numero_unidad].arma;
        string arma_def = unidad_contincante.arma;
        float def_o_res_rival;
        if (arma_atac == "Magic")
        {
            def_o_res_rival = unidad_contincante.res;
        }
        else
        {
            def_o_res_rival = unidad_contincante.def;
        }
        double wtb;
        if (arma_def == arma_atac || arma_atac == "Magic" || arma_def == "Magic"
            || arma_def == "Bow" || arma_atac == "Bow")
        {
            if (imprimir)
            { 
                view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
            }
            wtb = 1;
        }
        else if ( (arma_atac == "Sword" & arma_def == "Axe") ||
                  (arma_atac == "Lance" & arma_def == "Sword") ||
                  (arma_atac == "Axe" & arma_def == "Lance") )
        {
            if (imprimir)
            {
                view.WriteLine(this.unidades[numero_unidad].nombre + " (" +
                               this.unidades[numero_unidad].arma + ") tiene ventaja con respecto a " +
                               unidad_contincante.nombre + " (" +
                               unidad_contincante.arma + ")");
            }
            wtb = 1.2;
        }
        else
        {
            if (imprimir)
            { 
                view.WriteLine(unidad_contincante.nombre + " (" +
                                          unidad_contincante.arma + ") tiene ventaja con respecto a " +
                                          unidades[numero_unidad].nombre + " (" +
                                          unidades[numero_unidad].arma + ")");
            }
            wtb = 0.8;
        }
        int atk_unidad = this.unidades[numero_unidad].attk;
        return Convert.ToInt32(Math.Truncate(atk_unidad * wtb - def_o_res_rival));
    }
}