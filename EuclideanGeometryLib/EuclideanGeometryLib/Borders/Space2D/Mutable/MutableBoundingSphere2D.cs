using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Maps.Space2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;
using EuclideanGeometryLib.Borders.Space2D.Immutable;

namespace EuclideanGeometryLib.Borders.Space2D.Mutable
{
    public sealed class MutableBoundingSphere2D : IBorderCurve2D
    {
        public static MutableBoundingSphere2D Create()
        {
            return new MutableBoundingSphere2D();
        }

        public static MutableBoundingSphere2D Create(double centerX, double centerY, double radius)
        {
            return new MutableBoundingSphere2D(centerX, centerY, radius);
        }

        public static MutableBoundingSphere2D Create(ITuple2D center, double radius)
        {
            return new MutableBoundingSphere2D(center.X, center.Y, radius);
        }

        public static MutableBoundingSphere2D CreateFromPoints(IEnumerable<ITuple2D> pointsList, bool tightBound = true)
        {
            var pointsArray = pointsList.ToArray();

            if (!tightBound)
            {
                var center = pointsArray.GetCenterPoint();
                var radius = 
                    Math.Sqrt(
                        center
                            .GetDistancesSquaredToPoints(pointsArray)
                            .Max()
                    );

                //This is the fast but not tight method
                return new MutableBoundingSphere2D(center, radius);
            }

            //This is the slower but tighter method
            var maxDistance = 0.0d;
            var maxPoint1 = pointsArray[0];
            var maxPoint2 = pointsArray[0];

            for (var i = 0; i < pointsArray.Length - 1; i++)
            {
                var p1 = pointsArray[i];

                for (var j = i + 1; j < pointsArray.Length; j++)
                {
                    var p2 = pointsArray[j];
                    var distance = p1.GetDistanceToPoint(p2);

                    if (distance <= maxDistance)
                        continue;

                    maxDistance = distance;
                    maxPoint1 = p1;
                    maxPoint2 = p2;
                }
            }

            var center1 = new Tuple2D(
                0.5 * (maxPoint1.X + maxPoint2.X),
                0.5 * (maxPoint1.Y + maxPoint2.Y)
            );

            var radius1 = 0.5 * maxDistance;

            return new MutableBoundingSphere2D(center1, radius1);
        }


        public double Radius { get; private set; }

        public double CenterX { get; private set; }

        public double CenterY { get; private set; }

        public Tuple2D Center
            => new Tuple2D(CenterX, CenterY);

        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid
            => !double.IsNaN(Radius) &&
               !double.IsNaN(CenterX) &&
               !double.IsNaN(CenterY);

        public bool IsInvalid
            => double.IsNaN(Radius) || 
               double.IsNaN(CenterX) || 
               double.IsNaN(CenterY);


        internal MutableBoundingSphere2D()
        {
        }

        internal MutableBoundingSphere2D(ITuple2D center, double radius)
        {
            CenterX = center.X;
            CenterY = center.Y;
            Radius = radius;

            ValidateValues();
        }

        internal MutableBoundingSphere2D(double centerX, double centerY, double radius)
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius;

            ValidateValues();
        }


        private void ValidateValues()
        {
            Debug.Assert(!IsInvalid);

            if (Radius < 0) Radius = -Radius;
        }


        public MutableBoundingSphere2D SetRadius(double radius)
        {
            Radius = radius;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetCenter(ITuple2D center)
        {
            CenterX = center.X;
            CenterY = center.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetCenter(double centerX, double centerY)
        {
            CenterX = centerX;
            CenterY = centerY;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetTo(ITuple2D center, double radius)
        {
            CenterX = center.X;
            CenterY = center.Y;
            Radius = radius;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetTo(double centerX, double centerY, double radius)
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius;

            ValidateValues();

            return this;
        }


        public MutableBoundingSphere2D UpdateRadiusBy(double radiusDelta)
        {
            Radius = Radius + radiusDelta;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D UpdateRadiusByFactor(double radiusFactor)
        {
            Radius = Radius * radiusFactor;

            ValidateValues();

            return this;
        }


        public MutableBoundingSphere2D MoveCenterBy(double xDelta, double yDelta)
        {
            CenterX += xDelta;
            CenterY += yDelta;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D MoveCenterBy(ITuple2D delta)
        {
            CenterX += delta.X;
            CenterY += delta.Y;

            ValidateValues();

            return this;
        }


        public BoundingBox2D GetBoundingBox()
        {
            var point1 = new Tuple2D(CenterX - Radius, CenterY - Radius);
            var point2 = new Tuple2D(CenterX + Radius, CenterY + Radius);

            return BoundingBox2D.Create(point1, point2);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            var point1 = new Tuple2D(CenterX - Radius, CenterY - Radius);
            var point2 = new Tuple2D(CenterX + Radius, CenterY + Radius);

            return MutableBoundingBox2D.CreateFromPoints(point1, point2);
        }

        public IBorderCurve2D MapUsing(IAffineMap2D affineMap)
        {
            var s1 = affineMap.MapVector(Tuple2D.E1).ToTuple2D().GetLength();
            var s2 = affineMap.MapVector(Tuple2D.E2).ToTuple2D().GetLength();

            var sMax = s1 > s2 ? s1 : s2;

            var center = affineMap.MapPoint(Center);

            return new MutableBoundingSphere2D(
                center.X, 
                center.Y, 
                sMax * Radius
            );
        }
    }
}