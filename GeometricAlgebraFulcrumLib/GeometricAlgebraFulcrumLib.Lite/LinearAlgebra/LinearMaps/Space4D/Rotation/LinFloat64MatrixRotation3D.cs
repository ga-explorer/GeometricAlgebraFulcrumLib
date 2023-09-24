using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Rotation
{
    public sealed class LinFloat64MatrixRotation4D :
        LinFloat64RotationBase4D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixRotation4D CreateFromRotation(LinFloat64RotationBase4D rotation)
        {
            var rotationArray = 
                rotation.ToSquareMatrix3();

            return new LinFloat64MatrixRotation4D(rotationArray);
        }
        
    
        private readonly SquareMatrix4 _rotationArray;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64MatrixRotation4D(SquareMatrix4 rotationArray)
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
        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            return _rotationArray.ColumnToTuple4D(basisIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVector(IFloat64Vector4D vector)
        {
            return _rotationArray * vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64RotationBase4D GetVectorRotationInverse()
        {
            return new LinFloat64MatrixRotation4D(
                _rotationArray.Transpose()
            );
        }
    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
        {
            return LinFloat64HyperPlaneNormalReflectionSequence4D.CreateFromReflectionMatrix(
                _rotationArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorToVectorRotationSequence4D ToVectorToVectorRotationSequence()
        {
            return LinFloat64VectorToVectorRotationSequence4D.CreateFromRotationMatrix(
                _rotationArray
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