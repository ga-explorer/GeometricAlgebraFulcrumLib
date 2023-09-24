using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Rotation
{
    public sealed class LinFloat64MatrixRotation3D :
        LinFloat64Rotation3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixRotation3D CreateFromRotation(LinFloat64Rotation3D rotation)
        {
            var rotationArray = 
                rotation.ToSquareMatrix3();

            return new LinFloat64MatrixRotation3D(rotationArray);
        }
        
    
        private readonly SquareMatrix3 _rotationArray;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64MatrixRotation3D(SquareMatrix3 rotationArray)
        {
            Debug.Assert(
                rotationArray.Determinant.IsNearOne()
            );

            _rotationArray = rotationArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotationArray.IsValid() && 
                   _rotationArray.Determinant.IsNearOne();
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
        public override Float64Vector3D MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            return _rotationArray.ColumnToTuple3D(basisIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            return _rotationArray * vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64MatrixRotation3D GetInverseMatrixRotation()
        {
            return new LinFloat64MatrixRotation3D(
                _rotationArray.Transpose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Quaternion GetQuaternion()
        {
            return _rotationArray.ToQuaternion();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Rotation3D GetInverseRotation()
        {
            return GetInverseMatrixRotation();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
        {
            return LinFloat64HyperPlaneNormalReflectionSequence3D.CreateFromReflectionMatrix(
                _rotationArray
            );
        }
    }
}