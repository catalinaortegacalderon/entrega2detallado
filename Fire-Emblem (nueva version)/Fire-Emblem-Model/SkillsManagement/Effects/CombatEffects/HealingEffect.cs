using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.CombatEffects;

public class HealingEffect: Effect
{
    private double percentage;
    
    public HealingEffect(double percentage)
    {
        this.percentage = percentage;
    }
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        myUnit.CombatEffects.HpRecuperation += percentage;
    }
}