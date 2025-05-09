using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public sealed record XGaRepeatedScalarVectorDictionary<TValue> :
    IReadOnlyDictionary<IIndexSet, TValue>
{
    public int Count { get; }
        
    public TValue Value { get; }
    
    public TValue this[IIndexSet key]
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

    public IEnumerable<TValue> Values
        => Enumerable.Repeat(Value, Count);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaRepeatedScalarVectorDictionary(int count, TValue value)
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
    public bool ContainsKey(IIndexSet key)
    {
        if (key.Count != 1) return false;

        var index = key.FirstIndex;

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IIndexSet key, out TValue value)
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
    public IEnumerator<KeyValuePair<IIndexSet, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<IIndexSet,TValue>(
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