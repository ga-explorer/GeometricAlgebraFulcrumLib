using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Tuples;

public class DistinctTuplesList3D
    : IReadOnlyList<ILinFloat64Vector3D>
{
    private readonly Dictionary3Keys<double, int> _tupleIndicesTable =
        new Dictionary3Keys<double, int>();

    private readonly List<ILinFloat64Vector3D> _tuplesList;


    public int Count
    {
        get { return _tupleIndicesTable.Count; }
    }

    public ILinFloat64Vector3D this[int index]
    {
        get { return _tuplesList[index]; }
        set
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(nameof(value));

            var oldTuple = _tuplesList[index];

            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(value.Item1, value.Item2, value.Item3, out tupleIndex) && tupleIndex != index)
                throw new InvalidOperationException();

            _tuplesList[index] = value;

            _tupleIndicesTable.Remove(oldTuple.Item1, oldTuple.Item2, oldTuple.Item3);
            _tupleIndicesTable.Add(oldTuple.Item1, oldTuple.Item2, oldTuple.Item3, index);
        }
    }


    public DistinctTuplesList3D()
    {
        _tuplesList = new List<ILinFloat64Vector3D>();
    }

    public DistinctTuplesList3D(int capacity)
    {
        _tuplesList = new List<ILinFloat64Vector3D>(capacity);
    }

    public DistinctTuplesList3D(ILinFloat64Vector3D tuple)
    {
        _tuplesList = new List<ILinFloat64Vector3D>();

        AddTuple(tuple);
    }

    public DistinctTuplesList3D(params ILinFloat64Vector3D[] tuplesList)
    {
        _tuplesList = new List<ILinFloat64Vector3D>(tuplesList.Length);

        AddTuples(tuplesList);
    }

    public DistinctTuplesList3D(IEnumerable<ILinFloat64Vector3D> tuplesList)
    {
        _tuplesList = new List<ILinFloat64Vector3D>();

        AddTuples(tuplesList);
    }

    public DistinctTuplesList3D(DistinctTuplesList3D tuplesList)
    {
        foreach (var pair in tuplesList._tupleIndicesTable)
            _tupleIndicesTable.Add(
                pair.Key.Item1,
                pair.Key.Item2,
                pair.Key.Item3,
                pair.Value
            );

        _tuplesList.AddRange(tuplesList._tuplesList);
    }


    public DistinctTuplesList3D Clear()
    {
        _tupleIndicesTable.Clear();
        _tuplesList.Clear();

        return this;
    }

    public KeyValuePair<int, ILinFloat64Vector3D> AddTuple(double x, double y, double z)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
            return new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;
        var tuple = LinFloat64Vector3D.Create(x, y, z);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, z, tupleIndex);

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            tuple
        );
    }

    public KeyValuePair<int, ILinFloat64Vector3D> AddTuple(ILinFloat64Vector3D tuple)
    {
        if (ReferenceEquals(tuple, null))
            throw new ArgumentNullException(nameof(tuple));

        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out tupleIndex))
            return new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.Item1, tuple.Item2, tuple.Item3, tupleIndex);

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            tuple
        );
    }

    public IEnumerable<KeyValuePair<int, ILinFloat64Vector3D>> AddTuples(IEnumerable<ILinFloat64Vector3D> tuplesList)
    {
        return tuplesList.Select(AddTuple);
    }

    public KeyValuePair<int, ILinFloat64Vector3D>[] AddTuples(params ILinFloat64Vector3D[] tuplesList)
    {
        return tuplesList.Select(AddTuple).ToArray();
    }

    public ILinFloat64Vector3D GetTuple(double x, double y, double z)
    {
        var tupleIndex = _tupleIndicesTable[x, y, z];

        return _tuplesList[tupleIndex];
    }

    public ILinFloat64Vector3D GetTuple(ILinFloat64Vector3D tuple)
    {
        var tupleIndex = _tupleIndicesTable[tuple.Item1, tuple.Item2, tuple.Item3];

        return _tuplesList[tupleIndex];
    }

    public int GetTupleIndex(double x, double y, double z)
    {
        return _tupleIndicesTable[x, y, z];
    }

    public int GetTupleIndex(ILinFloat64Vector3D tuple)
    {
        return _tupleIndicesTable[tuple.Item1, tuple.Item2, tuple.Item3];
    }

    public KeyValuePair<int, ILinFloat64Vector3D> GetTupleWithIndex(double x, double y, double z)
    {
        var tupleIndex = _tupleIndicesTable[x, y, z];

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            _tuplesList[tupleIndex]
        );
    }

    public KeyValuePair<int, ILinFloat64Vector3D> GetTupleWithIndex(ILinFloat64Vector3D tuple)
    {
        var tupleIndex = _tupleIndicesTable[tuple.Item1, tuple.Item2, tuple.Item3];

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            _tuplesList[tupleIndex]
        );
    }

    public int GetOrAddTupleIndex(double x, double y, double z)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
            return tupleIndex;

        tupleIndex = _tuplesList.Count;
        var tuple = LinFloat64Vector3D.Create(x, y, z);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, z, tupleIndex);

        return tupleIndex;
    }

    public int GetOrAddTupleIndex(ILinFloat64Vector3D tuple)
    {
        if (_tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out var tupleIndex))
            return tupleIndex;

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.Item1, tuple.Item2, tuple.Item3, tupleIndex);

        return tupleIndex;
    }

    public KeyValuePair<int, ILinFloat64Vector3D> GetOrAddTupleWithIndex(double x, double y, double z)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
            return new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;
        var tuple = LinFloat64Vector3D.Create(x, y, z);

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(x, y, z, tupleIndex);

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            tuple
        );
    }

    public KeyValuePair<int, ILinFloat64Vector3D> GetOrAddTupleWithIndex(ILinFloat64Vector3D tuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out tupleIndex))
            return new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

        tupleIndex = _tuplesList.Count;

        _tuplesList.Add(tuple);
        _tupleIndicesTable.Add(tuple.Item1, tuple.Item2, tuple.Item3, tupleIndex);

        return new KeyValuePair<int, ILinFloat64Vector3D>(
            tupleIndex,
            tuple
        );
    }

    public bool TryGetTuple(double x, double y, double z, out ILinFloat64Vector3D outputTuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
        {
            outputTuple = _tuplesList[tupleIndex];
            return true;
        }

        outputTuple = null;
        return false;
    }

    public bool TryGetTuple(ILinFloat64Vector3D tuple, out ILinFloat64Vector3D outputTuple)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out tupleIndex))
        {
            outputTuple = _tuplesList[tupleIndex];
            return true;
        }

        outputTuple = null;
        return false;
    }

    public bool TryGetTupleIndex(double x, double y, double z, out int tupleIndex)
    {
        return _tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex);
    }

    public bool TryGetTupleIndex(ILinFloat64Vector3D tuple, out int tupleIndex)
    {
        return _tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out tupleIndex);
    }

    public bool TryGetTupleWithIndex(double x, double y, double z, out KeyValuePair<int, ILinFloat64Vector3D> tupleWithIndex)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
        {
            tupleWithIndex = new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

            return true;
        }

        tupleWithIndex = new KeyValuePair<int, ILinFloat64Vector3D>(-1, LinFloat64Vector3D.Zero);
        return false;
    }

    public bool TryGetTupleWithIndex(ILinFloat64Vector3D tuple, out KeyValuePair<int, ILinFloat64Vector3D> tupleWithIndex)
    {
        int tupleIndex;
        if (_tupleIndicesTable.TryGetValue(tuple.Item1, tuple.Item2, tuple.Item3, out tupleIndex))
        {
            tupleWithIndex = new KeyValuePair<int, ILinFloat64Vector3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );

            return true;
        }

        tupleWithIndex = new KeyValuePair<int, ILinFloat64Vector3D>(-1, LinFloat64Vector3D.Zero);
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


    public IEnumerator<ILinFloat64Vector3D> GetEnumerator()
    {
        return _tuplesList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _tuplesList.GetEnumerator();
    }
}