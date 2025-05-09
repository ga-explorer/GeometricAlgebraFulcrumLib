using System.Collections;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Permutations;

public sealed class IndexMapPermutation1D 
    : IReadOnlyList<int>, IIndexMap1D
{
    public static bool IsValidPermutationData(params int[] values)
    {
        var countingDict = new Dictionary<int, bool>();

        return values.All(value => countingDict.TryAdd(value, true));
    }

    public static bool IsValidPermutationData(IReadOnlyList<int> values)
    {
        var countingDict = new Dictionary<int, bool>();

        return values.All(value => countingDict.TryAdd(value, true));
    }

    public static IndexMapPermutation1D CreateWithValidation(params int[] values)
    {
        if (IsValidPermutationData(values))
            return new IndexMapPermutation1D(values);

        throw new InvalidOperationException();
    }

    public static IndexMapPermutation1D CreateWithoutValidation(params int[] values)
    {
        return new IndexMapPermutation1D(values);
    }

    public static IndexMapPermutation1D CreateRandom(int count)
    {
        var randGen = new System.Random();
        var sortedDict = new SortedDictionary<double, int>();

        for (var i = 0; i < count; i++)
            sortedDict.Add(randGen.NextDouble(), i);

        return new IndexMapPermutation1D(
            sortedDict.Values.ToArray()
        );
    }

    public static IndexMapPermutation1D CreateRandom(int count, int seed)
    {
        var randGen = new System.Random(seed);
        var sortedDict = new SortedDictionary<double, int>();

        for (var i = 0; i < count; i++)
            sortedDict.Add(randGen.NextDouble(), i);

        return new IndexMapPermutation1D(
            sortedDict.Values.ToArray()
        );
    }
        
        
    private readonly IReadOnlyList<int> _values;

    public int Count
        => _values.Count;

    public int IndexCount
    {
        get => _values.Count; 
        set { }
    }

    public int this[int index] 
        => _values[index.Mod(Count)];

    public int[] this[params int[] indexArray]
    {
        get
        {
            var result = new int[indexArray.Length];

            for (var i = 0; i < indexArray.Length; i++)
                result[i] = _values[indexArray[i].Mod(Count)];

            return result;
        }
    }

    public IEnumerable<int> this[IEnumerable<int> indexList] 
        => indexList.Select(i => _values[i.Mod(Count)]);

    public bool IsValid 
        => IsValidPermutationData(_values);


    private IndexMapPermutation1D(IReadOnlyList<int> values)
    {
        _values = values;
    }


    public IEnumerator<int> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _values.GetEnumerator();
    }
}