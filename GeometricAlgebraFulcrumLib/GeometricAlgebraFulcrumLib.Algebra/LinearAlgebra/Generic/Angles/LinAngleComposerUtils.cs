using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;

public static class LinAngleComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalar<T> angleCos, IScalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> angleCos, Scalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalarProcessor<T> scalarProcessor, Scalar<T> angleCos, IScalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalarProcessor<T> scalarProcessor, Scalar<T> angleCos, Scalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> angleCos, IScalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T angleCos, T angleSin)
    {
        return LinPolarAngle<T>.CreateFromCosSin(
            scalarProcessor.ScalarFromValue(angleCos), 
            scalarProcessor.ScalarFromValue(angleSin)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromRadians<T>(this IScalar<T> angleInRadians)
    {
        return LinPolarAngle<T>.CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(angleInRadians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromDegrees<T>(this IScalar<T> angleInDegrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreatePolarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, T angleInDegrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromValue(angleInDegrees)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDirectedAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, double radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDirectedAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, T radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, LinAngle<T> angle)
    {
        return scalarProcessor.CreateDirectedAngleFromRadians(angle.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360 => angleInDegrees % 720 + 360,
            > 360 => angleInDegrees % 360,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, long angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360L => angleInDegrees % 720L + 360L,
            > 360L => angleInDegrees % 360L,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, float angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360f => angleInDegrees % 720f + 360f,
            > 360f => angleInDegrees % 360f,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360d => angleInDegrees % 720d + 360d,
            > 360d => angleInDegrees % 360d,
            _ => angleInDegrees
        };

        return scalarProcessor.CreateAngle(
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> angleInDegrees)
    {
        return scalarProcessor.CreateAngle(
            angleInDegrees.DegreesToRadians()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
    {
        return scalarProcessor.CreateDirectedAngleFromRadians(
            angleInRadians
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, ILinFloat64Vector2D v1, ILinFloat64Vector2D v2)
    {
        var angleInRadians = v1.VectorESp(v2).Clamp(-1, 1).ArcCos();

        return scalarProcessor.CreateAngle(
            scalarProcessor.ScalarFromNumber(angleInRadians.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, ILinVector3D<T> v1, ILinVector3D<T> v2)
    {
        var angleInRadians = v1.VectorESp(v2).ArcCos().ScalarValue;

        return scalarProcessor.CreateDirectedAngleFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, ILinVector4D<T> v1, ILinVector4D<T> v2)
    {
        var angleInRadians = v1.ESp(v2).ArcCos().ScalarValue;

        return scalarProcessor.CreateDirectedAngleFromRadians(angleInRadians);
    }

}