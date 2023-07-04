using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Mutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Mutable
{
    public sealed class MutableLineSegment2D : ILineSegment2D
    {
        public static MutableLineSegment2D Create(double point1X, double point1Y, double point2X, double point2Y)
        {
            return new MutableLineSegment2D(
                point1X,
                point1Y,
                point2X,
                point2Y
            );
        }

        public static MutableLineSegment2D Create(IFloat64Vector2D point1, IFloat64Vector2D point2)
        {
            return new MutableLineSegment2D(
                point1.X,
                point1.Y,
                point2.X,
                point2.Y
            );
        }


        public double Point1X { get; set; }

        public double Point1Y { get; set; }


        public double Point2X { get; set; }

        public double Point2Y { get; set; }


        public bool IsValid()
        {
            return !double.IsNaN(Point1X) &&
                   !double.IsNaN(Point1Y) &&
                   !double.IsNaN(Point2X) &&
                   !double.IsNaN(Point2Y);
        }

        public bool IntersectionTestsEnabled { get; set; } = true;


        public MutableLineSegment2D()
        {
        }

        internal MutableLineSegment2D(double p1X, double p1Y, double p2X, double p2Y)
        {
            Point1X = p1X;
            Point1Y = p1Y;

            Point2X = p2X;
            Point2Y = p2Y;

            Debug.Assert(IsValid());
        }


        public MutableLineSegment2D SetPoint1(IFloat64Vector2D point)
        {
            Point1X = point.X;
            Point1Y = point.Y;

            return this;
        }

        public MutableLineSegment2D SetPoint2(IFloat64Vector2D point)
        {
            Point2X = point.X;
            Point2Y = point.Y;

            return this;
        }

        public MutableLineSegment2D SetLineSegment(double point1X, double point1Y, double point2X, double point2Y)
        {
            Point1X = point1X;
            Point1Y = point1Y;

            Point2X = point2X;
            Point2Y = point2Y;

            return this;
        }

        public MutableLineSegment2D SetLineSegment(IFloat64Vector2D point1, IFloat64Vector2D point2)
        {
            Point1X = point1.X;
            Point1Y = point1.Y;

            Point2X = point2.X;
            Point2Y = point2.Y;

            return this;
        }

        public MutableLineSegment2D SetLineSegment(ILineSegment2D lineSegment)
        {
            Point1X = lineSegment.Point1X;
            Point1Y = lineSegment.Point1Y;

            Point2X = lineSegment.Point2X;
            Point2Y = lineSegment.Point2Y;

            return this;
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