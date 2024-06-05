using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

public sealed class XGaFloat64MultivectorComposer :
    IXGaFloat64Element
{
    private Dictionary<IIndexSet, double> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<double>();


    public XGaMetric Metric
        => Processor;

    public XGaFloat64Processor Processor { get; }

    public bool IsZero
        => _idScalarDictionary.Count == 0;

    public double this[ulong id]
    {
        get => GetTermScalarValue(id.BitPatternToUInt64IndexSet());
        set => SetTerm(id.BitPatternToUInt64IndexSet(), value);
    }

    public double this[IIndexSet id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }

    public IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
        => _idScalarDictionary;

    public IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisBladeScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<XGaBasisBlade, double>(
                Metric.CreateBasisBlade(p.Key),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64MultivectorComposer(XGaFloat64Processor processor)
    {
        Processor = processor;
    }


    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer Clear()
    {
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            EmptyIndexSet.Instance
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.IndexToIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSetUtils.IndexPairToIndexSet(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer ClearTerm(IIndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarTermScalarValue()
    {
        var key = EmptyIndexSet.Instance;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetVectorTermScalarValue(int index)
    {
        var key = index.IndexToIndexSet();

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetBivectorTermScalarValue(int index1, int index2)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(IIndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer RemoveTerm(IIndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetScalarTerm(double scalar)
    {
        SetTerm(
            EmptyIndexSet.Instance,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetVectorTerm(int index, double scalar)
    {
        SetTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSetUtils.IndexTripletToIndexSet(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetTerm(ulong id, double scalar)
    {
        return SetTerm(id.BitPatternToUInt64IndexSet(), scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetTerm(IIndexSet basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
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
    public XGaFloat64MultivectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetScalar(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetScalarNegative(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            -scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SetScalarTerm(
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    public XGaFloat64MultivectorComposer SetMultivector(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64MultivectorComposer SetMultivectorNegative(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, -scalar);

        return this;
    }

    public XGaFloat64MultivectorComposer SetMultivector(XGaFloat64Multivector multivector, double scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddScalarTerm(double scalar)
    {
        AddTerm(
            EmptyIndexSet.Instance,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddVectorTerm(int index, double scalar)
    {
        AddTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        AddTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IPair<int> indexPair, double scalar)
    {
        AddTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IIndexSet basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = scalar1 + scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerms(IEnumerable<KeyValuePair<IIndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddScalar(XGaFloat64Scalar scalar)
    {
        if (scalar.IsZero)
            return this;

        AddTerm(
            EmptyIndexSet.Instance,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        AddTerm(
            EmptyIndexSet.Instance,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractScalarTerm(double scalar)
    {
        SubtractTerm(
            EmptyIndexSet.Instance,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractVectorTerm(int index, double scalar)
    {
        SubtractTerm(
            index.IndexToIndexSet(),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        SubtractTerm(
            IndexSetUtils.IndexPairToIndexSet(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(IPair<int> indexPair, double scalar)
    {
        SubtractTerm(
            IndexSetUtils.IndexPairToIndexSet(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(IIndexSet basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = scalar1 - scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, -scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<IIndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractScalar(XGaFloat64Scalar scalar)
    {
        if (scalar.IsZero)
            return this;

        SubtractTerm(
            EmptyIndexSet.Instance,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SubtractTerm(
            EmptyIndexSet.Instance,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetTerm(XGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Id);

        var scalar = basisBlade.IsPositive
            ? 1d
            : -1d;

        return SetTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SetTerm(XGaSignedBasisBlade basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return RemoveTerm(basisBlade.Id);

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : -scalar;

        return SetTerm(
            basisBlade.Id,
            scalar1
        );
    }

    public XGaFloat64MultivectorComposer SetTerms(IEnumerable<KeyValuePair<IIndexSet, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64MultivectorComposer SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IIndexSet basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? 1d
            : -1d;

        return AddTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : -scalar;

        return AddTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : -scalar
        );
    }

    public XGaFloat64MultivectorComposer AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddEGpTerm(IIndexSet id, double scalar1, double scalar2)
    {
        var term = Metric.EGp(id, id);
        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddEGpTerm(KeyValuePair<IIndexSet, double> term1, KeyValuePair<IIndexSet, double> term2)
    {
        var term = Metric.EGp(term1.Key, term2.Key);
        var scalar = term.IsPositive
            ? term1.Value * term2.Value
            : -(term1.Value * term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddGpTerm(IIndexSet id, double scalar1, double scalar2)
    {
        var term = Metric.Gp(id, id);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer AddGpTerm(KeyValuePair<IIndexSet, double> term1, KeyValuePair<IIndexSet, double> term2)
    {
        var term = Metric.Gp(term1.Key, term2.Key);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? term1.Value * term2.Value
            : -(term1.Value * term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(IIndexSet basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return SubtractTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? 1d
            : -1d;

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : -scalar;

        return SubtractTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        if (basisBlade.IsNegative)
            scalar = -scalar;

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    public XGaFloat64MultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaFloat64MultivectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64MultivectorComposer MapScalars(Func<IIndexSet, double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(id, scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64MultivectorComposer MapBasisBlades(Func<IIndexSet, IIndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = scalar2 + scalar;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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

    public XGaFloat64MultivectorComposer MapBasisBlades(Func<IIndexSet, double, IIndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = scalar2 + scalar;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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

    public XGaFloat64MultivectorComposer MapTerms(Func<IIndexSet, double, KeyValuePair<IIndexSet, double>> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var term1 = mappingFunction(id, scalar);

            if (term1.Value.IsZero())
                continue;

            if (idScalarDictionary.TryGetValue(term1.Key, out var scalar2))
            {
                var scalar1 = scalar2 + term1.Value;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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
    public XGaFloat64MultivectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64MultivectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Metric.HermitianConjugateSign(id) * scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ENormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Values
                .Select(s => s * s);

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Select(p =>
                    Metric.Signature(p.Key) * p.Value * p.Value
                );

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar GetScalarPart()
    {
        return Processor.Scalar(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorPart()
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
    public XGaFloat64Bivector GetBivectorPart()
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
    public XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
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
    public XGaFloat64KVector GetKVectorPart(int grade)
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
    public XGaFloat64Scalar GetScalar()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key.Count == 0
        );

        return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );

        return Processor.Vector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );

        return Processor.Bivector(_idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );

        return Processor.HigherKVector(grade, _idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector GetKVector(int grade)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivector GetMultivector()
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

        var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

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

            var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

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
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        return Processor.UniformMultivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector GetSimpleMultivector()
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

        var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

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

            var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

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