using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection
{
    public sealed class LinFloat64MatrixReflection3D :
        LinFloat64ReflectionBase3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64MatrixReflection3D CreateFromReflection(LinFloat64ReflectionBase3D reflection)
        {
            var reflectionArray =
                reflection.ToSquareMatrix3();

            return new LinFloat64MatrixReflection3D(reflectionArray);
        }
        

        private readonly SquareMatrix3 _reflectionArray;

        
        public override bool SwapsHandedness 
            => _reflectionArray.Determinant.IsNegative();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64MatrixReflection3D(SquareMatrix3 rotationArray)
        {
            Debug.Assert(
                rotationArray.Determinant.Abs().IsNearOne()
            );

            _reflectionArray = rotationArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _reflectionArray.IsValid() && 
                   _reflectionArray.Determinant.Abs().IsNearOne();
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
            Debug.Assert(
                basisIndex >= 0
            );

            return basisIndex < 3
                ? _reflectionArray.ColumnToTuple3D(basisIndex)
                : Float64Vector3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            return _reflectionArray * vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
        {
            return new LinFloat64MatrixReflection3D(
                _reflectionArray.Transpose()
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
        {
            return LinFloat64HyperPlaneNormalReflectionSequence3D.CreateFromReflectionMatrix(_reflectionArray);
        }
    }
}