using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;

public sealed class Float64TriangleComposer2D :
    IFloat64Triangle2D
{
    public static Float64TriangleComposer2D Create(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, ILinFloat64Vector2D point3)
    {
        return new Float64TriangleComposer2D(
            point1.X, point1.Y,
            point2.X, point2.Y,
            point3.X, point3.Y
        );
    }


    public double Point1X { get; set; }

    public double Point1Y { get; set; }


    public double Point2X { get; set; }

    public double Point2Y { get; set; }


    public double Point3X { get; set; }

    public double Point3Y { get; set; }


    public bool IsValid()
    {
        return !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y) &&
               !double.IsNaN(Point3X) &&
               !double.IsNaN(Point3Y);
    }

    public bool IntersectionTestsEnabled { get; set; } = true;


    public Float64TriangleComposer2D()
    {
    }

    internal Float64TriangleComposer2D(double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y)
    {
        Point1X = p1X;
        Point1Y = p1Y;

        Point2X = p2X;
        Point2Y = p2Y;

        Point3X = p3X;
        Point3Y = p3Y;

        Debug.Assert(IsValid());
    }


    public Float64TriangleComposer2D SetPoint1(ILinFloat64Vector2D point)
    {
        Point1X = point.X;
        Point1Y = point.Y;

        return this;
    }

    public Float64TriangleComposer2D SetPoint2(ILinFloat64Vector2D point)
    {
        Point2X = point.X;
        Point2Y = point.Y;

        return this;
    }

    public Float64TriangleComposer2D SetPoint3(ILinFloat64Vector2D point)
    {
        Point3X = point.X;
        Point3Y = point.Y;

        return this;
    }

    public Float64TriangleComposer2D SetTriangle(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, ILinFloat64Vector2D point3)
    {
        Point1X = point1.X;
        Point1Y = point1.Y;

        Point2X = point2.X;
        Point2Y = point2.Y;

        Point3X = point3.X;
        Point3Y = point3.Y;

        return this;
    }


    public Float64BoundingBox2D GetBoundingBox()
    {
        var minX = Point1X;
        var minY = Point1Y;

        var maxX = Point1X;
        var maxY = Point1Y;

        if (minX > Point2X) minX = Point2X;
        if (minX > Point3X) minX = Point3X;

        if (minY > Point2Y) minY = Point2Y;
        if (minY > Point3Y) minY = Point3Y;

        if (maxX < Point2X) maxX = Point2X;
        if (maxX < Point3X) maxX = Point3X;

        if (maxY < Point2Y) maxY = Point2Y;
        if (maxY < Point3Y) maxY = Point3Y;

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        var minX = Point1X;
        var minY = Point1Y;

        var maxX = Point1X;
        var maxY = Point1Y;

        if (minX > Point2X) minX = Point2X;
        if (minX > Point3X) minX = Point3X;

        if (minY > Point2Y) minY = Point2Y;
        if (minY > Point3Y) minY = Point3Y;

        if (maxX < Point2X) maxX = Point2X;
        if (maxX < Point3X) maxX = Point3X;

        if (maxY < Point2Y) maxY = Point2Y;
        if (maxY < Point3Y) maxY = Point3Y;

        return new Float64BoundingBoxComposer2D(minX, minY, maxX, maxY);
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("Immutable Triangle {(")
            .Append(Point1X)
            .Append(", ")
            .Append(Point1Y)
            .Append(") - (")
            .Append(Point2X)
            .Append(", ")
            .Append(Point2Y)
            .Append(") - (")
            .Append(Point3X)
            .Append(", ")
            .Append(Point3Y)
            .Append(")}")
            .ToString();
    }

}