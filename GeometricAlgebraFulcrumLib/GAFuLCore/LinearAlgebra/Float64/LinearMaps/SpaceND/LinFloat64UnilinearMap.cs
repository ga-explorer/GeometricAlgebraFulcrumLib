﻿using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

public class LinFloat64UnilinearMap :
    ILinFloat64UnilinearMap,
    IReadOnlyDictionary<IndexPair, double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1)
    {
        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Negative()
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator +(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
    {
        return new LinFloat64UnilinearMapComposer()
            .SetMap(map1)
            .AddMap(map2)
            .GetMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
    {
        return new LinFloat64UnilinearMapComposer()
            .SetMap(map1)
            .SubtractMap(map2)
            .GetMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator *(double scalar1, LinFloat64UnilinearMap map2)
    {
        var indexVectorDictionary =
            map2._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar1)
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator *(LinFloat64UnilinearMap map1, double scalar2)
    {
        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar2)
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap operator *(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
    {
        return map1.Map(map2);
    }


    private readonly IReadOnlyDictionary<int, LinFloat64Vector> _indexVectorDictionary;

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

    public IEnumerable<double> Values
        => _indexVectorDictionary
            .Values
            .SelectMany(v => v.Values);

    public double this[IndexPair key]
        => _indexVectorDictionary.TryGetValue(key.Index2, out var vector) &&
           vector.TryGetValue(key.Index1, out var scalar)
            ? scalar
            : 0d;

    public LinFloat64Vector this[int key]
        => _indexVectorDictionary.TryGetValue(key, out var mv)
            ? mv : LinFloat64Vector.VectorZero;

    public int VSpaceDimensions
        => _indexVectorDictionary
            .Values
            .Max(v => v.VSpaceDimensions);

    public bool SwapsHandedness { get; }

    public bool IsIdentity()
    {
        throw new NotImplementedException();
    }

    public bool IsReflection()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    public ILinFloat64UnilinearMap GetInverseMap()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> IndexVectorPairs
        => _indexVectorDictionary;

    public IEnumerable<KeyValuePair<IndexPair, double>> IndexScalarPairs
    {
        get
        {
            foreach (var (index2, vector) in _indexVectorDictionary)
            {
                foreach (var (index1, scalar) in vector)
                    yield return new KeyValuePair<IndexPair, double>(
                        new IndexPair(index1, index2),
                        scalar
                    );
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexPair, double>> TransposedIndexScalarPairs
    {
        get
        {
            foreach (var (index1, vector) in _indexVectorDictionary)
            {
                foreach (var (index2, scalar) in vector)
                    yield return new KeyValuePair<IndexPair, double>(
                        new IndexPair(index1, index2),
                        scalar
                    );
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64UnilinearMap(IReadOnlyDictionary<int, LinFloat64Vector> indexVectorDictionary)
    {
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
    public bool TryGetValue(IndexPair key, out double value)
    {
        if (_indexVectorDictionary.TryGetValue(key.Index2, out var vector))
        {
            if (vector.TryGetValue(key.Index1, out value))
                return true;
        }

        value = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVector(int index, out LinFloat64Vector vector)
    {
        return _indexVectorDictionary.TryGetValue(index, out vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsColumnVector(int index2)
    {
        return _indexVectorDictionary.ContainsKey(index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetColumnVector(int index2, out LinFloat64Vector? vector)
    {
        return _indexVectorDictionary.TryGetValue(index2, out vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap GetAdjoint()
    {
        if (_indexVectorDictionary.Count == 0)
            return this;

        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        var group =
            TransposedIndexScalarPairs.GroupBy(
                p => p.Key.Item2
            );

        foreach (var g in group)
        {
            var index = g.Key;

            var vector = g.ToDictionary(
                p => p.Key.Item1,
                p => p.Value
            ).CreateLinVector();

            indexVectorDictionary.Add(index, vector);
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector MapBasisVector(int index)
    {
        return _indexVectorDictionary.TryGetValue(index, out var mv)
            ? mv
            : LinFloat64Vector.VectorZero;
    }

    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var composer = LinFloat64VectorComposer.Create();

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
    public LinFloat64UnilinearMap Map(LinFloat64UnilinearMap map2)
    {
        var indexVectorDictionary =
            map2
                .GetMappedBasisVectors()
                .Select(p =>
                    new KeyValuePair<int, LinFloat64Vector>(p.Key, MapVector(p.Value))
                )
                .Where(p => !p.Value.IsZero)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors()
    {
        return _indexVectorDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return _indexVectorDictionary
            .Where(p => p.Key < vSpaceDimensions)
            .Select(p =>
                new KeyValuePair<int, LinFloat64Vector>(p.Key, p.Value)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap MapScalars(Func<double, double> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            var vector1 = vector.MapScalars(mappingFunc);

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector1);
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap MapScalars(Func<int, int, double, double> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            var vector1 = vector.MapScalars((i, s) => mappingFunc(i, index, s));

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector1);
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    public LinFloat64UnilinearMap GetSubMap(int vSpaceDimensions)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in _indexVectorDictionary)
        {
            if (index >= vSpaceDimensions)
                continue;

            var vector1 = vector.GetSubVector(vSpaceDimensions);

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector);
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] ToArray(int size)
    {
        return ToArray(size, size);
    }

    public double[,] ToArray(int rowCount, int colCount)
    {
        var mapArray =
            new double[rowCount, colCount];

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

    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        return GetSubMap(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexPair, double>> GetEnumerator()
    {
        foreach (var (index2, vector) in _indexVectorDictionary)
            foreach (var (index1, scalar) in vector)
                yield return new KeyValuePair<IndexPair, double>(
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