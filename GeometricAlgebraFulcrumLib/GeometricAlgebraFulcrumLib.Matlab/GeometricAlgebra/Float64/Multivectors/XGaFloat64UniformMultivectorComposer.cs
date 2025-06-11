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


    
    internal XGaFloat64UniformMultivectorComposer(XGaFloat64Processor processor)
        : base(processor)
    {
        _scalarComposer = Float64ScalarComposer.Create();
    }


    
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

    
    public XGaFloat64UniformMultivectorComposer Clear()
    {
        _scalarComposer.Clear();
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            IndexSet.EmptySet
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer ClearTerm(IndexSet basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    
    public override double GetScalarTermScalarValue()
    {
        var key = IndexSet.EmptySet;

        return _idScalarDictionary.GetValueOrDefault(key, 0d);
    }

    
    public double GetTermScalarValue(IPair<int> indexPair)
    {
        var key = IndexSet.CreatePair(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.GetValueOrDefault(key, 0d);
    }

    
    public override double GetTermScalarValue(IndexSet basisBlade)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBlade, 0d);
    }


    
    public new XGaFloat64UniformMultivectorComposer RemoveTerm(IndexSet basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }
    
    
    public new XGaFloat64UniformMultivectorComposer RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }


    
    public new XGaFloat64UniformMultivectorComposer SetScalarTerm(double scalar)
    {
        SetTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    
    public new XGaFloat64UniformMultivectorComposer SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    
    public new XGaFloat64UniformMultivectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }
    
    
    public XGaFloat64UniformMultivectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSet.CreateTriplet(index1, index2, index3),
            scalar
        );

        return this;
    }

    
    public new XGaFloat64UniformMultivectorComposer SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalarValue * sign) 
            : this;
    }

    
    public new XGaFloat64UniformMultivectorComposer SetTerm(IndexSet basisBlade, double scalar)
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


    
    public XGaFloat64UniformMultivectorComposer SetVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SetVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (index, scalar) in termList.ToTuples())
            SetVectorTerm(index, scalar);

        return this;
    }


    
    public XGaFloat64UniformMultivectorComposer SetScalar(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SetScalarNegative(XGaFloat64Scalar scalar)
    {
        SetScalarTerm(
            -scalar.ScalarValue
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SetScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SetScalarTerm(
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivector(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarTuples)
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivectorNegative(XGaFloat64Multivector multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarTuples)
            SetTerm(basis, -scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetMultivector(XGaFloat64Multivector multivector, double scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarTuples)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }


    
    public new XGaFloat64UniformMultivectorComposer AddScalarTerm(double scalar)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    
    public new XGaFloat64UniformMultivectorComposer AddVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    
    public new XGaFloat64UniformMultivectorComposer AddBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, scalar * sign) 
            : this;
    }

    
    public XGaFloat64UniformMultivectorComposer AddTerm(IPair<int> indexPair, double scalar)
    {
        AddTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
    
    
    public new XGaFloat64UniformMultivectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }

    
    public new XGaFloat64UniformMultivectorComposer AddTerm(IndexSet basisBlade, double scalar)
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


    
    public XGaFloat64UniformMultivectorComposer AddVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer AddVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer AddTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            AddTerm(basisBlade, scalar);

        return this;
    }


    
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

    
    public XGaFloat64UniformMultivectorComposer AddScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        AddTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer AddMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarTuples)
            AddTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer AddMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarTuples)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    
    public new XGaFloat64UniformMultivectorComposer SubtractScalarTerm(double scalar)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar
        );

        return this;
    }

    
    public new XGaFloat64UniformMultivectorComposer SubtractVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    
    public new XGaFloat64UniformMultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalar * sign) 
            : this;
    }

    
    public XGaFloat64UniformMultivectorComposer SubtractTerm(IPair<int> indexPair, double scalar)
    {
        SubtractTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
    
    
    public override XGaFloat64MultivectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    
    public new XGaFloat64UniformMultivectorComposer SubtractTerm(IndexSet basisBlade, double scalar)
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


    
    public XGaFloat64UniformMultivectorComposer SubtractVectorTerms(int firstIndex, IEnumerable<double> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SubtractVectorTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList.ToTuples())
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    
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

    
    public XGaFloat64UniformMultivectorComposer SubtractScalar(XGaFloat64Scalar scalar, double scalingFactor)
    {
        SubtractTerm(
            IndexSet.EmptySet,
            scalar.ScalarValue * scalingFactor
        );

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SubtractMultivector(XGaFloat64Multivector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarTuples)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    
    public XGaFloat64UniformMultivectorComposer SubtractMultivector(XGaFloat64Multivector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarTuples)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    
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
        foreach (var (basis, scalar) in termList.ToTuples())
            SetTerm(basis, scalar);

        return this;
    }

    public XGaFloat64UniformMultivectorComposer SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, double>> termList)
    {
        foreach (var (basis, scalar) in termList.ToTuples())
            SetTerm(basis, scalar);

        return this;
    }


    
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
        foreach (var (basis, scalar) in termList.ToTuples())
            AddTerm(basis, scalar);

        return this;
    }

    
    
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
        foreach (var (basis, scalar) in termList.ToTuples())
            AddTerm(basis, scalar);

        return this;
    }


    public XGaFloat64UniformMultivectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

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

    public XGaFloat64UniformMultivectorComposer MapScalars(Func<IndexSet, double, double> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

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

    public XGaFloat64UniformMultivectorComposer MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in _idScalarDictionary.ToTuples())
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

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaFloat64UniformMultivectorComposer MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

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

        _idScalarDictionary = idScalarDictionary;

        return this;
    }


    
    public XGaFloat64UniformMultivectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    
    public XGaFloat64UniformMultivectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    
    public XGaFloat64UniformMultivectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    
    public XGaFloat64UniformMultivectorComposer Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Count.ReverseIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    
    public XGaFloat64UniformMultivectorComposer GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    
    public XGaFloat64UniformMultivectorComposer CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Count.CliffordConjugateIsNegativeOfGrade()
                ? -scalar : scalar
        );
    }

    
    public XGaFloat64UniformMultivectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Metric.HermitianConjugateSign(id) * scalar
        );
    }

    
    public double ENormSquared()
    {
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
            _idScalarDictionary
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
        return Processor.Scalar(
            GetScalarTermScalarValue()
        );
    }

    
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
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            (_idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key.Count == 0)
        );

        return _idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 1)
        );
        
        return Processor.Vector(_idScalarDictionary);
    }

    
    public XGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == 2)
        );
        
        return Processor.Bivector(_idScalarDictionary);
    }
        
    
    public XGaFloat64HigherKVector GetHigherKVector(int grade)
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Count == grade)
        );
        
        return Processor.HigherKVector(grade, _idScalarDictionary);
    }
    
    
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
            return Processor.GradedMultivector(
                _idScalarDictionary.First()
            );

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

        return Processor.GradedMultivector(gradeKVectorDictionary);
    }

    
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        return Processor.UniformMultivector(_idScalarDictionary);
    }

    
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
            : Processor.GradedMultivector(gradeKVectorDictionary);
    }
}