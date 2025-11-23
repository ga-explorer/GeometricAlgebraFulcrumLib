using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

public static class Float64BoundingSurfaceUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this Float64ScalarRange boundingBox)
    {
        yield return boundingBox.MinValue;
        yield return boundingBox.MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingCircle2D GetBoundingCircle(this IEnumerable<LinFloat64Vector2D> pointsList)
    {
        return Float64BoundingCircle2D.CreateFromPoints(pointsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox2D GetBoundingBox(this IEnumerable<LinFloat64Vector2D> pointsList)
    {
        return Float64BoundingBox2D.Containing(pointsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox2D GetBoundingBox(this IEnumerable<LinFloat64Vector2D> pointsList, double scalingFactor)
    {
        return Float64BoundingBox2D.Containing(pointsList, scalingFactor);
    }

    public static Float64BoundingBox2D GetBoundingBox2D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64BoundedShape2D
    {
        var result = new Float64BoundingBoxComposer2D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }

    public static Float64BoundingBoxComposer2D GetBoundingBoxComposer2D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64BoundedShape2D
    {
        var result = new Float64BoundingBoxComposer2D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox3D GetBoundingBox(this IEnumerable<LinFloat64Vector3D> pointsList)
    {
        return Float64BoundingBox3D.Containing(pointsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox3D GetBoundingBox(this IEnumerable<LinFloat64Vector3D> pointsList, double scalingFactor)
    {
        return Float64BoundingBox3D.Containing(pointsList, scalingFactor);
    }

    public static Float64BoundingBox3D GetBoundingBox3D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64BoundedShape3D
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }

    public static Float64BoundingBoxComposer3D GetBoundingBoxComposer3D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64BoundedShape3D
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result;
    }

}
