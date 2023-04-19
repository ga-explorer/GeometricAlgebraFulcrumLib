using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64MatrixRotation :
        LinFloat64RotationBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixRotation CreateFromRotation(LinFloat64RotationBase rotation, int size)
        {
            var rotationArray = 
                rotation.ToArray(size, size);

            return new LinFloat64MatrixRotation(rotationArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixRotation CreateForwardClarkeRotation(int size)
        {
            var rotationArray = 
                Float64ArrayUtils.CreateClarkeRotationArray(size);

            return new LinFloat64MatrixRotation(rotationArray);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixRotation CreateInverseClarkeRotation(int size)
        {
            var rotationArray = 
                Float64ArrayUtils.CreateClarkeRotationArray(size).Transpose();

            return new LinFloat64MatrixRotation(rotationArray);
        }

    
        private readonly double[,] _rotationArray;


        public override int VSpaceDimensions
            => _rotationArray.GetLength(0);

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64MatrixRotation(double[,] rotationArray)
        {
            Debug.Assert(
                rotationArray.Determinant().IsNearOne()
            );

            _rotationArray = rotationArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotationArray.Determinant().IsNearOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1E-12)
        {
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            return _rotationArray.ColumnToLinVector(basisIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            return _rotationArray.MatrixProduct(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64RotationBase GetVectorRotationInverse()
        {
            return new LinFloat64MatrixRotation(
                _rotationArray.Transpose()
            );
        }
    

        public override double[,] ToArray(int rowCount, int colCount)
        {
            var array = new double[VSpaceDimensions, VSpaceDimensions];

            for (var j = 0; j < rowCount; j++)
            for (var i = 0; i < colCount; i++) 
                array[i, j] = _rotationArray[i, j];

            return array;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Matrix<double> ToMatrix(int rowCount, int colCount)
        {
            return ToArray(rowCount, colCount).ToMatrix();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
        {
            return LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
                _rotationArray.ToMatrix()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
        {
            return LinFloat64VectorToVectorRotationSequence.CreateFromRotationMatrix(
                _rotationArray.ToMatrix()
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
        //{
        //    return SimpleRotationSequence.CreateFromRotationMatrix(
        //        _rotationArray.ToMatrix()
        //    );
        //}
    }
}