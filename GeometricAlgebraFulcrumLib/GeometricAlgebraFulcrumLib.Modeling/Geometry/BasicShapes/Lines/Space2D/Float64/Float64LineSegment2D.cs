using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64LineSegment2D :
    IFloat64LineSegment2D
{
    public static Float64LineSegment2D ZeroSegment { get; }
        = new Float64LineSegment2D(0, 0, 0, 0);


    public static Float64LineSegment2D Create(double point1X, double point1Y, double point2X, double point2Y)
    {
        return new Float64LineSegment2D(
            point1X,
            point1Y,
            point2X,
            point2Y
        );
    }

    public static Float64LineSegment2D Create(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        return new Float64LineSegment2D(
            point1.X,
            point1.Y,
            point2.X,
            point2.Y
        );
    }


    public double Point1X { get; }

    public double Point1Y { get; }

    public double Point2X { get; }

    public double Point2Y { get; }


    public bool IsValid()
    {
        return !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y);
    }

    public bool IntersectionTestsEnabled { get; set; } = true;


    public Float64LineSegment2D(double p1X, double p1Y, double p2X, double p2Y)
    {
        Point1X = p1X;
        Point1Y = p1Y;

        Point2X = p2X;
        Point2Y = p2Y;

        Debug.Assert(IsValid());
    }


    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Create(Point1X, Point1Y, Point2X, Point2Y);
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.CreateFromPoints(Point1X, Point1Y, Point2X, Point2Y);
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("Line Segment <")
            .Append("(")
            .Append(Point1X)
            .Append(", ")
            .Append(Point1Y)
            .Append(") - (")
            .Append(Point2X)
            .Append(", ")
            .Append(Point2Y)
            .Append(")>")
            .ToString();
    }
}