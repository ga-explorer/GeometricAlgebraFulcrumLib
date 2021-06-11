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
    public class RandomGeometryGenerator : UniformRandomGenerator
    {
        public RandomGeometryGenerator()
        {
        }

        public RandomGeometryGenerator(int seed)
            : base(seed)
        {
        }


        public double GetAngle()
        {
            return GetNumber(MathNet.Numerics.Constants.Pi2);
        }

        public double GetAngle(double maxAngle)
        {
            return GetNumber(maxAngle.ClampAngle());
        }

        public double GetAngle(double minAngle, double maxAngle)
        {
            minAngle = minAngle.ClampAngle();
            maxAngle = maxAngle.ClampAngle();

            if (maxAngle < minAngle)
            {
                var s = minAngle;
                minAngle = maxAngle;
                maxAngle = s;
            }

            return GetNumber(minAngle, maxAngle);
        }


        public IEnumerable<ITuple2D> GetPolygonPoints(int sidesCount, double radius)
        {
            var pointsList = new List<ITuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + GetNumber());

                pointsList.Add(new Tuple2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public List<Tuple2D> GetPolygonPoints(int sidesCount, params double[] radiusList)
        {
            var pointsList = new List<Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + GetNumber());
                var radiusIndex = GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Tuple2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public IEnumerable<ITuple2D> GetPolygonPoints(int sidesCount, Tuple2D centerPoint, double radius)
        {
            var pointsList = new List<ITuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + GetNumber());

                pointsList.Add(new Tuple2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public List<Tuple2D> GetPolygonPoints2D(int sidesCount, Tuple2D centerPoint, params double[] radiusList)
        {
            var pointsList = new List<Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + GetNumber());
                var radiusIndex = GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Tuple2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }


        public Tuple2D GetPointInside(IEnumerable<ITuple2D> pointsList)
        {
            var x = 0.0d;
            var y = 0.0d;
            var d = 0.0d;

            foreach (var point in pointsList)
            {
                var weight = GetNumber();

                x += weight * point.X;
                y += weight * point.Y;
                d += weight;
            }

            d = 1 / d;

            return new Tuple2D(d * x, d * y);
        }

        public Tuple2D GetPointInside(IBoundingBox2D limitsBoundingBox)
        {
            return new Tuple2D(
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public Tuple2D GetPointInside(ILineSegment2D lineSegment)
            => lineSegment.GetPointAt(
                GetNumber()
            );

        public Tuple2D[] GetPointsInside(IBoundingBox2D limitsBoundingBox, int pointsCount)
        {
            var pointsArray = new Tuple2D[pointsCount];

            for (var i = 0; i < pointsCount; i++)
                pointsArray[i] = GetPointInside(limitsBoundingBox);

            return pointsArray;
        }

        public Tuple3D GetPointInside(IBoundingBox3D limitsBoundingBox)
        {
            return new Tuple3D(
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                GetNumber(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
            );
        }

        public Tuple3D GetPointInside(ITriangle3D triangle)
            => triangle.GetPointAt(
                GetNumber(),
                GetNumber(),
                GetNumber()
            );


        public Tuple2D GetUnitVector2D()
        {
            var a = GetNumber(MathNet.Numerics.Constants.Pi2);

            return new Tuple2D(Math.Cos(a), Math.Sin(a));
        }

        public Tuple3D GetUnitVector3D()
        {
            var phi = GetNumber() * 2 * Math.PI;
            var theta = GetNumber() * Math.PI;

            return new UnitSphericalPosition3D(theta, phi).ToTuple3D();
        }


        public LineSegment2D GetLineSegmentInside(IBoundingBox2D limitsBoundingBox)
        {
            return new LineSegment2D(
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public Triangle2D GetTriangleInside(IBoundingBox2D limitsBoundingBox)
        {
            return new Triangle2D(
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public List<Triangle2D> GetTrianglesInside(int trianglesCount, IBoundingBox2D limitsBoundingBox)
        {
            var result = new List<Triangle2D>(trianglesCount);

            for (var i = 0; i < trianglesCount; i++)
                result.Add(
                    new Triangle2D(
                        GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        GetNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        GetNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
                    )
                );

            return result;
        }
    }
}
