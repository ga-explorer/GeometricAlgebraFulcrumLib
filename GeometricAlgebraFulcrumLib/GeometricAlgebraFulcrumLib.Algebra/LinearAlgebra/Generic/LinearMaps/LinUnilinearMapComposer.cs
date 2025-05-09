using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;

public class LinUnilinearMapComposer<T> :
    IReadOnlyDictionary<IndexPair, T>
{
    private Dictionary<IndexPair, T> _indexPairScalarDictionary
        = new Dictionary<IndexPair, T>();


    public IScalarProcessor<T> ScalarProcessor { get; }

    public int Count
        => _indexPairScalarDictionary.Count;

    public IEnumerable<IndexPair> Keys
        => _indexPairScalarDictionary.Keys;

    public IEnumerable<T> Values
        => _indexPairScalarDictionary.Values;

    public T this[IndexPair key]
    {
        get => _indexPairScalarDictionary.TryGetValue(key, out var mv)
            ? mv : ScalarProcessor.ZeroValue;

        set => SetTerm(key.Index1, key.Index2, value);
    }
    
    public T this[int index1, int index2]
    {
        get => _indexPairScalarDictionary.TryGetValue(
                new IndexPair(index1, index2), 
                out var mv
            ) ? mv : ScalarProcessor.ZeroValue;

        set => SetTerm(index1, index2, value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinUnilinearMapComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _indexPairScalarDictionary.Values.All(
            d => ScalarProcessor.IsValid(d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> Clear()
    {
        _indexPairScalarDictionary.Clear();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> RemoveTerm(int index1, int index2)
    {
        var index = new IndexPair(index1, index2);

        _indexPairScalarDictionary.Remove(index);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(IndexPair key)
    {
        return _indexPairScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexPair key, out T value)
    {
        return _indexPairScalarDictionary.TryGetValue(key, out value);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(int index1, int index2)
    {
        var index = new IndexPair(index1, index2);

        return _indexPairScalarDictionary.TryGetValue(index, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(int index1, int index2)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(index1, index2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetTerm(int index1, int index2, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        var index = new IndexPair(index1, index2);

        if (ScalarProcessor.IsZero(scalar))
        {
            _indexPairScalarDictionary.Remove(index);
            return this;
        }

        if (_indexPairScalarDictionary.ContainsKey(index))
            _indexPairScalarDictionary[index] = scalar;
        else
            _indexPairScalarDictionary.Add(index, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetTerm(int index1, int index2, IScalar<T> scalar)
    {
        return SetTerm(index1, index2, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTerm(int index2, LinSignedBasisVector basisVector)
    {
        if (basisVector.IsZero)
            return RemoveTerm(basisVector.Index, index2);

        var scalar = basisVector.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SetTerm(
            basisVector.Index,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTerm(int index2, LinSignedBasisVector basisVector, T scalar)
    {
        if (basisVector.IsZero || ScalarProcessor.IsZero(scalar))
            return RemoveTerm(basisVector.Index, index2);

        var scalar1 = basisVector.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SetTerm(
            basisVector.Index,
            index2,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTerm(int index2, LinVectorTerm<T> term)
    {
        return SetTerm(
            term.Index,
            index2,
            term.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTerm(int index2, LinVectorTerm<T> term, IntegerSign scalar)
    {
        if (scalar.IsZero || term.IsZero)
            return RemoveTerm(term.Index, index2);

        var scalar1 = scalar.IsPositive
            ? term.ScalarValue
            : ScalarProcessor.Negative(term.ScalarValue).ScalarValue;

        return SetTerm(term.Index, index2, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTerm(int index2, LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        return SetTerm(term.Index, index2, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetColumnTermNegative(int index2, LinVectorTerm<T> term)
    {
        return SetTerm(
            term.Index,
            index2,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinUnilinearMapComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexPair, T>> terms)
    {
        foreach (var term in terms)
            SetTerm(term.Key.Index1, term.Key.Index2, term.Value);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnTerms(int index2, IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index1, scalar) in termList)
            SetTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnTerms(int index2, IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            SetColumnTerm(index2, basisVector, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnTerms(int index2, IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            SetTerm(basisVector.Index, index2, scalar.ScalarValue);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnVector(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            SetTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnVectorNegative(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            SetTerm(index1, index2, ScalarProcessor.Negative(scalar));

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnVector(int index2, LinVector<T> vector, T scalingFactor)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }
    
    public LinUnilinearMapComposer<T> SetColumnVector(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SetTerm(index1, index2, vector[index1]);

        return this;
    }

    public LinUnilinearMapComposer<T> SetColumnVector(int index2, IReadOnlyList<T> vector, T scalingFactor)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SetTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(vector[index1], scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SetColumnVectorNegative(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SetTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(vector[index1])
            );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetIdentityMap(int count)
    {
        _indexPairScalarDictionary =
            count
                .GetRange()
                .ToDictionary(
                    i => new IndexPair(i),
                    _ => ScalarProcessor.OneValue
                );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetDiagonalMap(IReadOnlyList<T> vector)
    {
        _indexPairScalarDictionary = new Dictionary<IndexPair, T>();

        for (var i = 0; i < vector.Count; i++)
        {
            var scalar = vector[i];

            if (ScalarProcessor.IsZero(scalar))
                continue;

            _indexPairScalarDictionary.Add(
                new IndexPair(i), 
                scalar
            );
        }

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SetDiagonalMap(LinVector<T> vector)
    {
        _indexPairScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (i, scalar) in vector.IndexScalarPairs)
        {
            _indexPairScalarDictionary.Add(
                new IndexPair(i), 
                scalar
            );
        }

        return this;
    }

    public LinUnilinearMapComposer<T> SetMap(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
            SetTerm(index1, index2, scalar);

        return this;
    }
    
    public LinUnilinearMapComposer<T> SetMap(LinUnilinearMap<T> map, T scalingFactor)
    {
        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
            SetTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SetMapNegative(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
            SetTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(scalar)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddTerm(int index1, int index2, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        var index = new IndexPair(index1, index2);

        if (_indexPairScalarDictionary.TryGetValue(index, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _indexPairScalarDictionary.Remove(index);
            else
                _indexPairScalarDictionary[index] = scalar2;
        }
        else
            _indexPairScalarDictionary.Add(index, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddTerm(int index1, int index2, IScalar<T> scalar)
    {
        return AddTerm(index1, index2, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddTerm(int index1, int index2, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (scalar.IsZero())
            return this;

        return AddTerm(
            index1,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, ILinSignedBasisVector basisVector)
    {
        if (basisVector.IsZero)
            return this;

        var scalar = basisVector.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return AddTerm(
            basisVector.Index,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, ILinSignedBasisVector index, T scalar)
    {
        if (index.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = index.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return AddTerm(
            index.Index,
            index2,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, ILinSignedBasisVector index, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (index.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            index.Index,
            index2,
            index.IsPositive ? scalar : scalar.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return AddTerm(
            term.Index,
            index2,
            term.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, LinVectorTerm<T> term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = scalar == 1
            ? term.ScalarValue
            : ScalarProcessor.Times(term.ScalarValue, scalar).ScalarValue;

        return AddTerm(
            term.Index,
            index2,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTerm(int index2, LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        if (scalar1.IsZero())
            return this;

        return AddTerm(
            term.Index,
            index2,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> AddColumnTermNegative(int index2, LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return AddTerm(
            term.Index,
            index2,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinUnilinearMapComposer<T> AddColumnTerms(int index2, IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            AddColumnTerm(index2, basisVector, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> AddColumnTerms(int index2, IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            AddTerm(basisVector.Index, index2, scalar.ScalarValue);

        return this;
    }

    public LinUnilinearMapComposer<T> AddColumnTerms(int index2, IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index1, scalar) in termList)
            AddTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> AddColumnVector(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            AddTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> AddColumnVector(int index2, LinVector<T> vector, T scalingFactor)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            AddTerm(
                index1,
                index2,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddColumnVectorNegative(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector)
            AddTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(scalar)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddColumnVector(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            AddTerm(index1, index2, vector[index1]);

        return this;
    }

    public LinUnilinearMapComposer<T> AddColumnVector(int index2, IReadOnlyList<T> vector, T scalingFactor)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            AddTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(vector[index1], scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddColumnVectorNegative(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            AddTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(vector[index1])
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddIdentityMap(int count)
    {
        for (var i = 0; i < count; i++)
            AddTerm(i, i, ScalarProcessor.OneValue);

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddDiagonalMap(IReadOnlyList<T> vector)
    {
        for (var i = 0; i < vector.Count; i++)
        {
            var scalar = vector[i];

            if (ScalarProcessor.IsZero(scalar))
                continue;

            AddTerm(i, i, scalar);
        }

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddDiagonalMap(LinVector<T> vector)
    {
        foreach (var (i, scalar) in vector)
            AddTerm(i, i, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> AddMap(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map)
            AddTerm(index1, index2, scalar);

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddMap(LinUnilinearMap<T> map, T scalingFactor)
    {
        foreach (var ((index1, index2), scalar) in map)
            AddTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> AddMapNegative(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map)
            AddTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(scalar)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractTerm(int index1, int index2, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        var index = new IndexPair(index1, index2);

        if (_indexPairScalarDictionary.TryGetValue(index, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _indexPairScalarDictionary.Remove(index);
            else
                _indexPairScalarDictionary[index] = scalar2;
        }
        else
            _indexPairScalarDictionary.Add(index, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractTerm(int index1, int index2, IScalar<T> scalar)
    {
        return SubtractTerm(index1, index2, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractTerm(int index1, int index2, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (scalar.IsZero())
            return this;

        return SubtractTerm(
            index1,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinSignedBasisVector index)
    {
        if (index.IsZero)
            return this;

        var scalar = index.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SubtractTerm(
            index.Index,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinSignedBasisVector index, T scalar)
    {
        if (index.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = index.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SubtractTerm(
            index.Index,
            index2,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinSignedBasisVector index, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (index.IsZero || scalar.IsZero())
            return this;

        if (index.IsNegative)
            scalar = scalar.Negative();

        return SubtractTerm(
            index.Index,
            index2,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return SubtractTerm(
            term.Index,
            index2,
            term.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinVectorTerm<T> term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = scalar == 1
            ? term.ScalarValue
            : ScalarProcessor.Times(term.ScalarValue, scalar).ScalarValue;

        return SubtractTerm(term.Index, index2, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTerm(int index2, LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        if (scalar1.IsZero())
            return this;

        return SubtractTerm(term.Index, index2, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> SubtractColumnTermNegative(int index2, LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return SubtractTerm(
            term.Index,
            index2,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinUnilinearMapComposer<T> SubtractColumnTerms(int index2, IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            AddColumnTerm(index2, basisVector, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractColumnTerms(int index2, IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basisVector, scalar) in termList)
            SubtractTerm(basisVector.Index, index2, scalar.ScalarValue);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractColumnTerms(int index2, IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index1, scalar) in termList)
            SubtractTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractColumnVector(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            SubtractTerm(index1, index2, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractColumnVector(int index2, LinVector<T> vector, T scalingFactor)
    {
        foreach (var (index1, scalar) in vector.IndexScalarPairs)
            SubtractTerm(
                index1,
                index2,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractColumnVectorNegative(int index2, LinVector<T> vector)
    {
        foreach (var (index1, scalar) in vector)
            SubtractTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(scalar)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractColumnVector(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SubtractTerm(index1, index2, vector[index1]);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractColumnVector(int index2, IReadOnlyList<T> vector, T scalingFactor)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SubtractTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(vector[index1], scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractColumnVectorNegative(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SubtractTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(vector[index1])
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractIdentityMap(int count)
    {
        for (var i = 0; i < count; i++)
            SubtractTerm(i, i, ScalarProcessor.OneValue);

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractDiagonalMap(IReadOnlyList<T> vector)
    {
        for (var i = 0; i < vector.Count; i++)
        {
            var scalar = vector[i];

            if (ScalarProcessor.IsZero(scalar))
                continue;

            SubtractTerm(i, i, scalar);
        }

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractDiagonalMap(LinVector<T> vector)
    {
        foreach (var (i, scalar) in vector)
            SubtractTerm(i, i, scalar);

        return this;
    }

    public LinUnilinearMapComposer<T> SubtractMap(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map)
            SubtractTerm(index1, index2, scalar);

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractMap(LinUnilinearMap<T> map, T scalingFactor)
    {
        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
            SubtractTerm(
                index1, 
                index2, 
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }
    
    public LinUnilinearMapComposer<T> SubtractMapNegative(LinUnilinearMap<T> map)
    {
        foreach (var ((index1, index2), scalar) in map)
            SubtractTerm(
                index1, 
                index2, 
                ScalarProcessor.Negative(scalar)
            );

        return this;
    }


    public LinUnilinearMapComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinUnilinearMapComposer<T> MapScalars(Func<int, int, T, T> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var (index1, index2) = id;
            var scalar1 = mappingFunction(index1, index2, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }
    
    public LinUnilinearMapComposer<T> MapScalars(Func<IndexPair, T, T> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id, scalar1);
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinUnilinearMapComposer<T> MapBasisVectors(Func<int, int> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var index1 = mappingFunction(id.Index1);
            var index2 = mappingFunction(id.Index2);

            var index = new IndexPair(index1, index2);

            if (idScalarDictionary.TryGetValue(index, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(index);
                else
                    idScalarDictionary[index] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(index, scalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }
    
    public LinUnilinearMapComposer<T> MapBasisVectors(Func<int, int, IndexPair> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var index = mappingFunction(id.Index1, id.Index2);

            if (idScalarDictionary.TryGetValue(index, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(index);
                else
                    idScalarDictionary[index] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(index, scalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinUnilinearMapComposer<T> MapBasisVectors(Func<IndexPair, IndexPair> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var index = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(index, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(index);
                else
                    idScalarDictionary[index] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(index, scalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinUnilinearMapComposer<T> MapBasisVectors(Func<int, T, int> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var index1 = mappingFunction(id.Index1, scalar);
            var index2 = mappingFunction(id.Index2, scalar);

            var index = new IndexPair(index1, index2);

            if (idScalarDictionary.TryGetValue(index, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(index);
                else
                    idScalarDictionary[index] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(index, scalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }
    
    public LinUnilinearMapComposer<T> MapBasisVectors(Func<IndexPair, T, IndexPair> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var index = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(index, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(index);
                else
                    idScalarDictionary[index] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(index, scalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinUnilinearMapComposer<T> MapTerms(Func<int, int, T, KeyValuePair<IndexPair, T>> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var (termIndex, termScalar) = mappingFunction(id.Index1, id.Index2, scalar);

            if (ScalarProcessor.IsZero(termScalar))
                continue;

            if (idScalarDictionary.TryGetValue(termIndex, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, termScalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(termIndex);
                else
                    idScalarDictionary[termIndex] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(termIndex, termScalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }
    
    public LinUnilinearMapComposer<T> MapTerms(Func<IndexPair, T, KeyValuePair<IndexPair, T>> mappingFunction)
    {
        if (_indexPairScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (id, scalar) in _indexPairScalarDictionary)
        {
            var (termIndex, termScalar) = mappingFunction(id, scalar);

            if (ScalarProcessor.IsZero(termScalar))
                continue;

            if (idScalarDictionary.TryGetValue(termIndex, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, termScalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(termIndex);
                else
                    idScalarDictionary[termIndex] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(termIndex, termScalar);
            }
        }

        _indexPairScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor).ScalarValue);
    }

    public LinUnilinearMapComposer<T> Times(LinUnilinearMap<T> map2)
    {
        var indexPairScalarDictionary = new Dictionary<IndexPair, T>();

        foreach (var (index2, vector) in map2.GetMappedBasisVectors())
        {
            var vector1 = GetMappedColumnVector(vector);

            foreach (var (index1, scalar) in vector1)
                indexPairScalarDictionary.Add(
                    new IndexPair(index1, index2), 
                    scalar
                );
        }

        _indexPairScalarDictionary = indexPairScalarDictionary;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMapComposer<T> Transpose()
    {
        return MapBasisVectors(index => index.Transpose());
    }


    public LinVector<T> GetMappedBasisVector(int index)
    {
        var composer = ScalarProcessor.CreateLinVectorComposer();

        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
        {
            if (index2 != index || ScalarProcessor.IsZero(scalar1))
                continue;
            
            composer.AddTerm(index1, scalar1);
        }

        return composer.GetVector();
    }

    public LinVector<T> GetMappedColumnVector(IReadOnlyList<T> vector)
    {
        var composer = ScalarProcessor.CreateLinVectorComposer();

        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
        {
            if (index2 >= vector.Count || ScalarProcessor.IsZero(scalar1))
                continue;

            var scalar2 = vector[index2];

            if (ScalarProcessor.IsZero(scalar2))
                continue;

            composer.AddTerm(
                index1, 
                ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        return composer.GetVector();
    }

    public LinVector<T> GetMappedColumnVector(LinVector<T> vector)
    {
        var composer = ScalarProcessor.CreateLinVectorComposer();
        
        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
        {
            if (!vector.TryGetTermScalar(index2, out var scalar2))
                continue;

            composer.AddTerm(
                index1, 
                ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        return composer.GetVector();
    }

    public LinUnilinearMap<T> GetMap()
    {
        return GetMapFromColumns();
    }

    public LinUnilinearMap<T> GetMapFromRows()
    {
        if (_indexPairScalarDictionary.Count == 0)
            return new LinUnilinearMap<T>(
                ScalarProcessor,
                new EmptyDictionary<int, LinVector<T>>()
            );

        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var group = _indexPairScalarDictionary.GroupBy(
            p => p.Key.Item1
        );

        foreach (var g in group)
        {
            var index = g.Key;

            var vector = ScalarProcessor.CreateLinVector(
                g.ToDictionary(
                    p => p.Key.Item2,
                    p => p.Value
                )
            );

            indexVectorDictionary.Add(index, vector);
        }

        if (indexVectorDictionary.Count == 1)
            return new LinUnilinearMap<T>(
                ScalarProcessor,
                new SingleItemDictionary<int, LinVector<T>>(indexVectorDictionary.First())
            );

        return new LinUnilinearMap<T>(
            ScalarProcessor,
            indexVectorDictionary
        );
    }

    public LinUnilinearMap<T> GetMapFromColumns()
    {
        if (_indexPairScalarDictionary.Count == 0)
            return new LinUnilinearMap<T>(
                ScalarProcessor,
                new EmptyDictionary<int, LinVector<T>>()
            );

        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var group = _indexPairScalarDictionary.GroupBy(
            p => p.Key.Item2
        );

        foreach (var g in group)
        {
            var index = g.Key;

            var vector = ScalarProcessor.CreateLinVector(
                g.ToDictionary(
                    p => p.Key.Item1,
                    p => p.Value
                )
            );

            indexVectorDictionary.Add(index, vector);
        }

        if (indexVectorDictionary.Count == 1)
            return new LinUnilinearMap<T>(
                ScalarProcessor,
                new SingleItemDictionary<int, LinVector<T>>(indexVectorDictionary.First())
            );

        return new LinUnilinearMap<T>(
            ScalarProcessor,
            indexVectorDictionary
        );
    }

    public T[,] GetMapArray(int rowCount, int colCount)
    {
        var mapArray =
            ScalarProcessor.CreateArrayZero2D(rowCount, colCount);

        if (_indexPairScalarDictionary.Count == 0)
            return mapArray;

        foreach (var ((rowIndex, colIndex), scalar) in _indexPairScalarDictionary)
        {
            if (!ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            mapArray[rowIndex, colIndex] = scalar;
        }

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexPair, T>> GetEnumerator()
    {
        return _indexPairScalarDictionary.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}