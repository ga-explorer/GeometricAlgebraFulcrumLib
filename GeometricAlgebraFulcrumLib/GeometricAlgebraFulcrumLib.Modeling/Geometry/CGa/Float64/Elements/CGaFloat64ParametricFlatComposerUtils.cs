using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricFlatComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatPoint(
                positionCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatPoint(
                positionCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatLineFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricCurve2D positionCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatLine(
                positionCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatPlaneFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                positionCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                positionCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            positionCurve.ParameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineFlatPlane(
                positionCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

}