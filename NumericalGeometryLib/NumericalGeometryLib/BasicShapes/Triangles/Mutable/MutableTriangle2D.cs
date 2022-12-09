using System.Diagnostics;
using System.Text;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Borders.Space2D.Mutable;

namespace NumericalGeometryLib.BasicShapes.Triangles.Mutable
{
    public sealed class MutableTriangle2D : ITriangle2D
    {
        public static MutableTriangle2D Create(IFloat64Tuple2D point1, IFloat64Tuple2D point2, IFloat64Tuple2D point3)
        {
            return new MutableTriangle2D(
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


        public MutableTriangle2D()
        {
        }

        internal MutableTriangle2D(double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y)
        {
            Point1X = p1X;
            Point1Y = p1Y;

            Point2X = p2X;
            Point2Y = p2Y;

            Point3X = p3X;
            Point3Y = p3Y;

            Debug.Assert(IsValid());
        }


        public MutableTriangle2D SetPoint1(IFloat64Tuple2D point)
        {
            Point1X = point.X;
            Point1Y = point.Y;

            return this;
        }

        public MutableTriangle2D SetPoint2(IFloat64Tuple2D point)
        {
            Point2X = point.X;
            Point2Y = point.Y;

            return this;
        }

        public MutableTriangle2D SetPoint3(IFloat64Tuple2D point)
        {
            Point3X = point.X;
            Point3Y = point.Y;

            return this;
        }

        public MutableTriangle2D SetTriangle(IFloat64Tuple2D point1, IFloat64Tuple2D point2, IFloat64Tuple2D point3)
        {
            Point1X = point1.X;
            Point1Y = point1.Y;

            Point2X = point2.X;
            Point2Y = point2.Y;

            Point3X = point3.X;
            Point3Y = point3.Y;

            return this;
        }


        public BoundingBox2D GetBoundingBox()
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

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
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
}