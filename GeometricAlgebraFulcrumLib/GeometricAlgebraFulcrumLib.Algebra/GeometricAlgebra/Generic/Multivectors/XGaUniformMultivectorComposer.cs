using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaUniformMultivectorComposer<T> :
    XGaMultivectorComposer<T>
{
    private readonly ScalarComposer<T> _scalarComposer;

    private Dictionary<IndexSet, T> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<T>();


    public override IEnumerable<int> KVectorGrades
        => _idScalarDictionary.Keys.Select(id => id.Count).Distinct();

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public T this[ulong id]
    {
        get => GetTermScalarValue(id.ToUInt64IndexSet());
        set => SetTerm(id.ToUInt64IndexSet(), value);
    }
    
    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars 
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;

    public IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisBladeScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<XGaBasisBlade, T>(
                Processor.BasisBlade(p.Key),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaUniformMultivectorComposer(XGaProcessor<T> processor)
        : base(processor)
    {
        _scalarComposer = ScalarComposer<T>.Create(processor.ScalarProcessor);
    }
    

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Clear()
    {
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            IndexSet.EmptySet
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> ClearTerm(IndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetScalarTermScalarValue()
    {
        var key = IndexSet.EmptySet;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSet.CreatePair(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetTermScalarValue(IndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalarTermScalar()
    {
        return ScalarProcessor.ScalarFromValue(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetVectorTermScalar(int index)
    {
        return ScalarProcessor.ScalarFromValue(
            GetVectorTermScalarValue(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(int index1, int index2)
    {
        return ScalarProcessor.ScalarFromValue(
            GetBivectorTermScalarValue(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(IPair<int> indexPair)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(indexPair)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(IndexSet basisBlade)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(basisBlade)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> RemoveTerm(IndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(int scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(long scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(float scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(double scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(string scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(T scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(Scalar<T> scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        return SetTerm(IndexSet.EmptySet, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, string scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, Scalar<T> scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(),
            scalar.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(),
            scalar.ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, int scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, long scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, float scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, string scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar.Times(sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar.Times(sign).ScalarValue) 
            : this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, int scalar)
    {
        return SetTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, long scalar)
    {
        return SetTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, float scalar)
    {
        return SetTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, double scalar)
    {
        return SetTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, string scalar)
    {
        return SetTerm(basisBlade, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
        {
            _idScalarDictionary.Remove(basisBlade);
            return this;
        }

        if (_idScalarDictionary.ContainsKey(basisBlade))
            _idScalarDictionary[basisBlade] = scalar;
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, Scalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SetTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetScalar(XGaScalar<T> scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetScalarNegative(XGaScalar<T> scalar)
    {
        SetScalarTerm(
            ScalarProcessor.Negative(scalar.ScalarValue)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SetScalarTerm(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    public XGaUniformMultivectorComposer<T> SetMultivector(XGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaUniformMultivectorComposer<T> SetMultivectorNegative(XGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Negative(scalar));

        return this;
    }

    public XGaUniformMultivectorComposer<T> SetMultivector(XGaMultivector<T> multivector, T scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddScalarTerm(T scalar)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }


    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar);

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2.ScalarValue;
        }
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, int scalar)
    {
        return AddTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, long scalar)
    {
        return AddTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, float scalar)
    {
        return AddTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, double scalar)
    {
        return AddTerm(basisBlade, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, string scalar)
    {
        return AddTerm(basisBlade, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, Scalar<T> scalar)
    {
        return AddTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return AddTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm((IndexSet)basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddScalar(IScalar<T> scalar)
    {
        if (scalar.IsZero())
            return this;

        AddTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        AddTerm(
            IndexSet.EmptySet,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddMultivector(XGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddMultivector(XGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivectorComposer<T> SubtractScalarTerm(T scalar)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(IPair<int> indexPair, T scalar)
    {
        SubtractTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    public override XGaUniformMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return SubtractTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractScalar(XGaScalar<T> scalar)
    {
        if (scalar.IsZero)
            return this;

        SubtractTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractMultivector(XGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractMultivector(XGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Id);

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SetTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return RemoveTerm(basisBlade.Id);

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SetTerm(
            basisBlade.Id,
            scalar1
        );
    }

    public XGaUniformMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaUniformMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerm(IndexSet basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return AddTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return AddTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : ScalarProcessor.Negative(scalar).ScalarValue
        );
    }

    public XGaUniformMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (ScalarProcessor.IsZero(scalar))
            return this;

        return SubtractTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SubtractTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        if (basisBlade.IsNegative)
            scalar = ScalarProcessor.Negative(scalar).ScalarValue;

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    public XGaUniformMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaUniformMultivectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaUniformMultivectorComposer<T> MapScalars(Func<IndexSet, T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaUniformMultivectorComposer<T> MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(id1);
                else
                    idScalarDictionary[id1] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(id1, scalar);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaUniformMultivectorComposer<T> MapBasisBlades(Func<IndexSet, T, IndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(id1);
                else
                    idScalarDictionary[id1] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(id1, scalar);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaUniformMultivectorComposer<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var term1 = mappingFunction(id, scalar);

            if (ScalarProcessor.IsZero(term1.Value))
                continue;

            if (idScalarDictionary.TryGetValue(term1.Key, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, term1.Value).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(term1.Key);
                else
                    idScalarDictionary[term1.Key] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(term1.Key, term1.Value);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivectorComposer<T> Conjugate()
    {
        return MapScalars((id, scalar) =>
            ScalarProcessor.Times(
                Processor.HermitianConjugateSign(id),
                scalar
            ).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Values
                .Select(s => ScalarProcessor.Times(s, s).ScalarValue);

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Select(p =>
                    ScalarProcessor.Times(Processor.Signature(p.Key), p.Value, p.Value).ScalarValue
                );

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetScalarPart()
    {
        return Processor.Scalar(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Count == 1)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Count == 2)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Bivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Count == grade)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.HigherKVector(grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetKVectorPart(int grade)
    {
        return grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => GetScalarPart(),
            1 => GetVectorPart(),
            2 => GetBivectorPart(),
            _ => GetHigherKVectorPart(grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetScalar()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key.Count == 0
        );
        
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        var mv = 
            _idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
                ? Processor.Scalar(scalar)
                : throw new InvalidOperationException();

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );
        
        
        if (_idScalarDictionary.Count == 0)
            return Processor.VectorZero;

        var mv = 
            Processor.Vector(_idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );
        
        if (_idScalarDictionary.Count == 0)
            return Processor.BivectorZero;

        var mv = 
            Processor.Bivector(_idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );

        if (grade < 3)
            throw new InvalidOperationException();

        if (_idScalarDictionary.Count == 0)
            return Processor.HigherKVectorZero(grade);

        var mv = 
            Processor.HigherKVector(grade, _idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetKVector(int grade)
    {
        return grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => GetScalar(),
            1 => GetVector(),
            2 => GetBivector(),
            _ => GetHigherKVector(grade)
        };
    }

    public XGaGradedMultivector<T> GetGradedMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.GradedMultivectorZero;

        if (_idScalarDictionary.Count == 1)
        {
            var mv = 
                Processor.GradedMultivector(_idScalarDictionary.First());

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            return mv;
        }

        {
            var gradeGroup =
                _idScalarDictionary.GroupBy(basisScalarPair => basisScalarPair.Key.Count
                );

            var gradeKVectorDictionary = new Dictionary<int, XGaKVector<T>>();

            foreach (var gradeBasisScalarPairGroups in gradeGroup)
            {
                var grade = gradeBasisScalarPairGroups.Key;

                if (grade == 0)
                {
                    var scalar = Processor.Scalar(
                        gradeBasisScalarPairGroups.First().Value
                    );

                    gradeKVectorDictionary.Add(grade, scalar);

                    continue;
                }

                var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

                idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

                var kVector = Processor.KVector(
                    grade,
                    idScalarDictionary
                );

                gradeKVectorDictionary.Add(grade, kVector);
            }

            var mv = 
                Processor.GradedMultivector(gradeKVectorDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            return mv;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.UniformMultivectorZero;

        var mv = 
            Processor.UniformMultivector(_idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    public XGaMultivector<T> GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_idScalarDictionary.Count == 1)
        {
            var mv = 
                Processor.KVectorTerm(_idScalarDictionary.First());

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            return mv;
        }

        {
            var gradeGroup =
                _idScalarDictionary.GroupBy(basisScalarPair => basisScalarPair.Key.Count
                );

            var gradeKVectorDictionary = new Dictionary<int, XGaKVector<T>>();

            foreach (var gradeBasisScalarPairGroups in gradeGroup)
            {
                var grade = gradeBasisScalarPairGroups.Key;

                if (grade == 0)
                {
                    var scalar = Processor.Scalar(
                        gradeBasisScalarPairGroups.First().Value
                    );

                    gradeKVectorDictionary.Add(grade, scalar);

                    continue;
                }

                var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

                idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

                var kVector = Processor.KVector(
                    grade,
                    idScalarDictionary
                );

                gradeKVectorDictionary.Add(grade, kVector);
            }

            XGaMultivector<T> mv = gradeKVectorDictionary.Count == 1
                ? gradeKVectorDictionary.First().Value
                : Processor.GradedMultivector(gradeKVectorDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            return mv;
        }
    }
}