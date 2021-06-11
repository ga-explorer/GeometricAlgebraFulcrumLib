using System.Diagnostics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;

namespace EuclideanGeometryLib.BasicShapes.Lines.Immutable
{
    public sealed class LineSegment2D : ILineSegment2D
    {
        public static LineSegment2D Create(double point1X, double point1Y, double point2X, double point2Y)
        {
            return new LineSegment2D(
                point1X,
                point1Y,
                point2X,
                point2Y
            );
        }

        public static LineSegment2D Create(ITuple2D point1, ITuple2D point2)
        {
            return new LineSegment2D(
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


        public bool IsValid
            => !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y);

        public bool IsInvalid
            => double.IsNaN(Point1X) || 
               double.IsNaN(Point1Y) ||
               double.IsNaN(Point2X) || 
               double.IsNaN(Point2Y);

        public bool IntersectionTestsEnabled { get; set; } = true;


        internal LineSegment2D(double p1X, double p1Y, double p2X, double p2Y)
        {
            Point1X = p1X;
            Point1Y = p1Y;

            Point2X = p2X;
            Point2Y = p2Y;

            Debug.Assert(!IsInvalid);
        }


        public BoundingBox2D GetBoundingBox()
        {
            return BoundingBox2D.Create(Point1X, Point1Y, Point2X, Point2Y);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return MutableBoundingBox2D.CreateFromPoints(Point1X, Point1Y, Point2X, Point2Y);
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
}
