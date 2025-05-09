using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

public sealed class RGaFloat64MultivectorComposer :
    IRGaElement
{
    private Dictionary<ulong, double> _idScalarDictionary
        = new Dictionary<ulong, double>();


    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric 
        => Processor;

    public double this[ulong id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }

    public bool IsZero
        => _idScalarDictionary.Count == 0;

    public IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
        => _idScalarDictionary;

    public IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisBladeScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<RGaBasisBlade, double>(
                Processor.CreateBasisBlade(p.Key),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64MultivectorComposer(RGaFloat64Processor metric)
    {
        Processor = metric;
    }


    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer Clear()
    {
        _idScalarDictionary = new Dictionary<ulong, double>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            0UL
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            1UL << index
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer ClearTerm(ulong basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarTermScalarValue()
    {
        var key = 0UL;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetVectorTermScalarValue(int index)
    {
        var key = 1UL << index;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetBivectorTermScalarValue(int index1, int index2)
    {
        var key = BasisBivectorUtils.IndexPairToBivectorId(index1, index2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(IPair<int> indexPair)
    {
        var key = BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(ulong basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer RemoveTerm(ulong basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetScalarTerm(double scalar)
    {
        SetTerm(
            0UL,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetVectorTerm(int index, double scalar)
    {
        SetTerm(
            1UL << index,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        SetTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            BasisBladeUtils.IndexTripletToTrivectorId(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetTerm(ulong basisBlade, double scalar)
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
    public RGaFloat64MultivectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetScalar(RGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetScalarNegative(RGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            -scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetScalar(RGaFloat64Scalar scalar, double scalingFactor)
    {
        SetScalarTerm(
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    public RGaFloat64MultivectorComposer SetMultivector(RGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public RGaFloat64MultivectorComposer SetMultivectorNegative(RGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, -scalar);

        return this;
    }

    public RGaFloat64MultivectorComposer SetMultivector(RGaFloat64Multivector multivector, double scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddScalarTerm(double scalar)
    {
        AddTerm(
            0UL,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddVectorTerm(int index, double scalar)
    {
        AddTerm(
            1UL << index,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        AddTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddTerm(IPair<int> indexPair, double scalar)
    {
        AddTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddTerm(ulong basisBlade, double scalar)
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
    public RGaFloat64MultivectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddTerms(IEnumerable<KeyValuePair<ulong, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddTerms(IEnumerable<RGaIdScalarRecord> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddScalar(RGaFloat64Scalar scalar)
    {
        if (scalar.IsZero)
            return this;

        AddTerm(
            0UL,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddScalar(RGaFloat64Scalar scalar, double scalingFactor)
    {
        AddTerm(
            0UL,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddMultivector(RGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddMultivector(RGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractScalarTerm(double scalar)
    {
        SubtractTerm(
            0UL,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractVectorTerm(int index, double scalar)
    {
        SubtractTerm(
            1UL << index,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        SubtractTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractTerm(IPair<int> indexPair, double scalar)
    {
        SubtractTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractTerm(ulong basisBlade, double scalar)
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
    public RGaFloat64MultivectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<ulong, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractScalar(RGaFloat64Scalar scalar)
    {
        if (scalar.IsZero)
            return this;

        SubtractTerm(
            0UL,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractScalar(RGaFloat64Scalar scalar, double scalingFactor)
    {
        SubtractTerm(
            0UL,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractMultivector(RGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractMultivector(RGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SetTerm(RGaSignedBasisBlade basisBlade)
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
    public RGaFloat64MultivectorComposer SetTerm(RGaSignedBasisBlade basisBlade, double scalar)
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

    public RGaFloat64MultivectorComposer SetTerms(IEnumerable<KeyValuePair<ulong, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public RGaFloat64MultivectorComposer SetTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddTerm(ulong basisBlade, double scalar1, double scalar2)
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
    public RGaFloat64MultivectorComposer AddTerm(IRGaSignedBasisBlade basisBlade)
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
    public RGaFloat64MultivectorComposer AddTerm(IRGaSignedBasisBlade basisBlade, double scalar)
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
    public RGaFloat64MultivectorComposer AddTerm(IRGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : -scalar
        );
    }

    public RGaFloat64MultivectorComposer AddTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddEGpTerm(ulong id, double scalar1, double scalar2)
    {
        var term = Processor.EGp(id, id);
        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddEGpTerm(KeyValuePair<ulong, double> term1, KeyValuePair<ulong, double> term2)
    {
        var term = Processor.EGp(term1.Key, term2.Key);
        var scalar = term.IsPositive
            ? term1.Value * term2.Value
            : -(term1.Value * term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddGpTerm(ulong id, double scalar1, double scalar2)
    {
        var term = Processor.Gp(id, id);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return AddTerm(term.Id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer AddGpTerm(KeyValuePair<ulong, double> term1, KeyValuePair<ulong, double> term2)
    {
        var term = Processor.Gp(term1.Key, term2.Key);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? term1.Value * term2.Value
            : -(term1.Value * term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer SubtractTerm(ulong basisBlade, double scalar1, double scalar2)
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
    public RGaFloat64MultivectorComposer SubtractTerm(RGaSignedBasisBlade basisBlade)
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
    public RGaFloat64MultivectorComposer SubtractTerm(RGaSignedBasisBlade basisBlade, double scalar)
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
    public RGaFloat64MultivectorComposer SubtractTerm(RGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
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

    public RGaFloat64MultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public RGaFloat64MultivectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, double>();

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

    public RGaFloat64MultivectorComposer MapScalars(Func<ulong, double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, double>();

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

    public RGaFloat64MultivectorComposer MapBasisBlades(Func<ulong, ulong> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, double>();

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

    public RGaFloat64MultivectorComposer MapBasisBlades(Func<ulong, double, ulong> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, double>();

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

    public RGaFloat64MultivectorComposer MapTerms(Func<ulong, double, KeyValuePair<ulong, double>> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, double>();

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
    public RGaFloat64MultivectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Grade().ReverseIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Grade().GradeInvolutionIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Grade().CliffordConjugateIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64MultivectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Processor.HermitianConjugateSign(id) * scalar
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
                    Processor.Signature(p.Key) * p.Value * p.Value
                );

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar GetScalarPart()
    {
        return Processor.Scalar(

            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == 1)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector GetBivectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == 2)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Bivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == grade)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.HigherKVector(grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector GetKVectorPart(int grade)
    {
        return grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => GetScalarPart(),
            1 => GetVectorPart(),
            2 => GetBivectorPart(),
            _ => GetHigherKVector(grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar GetScalar()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key == 0
        );

        return _idScalarDictionary.TryGetValue(0UL, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == 1)
        );

        return Processor.Vector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == 2)
        );

        return Processor.Bivector(_idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == grade)
        );

        return Processor.HigherKVector(grade, _idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector GetKVector(int grade)
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
    public RGaFloat64GradedMultivector GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.MultivectorZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.Multivector(
                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Grade()
            );

        var gradeKVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

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

            var idScalarDictionary = new Dictionary<ulong, double>();

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
    public RGaFloat64UniformMultivector GetUniformMultivector()
    {
        return Processor.UniformMultivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector GetSimpleMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.KVectorTerm(
                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Grade()
            );

        var gradeKVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

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

            var idScalarDictionary = new Dictionary<ulong, double>();

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