namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

public sealed class IndexMapIdentity1D
    : IIndexMap1D
{
    public int IndexCount { get; set; }

    public int this[int index]
        => index;

    public IEnumerable<int> this[IEnumerable<int> indexList]
        => indexList;


    public IndexMapIdentity1D()
    {
        IndexCount = 0;
    }

    public IndexMapIdentity1D(int indexCount)
    {
        IndexCount = indexCount;
    }
}