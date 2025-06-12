using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;


public sealed partial class XGaFloat64UniformMultivectorComposer :
    XGaFloat64MultivectorComposer
{
    private readonly Float64ScalarComposer _scalarComposer;

    private Dictionary<IndexSet, double> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<double>();


    public override IEnumerable<int> KVectorGrades
        => _idScalarDictionary.Keys.Select(id => id.Count).Distinct();

    public override IEnumerable<IndexSet> Ids 
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars 
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _idScalarDictionary;
    
    public override bool IsZero
        => _idScalarDictionary.Count == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64UniformMultivectorComposer(XGaFloat64Processor processor)
        : base(processor)
    {
        _scalarComposer = Float64ScalarComposer.Create();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        if (_idScalarDictionary.Count == 0) return true;

        return _idScalarDictionary.All(
            p => 
                p.Key.Count >= 0 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer Clear()
    {
        _scalarComposer.Clear();
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            IndexSet.EmptySet
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer ClearTerm(IndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalarTermScalarValue()
    {
        var key = IndexSet.EmptySet;

        return _idScalarDictionary.GetValueOrDefault(key, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSet.CreatePair(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.GetValueOrDefault(key, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetTermScalarValue(IndexSet basisBlade)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBlade, 0d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer RemoveTerm(IndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SetScalarTerm(double scalar)
    {
        SetTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSet.CreateTriplet(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SetTerm(IndexSet basisBlade, double scalar)
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
    public XGaFloat64UniformMultivectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetScalar(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetScalarNegative(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            -scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SetScalarTerm(
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivector(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivectorNegative(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, -scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivector(XGaFloat64Multivector multivector, double scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer AddScalarTerm(double scalar)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer AddVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddTerm(IPair<int> indexPair, double scalar)
    {
        AddTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer AddTerm(IndexSet basisBlade, double scalar)
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
    public XGaFloat64UniformMultivectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddScalar(XGaFloat64Scalar scalar)
    {
        if (scalar.IsZero)
            return this;

        AddTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SubtractScalarTerm(double scalar)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SubtractVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractTerm(IPair<int> indexPair, double scalar)
    {
        SubtractTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64MultivectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivectorComposer SubtractTerm(IndexSet basisBlade, double scalar)
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
    public XGaFloat64UniformMultivectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractScalar(XGaFloat64Scalar scalar)
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
    public XGaFloat64UniformMultivectorComposer SubtractScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SetTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaFloat64UniformMultivectorComposer SetTerm(XGaSignedBasisBlade basisBlade, double scalar)
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

    public XGaFloat64UniformMultivectorComposer SetTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer AddTerm(IndexSet basisBlade, double scalar1, double scalar2)
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
    public XGaFloat64UniformMultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade)
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
    public XGaFloat64UniformMultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar)
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
    public XGaFloat64UniformMultivectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : -scalar
        );
    }

    public XGaFloat64UniformMultivectorComposer AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer SubtractTerm(IndexSet basisBlade, double scalar1, double scalar2)
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
    public XGaFloat64UniformMultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaFloat64UniformMultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar)
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
    public XGaFloat64UniformMultivectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
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

    public XGaFloat64UniformMultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaFloat64UniformMultivectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64UniformMultivectorComposer MapScalars(Func<IndexSet, double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(id, scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64UniformMultivectorComposer MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
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

    public XGaFloat64UniformMultivectorComposer MapBasisBlades(Func<IndexSet, double, IndexSet> mappingFunction)
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

    public XGaFloat64UniformMultivectorComposer MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> mappingFunction)
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
    public XGaFloat64UniformMultivectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivectorComposer Conjugate()
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
            (_idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key.Count == 0)
        );
        
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        var mv = 
            _idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
                ? Processor.Scalar(scalar)
                : throw new InvalidOperationException();

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );
        
        if (_idScalarDictionary.Count == 0)
            return Processor.VectorZero;

        var mv = 
            Processor.Vector(_idScalarDictionary);
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );
        
        if (_idScalarDictionary.Count == 0)
            return Processor.BivectorZero;

        var mv = 
            Processor.Bivector(_idScalarDictionary);
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVector(int grade)
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );

        if (_idScalarDictionary.Count == 0)
            return Processor.HigherKVectorZero(grade);

        var mv = 
            Processor.HigherKVector(grade, _idScalarDictionary);
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
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
    
    public XGaFloat64GradedMultivector GetGradedMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.GradedMultivectorZero;

        if (_idScalarDictionary.Count == 1)
        {
            var mv = Processor.GradedMultivector(
                _idScalarDictionary.First()
            );

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            return mv;
        }

        {
            var gradeGroup =
                _idScalarDictionary.GroupBy(basisScalarPair => basisScalarPair.Key.Count);

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

            var mv =
                Processor.GradedMultivector(gradeKVectorDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            return mv;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.UniformMultivectorZero;

        var mv = 
            Processor.UniformMultivector(_idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }

    public XGaFloat64Multivector GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_idScalarDictionary.Count == 1)
        {
            var mv = Processor.KVectorTerm(
                _idScalarDictionary.First()
            );

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            return mv;
        }

        {
            var gradeGroup =
                _idScalarDictionary.GroupBy(basisScalarPair => basisScalarPair.Key.Count
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

            XGaFloat64Multivector mv = 
                gradeKVectorDictionary.Count == 1
                    ? gradeKVectorDictionary.First().Value
                    : Processor.GradedMultivector(gradeKVectorDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            return mv;
        }
    }
}