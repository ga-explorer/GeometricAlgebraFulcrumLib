namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

public sealed class UInt64Combination
{
    public static int MaxSetSize => 64;


    private readonly int[] _dataArray;

    public int SetSize { get; }

    public int SubsetSize 
        => _dataArray.Length;


    public UInt64Combination(int setSize, int subsetSize)
    {
        if (setSize < 0 || setSize > MaxSetSize)
            throw new InvalidOperationException($"Set size must be between 0 and {MaxSetSize}");

        if (subsetSize < 0 || subsetSize > setSize)
            throw new InvalidOperationException("Subset size must be between 0 and set size");

        SetSize = setSize;
        _dataArray = Enumerable.Range(0, subsetSize).ToArray();
    }


    public ulong GetBinomialCoefficient(int subsetSize)
    {
        return SetSize.GetBinomialCoefficient(subsetSize);
    }

    public UInt64Combination Successor()
    {
        if (_dataArray[0] == SetSize - SubsetSize)
            return null;

        var ans = new UInt64Combination(SetSize, SubsetSize);

        int i;
        for (i = 0; i < SubsetSize; ++i)
            ans._dataArray[i] = _dataArray[i];

        for (i = SubsetSize - 1; i > 0 && ans._dataArray[i] == SetSize - SubsetSize + i; --i) 
            ;

        ++ans._dataArray[i];

        for (var j = i; j < SubsetSize - 1; ++j)
            ans._dataArray[j + 1] = ans._dataArray[j] + 1;

        return ans;
    }
}