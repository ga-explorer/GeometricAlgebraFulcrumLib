using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaKVectorComposer<T> :
    XGaMultivectorComposer<T>
{
    private readonly ScalarComposer<T> _scalarComposer;

    private Dictionary<IndexSet, T> _idScalarDictionary
        = IndexSetUtils.CreateIndexSetDictionary<T>();


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

    public override IEnumerable<T> Scalars
    {
        get
        {
            if (IsZero) return [];

            if (IsScalar)
                return [_scalarComposer.ScalarValue];

            return _idScalarDictionary.Values;
        }
    }

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
    {
        get
        {
            if (IsZero) return [];

            if (IsScalar)
                return [new KeyValuePair<IndexSet, T>(
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
            ? _scalarComposer.IsZero() 
            : _idScalarDictionary.Count == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaKVectorComposer(XGaProcessor<T> processor, int grade)
        : base(processor)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        Grade = grade;

        _scalarComposer = ScalarComposer<T>.Create(processor.ScalarProcessor);
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
                ScalarProcessor.IsValid(p.Value) &&
                !ScalarProcessor.IsZero(p.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Clear()
    {
        _scalarComposer.Clear();
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> ClearScalarTerm()
    {
        _scalarComposer.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            index.ToUnitIndexSet()
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> ClearBivectorTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            IndexSet.CreatePair(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> ClearTerm(IndexSet id)
    {
        if (id.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(id);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarTermValue()
    {
        return _scalarComposer.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetScalarTermScalarValue()
    {
        return IsScalar
            ? _scalarComposer.ScalarValue 
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetTermScalarValue(IndexSet id)
    {
        return id.IsEmptySet 
            ? _scalarComposer.ScalarValue 
            : _idScalarDictionary.GetValueOrDefault(id, ScalarProcessor.ZeroValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> RemoveTerm(IndexSet id)
    {
        if (id.IsEmptySet)
            _scalarComposer.Clear();
        else
            _idScalarDictionary.Remove(id);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> RemoveTerm(params int[] indexList)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out _) 
            ? RemoveTerm(id)
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(T scalar)
    {
        if (Grade != 0)
            throw new InvalidOperationException();

        _scalarComposer.SetScalar(scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(int scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(long scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(float scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(double scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromNumber(scalar));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(string scalar)
    {
        return SetScalarTerm(ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(Scalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        return SetScalarTerm(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, string scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, T scalarValue)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, Scalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        var id = index.ToUnitIndexSet();

        return SetTerm(id, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, int scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, long scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, float scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, double scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, string scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, T scalarValue)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, Scalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetBivectorTerm(int index1, int index2, IScalar<T> scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue)
            : this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, int scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, long scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, float scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, string scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, T scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, Scalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, IScalar<T> scalar)
    {
        return new[]{index1, index2, index3}.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, int scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, long scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, float scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, double scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromNumber(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, string scalar)
    {
        return SetTerm(id, ScalarProcessor.ScalarFromText(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, T scalar)
    {
        if (id.IsEmptySet)
            return SetScalarTerm(scalar);

        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
        {
            _idScalarDictionary.Remove(id);
            return this;
        }

        if (id.Count != Grade)
            throw new InvalidOperationException();

        if (_idScalarDictionary.ContainsKey(id))
            _idScalarDictionary[id] = scalar;
        else
            _idScalarDictionary.Add(id, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, Scalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(IndexSet id, IScalar<T> scalar)
    {
        return SetTerm(id, scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, int scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, long scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, float scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, double scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, string scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, Scalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SetTerm(int[] indexList, IScalar<T> scalar)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SetTerm(id, ScalarProcessor.Times(scalar.ScalarValue, sign).ScalarValue) 
            : this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetKVector(XGaKVector<T> kv)
    {
        if (kv is XGaScalar<T> s)
            return SetScalarTerm(s.ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetKVectorNegative(XGaKVector<T> kv)
    {
        if (kv is XGaScalar<T> s)
            return SetScalarTerm(ScalarProcessor.Negative(s.ScalarValue).ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetKVectorScaled(XGaKVector<T> kv, T scalingFactor)
    {
        if (kv is XGaScalar<T> s)
            return SetScalarTerm(ScalarProcessor.Times(s.ScalarValue, scalingFactor).ScalarValue);

        foreach (var (basis, scalar) in kv.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Times(scalar, scalingFactor).ScalarValue);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> AddScalarTerm(T scalar)
    {
        SetScalarTerm(ScalarProcessor.Add(GetScalarTermValue(), scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> AddVectorTerm(int index, T scalar)
    {
        var id = index.ToUnitIndexSet();

        return AddTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> AddBivectorTerm(int index1, int index2, T scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> AddTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? AddTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> AddTerm(IndexSet id, T scalar)
    {
        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (id.IsEmptySet)
            AddScalarTerm(scalar);

        Debug.Assert(ScalarProcessor.IsValid(scalar));
        
        if (id.Count != Grade)
            throw new InvalidOperationException();
        
        if (_idScalarDictionary.TryGetValue(id, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(id);
            else
                _idScalarDictionary[id] = scalar2;
        }
        else
            _idScalarDictionary.Add(id, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddKVector(XGaKVector<T> kv)
    {
        if (kv is XGaScalar<T> s)
            AddScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddKVectorScaled(XGaKVector<T> kv, T scalingFactor)
    {
        if (kv is XGaScalar<T> s)
            AddScalarTerm(ScalarProcessor.Times(s.ScalarValue, scalingFactor).ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            AddTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor).ScalarValue
            );

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SubtractScalarTerm(T scalar)
    {
        SetScalarTerm(ScalarProcessor.Subtract(GetScalarTermValue(), scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SubtractVectorTerm(int index, T scalar)
    {
        var id = index.ToUnitIndexSet();

        return SubtractTerm(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SubtractBivectorTerm(int index1, int index2, T scalar)
    {
        return index1.TryGetBasisBivectorIdSign(index2, out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalar, sign).ScalarValue) 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SubtractTerm(int[] indexList, T scalarValue)
    {
        return indexList.TryGetBasisBladeIdSign(out var id, out var sign) 
            ? SubtractTerm(id, ScalarProcessor.Times(scalarValue, sign).ScalarValue) 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVectorComposer<T> SubtractTerm(IndexSet id, T scalar)
    {
        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (id.IsEmptySet)
            SubtractScalarTerm(scalar);

        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (id.Count != Grade)
            throw new InvalidOperationException();

        if (_idScalarDictionary.TryGetValue(id, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(id);
            else
                _idScalarDictionary[id] = scalar2;
        }
        else
            _idScalarDictionary.Add(id, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractKVector(XGaMultivector<T> kv)
    {
        if (kv is XGaScalar<T> s)
            SubtractScalarTerm(s.ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractKVectorScaled(XGaKVector<T> kv, T scalingFactor)
    {
        if (kv is XGaScalar<T> s)
            SubtractScalarTerm(ScalarProcessor.Times(s.ScalarValue, scalingFactor).ScalarValue);

        foreach (var (basisBlade, scalar) in kv.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor).ScalarValue
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaKVectorComposer<T> SetTerm(XGaSignedBasisBlade basisBlade, T scalar)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexSet, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SetTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddTerm(IndexSet id, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade)
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
    public XGaKVectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar)
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
    public XGaKVectorComposer<T> AddTerm(IXGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : ScalarProcessor.Negative(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> AddTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractTerm(IndexSet id, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2).ScalarValue;

        if (ScalarProcessor.IsZero(scalar))
            return this;

        return SubtractTerm(
            id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade)
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
    public XGaKVectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar)
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
    public XGaKVectorComposer<T> SubtractTerm(XGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<XGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public XGaKVectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (IsZero) return this;

        if (IsScalar)
            return SetScalarTerm(
                mappingFunction(GetScalarTermValue())
            );

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaKVectorComposer<T> MapScalars(Func<IndexSet, T, T> mappingFunction)
    {
        if (IsZero) return this;
        
        if (IsScalar)
            return SetScalarTerm(
                mappingFunction(IndexSet.EmptySet, GetScalarTermValue())
            );

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaKVectorComposer<T> MapBasisBlades(Func<IndexSet, IndexSet> mappingFunction)
    {
        if (IsZero) return this;

        if (IsScalar)
        {
            if (!mappingFunction(IndexSet.EmptySet).IsEmptySet)
                throw new InvalidOperationException();

            return this;
        }

        var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (id, scalar) in IdScalarPairs)
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

        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();
        
        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaKVectorComposer<T> MapBasisBlades(Func<IndexSet, T, IndexSet> mappingFunction)
    {
        if (IsZero) return this;
        
        if (IsScalar)
        {
            if (!mappingFunction(IndexSet.EmptySet, _scalarComposer.ScalarValue).IsEmptySet)
                throw new InvalidOperationException();

            return this;
        }

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
        
        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public XGaKVectorComposer<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> mappingFunction)
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
        
        if (idScalarDictionary.Keys.Any(id => id.Count != Grade))
            throw new InvalidOperationException();

        _idScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Negative()
    {
        if (IsZero) return this;

        if (IsScalar)
            return SetScalarTerm(
                ScalarProcessor.Negative(GetScalarTermValue()).ScalarValue
            );

        _idScalarDictionary = _idScalarDictionary.ToDictionary(
            p => p.Key,
            p => ScalarProcessor.Negative(p.Value).ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Divide(T scalarFactor)
    {
        var s1 = ScalarProcessor.Inverse(scalarFactor).ScalarValue;

        return MapScalars(s => ScalarProcessor.Times(s, s1).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade() ? Negative() : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVectorComposer<T> Conjugate()
    {
        return MapScalars((id, scalar) =>
            ScalarProcessor.Times(Metric.HermitianConjugateSign(id), scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        if (IsScalar)
            return ScalarProcessor.Square(_scalarComposer.ScalarValue);

        var scalarList =
            _idScalarDictionary
                .Values
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
            IdScalarPairs
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
        return _scalarComposer.GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVectorPart()
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
    public XGaBivector<T> GetBivectorPart()
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
    public XGaHigherKVector<T> GetHigherKVectorPart(int grade)
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
        if (!IsScalar)
            throw new InvalidOperationException();
        
        var mv = 
            _scalarComposer.GetXGaScalar(Processor);

        _scalarComposer.Clear();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetVector()
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
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivector()
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
        
        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetTrivector()
    {
        Debug.Assert(
            IsZero ||
            _idScalarDictionary.Keys.All(id => id.Count == Grade)
        );

        if (Grade != 3)
            throw new InvalidOperationException();

        if (_idScalarDictionary.Count == 0)
            return Processor.HigherKVectorZero(Grade);

        var mv = 
            Processor.HigherKVector(Grade, _idScalarDictionary);

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> GetHigherKVector()
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

        _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetKVector()
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
    public XGaGradedMultivector<T> GetGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = new Dictionary<int, XGaKVector<T>>();

        var kVector = GetKVector();
        gradeKVectorDictionary.Add(kVector.Grade, kVector);

        return Processor.GradedMultivector(gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector()
    {
        if (IsZero)
            return Processor.UniformMultivectorZero;

        if (IsScalar)
        {
            var mv = 
                _scalarComposer.GetXGaUniformMultivector(Processor);

            _scalarComposer.Clear();

            return mv;
        }
        else
        {
            var mv = 
                Processor.UniformMultivector(_idScalarDictionary);

            _idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            return mv;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> GetMultivector()
    {
        return IsZero 
            ? Processor.ScalarZero 
            : GetKVector();
    }
}