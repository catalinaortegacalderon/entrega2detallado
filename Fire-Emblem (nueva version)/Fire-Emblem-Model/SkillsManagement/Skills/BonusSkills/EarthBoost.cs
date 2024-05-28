using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class EarthBoost : Boost
{
    public EarthBoost() : base()
    {
        this.Effects[0] = new ChangeStatsInEffect( StatType.Def, 6); 
    }
}