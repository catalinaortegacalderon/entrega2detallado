using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class WaterBoost : Boost
{
    public WaterBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Res, 6); 
    }
}