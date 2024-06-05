using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public class ProListSubList2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

    public int Count 
        => IndexRange1.Count * IndexRange2.Count;

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            return SourceList[
                IndexRange1[index1], 
                IndexRange2[index2]
            ];
        }
    }

    public int Count1 
        => IndexRange1.Count;

    public int Count2 
        => IndexRange2.Count;

    public IndexMapRange1D IndexRange1 { get; }

    public IndexMapRange1D IndexRange2 { get; }

    public TValue this[int index1, int index2] 
        => SourceList[
            IndexRange1[index1], 
            IndexRange2[index2]
        ];


    public ProListSubList2D([NotNull] IPeriodicReadOnlyList2D<TValue> sourceLattice, [NotNull] IndexMapRange1D rowIndexRange, [NotNull] IndexMapRange1D columnIndexRange)
    {
        SourceList = sourceLattice;
        IndexRange1 = rowIndexRange;
        IndexRange2 = columnIndexRange;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        {
            var sourceIndex2 = IndexRange2[index2];

            for (var index1 = 0; index1 < Count1; index1++)
            {
                var sourceIndex1 = IndexRange1[index1];

                valuesArray[index1, index2] = 
                    SourceList[sourceIndex1, sourceIndex2];
            }
        }

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < Count2; index2++)
        {
            var sourceIndex2 = IndexRange2[index2];

            for (var index1 = 0; index1 < Count1; index1++)
            {
                var sourceIndex1 = IndexRange1[index1];

                yield return SourceList[sourceIndex1, sourceIndex2];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}