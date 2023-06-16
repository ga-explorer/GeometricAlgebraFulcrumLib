using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
{
    public sealed class IdentityMap2D : 
        IAffineMap2D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 GetSquareMatrix3()
        {
            return SquareMatrix3.CreateIdentityMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            throw new NotImplementedException();
        }

        public bool SwapsHandedness 
            => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D MapPoint(IFloat64Tuple2D point)
        {
            return point.ToLinVector2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D MapVector(IFloat64Tuple2D vector)
        {
            return vector.ToLinVector2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D MapNormal(IFloat64Tuple2D normal)
        {
            return normal.ToLinVector2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap2D GetInverseAffineMap()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
    }
}
