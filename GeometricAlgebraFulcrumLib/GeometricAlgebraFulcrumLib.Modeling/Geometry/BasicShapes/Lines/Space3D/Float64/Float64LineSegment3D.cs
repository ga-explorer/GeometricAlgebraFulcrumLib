using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LineSegment3D :
    IFloat64LineSegment3D
{
    public static Float64LineSegment3D Create(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        return new Float64LineSegment3D(
            point1.X,
            point1.Y,
            point1.Z,
            point2.X,
            point2.Y,
            point2.Z
        );
    }

    public static Float64LineSegment3D CreateFromPointAndVector(ILinFloat64Vector3D point1, ILinFloat64Vector3D direction)
    {
        return new Float64LineSegment3D(
            point1.X,
            point1.Y,
            point1.Z,
            point1.X + direction.X,
            point1.Y + direction.Y,
            point1.Z + direction.Z
        );
    }

    public static Float64LineSegment3D CreateFromPointAndScaledVector(ILinFloat64Vector3D point1, ILinFloat64Vector3D direction, double scaleFactor)
    {
        return new Float64LineSegment3D(
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


    public Float64LineSegment3D(double p1X, double p1Y, double p1Z, double p2X, double p2Y, double p2Z)
    {
        Point1X = p1X;
        Point1Y = p1Y;
        Point1Z = p1Z;

        Point2X = p2X;
        Point2Y = p2Y;
        Point2Z = p2Z;

        Debug.Assert(IsValid());
    }


    public Float64BoundingBox3D GetBoundingBox()
    {
        return Float64BoundingBox3D.CreateFromPoints(
            this.GetPoint1(),
            this.GetPoint2()
        );
    }

    public Float64BoundingBoxComposer3D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer3D.CreateFromPoints(
            this.GetPoint1(),
            this.GetPoint2()
        );
    }
}