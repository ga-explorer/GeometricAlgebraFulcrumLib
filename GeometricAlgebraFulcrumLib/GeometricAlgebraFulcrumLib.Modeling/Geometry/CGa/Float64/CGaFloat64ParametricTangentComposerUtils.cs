using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPoint(Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => TangentPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPoint(Float64ScalarRange parameterRange, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentLine(Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => TangentLine(
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentLine(Float64ScalarRange parameterRange, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentLine(
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentLineFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => TangentLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentLineFromPoints(Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentLineFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlane(Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => TangentPlane(
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlane(Float64ScalarRange parameterRange, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentPlane(
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromNormal(Float64Path3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            positionNormalCurve.TimeRange,
            t => TangentPlane(
                positionNormalCurve.GetValue(t),
                positionNormalCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromNormal(Float64ScalarRange parameterRange, Float64Path3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentPlane(
                positionNormalCurve.GetValue(t),
                positionNormalCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromNormal(Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => TangentPlane(
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromNormal(Float64ScalarRange parameterRange, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentPlane(
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => TangentPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement TangentPlaneFromPoints(Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => TangentPlaneFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

}