using System.Collections;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.EncapsulatedLists;

public class UnitsList : IEnumerable<Unit>
{
    private readonly List<Unit> _units = new() { new(), new(), new() };

    public IEnumerator<Unit> GetEnumerator()
    {
        foreach (var unit in _units)
            yield return unit;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _units.GetEnumerator();
    }

    // todo: expresion body
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
}