using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Mutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;

public sealed class LineSegment3D : 
    ILineSegment3D
{
    public static LineSegment3D Create(IFloat64Vector3D point1, IFloat64Vector3D point2)
    {
        return new LineSegment3D(
            point1.X,
            point1.Y,
            point1.Z,
            point2.X,
            point2.Y,
            point2.Z
        );
    }

    public static LineSegment3D CreateFromPointAndVector(IFloat64Vector3D point1, IFloat64Vector3D direction)
    {
        return new LineSegment3D(
            point1.X,
            point1.Y,
            point1.Z,
            point1.X + direction.X,
            point1.Y + direction.Y,
            point1.Z + direction.Z
        );
    }

    public static LineSegment3D CreateFromPointAndScaledVector(IFloat64Vector3D point1, IFloat64Vector3D direction, double scaleFactor)
    {
        return new LineSegment3D(
            point1.X,
            point1.Y,
            point1.Z,
            point1.X + scaleFactor * direction.X,
            point1.Y + scaleFactor * direction.Y,
            point1.Z + scaleFactor * direction.Z
        );
    }


    public double Point1X { get; }

    public double Point1Y { get; }

    public double Point1Z { get; }


    public double Point2X { get; }

    public double Point2Y { get; }

    public double Point2Z { get; }


    public bool IsValid()
    {
        return !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point1Z) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y) &&
               !double.IsNaN(Point2Z);
    }


    public bool IntersectionTestsEnabled { get; } = false;


    public LineSegment3D(double p1X, double p1Y, double p1Z, double p2X, double p2Y, double p2Z)
    {
        Point1X = p1X;
        Point1Y = p1Y;
        Point1Z = p1Z;

        Point2X = p2X;
        Point2Y = p2Y;
        Point2Z = p2Z;

        Debug.Assert(IsValid());
    }


    public BoundingBox3D GetBoundingBox()
    {
        return BoundingBox3D.CreateFromPoints(
            this.GetPoint1(), 
            this.GetPoint2()
        );
    }

    public MutableBoundingBox3D GetMutableBoundingBox()
    {
        return MutableBoundingBox3D.CreateFromPoints(
            this.GetPoint1(), 
            this.GetPoint2()
        );
    }
}