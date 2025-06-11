using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

public class LinFloat64UnilinearMap :
    ILinFloat64UnilinearMap,
    IReadOnlyDictionary<IndexPair, double>
{
    
    public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1)
    {
        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Negative()
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    
    public static LinFloat64UnilinearMap operator +(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
    {
        return new LinFloat64UnilinearMapComposer()
            .SetMap(map1)
            .AddMap(map2)
            .GetMap();
    }

    
    public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
    {
        return new LinFloat64UnilinearMapComposer()
            .SetMap(map1)
            .SubtractMap(map2)
            .GetMap();
    }

    
    public static LinFloat64UnilinearMap operator *(double scalar1, LinFloat64UnilinearMap map2)
    {
        var indexVectorDictionary =
            map2._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar1)
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    
    public static LinFloat64UnilinearMap operator *(LinFloat64UnilinearMap map1, double scalar2)
    {
        var indexVectorDictionary =
            map1._indexVectorDictionary.ToDictionary(
                p => p.Key,
                p => p.Value.Times(scalar2)
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    
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
            foreach (var (index2, vector) in _indexVectorDictionary.ToTuples())
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
            ? mv : LinFloat64Vector.Zero;

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
            foreach (var (index2, vector) in _indexVectorDictionary.ToTuples())
            {
                foreach (var (index1, scalar) in vector.ToTuples())
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
            foreach (var (index1, vector) in _indexVectorDictionary.ToTuples())
            {
                foreach (var (index2, scalar) in vector.ToTuples())
                    yield return new KeyValuePair<IndexPair, double>(
                        new IndexPair(index1, index2),
                        scalar
                    );
            }
        }
    }


    
    internal LinFloat64UnilinearMap(IReadOnlyDictionary<int, LinFloat64Vector> indexVectorDictionary)
    {
        _indexVectorDictionary = indexVectorDictionary;

        Debug.Assert(
            IsValid()
        );
    }


    
    public bool IsValid()
    {
        return _indexVectorDictionary.All(
            p => p.Key >= 0 && p.Value.IsValid()
        );
    }

    
    public bool ContainsKey(IndexPair key)
    {
        return _indexVectorDictionary.TryGetValue(key.Index2, out var vector) &&
               vector.ContainsKey(key.Index1);
    }

    
    public bool ContainsVector(int index)
    {
        return _indexVectorDictionary.ContainsKey(index);
    }

    
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

    
    public bool TryGetVector(int index, out LinFloat64Vector vector)
    {
        return _indexVectorDictionary.TryGetValue(index, out vector);
    }

    
    public bool ContainsColumnVector(int index2)
    {
        return _indexVectorDictionary.ContainsKey(index2);
    }

    
    public bool TryGetColumnVector(int index2, out LinFloat64Vector? vector)
    {
        return _indexVectorDictionary.TryGetValue(index2, out vector);
    }

    
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

    
    public LinFloat64Vector MapBasisVector(int index)
    {
        return _indexVectorDictionary.TryGetValue(index, out var mv)
            ? mv
            : LinFloat64Vector.Zero;
    }

    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var composer = LinFloat64VectorComposer.Create();

        if (Count <= vector.Count)
        {
            foreach (var (index, mv) in _indexVectorDictionary.ToTuples())
            {
                if (!vector.TryGetTermScalar(index, out var scalar))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }
        else
        {
            foreach (var (index, scalar) in vector.ToTuples())
            {
                if (!_indexVectorDictionary.TryGetValue(index, out var mv))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }

        return composer.GetVector();
    }

    
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

    
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors()
    {
        return _indexVectorDictionary;
    }

    
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return _indexVectorDictionary
            .Where(p => p.Key < vSpaceDimensions)
            .Select(p =>
                new KeyValuePair<int, LinFloat64Vector>(p.Key, p.Value)
            );
    }

    
    public LinFloat64UnilinearMap MapScalars(Func<double, double> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in _indexVectorDictionary.ToTuples())
        {
            var vector1 = vector.MapScalars(mappingFunc);

            if (vector1.IsZero)
                continue;

            indexVectorDictionary.Add(index, vector1);
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    
    public LinFloat64UnilinearMap MapScalars(Func<int, int, double, double> mappingFunc)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in _indexVectorDictionary.ToTuples())
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

        foreach (var (index, vector) in _indexVectorDictionary.ToTuples())
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

        foreach (var (colIndex, vector) in _indexVectorDictionary.ToTuples())
            foreach (var (rowIndex, scalar) in vector.ToTuples())
                mapArray[rowIndex, colIndex] = scalar;

        return mapArray;
    }

    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    
    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        return GetSubMap(vSpaceDimensions);
    }

    
    public XGaFloat64LinearMapOutermorphism ToOutermorphism(XGaFloat64Processor metric)
    {
        return new XGaFloat64LinearMapOutermorphism(metric, this);
    }

    
    public double[,] GetMapArray(int size)
    {
        return ToArray(size, size);
    }

    
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetColumns()
    {
        return GetMappedBasisVectors();
    }

    
    public LinFloat64Vector GetColumn(int colIndex)
    {
        return MapBasisVector(colIndex);
    }

    
    public LinFloat64Vector GetScaledColumn(int colIndex, double scalingFactor)
    {
        return MapBasisVector(colIndex).Times(scalingFactor);
    }

    
    public LinFloat64Vector GetMappedColumn(int colIndex, Func<double, double> scalarMapping)
    {
        return MapBasisVector(colIndex).MapScalars(scalarMapping);
    }

    
    public LinFloat64Vector GetMappedColumn(int colIndex, Func<int, double, double> indexScalarMapping)
    {
        return MapBasisVector(colIndex).MapScalars(indexScalarMapping);
    }


    public LinFloat64Vector CombineColumns(IReadOnlyList<double> scalarList, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {
        var vector = LinFloat64Vector.Zero;

        var count = scalarList.Count;
        for (var columnIndex = 0; columnIndex < count; columnIndex++)
        {
            if (!TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
                continue;

            var scalingFactor = scalarList[columnIndex];
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public LinFloat64Vector CombineColumns(LinFloat64Vector scalingVector, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {

        var vector = LinFloat64Vector.Zero;

        foreach (var (columnIndex, scalingFactor) in scalingVector.ToTuples())
        {
            if (!TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
                continue;

            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    
    public LinFloat64UnilinearMap CombineColumns(LinFloat64UnilinearMap map2, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {
        var vectorsDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in map2.GetColumns().ToTuples())
            vectorsDictionary.Add(
                index,
                CombineColumns(vector, scalingFunc, reducingFunc)
            );

        return vectorsDictionary.ToLinUnilinearMap();
    }

    
    public LinFloat64Vector MapVector(IReadOnlyList<double> vector)
    {
        var composer = LinFloat64VectorComposer.Create();

        if (Count <= vector.Count)
        {
            foreach (var (index, mv) in IndexVectorPairs.ToTuples())
            {
                if (index >= vector.Count)
                    continue;

                composer.AddVector(mv, vector[index]);
            }
        }
        else
        {
            for (var index = 0; index < vector.Count; index++)
            {
                if (!TryGetVector(index, out var mv))
                    continue;

                composer.AddVector(mv, vector[index]);
            }
        }

        return composer.GetVector();
    }

    public LinFloat64Vector MapVector(IReadOnlyDictionary<int, double> vector)
    {
        var composer = LinFloat64VectorComposer.Create();

        if (Count <= vector.Count)
        {
            foreach (var (index, mv) in IndexVectorPairs.ToTuples())
            {
                if (!vector.TryGetValue(index, out var scalar))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }
        else
        {
            foreach (var (index, scalar) in vector.ToTuples())
            {
                if (!TryGetVector(index, out var mv))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }

        return composer.GetVector();
    }

    
    
    public LinFloat64UnilinearMap AbsScalars()
    {
        return MapScalars(Math.Abs);
    }

    
    public LinFloat64UnilinearMap Sqrt()
    {
        return MapScalars(Math.Sqrt);
    }

    
    public LinFloat64UnilinearMap SqrtOfAbs()
    {
        return MapScalars(s => s.SqrtOfAbs());
    }

    
    public LinFloat64UnilinearMap Exp()
    {
        return MapScalars(Math.Exp);
    }

    
    public LinFloat64UnilinearMap LogE()
    {
        return MapScalars(s => s.LogE());
    }

    
    public LinFloat64UnilinearMap Log2()
    {
        return MapScalars(x => x.Log2());
    }

    
    public LinFloat64UnilinearMap Log10()
    {
        return MapScalars(Math.Log10);
    }

    
    public LinFloat64UnilinearMap Log(double scalar)
    {
        return MapScalars(s => Math.Log(s, scalar));
    }

    
    public LinFloat64UnilinearMap Power(double scalar)
    {
        return MapScalars(s => Math.Pow(s, scalar));
    }

    
    public LinFloat64UnilinearMap Cos()
    {
        return MapScalars(Math.Cos);
    }

    
    public LinFloat64UnilinearMap Sin()
    {
        return MapScalars(Math.Sin);
    }

    
    public LinFloat64UnilinearMap Tan()
    {
        return MapScalars(Math.Tan);
    }

    
    public LinFloat64UnilinearMap ArcCos()
    {
        return MapScalars(s => s.ArcCos());
    }

    
    public LinFloat64UnilinearMap ArcSin()
    {
        return MapScalars(s => s.ArcSin());
    }

    
    public LinFloat64UnilinearMap ArcTan()
    {
        return MapScalars(s => s.ArcTan());
    }

    
    public LinFloat64UnilinearMap Cosh()
    {
        return MapScalars(Math.Cosh);
    }

    
    public LinFloat64UnilinearMap Sinh()
    {
        return MapScalars(Math.Sinh);
    }

    
    public LinFloat64UnilinearMap Tanh()
    {
        return MapScalars(Math.Tanh);
    }

    
    //
    //public double[,] GetMapArray(int size)
    //{
    //    return map.ToArray(size, size);
    //}

    //
    //public IEnumerable<KeyValuePair<int, Float64Tuple3D>> GetColumns()
    //{
    //    return map.GetMappedBasisVectors();
    //}

    //
    //public Float64Tuple3D GetColumn(int colIndex)
    //{
    //    return map.MapBasisVector(colIndex);
    //}

    //
    //public Float64Tuple3D GetScaledColumn(int colIndex, double scalingFactor)
    //{
    //    return map.MapBasisVector(colIndex).Times(scalingFactor);
    //}

    //
    //public Float64Tuple3D GetMappedColumn(int colIndex, Func<double, double> scalarMapping)
    //{
    //    return map.MapBasisVector(colIndex).MapScalars(scalarMapping);
    //}

    //
    //public Float64Tuple3D GetMappedColumn(int colIndex, Func<int, double, double> indexScalarMapping)
    //{
    //    return map.MapBasisVector(colIndex).MapScalars(indexScalarMapping);
    //}


    //public Float64Tuple3D CombineColumns(IReadOnlyList<double> scalarList, Func<double, Float64Tuple3D, Float64Tuple3D> scalingFunc, Func<Float64Tuple3D, Float64Tuple3D, Float64Tuple3D> reducingFunc)
    //{
    //    var vector = Float64Tuple3D.Zero;

    //    var count = scalarList.Count;
    //    for (var columnIndex = 0; columnIndex < count; columnIndex++)
    //    {
    //        if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
    //            continue;

    //        var scalingFactor = scalarList[columnIndex];
    //        var scaledVector = scalingFunc(scalingFactor, columnVector);

    //        vector = reducingFunc(vector, scaledVector);
    //    }

    //    return vector;
    //}

    //public Float64Tuple3D CombineColumns(Float64Tuple3D scalingVector, Func<double, Float64Tuple3D, Float64Tuple3D> scalingFunc, Func<Float64Tuple3D, Float64Tuple3D, Float64Tuple3D> reducingFunc)
    //{

    //    var vector = Float64Tuple3D.VectorZero;

    //    foreach (var (columnIndex, scalingFactor) in scalingVector)
    //    {
    //        if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
    //            continue;

    //        var scaledVector = scalingFunc(scalingFactor, columnVector);

    //        vector = reducingFunc(vector, scaledVector);
    //    }

    //    return vector;
    //}

    //
    //public LinFloat64UnilinearMap CombineColumns(LinFloat64UnilinearMap map2, Func<double, Float64Tuple3D, Float64Tuple3D> scalingFunc, Func<Float64Tuple3D, Float64Tuple3D, Float64Tuple3D> reducingFunc)
    //{
    //    var vectorsDictionary = new Dictionary<int, Float64Tuple3D>();

    //    foreach (var (index, vector) in map2.GetColumns())
    //        vectorsDictionary.Add(
    //            index,
    //            map1.CombineColumns(vector, scalingFunc, reducingFunc)
    //        );

    //    return vectorsDictionary.ToLinUnilinearMap();
    //}

    
    //public Float64Tuple3D MapVector(IReadOnlyList<double> vector)
    //{
    //    var composer = new Float64Tuple3DComposer();

    //    if (map.Count <= vector.Count)
    //    {
    //        foreach (var (index, mv) in map.IndexVectorPairs)
    //        {
    //            if (index >= vector.Count)
    //                continue;

    //            composer.AddVector(mv, vector[index]);
    //        }
    //    }
    //    else
    //    {
    //        for (var index = 0; index < vector.Count; index++)
    //        {
    //            if (!map.TryGetVector(index, out var mv))
    //                continue;

    //            composer.AddVector(mv, vector[index]);
    //        }
    //    }

    //    return composer.GetVector();
    //}

    //public Float64Tuple3D MapVector(IReadOnlyDictionary<int, double> vector)
    //{
    //    var composer = new Float64Tuple3DComposer();

    //    if (map.Count <= vector.Count)
    //    {
    //        foreach (var (index, mv) in map.IndexVectorPairs)
    //        {
    //            if (!vector.TryGetValue(index, out var scalar))
    //                continue;

    //            composer.AddVector(mv, scalar);
    //        }
    //    }
    //    else
    //    {
    //        foreach (var (index, scalar) in vector)
    //        {
    //            if (!map.TryGetVector(index, out var mv))
    //                continue;

    //            composer.AddVector(mv, scalar);
    //        }
    //    }

    //    return composer.GetVector();
    //}

    
    //
    //public LinFloat64UnilinearMap AbsScalars()
    //{
    //    return v1.MapScalars(Math.Abs);
    //}

    //
    //public LinFloat64UnilinearMap Sqrt()
    //{
    //    return v1.MapScalars(Math.Sqrt);
    //}

    //
    //public LinFloat64UnilinearMap SqrtOfAbs()
    //{
    //    return v1.MapScalars(s => s.SqrtOfAbs());
    //}

    //
    //public LinFloat64UnilinearMap Exp()
    //{
    //    return v1.MapScalars(Math.Exp);
    //}

    //
    //public LinFloat64UnilinearMap LogE()
    //{
    //    return v1.MapScalars(s => s.LogE());
    //}

    //
    //public LinFloat64UnilinearMap Log2()
    //{
    //    return v1.MapScalars(Math.Log2);
    //}

    //
    //public LinFloat64UnilinearMap Log10()
    //{
    //    return v1.MapScalars(Math.Log10);
    //}

    //
    //public LinFloat64UnilinearMap Log(, double scalar)
    //{
    //    return v1.MapScalars(s => Math.Log(s, scalar));
    //}

    //
    //public LinFloat64UnilinearMap Log(this double scalar, LinFloat64UnilinearMap v1)
    //{
    //    return v1.MapScalars(s => Math.Log(scalar, s));
    //}

    //
    //public LinFloat64UnilinearMap Power(, double scalar)
    //{
    //    return v1.MapScalars(s => Math.Pow(s, scalar));
    //}

    
    //
    //public LinFloat64UnilinearMap Cos()
    //{
    //    return v1.MapScalars(Math.Cos);
    //}

    //
    //public LinFloat64UnilinearMap Sin()
    //{
    //    return v1.MapScalars(Math.Sin);
    //}

    //
    //public LinFloat64UnilinearMap Tan()
    //{
    //    return v1.MapScalars(Math.Tan);
    //}

    //
    //public LinFloat64UnilinearMap ArcCos()
    //{
    //    return v1.MapScalars(Math.Acos);
    //}

    //
    //public LinFloat64UnilinearMap ArcSin()
    //{
    //    return v1.MapScalars(Math.Asin);
    //}

    //
    //public LinFloat64UnilinearMap ArcTan()
    //{
    //    return v1.MapScalars(Math.Atan);
    //}

    //
    //public LinFloat64UnilinearMap Cosh()
    //{
    //    return v1.MapScalars(Math.Cosh);
    //}

    //
    //public LinFloat64UnilinearMap Sinh()
    //{
    //    return v1.MapScalars(Math.Sinh);
    //}

    //
    //public LinFloat64UnilinearMap Tanh()
    //{
    //    return v1.MapScalars(Math.Tanh);
    //}

    
    //
    //public double[,] GetMapArray(int size)
    //{
    //    return map.ToArray(size, size);
    //}

    //
    //public IEnumerable<KeyValuePair<int, Float64Tuple4D>> GetColumns()
    //{
    //    return map.GetMappedBasisVectors();
    //}

    //
    //public Float64Tuple4D GetColumn(int colIndex)
    //{
    //    return map.MapBasisVector(colIndex);
    //}

    //
    //public Float64Tuple4D GetScaledColumn(int colIndex, double scalingFactor)
    //{
    //    return map.MapBasisVector(colIndex).Times(scalingFactor);
    //}

    //
    //public Float64Tuple4D GetMappedColumn(int colIndex, Func<double, double> scalarMapping)
    //{
    //    return map.MapBasisVector(colIndex).MapScalars(scalarMapping);
    //}

    //
    //public Float64Tuple4D GetMappedColumn(int colIndex, Func<int, double, double> indexScalarMapping)
    //{
    //    return map.MapBasisVector(colIndex).MapScalars(indexScalarMapping);
    //}


    //public Float64Tuple4D CombineColumns(IReadOnlyList<double> scalarList, Func<double, Float64Tuple4D, Float64Tuple4D> scalingFunc, Func<Float64Tuple4D, Float64Tuple4D, Float64Tuple4D> reducingFunc)
    //{
    //    var vector = Float64Tuple4D.Zero;

    //    var count = scalarList.Count;
    //    for (var columnIndex = 0; columnIndex < count; columnIndex++)
    //    {
    //        if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
    //            continue;

    //        var scalingFactor = scalarList[columnIndex];
    //        var scaledVector = scalingFunc(scalingFactor, columnVector);

    //        vector = reducingFunc(vector, scaledVector);
    //    }

    //    return vector;
    //}

    //public Float64Tuple4D CombineColumns(Float64Tuple4D scalingVector, Func<double, Float64Tuple4D, Float64Tuple4D> scalingFunc, Func<Float64Tuple4D, Float64Tuple4D, Float64Tuple4D> reducingFunc)
    //{

    //    var vector = Float64Tuple4D.VectorZero;

    //    foreach (var (columnIndex, scalingFactor) in scalingVector)
    //    {
    //        if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
    //            continue;

    //        var scaledVector = scalingFunc(scalingFactor, columnVector);

    //        vector = reducingFunc(vector, scaledVector);
    //    }

    //    return vector;
    //}

    //
    //public LinFloat64UnilinearMap CombineColumns(LinFloat64UnilinearMap map2, Func<double, Float64Tuple4D, Float64Tuple4D> scalingFunc, Func<Float64Tuple4D, Float64Tuple4D, Float64Tuple4D> reducingFunc)
    //{
    //    var vectorsDictionary = new Dictionary<int, Float64Tuple4D>();

    //    foreach (var (index, vector) in map2.GetColumns())
    //        vectorsDictionary.Add(
    //            index,
    //            map1.CombineColumns(vector, scalingFunc, reducingFunc)
    //        );

    //    return vectorsDictionary.ToLinUnilinearMap();
    //}

    
    //public Float64Tuple4D MapVector(IReadOnlyList<double> vector)
    //{
    //    var composer = new Float64Tuple4DComposer();

    //    if (map.Count <= vector.Count)
    //    {
    //        foreach (var (index, mv) in map.IndexVectorPairs)
    //        {
    //            if (index >= vector.Count)
    //                continue;

    //            composer.AddVector(mv, vector[index]);
    //        }
    //    }
    //    else
    //    {
    //        for (var index = 0; index < vector.Count; index++)
    //        {
    //            if (!map.TryGetVector(index, out var mv))
    //                continue;

    //            composer.AddVector(mv, vector[index]);
    //        }
    //    }

    //    return composer.GetVector();
    //}

    //public Float64Tuple4D MapVector(IReadOnlyDictionary<int, double> vector)
    //{
    //    var composer = new Float64Tuple4DComposer();

    //    if (map.Count <= vector.Count)
    //    {
    //        foreach (var (index, mv) in map.IndexVectorPairs)
    //        {
    //            if (!vector.TryGetValue(index, out var scalar))
    //                continue;

    //            composer.AddVector(mv, scalar);
    //        }
    //    }
    //    else
    //    {
    //        foreach (var (index, scalar) in vector)
    //        {
    //            if (!map.TryGetVector(index, out var mv))
    //                continue;

    //            composer.AddVector(mv, scalar);
    //        }
    //    }

    //    return composer.GetVector();
    //}

    
    //
    //public LinFloat64UnilinearMap AbsScalars()
    //{
    //    return v1.MapScalars(Math.Abs);
    //}

    //
    //public LinFloat64UnilinearMap Sqrt()
    //{
    //    return v1.MapScalars(Math.Sqrt);
    //}

    //
    //public LinFloat64UnilinearMap SqrtOfAbs()
    //{
    //    return v1.MapScalars(s => s.SqrtOfAbs());
    //}

    //
    //public LinFloat64UnilinearMap Exp()
    //{
    //    return v1.MapScalars(Math.Exp);
    //}

    //
    //public LinFloat64UnilinearMap LogE()
    //{
    //    return v1.MapScalars(s => s.LogE());
    //}

    //
    //public LinFloat64UnilinearMap Log2()
    //{
    //    return v1.MapScalars(Math.Log2);
    //}

    //
    //public LinFloat64UnilinearMap Log10()
    //{
    //    return v1.MapScalars(Math.Log10);
    //}

    //
    //public LinFloat64UnilinearMap Log(, double scalar)
    //{
    //    return v1.MapScalars(s => Math.Log(s, scalar));
    //}

    //
    //public LinFloat64UnilinearMap Log(this double scalar, LinFloat64UnilinearMap v1)
    //{
    //    return v1.MapScalars(s => Math.Log(scalar, s));
    //}

    //
    //public LinFloat64UnilinearMap Power(, double scalar)
    //{
    //    return v1.MapScalars(s => Math.Pow(s, scalar));
    //}

    //
    //public LinFloat64UnilinearMap Power(this double scalar, LinFloat64UnilinearMap v1)
    //{
    //    return v1.MapScalars(s => Math.Log(scalar, s));
    //}

    //
    //public LinFloat64UnilinearMap Cos()
    //{
    //    return v1.MapScalars(Math.Cos);
    //}

    //
    //public LinFloat64UnilinearMap Sin()
    //{
    //    return v1.MapScalars(Math.Sin);
    //}

    //
    //public LinFloat64UnilinearMap Tan()
    //{
    //    return v1.MapScalars(Math.Tan);
    //}

    //
    //public LinFloat64UnilinearMap ArcCos()
    //{
    //    return v1.MapScalars(Math.Acos);
    //}

    //
    //public LinFloat64UnilinearMap ArcSin()
    //{
    //    return v1.MapScalars(Math.Asin);
    //}

    //
    //public LinFloat64UnilinearMap ArcTan()
    //{
    //    return v1.MapScalars(Math.Atan);
    //}

    //
    //public LinFloat64UnilinearMap Cosh()
    //{
    //    return v1.MapScalars(Math.Cosh);
    //}

    //
    //public LinFloat64UnilinearMap Sinh()
    //{
    //    return v1.MapScalars(Math.Sinh);
    //}

    //
    //public LinFloat64UnilinearMap Tanh()
    //{
    //    return v1.MapScalars(Math.Tanh);
    //}

    
    public IEnumerator<KeyValuePair<IndexPair, double>> GetEnumerator()
    {
        foreach (var (index2, vector) in _indexVectorDictionary.ToTuples())
            foreach (var (index1, scalar) in vector.ToTuples())
                yield return new KeyValuePair<IndexPair, double>(
                    new IndexPair(index1, index2),
                    scalar
                );
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}