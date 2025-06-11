using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public static class GrParametricSurfaceFactory3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnYzSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnZySphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnZxSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnXzSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi),
            radius * Math.Cos(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnXySphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnYxSphereSurface(double radius, double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            radius * Math.Cos(theta) * Math.Cos(phi),
            radius * Math.Sin(theta) * Math.Cos(phi),
            radius * Math.Sin(phi)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnYzSphereSurface(double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnZySphereSurface(double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnZxSphereSurface(double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnXzSphereSurface(double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi),
            Math.Cos(theta) * Math.Cos(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnXySphereSurface(double t1, double t2)
    {
        var theta = t1 * Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetNormalOnYxSphereSurface(double t1, double t2)
    {
        var theta = t1 * -Math.Tau;
        var phi = (t2 - 0.5d) * Math.PI;

        return LinFloat64Vector3D.Create(
            Math.Cos(theta) * Math.Cos(phi),
            Math.Sin(theta) * Math.Cos(phi),
            Math.Sin(phi)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateSphere3D(this LinBasisVectorPair3D sliceAxisPair, double radius)
    {
        if (sliceAxisPair.Item1 == LinBasisVector.Px)
        {
            if (sliceAxisPair.Item2 == LinBasisVector.Py)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnXySphereSurface(radius, t1, t2),
                    GetNormalOnXySphereSurface
                );

            if (sliceAxisPair.Item2 == LinBasisVector.Pz)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnXzSphereSurface(radius, t1, t2),
                    GetNormalOnXzSphereSurface
                );
            
            throw new InvalidOperationException();
        }

        if (sliceAxisPair.Item1 == LinBasisVector.Py)
        {
            if (sliceAxisPair.Item2 == LinBasisVector.Pz)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnYzSphereSurface(radius, t1, t2),
                    GetNormalOnYzSphereSurface
                );

            if (sliceAxisPair.Item2 == LinBasisVector.Px)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnYxSphereSurface(radius, t1, t2),
                    GetNormalOnYxSphereSurface
                );
            
            throw new InvalidOperationException();
        }

        if (sliceAxisPair.Item1 == LinBasisVector.Pz)
        {
            if (sliceAxisPair.Item2 == LinBasisVector.Px)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnZxSphereSurface(radius, t1, t2),
                    GetNormalOnZxSphereSurface
                );

            if (sliceAxisPair.Item2 == LinBasisVector.Py)
                return new GrComputedParametricSurface3D(
                    (t1, t2) => GetPointOnZySphereSurface(radius, t1, t2),
                    GetNormalOnZySphereSurface
                );
            
            throw new InvalidOperationException();
        }

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateSphere3D(this LinBasisVector sliceAxisNormal, double radius)
    {
        return sliceAxisNormal
            .GetAxisPair3D()
            .CreateSphere3D(radius);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateSphere3D(this ILinFloat64Vector3D sliceAxisUnitNormal, double radius)
    {
        return sliceAxisUnitNormal
            .SelectNearestBasisVector()
            .GetAxisPair3D()
            .CreateSphere3D(radius);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrCurveTubeParametricSurface3D CreateTube3D(this Float64Path3D curve, double radius)
    {
        return new GrCurveTubeParametricSurface3D(curve, radius);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrComputedParametricSurface3D CreateMathSurface3D(Func<double, double, double> mathFunction)
    {
        return new GrComputedParametricSurface3D(
            (x, y) => LinFloat64Vector3D.Create(x, y, mathFunction(x, y))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrParametricSurfaceTree3D CreateSampledSurface3D(this IGraphicsParametricSurface3D surface, GrParametricSurfaceTreeOptions3D options)
    {
        var surfaceTree = new GrParametricSurfaceTree3D(
            surface,
            Float64BoundingBox2D.Create(0, 0, 1, 1)
        );

        return surfaceTree.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrParametricSurfaceTree3D CreateSampledSurface3D(this IGraphicsParametricSurface3D surface, Float64BoundingBox2D parameterValueRange, GrParametricSurfaceTreeOptions3D options)
    {
        var surfaceTree = new GrParametricSurfaceTree3D(
            surface,
            parameterValueRange
        );

        return surfaceTree.GenerateTree(options);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrMappedParametricSurface3D CreateMappedSurface3D(this IGraphicsParametricSurface3D surface, IFloat64AffineMap3D map)
    {
        return new GrMappedParametricSurface3D(surface, map);
    }
}