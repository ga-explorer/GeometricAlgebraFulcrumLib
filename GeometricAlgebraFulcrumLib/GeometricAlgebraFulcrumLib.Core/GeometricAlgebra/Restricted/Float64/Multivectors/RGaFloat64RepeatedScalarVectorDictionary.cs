using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed record RGaFloat64RepeatedScalarVectorDictionary :
    IReadOnlyDictionary<ulong, double>
{
    public int Count { get; }
        
    public double Value { get; }
    
    public double this[ulong key]
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

    public IEnumerable<double> Values
        => Enumerable.Repeat(Value, Count);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64RepeatedScalarVectorDictionary(int count, double value)
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
    public bool ContainsKey(ulong key)
    {
        if (key.Grade() != 1) return false;

        var index = key.FirstOneBitPosition();

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out double value)
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
    public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<ulong,double>(
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