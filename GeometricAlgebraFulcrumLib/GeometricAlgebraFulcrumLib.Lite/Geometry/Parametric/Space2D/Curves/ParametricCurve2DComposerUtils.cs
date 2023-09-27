using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Bezier;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Circles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves
{
    public static class ParametricCurve2DComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricScalar curve, Func<double, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetValue(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricAngle curve, Func<Float64PlanarAngle, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetAngle(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricCurve2D curve, Func<Float64Vector2D, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetPoint(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricCurve3D curve, Func<Float64Vector3D, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetPoint(t))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricBivector3D curve, Func<Float64Bivector3D, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetBivector(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricCurve4D curve, Func<Float64Vector4D, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetPoint(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D ToParametricCurve2D(this IParametricQuaternion curve, Func<Float64Quaternion, Float64Vector2D> vectorMapping)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => vectorMapping(curve.GetQuaternion(t))
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricLine2D CreateLine2D(this IFloat64Vector2D point, IFloat64Vector2D vector)
        {
            return new ParametricLine2D(point, vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve0Degree2D CreateBezier2D(IFloat64Vector2D point1)
        {
            return new BezierCurve0Degree2D(
                point1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve1Degree2D CreateBezier2D(IFloat64Vector2D point1, IFloat64Vector2D point2)
        {
            return new BezierCurve1Degree2D(
                point1,
                point2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve2Degree2D CreateBezier2D(IFloat64Vector2D point1, IFloat64Vector2D point2, IFloat64Vector2D point3)
        {
            return new BezierCurve2Degree2D(
                point1,
                point2,
                point3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve3Degree2D CreateBezier2D(IFloat64Vector2D point1, IFloat64Vector2D point2, IFloat64Vector2D point3, IFloat64Vector2D point4)
        {
            return new BezierCurve3Degree2D(
                point1,
                point2,
                point3,
                point4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CatmullRomSpline2D CreateCatmullRomSpline2D(this IEnumerable<IFloat64Vector2D> pointList, CatmullRomSplineType curveType, bool isClosed)
        {
            return new CatmullRomSpline2D(pointList, curveType, isClosed);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricCircle2D CreateCircle2D(this IFloat64Vector2D center, double radius)
        {
            return new ParametricCircle2D(center, radius);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D CreateMathCurve2D(Func<double, double> mathFunction)
        {
            return ComputedParametricCurve2D.Create(x => Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)mathFunction(x)));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AdaptiveCurve2D CreateAdaptiveCurve2D(this IParametricCurve2D curve, AdaptiveCurveSamplingOptions2D options)
        {
            var surfaceTree = new AdaptiveCurve2D(
                curve,
                Float64ScalarRange.Create(0, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AdaptiveCurve2D CreateAdaptiveCurve2D(this IParametricCurve2D surface, Float64ScalarRange parameterValueRange, AdaptiveCurveSamplingOptions2D options)
        {
            var surfaceTree = new AdaptiveCurve2D(
                surface,
                parameterValueRange
            );

            return surfaceTree.GenerateTree(options);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrMappedParametricCurve2D CreateMappedCurve2D(this IParametricCurve2D surface, IAffineMap2D map)
        {
            return new GrMappedParametricCurve2D(surface, map);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricScalar GetDistanceCurve(this IParametricCurve2D curve1, IFloat64Vector2D curve2)
        {
            return ComputedParametricScalar.Create(
                curve1.ParameterRange,
                t => curve1.GetPoint(t).GetDistanceToPoint(curve2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricScalar GetDistanceCurve(this IParametricCurve2D curve1, IParametricCurve2D curve2)
        {
            return ComputedParametricScalar.Create(
                curve1.ParameterRange,
                t => curve1.GetPoint(t).GetDistanceToPoint(curve2.GetPoint(t))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetOffsetCurve(this IParametricCurve2D curve, double offsetVectorX, double offsetVectorY)
        {
            var offsetVector = Float64Vector2D.Create(
                offsetVectorX, 
                offsetVectorY
            );

            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => curve.GetPoint(t) + offsetVector,
                curve.GetDerivative1Point
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetOffsetCurve(this IParametricCurve2D curve, IFloat64Vector2D offsetVector)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => curve.GetPoint(t) + offsetVector,
                curve.GetDerivative1Point
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetOffsetCurve(this IParametricCurve2D curve, IParametricCurve2D offsetVectorCurve)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => curve.GetPoint(t) + offsetVectorCurve.GetPoint(t),
                t => curve.GetDerivative1Point(t) + offsetVectorCurve.GetDerivative1Point(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetTangentCurve(this IParametricCurve2D curve)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                curve.GetDerivative1Point
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetTangentCurve(this IParametricC2Curve2D curve)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                curve.GetDerivative1Point,
                curve.GetDerivative2Point
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetLineNormalCurve(this IParametricCurve2D curve1, IParametricCurve2D curve2)
        {
            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t =>
                {
                    var p1 = curve1.GetPoint(t);
                    var p2 = curve2.GetPoint(t);
                    
                    return (p2 - p1).GetUnitNormal();
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetMidPointCurve(this IParametricCurve2D curve1, IFloat64Vector2D curve2)
        {
            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t => 0.5 * (curve1.GetPoint(t) + curve2),
                t => 0.5 * curve1.GetDerivative1Point(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetMidPointCurve(this IParametricCurve2D curve1, IParametricCurve2D curve2)
        {
            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t => 0.5 * (curve1.GetPoint(t) + curve2.GetPoint(t)),
                t => 0.5 * (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetMedianPointCurve(this IParametricCurve2D curve1, IParametricCurve2D curve2, IFloat64Vector2D curve3)
        {
            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t => (curve1.GetPoint(t) + curve2.GetPoint(t) + curve3) / 3d,
                t => (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t)) / 3
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetMedianPointCurve(this IParametricCurve2D curve1, IFloat64Vector2D curve2, IFloat64Vector2D curve3)
        {
            var point = curve2.Add(curve3);

            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t => (curve1.GetPoint(t) + point) / 3d,
                t => curve1.GetDerivative1Point(t) / 3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D GetMedianPointCurve(this IParametricCurve2D curve1, IParametricCurve2D curve2, IParametricCurve2D curve3)
        {
            return ComputedParametricCurve2D.Create(
                curve1.ParameterRange,
                t => (curve1.GetPoint(t) + curve2.GetPoint(t) + curve3.GetPoint(t)) / 3d,
                t => (curve1.GetDerivative1Point(t) + curve2.GetDerivative1Point(t) + curve3.GetDerivative1Point(t)) / 3
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IParametricCurve2D GetNormalCurve(this IParametricCurve2D curve)
        {
            return ComputedParametricCurve2D.Create(
                curve.ParameterRange,
                t => curve.GetPoint(t).DirectionToNormal2D()
            );
        }
    }
}