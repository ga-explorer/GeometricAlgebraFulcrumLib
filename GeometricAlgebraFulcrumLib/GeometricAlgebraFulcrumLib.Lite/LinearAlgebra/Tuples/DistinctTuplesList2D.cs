﻿using System.Collections;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;

public class DistinctTuplesList2D : IReadOnlyCollection<IFloat64Vector2D>
{
    private readonly Dictionary2Keys<double, int> _tupleIndicesTable =
        new Dictionary2Keys<double, int>();

    private readonly List<IFloat64Vector2D> _tuplesList;


    public int Count
    {
        get { return _tupleIndicesTable.Count; }
    }

    public IFloat64Vector2D this[int index]
    {
        get { return _tuplesList[index]; }
        set
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(nameof(value));

            var oldTuple = _tuplesList[index];

            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(value.X, value.Y, out tupleIndex) && tupleIndex != index)
                throw new InvalidOperationException();

            _tuplesList[index] = value;

            _tupleIndicesTable.Remove(oldTuple.X, oldTuple.Y);
            _tupleIndicesTable.Add(oldTuple.X, oldTuple.Y, index);
        }
    }


    public DistinctTuplesList2D()
    {
        _tuplesList = new List<IFloat64Vector2D>();
    }

    public DistinctTuplesList2D(int capacity)
    {
        _tuplesList = new List<IFloat64Vector2D>(capacity);
    }

    public DistinctTuplesList2D(IFloat64Vector2D tuple)
    {
        _tuplesList = new List<IFloat64Vector2D>();

        AddTuple(tuple);
    }

    public DistinctTuplesList2D(params IFloat64Vector2D[] tuplesList)
    {
        _tuplesList = new List<IFloat64Vector2D>(tuplesList.Length);

        AddTuples(tuplesList);
    }

    public DistinctTuplesList2D(IEnumerable<IFloat64Vector2D> tuplesList)
    {
        _tuplesList = new List<IFloat64Vector2D>();

        AddTuples(tuplesList);
    }


    public DistinctTuplesList2D Clear()
    {
        _tupleIndicesTable.Clear();
        _tuplesList.Clear();

        return this;
    }

    public KeyValuePair<int, IFloat64Vector2D> AddTuple(double x, double y)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
            return new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;
        var tuple = Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, tupleIndex);

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            tuple
        );
    }

    public KeyValuePair<int, IFloat64Vector2D> AddTuple(IFloat64Vector2D tuple)
    {
        if (ReferenceEquals(tuple, null))
            throw new ArgumentNullException(nameof(tuple));

        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
            return new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            tuple
        );
    }

    public IEnumerable<KeyValuePair<int, IFloat64Vector2D>> AddTuples(IEnumerable<IFloat64Vector2D> tuplesList)
    {
        return tuplesList.Select(AddTuple);
    }

    public KeyValuePair<int, IFloat64Vector2D>[] AddTuples(params IFloat64Vector2D[] tuplesList)
    {
        return tuplesList.Select(AddTuple).ToArray();
    }

    public IFloat64Vector2D GetTuple(double x, double y)
    {
        var tupleIndex = _tupleIndicesTable[x, y];

        return _tuplesList[tupleIndex];
    }

    public IFloat64Vector2D GetTuple(IFloat64Vector2D tuple)
    {
        var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y];

        return _tuplesList[tupleIndex];
    }

    public int GetTupleIndex(double x, double y)
    {
        return _tupleIndicesTable[x, y];
    }

    public int GetTupleIndex(IFloat64Vector2D tuple)
    {
        return _tupleIndicesTable[tuple.X, tuple.Y];
    }

    public int GetOrAddTupleIndex(double x, double y)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
            return tupleIndex;

        tupleIndex = _tuplesList.Count;
        var tuple = Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, tupleIndex);

        return tupleIndex;
    }

    public int GetOrAddTupleIndex(IFloat64Vector2D tuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
            return tupleIndex;

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

        return tupleIndex;
    }

    public KeyValuePair<int, IFloat64Vector2D> GetTupleWithIndex(double x, double y)
    {
        var tupleIndex = _tupleIndicesTable[x, y];

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            _tuplesList[tupleIndex]
        );
    }

    public KeyValuePair<int, IFloat64Vector2D> GetTupleWithIndex(IFloat64Vector2D tuple)
    {
        var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y];

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            _tuplesList[tupleIndex]
        );
    }

    public KeyValuePair<int, IFloat64Vector2D> GetOrAddTupleWithIndex(double x, double y)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
            return new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;
        var tuple = Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, tupleIndex);

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            tuple
        );
    }

    public KeyValuePair<int, IFloat64Vector2D> GetOrAddTupleWithIndex(IFloat64Vector2D tuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
            return new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

        return new KeyValuePair<int, IFloat64Vector2D>(
            tupleIndex,
            tuple
        );
    }

    public bool TryGetTuple(double x, double y, out IFloat64Vector2D outputTuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
        {
            outputTuple = _tuplesList[tupleIndex];
            return true;
        }

        outputTuple = null;
        return false;
    }

    public bool TryGetTuple(IFloat64Vector2D tuple, out IFloat64Vector2D outputTuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
        {
            outputTuple = _tuplesList[tupleIndex];
            return true;
        }

        outputTuple = null;
        return false;
    }

    public bool TryGetTupleIndex(double x, double y, out int tupleIndex)
    {
        return _tupleIndicesTable.TryGetValue(x, y, out tupleIndex);
    }

    public bool TryGetTupleIndex(IFloat64Vector2D tuple, out int tupleIndex)
    {
        return _tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex);
    }

    public bool TryGetTupleWithIndex(double x, double y, out KeyValuePair<int, IFloat64Vector2D> tupleWithIndex)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
        {
            tupleWithIndex = new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

            return true;
        }

        tupleWithIndex = new KeyValuePair<int, IFloat64Vector2D>(-1, Float64Vector2D.Zero);
        return false;
    }

    public bool TryGetTupleWithIndex(IFloat64Vector2D tuple, out KeyValuePair<int, IFloat64Vector2D> tupleWithIndex)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
        {
            tupleWithIndex = new KeyValuePair<int, IFloat64Vector2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

            return true;
        }

        tupleWithIndex = new KeyValuePair<int, IFloat64Vector2D>(-1, Float64Vector2D.Zero);
        return false;
    }

    public bool ContainsIndex(int index)
    {
        return index >= 0 && index <= _tuplesList.Count;
    }

    public bool ContainsIndices(int index1, int index2)
    {
        return index1 >= 0 && index1 <= _tuplesList.Count &&
               index2 >= 0 && index2 <= _tuplesList.Count;
    }

    public bool ContainsIndices(int index1, int index2, int index3)
    {
        return index1 >= 0 && index1 <= _tuplesList.Count &&
               index2 >= 0 && index2 <= _tuplesList.Count &&
               index3 >= 0 && index3 <= _tuplesList.Count;
    }


    public IEnumerator<IFloat64Vector2D> GetEnumerator()
    {
        return _tuplesList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _tuplesList.GetEnumerator();
    }
}