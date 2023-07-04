using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

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
        public Float64Vector3D MapPoint(IFloat64Vector3D point)
        {
            return point.ToVector3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            return vector.ToVector3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapNormal(IFloat64Vector3D normal)
        {
            return normal.ToVector3D();
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