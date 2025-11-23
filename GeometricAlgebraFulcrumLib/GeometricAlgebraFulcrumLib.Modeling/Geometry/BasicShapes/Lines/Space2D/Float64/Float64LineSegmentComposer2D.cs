using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64LineSegmentComposer2D : 
    IFloat64LineSegment2D
{
    public static Float64LineSegmentComposer2D Create(double point1X, double point1Y, double point2X, double point2Y)
    {
        return new Float64LineSegmentComposer2D(
            point1X,
            point1Y,
            point2X,
            point2Y
        );
    }

    public static Float64LineSegmentComposer2D Create(IPair<double> point1, IPair<double> point2)
    {
        return new Float64LineSegmentComposer2D(
            point1.Item1,
            point1.Item2,
            point2.Item1,
            point2.Item2
        );
    }


    public double Point1X { get; set; }

    public double Point1Y { get; set; }
    
    public double Point2X { get; set; }

    public double Point2Y { get; set; }
    
    public LinFloat64Vector2D Point1 
        => LinFloat64Vector2D.Create(Point1X, Point1Y);
    
    public LinFloat64Vector2D Point2 
        => LinFloat64Vector2D.Create(Point2X, Point2Y);

    public LinFloat64Vector2D Item1 
        => Point1;

    public LinFloat64Vector2D Item2 
        => Point2;


    public bool IsValid()
    {
        return !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y);
    }

    public bool IntersectionTestsEnabled { get; set; } = true;


    public Float64LineSegmentComposer2D()
    {
    }

    internal Float64LineSegmentComposer2D(double p1X, double p1Y, double p2X, double p2Y)
    {
        Point1X = p1X;
        Point1Y = p1Y;

        Point2X = p2X;
        Point2Y = p2Y;

        Debug.Assert(IsValid());
    }


    public Float64LineSegmentComposer2D SetPoint1(IPair<double> point)
    {
        Point1X = point.Item1;
        Point1Y = point.Item2;

        return this;
    }

    public Float64LineSegmentComposer2D SetPoint2(IPair<double> point)
    {
        Point2X = point.Item1;
        Point2Y = point.Item2;

        return this;
    }

    public Float64LineSegmentComposer2D SetLineSegment(double point1X, double point1Y, double point2X, double point2Y)
    {
        Point1X = point1X;
        Point1Y = point1Y;

        Point2X = point2X;
        Point2Y = point2Y;

        return this;
    }

    public Float64LineSegmentComposer2D SetLineSegment(IPair<double> point1, IPair<double> point2)
    {
        Point1X = point1.Item1;
        Point1Y = point1.Item2;

        Point2X = point2.Item1;
        Point2Y = point2.Item2;

        return this;
    }

    public Float64LineSegmentComposer2D SetLineSegment(IFloat64LineSegment2D lineSegment)
    {
        Point1X = lineSegment.Point1X;
        Point1Y = lineSegment.Point1Y;

        Point2X = lineSegment.Point2X;
        Point2Y = lineSegment.Point2Y;

        return this;
    }

    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Containing(Point1X, Point1Y, Point2X, Point2Y);
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.Containing(Point1X, Point1Y, Point2X, Point2Y);
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