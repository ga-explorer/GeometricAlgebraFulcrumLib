using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed record XGaFloat64RepeatedScalarVectorDictionary :
    IReadOnlyDictionary<IndexSet, double>
{
    public int Count { get; }
        
    public double Value { get; }
    
    public double this[IndexSet key]
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

    public IEnumerable<IndexSet> Keys 
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
    public bool ContainsKey(IndexSet key)
    {
        if (key.Count != 1) return false;

        var index = key.FirstIndex;

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out double value)
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
    public IEnumerator<KeyValuePair<IndexSet, double>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<IndexSet,double>(
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