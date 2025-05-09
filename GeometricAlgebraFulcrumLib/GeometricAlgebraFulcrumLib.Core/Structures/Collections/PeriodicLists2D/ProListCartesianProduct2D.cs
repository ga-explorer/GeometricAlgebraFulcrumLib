using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists2D;

public class ProListCartesianProduct2D<TValue1, TValue2, TValue> : 
    IPeriodicReadOnlyList2D<TValue>
{
    public IPeriodicReadOnlyList<TValue1> SourceList1 { get; }

    public IPeriodicReadOnlyList<TValue2> SourceList2 { get; }

    public Func<TValue1, TValue2, TValue> CombinationFunc { get; }


    public int Count 
        => SourceList1.Count * SourceList2.Count;

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            return CombinationFunc(
                SourceList1[index1],
                SourceList2[index2]
            );
        }
    }

    public int Count1 
        => SourceList1.Count;

    public int Count2 
        => SourceList2.Count;

    public TValue this[int index1, int index2]
        => CombinationFunc(
            SourceList1[index1],
            SourceList2[index2]
        );


    public ProListCartesianProduct2D([NotNull] IPeriodicReadOnlyList<TValue1> sourceList1, [NotNull] IPeriodicReadOnlyList<TValue2> sourceList2, [NotNull] Func<TValue1, TValue2, TValue> combinationFunc)
    {
        SourceList1 = sourceList1;
        SourceList2 = sourceList2;
        CombinationFunc = combinationFunc;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        var valuesArray1 = SourceList1.ToArray();
        var valuesArray2 = SourceList2.ToArray();

        for (var index2 = 0; index2 < valuesArray2.Length; index2++)
        {
            var value2 = valuesArray2[index2];

            for (var index1 = 0; index1 < valuesArray1.Length; index1++)
            {
                var value1 = valuesArray1[index1];

                valuesArray[index1, index2] = CombinationFunc(value1, value2);
            }
        }

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        var valuesArray1 = SourceList1.ToArray();
        var valuesArray2 = SourceList2.ToArray();

        foreach (var value2 in valuesArray2)
        foreach (var value1 in valuesArray1)
            yield return CombinationFunc(value1, value2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}