using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces.Sampled;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces;

public static class GrParametricSurfaceFactory3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnYzSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnZySphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnZxSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnXzSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnXySphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointOnYxSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnYzSphereSurface(double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnZySphereSurface(double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnZxSphereSurface(double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnXzSphereSurface(double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnXySphereSurface(double t1, double t2)
    {
        var theta = t1 * 2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormalOnYxSphereSurface(double t1, double t2)
    {
        var theta = t1 * -2 * Math.PI;
        var phi = (t2 - 0.5d) * Math.PI;

        return Float64Vector3D.Create(Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateSphere3D(this LinBasisVectorPair3D sliceAxisPair, double radius)
    {
        return sliceAxisPair switch
        {
            LinBasisVectorPair3D.Xy => 
                new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnXySphereSurface(radius, t1, t2),
                    GetNormalOnXySphereSurface
                ),

            LinBasisVectorPair3D.Yx => 
                new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnYxSphereSurface(radius, t1, t2),
                    GetNormalOnYxSphereSurface
                ),

            LinBasisVectorPair3D.Yz => 
                new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnYzSphereSurface(radius, t1, t2),
                    GetNormalOnYzSphereSurface
                ),

            LinBasisVectorPair3D.Zy => 
                new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnZySphereSurface(radius, t1, t2),
                    GetNormalOnZySphereSurface
                ),

            LinBasisVectorPair3D.Zx => 
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
    public static GrComputedParametricSurface3D CreateSphere3D(this LinUnitBasisVector3D sliceAxisNormal, double radius)
    {
        return sliceAxisNormal
            .GetAxisPair3D()
            .CreateSphere3D(radius);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateSphere3D(this IFloat64Vector3D sliceAxisUnitNormal, double radius)
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
            (x, y) => Float64Vector3D.Create(x, y, mathFunction(x, y))
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