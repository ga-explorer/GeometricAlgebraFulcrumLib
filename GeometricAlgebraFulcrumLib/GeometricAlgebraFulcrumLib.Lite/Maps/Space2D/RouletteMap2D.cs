﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;

public sealed record RouletteMap2D :
    IAffineMap2D
{
    public Float64Vector2D FixedFrameOrigin { get; }

    public Float64Vector2D MovingFrameOrigin { get; }

    public Float64PlanarAngle RotationAngle { get; }

    public bool SwapsHandedness
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RouletteMap2D(Float64Vector2D fixedFrameOrigin, Float64Vector2D movingFrameOrigin, Float64PlanarAngle rotationAngle)
    {
        FixedFrameOrigin = fixedFrameOrigin;
        MovingFrameOrigin = movingFrameOrigin;
        RotationAngle = rotationAngle;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return new SquareMatrix3(GetArray2D());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        var (c1, c2) = 
            RotationAngle.RotateBasisFrame2D();

        var c3 = MapPoint(Float64Vector2D.Zero);

        return new double[,]
        {
            {c1.X, c2.X, c3.X},
            {c1.Y, c2.Y, c3.Y},
            {0d, 0d, 1d}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapPoint(IFloat64Vector2D point)
    {
        return FixedFrameOrigin + 
               RotationAngle.Rotate(point - MovingFrameOrigin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapVector(IFloat64Vector2D vector)
    {
        return RotationAngle.Rotate(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapNormal(IFloat64Vector2D normal)
    {
        return RotationAngle.Rotate(normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IAffineMap2D GetInverseAffineMap()
    {
        return new RouletteMap2D(
            MovingFrameOrigin,
            FixedFrameOrigin,
            -RotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return MovingFrameOrigin.IsValid() &&
               FixedFrameOrigin.IsValid() &&
               RotationAngle.IsValid();
    }
}