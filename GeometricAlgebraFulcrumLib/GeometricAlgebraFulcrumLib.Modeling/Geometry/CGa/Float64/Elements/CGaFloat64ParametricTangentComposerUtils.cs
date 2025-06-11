using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineTangentPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineTangentLine(
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentLine(
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineTangentLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionNormalCurve.TimeRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                positionNormalCurve.GetValue(t),
                positionNormalCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                positionNormalCurve.GetValue(t),
                positionNormalCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

}