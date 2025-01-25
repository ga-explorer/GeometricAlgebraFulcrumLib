//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Matrices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Scaling
//{
//    public sealed class VectorDirectionalScalingSequence :
//        ILinearMap,
//        IReadOnlyList<VectorDirectionalScaling>
//    {
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        //public static VectorDirectionalScalingSequence CreateFromMatrix(Matrix<double> matrix)
//        //{
//        //    // Make sure it's a square matrix
//        //    Debug.Assert(
//        //        matrix.RowCount == matrix.ColumnCount
//        //    );

//        //    var mapList = 
//        //        matrix
//        //            .GetSimpleEigenSubspaces()
//        //            .SelectMany(s => s.GetVectorDirectionalScalingMaps())
//        //            //.Select(s => s.ToVectorDirectionalScaling())
//        //            .ToList();

//        //    return new VectorDirectionalScalingSequence(matrix.RowCount, mapList);
//        //}

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorDirectionalScalingSequence CreateFromMatrix(Matrix<double> matrix)
//        {
//            return matrix.GetVectorDirectionalScalingSequence();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorDirectionalScalingSequence Create(int dimensions)
//        {
//            return new VectorDirectionalScalingSequence(dimensions);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorDirectionalScalingSequence Create(VectorDirectionalScaling scaling)
//        {
//            var reflectionSequence =
//                new VectorDirectionalScalingSequence(scaling.VSpaceDimensions);

//            reflectionSequence.AppendMap(scaling);

//            return reflectionSequence;
//        }


//        private readonly List<VectorDirectionalScaling> _mapList
//            = new List<VectorDirectionalScaling>();


//        public int Count
//            => _mapList.Count;

//        public VectorDirectionalScaling this[int index] 
//            => _mapList[index];
    
//        public int VSpaceDimensions { get; }

//        public bool SwapsHandedness
//            => _mapList
//                .Count(m => m.SwapsHandedness)
//                .IsOdd();


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private VectorDirectionalScalingSequence(int dimensions)
//        {
//            if (dimensions < 1)
//                throw new ArgumentOutOfRangeException(nameof(dimensions));

//            VSpaceDimensions = dimensions;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private VectorDirectionalScalingSequence(int dimensions, List<VectorDirectionalScaling> scalingFactorVectorList)
//        {
//            if (dimensions < 1)
//                throw new ArgumentOutOfRangeException(nameof(dimensions));

//            VSpaceDimensions = dimensions;
//            _mapList = scalingFactorVectorList;

//            Debug.Assert(IsValid());
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence AppendMap(double scalingFactor, Float64Tuple scalingVector)
//        {
//            if (scalingVector.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(
//                VectorDirectionalScaling.Create(scalingFactor, scalingVector)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence AppendMap(VectorDirectionalScaling scaling)
//        {
//            if (scaling.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(scaling);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence AppendMaps(IEnumerable<VectorDirectionalScaling> scalingList)
//        {
//            foreach (var scaling in scalingList)
//                AppendMap(scaling);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence PrependMap(VectorDirectionalScaling scaling)
//        {
//            if (scaling.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(0, scaling);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence InsertMap(int index, VectorDirectionalScaling scaling)
//        {
//            if (scaling.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(index, scaling);

//            return this;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsValid()
//        {
//            return _mapList.All(a => a.IsValid());
//        }

//        public bool IsIdentity()
//        {
//            if (_mapList.Count == 0)
//                return true;

//            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
//            {
//                var isSameVectorBasis =
//                    MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

//                if (!isSameVectorBasis) return false;
//            }

//            return true;
//        }

//        public bool IsNearIdentity(double zeroEpsilon = 1E-12)
//        {
//            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
//            {
//                var isSameVectorBasis =
//                    MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, zeroEpsilon);

//                if (!isSameVectorBasis) return false;
//            }

//            return true;
//        }

//        public double[] MapVectorInPlace(double[] vector)
//        {
//            foreach (var scaling in _mapList)
//            {
//                var u = scaling.ScalingVector.ScalarArray;
//                var s = (scaling.ScalingFactor - 1d) * vector.VectorDot(u);

//                for (var i = 0; i < VSpaceDimensions; i++)
//                    vector[i] += s * u[i];
//            }

//            return vector;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(
//                basisIndex >= 0 && basisIndex < VSpaceDimensions
//            );

//            if (_mapList.Count == 0)
//                return Float64Tuple.CreateBasis(VSpaceDimensions, basisIndex);

//            var x = new double[VSpaceDimensions];
//            x[basisIndex] = 1d;

//            return Float64Tuple.Create(
//                MapVectorInPlace(x)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapVector(Float64Tuple vector)
//        {
//            Debug.Assert(
//                vector.Dimensions == VSpaceDimensions
//            );

//            if (_mapList.Count == 0)
//                return vector;

//            var x = vector.GetScalarArrayCopy();

//            return Float64Tuple.Create(
//                MapVectorInPlace(x)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Matrix<double> ToMatrix()
//        {
//            var columnList =
//                VSpaceDimensions
//                    .GetRange()
//                    .Select(i => MapBasisVector(i).ScalarArray);

//            return Matrix<double>
//                .Build
//                .DenseOfColumnArrays(columnList);
//        }

//        public double[,] ToArray()
//        {
//            var array = new double[VSpaceDimensions, VSpaceDimensions];

//            for (var j = 0; j < VSpaceDimensions; j++)
//            {
//                var columnVector = MapBasisVector(j).ScalarArray;

//                for (var i = 0; i < VSpaceDimensions; i++) 
//                    array[i, j] = columnVector[i];
//            }

//            return array;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScalingSequence GetDirectionalScalingSequenceInverse()
//        {
//            if (_mapList.Count == 0)
//                return this;

//            var scalingFactorVectorList =
//                ((IEnumerable<VectorDirectionalScaling>) _mapList)
//                .Reverse()
//                .Select(t => t.GetVectorDirectionalScalingInverse())
//                .ToList();

//            return new VectorDirectionalScalingSequence(VSpaceDimensions, scalingFactorVectorList);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinearMap GetInverseMap()
//        {
//            return GetDirectionalScalingSequenceInverse();
//        }
    

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerator<VectorDirectionalScaling> GetEnumerator()
//        {
//            return _mapList.GetEnumerator();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}