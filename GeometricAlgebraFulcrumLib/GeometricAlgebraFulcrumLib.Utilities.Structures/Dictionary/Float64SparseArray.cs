using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

public sealed class Float64SparseArray : 
    IReadOnlyCollection<double>
{
    private readonly Dictionary<Pair<int>, double> _numbersDictionary
        = new Dictionary<Pair<int>, double>();


    public int Count1 { get; }

    public int Count2 { get; }

    public int Count 
        => Count1 * Count2;

    public double this[int index1, int index2]
    {
        get
        {
            if (index1 < 0 || index1 >= Count1)
                throw new IndexOutOfRangeException();

            if (index2 < 0 || index2 >= Count2)
                throw new IndexOutOfRangeException();

            var index = new Pair<int>(index1, index2);

            return _numbersDictionary.TryGetValue(index, out var number) 
                ? number : 0d;
        }
        set
        {
            if (index1 < 0 || index1 >= Count1)
                throw new IndexOutOfRangeException();

            if (index2 < 0 || index2 >= Count2)
                throw new IndexOutOfRangeException();

            Debug.Assert(!double.IsNaN(value));

            var index = new Pair<int>(index1, index2);

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

    public IEnumerable<Pair<int>> StoredIndices 
        => _numbersDictionary.Keys;

    public IEnumerable<double> StoredNumbers 
        => _numbersDictionary.Values;

    public IEnumerable<KeyValuePair<Pair<int>, double>> StoredPairs
        => _numbersDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseArray(int count1, int count2)
    {
        if (count1 <= 0)
            throw new ArgumentOutOfRangeException(nameof(count1));

        if (count2 <= 0)
            throw new ArgumentOutOfRangeException(nameof(count1));

        Count1 = count1;
        Count2 = count2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SparseArray(int count1, int count2, Dictionary<Pair<int>, double> numbersDictionary)
    {
        Count1 = count1;
        Count2 = count2;
        _numbersDictionary = numbersDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _numbersDictionary.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseArray MapNumbers(Func<double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<Pair<int>, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(scalar);

            if (scalar1 != 0)
                _numbersDictionary.Add(id, scalar1);
        }

        return new Float64SparseArray(Count1, Count2, numbersDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SparseArray MapNumbers(Func<int, int, double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<Pair<int>, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(id.Item1, id.Item2, scalar);

            if (scalar1 != 0)
                _numbersDictionary.Add(id, scalar1);
        }

        return new Float64SparseArray(Count1, Count2, numbersDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        for (var j = 0; j < Count2; j++)
        for (var i = 0; i < Count1; i++)
            yield return this[i, j];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}