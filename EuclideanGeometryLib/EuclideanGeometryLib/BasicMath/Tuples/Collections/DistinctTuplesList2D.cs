using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Tuples.Collections
{
    public class DistinctTuplesList2D : IReadOnlyCollection<ITuple2D>
    {
        private readonly Dictionary2Keys<double, int> _tupleIndicesTable =
            new Dictionary2Keys<double, int>();

        private readonly List<ITuple2D> _tuplesList;


        public int Count
            => _tupleIndicesTable.Count;

        public ITuple2D this[int index]
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
            _tuplesList = new List<ITuple2D>();
        }

        public DistinctTuplesList2D(int capacity)
        {
            _tuplesList = new List<ITuple2D>(capacity);
        }

        public DistinctTuplesList2D(ITuple2D tuple)
        {
            _tuplesList = new List<ITuple2D>();

            AddTuple(tuple);
        }

        public DistinctTuplesList2D(params ITuple2D[] tuplesList)
        {
            _tuplesList = new List<ITuple2D>(tuplesList.Length);

            AddTuples(tuplesList);
        }

        public DistinctTuplesList2D(IEnumerable<ITuple2D> tuplesList)
        {
            _tuplesList = new List<ITuple2D>();

            AddTuples(tuplesList);
        }


        public DistinctTuplesList2D Clear()
        {
            _tupleIndicesTable.Clear();
            _tuplesList.Clear();

            return this;
        }

        public KeyValuePair<int, ITuple2D> AddTuple(double x, double y)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
                return new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;
            var tuple = new Tuple2D(x, y);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, tupleIndex);

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                tuple
            );
        }

        public KeyValuePair<int, ITuple2D> AddTuple(ITuple2D tuple)
        {
            if (ReferenceEquals(tuple, null))
                throw new ArgumentNullException(nameof(tuple));

            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
                return new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                tuple
            );
        }

        public IEnumerable<KeyValuePair<int, ITuple2D>> AddTuples(IEnumerable<ITuple2D> tuplesList)
            => tuplesList.Select(AddTuple);

        public KeyValuePair<int, ITuple2D>[] AddTuples(params ITuple2D[] tuplesList)
            => tuplesList.Select(AddTuple).ToArray();

        public ITuple2D GetTuple(double x, double y)
        {
            var tupleIndex = _tupleIndicesTable[x, y];

            return _tuplesList[tupleIndex];
        }

        public ITuple2D GetTuple(ITuple2D tuple)
        {
            var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y];

            return _tuplesList[tupleIndex];
        }

        public int GetTupleIndex(double x, double y)
        {
            return _tupleIndicesTable[x, y];
        }

        public int GetTupleIndex(ITuple2D tuple)
        {
            return _tupleIndicesTable[tuple.X, tuple.Y];
        }

        public int GetOrAddTupleIndex(double x, double y)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
                return tupleIndex;

            tupleIndex = _tuplesList.Count;
            var tuple = new Tuple2D(x, y);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, tupleIndex);

            return tupleIndex;
        }

        public int GetOrAddTupleIndex(ITuple2D tuple)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
                return tupleIndex;

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

            return tupleIndex;
        }

        public KeyValuePair<int, ITuple2D> GetTupleWithIndex(double x, double y)
        {
            var tupleIndex = _tupleIndicesTable[x, y];

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );
        }

        public KeyValuePair<int, ITuple2D> GetTupleWithIndex(ITuple2D tuple)
        {
            var tupleIndex = _tupleIndicesTable[tuple.X, tuple.Y];

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                _tuplesList[tupleIndex]
            );
        }

        public KeyValuePair<int, ITuple2D> GetOrAddTupleWithIndex(double x, double y)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
                return new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;
            var tuple = new Tuple2D(x, y);

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(x, y, tupleIndex);

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                tuple
            );
        }

        public KeyValuePair<int, ITuple2D> GetOrAddTupleWithIndex(ITuple2D tuple)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
                return new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

            tupleIndex = _tuplesList.Count;

            _tuplesList.Add(tuple);
            _tupleIndicesTable.Add(tuple.X, tuple.Y, tupleIndex);

            return new KeyValuePair<int, ITuple2D>(
                tupleIndex,
                tuple
            );
        }

        public bool TryGetTuple(double x, double y, out ITuple2D outputTuple)
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

        public bool TryGetTuple(ITuple2D tuple, out ITuple2D outputTuple)
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

        public bool TryGetTupleIndex(ITuple2D tuple, out int tupleIndex)
        {
            return _tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex);
        }

        public bool TryGetTupleWithIndex(double x, double y, out KeyValuePair<int, ITuple2D> tupleWithIndex)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(x, y, out tupleIndex))
            {
                tupleWithIndex = new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

                return true;
            }

            tupleWithIndex = new KeyValuePair<int, ITuple2D>(-1, Tuple2D.Zero);
            return false;
        }

        public bool TryGetTupleWithIndex(ITuple2D tuple, out KeyValuePair<int, ITuple2D> tupleWithIndex)
        {
            int tupleIndex;
            if (_tupleIndicesTable.TryGetValue(tuple.X, tuple.Y, out tupleIndex))
            {
                tupleWithIndex = new KeyValuePair<int, ITuple2D>(
                    tupleIndex,
                    _tuplesList[tupleIndex]
                );

                return true;
            }

            tupleWithIndex = new KeyValuePair<int, ITuple2D>(-1, Tuple2D.Zero);
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


        public IEnumerator<ITuple2D> GetEnumerator()
        {
            return _tuplesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tuplesList.GetEnumerator();
        }
    }
}