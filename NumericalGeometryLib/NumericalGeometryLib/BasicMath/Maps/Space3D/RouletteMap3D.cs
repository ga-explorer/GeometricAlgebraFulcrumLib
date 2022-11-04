using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D;

public sealed record RouletteMap3D :
    IAffineMap3D
{
    public Tuple3D FixedFrameOrigin { get; }

    public Tuple3D MovingFrameOrigin { get; }

    public Tuple4D RotationQuaternion { get; }

    public bool SwapsHandedness
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RouletteMap3D(Tuple3D fixedFrameOrigin, Tuple3D movingFrameOrigin, Tuple4D rotationQuaternion)
    {
        FixedFrameOrigin = fixedFrameOrigin;
        MovingFrameOrigin = movingFrameOrigin;
        RotationQuaternion = rotationQuaternion;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 ToSquareMatrix4()
    {
        return new SquareMatrix4(ToArray2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 ToMatrix4x4()
    {
        var (c1, c2, c3) = 
            RotationQuaternion.QuaternionRotateBasisFrame();

        var c4 = MapPoint(Tuple3D.Zero);

        return new Matrix4x4(
            (float) c1.X, (float) c2.X, (float) c3.X, (float) c4.X,
            (float) c1.Y, (float) c2.Y, (float) c3.Y, (float) c4.Y,
            (float) c1.Z, (float) c2.Z, (float) c3.Z, (float) c4.Z,
            0f, 0f, 0f, 1f
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] ToArray2D()
    {
        var (c1, c2, c3) = 
            RotationQuaternion.QuaternionRotateBasisFrame();

        var c4 = MapPoint(Tuple3D.Zero);

        return new[,]
        {
            {c1.X, c2.X, c3.X, c4.X},
            {c1.Y, c2.Y, c3.Y, c4.Y},
            {c1.Z, c2.Z, c3.Z, c4.Z},
            {0d, 0d, 0d, 1d}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D MapPoint(ITuple3D point)
    {
        return FixedFrameOrigin + 
               RotationQuaternion.QuaternionRotate(point - MovingFrameOrigin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D MapVector(ITuple3D vector)
    {
        return RotationQuaternion.QuaternionRotate(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D MapNormal(ITuple3D normal)
    {
        return RotationQuaternion.QuaternionRotate(normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IAffineMap3D InverseMap()
    {
        return new RouletteMap3D(
            MovingFrameOrigin,
            FixedFrameOrigin,
            RotationQuaternion.QuaternionInverse()
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