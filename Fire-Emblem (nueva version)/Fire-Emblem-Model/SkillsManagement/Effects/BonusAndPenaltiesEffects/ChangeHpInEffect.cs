using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

public class ChangeHpInEffect : Effect
{
    public ChangeHpInEffect(int amount) : base()
    {
        this.Amount = amount;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (!myUnit.ActiveBonus.HpBonusActivated)
        {
            myUnit.CurrentHp = myUnit.CurrentHp + this.Amount; 
            myUnit.HpMax = myUnit.HpMax + this.Amount;
            myUnit.ActiveBonus.HpBonusActivated = true;
        }
    }
}