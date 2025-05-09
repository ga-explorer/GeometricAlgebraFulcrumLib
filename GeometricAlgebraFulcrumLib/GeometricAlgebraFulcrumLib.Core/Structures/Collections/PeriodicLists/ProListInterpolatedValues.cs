using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists;

public class ProListInterpolatedValues<TValue> :
    IPeriodicReadOnlyList<TValue>
{
    public TValue Value1 { get; }

    public TValue Value2 { get; }

    public Func<double, TValue, TValue, TValue> InterpolationFunc { get; }

    public int Count { get; }

    public TValue this[int index] 
        => InterpolationFunc(
            ((double)index) / (Count - 1),
            Value1,
            Value2
        );


    public ProListInterpolatedValues(int count, [NotNull] TValue value1, [NotNull] TValue value2, [NotNull] Func<double, TValue, TValue, TValue> interpolationFunc)
    {
        Debug.Assert(count > 1);

        Count = count;
        Value1 = value1;
        Value2 = value2;
        InterpolationFunc = interpolationFunc;
    }

        
    public IEnumerator<TValue> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(index => 
                InterpolationFunc(((double)index) / (Count - 1), Value1, Value2)
            )
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}