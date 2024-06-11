using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPoint(
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentLine(
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentLine(
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionNormalCurve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                positionNormalCurve.GetPoint(t),
                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionNormalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                positionNormalCurve.GetPoint(t),
                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

}