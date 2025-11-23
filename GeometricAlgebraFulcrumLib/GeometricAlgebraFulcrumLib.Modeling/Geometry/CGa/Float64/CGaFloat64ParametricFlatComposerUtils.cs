using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPoint(Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatPoint(
                positionCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPoint(Float64ScalarRange timeRange, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatPoint(
                positionCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLineFromPoints(Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => FlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLineFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => FlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLineFromPoints(Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLine(Float64Path2D positionCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatLine(
                positionCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLine(Float64Path3D positionCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatLine(
                positionCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatLine(Float64ScalarRange timeRange, Float64Path3D positionCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatLine(
                positionCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlaneFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => FlatPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlaneFromPoints(Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                positionCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64ScalarRange timeRange, Float64Path3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                positionCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64Path3D positionCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64ScalarRange timeRange, Float64Path3D positionCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64Path3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionCurve.TimeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement FlatPlane(Float64ScalarRange timeRange, Float64Path3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => FlatPlane(
                positionCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

}