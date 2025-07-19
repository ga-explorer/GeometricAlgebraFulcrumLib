using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public sealed class RGaFloat64Bivector :
    RGaFloat64KVector
{
    public sealed record IndexMapper
    {
        public int Size { get; }

        public int LinearLength { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexMapper(int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            Size = size;
            LinearLength = Size * (Size - 1) / 2;
        }


        /// <summary>
        /// Compute linear index from (i, j)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetLinearIndex(int i, int j)
        {
            if (i >= j || i < 0 || j >= Size)
                throw new ArgumentException("Invalid indices. Must satisfy 0 ≤ i < j < n");

            var i1 = -(i + 1);
            return i * ((Size << 1) + i1) / 2 + j + i1;
        }

        /// <summary>
        /// Recover (i, j) from linear index k
        /// </summary>
        /// <param name="linearIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Pair<int> GetIndexPair(int linearIndex)
        {
            if (linearIndex < 0 || linearIndex >= LinearLength)
                throw new ArgumentException("Invalid linear index");

            // Binary search to find the row i such that the cumulative
            // count up to i is <= k
            int low = 0, high = Size - 2;
            while (low < high)
            {
                var mid = (low + high + 1) / 2;
                var countUpToMid = mid * (2 * Size - mid - 1) / 2;

                if (countUpToMid <= linearIndex)
                    low = mid;
                else
                    high = mid - 1;
            }

            var i = low;
            var offset = linearIndex - i * (2 * Size - i - 1) / 2;
            var j = i + offset + 1;

            return new Pair<int>(i, j);
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
        => 2;
        
    public int VSpaceDimensions 
        => _indexMapper.Size;

    
    public IEnumerable<double> Scalars 
        => _scalarArray;

    public IEnumerable<Tuple<int, int>> IndexTuples
    {
        get
        {
            for (var i = 0; i < VSpaceDimensions - 1; i++)
            for (var j = i + 1; j < VSpaceDimensions; j++)
                yield return new Tuple<int, int>(i, j);
        }
    }

    public IEnumerable<Tuple<int, int, double>> IndexScalarTuples
    {
        get
        {
            var linearIndex = 0;
            for (var i = 0; i < VSpaceDimensions - 1; i++)
            for (var j = i + 1; j < VSpaceDimensions; j++)
                yield return new Tuple<int, int, double>(
                    i, j, 
                    _scalarArray[linearIndex++]
                );
        }
    }

    public double this[int i, int j]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetItem(i, j);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => SetItem(i, j, value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector(RGaFloat64Processor processor, int vSpaceDimensions)
        : base(processor)
    {
        if (vSpaceDimensions is < 3 or > 16384)
            throw new InvalidOperationException();

        _indexMapper = new IndexMapper(vSpaceDimensions);
        _scalarArray = _indexMapper.CreateArray();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetItem(int i, int j)
    {
        if (i < j)
            return _scalarArray[_indexMapper.GetLinearIndex(i, j)];

        if (j < i)
            return -_scalarArray[_indexMapper.GetLinearIndex(j, i)];

        throw new InvalidOperationException();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector SetItem(int i, int j, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        if (i < j)
        {
            _scalarArray[_indexMapper.GetLinearIndex(i, j)] = value;
            return this;
        }

        if (j < i)
        {
            _scalarArray[_indexMapper.GetLinearIndex(j, i)] = -value;
            return this;

        }

        throw new InvalidOperationException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector AddItem(int i, int j, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        if (i < j)
        {
            _scalarArray[_indexMapper.GetLinearIndex(i, j)] += value;
            return this;
        }

        if (j < i)
        {
            _scalarArray[_indexMapper.GetLinearIndex(j, i)] -= value;
            return this;

        }

        throw new InvalidOperationException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector SubtractItem(int i, int j, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        if (i < j)
        {
            _scalarArray[_indexMapper.GetLinearIndex(i, j)] -= value;
            return this;
        }

        if (j < i)
        {
            _scalarArray[_indexMapper.GetLinearIndex(j, i)] += value;
            return this;

        }

        throw new InvalidOperationException();
    }

}