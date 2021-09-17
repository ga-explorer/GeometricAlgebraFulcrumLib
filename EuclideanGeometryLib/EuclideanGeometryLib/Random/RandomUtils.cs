using System;
using System.Collections.Generic;
using DataStructuresLib.Random;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Coordinates;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;
using EuclideanGeometryLib.Borders.Space2D;
using EuclideanGeometryLib.Borders.Space3D;

namespace EuclideanGeometryLib.Random
{
    public static class RandomUtils
    {
        public static IEnumerable<ITuple2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, double radius)
        {
            var pointsList = new List<ITuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

                pointsList.Add(new Tuple2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static List<Tuple2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, params double[] radiusList)
        {
            var pointsList = new List<Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
                var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Tuple2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static IEnumerable<ITuple2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, Tuple2D centerPoint, double radius)
        {
            var pointsList = new List<ITuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

                pointsList.Add(new Tuple2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static List<Tuple2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, Tuple2D centerPoint, params double[] radiusList)
        {
            var pointsList = new List<Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
                var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Tuple2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }


        public static Tuple2D GetPointInside(this System.Random randomGenerator, IEnumerable<ITuple2D> pointsList)
        {
            var x = 0.0d;
            var y = 0.0d;
            var d = 0.0d;

            foreach (var point in pointsList)
            {
                var weight = randomGenerator.GetNumber();

                x += weight * point.X;
                y += weight * point.Y;
                d += weight;
            }

            d = 1 / d;

            return new Tuple2D(d * x, d * y);
        }

        public static Tuple2D GetPointInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new Tuple2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static Tuple2D GetPointInside(this System.Random randomGenerator, ILineSegment2D lineSegment)
            => lineSegment.GetPointAt(
                randomGenerator.GetNumber()
            );

        public static Tuple2D[] GetPointsInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox, int pointsCount)
        {
            var pointsArray = new Tuple2D[pointsCount];

            for (var i = 0; i < pointsCount; i++)
                pointsArray[i] = randomGenerator.GetPointInside(limitsBoundingBox);

            return pointsArray;
        }

        public static Tuple3D GetPointInside(this System.Random randomGenerator, IBoundingBox3D limitsBoundingBox)
        {
            return new Tuple3D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
            );
        }

        public static Tuple3D GetPointInside(this System.Random randomGenerator, ITriangle3D triangle)
            => triangle.GetPointAt(
                randomGenerator.GetNumber(),
                randomGenerator.GetNumber(),
                randomGenerator.GetNumber()
            );


        public static Tuple2D GetUnitVector2D(this System.Random randomGenerator)
        {
            var angle = randomGenerator.GetAngle();

            return new Tuple2D(Math.Cos(angle), Math.Sin(angle));
        }

        public static Tuple3D GetUnitVector3D(this System.Random randomGenerator)
        {
            var phi = randomGenerator.GetNumber() * 2d * Math.PI;
            var theta = randomGenerator.GetNumber() * Math.PI;

            return new UnitSphericalPosition3D(theta, phi).ToTuple3D();
        }


        public static LineSegment2D GetLineSegmentInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new LineSegment2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static Triangle2D GetTriangleInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new Triangle2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static List<Triangle2D> GetTrianglesInside(this System.Random randomGenerator, int trianglesCount, IBoundingBox2D limitsBoundingBox)
        {
            var result = new List<Triangle2D>(trianglesCount);

            for (var i = 0; i < trianglesCount; i++)
                result.Add(
                    new Triangle2D(
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
                    )
                );

            return result;
        }
    }
}
