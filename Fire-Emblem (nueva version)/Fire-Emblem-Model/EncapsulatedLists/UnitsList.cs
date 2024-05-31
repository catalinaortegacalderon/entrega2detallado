using System.Collections;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.EncapsulatedLists;


public class UnitsList : IEnumerable<Unit>
{
    private List<Unit> _units = new List<Unit> { new Unit(), new Unit(), new Unit() };
    // todo: expresion body
    public Unit GetUnitByIndex(int index) => _units[index];

    public void AddUnit(int index, Unit unit) => _units[index] = unit;

    public void EliminateUnit(int index) 
        => _units.RemoveAt(index);

    public IEnumerator<Unit> GetEnumerator()
    {
        foreach (Unit unit in _units)
            yield return unit;
    }

    IEnumerator IEnumerable.GetEnumerator() => _units.GetEnumerator();
}