namespace Fire_Emblem;
using Fire_Emblem_View;

public class SkillsPrinter
{
    public static void PrintBonus(View view, Unit unit)
    {
        if (unit.activeBonus.attk > 0) view.WriteLine(unit.nombre + " obtiene Atk+" + unit.activeBonus.attk);
        if (unit.activeBonus.spd > 0) view.WriteLine(unit.nombre + " obtiene Spd+" + unit.activeBonus.spd);
        if (unit.activeBonus.def > 0) view.WriteLine(unit.nombre + " obtiene Def+" + unit.activeBonus.def);
        if (unit.activeBonus.res > 0) view.WriteLine(unit.nombre + " obtiene Res+" + unit.activeBonus.res);
    }

}