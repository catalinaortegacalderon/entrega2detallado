namespace Fire_Emblem;
using Fire_Emblem_View;

public class SkillsPrinter
{
    public static void PrintBonus(View view, Unit unit)
    {
        if (unit.activeBonus.attk > 0) view.WriteLine(unit.name + " obtiene Atk+" + unit.activeBonus.attk);
        if (unit.activeBonus.spd > 0) view.WriteLine(unit.name + " obtiene Spd+" + unit.activeBonus.spd);
        if (unit.activeBonus.def > 0) view.WriteLine(unit.name + " obtiene Def+" + unit.activeBonus.def);
        if (unit.activeBonus.res > 0) view.WriteLine(unit.name + " obtiene Res+" + unit.activeBonus.res);
        if (unit.activeBonus.atkFirstAttack > 0) view.WriteLine(unit.name + " obtiene Atk+" + unit.activeBonus.res + " en su primer ataque");
    }
    
    public static void PrintPenalties(View view, Unit unit)
    {
        if (unit.activePenalties.attk < 0) view.WriteLine(unit.name + " obtiene Atk" + unit.activePenalties.attk);
        if (unit.activePenalties.spd < 0) view.WriteLine(unit.name + " obtiene Spd" + unit.activePenalties.spd);
        if (unit.activePenalties.def < 0) view.WriteLine(unit.name + " obtiene Def" + unit.activePenalties.def);
        if (unit.activePenalties.res < 0) view.WriteLine(unit.name + " obtiene Res" + unit.activePenalties.res);
    }
    
    public static void PrintBonusNetralization(View view, Unit unit)
    {
        if (unit.activeBonusNeutralization.attk == 0) view.WriteLine("Los bonus de Atk de " + unit.name + " fueron neutralizados");
        if (unit.activeBonusNeutralization.spd == 0) view.WriteLine("Los bonus de Spd de " + unit.name + " fueron neutralizados");
        if (unit.activeBonusNeutralization.def == 0) view.WriteLine("Los bonus de Def de " + unit.name + " fueron neutralizados");
        if (unit.activeBonusNeutralization.res == 0) view.WriteLine("Los bonus de Res de " + unit.name + " fueron neutralizados");
    }
    
    public static void PrintPenaltyNetralization(View view, Unit unit)
    {
        if (unit.activePenaltiesNeutralization.attk == 0) view.WriteLine("Los penalty de Atk de " + unit.name + " fueron neutralizados");
        if (unit.activePenaltiesNeutralization.spd == 0) view.WriteLine("Los penalty de Spd de " + unit.name + " fueron neutralizados");
        if (unit.activePenaltiesNeutralization.def == 0) view.WriteLine("Los penalty de Def de " + unit.name + " fueron neutralizados");
        if (unit.activePenaltiesNeutralization.res == 0) view.WriteLine("Los penalty de Res de " + unit.name + " fueron neutralizados");
    }
}