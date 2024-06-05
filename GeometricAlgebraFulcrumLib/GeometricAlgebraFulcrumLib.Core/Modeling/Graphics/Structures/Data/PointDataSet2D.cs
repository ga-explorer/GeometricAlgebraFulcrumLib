using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Structures.Data;

public class PointDataSet2D<T> :
    IReadOnlyList<PointData2D<T>>
{
    private readonly Dictionary<Pair<double>, int> _pointIndexDictionary
        = new Dictionary<Pair<double>, int>();

    private readonly List<PointData2D<T>> _pointDataList;


    public int Count 
        => _pointDataList.Count;

    public PointData2D<T> this[int index] 
        => _pointDataList[index];

    public T this[double x, double y]
    {
        get
        {
            var key = new Pair<double>(x, y);

            return this[key];
        }
        set
        {
            var key = new Pair<double>(x, y);

            this[key] = value;
        }
    }
        
    public T this[IPair<double> pair]
    {
        get => this[pair.ToPair()];
        set => this[pair.ToPair()] = value;
    }
        
    public T this[Pair<double> key]
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
                _pointDataList[index] = new PointData2D<T>(index, key, value);
                return;
            }

            index = _pointDataList.Count;

            _pointDataList.Add(new PointData2D<T>(index, key, value));
            _pointIndexDictionary.Add(key, index);
        }
    }
        
    public IEnumerable<LinFloat64Vector2D> Points
        => _pointDataList.Select(p => LinFloat64Vector2D.Create(p.X, p.Y));

    public PointDataSet2D()
    {
        _pointDataList = new List<PointData2D<T>>();
    }

    public PointDataSet2D(int capacity)
    {
        _pointDataList = new List<PointData2D<T>>(capacity);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet2D<T> Clear()
    {
        _pointIndexDictionary.Clear();
        _pointDataList.Clear();

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet2D<T> BeginBatch()
    {
        _pointIndexDictionary.Clear();

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet2D<T> EndBatch()
    {
        _pointIndexDictionary.Clear();
            
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointDataSet2D<T> EndBatch(Action<T> processPointDataAction)
    {
        var pointDataValueList =
            _pointIndexDictionary.Values.Select(i => _pointDataList[i].DataValue);
            
        foreach (var pointDataValue in pointDataValueList)
            processPointDataAction(pointDataValue);

        return EndBatch();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(double x, double y)
    {
        var key = new Pair<double>(x, y);

        return _pointIndexDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(IPair<double> triplet)
    {
        return _pointIndexDictionary.ContainsKey(triplet.ToPair());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(Pair<double> key)
    {
        return _pointIndexDictionary.ContainsKey(key);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(double x, double y, out int pointIndex)
    {
        var key = new Pair<double>(x, y);

        return TryGetPointIndex(key, out pointIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(IPair<double> pair, out int pointIndex)
    {
        return TryGetPointIndex(pair.ToPair(), out pointIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointIndex(Pair<double> key, out int pointIndex)
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
    public bool TryGetPointDataValue(double x, double y, out T pointDataValue)
    {
        var key = new Pair<double>(x, y);

        return TryGetPointDataValue(key, out pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointDataValue(IPair<double> pair, out T pointDataValue)
    {
        return TryGetPointDataValue(pair.ToPair(), out pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointDataValue(Pair<double> key, out T pointDataValue)
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
    public bool TryGetPointData(double x, double y, out PointData2D<T> pointData)
    {
        var key = new Pair<double>(x, y);

        return TryGetPointData(key, out pointData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointData(IPair<double> pair, out PointData2D<T> pointData)
    {
        return TryGetPointData(pair.ToPair(), out pointData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetPointData(Pair<double> key, out PointData2D<T> pointData)
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
    public int GetPointIndex(double x, double y)
    {
        var key = new Pair<double>(x, y);

        return GetPointIndex(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetPointIndex(IPair<double> pair)
    {
        return GetPointIndex(pair.ToPair());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetPointIndex(Pair<double> key)
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
    public T GetPointDataValue(double x, double y)
    {
        var key = new Pair<double>(x, y);

        return GetPointDataValue(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(IPair<double> pair)
    {
        return GetPointDataValue(pair.ToPair());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetPointDataValue(Pair<double> key)
    {
        return _pointIndexDictionary.TryGetValue(key, out var index) 
            ? _pointDataList[index].DataValue
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> GetPointData(int index)
    {
        return _pointDataList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> GetPointData(double x, double y)
    {
        var key = new Pair<double>(x, y);

        return GetPointData(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> GetPointData(IPair<double> pair)
    {
        return GetPointData(pair.ToPair());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> GetPointData(Pair<double> key)
    {
        return _pointIndexDictionary.TryGetValue(key, out var index) 
            ? _pointDataList[index]
            : throw new KeyNotFoundException();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> AddPoint(double x, double y, T pointDataValue)
    {
        var key = new Pair<double>(x, y);
            
        return AddPoint(key, pointDataValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> AddPoint(IPair<double> pair, T pointDataValue)
    {
        var key = pair.ToPair();
            
        return AddPoint(key, pointDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> AddPoint(Pair<double> key, T pointDataValue)
    {
        var index = _pointDataList.Count;
        var pointData = new PointData2D<T>(index, key, pointDataValue);

        _pointDataList.Add(pointData);
        _pointIndexDictionary.Add(key, index);

        return pointData;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(int index)
    {
        return _pointDataList[index].ToLinVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<PointData2D<T>> GetEnumerator()
    {
        return _pointDataList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}