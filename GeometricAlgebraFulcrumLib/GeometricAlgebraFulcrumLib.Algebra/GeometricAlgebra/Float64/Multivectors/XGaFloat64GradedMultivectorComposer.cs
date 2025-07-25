﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64GradedMultivectorComposer :
    XGaFloat64MultivectorComposer
{
    private readonly Float64ScalarComposer _scalarComposer;

    private Dictionary<int, Dictionary<IndexSet, double>> _gradeKVectorDictionary
        = new Dictionary<int, Dictionary<IndexSet, double>>();


    public override IEnumerable<int> KVectorGrades
        => _gradeKVectorDictionary.Keys;

    public override IEnumerable<IndexSet> Ids 
        => _gradeKVectorDictionary.SelectMany(d => d.Value.Keys);

    public override IEnumerable<double> Scalars 
        => _gradeKVectorDictionary.SelectMany(d => d.Value.Values);

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _gradeKVectorDictionary.SelectMany(p => p.Value);
    
    public override bool IsZero
        => _gradeKVectorDictionary.Count == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64GradedMultivectorComposer(XGaFloat64Processor processor)
        : base(processor)
    {
        _scalarComposer = Float64ScalarComposer.Create();
    }
    

    public override bool IsValid()
    {
        if (_gradeKVectorDictionary.Count == 0) 
            return true;

        foreach (var (grade, idScalarDict) in _gradeKVectorDictionary)
        {
            if (grade < 0 || 
                idScalarDict.Any(p => 
                    p.Key.Count != grade ||
                    !p.Value.IsValid() ||
                    p.Value.IsZero()
                )) return false;
        }

        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64MultivectorComposer RemoveTerm(IndexSet id)
    {
        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;

        idScalarDict.Remove(id);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Clear()
    {
        _scalarComposer.Clear();
        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer ClearScalar()
    {
        _gradeKVectorDictionary.Remove(0);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer ClearVector()
    {
        _gradeKVectorDictionary.Remove(1);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer ClearBivector()
    {
        _gradeKVectorDictionary.Remove(2);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer ClearKVector(int grade)
    {
        _gradeKVectorDictionary.Remove(grade);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalarTermScalarValue()
    {
        return _gradeKVectorDictionary.TryGetValue(0, out var idScalarDict) 
            ? idScalarDict.GetValueOrDefault(IndexSet.EmptySet, 0d) 
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetTermScalarValue(IndexSet id)
    {
        return _gradeKVectorDictionary.TryGetValue(id.Count, out var idScalarDict) 
            ? idScalarDict.GetValueOrDefault(id, 0d) 
            : 0d;
    }

    
    public override XGaFloat64GradedMultivectorComposer SetScalarTerm(double scalarValue)
    {
        if (!scalarValue.IsValid())
            throw new InvalidOperationException();

        if (scalarValue.IsZero())
        {
            _gradeKVectorDictionary.Remove(0);

            return this;
        }

        if (_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict[IndexSet.EmptySet] = scalarValue;

            return this;
        }

        idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();
        idScalarDict.Add(IndexSet.EmptySet, scalarValue);

        _gradeKVectorDictionary.Add(0, idScalarDict);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, scalar * sign)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetBivectorTerm(IPair<int> indexPair, double scalar)
    {
        SetTerm(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            IndexSet.CreateTriplet(index1, index2, index3),
            scalar
        );

        return this;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaFloat64GradedMultivectorComposer SetTerm(ulong id, double scalarValue)
    //{
    //    return SetTerm(id.ToUInt64IndexSet(), scalarValue);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SetTerm(int[] indexList, double scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, scalar * sign) 
            : this;
    }

    public override XGaFloat64GradedMultivectorComposer SetTerm(IndexSet id, double scalarValue)
    {
        if (id.IsEmptySet) 
            return SetScalarTerm(scalarValue);

        if (!scalarValue.IsValid())
            throw new InvalidOperationException();

        var grade = id.Count;

        if (scalarValue.IsZero())
        {
            if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict)) 
                return this;
            
            idScalarDict.Remove(id);

            if (idScalarDict.Count == 0)
                _gradeKVectorDictionary.Remove(grade);

            return this;
        }
        
        {
            if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            {
                idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();

                _gradeKVectorDictionary.Add(grade, idScalarDict);
            }

            Debug.Assert(idScalarDict is not null);

            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = scalarValue;
            else
                idScalarDict.Add(id, scalarValue);

            return this;
        }
    }
    
    public XGaFloat64GradedMultivectorComposer SetTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        var gradeKVectorGroups = 
            termList.Where(
                t => !t.Value.IsZero()
            ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            SetKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer SetKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, double>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                idScalarDict.Add(id, scalar);
        }

        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }

    public XGaFloat64GradedMultivectorComposer SetKVector(XGaFloat64KVector kv)
    {
        var grade = kv.Grade;

        if (kv.IsZero)
        {
            _gradeKVectorDictionary.Remove(grade);

            return this;
        }
        
        var idScalarDict = 
            kv.ToDictionary(
                p => p.Key, 
                p => p.Value
            );

        if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer SetKVectorNegative(XGaFloat64KVector kv)
    {
        var grade = kv.Grade;

        if (kv.IsZero)
        {
            _gradeKVectorDictionary.Remove(grade);

            return this;
        }

        var idScalarDict = 
            kv.ToDictionary(
                p => p.Key, 
                p => -p.Value
            );

        if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer SetKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (!scalingFactor.IsValid())
            throw new InvalidOperationException();

        var grade = kv.Grade;

        if (kv.IsZero || scalingFactor.IsZero())
        {
            _gradeKVectorDictionary.Remove(grade);

            return this;
        }

        var idScalarDict = 
            kv.ToDictionary(
                p => p.Key, 
                p => p.Value * scalingFactor
            );

        if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetKVectors(IEnumerable<XGaFloat64KVector> kvList)
    {
        foreach (var kv in kvList)
            SetKVector(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetKVectorsNegative(IEnumerable<XGaFloat64KVector> kvList)
    {
        foreach (var kv in kvList)
            SetKVectorNegative(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetMultivector(XGaFloat64Multivector mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetMultivectorNegative(XGaFloat64Multivector mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVectorNegative(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetMultivectorScaled(XGaFloat64Multivector mv, double scalingFactor)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVectorScaled(kv, scalingFactor);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetKVectorNegative(int grade)
    {
        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;
        
        var idScalarDict1 = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in idScalarDict)
            idScalarDict1.Add(id, -scalar);

        _gradeKVectorDictionary[grade] = idScalarDict1;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetKVectorsNegative(params int[] gradeList)
    {
        foreach (var grade in gradeList)
            SetKVectorNegative(grade);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SetKVectorsNegative(Func<int, bool> gradeFilter)
    {
        var gradeList = 
            _gradeKVectorDictionary.Keys.Where(gradeFilter).ToArray();

        foreach (var grade in gradeList)
            SetKVectorNegative(grade);

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer ScaleKVector(int grade, double scalingFactor)
    {
        if (!scalingFactor.IsValid())
            throw new InvalidOperationException();

        if (scalingFactor.IsZero())
            return ClearKVector(grade);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;
        
        var idScalarDict1 = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in idScalarDict)
            idScalarDict1.Add(id, scalar * scalingFactor);

        _gradeKVectorDictionary[grade] = idScalarDict1;

        return this;
    }


    public override XGaFloat64GradedMultivectorComposer AddScalarTerm(double scalarValue)
    {
        if (scalarValue.IsZero())
            return this;

        if (!_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();
            idScalarDict.Add(IndexSet.EmptySet, scalarValue);

            _gradeKVectorDictionary.Add(0, idScalarDict);

            return this;
        }

        var scalarNew = idScalarDict[IndexSet.EmptySet] + scalarValue;

        if (scalarNew.IsZero())
            _gradeKVectorDictionary.Remove(0);
        else
            idScalarDict[IndexSet.EmptySet] = scalarNew;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer AddTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, scalarValue * sign) 
            : this;
    }

    public override XGaFloat64GradedMultivectorComposer AddTerm(IndexSet id, double scalarValue)
    {
        if (scalarValue.IsZero())
            return this;

        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();
            idScalarDict.Add(id, scalarValue);

            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }
        
        if (!idScalarDict.TryGetValue(id, out var scalarOld))
        {
            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = scalarValue;
            else
                idScalarDict.Add(id, scalarValue);

            return this;
        }

        var scalarNew = scalarOld + scalarValue;

        if (scalarNew.IsZero())
        {
            idScalarDict.Remove(id);

            if (idScalarDict.Count == 0)
                _gradeKVectorDictionary.Remove(grade);
        }
        else
        {
            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = scalarNew;
            else
                idScalarDict.Add(id, scalarNew);
        }

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer AddTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        var gradeKVectorGroups = 
            termList
                .Where(
                    t => !t.Value.IsZero()
                ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            AddKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer AddKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, double>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                idScalarDict.Add(id, scalar);
        }

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            if (idScalarDict.Count == 0)
                _gradeKVectorDictionary.Remove(grade);
            else
                _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = scalarOld + scalarNew;

                if (scalar.IsZero())
                    idScalarDict.Remove(id);
                else
                    idScalarDict[id] = scalar;
            }
            else
            {
                idScalarDict.Add(id, scalarOld);
            }
        }
        
        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else
            _gradeKVectorDictionary[grade] = idScalarDict;
        
        return this;
    }

    public XGaFloat64GradedMultivectorComposer AddKVector(XGaFloat64KVector kv)
    {
        if (kv.IsZero) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in kv)
            idScalarDict.Add(id, scalar);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = scalarOld + scalarNew;

                if (scalar.IsZero())
                    idScalarDict.Remove(id);
                else
                    idScalarDict[id] = scalar;
            }
            else
            {
                idScalarDict.Add(id, scalarOld);
            }
        }
        
        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else
            _gradeKVectorDictionary[grade] = idScalarDict;

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer AddKVectorScaled(XGaFloat64KVector kv, double scalingFactor)
    {
        if (!scalingFactor.IsValid())
            throw new InvalidOperationException();

        if (kv.IsZero || scalingFactor.IsZero()) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in kv)
            idScalarDict.Add(id, scalar * scalingFactor);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = scalarOld + scalarNew;

                if (scalar.IsZero())
                    idScalarDict.Remove(id);
                else
                    idScalarDict[id] = scalar;
            }
            else
            {
                idScalarDict.Add(id, scalarOld);
            }
        }
        
        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else
            _gradeKVectorDictionary[grade] = idScalarDict;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer AddKVectors(IEnumerable<XGaFloat64KVector> kvList)
    {
        foreach (var kv in kvList)
            AddKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer AddMultivector(XGaFloat64Multivector mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            AddKVector(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer AddMultivectorScaled(XGaFloat64Multivector mv, double scalingFactor)
    {
        foreach (var kv in mv.GetKVectorParts())
            AddKVectorScaled(kv, scalingFactor);

        return this;
    }


    public override XGaFloat64GradedMultivectorComposer SubtractScalarTerm(double scalarValue)
    {
        if (scalarValue.IsZero())
            return this;

        if (!_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();
            idScalarDict.Add(IndexSet.EmptySet, -scalarValue);

            _gradeKVectorDictionary.Add(0, idScalarDict);

            return this;
        }

        var scalarNew = idScalarDict[IndexSet.EmptySet] - scalarValue;

        if (scalarNew.IsZero())
            _gradeKVectorDictionary.Remove(0);
        else
            idScalarDict[IndexSet.EmptySet] = scalarNew;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SubtractVectorTerm(int index, double scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SubtractBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64GradedMultivectorComposer SubtractTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, scalarValue * sign) 
            : this;
    }

    public override XGaFloat64GradedMultivectorComposer SubtractTerm(IndexSet id, double scalarValue)
    {
        if (scalarValue.IsZero())
            return this;

        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<double>();
            idScalarDict.Add(id, -scalarValue);

            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }
        
        if (!idScalarDict.TryGetValue(id, out var scalarOld))
        {
            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = -scalarValue;
            else
                idScalarDict.Add(id, -scalarValue);

            return this;
        }

        var scalarNew = scalarOld - scalarValue;

        if (scalarNew.IsZero())
        {
            idScalarDict.Remove(id);

            if (idScalarDict.Count == 0)
                _gradeKVectorDictionary.Remove(grade);
        }
        else
        {
            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = scalarNew;
            else
                idScalarDict.Add(id, scalarNew);
        }

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SubtractTerms(IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        var gradeKVectorGroups = 
            termList.Where(
                t => !t.Value.IsZero()
            ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            SubtractKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer SubtractKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, double>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                idScalarDict.Add(id, -scalar);
        }

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            if (idScalarDict.Count == 0)
                _gradeKVectorDictionary.Remove(grade);
            else
                _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = scalarOld + scalarNew;

                if (scalar.IsZero())
                    idScalarDict.Remove(id);
                else
                    idScalarDict[id] = scalar;
            }
            else
            {
                idScalarDict.Add(id, scalarOld);
            }
        }
        
        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else
            _gradeKVectorDictionary[grade] = idScalarDict;

        return this;
    }

    public XGaFloat64GradedMultivectorComposer SubtractKVector(XGaFloat64KVector kv)
    {
        if (kv.IsZero) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (id, scalar) in kv)
            idScalarDict.Add(id, -scalar);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = scalarOld + scalarNew;

                if (scalar.IsZero())
                    idScalarDict.Remove(id);
                else
                    idScalarDict[id] = scalar;
            }
            else
            {
                idScalarDict.Add(id, scalarOld);
            }
        }

        if (idScalarDict.Count == 0)
            _gradeKVectorDictionary.Remove(grade);
        else
            _gradeKVectorDictionary[grade] = idScalarDict;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SubtractKVectors(IEnumerable<XGaFloat64KVector> kvList)
    {
        foreach (var kv in kvList)
            SubtractKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer SubtractMultivector(XGaFloat64Multivector mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SubtractKVector(kv);

        return this;
    }
    
    
    public XGaFloat64GradedMultivectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        var gradeKVectorDict = new Dictionary<int, Dictionary<IndexSet, double>>();

        foreach (var (grade, idScalarDictOld) in _gradeKVectorDictionary)
        {
            var idScalarDict = 
                IndexSetUtils.CreateIndexSetDictionary<double>();

            foreach (var (id, scalarOld) in idScalarDictOld)
            {
                var scalar = mappingFunction(scalarOld);
                
                if (!scalar.IsValid())
                    throw new InvalidOperationException();

                if (!scalar.IsZero())
                    idScalarDict.Add(id, scalar);
            }

            if (idScalarDict.Count > 0)
                gradeKVectorDict.Add(grade, idScalarDict);
        }

        _gradeKVectorDictionary = gradeKVectorDict;

        return this;
    }
    
    public XGaFloat64GradedMultivectorComposer MapScalars(Func<IndexSet, double, double> mappingFunction)
    {
        var gradeKVectorDict = new Dictionary<int, Dictionary<IndexSet, double>>();

        foreach (var (grade, idScalarDictOld) in _gradeKVectorDictionary)
        {
            var idScalarDict = 
                IndexSetUtils.CreateIndexSetDictionary<double>();

            foreach (var (id, scalarOld) in idScalarDictOld)
            {
                var scalar = mappingFunction(id, scalarOld);
                
                if (!scalar.IsValid())
                    throw new InvalidOperationException();

                if (!scalar.IsZero())
                    idScalarDict.Add(id, scalar);
            }

            if (idScalarDict.Count > 0)
                gradeKVectorDict.Add(grade, idScalarDict);
        }

        _gradeKVectorDictionary = gradeKVectorDict;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Reverse()
    {
        return SetKVectorsNegative(
            grade => grade.ReverseIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer GradeInvolution()
    {
        return SetKVectorsNegative(
            grade => grade.GradeInvolutionIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer CliffordConjugate()
    {
        return SetKVectorsNegative(
            grade => grade.CliffordConjugateIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivectorComposer Conjugate()
    {
        return MapScalars((id, scalar) =>
            Metric.HermitianConjugateSign(id) * scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ENormSquared()
    {
        var scalarList =
            _gradeKVectorDictionary
                .Values
                .SelectMany(p => p.Values)
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
            _gradeKVectorDictionary.Values
                .SelectMany(p => p)
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
        return _gradeKVectorDictionary.TryGetValue(0, out var idScalarDict)
            ? Processor.Scalar(idScalarDict[IndexSet.EmptySet])
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorPart()
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var idScalarDict)
            ? Processor.Vector(idScalarDict)
            : Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivectorPart()
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var idScalarDict)
            ? Processor.Bivector(idScalarDict)
            : Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return _gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict)
            ? Processor.HigherKVector(grade, idScalarDict)
            : Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector GetKVectorPart(int grade)
    {
        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return Processor.KVectorZero(grade);
        
        return grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => Processor.Scalar(idScalarDict[IndexSet.EmptySet]),
            1 => Processor.Vector(idScalarDict),
            2 => Processor.Bivector(idScalarDict),
            _ => Processor.HigherKVectorZero(grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar GetScalar()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            (_gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 0)
        );

        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();
        
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.ScalarZero;
        
        var (grade, idScalarDict) = 
            _gradeKVectorDictionary.First();
        
        if (grade != 0)
            throw new InvalidOperationException();
        
        Debug.Assert(
            idScalarDict.Count == 1 &&
            idScalarDict.Keys.First().IsEmptySet
        );

        var mv = 
            Processor.Scalar(idScalarDict.Values.First());

        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            (_gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 1)
        );
        
        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();

        if (_gradeKVectorDictionary.Count == 0)
            return Processor.VectorZero;

        var (grade, idScalarDict) = 
            _gradeKVectorDictionary.First();

        if (grade != 1)
            throw new InvalidOperationException();

        var mv = 
            Processor.Vector(idScalarDict);

        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivector()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            (_gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 2)
        );
        
        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();

        if (_gradeKVectorDictionary.Count == 0)
            return Processor.BivectorZero;

        var (grade, idScalarDict) = 
            _gradeKVectorDictionary.First();

        if (grade != 2)
            throw new InvalidOperationException();

        var mv = 
            Processor.Bivector(idScalarDict);

        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetHigherKVector(int grade)
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 || 
            (_gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == grade)
        );
        
        if (grade < 3 || _gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();
        
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.HigherKVectorZero(grade);
        
        var (grade1, idScalarDict) = 
            _gradeKVectorDictionary.First();

        if (grade1 != grade)
            throw new InvalidOperationException();

        var mv = 
            Processor.HigherKVector(grade, idScalarDict);

        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivector GetGradedMultivector()
    {
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.GradedMultivectorZero;

        var mv = Processor.GradedMultivector(
                _gradeKVectorDictionary.ToDictionary(
                    p => p.Key,
                    p => Processor.KVector(p.Key, p.Value)
                )
            );

        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector GetUniformMultivector()
    {
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.UniformMultivectorZero;

        var mv = Processor.UniformMultivector(
            IdScalarPairs.ToDictionary(
                p => p.Key, 
                p => p.Value
            )
        );
        
        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector GetMultivector()
    {
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_gradeKVectorDictionary.Count == 1)
        {
            var (grade, idScalarDict) = 
                _gradeKVectorDictionary.First();

            var mv = Processor.KVector(grade, idScalarDict);
        
            _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

            return mv;
        }

        {
            var gradeKVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var (grade, idScalarDictionary) in _gradeKVectorDictionary)
            {
                if (grade == 0)
                {
                    var scalar = Processor.Scalar(
                        idScalarDictionary.Values.First()
                    );

                    gradeKVectorDictionary.Add(grade, scalar);

                    continue;
                }

                var idScalarDict =
                    IndexSetUtils.CreateIndexSetDictionary<double>();

                idScalarDict.AddRange(idScalarDictionary);

                var kVector = Processor.KVector(
                    grade,
                    idScalarDict
                );

                gradeKVectorDictionary.Add(grade, kVector);
            }

            var mv = 
                Processor.GradedMultivector(gradeKVectorDictionary);
        
            _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, double>>();

            return mv;
        }
    }
}