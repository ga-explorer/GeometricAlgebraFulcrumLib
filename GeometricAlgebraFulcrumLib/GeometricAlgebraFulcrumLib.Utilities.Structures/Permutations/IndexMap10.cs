using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

public sealed class IndexMap10
    : IIndexMap1D2D
{
    public int IndexCount1 { get; set; }

    public int IndexCount2 { get; set; }

    public int IndexCount
        => IndexCount1 * IndexCount2;

    public Pair<int> this[int index]
    {
        get
        {
            var index2 = index % IndexCount2;
            var index1 = (index - index2) / IndexCount2;

            return new Pair<int>(index1, index2);
        }
    }

    public IEnumerable<Pair<int>> this[IEnumerable<int> indexList]
        => indexList.Select(i => this[i]);

    public int this[Pair<int> indexPair]
        => indexPair.Item2 + indexPair.Item1 * IndexCount2;

    public int this[int index1, int index2]
        => index2 + index1 * IndexCount2;

    public IEnumerable<int> this[IEnumerable<Pair<int>> inputsList]
        => inputsList.Select(indexPair =>
            indexPair.Item2 + indexPair.Item1 * IndexCount2
        );


    public IndexMap10(int count1, int count2)
    {
        IndexCount1 = count1;
        IndexCount2 = count2;
    }
}