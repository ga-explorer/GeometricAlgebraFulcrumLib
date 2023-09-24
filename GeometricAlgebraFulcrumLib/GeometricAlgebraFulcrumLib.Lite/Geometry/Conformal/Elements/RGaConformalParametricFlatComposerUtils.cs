using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public static class RGaConformalParametricFlatComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPoint(this RGaConformalSpace conformalSpace, IParametricCurve3D positionCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatPoint(
                positionCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPoint(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatPoint(
                positionCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLineFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve2D point1Curve, IParametricCurve2D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLineFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLineFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLine(this RGaConformalSpace conformalSpace, IParametricCurve2D positionCurve, IParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLine(this RGaConformalSpace conformalSpace, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatLine(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, IParametricCurve3D positionCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                positionCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                positionCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            positionCurve.ParameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineFlatPlane(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

}