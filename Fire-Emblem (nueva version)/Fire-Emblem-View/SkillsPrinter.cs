using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;

public class SkillsPrinter
{
    // tal vez no hacer estatico
    public static void PrintAll(View view, Unit unit)
    {
        PrintBonus(view, unit);
        PrintPenalties(view, unit);
        PrintBonusNetralization(view, unit);
        PrintPenaltyNetralization(view, unit);
        PrintDamageEffects(view, unit);
    }

    private static void PrintBonus(View view, Unit unit)
    {
        if (unit.ActiveBonus.Attk > 0)
            view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.Attk);
        if (unit.ActiveBonus.Spd > 0) 
            view.WriteLine(unit.Name + " obtiene Spd+" + unit.ActiveBonus.Spd);
        if (unit.ActiveBonus.Def > 0) 
            view.WriteLine(unit.Name + " obtiene Def+" + unit.ActiveBonus.Def);
        if (unit.ActiveBonus.Res > 0) 
            view.WriteLine(unit.Name + " obtiene Res+" + unit.ActiveBonus.Res);
        if (unit.ActiveBonus.AtkFirstAttack > 0) 
            view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.AtkFirstAttack 
                           + " en su primer ataque");
        if (unit.ActiveBonus.DefFirstAttack > 0) 
            view.WriteLine(unit.Name + " obtiene Def+" + unit.ActiveBonus.DefFirstAttack 
                           + " en su primer ataque");
        if (unit.ActiveBonus.ResFirstAttack > 0) 
            view.WriteLine(unit.Name + " obtiene Res+" + unit.ActiveBonus.ResFirstAttack 
                           + " en su primer ataque");
        if (unit.ActiveBonus.AtkFollowup > 0) 
            view.WriteLine(unit.Name + " obtiene Atk+" + unit.ActiveBonus.AtkFollowup 
                           + " en su Follow-Up");
    }

    private static void PrintPenalties(View view, Unit unit)
    {
        if (unit.ActivePenalties.Attk < 0) 
            view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.Attk);
        if (unit.ActivePenalties.Spd < 0) 
            view.WriteLine(unit.Name + " obtiene Spd" + unit.ActivePenalties.Spd);
        if (unit.ActivePenalties.Def < 0) 
            view.WriteLine(unit.Name + " obtiene Def" + unit.ActivePenalties.Def);
        if (unit.ActivePenalties.Res < 0) 
            view.WriteLine(unit.Name + " obtiene Res" + unit.ActivePenalties.Res);
        if (unit.ActivePenalties.AtkFirstAttack < 0) 
            view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.AtkFirstAttack 
                           + " en su primer ataque");
        if (unit.ActivePenalties.DefFirstAttack < 0) 
            view.WriteLine(unit.Name + " obtiene Def" + unit.ActivePenalties.DefFirstAttack 
                           + " en su primer ataque");
        if (unit.ActivePenalties.ResFirstAttack < 0) 
            view.WriteLine(unit.Name + " obtiene Res" + unit.ActivePenalties.ResFirstAttack 
                           + " en su primer ataque");
        if (unit.ActivePenalties.AtkFollowup < 0) 
            view.WriteLine(unit.Name + " obtiene Atk" + unit.ActivePenalties.AtkFollowup 
                           + " en su Follow-Up");
    }

    private static void PrintBonusNetralization(View view, Unit unit)
    {
        if (unit.ActiveBonusNeutralizator.Attk == 0) 
            view.WriteLine("Los bonus de Atk de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralizator.Spd == 0) 
            view.WriteLine("Los bonus de Spd de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralizator.Def == 0) 
            view.WriteLine("Los bonus de Def de " + unit.Name + " fueron neutralizados");
        if (unit.ActiveBonusNeutralizator.Res == 0) 
            view.WriteLine("Los bonus de Res de " + unit.Name + " fueron neutralizados");
    }

    private static void PrintPenaltyNetralization(View view, Unit unit)
    {
        if (unit.ActivePenaltiesNeutralizator.Attk == 0) 
            view.WriteLine("Los penalty de Atk de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralizator.Spd == 0) 
            view.WriteLine("Los penalty de Spd de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralizator.Def == 0) 
            view.WriteLine("Los penalty de Def de " + unit.Name + " fueron neutralizados");
        if (unit.ActivePenaltiesNeutralizator.Res == 0) 
            view.WriteLine("Los penalty de Res de " + unit.Name + " fueron neutralizados");
    }

    private static void PrintDamageEffects(View view, Unit unit)
    {
        if (unit.DamageEffects.ExtraDamage != 0) 
            view.WriteLine(unit.Name + " realizará +" + unit.DamageEffects.ExtraDamage
            + " daño extra en cada ataque");
        if (unit.DamageEffects.ExtraDamageFirstAttack != 0) 
            view.WriteLine(unit.Name + " realizará +" + unit.DamageEffects.ExtraDamageFirstAttack
            + " daño extra en su primer ataque");
        if (unit.DamageEffects.ExtraDamageFollowup != 0) 
            view.WriteLine(unit.Name + " realizará +" + unit.DamageEffects.ExtraDamageFollowup
            + " daño extra en su Follow-Up");
        if (unit.DamageEffects.PercentageReduction != 1.000) 
            view.WriteLine(unit.Name + " reducirá el daño de los ataques del rival en un " 
            + (Math.Round(((1 - unit.DamageEffects.PercentageReduction)* 100)))
            + "%");
        if (unit.DamageEffects.PercentageReductionOpponentsFirstAttack != 1) 
            view.WriteLine(unit.Name + " reducirá el daño del primer ataque del rival en un " 
            + (Math.Round(((1 - unit.DamageEffects.PercentageReductionOpponentsFirstAttack)* 100)))
            + "%");
        if (unit.DamageEffects.PercentageReductionOpponentsFollowup != 1) 
            view.WriteLine(unit.Name + " reducirá el daño del Follow-Up del rival en un " 
            + (Math.Round((1 - unit.DamageEffects.PercentageReductionOpponentsFollowup)* 100))
            + "%");
        if (unit.DamageEffects.AbsolutDamageReduction != 0) 
            view.WriteLine(unit.Name + " recibirá " + unit.DamageEffects.AbsolutDamageReduction
            + " daño en cada ataque");
    }
}