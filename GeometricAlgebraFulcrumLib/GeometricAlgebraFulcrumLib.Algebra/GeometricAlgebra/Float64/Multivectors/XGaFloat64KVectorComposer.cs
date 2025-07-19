using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64KVectorComposer :
    XGaFloat64MultivectorComposer
{
    private readonly Float64ScalarComposer _scalarComposer 
        = Float64ScalarComposer.Create();

    private Dictionary<IndexSet, double> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<double>();


    public int Grade { get; }

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return Grade;
        }
    }

    public override IEnumerable<IndexSet> Ids
    {
        get
        {
            if (IsZero) return [];

            if (IsScalar)
                return [IndexSet.EmptySet];

            return _idScalarDictionary.Keys;
        }
    }

    public override IEnumerable<double> Scalars
    {
        get
        {
            if (IsZero) return [];

            if (IsScalar)
                return [_scalarComposer.ScalarValue];

            return _idScalarDictionary.Values;
        }
    }

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
    {
        get
        {
            if (IsZero) return [];

            if (IsScalar)
                return [new KeyValuePair<IndexSet, double>(
                    IndexSet.EmptySet, 
                    _scalarComposer.ScalarValue
                )];

            return _idScalarDictionary;
        }
    }
    
    public bool IsScalar 
        => Grade == 0;

    public override bool IsZero
        => IsScalar 
            ? _scalarComposer.IsZero 
            : _idScalarDictionary.Count == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64KVectorComposer(XGaFloat64Processor processor, int grade)
        : base(processor)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        Grade = grade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        if (Grade < 0) 
            return false;

        if (IsScalar)
            return _idScalarDictionary.Count == 0 && 
                   _scalarComposer.IsValid();

        if (_idScalarDictionary.Count == 0) 
            return true;

        return _idScalarDictionary.All(
            p => 
                p.Key.Count == Grade &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Clear()
    {
        _scalarComposer.Clear();
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer ClearScalarTerm()
    {
        _scalarComposer.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer ClearBivectorTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer ClearTerm(IndexSet basisBladeId)
    {
        if (basisBladeId.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(basisBladeId);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarTermValue()
    {
        return _scalarComposer.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalarTermScalarValue()
    {
        return IsScalar
            ? _scalarComposer.ScalarValue 
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetTermScalarValue(IndexSet basisBladeId)
    {
        return basisBladeId.IsEmptySet 
            ? _scalarComposer.ScalarValue 
            : _idScalarDictionary.GetValueOrDefault(basisBladeId, 0d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer RemoveTerm(IndexSet basisBladeId)
    {
        if (basisBladeId.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(basisBladeId);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SetScalarTerm(double scalar)
    {
        if (Grade != 0)
            throw new InvalidOperationException();

        _scalarComposer.SetScalarValue(scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSet.CreateTriplet(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaFloat64KVectorComposer SetTerm(ulong id, double scalar)
    //{
    //    return id == 0UL 
    //        ? SetScalarTerm(scalar) 
    //        : SetTerm(id.ToUInt64IndexSet(), scalar);
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SetTerm(IndexSet basisBladeId, double scalar)
    {
        if (basisBladeId.IsEmptySet)
            return SetScalarTerm(scalar);

        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
        {
            _idScalarDictionary.Remove(basisBladeId);
            return this;
        }

        if (basisBladeId.Count != Grade)
            throw new InvalidOperationException();

        if (_idScalarDictionary.ContainsKey(basisBladeId))
            _idScalarDictionary[basisBladeId] = scalar;
        else
            _idScalarDictionary.Add(basisBladeId, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetKVector(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(s.ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetKVectorNegative(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(-s.ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, -scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer AddScalarTerm(double scalar)
    {
        SetScalarTerm(GetScalarTermValue() + scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer AddVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer AddTerm(IndexSet basisBladeId, double scalar)
    {
        if (scalar.IsZero())
            return this;

        if (basisBladeId.IsEmptySet)
            AddScalarTerm(scalar);

        Debug.Assert(scalar.IsValid());
        
        if (basisBladeId.Count != Grade)
            throw new InvalidOperationException();
        
        if (_idScalarDictionary.TryGetValue(basisBladeId, out var scalar1))
        {
            var scalar2 = scalar1 + scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _idScalarDictionary.Remove(basisBladeId);
            else
                _idScalarDictionary[basisBladeId] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBladeId, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddKVector(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            AddScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            AddScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SubtractScalarTerm(double scalar)
    {
        SetScalarTerm(GetScalarTermValue() - scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SubtractVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalar * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVectorComposer SubtractTerm(IndexSet basisBladeId, double scalar)
    {
        if (scalar.IsZero())
            return this;

        if (basisBladeId.IsEmptySet)
            SubtractScalarTerm(scalar);

        Debug.Assert(scalar.IsValid());

        if (basisBladeId.Count != Grade)
            throw new InvalidOperationException();

        if (_idScalarDictionary.TryGetValue(basisBladeId, out var scalar1))
        {
            var scalar2 = scalar1 - scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _idScalarDictionary.Remove(basisBladeId);
            else
                _idScalarDictionary[basisBladeId] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBladeId, -scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractKVector(XGaFloat64Multivector kv)
    {
        if (kv is XGaFloat64Scalar s)
            SubtractScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            SubtractScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaFloat64KVectorComposer SetTerm(XGaSignedBasisBlade basisBlade, double scalar)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddTerm(IndexSet basisBladeId, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return AddTerm(
            basisBladeId,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddTerm(IXGaSignedBasisBlade basisBlade)
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
    public XGaFloat64KVectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar)
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
    public XGaFloat64KVectorComposer AddTerm(IXGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : -scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractTerm(IndexSet basisBladeId, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return SubtractTerm(
            basisBladeId,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaFloat64KVectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar)
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
    public XGaFloat64KVectorComposer SubtractTerm(XGaSignedBasisBlade basisBlade, double scalar1, double scalar2)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaFloat64KVectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (IsZero) return this;

        if (IsScalar)
            return SetScalarTerm(
                mappingFunction(GetScalarTermValue())
            );

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

    public XGaFloat64KVectorComposer MapScalars(Func<IndexSet, double, double> mappingFunction)
    {
        if (IsZero) return this;
        
        if (IsScalar)
            return SetScalarTerm(
                mappingFunction(IndexSet.EmptySet, GetScalarTermValue())
            );

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

    public XGaFloat64KVectorComposer MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
    {
        if (IsZero) return this;

        if (IsScalar)
        {
            if (!mappingFunction(IndexSet.EmptySet).IsEmptySet)
                throw new InvalidOperationException();

            return this;
        }

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in IdScalarPairs)
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

        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();
        
        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64KVectorComposer MapBasisBlades(Func<IndexSet, double, IndexSet> mappingFunction)
    {
        if (IsZero) return this;
        
        if (IsScalar)
        {
            if (!mappingFunction(IndexSet.EmptySet, _scalarComposer.ScalarValue).IsEmptySet)
                throw new InvalidOperationException();

            return this;
        }

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
        
        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64KVectorComposer MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> mappingFunction)
    {
        if (IsZero) return this;
        
        if (IsScalar)
        {
            var (id, scalar) = 
                mappingFunction(
                    IndexSet.EmptySet, 
                    _scalarComposer.ScalarValue
                );

            if (!id.IsEmptySet)
                throw new InvalidOperationException();

            return SetScalarTerm(scalar);
        }

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
        
        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();

        _idScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Negative()
    {
        if (IsZero) return this;

        if (IsScalar)
            return SetScalarTerm(
                -GetScalarTermValue()
            );

        _idScalarDictionary = _idScalarDictionary.ToDictionary(
            p => p.Key,
            p => -p.Value
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Metric.HermitianConjugateSign(id) * scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ENormSquared()
    {
        if (IsScalar)
            return _scalarComposer.ScalarValue.Square();

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
            IdScalarPairs
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
        return _scalarComposer.GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorPart()
    {
        if (Grade != 1 || IsZero)
            return Processor.VectorZero;

        var idScalarDictionary =
            _idScalarDictionary
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivectorPart()
    {
        if (Grade != 2 || IsZero)
            return Processor.BivectorZero;

        var idScalarDictionary =
            _idScalarDictionary
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        if (Grade != grade || IsZero)
            return Processor.HigherKVectorZero(grade);

        var idScalarDictionary =
            _idScalarDictionary
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
        if (!IsScalar)
            throw new InvalidOperationException();
        
        var mv = 
            _scalarComposer.GetXGaFloat64Scalar(Processor);

        _scalarComposer.Clear();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );
        
        if (Grade != 1)
            throw new InvalidOperationException();
        
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
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );

        if (Grade != 2)
            throw new InvalidOperationException();
        
        if (_idScalarDictionary.Count == 0)
            return Processor.BivectorZero;

        var mv = 
            Processor.Bivector(_idScalarDictionary);
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == Grade)
        );

        if (Grade < 3)
            throw new InvalidOperationException();

        if (_idScalarDictionary.Count == 0)
            return Processor.HigherKVectorZero(Grade);

        var mv = 
            Processor.HigherKVector(Grade, _idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector GetKVector()
    {
        if (IsZero)
            return Processor.ScalarZero; 

        return Grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => GetScalar(),
            1 => GetVector(),
            2 => GetBivector(),
            _ => GetHigherKVector()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivector GetGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

        var kVector = GetKVector();
        gradeKVectorDictionary.Add(kVector.Grade, kVector);

        return Processor.GradedMultivector(gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        if (IsZero)
            return Processor.UniformMultivectorZero;

        if (IsScalar)
        {
            var mv = 
                _scalarComposer.GetXGaFloat64UniformMultivector(Processor);

            _scalarComposer.Clear();

            return mv;
        }
        else
        {
            var mv = 
                Processor.UniformMultivector(_idScalarDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            return mv;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector GetMultivector()
    {
        return IsZero 
            ? Processor.ScalarZero 
            : GetKVector();
    }
}