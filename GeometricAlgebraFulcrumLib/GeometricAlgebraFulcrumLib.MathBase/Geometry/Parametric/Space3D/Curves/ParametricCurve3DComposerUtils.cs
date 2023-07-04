using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Bezier;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Circles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space4D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves
{
    public static class ParametricCurve3DComposerUtils
    {
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
                Float64Range1D.Create(0, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AdaptiveCurve3D CreateAdaptiveCurve3D(this IParametricCurve3D curve, Float64Range1D parameterValueRange, AdaptiveCurveSamplingOptions3D options)
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
    }
}