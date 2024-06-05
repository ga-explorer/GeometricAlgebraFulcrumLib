using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists;

public class ProListChainedLists<TValue> :
    IPeriodicReadOnlyList<TValue>
{
    private readonly List<IPeriodicReadOnlyList<TValue>> _laticesList
        = new List<IPeriodicReadOnlyList<TValue>>();


    public int Count 
        => _laticesList.Sum(lattice => lattice.Count);

    public TValue this[int index]
    {
        get
        {
            index = index.Mod(Count);
            foreach (var sourceLattice in _laticesList)
            {
                if (index < sourceLattice.Count)
                    return sourceLattice[index];

                index -= sourceLattice.Count;
            }

            //This should never happen
            throw new InvalidOperationException();
        }
    }

        
    public ProListChainedLists<TValue> AppendLattice(IPeriodicReadOnlyList<TValue> sourceLattice)
    {
        _laticesList.Add(sourceLattice);

        return this;
    }

    public ProListChainedLists<TValue> PrependLattice(IPeriodicReadOnlyList<TValue> sourceLattice)
    {
        _laticesList.Insert(0, sourceLattice);

        return this;
    }

    public ProListChainedLists<TValue> PrependLattice(IPeriodicReadOnlyList<TValue> sourceLattice, int index)
    {
        _laticesList.Insert(index, sourceLattice);

        return this;
    }

    public ProListChainedLists<TValue> RemoveLattice(int index)
    {
        _laticesList.RemoveAt(index);

        return this;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        return _laticesList
            .SelectMany(lattice => lattice)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}