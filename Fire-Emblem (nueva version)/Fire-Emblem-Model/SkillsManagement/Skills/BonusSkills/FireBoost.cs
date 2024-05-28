using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class FireBoost : Boost
{
    public FireBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
    }
}