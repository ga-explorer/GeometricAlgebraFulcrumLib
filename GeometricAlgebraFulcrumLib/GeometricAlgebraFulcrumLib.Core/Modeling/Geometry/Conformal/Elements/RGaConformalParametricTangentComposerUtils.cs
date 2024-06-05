using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public static class RGaConformalParametricTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPoint(this RGaConformalSpace conformalSpace, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineTangentPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPoint(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentPoint(
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentLine(this RGaConformalSpace conformalSpace, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineTangentLine(
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentLine(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentLine(
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineTangentLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlane(this RGaConformalSpace conformalSpace, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlane(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromNormal(this RGaConformalSpace conformalSpace, IParametricCurve3D positionNormalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionNormalCurve.ParameterRange,
            t => conformalSpace.DefineTangentPlane(
                positionNormalCurve.GetPoint(t),
                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromNormal(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionNormalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentPlane(
                positionNormalCurve.GetPoint(t),
                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromNormal(this RGaConformalSpace conformalSpace, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromNormal(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentPlane(
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineTangentPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

}