using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
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
        public SquareMatrix4 ToSquareMatrix4()
        {
            return SquareMatrix4.CreateIdentityMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 ToMatrix4x4()
        {
            return Matrix4x4.Identity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] ToArray2D()
        {
            var array = new double[4, 4];

            array[0, 0] = 1d;
            array[1, 1] = 1d;
            array[2, 2] = 1d;
            array[3, 3] = 1d;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapPoint(ITuple3D point)
        {
            return point.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapVector(ITuple3D vector)
        {
            return vector.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapNormal(ITuple3D normal)
        {
            return normal.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D InverseMap()
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