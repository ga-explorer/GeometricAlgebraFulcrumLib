using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists;

public class ProListMappedValues<TValue> :
    IPeriodicReadOnlyList<TValue>
{
    public IPeriodicReadOnlyList<TValue> SourceList { get; }

    public Func<TValue, TValue> ValueMapping { get; }
        
    public int Count 
        => SourceList.Count;

    public TValue this[int index] 
        => ValueMapping(SourceList[index]);


    public ProListMappedValues([NotNull] IPeriodicReadOnlyList<TValue> sourceList, [NotNull] Func<TValue, TValue> valueMapping)
    {
        SourceList = sourceList;
        ValueMapping = valueMapping;
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

public class ProListMappedValues<TValue1, TValue2> :
    IPeriodicReadOnlyList<TValue2>
{
    public IPeriodicReadOnlyList<TValue1> SourceList { get; }

    public Func<TValue1, TValue2> ValueMapping { get; }
        
    public int Count 
        => SourceList.Count;

    public TValue2 this[int index] 
        => ValueMapping(SourceList[index]);


    public ProListMappedValues([NotNull] IPeriodicReadOnlyList<TValue1> sourceList, [NotNull] Func<TValue1, TValue2> valueMapping)
    {
        SourceList = sourceList;
        ValueMapping = valueMapping;
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