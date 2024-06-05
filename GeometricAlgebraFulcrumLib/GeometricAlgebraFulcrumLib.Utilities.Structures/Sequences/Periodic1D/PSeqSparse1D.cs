using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public class PSeqSparse1D<T>
    : IPeriodicSequence1D<T>
{
    private readonly Dictionary<int, T> _dataDictionary;


    public int Count { get; }

    public T DefaultValue { get; set; }

    public T this[int index]
    {
        get => _dataDictionary.TryGetValue(
            index.Mod(Count),
            out var value
        ) ? value : DefaultValue;
        set
        {
            index = index.Mod(Count);

            if (_dataDictionary.ContainsKey(index))
                _dataDictionary[index] = value;
            else
                _dataDictionary.Add(index, value);
        }
    }

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PSeqSparse1D(int count, IEnumerable<KeyValuePair<int, T>> dataDictionary)
    {
        Count = count;
        DefaultValue = default;
        _dataDictionary = dataDictionary.ToDictionary(
            p => p.Key, 
            p => p.Value
        );
    }

    public PSeqSparse1D(int count, T defaultValue, IEnumerable<KeyValuePair<int, T>> dataDictionary)
    {
        Count = count;
        DefaultValue = defaultValue;
        _dataDictionary = dataDictionary.ToDictionary(
            p => p.Key, 
            p => p.Value
        );
    }


    public PSeqSparse1D<T> Clear()
    {
        _dataDictionary.Clear();

        return this;
    }

    public PSeqSparse1D<T> SetToDefault(int index)
    {
        _dataDictionary.Remove(index.Mod(Count));

        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }
}