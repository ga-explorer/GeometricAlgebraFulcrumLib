using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

public sealed record RGaRepeatedScalarVectorDictionary<TValue> :
    IReadOnlyDictionary<ulong, TValue>
{
    public int Count { get; }
        
    public TValue Value { get; }
    
    public TValue this[ulong key]
    {
        get
        {
            Debug.Assert(key.Grade() == 1);

            var index = key.FirstOneBitPosition();

            return index >= 0 && index < Count 
                ? Value 
                : throw new KeyNotFoundException();
        }
    }

    public IEnumerable<ulong> Keys 
        => Count.GetRange().Select(index => index.BasisVectorIndexToId());

    public IEnumerable<TValue> Values
        => Enumerable.Repeat(Value, Count);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaRepeatedScalarVectorDictionary(int count, TValue value)
    {
        Count = count;
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int count, out TValue value)
    {
        count = Count;
        value = Value;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(ulong key)
    {
        if (key.Grade() != 1) return false;

        var index = key.FirstOneBitPosition();

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out TValue value)
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
    public IEnumerator<KeyValuePair<ulong, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<ulong,TValue>(
                    index.BasisVectorIndexToId(), 
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