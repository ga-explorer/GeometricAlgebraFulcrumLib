﻿using System.Diagnostics;
using System.Text;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.BasicShapes.Triangles.Mutable
{
    public sealed class MutableTriangle3D : ITriangle3D
    {
        public static MutableTriangle3D CreateFromPoints(ITuple3D point1, ITuple3D point2, ITuple3D point3)
        {
            return new MutableTriangle3D(
                point1.X, point1.Y, point1.Z,
                point2.X, point2.Y, point2.Z,
                point3.X, point3.Y, point3.Z
            );
        }


        public bool IntersectionTestsEnabled { get; set; } = true;


        public double Point1X { get; set; }

        public double Point1Y { get; set; }

        public double Point1Z { get; set; }


        public double Point2X { get; set; }

        public double Point2Y { get; set; }

        public double Point2Z { get; set; }


        public double Point3X { get; set; }

        public double Point3Y { get; set; }

        public double Point3Z { get; set; }


        public bool IsValid()
        {
            return !double.IsNaN(Point1X) &&
                   !double.IsNaN(Point1Y) &&
                   !double.IsNaN(Point1Z) &&
                   !double.IsNaN(Point2X) &&
                   !double.IsNaN(Point2Y) &&
                   !double.IsNaN(Point2Z) &&
                   !double.IsNaN(Point3X) &&
                   !double.IsNaN(Point3Y) &&
                   !double.IsNaN(Point3Z);
        }


        public MutableTriangle3D()
        {
        }

        internal MutableTriangle3D(double p1X, double p1Y, double p1Z, double p2X, double p2Y, double p2Z, double p3X, double p3Y, double p3Z)
        {
            Point1X = p1X;
            Point1Y = p1Y;
            Point1Z = p1Z;

            Point2X = p2X;
            Point2Y = p2Y;
            Point2Z = p2Z;

            Point3X = p3X;
            Point3Y = p3Y;
            Point3Z = p3Z;

            Debug.Assert(IsValid());
        }


        public MutableTriangle3D SetPoint1(double x, double y, double z)
        {
            Point1X = x;
            Point1Y = y;
            Point1Z = z;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableTriangle3D SetPoint2(double x, double y, double z)
        {
            Point2X = x;
            Point2Y = y;
            Point2Z = z;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableTriangle3D SetPoint3(double x, double y, double z)
        {
            Point3X = x;
            Point3Y = y;
            Point3Z = z;

            Debug.Assert(IsValid());

            return this;
        }


        public BoundingBox3D GetBoundingBox()
        {
            var minX = Point1X;
            var minY = Point1Y;
            var minZ = Point1Z;

            var maxX = Point1X;
            var maxY = Point1Y;
            var maxZ = Point1Z;

            if (minX > Point2X) minX = Point2X;
            if (minX > Point3X) minX = Point3X;

            if (minY > Point2Y) minY = Point2Y;
            if (minY > Point3Y) minY = Point3Y;

            if (minZ > Point2Z) minZ = Point2Z;
            if (minZ > Point3Z) minZ = Point3Z;

            if (maxX < Point2X) maxX = Point2X;
            if (maxX < Point3X) maxX = Point3X;

            if (maxY < Point2Y) maxY = Point2Y;
            if (maxY < Point3Y) maxY = Point3Y;

            if (maxZ < Point2Z) maxZ = Point2Z;
            if (maxZ < Point3Z) maxZ = Point3Z;

            return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            var minX = Point1X;
            var minY = Point1Y;
            var minZ = Point1Z;

            var maxX = Point1X;
            var maxY = Point1Y;
            var maxZ = Point1Z;

            if (minX > Point2X) minX = Point2X;
            if (minX > Point3X) minX = Point3X;

            if (minY > Point2Y) minY = Point2Y;
            if (minY > Point3Y) minY = Point3Y;

            if (minZ > Point2Z) minZ = Point2Z;
            if (minZ > Point3Z) minZ = Point3Z;

            if (maxX < Point2X) maxX = Point2X;
            if (maxX < Point3X) maxX = Point3X;

            if (maxY < Point2Y) maxY = Point2Y;
            if (maxY < Point3Y) maxY = Point3Y;

            if (maxZ < Point2Z) maxZ = Point2Z;
            if (maxZ < Point3Z) maxZ = Point3Z;

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("Triangle <")
                .Append("(")
                .Append(Point1X)
                .Append(", ")
                .Append(Point1Y)
                .Append(", ")
                .Append(Point1Z)
                .Append(") - (")
                .Append(Point2X)
                .Append(", ")
                .Append(Point2Y)
                .Append(", ")
                .Append(Point2Z)
                .Append(") - (")
                .Append(Point3X)
                .Append(", ")
                .Append(Point3Y)
                .Append(", ")
                .Append(Point3Z)
                .Append(")>")
                .ToString();
        }
    }
}