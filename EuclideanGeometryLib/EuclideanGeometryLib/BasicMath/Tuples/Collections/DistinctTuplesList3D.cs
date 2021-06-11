using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Tuples.Collections
{
    public class DistinctTuplesList3D 
        : IReadOnlyList<ITuple3D>
    {
        private readonly Dictionary3Keys<double, int> _tupleIndicesTable =
            new Dictionary3Keys<double, int>();

        private readonly List<ITuple3D> _tuplesList;


        public int Count 
            => _tupleIndicesTable.Count;

        public ITuple3D this[int index]
        {
            get { return _tuplesList[index]; }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException(nameof(value));

                var oldTuple = _tuplesList[index];

                int tupleIndex;
                if (_tupleIndicesTable.TryGetValue(value.X, value.Y, value.Z, out tupleIndex) && tupleIndex != index)
                    throw new InvalidOperationException();

                _tuplesList[index] = value;

                _tupleIndicesTable.Remove(oldTuple.X, oldTuple.Y, oldTuple.Z);
                _tupleIndicesTable.Add(oldTuple.X, oldTuple.Y, oldTuple.Z, index);
            }
        }


        public DistinctTuplesList3D()
        {
            _tuplesList = new List<ITuple3D>();
        }

        public DistinctTuplesList3D(int capacity)
        {
            _tuplesList = new List<ITuple3D>(capacity);
        }

        public DistinctTuplesList3D(ITuple3D tuple)
        {
            _tuplesList = new List<ITuple3D>();

            AddTuple(tuple);
        }

        public DistinctTuplesList3D(params ITuple3D[] tuplesList)
        {
            _tuplesList = new List<ITuple3D>(tuplesList.Length);

            AddTuples(tuplesList);
        }

        public DistinctTuplesList3D(IEnumerable<ITuple3D> tuplesList)
        {
            _tuplesList = new List<ITuple3D>();

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

        public KeyValuePair<int, ITuple3D> AddTuple(double x, double y, double z)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
                return new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;
            var tuple = new Tuple3D(x, y, z);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, z, tupleIndex);

            return new KeyValuePair<int, ITuple3D>(
                tupleIndex,
                tuple
            );
        }

        public KeyValuePair<int, ITuple3D> AddTuple(ITuple3D tuple)
        {
            if (ReferenceEquals(tuple, null))
                throw new ArgumentNullException(nameof(tuple));

            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out tupleIndex))
                return new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tuple.Z, tupleIndex);

            return new KeyValuePair<int, ITuple3D>(
                tupleIndex,
                tuple
            );
        }

        public IEnumerable<KeyValuePair<int, ITuple3D>> AddTuples(IEnumerable<ITuple3D> tuplesList)
            => tuplesList.Select(AddTuple);

        public KeyValuePair<int, ITuple3D>[] AddTuples(params ITuple3D[] tuplesList)
            => tuplesList.Select(AddTuple).ToArray();

        public ITuple3D GetTuple(double x, double y, double z)
        {
            var tupleIndex = _tupleIndicesTable[x, y, z];

            return _tuplesList[tupleIndex];
        }

        public ITuple3D GetTuple(ITuple3D tuple)
        {
            var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y, tuple.Z];

            return _tuplesList[tupleIndex];
        }

        public int GetTupleIndex(double x, double y, double z)
        {
            return _tupleIndicesTable[x, y, z];
        }

        public int GetTupleIndex(ITuple3D tuple)
        {
            return _tupleIndicesTable[tuple.X, tuple.Y, tuple.Z];
        }

        public KeyValuePair<int, ITuple3D> GetTupleWithIndex(double x, double y, double z)
        {
            var tupleIndex = _tupleIndicesTable[x, y, z];

            return new KeyValuePair<int, ITuple3D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );
        }

        public KeyValuePair<int, ITuple3D> GetTupleWithIndex(ITuple3D tuple)
        {
            var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y, tuple.Z];

            return new KeyValuePair<int, ITuple3D>(
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
            var tuple = new Tuple3D(x, y, z);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, z, tupleIndex);

            return tupleIndex;
        }

        public int GetOrAddTupleIndex(ITuple3D tuple)
        {
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out var tupleIndex))
                return tupleIndex;

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tuple.Z, tupleIndex);

            return tupleIndex;
        }

        public KeyValuePair<int, ITuple3D> GetOrAddTupleWithIndex(double x, double y, double z)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
                return new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;
            var tuple = new Tuple3D(x, y, z);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, z, tupleIndex);

            return new KeyValuePair<int, ITuple3D>(
                tupleIndex,
                tuple
            );
        }

        public KeyValuePair<int, ITuple3D> GetOrAddTupleWithIndex(ITuple3D tuple)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out tupleIndex))
                return new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tuple.Z, tupleIndex);

            return new KeyValuePair<int, ITuple3D>(
                tupleIndex,
                tuple
            );
        }

        public bool TryGetTuple(double x, double y, double z, out ITuple3D outputTuple)
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

        public bool TryGetTuple(ITuple3D tuple, out ITuple3D outputTuple)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out tupleIndex))
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

        public bool TryGetTupleIndex(ITuple3D tuple, out int tupleIndex)
        {
            return _tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out tupleIndex);
        }

        public bool TryGetTupleWithIndex(double x, double y, double z, out KeyValuePair<int, ITuple3D> tupleWithIndex)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, z, out tupleIndex))
            {
                tupleWithIndex = new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

                return true;
            }

            tupleWithIndex = new KeyValuePair<int, ITuple3D>(-1, Tuple3D.Zero);
            return false;
        }

        public bool TryGetTupleWithIndex(ITuple3D tuple, out KeyValuePair<int, ITuple3D> tupleWithIndex)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, tuple.Z, out tupleIndex))
            {
                tupleWithIndex = new KeyValuePair<int, ITuple3D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

                return true;
            }

            tupleWithIndex = new KeyValuePair<int, ITuple3D>(-1, Tuple3D.Zero);
            return false;
        }

        public bool ContainsIndex(int index)
            => index >= 0 && index <= _tuplesList.Count;

        public bool ContainsIndices(int index1, int index2)
            => index1 >= 0 && index1 <= _tuplesList.Count &&
               index2 >= 0 && index2 <= _tuplesList.Count;

        public bool ContainsIndices(int index1, int index2, int index3)
            => index1 >= 0 && index1 <= _tuplesList.Count &&
               index2 >= 0 && index2 <= _tuplesList.Count &&
               index3 >= 0 && index3 <= _tuplesList.Count;


        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return _tuplesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tuplesList.GetEnumerator();
        }
    }
}
