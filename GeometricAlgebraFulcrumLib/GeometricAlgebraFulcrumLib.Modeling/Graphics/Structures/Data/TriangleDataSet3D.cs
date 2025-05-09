using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Data;

public class TriangleDataSet3D<T> :
    IReadOnlyList<TriangleData3D<T>>
{
    private readonly Dictionary<Triplet<int>, int> _triangleIndexDictionary
        = new Dictionary<Triplet<int>, int>();

    private readonly List<TriangleData3D<T>> _triangleDataList;


    public int Count 
        => _triangleDataList.Count;

    public TriangleData3D<T> this[int index] 
        => _triangleDataList[index];
        
    public T this[int index1, int index2, int index3]
    {
        get
        {
            var key = 
                GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

            return GetDataValue(key);
        }
        set
        {
            var key = 
                GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

            SetDataValue(key, value);
        }
    }
        
    public T this[ITriplet<int> triplet]
    {
        get
        {
            var key = 
                triplet.GetValidTriangleIndexTriplet();

            return GetDataValue(key);
        }
        set
        {
            var key = 
                triplet.GetValidTriangleIndexTriplet();

            SetDataValue(key, value);
        }
    }
        
    public IEnumerable<Triplet<int>> Triangles
        => _triangleDataList.Select(
            p => new Triplet<int>(p.Item1, p.Item2, p.Item3)
        );
        

    public TriangleDataSet3D()
    {
        _triangleDataList = new List<TriangleData3D<T>>();
    }

    public TriangleDataSet3D(int capacity)
    {
        _triangleDataList = new List<TriangleData3D<T>>(capacity);
    }


    private T GetDataValue(Triplet<int> key)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        if (_triangleIndexDictionary.TryGetValue(key, out var index))
            return _triangleDataList[index].DataValue;

        throw new KeyNotFoundException();
    }

    private void SetDataValue(Triplet<int> key, T dataValue)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        if (_triangleIndexDictionary.TryGetValue(key, out var index))
        {
            _triangleDataList[index] = new TriangleData3D<T>(index, key, dataValue);
            return;
        }

        index = _triangleDataList.Count;

        _triangleDataList.Add(new TriangleData3D<T>(index, key, dataValue));
        _triangleIndexDictionary.Add(key, index);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetMaxPointIndex()
    {
        return _triangleDataList
            .Select(triangleData => triangleData.MaxIndex)
            .Prepend(-1)
            .Max();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleDataSet3D<T> Clear()
    {
        _triangleIndexDictionary.Clear();
        _triangleDataList.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsTriangle(int index1, int index2, int index3)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return ContainsTriangle(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsTriangle(ITriplet<int> indexTriplet)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return ContainsTriangle(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool ContainsTriangle(Triplet<int> key)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        return _triangleIndexDictionary.ContainsKey(key);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleIndex(int index1, int index2, int index3, out int triangleIndex)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return TryGetTriangleIndex(key, out triangleIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleIndex(ITriplet<int> indexTriplet, out int triangleIndex)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return TryGetTriangleIndex(key, out triangleIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryGetTriangleIndex(Triplet<int> key, out int triangleIndex)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        if (_triangleIndexDictionary.TryGetValue(key, out var index))
        {
            triangleIndex = index;
            return true;
        }

        triangleIndex = -1;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleDataValue(int index1, int index2, int index3, out T triangleDataValue)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return TryGetTriangleDataValue(key, out triangleDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleDataValue(ITriplet<int> indexTriplet, out T triangleDataValue)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return TryGetTriangleDataValue(key.ToTriplet(), out triangleDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryGetTriangleDataValue(Triplet<int> key, out T triangleDataValue)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        if (_triangleIndexDictionary.TryGetValue(key, out var index))
        {
            triangleDataValue = _triangleDataList[index].DataValue;
            return true;
        }

        triangleDataValue = default;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleData(int index1, int index2, int index3, out TriangleData3D<T> triangleData)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return TryGetTriangleData(key, out triangleData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTriangleData(ITriplet<int> indexTriplet, out TriangleData3D<T> triangleData)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return TryGetTriangleData(key.ToTriplet(), out triangleData);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryGetTriangleData(Triplet<int> key, out TriangleData3D<T> triangleData)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        if (_triangleIndexDictionary.TryGetValue(key, out var index))
        {
            triangleData = _triangleDataList[index];
            return true;
        }

        triangleData = default;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetTriangleIndex(int index1, int index2, int index3)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return GetTriangleIndex(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetTriangleIndex(ITriplet<int> indexTriplet)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return GetTriangleIndex(key.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetTriangleIndex(Triplet<int> key)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        return _triangleIndexDictionary.TryGetValue(key, out var index) 
            ? index 
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTriangleDataValue(int index)
    {
        return _triangleDataList[index].DataValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTriangleDataValue(int index1, int index2, int index3)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return GetTriangleDataValue(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTriangleDataValue(ITriplet<int> indexTriplet)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return GetTriangleDataValue(key.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T GetTriangleDataValue(Triplet<int> key)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        return _triangleIndexDictionary.TryGetValue(key, out var index) 
            ? _triangleDataList[index].DataValue
            : throw new KeyNotFoundException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleData3D<T> GetTriangleData(int index)
    {
        return _triangleDataList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleData3D<T> GetTriangleData(int index1, int index2, int index3)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);

        return GetTriangleData(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleData3D<T> GetTriangleData(ITriplet<int> indexTriplet)
    {
        var key = 
            indexTriplet.GetValidTriangleIndexTriplet();

        return GetTriangleData(key.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TriangleData3D<T> GetTriangleData(Triplet<int> key)
    {
        Debug.Assert(key.IsValidTriangleIndexTriplet());

        return _triangleIndexDictionary.TryGetValue(key, out var index) 
            ? _triangleDataList[index]
            : throw new KeyNotFoundException();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleData3D<T> AddTriangle(int index1, int index2, int index3, T triangleDataValue)
    {
        var key = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(index1, index2, index3);
            
        return AddTriangle(key, triangleDataValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleData3D<T> AddTriangle(ITriplet<int> triplet, T triangleDataValue)
    {
        var key = triplet.ToTriplet();
            
        return AddTriangle(key, triangleDataValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TriangleData3D<T> AddTriangle(Triplet<int> key, T triangleDataValue)
    {
        var index = _triangleDataList.Count;

        var triangleData = new TriangleData3D<T>(index, key, triangleDataValue);

        _triangleDataList.Add(triangleData);
        _triangleIndexDictionary.Add(key, index);

        return triangleData;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<int> GetTriangle(int index)
    {
        return _triangleDataList[index].ToTriplet();
    }

        
    public IReadOnlyList<int> GetIndicesArray()
    {
        var indicesArray = new int[_triangleDataList.Count];

        var i = 0;
        foreach (var triangleData in _triangleDataList)
        {
            indicesArray[i] = triangleData.Item1;
            indicesArray[i + 1] = triangleData.Item2;
            indicesArray[i + 2] = triangleData.Item3;

            i += 3;
        }

        return indicesArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<TriangleData3D<T>> GetEnumerator()
    {
        return _triangleDataList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}