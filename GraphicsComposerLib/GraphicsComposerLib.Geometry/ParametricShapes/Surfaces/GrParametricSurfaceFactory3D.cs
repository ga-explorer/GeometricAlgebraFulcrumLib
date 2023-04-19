using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves;
using GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public static class GrParametricSurfaceFactory3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnYzSphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Sin(phi),
                radius * Math.Cos(theta) * Math.Cos(phi),
                radius * Math.Sin(theta) * Math.Cos(phi)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnZySphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Sin(phi),
                radius * Math.Cos(theta) * Math.Cos(phi),
                radius * Math.Sin(theta) * Math.Cos(phi)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnZxSphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Sin(theta) * Math.Cos(phi),
                radius * Math.Sin(phi),
                radius * Math.Cos(theta) * Math.Cos(phi)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnXzSphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Sin(theta) * Math.Cos(phi),
                radius * Math.Sin(phi),
                radius * Math.Cos(theta) * Math.Cos(phi)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnXySphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Cos(theta) * Math.Cos(phi),
                radius * Math.Sin(theta) * Math.Cos(phi),
                radius * Math.Sin(phi)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointOnYxSphereSurface(double radius, double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                radius * Math.Cos(theta) * Math.Cos(phi),
                radius * Math.Sin(theta) * Math.Cos(phi),
                radius * Math.Sin(phi)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnYzSphereSurface(double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Sin(phi),
                Math.Cos(theta) * Math.Cos(phi),
                Math.Sin(theta) * Math.Cos(phi)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnZySphereSurface(double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Sin(phi),
                Math.Cos(theta) * Math.Cos(phi),
                Math.Sin(theta) * Math.Cos(phi)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnZxSphereSurface(double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Sin(theta) * Math.Cos(phi),
                Math.Sin(phi),
                Math.Cos(theta) * Math.Cos(phi)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnXzSphereSurface(double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Sin(theta) * Math.Cos(phi),
                Math.Sin(phi),
                Math.Cos(theta) * Math.Cos(phi)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnXySphereSurface(double t1, double t2)
        {
            var theta = t1 * 2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Cos(theta) * Math.Cos(phi),
                Math.Sin(theta) * Math.Cos(phi),
                Math.Sin(phi)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormalOnYxSphereSurface(double t1, double t2)
        {
            var theta = t1 * -2 * Math.PI;
            var phi = (t2 - 0.5d) * Math.PI;

            return new Float64Tuple3D(
                Math.Cos(theta) * Math.Cos(phi),
                Math.Sin(theta) * Math.Cos(phi),
                Math.Sin(phi)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrComputedParametricSurface3D CreateSphere3D(this AxisPair3D sliceAxisPair, double radius)
        {
            return sliceAxisPair switch
            {
                AxisPair3D.Xy => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnXySphereSurface(radius, t1, t2),
                        GetNormalOnXySphereSurface
                    ),

                AxisPair3D.Yx => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnYxSphereSurface(radius, t1, t2),
                        GetNormalOnYxSphereSurface
                    ),

                AxisPair3D.Yz => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnYzSphereSurface(radius, t1, t2),
                        GetNormalOnYzSphereSurface
                    ),

                AxisPair3D.Zy => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnZySphereSurface(radius, t1, t2),
                        GetNormalOnZySphereSurface
                    ),

                AxisPair3D.Zx => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnZxSphereSurface(radius, t1, t2),
                        GetNormalOnZxSphereSurface
                    ),

                _ => 
                    new GrComputedParametricSurface3D(
                        (t1, t2) => GetPointOnXzSphereSurface(radius, t1, t2),
                        GetNormalOnXzSphereSurface
                    ),
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrComputedParametricSurface3D CreateSphere3D(this Axis3D sliceAxisNormal, double radius)
        {
            return sliceAxisNormal
                .GetAxisPair3D()
                .CreateSphere3D(radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrComputedParametricSurface3D CreateSphere3D(this IFloat64Tuple3D sliceAxisUnitNormal, double radius)
        {
            return sliceAxisUnitNormal
                .SelectNearestAxis()
                .GetAxisPair3D()
                .CreateSphere3D(radius);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrCurveTubeParametricSurface3D CreateTube3D(this IParametricCurve3D curve, double radius)
        {
            return new GrCurveTubeParametricSurface3D(curve, radius);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrComputedParametricSurface3D CreateMathSurface3D(Func<double, double, double> mathFunction)
        {
            return new GrComputedParametricSurface3D(
                (x, y) => new Float64Tuple3D(x, y, mathFunction(x, y))
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricSurfaceTree3D CreateSampledSurface3D(this IGraphicsParametricSurface3D surface, GrParametricSurfaceTreeOptions3D options)
        {
            var surfaceTree = new GrParametricSurfaceTree3D(
                surface,
                BoundingBox2D.Create(0, 0, 1, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricSurfaceTree3D CreateSampledSurface3D(this IGraphicsParametricSurface3D surface, BoundingBox2D parameterValueRange, GrParametricSurfaceTreeOptions3D options)
        {
            var surfaceTree = new GrParametricSurfaceTree3D(
                surface,
                parameterValueRange
            );

            return surfaceTree.GenerateTree(options);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrMappedParametricSurface3D CreateMappedSurface3D(this IGraphicsParametricSurface3D surface, IAffineMap3D map)
        {
            return new GrMappedParametricSurface3D(surface, map);
        }
    }
}