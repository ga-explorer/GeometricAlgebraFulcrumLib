using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed record XGaFloat64RepeatedScalarVectorDictionary :
    IReadOnlyDictionary<IIndexSet, double>
{
    public int Count { get; }
        
    public double Value { get; }
    
    public double this[IIndexSet key]
    {
        get
        {
            Debug.Assert(key.Count == 1);

            var index = key.FirstIndex;

            return index >= 0 && index < Count 
                ? Value 
                : throw new KeyNotFoundException();
        }
    }

    public IEnumerable<IIndexSet> Keys 
        => Count.GetRange().Select(index => index.IndexToIndexSet());

    public IEnumerable<double> Values
        => Enumerable.Repeat(Value, Count);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64RepeatedScalarVectorDictionary(int count, double value)
    {
        Count = count;
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int count, out double value)
    {
        count = Count;
        value = Value;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(IIndexSet key)
    {
        if (key.Count != 1) return false;

        var index = key.FirstIndex;

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IIndexSet key, out double value)
    {
        if (ContainsKey(key))
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IIndexSet, double>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<IIndexSet,double>(
                    index.IndexToIndexSet(), 
                    Value
                )
            ).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}