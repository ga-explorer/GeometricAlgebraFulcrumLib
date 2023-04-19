using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Bezier;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Circles;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Mapped;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    public static class ParametricCurveFactory3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricLine3D CreateLine3D(this IFloat64Tuple3D point, IFloat64Tuple3D vector)
        {
            return new ParametricLine3D(point, vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve0Degree3D CreateBezier3D(IFloat64Tuple3D point1)
        {
            return new BezierCurve0Degree3D(
                point1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve1Degree3D CreateBezier3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
        {
            return new BezierCurve1Degree3D(
                point1,
                point2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve2Degree3D CreateBezier3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2, IFloat64Tuple3D point3)
        {
            return new BezierCurve2Degree3D(
                point1,
                point2,
                point3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierCurve3Degree3D CreateBezier3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2, IFloat64Tuple3D point3, IFloat64Tuple3D point4)
        {
            return new BezierCurve3Degree3D(
                point1,
                point2,
                point3,
                point4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CatmullRomSpline2D CreateCatmullRomSpline2D(this IEnumerable<IFloat64Tuple2D> pointList, CatmullRomSplineType curveType, bool isClosed)
        {
            return new CatmullRomSpline2D(pointList, curveType, isClosed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CatmullRomSpline3D CreateCatmullRomSpline3D(this IEnumerable<IFloat64Tuple3D> pointList, CatmullRomSplineType curveType, bool isClosed)
        {
            return new CatmullRomSpline3D(pointList, curveType, isClosed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CatmullRomSpline4D CreateCatmullRomSpline4D(this IEnumerable<IFloat64Tuple4D> pointList, CatmullRomSplineType curveType, bool isClosed)
        {
            return new CatmullRomSpline4D(pointList, curveType, isClosed);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGraphicsParametricCircle3D CreateCircle3D(this Axis3D normalAxis, double radius)
        {
            return normalAxis switch
            {
                Axis3D.PositiveX => new ParametricCircleYz3D(radius, false),
                Axis3D.NegativeX => new ParametricCircleYz3D(radius, true),
                Axis3D.PositiveY => new ParametricCircleZx3D(radius, false),
                Axis3D.NegativeY => new ParametricCircleZx3D(radius, true),
                Axis3D.PositiveZ => new ParametricCircleXy3D(radius, false),
                _ => new ParametricCircleXy3D(radius, true)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricCircle3D CreateCircle3D(this IFloat64Tuple3D unitNormal, double radius)
        {
            return new ParametricCircle3D(Float64Tuple3D.Zero, unitNormal, radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricCircle3D CreateCircle3D(this IFloat64Tuple3D unitNormal, IFloat64Tuple3D center, double radius)
        {
            return new ParametricCircle3D(center, unitNormal, radius);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D CreateMathCurve2D(Func<double, double> mathFunction)
        {
            return new ComputedParametricCurve2D(
                x => new Float64Tuple2D(x, mathFunction(x))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SampledParametricCurve3D CreateSampledCurve3D(this IParametricCurve3D curve, SampledParametricCurveTreeOptions3D options)
        {
            var surfaceTree = new SampledParametricCurve3D(
                curve,
                BoundingBox1D.Create(0, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SampledParametricCurve3D CreateSampledCurve3D(this IParametricCurve3D surface, BoundingBox1D parameterValueRange, SampledParametricCurveTreeOptions3D options)
        {
            var surfaceTree = new SampledParametricCurve3D(
                surface,
                parameterValueRange
            );

            return surfaceTree.GenerateTree(options);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrMappedParametricCurve3D CreateMappedCurve3D(this IParametricCurve3D surface, IAffineMap3D map)
        {
            return new GrMappedParametricCurve3D(surface, map);
        }
    }
}