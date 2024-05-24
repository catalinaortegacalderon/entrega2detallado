using System.Collections;


namespace Fire_Emblem_Model.GameDataStructures.Lists;


public class UnitsList : IEnumerable<Unit>
{
    private List<Unit> _units = new List<Unit>(3);
    
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
        // Loop through each Unit in the Units list
        foreach (Unit unit in _units)
        {
            // Yield the current Unit for iteration
            yield return unit;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _units.GetEnumerator(); // Forward to the generic implementation
    }
    
}