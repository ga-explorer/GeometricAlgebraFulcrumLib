using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class IdentityMap2D : 
        IAffineMap2D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 ToSquareMatrix3()
        {
            return SquareMatrix3.CreateIdentityMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D MapPoint(ITuple2D point)
        {
            return point.ToTuple2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D MapVector(ITuple2D vector)
        {
            return vector.ToTuple2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D MapNormal(ITuple2D normal)
        {
            return normal.ToTuple2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap2D InverseMap()
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
