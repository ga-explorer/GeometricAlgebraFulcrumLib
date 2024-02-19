using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

public sealed class XGaMultivectorComposer<T> :
    IXGaElement<T>
{
    private Dictionary<IIndexSet, T> _idScalarDictionary
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
        get => GetTermScalarValue(id.BitPatternToUInt64IndexSet());
        set => SetTerm(id.BitPatternToUInt64IndexSet(), value);
    }

    public T this[IIndexSet id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }

    public IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
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
            EmptyIndexSet.Instance
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
    public XGaMultivectorComposer<T> ClearTerm(IIndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarTermScalarValue()
    {
        var key = EmptyIndexSet.Instance;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetVectorTermScalarValue(int index)
    {
        var key = index.IndexToIndexSet();

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetBivectorTermScalarValue(int index1, int index2)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IIndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalarTermScalar()
    {
        return ScalarProcessor.CreateScalar(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetVectorTermScalar(int index)
    {
        return ScalarProcessor.CreateScalar(
            GetVectorTermScalarValue(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(int index1, int index2)
    {
        return ScalarProcessor.CreateScalar(
            GetBivectorTermScalarValue(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(IPair<int> indexPair)
    {
        return ScalarProcessor.CreateScalar(
            GetTermScalarValue(indexPair)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(IIndexSet basisBlade)
    {
        return ScalarProcessor.CreateScalar(
            GetTermScalarValue(basisBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> RemoveTerm(IIndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarTerm(T scalar)
    {
        SetTerm(
            EmptyIndexSet.Instance,
            scalar
        );

        return this;
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
    public XGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            ScalarProcessor.GetScalarFromNumber(scalar)
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
    public XGaMultivectorComposer<T> SetBivectorTerm(IPair<int> indexPair, T scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, float scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToUInt64IndexSet(),
            ScalarProcessor.GetScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, double scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToUInt64IndexSet(),
            ScalarProcessor.GetScalarFromNumber(scalar)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, string scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToUInt64IndexSet(),
            ScalarProcessor.GetScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(ulong basisBladeId, T scalar)
    {
        return SetTerm(
            basisBladeId.BitPatternToUInt64IndexSet(),
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetTerm(IIndexSet basisBlade, T scalar)
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
            scalar.ScalarValue()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalarNegative(XGaScalar<T> scalar)
    {
        SetScalarTerm(
            ScalarProcessor.Negative(scalar.ScalarValue())
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SetScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SetScalarTerm(
            ScalarProcessor.Times(scalar.ScalarValue(), scalingFactor)
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
            EmptyIndexSet.Instance,
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddTerm(IIndexSet basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar);

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
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
    public XGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<IIndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddScalar(XGaScalar<T> scalar)
    {
        if (scalar.IsZero)
            return this;

        AddTerm(
            EmptyIndexSet.Instance,
            scalar.ScalarValue()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        AddTerm(
            EmptyIndexSet.Instance,
            ScalarProcessor.Times(scalar.ScalarValue(), scalingFactor)
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
            EmptyIndexSet.Instance,
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerm(IIndexSet basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar);

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, ScalarProcessor.Negative(scalar));

        return this;
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
    public XGaMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<IIndexSet, T>> termList)
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
            EmptyIndexSet.Instance,
            scalar.ScalarValue()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractScalar(XGaScalar<T> scalar, T scalingFactor)
    {
        SubtractTerm(
            EmptyIndexSet.Instance,
            ScalarProcessor.Times(scalar.ScalarValue(), scalingFactor)
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
            ? ScalarProcessor.ScalarOne
            : ScalarProcessor.ScalarMinusOne;

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
            : ScalarProcessor.Negative(scalar);

        return SetTerm(
            basisBlade.Id,
            scalar1
        );
    }

    public XGaMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<IIndexSet, T>> termList)
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
    public XGaMultivectorComposer<T> AddTerm(IIndexSet basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

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
            ? ScalarProcessor.ScalarOne
            : ScalarProcessor.ScalarMinusOne;

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
            : ScalarProcessor.Negative(scalar);

        return AddTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : ScalarProcessor.Negative(scalar)
        );
    }

    public XGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddEGpTerm(IIndexSet id, T scalar1, T scalar2)
    {
        var term = Processor.EGp(id, id);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddEGpTerm(KeyValuePair<IIndexSet, T> term1, KeyValuePair<IIndexSet, T> term2)
    {
        var term = Processor.EGp(term1.Key, term2.Key);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddGpTerm(IIndexSet id, T scalar1, T scalar2)
    {
        var term = Processor.Gp(id, id);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> AddGpTerm(KeyValuePair<IIndexSet, T> term1, KeyValuePair<IIndexSet, T> term2)
    {
        var term = Processor.Gp(term1.Key, term2.Key);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerm(IIndexSet basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

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
            ? ScalarProcessor.ScalarOne
            : ScalarProcessor.ScalarMinusOne;

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
            : ScalarProcessor.Negative(scalar);

        return SubtractTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        if (basisBlade.IsNegative)
            scalar = ScalarProcessor.Negative(scalar);

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

    public XGaMultivectorComposer<T> MapScalars(Func<IIndexSet, T, T> mappingFunction)
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

    public XGaMultivectorComposer<T> MapBasisBlades(Func<IIndexSet, IIndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar);

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

    public XGaMultivectorComposer<T> MapBasisBlades(Func<IIndexSet, T, IIndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar);

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

    public XGaMultivectorComposer<T> MapTerms(Func<IIndexSet, T, KeyValuePair<IIndexSet, T>> mappingFunction)
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
                var scalar1 = ScalarProcessor.Add(scalar2, term1.Value);

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
        return MapScalars(s => ScalarProcessor.Negative(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivectorComposer<T> Conjugate()
    {
        return MapScalars((id, scalar) =>
            ScalarProcessor.Times(
                Processor.ConjugateSign(id),
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ENormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Values
                .Select(s => ScalarProcessor.Times(s, s));

        return Processor.CreateScalarFromSum(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> NormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Select(p =>
                    ScalarProcessor.Times(Processor.Signature(p.Key), p.Value, p.Value)
                );

        return Processor.CreateScalarFromSum(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetScalarPart()
    {
        return Processor.CreateScalar(
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

        return Processor.CreateVector(idScalarDictionary);
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

        return Processor.CreateBivector(idScalarDictionary);
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

        return Processor.CreateHigherKVector(grade, idScalarDictionary);
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

        return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
            ? Processor.CreateScalar(scalar)
            : Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );

        return Processor.CreateVector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );

        return Processor.CreateBivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );

        return Processor.CreateHigherKVector(grade, _idScalarDictionary);
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
            _ => Processor.CreateHigherKVector(grade, _idScalarDictionary)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.CreateZeroMultivector();

        if (_idScalarDictionary.Count == 1)
            return Processor.CreateMultivector(
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
                var scalar = Processor.CreateScalar(
                    gradeBasisScalarPairGroups.First().Value
                );

                gradeKVectorDictionary.Add(grade, scalar);

                continue;
            }

            var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

            var kVector = Processor.CreateKVector(
                grade,
                idScalarDictionary
            );

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return Processor.CreateMultivector(gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector()
    {
        return Processor.CreateUniformMultivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetSimpleMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.CreateZeroScalar();

        if (_idScalarDictionary.Count == 1)
            return Processor.CreateTermKVector(
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
                var scalar = Processor.CreateScalar(
                    gradeBasisScalarPairGroups.First().Value
                );

                gradeKVectorDictionary.Add(grade, scalar);

                continue;
            }

            var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

            var kVector = Processor.CreateKVector(
                grade,
                idScalarDictionary
            );

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return gradeKVectorDictionary.Count == 1
            ? gradeKVectorDictionary.First().Value
            : Processor.CreateMultivector(gradeKVectorDictionary);
    }
}