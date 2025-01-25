using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Planes.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;

public static class Float64TrianglesUtils
{
    #region Triangles in 2D
    public static LinFloat64Vector2D GetPoint1(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)triangle.Point1X, (Float64Scalar)triangle.Point1Y);
    }

    public static LinFloat64Vector2D GetPoint2(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)triangle.Point2X, (Float64Scalar)triangle.Point2Y);
    }

    public static LinFloat64Vector2D GetPoint3(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)triangle.Point3X, (Float64Scalar)triangle.Point3Y);
    }

    public static LinFloat64Vector2D[] GetEndPoints(this IFloat64Triangle2D lineSegment)
    {
        return new[]
        {
            LinFloat64Vector2D.Create((Float64Scalar)lineSegment.Point1X,
                (Float64Scalar)lineSegment.Point1Y),
            LinFloat64Vector2D.Create((Float64Scalar)lineSegment.Point2X,
                (Float64Scalar)lineSegment.Point2Y),
            LinFloat64Vector2D.Create((Float64Scalar)lineSegment.Point3X,
                (Float64Scalar)lineSegment.Point3Y)
        };
    }

    public static IEnumerable<LinFloat64Vector2D> GetEndPoints(this IEnumerable<IFloat64Triangle2D> trianglesList)
    {
        return trianglesList.SelectMany(t => t.GetEndPoints());
    }

    public static Float64Triangle3D ToTriangle(this IFloat64Triangle3D triangle)
    {
        return new Float64Triangle3D(
            triangle.Point1X, triangle.Point1Y, triangle.Point1Z,
            triangle.Point2X, triangle.Point2Y, triangle.Point2Z,
            triangle.Point3X, triangle.Point3Y, triangle.Point3Z
        );
    }

    public static Float64Triangle3D ToTriangleReverse(this IFloat64Triangle3D triangle)
    {
        return new Float64Triangle3D(
            triangle.Point3X, triangle.Point3Y, triangle.Point3Z,
            triangle.Point2X, triangle.Point2Y, triangle.Point2Z,
            triangle.Point1X, triangle.Point1Y, triangle.Point1Z
        );
    }

    public static LinFloat64Vector2D GetDirection12(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point2X - triangle.Point1X),
            (Float64Scalar)(triangle.Point2Y - triangle.Point1Y));
    }

    public static LinFloat64Vector2D GetDirection21(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point1X - triangle.Point2X),
            (Float64Scalar)(triangle.Point1Y - triangle.Point2Y));
    }

    public static LinFloat64Vector2D GetDirection23(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point3X - triangle.Point2X),
            (Float64Scalar)(triangle.Point3Y - triangle.Point2Y));
    }

    public static LinFloat64Vector2D GetDirection32(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point2X - triangle.Point3X),
            (Float64Scalar)(triangle.Point2Y - triangle.Point3Y));
    }

    public static LinFloat64Vector2D GetDirection31(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point1X - triangle.Point3X),
            (Float64Scalar)(triangle.Point1Y - triangle.Point3Y));
    }

    public static LinFloat64Vector2D GetDirection13(this IFloat64Triangle2D triangle)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(triangle.Point3X - triangle.Point1X),
            (Float64Scalar)(triangle.Point3Y - triangle.Point1Y));
    }

    public static Float64LineSegment2D GetLineSegment12(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point1X, triangle.Point1Y,
            triangle.Point2X, triangle.Point2Y
        );
    }

    public static Float64LineSegment2D GetLineSegment21(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point2X, triangle.Point2Y,
            triangle.Point1X, triangle.Point1Y
        );
    }

    public static Float64LineSegment2D GetLineSegment13(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point1X, triangle.Point1Y,
            triangle.Point3X, triangle.Point3Y
        );
    }

    public static Float64LineSegment2D GetLineSegment31(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point3X, triangle.Point3Y,
            triangle.Point1X, triangle.Point1Y
        );
    }

    public static Float64LineSegment2D GetLineSegment23(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point2X, triangle.Point2Y,
            triangle.Point3X, triangle.Point3Y
        );
    }

    public static Float64LineSegment2D GetLineSegment32(this IFloat64Triangle2D triangle)
    {
        return new Float64LineSegment2D(
            triangle.Point3X, triangle.Point3Y,
            triangle.Point2X, triangle.Point2Y
        );
    }

    public static Float64Line2D GetLine12(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point2X - triangle.Point1X,
            triangle.Point2Y - triangle.Point1Y
        );
    }

    public static Float64Line2D GetLine21(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point1X - triangle.Point2X,
            triangle.Point1Y - triangle.Point2Y
        );
    }

    public static Float64Line2D GetLine23(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point3X - triangle.Point2X,
            triangle.Point3Y - triangle.Point2Y
        );
    }

    public static Float64Line2D GetLine32(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point2X - triangle.Point3X,
            triangle.Point2Y - triangle.Point3Y
        );
    }

    public static Float64Line2D GetLine31(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point1X - triangle.Point3X,
            triangle.Point1Y - triangle.Point3Y
        );
    }

    public static Float64Line2D GetLine13(this IFloat64Triangle2D triangle)
    {
        return new Float64Line2D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point3X - triangle.Point1X,
            triangle.Point3Y - triangle.Point1Y
        );
    }

    public static Float64LimitedLine2D GetLimitedLine12(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point2X - triangle.Point1X,
            triangle.Point2Y - triangle.Point1Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D GetLimitedLine21(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point1X - triangle.Point2X,
            triangle.Point1Y - triangle.Point2Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D GetLimitedLine13(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point3X - triangle.Point1X,
            triangle.Point3Y - triangle.Point1Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D GetLimitedLine31(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point1X - triangle.Point3X,
            triangle.Point1Y - triangle.Point3Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D GetLimitedLine23(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point3X - triangle.Point2X,
            triangle.Point3Y - triangle.Point2Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D GetLimitedLine32(this IFloat64Triangle2D triangle)
    {
        return new Float64LimitedLine2D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point2X - triangle.Point3X,
            triangle.Point2Y - triangle.Point3Y,
            0, 1
        );
    }

    public static LinFloat64Vector2D GetInnerPointAt(this IFloat64Triangle2D triangle, double d1, double d2, double d3)
    {
        var dInv = 1 / (d1 + d2 + d3);
        var w1 = d1 * dInv;
        var w2 = d2 * dInv;
        var w3 = d3 * dInv;

        return LinFloat64Vector2D.Create((Float64Scalar)(w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X),
            (Float64Scalar)(w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y));
    }

    public static LinFloat64Vector2D GetInnerPointAt(this IFloat64Triangle2D triangle, ILinFloat64Vector3D dTuple)
    {
        var dInv = 1 / (dTuple.X + dTuple.Y + dTuple.Z);
        var w1 = dTuple.X * dInv;
        var w2 = dTuple.Y * dInv;
        var w3 = dTuple.Z * dInv;

        return LinFloat64Vector2D.Create(w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
            w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y);
    }

    public static IEnumerable<LinFloat64Vector2D> GetInnerPointsAt(this IFloat64Triangle2D triangle, IEnumerable<ILinFloat64Vector3D> parametersList)
    {
        return parametersList.Select(
            p => triangle.GetInnerPointAt(p.X, p.Y, p.Z)
        );
    }

    public static LinFloat64Vector2D GetPointAt(this IFloat64Triangle2D triangle, double w1, double w2)
    {
        var w3 = 1.0d - (w1 + w2);

        return LinFloat64Vector2D.Create((Float64Scalar)(w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X),
            (Float64Scalar)(w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y));
    }

    public static IEnumerable<LinFloat64Vector2D> GetPointsAt(this IFloat64Triangle2D triangle, IEnumerable<LinFloat64Vector2D> parametersList)
    {
        return parametersList.Select(
            p => triangle.GetPointAt(p.X, p.Y)
        );
    }

    public static Float64Triangle2D GetTriangleAt(this IFloat64Triangle2D triangle, LinFloat64Vector2D w1, LinFloat64Vector2D w2, LinFloat64Vector2D w3)
    {
        var point1 = triangle.GetPointAt(w1.X, w1.Y);
        var point2 = triangle.GetPointAt(w2.X, w2.Y);
        var point3 = triangle.GetPointAt(w3.X, w3.Y);

        return new Float64Triangle2D(
            point1.X, point1.Y,
            point2.X, point2.Y,
            point3.X, point3.Y
        );
    }
    #endregion

    #region Triangles in 3D
    public static LinFloat64Vector3D GetPoint1(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point1X, triangle.Point1Y, triangle.Point1Z);
    }

    public static LinFloat64Vector3D GetPoint2(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point2X, triangle.Point2Y, triangle.Point2Z);
    }

    public static LinFloat64Vector3D GetPoint3(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point3X, triangle.Point3Y, triangle.Point3Z);
    }

    public static LinFloat64Vector3D[] GetEndPoints(this IFloat64Triangle3D lineSegment)
    {
        return new[]
        {
            LinFloat64Vector3D.Create(lineSegment.Point1X,
                lineSegment.Point1Y,
                lineSegment.Point1Z),
            LinFloat64Vector3D.Create(lineSegment.Point2X,
                lineSegment.Point2Y,
                lineSegment.Point2Z),
            LinFloat64Vector3D.Create(lineSegment.Point3X,
                lineSegment.Point3Y,
                lineSegment.Point3Z)
        };
    }

    public static IEnumerable<LinFloat64Vector3D> GetEndPoints(this IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        return trianglesList.SelectMany(t => t.GetEndPoints());
    }

    public static LinFloat64Vector3D GetDirection12(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point2X - triangle.Point1X,
            triangle.Point2Y - triangle.Point1Y,
            triangle.Point2Z - triangle.Point1Z);
    }

    public static LinFloat64Vector3D GetDirection21(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point1X - triangle.Point2X,
            triangle.Point1Y - triangle.Point2Y,
            triangle.Point1Z - triangle.Point2Z);
    }

    public static LinFloat64Vector3D GetDirection23(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point3X - triangle.Point2X,
            triangle.Point3Y - triangle.Point2Y,
            triangle.Point3Z - triangle.Point2Z);
    }

    public static LinFloat64Vector3D GetDirection32(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point2X - triangle.Point3X,
            triangle.Point2Y - triangle.Point3Y,
            triangle.Point2Z - triangle.Point3Z);
    }

    public static LinFloat64Vector3D GetDirection31(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point1X - triangle.Point3X,
            triangle.Point1Y - triangle.Point3Y,
            triangle.Point1Z - triangle.Point3Z);
    }

    public static LinFloat64Vector3D GetDirection13(this IFloat64Triangle3D triangle)
    {
        return LinFloat64Vector3D.Create(triangle.Point3X - triangle.Point1X,
            triangle.Point3Y - triangle.Point1Y,
            triangle.Point3Z - triangle.Point1Z);
    }

    public static Float64LineSegment3D GetSide12(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z
        );
    }

    public static Float64LineSegment3D GetSide21(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z
        );
    }

    public static Float64LineSegment3D GetSide13(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z
        );
    }

    public static Float64LineSegment3D GetSide31(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z
        );
    }

    public static Float64LineSegment3D GetSide23(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z
        );
    }

    public static Float64LineSegment3D GetSide32(this IFloat64Triangle3D triangle)
    {
        return new Float64LineSegment3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z
        );
    }

    public static Float64LineSegment3D[] GetSides(this IFloat64Triangle3D triangle)
    {
        return new[]
        {
            triangle.GetSide12(),
            triangle.GetSide23(),
            triangle.GetSide31()
        };
    }

    public static IEnumerable<Float64LineSegment3D> GetSides(this IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        return trianglesList.SelectMany(t => t.GetSides());
    }

    public static Float64Line3D GetLine12(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point2X - triangle.Point1X,
            triangle.Point2Y - triangle.Point1Y,
            triangle.Point2Z - triangle.Point1Z
        );
    }

    public static Float64Line3D GetLine21(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point1X - triangle.Point2X,
            triangle.Point1Y - triangle.Point2Y,
            triangle.Point1Z - triangle.Point2Z
        );
    }

    public static Float64Line3D GetLine23(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point3X - triangle.Point2X,
            triangle.Point3Y - triangle.Point2Y,
            triangle.Point3Z - triangle.Point2Z
        );
    }

    public static Float64Line3D GetLine32(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point2X - triangle.Point3X,
            triangle.Point2Y - triangle.Point3Y,
            triangle.Point2Z - triangle.Point3Z
        );
    }

    public static Float64Line3D GetLine31(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point1X - triangle.Point3X,
            triangle.Point1Y - triangle.Point3Y,
            triangle.Point1Z - triangle.Point3Z
        );
    }

    public static Float64Line3D GetLine13(this IFloat64Triangle3D triangle)
    {
        return new Float64Line3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point3X - triangle.Point1X,
            triangle.Point3Y - triangle.Point1Y,
            triangle.Point3Z - triangle.Point1Z
        );
    }

    public static Float64Line3D[] GetLines(this IFloat64Triangle3D triangle)
    {
        return new[]
        {
            triangle.GetLine12(),
            triangle.GetLine23(),
            triangle.GetLine31()
        };
    }

    public static IEnumerable<Float64Line3D> GetLines(this IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        return trianglesList.SelectMany(t => t.GetLines());
    }

    public static Float64LimitedLine3D GetLimitedLine12(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point2X - triangle.Point1X,
            triangle.Point2Y - triangle.Point1Y,
            triangle.Point2Z - triangle.Point1Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D GetLimitedLine21(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point1X - triangle.Point2X,
            triangle.Point1Y - triangle.Point2Y,
            triangle.Point1Z - triangle.Point2Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D GetLimitedLine23(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point2X,
            triangle.Point2Y,
            triangle.Point2Z,
            triangle.Point3X - triangle.Point2X,
            triangle.Point3Y - triangle.Point2Y,
            triangle.Point3Z - triangle.Point2Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D GetLimitedLine32(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point2X - triangle.Point3X,
            triangle.Point2Y - triangle.Point3Y,
            triangle.Point2Z - triangle.Point3Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D GetLimitedLine31(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point3X,
            triangle.Point3Y,
            triangle.Point3Z,
            triangle.Point1X - triangle.Point3X,
            triangle.Point1Y - triangle.Point3Y,
            triangle.Point1Z - triangle.Point3Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D GetLimitedLine13(this IFloat64Triangle3D triangle)
    {
        return new Float64LimitedLine3D(
            triangle.Point1X,
            triangle.Point1Y,
            triangle.Point1Z,
            triangle.Point3X - triangle.Point1X,
            triangle.Point3Y - triangle.Point1Y,
            triangle.Point3Z - triangle.Point1Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D[] GetLimitedLines(this IFloat64Triangle3D triangle)
    {
        return new[]
        {
            triangle.GetLimitedLine12(),
            triangle.GetLimitedLine23(),
            triangle.GetLimitedLine31()
        };
    }

    public static IEnumerable<Float64LimitedLine3D> GetLimitedLines(this IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        return trianglesList.SelectMany(t => t.GetLimitedLines());
    }

    public static LinFloat64Vector3D GetNormal(this IFloat64Triangle3D triangle)
    {
        return triangle
            .GetDirection12()
            .VectorCross(
                triangle.GetDirection23()
            );
    }

    public static LinFloat64Vector3D GetUnitNormal(this IFloat64Triangle3D triangle)
    {
        return triangle
            .GetDirection12()
            .VectorUnitCross(
                triangle.GetDirection23()
            );
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64Triangle3D triangle, double d1, double d2, double d3)
    {
        var d = 1 / (d1 + d2 + d3);
        var w1 = d1 * d;
        var w2 = d2 * d;
        var w3 = d3 * d;

        return LinFloat64Vector3D.Create(w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
            w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y,
            w1 * triangle.Point1Z + w2 * triangle.Point2Z + w3 * triangle.Point3Z);
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64Triangle3D triangle, double w1, double w2)
    {
        var w3 = 1.0d - (w1 + w2);

        return LinFloat64Vector3D.Create(w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
            w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y,
            w1 * triangle.Point1Z + w2 * triangle.Point2Z + w3 * triangle.Point3Z);
    }

    public static IEnumerable<LinFloat64Vector3D> GetPointsAt(this IFloat64Triangle3D triangle, IEnumerable<LinFloat64Vector3D> parametersList)
    {
        return parametersList.Select(p => triangle.GetPointAt(p.X, p.Y));
    }

    public static Float64Triangle3D GetTriangleAt(this IFloat64Triangle3D triangle, LinFloat64Vector3D w1, LinFloat64Vector3D w2, LinFloat64Vector3D w3)
    {
        var point1 = triangle.GetPointAt(w1.X, w1.Y);
        var point2 = triangle.GetPointAt(w2.X, w2.Y);
        var point3 = triangle.GetPointAt(w3.X, w3.Y);

        return new Float64Triangle3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z,
            point3.X, point3.Y, point3.Z
        );
    }

    public static Float64Plane3D ToPlane(this IFloat64Triangle3D triangle)
    {
        return new Float64Plane3D(
            triangle.GetPoint1(),
            triangle.GetDirection12(),
            triangle.GetDirection23()
        );
    }
    #endregion

}