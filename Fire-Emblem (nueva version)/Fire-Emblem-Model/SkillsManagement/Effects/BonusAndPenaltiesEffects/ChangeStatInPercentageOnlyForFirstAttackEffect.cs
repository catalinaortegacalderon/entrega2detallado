namespace Fire_Emblem_Model;

public class ChangeStatInPercentageOnlyForFirstAttackEffect : Effect
{
    private String _stat;
    private double _percentage;
    public ChangeStatInPercentageOnlyForFirstAttackEffect(String stat, Double percentage) : base()
    {
        this._stat = stat;
        this._percentage = percentage;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        Amount = 0;
        if (_stat == "Atk")
        {
            Amount = Convert.ToInt32(Math.Truncate(myUnit.Atk * this._percentage));
            myUnit.ActiveBonus.AtkFirstAttack  += Amount;
        }
    }
}