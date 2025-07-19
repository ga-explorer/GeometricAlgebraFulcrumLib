using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense
{
    public sealed class RGaFloat64Trivector :
        RGaFloat64KVector
    {
        public sealed class IndexMapper
        {
            private readonly int[] _cumTripletCounts;
            private readonly int[][] _cumPairCounts;

            public int Size { get; }
            
            public int LinearLength { get; }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IndexMapper(int size)
            {
                Size = size;

                // Precompute cumulative triplet counts: how many triplets exist before each i
                _cumTripletCounts = new int[Size + 1];
                for (var i = 1; i <= Size; i++)
                {
                    _cumTripletCounts[i] = _cumTripletCounts[i - 1];
                    if (Size - i >= 2)
                        _cumTripletCounts[i] += (Size - i) * (Size - i - 1) / 2;
                }

                // For each i, precompute cumulative pair counts (j, k) such that i < j < k
                _cumPairCounts = new int[Size][];
                for (var i = 0; i < Size; i++)
                {
                    _cumPairCounts[i] = new int[Size - i + 1];
                    for (var j = 1; j < Size - i; j++)
                    {
                        _cumPairCounts[i][j] = _cumPairCounts[i][j - 1];
                        if (Size - (i + j) >= 1)
                            _cumPairCounts[i][j] += Size - (i + j) - 1;
                    }
                }

                LinearLength = _cumTripletCounts[Size];
            }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetLinearIndex(int i, int j, int k)
            {
                if (!(0 <= i && i < j && j < k && k < Size))
                    throw new ArgumentException("Invalid indices. Must satisfy 0 ≤ i < j < k < n");

                // Total triplets before this i
                var index = _cumTripletCounts[i];

                // Within this i, subtract how many (j,k) pairs are before current j
                var offsetWithinI = _cumPairCounts[i][j - i - 1];

                // Add offset for k
                var offsetWithinJ = k - j - 1;

                return index + offsetWithinI + offsetWithinJ;
            }

            public Triplet<int> GetIndexTriplet(int linearIndex)
            {
                if (linearIndex < 0 || linearIndex >= LinearLength)
                    throw new ArgumentException("Invalid linear index");

                // Binary search to find the right i
                int low = 0, high = Size - 2;
                while (low < high)
                {
                    var mid = (low + high + 1) / 2;
                    if (_cumTripletCounts[mid] <= linearIndex)
                        low = mid;
                    else
                        high = mid - 1;
                }
                var i = low;

                var remaining = linearIndex - _cumTripletCounts[i];

                // Now binary search for j inside i
                low = i + 1;
                high = Size - 1;
                var pairCountArray = _cumPairCounts[i];
                while (low < high)
                {
                    var mid = (low + high + 1) / 2;
                    var jOffset = mid - i - 1;
                    if (pairCountArray[jOffset] <= remaining)
                        low = mid;
                    else
                        high = mid - 1;
                }
                var j = low;

                // Remaining after accounting for (i, j)
                var pairCountUpToJ = _cumPairCounts[i][low - i - 1];
                var kOffset = remaining - pairCountUpToJ;

                var k = j + kOffset + 1;

                return new Triplet<int>(i, j, k);
            }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double[] CreateArray()
            {
                return new double[LinearLength];
            }
        }

        
        private readonly IndexMapper _indexMapper;
        private readonly double[] _scalarArray;


        public override int Grade 
            => 3;
        
        public int VSpaceDimensions 
            => _indexMapper.Size;

        
        public IEnumerable<double> Scalars 
            => _scalarArray;

        public IEnumerable<Tuple<int, int, int>> IndexTuples
        {
            get
            {
                for (var i = 0; i < VSpaceDimensions - 2; i++)
                for (var j = i + 1; j < VSpaceDimensions - 1; j++)
                for (var k = j + 1; k < VSpaceDimensions; k++)
                    yield return new Tuple<int, int, int>(i, j, k);
            }
        }

        public IEnumerable<Tuple<int, int, int, double>> IndexScalarTuples
        {
            get
            {
                var linearIndex = 0;
                for (var i = 0; i < VSpaceDimensions - 2; i++)
                for (var j = i + 1; j < VSpaceDimensions - 1; j++)
                for (var k = j + 1; k < VSpaceDimensions; k++)
                    yield return new Tuple<int, int, int, double>(
                        i, j, k,
                        _scalarArray[linearIndex++]
                    );
            }
        }

        public double this[int i, int j, int k]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetItem(i, j, k);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => SetItem(i, j, k, value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Trivector(RGaFloat64Processor processor, int vSpaceDimensions)
            : base(processor)
        {
            if (vSpaceDimensions is < 3 or > 1024)
                throw new InvalidOperationException();

            _indexMapper = new IndexMapper(vSpaceDimensions);
            _scalarArray = _indexMapper.CreateArray();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetItem(int i, int j, int k)
        {
            if (i < j)
            {
                if (j < k) return _scalarArray[_indexMapper.GetLinearIndex(i, j, k)];
                if (i < k) return -_scalarArray[_indexMapper.GetLinearIndex(i, k, j)];
                if (k < i) return _scalarArray[_indexMapper.GetLinearIndex(k, i, j)];
            }

            else if (j < i)
            {
                if (i < k) return -_scalarArray[_indexMapper.GetLinearIndex(j, i, k)];
                if (j < k) return _scalarArray[_indexMapper.GetLinearIndex(j, k, i)];
                if (k < j) return -_scalarArray[_indexMapper.GetLinearIndex(k, j, i)];
            }

            throw new InvalidOperationException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Trivector SetItem(int i, int j, int k, double value)
        {
            if (!value.IsValid())
                throw new InvalidOperationException();

            if (i < j)
            {
                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, j, k)] = value;
                    return this;
                }

                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, k, j)] = -value;
                    return this;
                }

                if (k < i)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, i, j)] = value;
                    return this;
                }
            }

            else if (j < i)
            {
                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, i, k)] = -value;
                    return this;
                }

                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, k, i)] = value;
                    return this;
                }

                if (k < j)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, j, i)] = -value;
                    return this;
                }
            }

            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Trivector AddItem(int i, int j, int k, double value)
        {
            if (!value.IsValid())
                throw new InvalidOperationException();

            if (i < j)
            {
                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, j, k)] += value;
                    return this;
                }

                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, k, j)] -= value;
                    return this;
                }

                if (k < i)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, i, j)] += value;
                    return this;
                }
            }

            else if (j < i)
            {
                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, i, k)] -= value;
                    return this;
                }

                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, k, i)] += value;
                    return this;
                }

                if (k < j)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, j, i)] -= value;
                    return this;
                }
            }

            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Trivector SubtractItem(int i, int j, int k, double value)
        {
            if (!value.IsValid())
                throw new InvalidOperationException();

            if (i < j)
            {
                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, j, k)] -= value;
                    return this;
                }

                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(i, k, j)] += value;
                    return this;
                }

                if (k < i)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, i, j)] -= value;
                    return this;
                }
            }

            else if (j < i)
            {
                if (i < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, i, k)] += value;
                    return this;
                }

                if (j < k)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(j, k, i)] -= value;
                    return this;
                }

                if (k < j)
                {
                    _scalarArray[_indexMapper.GetLinearIndex(k, j, i)] += value;
                    return this;
                }
            }

            throw new InvalidOperationException();
        }

        
    }
}
