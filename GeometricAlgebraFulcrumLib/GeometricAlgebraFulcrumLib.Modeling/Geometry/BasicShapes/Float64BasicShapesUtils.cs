using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public static class Float64BasicShapesUtils
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBoxComposer3D MapBoundingBox(this IFloat64AffineMap3D affineMap, Float64BoundingBoxComposer3D boundingBox)
    {
        return (Float64BoundingBoxComposer3D)boundingBox.MapUsing(affineMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IIntersectable> GetIntersectables<T>(this IEnumerable<T> geometricObjectsList)
        where T : IFloat64FiniteGeometricShape2D
    {
        return geometricObjectsList
            .Cast<IIntersectable>()
            .Where(g => !ReferenceEquals(g, null));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IIntersectable> GetIntersectables(this IEnumerable<IFloat64FiniteGeometricShape3D> geometricObjectsList)
    {
        return geometricObjectsList
            .Cast<IIntersectable>()
            .Where(g => !ReferenceEquals(g, null));
    }

    public static IEnumerable<ILinFloat64Vector2D> GetRegularPolygonPoints(int sidesCount, double centerX, double centerY, double radius, double offsetAngle = 0, bool reverseOrder = false)
    {
        if (sidesCount < 3)
            throw new InvalidOperationException();

        var angleStep = Math.Tau / sidesCount;

        var result = Enumerable
            .Range(0, sidesCount)
            .Select(i => offsetAngle + i * angleStep)
            .Select(a => (ILinFloat64Vector2D)LinFloat64Vector2D.Create((Float64Scalar)(centerX + radius * Math.Cos(a)),
                (Float64Scalar)(centerY + radius * Math.Sin(a))));

        return reverseOrder ? result.Reverse() : result;
    }

    public static IEnumerable<ILinFloat64Vector2D> GetRegularPolygonPoints(int sidesCount, ILinFloat64Vector2D center, double radius, double offsetAngle = 0, bool reverseOrder = false)
    {
        if (sidesCount < 3)
            throw new InvalidOperationException();

        var angleStep = Math.Tau / sidesCount;

        var result = Enumerable
            .Range(0, sidesCount)
            .Select(i => offsetAngle + i * angleStep)
            .Select(a => (ILinFloat64Vector2D)LinFloat64Vector2D.Create(center.X + radius * Math.Cos(a),
                center.Y + radius * Math.Sin(a)));

        return reverseOrder ? result.Reverse() : result;
    }

    #region Affine Maps
    public static Float64Line2D MapLine(this IFloat64AffineMap2D linearMap, IFloat64Line2D line)
    {
        return Float64Line2D.Create(
            linearMap.MapPoint(line.GetOrigin()),
            linearMap.MapVector(line.GetDirection())
        );
    }

    public static Float64Line3D MapLine(this IFloat64AffineMap3D linearMap, IFloat64Line3D line)
    {
        return new Float64Line3D(
            linearMap.MapPoint(line.GetOrigin()),
            linearMap.MapVector(line.GetDirection())
        );
    }

    public static Float64LineSegment2D MapLineSegment(this IFloat64AffineMap2D linearMap, IFloat64LineSegment2D lineSegment)
    {
        return Float64LineSegment2D.Create(
            linearMap.MapPoint(lineSegment.GetPoint1()),
            linearMap.MapPoint(lineSegment.GetPoint2())
        );
    }

    public static Float64LineSegment3D MapLineSegment(this IFloat64AffineMap3D linearMap, IFloat64LineSegment3D lineSegment)
    {
        return Float64LineSegment3D.Create(
            linearMap.MapPoint(lineSegment.GetPoint1()),
            linearMap.MapPoint(lineSegment.GetPoint2())
        );
    }

    public static Float64Triangle2D MapTriangle(this IFloat64AffineMap2D linearMap, IFloat64Triangle2D triangle)
    {
        return Float64Triangle2D.Create(
            linearMap.MapPoint(triangle.GetPoint1()),
            linearMap.MapPoint(triangle.GetPoint2()),
            linearMap.MapPoint(triangle.GetPoint3())
        );
    }

    public static Float64Triangle3D MapTriangle(this IFloat64AffineMap3D linearMap, IFloat64Triangle3D triangle)
    {
        return Float64Triangle3D.Create(
            linearMap.MapPoint(triangle.GetPoint1()),
            linearMap.MapPoint(triangle.GetPoint2()),
            linearMap.MapPoint(triangle.GetPoint3())
        );
    }
    #endregion
}