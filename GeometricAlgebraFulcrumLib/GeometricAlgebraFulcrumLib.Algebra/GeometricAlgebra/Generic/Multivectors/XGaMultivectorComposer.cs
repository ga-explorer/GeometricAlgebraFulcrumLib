using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract class XGaMultivectorComposer<T> :
    IXGaElement<T>
{
    public XGaMetric Metric
        => Processor;

    public XGaProcessor<T> Processor { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public abstract IEnumerable<IndexSet> Ids { get; }
    
    public abstract IEnumerable<T> Scalars { get; }

    public abstract IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs { get; }
    
    public IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
        => IdScalarPairs.Select(p =>
            new KeyValuePair<XGaBasisBlade, T>(
                Metric.BasisBlade(p.Key),
                p.Value
            )
        );
    
    public abstract IEnumerable<int> KVectorGrades { get; }

    public abstract bool IsZero { get; }

    public T this[params int[] indexList]
    {
        get => GetTermScalarValue(indexList);
        set => SetTerm(indexList, value);
    }

    public T this[IndexSet id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }


    protected XGaMultivectorComposer(XGaProcessor<T> processor)
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
    public XGaMultivectorComposer<T> RemoveVectorTerm(int index)
    {
        var id = index.ToUnitIndexSet();

        return RemoveTerm(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> RemoveBivectorTerm(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    public abstract XGaMultivectorComposer<T> RemoveTerm(IndexSet id);


    public abstract T GetScalarTermScalarValue();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetVectorTermScalarValue(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetTermScalarValue(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetBivectorTermScalarValue(int index1, int index2)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? ScalarProcessor.Times(sign, GetTermScalarValue(id)).ScalarValue 
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? ScalarProcessor.Times(sign, GetTermScalarValue(id)).ScalarValue 
            : ScalarProcessor.ZeroValue;
    }
    
    public abstract T GetTermScalarValue(IndexSet id);

    
    public abstract XGaMultivectorComposer<T> SetScalarTerm(T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(int scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(long scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(float scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(double scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(string scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(Scalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, string scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, Scalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, int scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, long scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, float scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, string scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, T scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }


    public abstract XGaMultivectorComposer<T> SetTerm(IndexSet id, T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, int scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, long scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, float scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, double scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, string scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, Scalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SetTerm(IndexSet id, IScalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }


    public abstract XGaMultivectorComposer<T> AddScalarTerm(T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(int scalar)
    {
        return AddScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(long scalar)
    {
        return AddScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(float scalar)
    {
        return AddScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(double scalar)
    {
        return AddScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(string scalar)
    {
        return AddScalarTerm(ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(Scalar<T> scalar)
    {
        return AddScalarTerm(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddScalarTerm(IScalar<T> scalar)
    {
        return AddScalarTerm(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, int scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, long scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, float scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, string scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, Scalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddVectorTerm(int index, IScalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, int scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, long scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, float scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, string scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, T scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }


    public abstract XGaMultivectorComposer<T> AddTerm(IndexSet id, T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, int scalar)
    {
        return AddTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, long scalar)
    {
        return AddTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, float scalar)
    {
        return AddTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, double scalar)
    {
        return AddTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, string scalar)
    {
        return AddTerm(id, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, Scalar<T> scalar)
    {
        return AddTerm(id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> AddTerm(IndexSet id, IScalar<T> scalar)
    {
        return AddTerm(id, scalar.ScalarValue);
    }

    
    public abstract XGaMultivectorComposer<T> SubtractScalarTerm(T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(int scalar)
    {
        return SubtractScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(long scalar)
    {
        return SubtractScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(float scalar)
    {
        return SubtractScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(double scalar)
    {
        return SubtractScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(string scalar)
    {
        return SubtractScalarTerm(ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(Scalar<T> scalar)
    {
        return SubtractScalarTerm(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractScalarTerm(IScalar<T> scalar)
    {
        return SubtractScalarTerm(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, int scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, long scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, float scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, string scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, Scalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractVectorTerm(int index, IScalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, int scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, long scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, float scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, string scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, T scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }


    public abstract XGaMultivectorComposer<T> SubtractTerm(IndexSet id, T scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, int scalar)
    {
        return SubtractTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, long scalar)
    {
        return SubtractTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, float scalar)
    {
        return SubtractTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, double scalar)
    {
        return SubtractTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, string scalar)
    {
        return SubtractTerm(id, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, Scalar<T> scalar)
    {
        return SubtractTerm(id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivectorComposer<T> SubtractTerm(IndexSet id, IScalar<T> scalar)
    {
        return SubtractTerm(id, scalar.ScalarValue);
    }

    
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