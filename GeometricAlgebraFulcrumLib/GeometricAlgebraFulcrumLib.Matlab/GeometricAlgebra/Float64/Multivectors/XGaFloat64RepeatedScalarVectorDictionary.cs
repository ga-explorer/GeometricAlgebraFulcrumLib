using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

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
        => Count.GetRange().Select(index => index.ToUnitIndexSet());

    public IEnumerable<double> Values
        => Enumerable.Repeat(Value, Count);


    
    public XGaFloat64RepeatedScalarVectorDictionary(int count, double value)
    {
        Count = count;
        Value = value;
    }

    
    public void Deconstruct(out int count, out double value)
    {
        count = Count;
        value = Value;
    }

        
    
    public bool ContainsKey(IndexSet key)
    {
        if (key.Count != 1) return false;

        var index = key.FirstIndex;

        return index >= 0 && index <= Count;
    }

    
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

        
    
    public IEnumerator<KeyValuePair<IndexSet, double>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<IndexSet,double>(
                    index.ToUnitIndexSet(), 
                    Value
                )
            ).GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}