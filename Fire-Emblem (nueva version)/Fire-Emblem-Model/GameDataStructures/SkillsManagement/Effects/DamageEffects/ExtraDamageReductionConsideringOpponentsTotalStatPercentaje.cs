
namespace Fire_Emblem_Model;

public class ExtraDamageReductionConsideringOpponentsTotalStatPercentaje: Effect
{
    private string _type;
    private string _stat;
    private double _percentage;
    
    public ExtraDamageReductionConsideringOpponentsTotalStatPercentaje( string type, string stat, double percentage) : base()
    {
        this._type = type;
        this._stat = stat;
        this._percentage = percentage;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit, bool attacking)
    {
        int amount = 0;
        if (this._stat == "Res")
        {
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralization.Res
                                  + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralization.Res;
        }
        if (this._stat == "Atk")
        {
            amount =
                opponentsUnit.Atk + opponentsUnit.ActiveBonus.Attk * opponentsUnit.ActiveBonusNeutralization.Attk
                                  + opponentsUnit.ActivePenalties.Attk * opponentsUnit.ActivePenaltiesNeutralization.Attk;
        }
        if (this._stat == "Def")
        {
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralization.Def
                                  + opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenaltiesNeutralization.Def;
        }
        if (this._stat == "Spd")
        {
            amount =
                opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralization.Spd
                                  + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralization.Spd;
        }
        amount = Convert.ToInt32(Math.Truncate(amount * this._percentage));
        if (this._type == "All")
        {
            myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + amount;
        }
        else if (this._type == "First Attack")
        {
            myUnit.DamageEffects.ExtraDamageFirstAttack= myUnit.DamageEffects.ExtraDamageFirstAttack + amount;
        }
        else if (this._type == "Followup")
        {
            myUnit.DamageEffects.ExtraDamageFollowup = myUnit.DamageEffects.ExtraDamageFollowup + amount;
        }
    }
}