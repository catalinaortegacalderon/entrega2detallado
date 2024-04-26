namespace Fire_Emblem;
using Fire_Emblem_View;

public class SkillsPrinter
{
    public static void PrintBonus(View view, Unit unit)
    {
        if (unit.ActiveBonus.attk > 0) view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.attk);
        if (unit.ActiveBonus.spd > 0) view.WriteLine(unit.Name + " obtiene Spd+" + unit.ActiveBonus.spd);
        if (unit.ActiveBonus.def > 0) view.WriteLine(unit.Name + " obtiene Def+" + unit.ActiveBonus.def);
        if (unit.ActiveBonus.res > 0) view.WriteLine(unit.Name + " obtiene Res+" + unit.ActiveBonus.res);
        if (unit.ActiveBonus.atkFirstAttack > 0) view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.atkFirstAttack + " en su primer ataque");
        if (unit.ActiveBonus.defFirstAttack > 0) view.WriteLine(unit.Name + " obtiene Def+" + unit.ActiveBonus.defFirstAttack + " en su primer ataque");
        if (unit.ActiveBonus.resFirstAttack > 0) view.WriteLine(unit.Name + " obtiene Res+" + unit.ActiveBonus.resFirstAttack + " en su primer ataque");
        if (unit.ActiveBonus.atkFollowup > 0) view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.atkFollowup + " en su Follow-Up");
    }
    
    public static void PrintPenalties(View view, Unit unit)
    {
        Console.WriteLine("penalty def first atack" + unit.ActiveBonus.defFirstAttack);
        Console.WriteLine("penalty res first atack" + unit.ActiveBonus.defFirstAttack);
        if (unit.ActivePenalties.attk < 0) view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.attk);
        if (unit.ActivePenalties.spd < 0) view.WriteLine(unit.Name + " obtiene Spd" + unit.ActivePenalties.spd);
        if (unit.ActivePenalties.def < 0) view.WriteLine(unit.Name + " obtiene Def" + unit.ActivePenalties.def);
        if (unit.ActivePenalties.res < 0) view.WriteLine(unit.Name + " obtiene Res" + unit.ActivePenalties.res);
        if (unit.ActivePenalties.atkFirstAttack < 0) view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.atkFirstAttack + " en su primer ataque");
        if (unit.ActivePenalties.defFirstAttack < 0) view.WriteLine(unit.Name + " obtiene Def" + unit.ActivePenalties.defFirstAttack + " en su primer ataque");
        if (unit.ActivePenalties.resFirstAttack < 0) view.WriteLine(unit.Name + " obtiene Res" + unit.ActivePenalties.resFirstAttack + " en su primer ataque");
        if (unit.ActivePenalties.atkFollowup < 0) view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.atkFollowup + " en su Follow-Up");
    }
    
    public static void PrintBonusNetralization(View view, Unit unit)
    {
        if (unit.ActiveBonusNeutralization.attk == 0) view.WriteLine("Los bonus de Atk de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralization.spd == 0) view.WriteLine("Los bonus de Spd de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralization.def == 0) view.WriteLine("Los bonus de Def de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralization.res == 0) view.WriteLine("Los bonus de Res de " + unit.Name + " fueron neutralizados");
    }
    
    public static void PrintPenaltyNetralization(View view, Unit unit)
    {
        if (unit.ActivePenaltiesNeutralization.attk == 0) view.WriteLine("Los penalty de Atk de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralization.spd == 0) view.WriteLine("Los penalty de Spd de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralization.def == 0) view.WriteLine("Los penalty de Def de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralization.res == 0) view.WriteLine("Los penalty de Res de " + unit.Name + " fueron neutralizados");
    }
}