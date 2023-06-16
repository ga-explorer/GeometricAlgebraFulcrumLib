using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Structures
{
    public sealed class GaBivectorSparseList :
        IReadOnlyCollection<double>
    {
        private readonly Dictionary<Pair<ulong>, double> _numbersDictionary
            = new Dictionary<Pair<ulong>, double>();


        public int Count 
            => _numbersDictionary.Count;

        public ulong GaSpaceDimensions { get; }

        public double this[ulong index1, ulong index2]
        {
            get
            {
                Debug.Assert(
                    index1 < GaSpaceDimensions && 
                    index2 < GaSpaceDimensions
                );

                if (index1 < index2)
                {
                    var index = new Pair<ulong>(index1, index2);

                    return _numbersDictionary.TryGetValue(index, out var number)
                        ? number
                        : 0d;
                }
                
                if (index1 > index2)
                {
                    var index = new Pair<ulong>(index2, index1);

                    return _numbersDictionary.TryGetValue(index, out var number)
                        ? -number
                        : 0d;
                }

                return 0;
            }
            set
            {
                Debug.Assert(
                    index1 < GaSpaceDimensions && 
                    index2 < GaSpaceDimensions && 
                    value.IsValid()
                );

                if (index1 < index2)
                {
                    var index = new Pair<ulong>(index1, index2);

                    if (_numbersDictionary.ContainsKey(index))
                    {
                        if (value == 0d)
                            _numbersDictionary.Remove(index);
                        else
                            _numbersDictionary[index] = value;
                    }
                    else
                        _numbersDictionary.Add(index, value);
                }
                else if (index1 > index2)
                {
                    var index = new Pair<ulong>(index2, index1);

                    if (_numbersDictionary.ContainsKey(index))
                    {
                        if (value == 0d)
                            _numbersDictionary.Remove(index);
                        else
                            _numbersDictionary[index] = -value;
                    }
                    else
                        _numbersDictionary.Add(index, -value);
                }
            }
        }

        public int StoredPairsCount 
            => _numbersDictionary.Count;

        public IEnumerable<ulong> StoredIDs 
            => _numbersDictionary.Keys.Select(
                indexPair => 
                    (1UL << (int) indexPair.Item1) | 
                    (1UL << (int) indexPair.Item2)
            );

        public IEnumerable<double> StoredNumbers 
            => _numbersDictionary.Values;

        public IEnumerable<KeyValuePair<ulong, double>> StoredIdNumberPairs
            => _numbersDictionary.Select(
                p => 
                    new KeyValuePair<ulong, double>(
                        (1UL << (int) p.Key.Item1) | 
                        (1UL << (int) p.Key.Item2),
                        p.Value
                    )
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorSparseList(ulong gaSpaceDimensions)
        {
            if (BitOperations.PopCount(gaSpaceDimensions) != 1)
                throw new ArgumentException(nameof(gaSpaceDimensions));

            GaSpaceDimensions = gaSpaceDimensions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GaBivectorSparseList(ulong gaSpaceDimensions, Dictionary<Pair<ulong>, double> numbersDictionary)
        {
            if (BitOperations.PopCount(gaSpaceDimensions) != 1)
                throw new ArgumentException(nameof(gaSpaceDimensions));

            GaSpaceDimensions = gaSpaceDimensions;
            _numbersDictionary = numbersDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _numbersDictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorSparseList RemoveNearZeroNumbers(double zeroEpsilon)
        {
            var keys = 
                _numbersDictionary
                    .Where(p => p.Value.IsNearZero(zeroEpsilon))
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var key in keys)
                _numbersDictionary.Remove(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsStoredId(Pair<ulong> id)
        {
            return _numbersDictionary.ContainsKey(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetStoredNumber(Pair<ulong> id, out double number)
        {
            return _numbersDictionary.TryGetValue(id, out number);
        }


        public GaBivectorSparseList MapNumbers(Func<double, double> numberMapping)
        {
            var numbersDictionary = new Dictionary<Pair<ulong>, double>();

            foreach (var (id, scalar) in _numbersDictionary)
            {
                var scalar1 = numberMapping(scalar);

                if (scalar1 != 0)
                    numbersDictionary.Add(id, scalar1);
            }

            return new GaBivectorSparseList(GaSpaceDimensions, numbersDictionary);
        }
        
        public GaBivectorSparseList MapNumbers(Func<ulong, ulong, double, double> numberMapping)
        {
            var numbersDictionary = new Dictionary<Pair<ulong>, double>();

            foreach (var (id, scalar) in _numbersDictionary)
            {
                var scalar1 = numberMapping(id.Item1, id.Item2, scalar);

                if (scalar1 != 0)
                    numbersDictionary.Add(id, scalar1);
            }

            return new GaBivectorSparseList(GaSpaceDimensions, numbersDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorSparseList FilterById(Func<ulong, ulong, bool> filterPredicate)
        {
            var numbersDictionary =
                _numbersDictionary
                    .Where(p => filterPredicate(p.Key.Item1, p.Key.Item2))
                    .ToDictionary(p => p.Key, p => p.Value);

            return new GaBivectorSparseList(GaSpaceDimensions, numbersDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorSparseList FilterByNumber(Predicate<double> filterPredicate)
        {
            var numbersDictionary =
                _numbersDictionary
                    .Where(p => filterPredicate(p.Value))
                    .ToDictionary(p => p.Key, p => p.Value);

            return new GaBivectorSparseList(GaSpaceDimensions, numbersDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorSparseList FilterByIdNumber(Func<ulong, ulong, double, bool> filterPredicate)
        {
            var numbersDictionary =
                _numbersDictionary
                    .Where(p => filterPredicate(p.Key.Item1, p.Key.Item2, p.Value))
                    .ToDictionary(p => p.Key, p => p.Value);

            return new GaBivectorSparseList(GaSpaceDimensions, numbersDictionary);
        }

        public Pair<ulong> GetMaxNumberMagnitudeId()
        {
            if (_numbersDictionary.Count == 0)
                throw new InvalidOperationException();

            var (maxValueId, maxValue) = _numbersDictionary.First();
            maxValue = maxValue.Abs();

            foreach (var (id, number) in _numbersDictionary)
            {
                var absNumber = number.Abs();

                if (absNumber <= maxValue) continue;

                maxValue = absNumber;
                maxValueId = id;
            }

            return maxValueId;
        }
        
        public Pair<ulong> GetMinNumberMagnitudeId()
        {
            if (_numbersDictionary.Count == 0)
                throw new InvalidOperationException();

            var (minValueId, minValue) = _numbersDictionary.First();
            minValue = minValue.Abs();

            if (minValue == 0d) return minValueId;

            foreach (var (id, number) in _numbersDictionary)
            {
                var absNumber = number.Abs();

                if (absNumber >= minValue) continue;

                minValue = absNumber;
                minValueId = id;

                if (minValue == 0d) return minValueId;
            }

            return minValueId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<double> GetEnumerator()
        {
            for (var i = 0UL; i < GaSpaceDimensions - 1; i++)
            for (var j = i + 1; j < GaSpaceDimensions; j++)
                yield return this[i, j];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}