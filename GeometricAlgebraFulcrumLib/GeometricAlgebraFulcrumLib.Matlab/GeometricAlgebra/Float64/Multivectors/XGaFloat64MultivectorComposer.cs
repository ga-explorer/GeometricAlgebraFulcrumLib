using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public abstract class XGaFloat64MultivectorComposer :
    IXGaFloat64Element
{
    public XGaMetric Metric
        => Processor;

    public XGaFloat64Processor Processor { get; }
    
    public abstract IEnumerable<int> KVectorGrades { get; }

    public abstract IEnumerable<IndexSet> Ids { get; }
    
    public abstract IEnumerable<double> Scalars { get; }

    public abstract IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs { get; }
    
    public IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
        => IdScalarPairs.Select(p =>
            new KeyValuePair<XGaBasisBlade, double>(
                Metric.BasisBlade(p.Key),
                p.Value
            )
        );
    
    public abstract bool IsZero { get; }

    public double this[params int[] indexList]
    {
        get => GetTermScalarValue(indexList);
        set => SetTerm(indexList, value);
    }

    public double this[IndexSet id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }


    protected XGaFloat64MultivectorComposer(XGaFloat64Processor processor)
    {
        Processor = processor;
    }

    
    public abstract bool IsValid();

    
    public IndexSet GetStoredBasisVectorIndices()
    {
        return IndexSet.Create(
            Ids.SelectMany(id => id), 
            false
        );
    }

    public IndexSet GetStoredKVectorGrades()
    {
        return IndexSet.Create(
            KVectorGrades, 
            false
        );
    }


    public XGaFloat64MultivectorComposer RemoveVectorTerm(int index)
    {
        var id = index.ToUnitIndexSet();

        return RemoveTerm(id);
    }

    
    public XGaFloat64MultivectorComposer RemoveBivectorTerm(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    
    public XGaFloat64MultivectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    
    public virtual XGaFloat64MultivectorComposer RemoveTerm(IndexSet id)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.RemoveTerm(id),
            XGaFloat64GradedMultivectorComposer c => c.RemoveTerm(id),
            XGaFloat64UniformMultivectorComposer c => c.RemoveTerm(id),
            _ => throw new InvalidOperationException()
        };
    }


    public abstract double GetScalarTermScalarValue();

    
    public double GetVectorTermScalarValue(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetTermScalarValue(id);
    }

    
    public double GetBivectorTermScalarValue(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? sign * GetTermScalarValue(id) : 0d;
    }

    
    public double GetTermScalarValue(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? sign * GetTermScalarValue(id) : 0d;
    }
    
    public abstract double GetTermScalarValue(IndexSet id);


    
    public XGaFloat64MultivectorComposer SetScalarTerm(double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.SetScalarTerm(scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.SetScalarTerm(scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.SetScalarTerm(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    
    public XGaFloat64MultivectorComposer SetVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalarValue);
    }

    
    public XGaFloat64MultivectorComposer SetBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }

    
    public XGaFloat64MultivectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }
    
    
    public XGaFloat64MultivectorComposer SetTerm(IndexSet id, double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.SetTerm(id, scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.SetTerm(id, scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.SetTerm(id, scalarValue),
            _ => throw new InvalidOperationException()
        };
    }


    
    public XGaFloat64MultivectorComposer AddScalarTerm(double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.AddScalarTerm(scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.AddScalarTerm(scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.AddScalarTerm(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    
    public XGaFloat64MultivectorComposer AddVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalarValue);
    }

    
    public XGaFloat64MultivectorComposer AddBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }

    
    public XGaFloat64MultivectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }
    
    
    public XGaFloat64MultivectorComposer AddTerm(IndexSet id, double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.AddTerm(id, scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.AddTerm(id, scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.AddTerm(id, scalarValue),
            _ => throw new InvalidOperationException()
        };
    }


    
    public XGaFloat64MultivectorComposer SubtractScalarTerm(double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.SubtractScalarTerm(scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.SubtractScalarTerm(scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.SubtractScalarTerm(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    
    public XGaFloat64MultivectorComposer SubtractVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalarValue);
    }

    
    public XGaFloat64MultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    
    public virtual XGaFloat64MultivectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }
    
    
    public XGaFloat64MultivectorComposer SubtractTerm(IndexSet id, double scalarValue)
    {
        return this switch
        {
            XGaFloat64KVectorComposer c => c.SubtractTerm(id, scalarValue),
            XGaFloat64GradedMultivectorComposer c => c.SubtractTerm(id, scalarValue),
            XGaFloat64UniformMultivectorComposer c => c.SubtractTerm(id, scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    
    public override string ToString()
    {
        if (IsZero) return "0";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"{p.Value:G} {p.Key}")
            .ConcatenateText(" + ");
    }
}