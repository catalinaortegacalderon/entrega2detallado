using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class DragonsWrathSecondEffect : Effect
{
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    { 
        int unitsAtk = myUnit.Atk + myUnit.ActiveBonus.Attk 
            * myUnit.ActiveBonusNeutralizator.Attk + myUnit.ActivePenalties.Attk 
            * myUnit.ActivePenaltiesNeutralizator.Attk;
        
        int rivalsRes = opponentsUnit.Res + opponentsUnit.ActiveBonus.Res 
            * opponentsUnit.ActiveBonusNeutralizator.Res + opponentsUnit.ActivePenalties.Res 
            * opponentsUnit.ActivePenaltiesNeutralizator.Res;
        
        int amount = Convert.ToInt32(Math.Truncate((unitsAtk - rivalsRes) * 0.25));
        myUnit.DamageEffects.ExtraDamageFirstAttack +=  amount;
    }
}

