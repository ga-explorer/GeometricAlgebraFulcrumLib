using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricFlatComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatPoint(
                positionCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatPoint(
                positionCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path2D positionCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D positionCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                positionCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                positionCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D positionCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.TimeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

}