using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed record Float64RouletteAffineMap3D :
    IFloat64AffineMap3D
{
    public LinFloat64Vector3D FixedFrameOrigin { get; }

    public LinFloat64Vector3D MovingFrameOrigin { get; }

    public LinFloat64Quaternion RotationQuaternion { get; }

    public bool SwapsHandedness
        => false;


    public bool IsIdentity()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RouletteAffineMap3D(LinFloat64Vector3D fixedFrameOrigin, LinFloat64Vector3D movingFrameOrigin, LinFloat64Quaternion rotationQuaternion)
    {
        FixedFrameOrigin = fixedFrameOrigin;
        MovingFrameOrigin = movingFrameOrigin;
        RotationQuaternion = rotationQuaternion;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4(GetArray2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        var (c1, c2, c3) =
            RotationQuaternion.RotateBasisVectors();

        var c4 = MapPoint(LinFloat64Vector3D.Zero);

        return new Matrix4x4(
            (float)c1.X, (float)c2.X, (float)c3.X, (float)c4.X,
            (float)c1.Y, (float)c2.Y, (float)c3.Y, (float)c4.Y,
            (float)c1.Z, (float)c2.Z, (float)c3.Z, (float)c4.Z,
            0f, 0f, 0f, 1f
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        var (c1, c2, c3) =
            RotationQuaternion.RotateBasisVectors();

        var c4 = MapPoint(LinFloat64Vector3D.Zero);

        return new double[,]
        {
            {c1.X, c2.X, c3.X, c4.X},
            {c1.Y, c2.Y, c3.Y, c4.Y},
            {c1.Z, c2.Z, c3.Z, c4.Z},
            {0d, 0d, 0d, 1d}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return FixedFrameOrigin +
               RotationQuaternion.RotateVector(point - MovingFrameOrigin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return RotationQuaternion.RotateVector(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return RotationQuaternion.RotateVector(normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64RouletteAffineMap3D(
            MovingFrameOrigin,
            FixedFrameOrigin,
            RotationQuaternion.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return MovingFrameOrigin.IsValid() &&
               FixedFrameOrigin.IsValid() &&
               RotationQuaternion.IsValid();
    }
}