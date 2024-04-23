namespace Fire_Emblem;
using Fire_Emblem_View;
public class Player
{
    public int amountOfUnits;
    public Unit[] units = new Unit[3];
    public String lastLooserUnit = "";
    public Unit currentUnit;
    public Player(int amountOfUnits, Unit[] units)
    {
        this.amountOfUnits = amountOfUnits;
        this.units = units;
    }

    public void SetUnitInUse(int numberOfUnit)
    {
        this.currentUnit = this.units[numberOfUnit];

    }
    public int calcular_atque(bool imprimir, View view, int numero_unidad, Unit unitContincante)
    {
        string arma_atac = this.units[numero_unidad].arma;
        string arma_def = unitContincante.arma;
        float def_o_res_rival;
        if (arma_atac == "Magic")
        {
            def_o_res_rival = unitContincante.res + unitContincante.ActiveBonusAndPenalties.res;
        }
        else
        {
            def_o_res_rival = unitContincante.def + unitContincante.ActiveBonusAndPenalties.def;
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
                view.WriteLine(this.units[numero_unidad].nombre + " (" +
                               this.units[numero_unidad].arma + ") tiene ventaja con respecto a " +
                               unitContincante.nombre + " (" +
                               unitContincante.arma + ")");
            }
            wtb = 1.2;
        }
        else
        {
            if (imprimir)
            { 
                view.WriteLine(unitContincante.nombre + " (" +
                                          unitContincante.arma + ") tiene ventaja con respecto a " +
                                          units[numero_unidad].nombre + " (" +
                                          units[numero_unidad].arma + ")");
            }
            wtb = 0.8;
        }
        int atk_unidad = this.units[numero_unidad].attk + this.units[numero_unidad].ActiveBonusAndPenalties.attk;
        if ((atk_unidad * wtb - def_o_res_rival) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(atk_unidad * wtb - def_o_res_rival));
    }
}