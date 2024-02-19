using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Bezier;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Circles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;

public static class ParametricCurve3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricScalar curve, Func<double, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricAngle curve, Func<Float64PlanarAngle, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricCurve2D curve, Func<Float64Vector2D, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToXyParametricCurve3D(this IParametricCurve2D curve)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetPoint(t).ToXyVector3D()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricCurve3D curve, Func<Float64Vector3D, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricBivector2D curve, Func<Float64Bivector2D, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetBivector(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricBivector3D curve, Func<Float64Bivector3D, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetBivector(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricCurve4D curve, Func<Float64Vector4D, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D ToParametricCurve3D(this IParametricQuaternion curve, Func<Float64Quaternion, Float64Vector3D> vectorMapping)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D XyDirectionToNormal3D(this IParametricBivector2D curve, Float64Vector3D? zeroNormal = null)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetBivector(t).ToXyBivector3D().DirectionToNormal3D(zeroNormal)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D XyNormalToDirection3D(this IParametricBivector2D curve, Float64Vector3D? zeroNormal = null)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetBivector(t).ToXyBivector3D().NormalToDirection3D(zeroNormal)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParametricLine3D CreateLine3D(this IFloat64Vector3D point, IFloat64Vector3D vector)
    {
        return new ParametricLine3D(point, vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BezierCurve0Degree3D CreateBezier3D(IFloat64Vector3D point1)
    {
        return new BezierCurve0Degree3D(
            point1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BezierCurve1Degree3D CreateBezier3D(IFloat64Vector3D point1, IFloat64Vector3D point2)
    {
        return new BezierCurve1Degree3D(
            point1,
            point2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BezierCurve2Degree3D CreateBezier3D(IFloat64Vector3D point1, IFloat64Vector3D point2, IFloat64Vector3D point3)
    {
        return new BezierCurve2Degree3D(
            point1,
            point2,
            point3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BezierCurve3Degree3D CreateBezier3D(IFloat64Vector3D point1, IFloat64Vector3D point2, IFloat64Vector3D point3, IFloat64Vector3D point4)
    {
        return new BezierCurve3Degree3D(
            point1,
            point2,
            point3,
            point4
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CatmullRomSpline3D CreateCatmullRomSpline3D(this IEnumerable<IFloat64Vector3D> pointList, CatmullRomSplineType curveType, bool isClosed)
    {
        return new CatmullRomSpline3D(pointList, curveType, isClosed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CatmullRomSpline4D CreateCatmullRomSpline4D(this IEnumerable<IFloat64Vector4D> pointList, CatmullRomSplineType curveType, bool isClosed)
    {
        return new CatmullRomSpline4D(pointList, curveType, isClosed);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IGraphicsParametricCircle3D CreateCircle3D(this LinUnitBasisVector3D normalAxis, double radius, int rotationCount = 1)
    {
        return normalAxis switch
        {
            LinUnitBasisVector3D.PositiveX => new ParametricCircleYz3D(radius, rotationCount),
            LinUnitBasisVector3D.NegativeX => new ParametricCircleYz3D(radius, -rotationCount),
            LinUnitBasisVector3D.PositiveY => new ParametricCircleZx3D(radius, rotationCount),
            LinUnitBasisVector3D.NegativeY => new ParametricCircleZx3D(radius, -rotationCount),
            LinUnitBasisVector3D.PositiveZ => new ParametricCircleXy3D(radius, rotationCount),
            _ => new ParametricCircleXy3D(radius, -rotationCount)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParametricCircle3D CreateCircle3D(this IFloat64Vector3D unitNormal, double radius, int rotationCount = 1)
    {
        return new ParametricCircle3D(Float64Vector3D.Zero, unitNormal, radius, rotationCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParametricCircle3D CreateCircle3D(this IFloat64Vector3D unitNormal, IFloat64Vector3D center, double radius, int rotationCount = 1)
    {
        return new ParametricCircle3D(center, unitNormal, radius, rotationCount);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D CreateMathCurve2D(Func<double, double> mathFunction)
    {
        return ComputedParametricCurve2D.Create(x => Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)mathFunction(x)));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AdaptiveCurve3D CreateAdaptiveCurve3D(this IParametricCurve3D curve, AdaptiveCurveSamplingOptions3D options)
    {
        var surfaceTree = new AdaptiveCurve3D(
            curve,
            Float64ScalarRange.Create(0, 1)
        );

        return surfaceTree.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AdaptiveCurve3D CreateAdaptiveCurve3D(this IParametricCurve3D curve, Float64ScalarRange parameterValueRange, AdaptiveCurveSamplingOptions3D options)
    {
        var surfaceTree = new AdaptiveCurve3D(
            curve,
            parameterValueRange
        );

        return surfaceTree.GenerateTree(options);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrMappedParametricCurve3D CreateMappedCurve3D(this IParametricCurve3D curve, IAffineMap3D map)
    {
        return new GrMappedParametricCurve3D(curve, map);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar GetDistanceCurve(this IParametricCurve3D curve1, IFloat64Vector3D curve2)
    {
        return ComputedParametricScalar.Create(
            curve1.ParameterRange,
            t => curve1.GetPoint(t).GetDistanceToPoint(curve2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar GetDistanceCurve(this IParametricCurve3D curve1, IParametricCurve3D curve2)
    {
        return ComputedParametricScalar.Create(
            curve1.ParameterRange,
            t => curve1.GetPoint(t).GetDistanceToPoint(curve2.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetOffsetCurve(this IParametricCurve3D curve, double offsetVectorX, double offsetVectorY, double offsetVectorZ)
    {
        var offsetVector = Float64Vector3D.Create(
            offsetVectorX, 
            offsetVectorY, 
            offsetVectorZ
        );

        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetPoint(t) + offsetVector,
            curve.GetDerivative1Point
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetOffsetCurve(this IParametricCurve3D curve, IFloat64Vector3D offsetVector)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetPoint(t) + offsetVector,
            curve.GetDerivative1Point
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetOffsetCurve(this IParametricCurve3D curve, IParametricCurve3D offsetVectorCurve)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            t => curve.GetPoint(t) + offsetVectorCurve.GetPoint(t),
            t => curve.GetDerivative1Point(t) + offsetVectorCurve.GetDerivative1Point(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetTangentCurve(this IParametricCurve3D curve)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            curve.GetDerivative1Point
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetTangentCurve(this IParametricC2Curve3D curve)
    {
        return ComputedParametricCurve3D.Create(
            curve.ParameterRange,
            curve.GetDerivative1Point,
            curve.GetDerivative2Point
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetPlaneNormalCurve(this IParametricCurve3D curve1, IParametricCurve3D curve2, IParametricCurve3D curve3)
    {
        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t =>
            {
                var p1 = curve1.GetPoint(t);
                var p2 = curve2.GetPoint(t);
                var p3 = curve3.GetPoint(t);

                return (p2 - p1).VectorUnitCross(p3 - p2);
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetMidPointCurve(this IParametricCurve3D curve1, IFloat64Vector3D curve2)
    {
        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t => 0.5 * (curve1.GetPoint(t) + curve2),
            t => 0.5 * curve1.GetDerivative1Point(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetMidPointCurve(this IParametricCurve3D curve1, IParametricCurve3D curve2)
    {
        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t => 0.5 * (curve1.GetPoint(t) + curve2.GetPoint(t)),
            t => 0.5 * (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetMedianPointCurve(this IParametricCurve3D curve1, IParametricCurve3D curve2, IFloat64Vector3D curve3)
    {
        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t => (curve1.GetPoint(t) + curve2.GetPoint(t) + curve3) / 3d,
            t => (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t)) / 3
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetMedianPointCurve(this IParametricCurve3D curve1, IFloat64Vector3D curve2, IFloat64Vector3D curve3)
    {
        var point = curve2.Add(curve3);

        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t => (curve1.GetPoint(t) + point) / 3d,
            t => curve1.GetDerivative1Point(t) / 3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D GetMedianPointCurve(this IParametricCurve3D curve1, IParametricCurve3D curve2, IParametricCurve3D curve3)
    {
        return ComputedParametricCurve3D.Create(
            curve1.ParameterRange,
            t => (curve1.GetPoint(t) + curve2.GetPoint(t) + curve3.GetPoint(t)) / 3d,
            t => (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t) + curve3.GetDerivative1Point(t)) / 3
        );
    }

}