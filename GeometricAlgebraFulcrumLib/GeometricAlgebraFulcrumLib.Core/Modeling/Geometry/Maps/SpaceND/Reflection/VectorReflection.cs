//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Reflection
//{
//    public sealed class VectorReflection :
//        ILinearMap
//    {
//        public Float64Tuple ReflectionVector { get; }

//        public int VSpaceDimensions { get; }

//        public bool SwapsHandedness 
//            => true;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorReflection(int dimensions, Float64Tuple vector)
//        {
//            Debug.Assert(vector.GetVectorNormSquared().IsNearOne());

//            VSpaceDimensions = dimensions;
//            ReflectionVector = vector;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsValid()
//        {
//            return ReflectionVector.IsValid();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsIdentity()
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(basisIndex >= 0 && basisIndex < VSpaceDimensions);

//            var y = new double[VSpaceDimensions];

//            var u = ReflectionVector.ScalarArray;
//            var s = 2d * u[basisIndex];

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] = s * u[i];

//            y[basisIndex] -= 1d;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapVector(Float64Tuple vector)
//        {
//            var y = new double[VSpaceDimensions];

//            var x = vector.ScalarArray;
//            var u = ReflectionVector.ScalarArray;
//            var s = 2d * x.VectorDot(u);

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] = s * u[i] - x[i];

//            return Float64Tuple.Create(y);
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
//        public VectorReflection GetVectorReflectionInverse()
//        {
//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinearMap GetInverseMap()
//        {
//            return GetVectorReflectionInverse();
//        }
//    }
//}
