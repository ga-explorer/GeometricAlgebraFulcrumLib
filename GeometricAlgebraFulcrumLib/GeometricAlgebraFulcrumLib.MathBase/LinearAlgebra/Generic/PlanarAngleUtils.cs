using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

public static class PlanarAngleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, double radians)
    {
        return new PlanarAngle<T>(
            scalarProcessor, 
            scalarProcessor.GetScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, T radians)
    {
        return new PlanarAngle<T>(scalarProcessor, radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngle<T>(this IScalarProcessor<T> scalarProcessor, Float64PlanarAngle angle)
    {
        return scalarProcessor.CreateAngle(angle.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360 => (angleInDegrees % 720) + 360,
            > 360 => angleInDegrees % 360,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, long angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360L => (angleInDegrees % 720L) + 360L,
            > 360L => angleInDegrees % 360L,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, float angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360f => (angleInDegrees % 720f) + 360f,
            > 360f => angleInDegrees % 360f,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360d => (angleInDegrees % 720d) + 360d,
            > 360d => angleInDegrees % 360d,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, T angleInDegrees)
    {
        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
    {
        return scalarProcessor.CreateAngle(
            angleInRadians
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        var angleInRadians = v1.ESp(v2).Clamp(-1, 1).ArcCos();

        return scalarProcessor.CreateAngle(
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var angleInRadians = Math.Acos(v1.ESp(v2));

        return scalarProcessor.CreateAngle(
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
    {
        var angleInRadians = v1.ESp(v2).Clamp(-1, 1).ArcCos();

        return scalarProcessor.CreateAngle(
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }
        
}