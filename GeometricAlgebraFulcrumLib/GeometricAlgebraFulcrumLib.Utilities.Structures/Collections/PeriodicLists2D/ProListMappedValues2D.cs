using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public class ProListMappedValues2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

    public Func<TValue, TValue> ValueMapping { get; }

    public int Count 
        => SourceList.Count;

    public int Count1 
        => SourceList.Count1;

    public int Count2 
        => SourceList.Count2;

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) =
                this.GetItemIndexTuple(index);

            return ValueMapping(SourceList[index1, index2]);
        }
    }

    public TValue this[int index1, int index2] 
        => ValueMapping(SourceList[index1, index2]);


    public ProListMappedValues2D(IPeriodicReadOnlyList2D<TValue> sourceList, Func<TValue, TValue> valueMapping)
    {
        SourceList = sourceList;
        ValueMapping = valueMapping;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            valuesArray[index1, index2] = ValueMapping(SourceList[index1, index2]);

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        return SourceList
            .Select(ValueMapping)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class ProListMappedValues2D<TValue1, TValue2> :
    IPeriodicReadOnlyList2D<TValue2>
{
    public IPeriodicReadOnlyList2D<TValue1> SourceList { get; }

    public Func<TValue1, TValue2> ValueMapping { get; }
        
    public int Count 
        => SourceList.Count;

    public int Count1 
        => SourceList.Count1;

    public int Count2 
        => SourceList.Count2;

    public TValue2 this[int index]
    {
        get
        {
            var (index1, index2) =
                this.GetItemIndexTuple(index);

            return ValueMapping(SourceList[index1, index2]);
        }
    }

    public TValue2 this[int index1, int index2] 
        => ValueMapping(SourceList[index1, index2]);


    public ProListMappedValues2D(IPeriodicReadOnlyList2D<TValue1> sourceList, Func<TValue1, TValue2> valueMapping)
    {
        SourceList = sourceList;
        ValueMapping = valueMapping;
    }


    public TValue2[,] ToArray2D()
    {
        var valuesArray = new TValue2[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            valuesArray[index1, index2] = ValueMapping(SourceList[index1, index2]);

        return valuesArray;
    }
        
    public IEnumerator<TValue2> GetEnumerator()
    {
        return SourceList
            .Select(ValueMapping)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}