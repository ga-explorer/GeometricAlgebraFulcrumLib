using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Bezier;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Circles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    public static class ParametricCurveFactory2D
    {
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
                Float64Range1D.Create(0, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AdaptiveCurve2D CreateAdaptiveCurve2D(this IParametricCurve2D surface, Float64Range1D parameterValueRange, AdaptiveCurveSamplingOptions2D options)
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
    }
}