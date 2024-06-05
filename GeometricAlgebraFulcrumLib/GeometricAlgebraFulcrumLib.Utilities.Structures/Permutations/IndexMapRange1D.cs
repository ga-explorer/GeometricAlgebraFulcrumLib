using System.Collections;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

public sealed class IndexMapRange1D
    : IReadOnlyList<int>, IIndexMap1D
{
    public int Count 
        => IndexCount;

    public int IndexCount { get; }

    public int FirstIndex { get; set; }

    public int LastIndex 
        => MoveForward 
            ? FirstIndex + Count - 1 
            : FirstIndex - Count + 1;

    public int MinIndex
        => MoveForward 
            ? FirstIndex 
            : FirstIndex - Count + 1;

    public int MaxIndex
        => MoveForward 
            ? FirstIndex + Count - 1 
            : FirstIndex;

    public bool MoveForward { get; set; }

    public bool MoveBackward
    {
        get => !MoveForward;
        set => MoveForward = !value;
    }

    public int this[int index] 
        => MoveForward 
            ? FirstIndex + index 
            : FirstIndex - index;

    public int[] this[params int[] indexList] 
        => MoveForward
            ? indexList.Select(i => FirstIndex + i).ToArray()
            : indexList.Select(i => FirstIndex - i).ToArray();

    public IEnumerable<int> this[IEnumerable<int> indexList] 
        => MoveForward
            ? indexList.Select(i => FirstIndex + i)
            : indexList.Select(i => FirstIndex - i);


    public IndexMapRange1D(int indexCount)
    {
        FirstIndex = 0;
        IndexCount = indexCount;
        MoveForward = true;
    }

    public IndexMapRange1D(int indexCount, int firstIndex)
    {
        FirstIndex = firstIndex;
        IndexCount = indexCount;
        MoveForward = true;
    }

    public IndexMapRange1D(int indexCount, int firstIndex, bool moveForward)
    {
        FirstIndex = firstIndex;
        IndexCount = indexCount;
        MoveForward = moveForward;
    }


    public IndexMapRange1D ReverseDirection()
    {
        FirstIndex = LastIndex;
        MoveForward = !MoveForward;

        return this;
    }

    public IEnumerable<Pair<int>> GetIndexPairs(bool periodicPairs = false)
    {
        var step = MoveForward ? 1 : -1;

        var index1 = FirstIndex;
        var index2 = FirstIndex;
        var stepsLeft = IndexCount - 1;
        var indexPair = new Pair<int>(index1, index1);

        while (stepsLeft > 0)
        {
            index2 += step;

            yield return indexPair.NextPair(index2);

            stepsLeft--;
        }

        if (periodicPairs)
            yield return indexPair.NextPair(FirstIndex);
    }
        

    public IEnumerator<int> GetEnumerator()
    {
        var step = MoveForward ? 1 : -1;
        var index = FirstIndex;
        var stepsLeft = IndexCount;

        while (stepsLeft > 0)
        {
            yield return index;

            index += step;

            stepsLeft--;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine("Index Range <")
            .Append(FirstIndex)
            .Append(", ")
            .Append(LastIndex)
            .AppendLine(">")
            .ToString();
    }
}