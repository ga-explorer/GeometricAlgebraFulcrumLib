using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;

public class LinUnilinearMap<T> :
    ILinUnilinearMap<T>,
    IReadOnlyDictionary<IndexPair, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator -(LinUnilinearMap<T> map1)
    {
        var scalarProcessor = map1.ScalarProcessor;

        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Negative()
            );

        return scalarProcessor.CreateLinUnilinearMap(
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator +(LinUnilinearMap<T> map1, LinUnilinearMap<T> map2)
    {
        return map1.ScalarProcessor
            .CreateLinUnilinearMapComposer()
            .SetMap(map1)
            .AddMap(map2)
            .GetMap();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator -(LinUnilinearMap<T> map1, LinUnilinearMap<T> map2)
    {
        return map1.ScalarProcessor
            .CreateLinUnilinearMapComposer()
            .SetMap(map1)
            .SubtractMap(map2)
            .GetMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator *(T scalar1, LinUnilinearMap<T> map2)
    {
        var scalarProcessor = map2.ScalarProcessor;

        var indexVectorDictionary =
            map2._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar1)
            );

        return scalarProcessor.CreateLinUnilinearMap(
            indexVectorDictionary
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator *(LinUnilinearMap<T> map1, T scalar2)
    {
        var scalarProcessor = map1.ScalarProcessor;

        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar2)
            );

        return scalarProcessor.CreateLinUnilinearMap(
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> operator *(LinUnilinearMap<T> map1, LinUnilinearMap<T> map2)
    {
        return map1.Map(map2);
    }


    private readonly IReadOnlyDictionary<int, LinVector<T>> _indexVectorDictionary;
    
    public IScalarProcessor<T> ScalarProcessor { get; }

    public int Count 
        => _indexVectorDictionary.Values.Sum(v => v.Count);

    public IEnumerable<IndexPair> Keys
    {
        get
        {
            foreach (var (index2, vector) in _indexVectorDictionary)
            foreach (var index1 in vector.Keys)
                yield return new IndexPair(index1, index2);
        }
    }

    public IEnumerable<T> Values 
        => _indexVectorDictionary
            .Values
            .SelectMany(v => v.Values);

    public T this[IndexPair key] 
        => _indexVectorDictionary.TryGetValue(key.Index2, out var vector) && 
           vector.TryGetValue(key.Index1, out var scalar)
            ? scalar
            : ScalarProcessor.ZeroValue;

    public LinVector<T> this[int key] 
        => _indexVectorDictionary.TryGetValue(key, out var mv)
            ? mv : ScalarProcessor.CreateZeroLinVector();

    public int VSpaceDimensions 
        => _indexVectorDictionary
            .Values
            .Max(v => v.VSpaceDimensions);

    public IEnumerable<KeyValuePair<int, LinVector<T>>> IndexVectorPairs
        => _indexVectorDictionary;

    public IEnumerable<KeyValuePair<IndexPair, T>> IndexScalarPairs 
    {
        get
        {
            foreach (var (index2, vector) in _indexVectorDictionary)
            {
                foreach (var (index1, scalar) in vector)
                    yield return new KeyValuePair<IndexPair, T>(
                        new IndexPair(index1, index2), 
                        scalar
                    );
            }
        }
    }
        
    public IEnumerable<KeyValuePair<IndexPair, T>> TransposedIndexScalarPairs 
    {
        get
        {
            foreach (var (index1, vector) in _indexVectorDictionary)
            {
                foreach (var (index2, scalar) in vector)
                    yield return new KeyValuePair<IndexPair, T>(
                        new IndexPair(index1, index2), 
                        scalar
                    );
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinUnilinearMap(IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, LinVector<T>> indexVectorDictionary)
    {
        ScalarProcessor = scalarProcessor;
        _indexVectorDictionary = indexVectorDictionary;

        Debug.Assert(
            IsValid()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _indexVectorDictionary.All(
            p => p.Key >= 0 && p.Value.IsValid()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(IndexPair key)
    {
        return _indexVectorDictionary.TryGetValue(key.Index2, out var vector) &&
               vector.ContainsKey(key.Index1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsVector(int index)
    {
        return _indexVectorDictionary.ContainsKey(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexPair key, out T value)
    {
        if (_indexVectorDictionary.TryGetValue(key.Index2, out var vector))
        {
            if (vector.TryGetValue(key.Index1, out value))
                return true;
        }

        value = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVector(int index, out LinVector<T> vector)
    {
        return _indexVectorDictionary.TryGetValue(index, out vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsColumnVector(int index2)
    {
        return _indexVectorDictionary.ContainsKey(index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetColumnVector(int index2, out LinVector<T>? vector)
    {
        return _indexVectorDictionary.TryGetValue(index2, out vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> GetAdjoint()
    {
        if (_indexVectorDictionary.Count == 0)
            return this;

        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var group = 
            TransposedIndexScalarPairs.GroupBy(
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

        return ScalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapBasisVector(int index)
    {
        return _indexVectorDictionary.TryGetValue(index, out var mv)
            ? mv
            : ScalarProcessor.CreateZeroLinVector();
    }

    public LinVector<T> Map(LinVector<T> vector)
    {
        var composer = ScalarProcessor.CreateLinVectorComposer();

        if (Count <= vector.Count)
        {
            foreach (var (index, mv) in _indexVectorDictionary)
            {
                if (!vector.TryGetTermScalar(index, out var scalar))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }
        else
        {
            foreach (var (index, scalar) in vector)
            {
                if (!_indexVectorDictionary.TryGetValue(index, out var mv))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> Map(LinUnilinearMap<T> map2)
    {
        var indexVectorDictionary =
            map2
                .GetMappedBasisVectors()
                .Select(p => 
                    new KeyValuePair<int, LinVector<T>>(p.Key, Map(p.Value))
                )
                .Where(p => !p.Value.IsZero)
                .ToDictionary(
                    p => p.Key, 
                    p => p.Value
                );

        return ScalarProcessor.CreateLinUnilinearMap(
            indexVectorDictionary
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinVector<T>>> GetMappedBasisVectors()
    {
        return _indexVectorDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinVector<T>>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return _indexVectorDictionary
            .Where(p => p.Key < vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<int, LinVector<T>>(p.Key, p.Value)
            );
    }
        
    public LinUnilinearMap<T> MapScalars(Func<T, T> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            var vector1 = vector.MapScalars(mappingFunc);

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector1);
        }

        return ScalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> MapScalars(Func<T, Scalar<T>> mappingFunc)
    {
        return MapScalars(s => mappingFunc(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> MapScalars(Func<int, int, T, T> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            var vector1 = vector.MapScalars((i, s) => mappingFunc(i, index, s));

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector1);
        }

        return ScalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
        
    public LinUnilinearMap<T> GetSubMap(int vSpaceDimensions)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            if (index >= vSpaceDimensions)
                continue;

            var vector1 = vector.GetSubVector(vSpaceDimensions);

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector);
        }

        return indexVectorDictionary.ToLinUnilinearMap(ScalarProcessor);
    }

    public T[,] ToArray(int size)
    {
        return ToArray(size, size);
    }

    public T[,] ToArray(int rowCount, int colCount)
    {
        var mapArray = 
            ScalarProcessor.CreateArrayZero2D(rowCount, colCount);

        if (_indexVectorDictionary.Count == 0)
            return mapArray;

        var minRowCount = 
            _indexVectorDictionary.Values.Max(v => v.VSpaceDimensions);

        if (rowCount < minRowCount)
            throw new InvalidOperationException();

        var minColCount = _indexVectorDictionary.Keys.Max();

        if (colCount < minColCount)
            throw new InvalidOperationException();

        foreach (var (colIndex, vector) in _indexVectorDictionary)
        foreach (var (rowIndex, scalar) in vector)
            mapArray[rowIndex, colIndex] = scalar;

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> ToUnilinearMap(int vSpaceDimensions)
    {
        return GetSubMap(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexPair, T>> GetEnumerator()
    {
        foreach (var (index2, vector) in _indexVectorDictionary)
        foreach (var (index1, scalar) in vector)
            yield return new KeyValuePair<IndexPair, T>(
                new IndexPair(index1, index2), 
                scalar
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}