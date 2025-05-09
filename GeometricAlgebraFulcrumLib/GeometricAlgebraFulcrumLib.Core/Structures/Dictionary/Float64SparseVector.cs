using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

public sealed class Float64SparseVector :
    IReadOnlyList<double>
{
    private readonly Dictionary<int, double> _numbersDictionary
        = new Dictionary<int, double>();


    public int Count { get; }

    public double this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return _numbersDictionary.TryGetValue(index, out var number) 
                ? number : 0d;
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            Debug.Assert(!double.IsNaN(value));

            if (value == 0)
                _numbersDictionary.Remove(index);

            else if (_numbersDictionary.ContainsKey(index))
                _numbersDictionary[index] = value;

            else
                _numbersDictionary.Add(index, value);
        }
    }

    public int StoredPairsCount 
        => _numbersDictionary.Count;

    public IEnumerable<int> StoredIndices 
        => _numbersDictionary.Keys;

    public IEnumerable<double> StoredNumbers 
        => _numbersDictionary.Values;

    public IEnumerable<KeyValuePair<int, double>> StoredPairs
        => _numbersDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseVector(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SparseVector(int count, Dictionary<int, double> numbersDictionary)
    {
        Count = count;
        _numbersDictionary = numbersDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _numbersDictionary.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseVector MapNumbers(Func<double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(scalar);

            if (scalar1 != 0)
                _numbersDictionary.Add(id, scalar1);
        }

        return new Float64SparseVector(Count, numbersDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseVector MapNumbers(Func<int, double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(id, scalar);

            if (scalar1 != 0)
                _numbersDictionary.Add(id, scalar1);
        }

        return new Float64SparseVector(Count, numbersDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
            yield return this[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}