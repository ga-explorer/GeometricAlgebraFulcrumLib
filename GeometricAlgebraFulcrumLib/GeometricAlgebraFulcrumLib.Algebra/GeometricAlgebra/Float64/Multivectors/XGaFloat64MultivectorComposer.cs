using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public abstract class XGaFloat64MultivectorComposer :
    IXGaFloat64Element
{
    public XGaMetric Metric
        => Processor;

    public XGaFloat64Processor Processor { get; }
    
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
    
    public abstract IEnumerable<int> KVectorGrades { get; }

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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetStoredBasisVectorIndices()
    {
        return IndexSet.Create(
            Ids.SelectMany(id => id), 
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetStoredKVectorGrades()
    {
        return IndexSet.Create(
            KVectorGrades, 
            false
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer RemoveVectorTerm(int index)
    {
        var id = index.ToUnitIndexSet();

        return RemoveTerm(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer RemoveBivectorTerm(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    public abstract XGaFloat64MultivectorComposer RemoveTerm(IndexSet id);


    public abstract double GetScalarTermScalarValue();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetVectorTermScalarValue(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetTermScalarValue(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetBivectorTermScalarValue(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? sign * GetTermScalarValue(id) : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? sign * GetTermScalarValue(id) : 0d;
    }
    
    public abstract double GetTermScalarValue(IndexSet id);

    
    public abstract XGaFloat64MultivectorComposer SetScalarTerm(double scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SetVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SetBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }
    
    public abstract XGaFloat64MultivectorComposer SetTerm(IndexSet id, double scalarValue);


    public abstract XGaFloat64MultivectorComposer AddScalarTerm(double scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer AddVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer AddBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }
    
    public abstract XGaFloat64MultivectorComposer AddTerm(IndexSet id, double scalarValue);

    
    public abstract XGaFloat64MultivectorComposer SubtractScalarTerm(double scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SubtractVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaFloat64MultivectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }
    
    public abstract XGaFloat64MultivectorComposer SubtractTerm(IndexSet id, double scalarValue);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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