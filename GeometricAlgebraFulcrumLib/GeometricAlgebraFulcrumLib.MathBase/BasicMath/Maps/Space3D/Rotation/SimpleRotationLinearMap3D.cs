using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D.Rotation
{
    public abstract class SimpleRotationLinearMap3D :
        IAffineMap3D
    {
        public bool SwapsHandedness
            => false;


        public abstract bool IsValid();

        public abstract bool IsIdentity();

        public abstract bool IsNearIdentity(double epsilon = 1e-12d);

        public abstract Float64Tuple3D MapAxis(Axis3D axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 GetSquareMatrix4()
        {
            var e1 = MapAxis(Axis3D.PositiveX);
            var e2 = MapAxis(Axis3D.PositiveY);
            var e3 = MapAxis(Axis3D.PositiveZ);

            return new SquareMatrix4
            {
                [0, 0] = e1.X,
                [1, 0] = e1.Y,
                [2, 0] = e1.Z,

                [0, 1] = e2.X,
                [1, 1] = e2.Y,
                [2, 1] = e2.Z,

                [0, 2] = e3.X,
                [1, 2] = e3.Y,
                [2, 2] = e3.Z,

                [3, 3] = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 GetMatrix4x4()
        {
            var e1 = MapAxis(Axis3D.PositiveX);
            var e2 = MapAxis(Axis3D.PositiveY);
            var e3 = MapAxis(Axis3D.PositiveZ);

            return new Matrix4x4(
                (float)e1.X, (float)e2.X, (float)e3.X, 0f,
                (float)e1.Y, (float)e2.Y, (float)e3.Y, 0f,
                (float)e1.Z, (float)e2.Z, (float)e3.Z, 0f,
                0f, 0f, 0f, 1f
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            var e1 = MapAxis(Axis3D.PositiveX);
            var e2 = MapAxis(Axis3D.PositiveY);
            var e3 = MapAxis(Axis3D.PositiveZ);

            return new[,]
            {
                { e1.X, e2.X, e3.X, 0d },
                { e1.Y, e2.Y, e3.Y, 0d },
                { e1.Z, e2.Z, e3.Z, 0d },
                { 0d, 0d, 0d, 1d }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapPoint(IFloat64Tuple3D point)
        {
            return MapVector(point);
        }

        public abstract Float64Tuple3D MapVector(IFloat64Tuple3D x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapNormal(IFloat64Tuple3D normal)
        {
            return MapVector(normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return GetInverseSimpleVectorRotation();
        }

        public abstract SimpleRotationLinearMap3D GetInverseSimpleVectorRotation();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<Float64Tuple3D> MapVectors(params IFloat64Tuple3D[] xList)
        {
            return xList.Select(MapVector).ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Float64Tuple3D> MapVectors(IEnumerable<IFloat64Tuple3D> xList)
        {
            return xList.Select(MapVector);
        }
    }
}