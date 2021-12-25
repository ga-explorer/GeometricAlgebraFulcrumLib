using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.Borders.Space1D.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public static class GrParametricCurveFactory3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricLine3D CreateLine3D(this ITuple3D point, ITuple3D vector)
        {
            return new GrParametricLine3D(point, vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrBezierCurve0Degree3D CreateBezier3D(ITuple3D point1)
        {
            return new GrBezierCurve0Degree3D(
                point1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrBezierCurve1Degree3D CreateBezier3D(ITuple3D point1, ITuple3D point2)
        {
            return new GrBezierCurve1Degree3D(
                point1,
                point2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrBezierCurve2Degree3D CreateBezier3D(ITuple3D point1, ITuple3D point2, ITuple3D point3)
        {
            return new GrBezierCurve2Degree3D(
                point1,
                point2,
                point3
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrBezierCurve3Degree3D CreateBezier3D(ITuple3D point1, ITuple3D point2, ITuple3D point3, ITuple3D point4)
        {
            return new GrBezierCurve3Degree3D(
                point1,
                point2,
                point3,
                point4
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGraphicsParametricCircle3D CreateCircle3D(this Axis3D normalAxis, double radius)
        {
            return normalAxis switch
            {
                Axis3D.PositiveX => new GrParametricCircleYz3D(radius, false),
                Axis3D.NegativeX => new GrParametricCircleYz3D(radius, true),
                Axis3D.PositiveY => new GrParametricCircleZx3D(radius, false),
                Axis3D.NegativeY => new GrParametricCircleZx3D(radius, true),
                Axis3D.PositiveZ => new GrParametricCircleXy3D(radius, false),
                _ => new GrParametricCircleXy3D(radius, true)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricCircle3D CreateCircle3D(this ITuple3D unitNormal, double radius)
        {
            return new GrParametricCircle3D(Tuple3D.Zero, unitNormal, radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricCircle3D CreateCircle3D(this ITuple3D unitNormal, ITuple3D center, double radius)
        {
            return new GrParametricCircle3D(center, unitNormal, radius);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrComputedParametricCurve2D CreateMathCurve2D(Func<double, double> mathFunction)
        {
            return new GrComputedParametricCurve2D(
                x => new Tuple2D(x, mathFunction(x))
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricCurveTree3D CreateSampledCurve3D(this IGraphicsParametricCurve3D curve, GrParametricCurveTreeOptions3D options)
        {
            var surfaceTree = new GrParametricCurveTree3D(
                curve,
                BoundingBox1D.Create(0, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricCurveTree3D CreateSampledCurve3D(this IGraphicsParametricCurve3D surface, BoundingBox1D parameterValueRange, GrParametricCurveTreeOptions3D options)
        {
            var surfaceTree = new GrParametricCurveTree3D(
                surface,
                parameterValueRange
            );

            return surfaceTree.GenerateTree(options);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrMappedParametricCurve3D CreateMappedCurve3D(this IGraphicsParametricCurve3D surface, IAffineMap3D map)
        {
            return new GrMappedParametricCurve3D(surface, map);
        }
    }
}