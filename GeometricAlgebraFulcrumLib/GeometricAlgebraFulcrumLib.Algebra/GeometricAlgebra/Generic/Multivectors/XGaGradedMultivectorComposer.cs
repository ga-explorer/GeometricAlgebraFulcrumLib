using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaGradedMultivectorComposer<T> :
    XGaMultivectorComposer<T>
{
    private readonly ScalarComposer<T> _scalarComposer;

    private Dictionary<int, Dictionary<IndexSet, T>> _gradeKVectorDictionary
        = new Dictionary<int, Dictionary<IndexSet, T>>();


    public override IEnumerable<int> KVectorGrades
        => _gradeKVectorDictionary.Keys;

    public override IEnumerable<IndexSet> Ids 
        => _gradeKVectorDictionary.SelectMany(d => d.Value.Keys);

    public override IEnumerable<T> Scalars 
        => _gradeKVectorDictionary.SelectMany(d => d.Value.Values);

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _gradeKVectorDictionary.SelectMany(p => p.Value);
    
    public override bool IsZero
        => _gradeKVectorDictionary.Count == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaGradedMultivectorComposer(XGaProcessor<T> processor)
        : base(processor)
    {
        _scalarComposer = ScalarComposer<T>.Create(processor.ScalarProcessor);
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
                    !ScalarProcessor.IsValid(p.Value) ||
                    ScalarProcessor.IsZero(p.Value)
                )) return false;
        }

        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> RemoveTerm(IndexSet id)
    {
        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;

        idScalarDict.Remove(id);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Clear()
    {
        _scalarComposer.Clear();
        _gradeKVectorDictionary = new Dictionary<int, Dictionary<IndexSet, T>>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> ClearScalar()
    {
        _gradeKVectorDictionary.Remove(0);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> ClearVector()
    {
        _gradeKVectorDictionary.Remove(1);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> ClearBivector()
    {
        _gradeKVectorDictionary.Remove(2);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> ClearKVector(int grade)
    {
        _gradeKVectorDictionary.Remove(grade);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetScalarTermScalarValue()
    {
        return _gradeKVectorDictionary.TryGetValue(0, out var idScalarDict) 
            ? idScalarDict.GetValueOrDefault(IndexSet.EmptySet, ScalarProcessor.ZeroValue) 
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetTermScalarValue(IndexSet id)
    {
        return _gradeKVectorDictionary.TryGetValue(id.Count, out var idScalarDict) 
            ? idScalarDict.GetValueOrDefault(id, ScalarProcessor.ZeroValue) 
            : ScalarProcessor.ZeroValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(int scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(long scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(float scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(double scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(string scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromText(scalar));
    }
    
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(T scalarValue)
    {
        if (!ScalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        if (ScalarProcessor.IsZero(scalarValue))
        {
            _gradeKVectorDictionary.Remove(0);

            return this;
        }

        if (_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict[IndexSet.EmptySet] = scalarValue;

            return this;
        }

        idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();
        idScalarDict.Add(IndexSet.EmptySet, scalarValue);

        _gradeKVectorDictionary.Add(0, idScalarDict);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(Scalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(), 
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(), 
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(), 
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(), 
            ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, string scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(), 
            ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, T scalar)
    {
        return SetTerm(index.ToUnitIndexSet(), scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, Scalar<T> scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(),
            scalar.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        return SetTerm(
            index.ToUnitIndexSet(),
            scalar.ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, int scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, long scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, float scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, string scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, T scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, int scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, long scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, float scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, double scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, string scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, T scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, int scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, long scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, float scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, double scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, string scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromText(scalar));
    }

    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, T scalarValue)
    {
        if (id.IsEmptySet) 
            return SetScalarTerm(scalarValue);

        if (!ScalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        var grade = id.Count;

        if (ScalarProcessor.IsZero(scalarValue))
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
                idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();

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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, Scalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SetTerm(IndexSet id, IScalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }


    public XGaGradedMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        var gradeKVectorGroups = 
            termList.Where(
                t => !ScalarProcessor.IsZero(t.Value)
            ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            SetKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaGradedMultivectorComposer<T> SetKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, T>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar))
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

    public XGaGradedMultivectorComposer<T> SetKVector(XGaKVector<T> kv)
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
    
    public XGaGradedMultivectorComposer<T> SetKVectorNegative(XGaKVector<T> kv)
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
                p => ScalarProcessor.Negative(p.Value).ScalarValue
            );

        if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }
    
    public XGaGradedMultivectorComposer<T> SetKVectorScaled(XGaKVector<T> kv, T scalingFactor)
    {
        if (!ScalarProcessor.IsValid(scalingFactor))
            throw new InvalidOperationException();

        var grade = kv.Grade;

        if (kv.IsZero || ScalarProcessor.IsZero(scalingFactor))
        {
            _gradeKVectorDictionary.Remove(grade);

            return this;
        }

        var idScalarDict = 
            kv.ToDictionary(
                p => p.Key, 
                p => ScalarProcessor.Times(p.Value, scalingFactor).ScalarValue
            );

        if (_gradeKVectorDictionary.ContainsKey(grade))
            _gradeKVectorDictionary[grade] = idScalarDict;
        else
            _gradeKVectorDictionary.Add(grade, idScalarDict);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetKVectors(IEnumerable<XGaKVector<T>> kvList)
    {
        foreach (var kv in kvList)
            SetKVector(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetKVectorsNegative(IEnumerable<XGaKVector<T>> kvList)
    {
        foreach (var kv in kvList)
            SetKVectorNegative(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetMultivector(XGaMultivector<T> mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetMultivectorNegative(XGaMultivector<T> mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVectorNegative(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetMultivectorScaled(XGaMultivector<T> mv, T scalingFactor)
    {
        foreach (var kv in mv.GetKVectorParts())
            SetKVectorScaled(kv, scalingFactor);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetKVectorNegative(int grade)
    {
        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;
        
        var idScalarDict1 = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in idScalarDict)
            idScalarDict1.Add(id, ScalarProcessor.Negative(scalar).ScalarValue);

        _gradeKVectorDictionary[grade] = idScalarDict1;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetKVectorsNegative(params int[] gradeList)
    {
        foreach (var grade in gradeList)
            SetKVectorNegative(grade);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SetKVectorsNegative(Func<int, bool> gradeFilter)
    {
        var gradeList = 
            _gradeKVectorDictionary.Keys.Where(gradeFilter).ToArray();

        foreach (var grade in gradeList)
            SetKVectorNegative(grade);

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> ScaleKVector(int grade, T scalingFactor)
    {
        if (!ScalarProcessor.IsValid(scalingFactor))
            throw new InvalidOperationException();

        if (ScalarProcessor.IsZero(scalingFactor))
            return ClearKVector(grade);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
            return this;
        
        var idScalarDict1 = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in idScalarDict)
            idScalarDict1.Add(id, ScalarProcessor.Times(scalar, scalingFactor).ScalarValue);

        _gradeKVectorDictionary[grade] = idScalarDict1;

        return this;
    }


    public override XGaGradedMultivectorComposer<T> AddScalarTerm(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        if (!_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();
            idScalarDict.Add(IndexSet.EmptySet, scalarValue);

            _gradeKVectorDictionary.Add(0, idScalarDict);

            return this;
        }

        var scalarNew = 
            ScalarProcessor.Add(idScalarDict[IndexSet.EmptySet], scalarValue).ScalarValue;

        if (ScalarProcessor.IsZero(scalarNew))
            _gradeKVectorDictionary.Remove(0);
        else
            idScalarDict[IndexSet.EmptySet] = scalarNew;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> AddTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    public override XGaGradedMultivectorComposer<T> AddTerm(IndexSet id, T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();
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

        var scalarNew = ScalarProcessor.Add(scalarOld, scalarValue).ScalarValue;

        if (ScalarProcessor.IsZero(scalarNew))
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
    public XGaGradedMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        var gradeKVectorGroups = 
            termList
                .Where(
                    t => !ScalarProcessor.IsZero(t.Value)
                ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            AddKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaGradedMultivectorComposer<T> AddKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, T>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar))
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
                var scalar = ScalarProcessor.Add(scalarOld, scalarNew).ScalarValue;

                if (ScalarProcessor.IsZero(scalar))
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

    public XGaGradedMultivectorComposer<T> AddKVector(XGaKVector<T> kv)
    {
        if (kv.IsZero) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

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
                var scalar = ScalarProcessor.Add(scalarOld, scalarNew).ScalarValue;

                if (ScalarProcessor.IsZero(scalar))
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
    
    public XGaGradedMultivectorComposer<T> AddKVectorScaled(XGaKVector<T> kv, T scalingFactor)
    {
        if (!ScalarProcessor.IsValid(scalingFactor))
            throw new InvalidOperationException();

        if (kv.IsZero || ScalarProcessor.IsZero(scalingFactor)) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in kv)
            idScalarDict.Add(id, ScalarProcessor.Times(scalar, scalingFactor).ScalarValue);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = ScalarProcessor.Add(scalarOld, scalarNew).ScalarValue;

                if (ScalarProcessor.IsZero(scalar))
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
    public XGaGradedMultivectorComposer<T> AddKVectors(IEnumerable<XGaKVector<T>> kvList)
    {
        foreach (var kv in kvList)
            AddKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> AddMultivector(XGaMultivector<T> mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            AddKVector(kv);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> AddMultivectorScaled(XGaMultivector<T> mv, T scalingFactor)
    {
        foreach (var kv in mv.GetKVectorParts())
            AddKVectorScaled(kv, scalingFactor);

        return this;
    }


    public override XGaGradedMultivectorComposer<T> SubtractScalarTerm(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        if (!_gradeKVectorDictionary.TryGetValue(0, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();
            idScalarDict.Add(IndexSet.EmptySet, ScalarProcessor.Negative(scalarValue).ScalarValue);

            _gradeKVectorDictionary.Add(0, idScalarDict);

            return this;
        }

        var scalarNew = 
            ScalarProcessor.Subtract(idScalarDict[IndexSet.EmptySet], scalarValue).ScalarValue;

        if (ScalarProcessor.IsZero(scalarNew))
            _gradeKVectorDictionary.Remove(0);
        else
            idScalarDict[IndexSet.EmptySet] = scalarNew;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SubtractVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, T scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> SubtractTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    public override XGaGradedMultivectorComposer<T> SubtractTerm(IndexSet id, T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        var grade = id.Count;

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict))
        {
            idScalarDict = IndexSetUtils.CreateIndexSetDictionary<T>();
            idScalarDict.Add(id, ScalarProcessor.Negative(scalarValue).ScalarValue);

            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }
        
        if (!idScalarDict.TryGetValue(id, out var scalarOld))
        {
            if (idScalarDict.ContainsKey(id))
                idScalarDict[id] = ScalarProcessor.Negative(scalarValue).ScalarValue;
            else
                idScalarDict.Add(id, ScalarProcessor.Negative(scalarValue).ScalarValue);

            return this;
        }

        var scalarNew = 
            ScalarProcessor.Subtract(scalarOld, scalarValue).ScalarValue;

        if (ScalarProcessor.IsZero(scalarNew))
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
    public XGaGradedMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        var gradeKVectorGroups = 
            termList.Where(
                t => !ScalarProcessor.IsZero(t.Value)
            ).GroupBy(t => t.Key.Count);

        foreach (var group in gradeKVectorGroups)
            SubtractKVectorTerms(group.Key, group);

        return this;
    }
    
    public XGaGradedMultivectorComposer<T> SubtractKVectorTerms(int grade, IEnumerable<KeyValuePair<IndexSet, T>> idScalarPairs)
    {
        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in idScalarPairs)
        {
            if (id.Count != grade || !ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar))
                idScalarDict.Add(id, ScalarProcessor.Negative(scalar).ScalarValue);
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
                var scalar = ScalarProcessor.Add(scalarOld, scalarNew).ScalarValue;

                if (ScalarProcessor.IsZero(scalar))
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

    public XGaGradedMultivectorComposer<T> SubtractKVector(XGaKVector<T> kv)
    {
        if (kv.IsZero) return this;

        var grade = kv.Grade;

        var idScalarDict = 
            IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in kv)
            idScalarDict.Add(id, ScalarProcessor.Negative(scalar).ScalarValue);

        if (!_gradeKVectorDictionary.TryGetValue(grade, out var idScalarDictOld))
        {
            _gradeKVectorDictionary.Add(grade, idScalarDict);

            return this;
        }

        foreach (var (id, scalarOld) in idScalarDictOld)
        {
            if (idScalarDict.TryGetValue(id, out var scalarNew))
            {
                var scalar = ScalarProcessor.Add(scalarOld, scalarNew).ScalarValue;

                if (ScalarProcessor.IsZero(scalar))
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
    public XGaGradedMultivectorComposer<T> SubtractKVectors(IEnumerable<XGaKVector<T>> kvList)
    {
        foreach (var kv in kvList)
            SubtractKVector(kv);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> SubtractMultivector(XGaMultivector<T> mv)
    {
        foreach (var kv in mv.GetKVectorParts())
            SubtractKVector(kv);

        return this;
    }
    
    
    public XGaGradedMultivectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        var gradeKVectorDict = new Dictionary<int, Dictionary<IndexSet, T>>();

        foreach (var (grade, idScalarDictOld) in _gradeKVectorDictionary)
        {
            var idScalarDict = 
                IndexSetUtils.CreateIndexSetDictionary<T>();

            foreach (var (id, scalarOld) in idScalarDictOld)
            {
                var scalar = mappingFunction(scalarOld);
                
                if (!ScalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                if (!ScalarProcessor.IsZero(scalar))
                    idScalarDict.Add(id, scalar);
            }

            if (idScalarDict.Count > 0)
                gradeKVectorDict.Add(grade, idScalarDict);
        }

        _gradeKVectorDictionary = gradeKVectorDict;

        return this;
    }
    
    public XGaGradedMultivectorComposer<T> MapScalars(Func<IndexSet, T, T> mappingFunction)
    {
        var gradeKVectorDict = new Dictionary<int, Dictionary<IndexSet, T>>();

        foreach (var (grade, idScalarDictOld) in _gradeKVectorDictionary)
        {
            var idScalarDict = 
                IndexSetUtils.CreateIndexSetDictionary<T>();

            foreach (var (id, scalarOld) in idScalarDictOld)
            {
                var scalar = mappingFunction(id, scalarOld);
                
                if (!ScalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                if (!ScalarProcessor.IsZero(scalar))
                    idScalarDict.Add(id, scalar);
            }

            if (idScalarDict.Count > 0)
                gradeKVectorDict.Add(grade, idScalarDict);
        }

        _gradeKVectorDictionary = gradeKVectorDict;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Divide(T scalarFactor)
    {
        var s1 = ScalarProcessor.Inverse(scalarFactor).ScalarValue;

        return MapScalars(s => ScalarProcessor.Times(s, s1).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Reverse()
    {
        return SetKVectorsNegative(
            grade => grade.ReverseIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> GradeInvolution()
    {
        return SetKVectorsNegative(
            grade => grade.GradeInvolutionIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> CliffordConjugate()
    {
        return SetKVectorsNegative(
            grade => grade.CliffordConjugateIsNegativeOfGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivectorComposer<T> Conjugate()
    {
        return MapScalars((id, scalar) =>
            ScalarProcessor.Times(Metric.HermitianConjugateSign(id), scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        var scalarList =
            _gradeKVectorDictionary
                .Values
                .SelectMany(p => p.Values)
                .Select(s => ScalarProcessor.Square(s).ScalarValue);

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
            _gradeKVectorDictionary.Values
                .SelectMany(p => p)
                .Select(p =>
                    ScalarProcessor.Times(
                        Metric.Signature(p.Key), 
                        ScalarProcessor.Square(p.Value).ScalarValue
                    ).ScalarValue
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
        return _gradeKVectorDictionary.TryGetValue(0, out var idScalarDict)
            ? Processor.Scalar(idScalarDict[IndexSet.EmptySet])
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVectorPart()
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var idScalarDict)
            ? Processor.Vector(idScalarDict)
            : Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivectorPart()
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var idScalarDict)
            ? Processor.Bivector(idScalarDict)
            : Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return _gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict)
            ? Processor.HigherKVector(grade, idScalarDict)
            : Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetKVectorPart(int grade)
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
    public XGaScalar<T> GetScalar()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            _gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 0
        );

        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();
        
        return _gradeKVectorDictionary.TryGetValue(0, out var idScalarDict) && 
               idScalarDict.TryGetValue(IndexSet.EmptySet, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVector()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            _gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 1
        );
        
        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();

        return _gradeKVectorDictionary.TryGetValue(1, out var idScalarDict) 
            ? Processor.Vector(idScalarDict) 
            : Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivector()
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 ||
            _gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == 2
        );
        
        if (_gradeKVectorDictionary.Count > 1)
            throw new InvalidOperationException();

        return _gradeKVectorDictionary.TryGetValue(2, out var idScalarDict) 
            ? Processor.Bivector(idScalarDict) 
            : Processor.BivectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVector(int grade)
    {
        Debug.Assert(
            _gradeKVectorDictionary.Count == 0 || 
            _gradeKVectorDictionary.Count == 1 && _gradeKVectorDictionary.Keys.First() == grade
        );
        
        if (grade < 3)
            throw new InvalidOperationException();
        
        return _gradeKVectorDictionary.TryGetValue(grade, out var idScalarDict) 
            ? Processor.HigherKVector(grade, idScalarDict)
            : Processor.HigherKVectorZero(grade);
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetGradedMultivector()
    {
        return _gradeKVectorDictionary.Count == 0
            ? Processor.GradedMultivectorZero
            : Processor.GradedMultivector(
                _gradeKVectorDictionary.ToDictionary(
                    p => p.Key,
                    p => Processor.KVector(p.Key, p.Value)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector()
    {
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.UniformMultivectorZero;

        return Processor.UniformMultivector(
            IdScalarPairs.ToDictionary(
                p => p.Key, 
                p => p.Value
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetSimpleMultivector()
    {
        if (_gradeKVectorDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_gradeKVectorDictionary.Count == 1)
        {
            var (grade, idScalarDict) = 
                _gradeKVectorDictionary.First();

            return Processor.KVector(grade, idScalarDict);
        }

        var gradeKVectorDictionary = new Dictionary<int, XGaKVector<T>>();

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
                IndexSetUtils.CreateIndexSetDictionary<T>();

            idScalarDict.AddRange(idScalarDictionary);

            var kVector = Processor.KVector(
                grade,
                idScalarDict
            );

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return Processor.GradedMultivector(gradeKVectorDictionary);
    }
}