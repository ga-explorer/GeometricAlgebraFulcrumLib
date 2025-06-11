using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

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


    
    internal XGaFloat64KVectorComposer(XGaFloat64Processor processor, int grade)
        : base(processor)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        Grade = grade;
    }


    
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

    
    public XGaFloat64KVectorComposer Clear()
    {
        _scalarComposer.Clear();
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return this;
    }

    
    
    public XGaFloat64KVectorComposer ClearScalarTerm()
    {
        _scalarComposer.Clear();

        return this;
    }

    
    public XGaFloat64KVectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    
    public XGaFloat64KVectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    
    public XGaFloat64KVectorComposer ClearBivectorTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    
    public XGaFloat64KVectorComposer ClearTerm(IndexSet basisBladeId)
    {
        if (basisBladeId.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(basisBladeId);

        return this;
    }

    
    
    public double GetScalarTermValue()
    {
        return _scalarComposer.ScalarValue;
    }


    
    public override double GetScalarTermScalarValue()
    {
        return IsScalar
            ? _scalarComposer.ScalarValue 
            : 0d;
    }

    
    public override double GetTermScalarValue(IndexSet basisBladeId)
    {
        return basisBladeId.IsEmptySet 
            ? _scalarComposer.ScalarValue 
            : _idScalarDictionary.GetValueOrDefault(basisBladeId, 0d);
    }


    
    public new XGaFloat64KVectorComposer RemoveTerm(IndexSet basisBladeId)
    {
        if (basisBladeId.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(basisBladeId);

        return this;
    }
    
    
    public new XGaFloat64KVectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    
    
    public new XGaFloat64KVectorComposer SetScalarTerm(double scalar)
    {
        if (Grade != 0)
            throw new InvalidOperationException();

        _scalarComposer.SetScalarValue(scalar);

        return this;
    }

    
    public new XGaFloat64KVectorComposer SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    
    public new XGaFloat64KVectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }
    
    
    public XGaFloat64KVectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSet.CreateTriplet(index1, index2, index3),
            scalar
        );

        return this;
    }

    
    public XGaFloat64KVectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    
    public new XGaFloat64KVectorComposer SetTerm(IndexSet basisBladeId, double scalar)
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
    
    
    public new XGaFloat64KVectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }


    
    public XGaFloat64KVectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList.ToTuples())
            SetVectorTerm(index, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SetKVector(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(s.ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarTuples)
            SetTerm(basis, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SetKVectorNegative(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(-s.ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarTuples)
            SetTerm(basis, -scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SetKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            return SetScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basis, scalar) in kv.IdScalarTuples)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }

    
    
    public new XGaFloat64KVectorComposer AddScalarTerm(double scalar)
    {
        SetScalarTerm(GetScalarTermValue() + scalar);

        return this;
    }

    
    public new XGaFloat64KVectorComposer AddVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    
    public new XGaFloat64KVectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalar * sign) 
            : this;
    }

    
    public new XGaFloat64KVectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }
    
    
    public new XGaFloat64KVectorComposer AddTerm(IndexSet basisBladeId, double scalar)
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


    
    public XGaFloat64KVectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer AddTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            AddTerm(basisBlade, scalar);

        return this;
    }


    
    public XGaFloat64KVectorComposer AddKVector(XGaFloat64KVector kv)
    {
        if (kv is XGaFloat64Scalar s)
            AddScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarTuples)
            AddTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer AddKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            AddScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basisBlade, scalar) in kv.IdScalarTuples)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }

    
    
    public new XGaFloat64KVectorComposer SubtractScalarTerm(double scalar)
    {
        SetScalarTerm(GetScalarTermValue() - scalar);

        return this;
    }

    
    public new XGaFloat64KVectorComposer SubtractVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    
    public new XGaFloat64KVectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalar * sign) 
            : this;
    }

    
    public new XGaFloat64KVectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }
    
    
    public new XGaFloat64KVectorComposer SubtractTerm(IndexSet basisBladeId, double scalar)
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


    
    public XGaFloat64KVectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SubtractTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    
    public XGaFloat64KVectorComposer SubtractKVector(XGaFloat64Multivector kv)
    {
        if (kv is XGaFloat64Scalar s)
            SubtractScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarTuples)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SubtractKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (kv is XGaFloat64Scalar s)
            SubtractScalarTerm(s.ScalarValue * scalingFactor);

        foreach (var (basisBlade, scalar) in kv.IdScalarTuples)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    
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

    
    public XGaFloat64KVectorComposer SetTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basis, scalar) in termList.ToTuples())
            SetTerm(basis, scalar);

        return this;
    }

    
    public XGaFloat64KVectorComposer SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList.ToTuples())
            SetTerm(basis, scalar);

        return this;
    }


    
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

    
    public XGaFloat64KVectorComposer AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList.ToTuples())
            AddTerm(basis, scalar);

        return this;
    }

    
    
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

    
    public XGaFloat64KVectorComposer SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList.ToTuples())
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

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
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

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
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

        foreach (var (id, scalar) in IdScalarPairs.ToTuples())
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

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
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
                ).ToTuple();

            if (!id.IsEmptySet)
                throw new InvalidOperationException();

            return SetScalarTerm(scalar);
        }

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
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

    
    public XGaFloat64KVectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    
    public XGaFloat64KVectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    
    public XGaFloat64KVectorComposer Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade() ? Negative() : this;
    }

    
    public XGaFloat64KVectorComposer GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade() ? Negative() : this;
    }

    
    public XGaFloat64KVectorComposer CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade() ? Negative() : this;
    }

    
    public XGaFloat64KVectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Metric.HermitianConjugateSign(id) * scalar
        );
    }

    
    public double ENormSquared()
    {
        if (IsScalar)
            return _scalarComposer.ScalarValue.Square();

        var scalarList =
            _idScalarDictionary
                .Values
                .Select(s => s * s);

        return scalarList.Sum();
    }

    
    public double ENorm()
    {
        return ENormSquared().Sqrt();
    }

    
    public double NormSquared()
    {
        var scalarList =
            IdScalarPairs
                .Select(p =>
                    Metric.Signature(p.Key) * p.Value * p.Value
                );

        return scalarList.Sum();
    }

    
    public double Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    
    public XGaFloat64Scalar GetScalarPart()
    {
        return _scalarComposer.GetXGaFloat64Scalar(Processor);
    }

    
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

    
    
    public XGaFloat64Scalar GetScalar()
    {
        return IsScalar
            ? _scalarComposer.GetXGaFloat64Scalar(Processor) 
            : throw new InvalidOperationException();
    }

    
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );

        return Grade == 1 
            ? Processor.Vector(_idScalarDictionary) 
            : throw new InvalidOperationException();
    }

    
    public XGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );

        return Grade == 2 
            ? Processor.Bivector(_idScalarDictionary) 
            : throw new InvalidOperationException();
    }
        
    
    public XGaFloat64HigherKVector GetHigherKVector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == Grade)
        );

        return Grade >= 3 
            ? Processor.HigherKVector(Grade, _idScalarDictionary) 
            : throw new InvalidOperationException();
    }

    
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

    
    public XGaFloat64GradedMultivector GetGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

        var kVector = GetKVector();
        gradeKVectorDictionary.Add(kVector.Grade, kVector);

        return Processor.GradedMultivector(gradeKVectorDictionary);
    }

    
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        if (IsZero)
            return Processor.UniformMultivectorZero;

        return IsScalar
            ? _scalarComposer.GetXGaFloat64UniformMultivector(Processor)
            : Processor.UniformMultivector(_idScalarDictionary);
    }

    
    public XGaFloat64Multivector GetSimpleMultivector()
    {
        return IsZero 
            ? Processor.ScalarZero 
            : GetKVector();
    }
}