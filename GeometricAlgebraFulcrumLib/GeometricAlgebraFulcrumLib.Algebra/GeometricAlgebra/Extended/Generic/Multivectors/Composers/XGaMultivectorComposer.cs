using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

public sealed class XGaMultivectorComposer<T> :
    IXGaElement<T>
{
    private Dictionary<IndexSet, T> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<T>();


    public XGaMetric Metric
        => Processor;

    public IScalarProcessor<T> ScalarProcessor
        => Processor.ScalarProcessor;

    public XGaProcessor<T> Processor { get; }

    public bool IsZero
        => _idScalarDictionary.Count == 0;

    public T this[ulong id]
    {
        get => GetTermScalarValue(id.BitPatternToIndexSet());
        set => SetTerm(id.BitPatternToIndexSet(), value);
    }

    public T this[IndexSet id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;

    public IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisBladeScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<XGaBasisBlade, T>(
                Processor.CreateBasisBlade(p.Key),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaMultivectorComposer(XGaProcessor<T> processor)
    {
        Processor = processor;
    }


    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Clear()
    {
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            IndexSet.EmptySet
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.IndexToIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSetUtils.IndexPairToIndexSet(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> ClearTerm(IndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarTermScalarValue()
    {
        var key = IndexSet.EmptySet;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetVectorTermScalarValue(int index)
    {
        var key = index.IndexToIndexSet();

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetBivectorTermScalarValue(int index1, int index2)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IndexSet basisBlade)
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
    public XGaMultivectorComposer<T> RemoveTerm(IndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarTerm(T scalar)
    {
        SetTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarTerm(Scalar<T> scalar)
    {
        SetTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        SetTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, uint scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, ulong scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        return SetVectorTerm(index, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, T scalar)
    {
        SetTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, Scalar<T> scalar)
    {
        SetTerm(
            index.IndexToIndexSet(),
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        SetTerm(
            index.IndexToIndexSet(),
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            ScalarProcessor.ScalarFromNumber(scalar)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, T scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetBivectorTerm(IPair<int> indexPair, T scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        SetTerm(
            IndexSetUtils.IndexTripletToIndexSet(index1, index2, index3),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        SetTerm(
            IndexSetUtils.IndexTripletToIndexSet(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        SetTerm(
            IndexSetUtils.IndexTripletToIndexSet(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, float scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, double scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, string scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, T scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, Scalar<T> scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            scalar.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, IScalar<T> scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToIndexSet(),
            scalar.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(IndexSet basisBlade, T scalar)
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
    public XGaMultivectorComposer<T> SetTerm(IndexSet basisBlade, Scalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalar(XGaScalar<T> scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarNegative(XGaScalar<T> scalar)
    {
        SetScalarTerm(
            ScalarProcessor.Negative(scalar.ScalarValue)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SetScalarTerm(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    public XGaMultivectorComposer<T> SetMultivector(XGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaMultivectorComposer<T> SetMultivectorNegative(XGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Negative(scalar));

        return this;
    }

    public XGaMultivectorComposer<T> SetMultivector(XGaMultivector<T> multivector, T scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddScalarTerm(T scalar)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddVectorTerm(int index, T scalar)
    {
        AddTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, T scalar)
    {
        AddTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddTerm(IPair<int> indexPair, T scalar)
    {
        AddTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    public XGaMultivectorComposer<T> AddTerm(IndexSet basisBlade, T scalar)
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
    public XGaMultivectorComposer<T> AddTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return AddTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddScalar(IScalar<T> scalar)
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
    public XGaMultivectorComposer<T> AddScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        AddTerm(
            IndexSet.EmptySet,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddMultivector(XGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddMultivector(XGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractScalarTerm(T scalar)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractVectorTerm(int index, T scalar)
    {
        SubtractTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, T scalar)
    {
        SubtractTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerm(IPair<int> indexPair, T scalar)
    {
        SubtractTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    public XGaMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, T scalar)
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
    public XGaMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, IScalar<T> scalar)
    {
        return SubtractTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractScalar(XGaScalar<T> scalar)
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
    public XGaMultivectorComposer<T> SubtractScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractMultivector(XGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractMultivector(XGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaMultivectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade, T scalar)
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

    public XGaMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddTerm(IndexSet basisBlade, T scalar1, T scalar2)
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
    public XGaMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade)
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
    public XGaMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar)
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
    public XGaMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : ScalarProcessor.Negative(scalar).ScalarValue
        );
    }

    public XGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddEGpTerm(IndexSet id, T scalar1, T scalar2)
    {
        var term = Processor.EGp(id, id);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddEGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
    {
        var term = Processor.EGp(term1.Key, term2.Key);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddGpTerm(IndexSet id, T scalar1, T scalar2)
    {
        var term = Processor.Gp(id, id);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
    {
        var term = Processor.Gp(term1.Key, term2.Key);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerm(IndexSet basisBlade, T scalar1, T scalar2)
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
    public XGaMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar)
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
    public XGaMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
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

    public XGaMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaMultivectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaMultivectorComposer<T> MapScalars(Func<IndexSet, T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaMultivectorComposer<T> MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
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

    public XGaMultivectorComposer<T> MapBasisBlades(Func<IndexSet, T, IndexSet> mappingFunction)
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

    public XGaMultivectorComposer<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> mappingFunction)
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
    public XGaMultivectorComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Conjugate()
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

        return _idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );

        return Processor.Vector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );

        return Processor.Bivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );

        return Processor.HigherKVector(grade, _idScalarDictionary);
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
            _ => Processor.HigherKVector(grade, _idScalarDictionary)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.MultivectorZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.Multivector(
                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Count
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

        return Processor.Multivector(gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector()
    {
        return Processor.UniformMultivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetSimpleMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.KVectorTerm(
                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Count
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

        return gradeKVectorDictionary.Count == 1
            ? gradeKVectorDictionary.First().Value
            : Processor.Multivector(gradeKVectorDictionary);
    }
}