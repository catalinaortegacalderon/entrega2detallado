using System.Collections;


namespace Fire_Emblem_Model.GameDataStructures.Lists;


public class UnitsList : IEnumerable<Unit>
{
    private List<Unit> _units = new List<Unit> { new Unit(), new Unit(), new Unit() };
    
    public void Construct()
    {
        // ver que hago aca 
    }

    public Unit GetUnitByIndex(int index)
    {
        return _units[index];
    }
    
    public void AddUnit(int index, Unit unit)
    {
        _units[index] = unit;
    }

    public void EliminateUnit(int index)
    {
        _units.RemoveAt(index);
    }
    
    public IEnumerator<Unit> GetEnumerator()
    {
        foreach (Unit unit in _units)
        {
            yield return unit;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _units.GetEnumerator();
    }
    
}