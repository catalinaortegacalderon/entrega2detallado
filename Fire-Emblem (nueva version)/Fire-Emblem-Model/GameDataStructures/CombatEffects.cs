namespace ConsoleApp1.GameDataStructures;

public class CombatEffects
// todo: revisar este nombre
{
    public double HpRecuperationAtEveryAttack = 0;
    public double HpLostAtTheBeginning = 0;
    
    public double DamageAfterCombat = 0;
    
    public bool HasCounterAttackDenial = false;
    public bool HasNeutralizationOfCounterattackDenial = false;
    
    public bool HasGuaranteedFollowUp = false;
    public int AmountOfEffectsThatGuaranteeFollowup = 0;
    
    public bool HasDenialOfGuaranteedFollowUp = false;
    
    public bool HasFollowUpDenial = false;
    public bool HasDenialOfFollowUpDenial = false;
}