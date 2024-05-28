using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class WindBoost : Boost
{
    public WindBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Spd, 6); 
    }
}