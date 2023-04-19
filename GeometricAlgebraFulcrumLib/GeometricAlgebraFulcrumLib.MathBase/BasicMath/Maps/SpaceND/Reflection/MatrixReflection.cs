//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using DataStructuresLib.BitManipulation;
//using DataStructuresLib.Extensions;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Rotation;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Reflection
//{
//    public sealed class MatrixReflection :
//        ReflectionLinearMap
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static MatrixReflection CreateFromReflection(ReflectionLinearMap reflection)
//        {
//            var reflectionArray =
//                reflection.ToArray();

//            return new MatrixReflection(reflectionArray);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static MatrixReflection CreateFromRotation(RotationLinearMap rotation)
//        {
//            var rotationArray =
//                rotation.ToArray();

//            return new MatrixReflection(rotationArray);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static MatrixReflection CreateForwardClarkeRotation(int size)
//        {
//            var rotationArray =
//                Float64ArrayUtils.CreateClarkeRotationArray(size);

//            return new MatrixReflection(rotationArray);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static MatrixReflection CreateInverseClarkeRotation(int size)
//        {
//            var rotationArray =
//                Float64ArrayUtils.CreateClarkeRotationArray(size).Transpose();

//            return new MatrixReflection(rotationArray);
//        }


//        private readonly double[,] _rotationArray;


//        public override int VSpaceDimensions
//            => _rotationArray.GetLength(0);

//        public override bool SwapsHandedness 
//            => _rotationArray.Determinant() < 0;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private MatrixReflection(double[,] rotationArray)
//        {
//            Debug.Assert(
//                rotationArray.Determinant().Abs().IsNearOne()
//            );

//            _rotationArray = rotationArray;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return _rotationArray.Determinant().Abs().IsNearOne();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsIdentity()
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(
//                basisIndex >= 0 && basisIndex < VSpaceDimensions
//            );

//            return Float64Tuple.Create(
//                VSpaceDimensions.GetRange().Select(i => _rotationArray[i, basisIndex])
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            Debug.Assert(
//                vector.Dimensions == VSpaceDimensions
//            );

//            return Float64Tuple.Create(
//                _rotationArray.MatrixProduct(vector.ScalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ReflectionLinearMap GetReflectionLinearMapInverse()
//        {
//            return new MatrixReflection(
//                _rotationArray.Transpose()
//            );
//        }
    
//        public override double[,] ToArray()
//        {
//            var array = new double[VSpaceDimensions, VSpaceDimensions];

//            for (var j = 0; j < VSpaceDimensions; j++)
//            for (var i = 0; i < VSpaceDimensions; i++)
//                array[i, j] = _rotationArray[i, j];

//            return array;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Matrix<double> ToMatrix()
//        {
//            return _rotationArray.ToMatrix();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//        {
//            return HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
//                _rotationArray.ToMatrix()
//            );
//        }
//    }
//}