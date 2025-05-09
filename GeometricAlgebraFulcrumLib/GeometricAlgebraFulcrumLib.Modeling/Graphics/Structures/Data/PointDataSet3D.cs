﻿using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Data;

public class PointDataSet3D<T> :
    IReadOnlyList<PointData3D<T>>
{
    private readonly Dictionary<Triplet<Float64Scalar>, int> _pointIndexDictionary
        = new Dictionary<Triplet<Float64Scalar>, int>();

    private readonly List<PointData3D<T>> _pointDataList;

    public int Count 
        => _pointDataList.Count;

    public PointData3D<T> this[int index] 
        => _pointDataList[index];

    public T this[double x, double y, double z]
    {
        get
        {
            var key = new Triplet<Float64Scalar>(x, y, z);

            return this[key];
        }
        set
        {
            var key = new Triplet<Float64Scalar>(x, y, z);

            this[key] = value;
        }
    }
        
    public T this[ITriplet<Float64Scalar> triplet]
    {
        get => this[triplet.ToTriplet()];
        set => this[triplet.ToTriplet()] = value;
    }
        
    public T this[Triplet<Float64Scalar> key]
    {
        get
        {
            if (_pointIndexDictionary.TryGetValue(key, out var index))
                return _pointDataList[index].DataValue;

            throw new KeyNotFoundException();
        }
        set
        {
            if (_pointIndexDictionary.TryGetValue(key, out var index))
            {
                _pointDataList[index] = new PointData3D<T>(index, key, value);
                return;
            }

            index = _pointDataList.Count;

            _pointDataList.Add(new PointData3D<T>(index, key, value));
            _pointIndexDictionary.Add(key, index);
        }
    }

    public IEnumerable<LinFloat64Vector3D> Points
        => _pointDataList.Select(p => LinFloat64Vector3D.Create(p.X, p.Y, p.Z));


    public PointDataSet3D()
    {
        _pointDataList = new List<PointData3D<T>>();
    }

    public PointDataSet3D(int capacity)
    {
        _pointDataList = new List<PointData3D<T>>(capacity);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet3D<T> Clear()
    {
        _pointIndexDictionary.Clear();
        _pointDataList.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet3D<T> BeginBatch()
    {
        _pointIndexDictionary.Clear();

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet3D<T> EndBatch()
    {
        _pointIndexDictionary.Clear();
            
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet3D<T> EndBatch(Action<T> processPointDataAction)
    {
        var pointDataValueList =
            _pointIndexDictionary.Values.Select(i => _pointDataList[i].DataValue);
            
        foreach (var pointDataValue in pointDataValueList)
            processPointDataAction(pointDataValue);

        return EndBatch();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(double x, double y, double z)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return _pointIndexDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(ITriplet<Float64Scalar> triplet)
    {
        return _pointIndexDictionary.ContainsKey(triplet.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(Triplet<Float64Scalar> key)
    {
        return _pointIndexDictionary.ContainsKey(key);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(double x, double y, double z, out int pointIndex)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return TryGetPointIndex(key, out pointIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(ITriplet<Float64Scalar> triplet, out int pointIndex)
    {
        return TryGetPointIndex(triplet.ToTriplet(), out pointIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(Triplet<Float64Scalar> key, out int pointIndex)
    {
        if (_pointIndexDictionary.TryGetValue(key, out var index))
        {
            pointIndex = index;
            return true;
        }

        pointIndex = -1;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointDataValue(double x, double y, double z, out T pointDataValue)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return TryGetPointDataValue(key, out pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointDataValue(ITriplet<Float64Scalar> triplet, out T pointDataValue)
    {
        return TryGetPointDataValue(triplet.ToTriplet(), out pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointDataValue(Triplet<Float64Scalar> key, out T pointDataValue)
    {
        if (_pointIndexDictionary.TryGetValue(key, out var index))
        {
            pointDataValue = _pointDataList[index].DataValue;
            return true;
        }

        pointDataValue = default;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointData(double x, double y, double z, out PointData3D<T> pointData)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return TryGetPointData(key, out pointData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointData(ITriplet<Float64Scalar> triplet, out PointData3D<T> pointData)
    {
        return TryGetPointData(triplet.ToTriplet(), out pointData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointData(Triplet<Float64Scalar> key, out PointData3D<T> pointData)
    {
        if (_pointIndexDictionary.TryGetValue(key, out var index))
        {
            pointData = _pointDataList[index];
            return true;
        }

        pointData = default;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetPointIndex(double x, double y, double z)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return GetPointIndex(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetPointIndex(ITriplet<Float64Scalar> triplet)
    {
        return GetPointIndex(triplet.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetPointIndex(Triplet<Float64Scalar> key)
    {
        return _pointIndexDictionary.TryGetValue(key, out var index) 
            ? index 
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(int index)
    {
        return _pointDataList[index].DataValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(double x, double y, double z)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return GetPointDataValue(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(ITriplet<Float64Scalar> triplet)
    {
        return GetPointDataValue(triplet.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(Triplet<Float64Scalar> key)
    {
        return _pointIndexDictionary.TryGetValue(key, out var index) 
            ? _pointDataList[index].DataValue
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> GetPointData(int index)
    {
        return _pointDataList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> GetPointData(double x, double y, double z)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return GetPointData(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> GetPointData(ITriplet<Float64Scalar> triplet)
    {
        return GetPointData(triplet.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> GetPointData(Triplet<Float64Scalar> key)
    {
        return _pointIndexDictionary.TryGetValue(key, out var index) 
            ? _pointDataList[index]
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> AddPoint(double x, double y, double z, T pointDataValue)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);
            
        return AddPoint(key, pointDataValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> AddPoint(ITriplet<Float64Scalar> triplet, T pointDataValue)
    {
        var key = triplet.ToTriplet();

        return AddPoint(key, pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> AddPoint(Triplet<Float64Scalar> key, T pointDataValue)
    {
        var index = _pointDataList.Count;
        var pointData = new PointData3D<T>(index, key, pointDataValue);

        _pointDataList.Add(pointData);
        _pointIndexDictionary.Add(key, index);

        return pointData;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(int index)
    {
        return _pointDataList[index].ToLinVector3D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<PointData3D<T>> GetEnumerator()
    {
        return _pointDataList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}