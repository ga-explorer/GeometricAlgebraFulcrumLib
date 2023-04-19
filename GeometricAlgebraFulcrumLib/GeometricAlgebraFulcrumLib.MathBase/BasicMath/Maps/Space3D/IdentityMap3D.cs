using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
{
    public sealed record IdentityMap3D :
        IRotateMap3D
    {
        public static IdentityMap3D DefaultMap { get; }
            = new IdentityMap3D();


        public bool SwapsHandedness
            => false;


        private IdentityMap3D()
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 GetSquareMatrix4()
        {
            return SquareMatrix4.CreateIdentityMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 GetMatrix4x4()
        {
            return Matrix4x4.Identity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            var array = new double[4, 4];

            array[0, 0] = 1d;
            array[1, 1] = 1d;
            array[2, 2] = 1d;
            array[3, 3] = 1d;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapPoint(IFloat64Tuple3D point)
        {
            return point.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapVector(IFloat64Tuple3D vector)
        {
            return vector.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapNormal(IFloat64Tuple3D normal)
        {
            return normal.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRotateMap3D InverseRotateMap()
        {
            return this;
        }
    }
}