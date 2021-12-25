using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space2D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space2D.Mutable;

namespace NumericalGeometryLib.Borders.Space2D.Immutable
{
    public sealed record BoundingBox2D : 
        IBoundingBox2D
    {
        public static BoundingBox2D CreateInfinite()
        {
            return new BoundingBox2D(
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity
            );
        }

        public static BoundingBox2D CreateInfiniteX(double y1, double y2)
        {
            return y1 >= y2
                ? new BoundingBox2D(double.NegativeInfinity, y1, double.PositiveInfinity, y2)
                : new BoundingBox2D(double.NegativeInfinity, y2, double.PositiveInfinity, y1);
        }

        public static BoundingBox2D CreateInfiniteY(double x1, double x2)
        {
            return x1 >= x2
                ? new BoundingBox2D(x1, double.NegativeInfinity, x2, double.PositiveInfinity)
                : new BoundingBox2D(x2, double.NegativeInfinity, x1, double.PositiveInfinity);
        }


        public static BoundingBox2D CreateAround(double centerX, double centerY, double deltaX, double deltaY)
        {
            var minX = centerX - deltaX;
            var maxX = centerX + deltaX;
            var minY = centerY - deltaY;
            var maxY = centerY + deltaY;

            if (deltaX < 0) 
                (maxX, minX) = (minX, maxX);

            if (deltaY < 0) 
                (maxY, minY) = (minY, maxY);

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static BoundingBox2D CreateAround(ITuple2D center, double deltaX, double deltaY)
        {
            var minX = center.X - deltaX;
            var maxX = center.X + deltaX;
            var minY = center.Y - deltaY;
            var maxY = center.Y + deltaY;

            if (deltaX < 0) 
                (maxX, minX) = (minX, maxX);

            if (deltaY < 0) 
                (maxY, minY) = (minY, maxY);

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }


        public static BoundingBox2D Create(ITuple2D point)
        {
            return new BoundingBox2D(
                point.X,
                point.Y,
                point.X,
                point.Y
            );
        }

        public static BoundingBox2D Create(double point1X, double point1Y, double point2X, double point2Y)
        {
            double minX, minY, maxX, maxY;

            if (point1X <= point2X)
            {
                minX = point1X;
                maxX = point2X;
            }
            else
            {
                minX = point2X;
                maxX = point1X;
            }

            if (point1Y <= point2Y)
            {
                minY = point1Y;
                maxY = point2Y;
            }
            else
            {
                minY = point2Y;
                maxY = point1Y;
            }

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static BoundingBox2D Create(ITuple2D point1, ITuple2D point2)
        {
            double minX, minY, maxX, maxY;

            if (point1.X <= point2.X)
            {
                minX = point1.X;
                maxX = point2.X;
            }
            else
            {
                minX = point2.X;
                maxX = point1.X;
            }

            if (point1.Y <= point2.Y)
            {
                minY = point1.Y;
                maxY = point2.Y;
            }
            else
            {
                minY = point2.Y;
                maxY = point1.Y;
            }

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static BoundingBox2D CreateFromPoints(ITuple2D point1, ITuple2D point2, ITuple2D point3)
        {
            var minX = point1.X;
            var minY = point1.Y;

            var maxX = point1.X;
            var maxY = point1.Y;

            if (minX > point2.X) minX = point2.X;
            if (minX > point3.X) minX = point3.X;

            if (minY > point2.Y) minY = point2.Y;
            if (minY > point3.Y) minY = point3.Y;

            if (maxX < point2.X) maxX = point2.X;
            if (maxX < point3.X) maxX = point3.X;

            if (maxY < point2.Y) maxY = point2.Y;
            if (maxY < point3.Y) maxY = point3.Y;

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static BoundingBox2D CreateFromPoints(params ITuple2D[] pointsList)
        {
            var point1 = pointsList[0];

            var minX = point1.X;
            var minY = point1.Y;

            var maxX = point1.X;
            var maxY = point1.Y;

            foreach (var point in pointsList.Skip(1))
            {
                if (minX > point.X) minX = point.X;
                if (minY > point.Y) minY = point.Y;
                if (maxX < point.X) maxX = point.X;
                if (maxY < point.Y) maxY = point.Y;
            }

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static BoundingBox2D CreateFromPoints(IEnumerable<ITuple2D> pointsList)
        {
            double minX = 0, minY = 0, maxX = 0, maxY = 0;

            var flag = false;
            foreach (var point in pointsList)
            {
                if (!flag)
                {
                    minX = point.X;
                    minY = point.Y;

                    maxX = point.X;
                    maxY = point.Y;

                    flag = true;
                    continue;
                }

                if (minX > point.X) minX = point.X;
                if (minY > point.Y) minY = point.Y;
                if (maxX < point.X) maxX = point.X;
                if (maxY < point.Y) maxY = point.Y;
            }

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
        
        public static BoundingBox2D CreateFromPoints(IEnumerable<ITuple2D> pointsList, double updateFactor)
        {
            double minX = 0, minY = 0, maxX = 0, maxY = 0;

            var flag = false;
            foreach (var point in pointsList)
            {
                if (!flag)
                {
                    minX = point.X;
                    minY = point.Y;

                    maxX = point.X;
                    maxY = point.Y;

                    flag = true;
                    continue;
                }

                if (minX > point.X) minX = point.X;
                if (minY > point.Y) minY = point.Y;
                if (maxX < point.X) maxX = point.X;
                if (maxY < point.Y) maxY = point.Y;
            }

            var midX = 0.5d * (maxX + minX);
            var midY = 0.5d * (maxY + minY);

            minX = (minX - midX) * updateFactor + midX;
            minY = (minY - midY) * updateFactor + midY;

            maxX = (maxX - midX) * updateFactor + midX;
            maxY = (maxY - midY) * updateFactor + midY;

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }


        public static BoundingBox2D Create(IBoundingBox2D boundingBox)
        {
            return new BoundingBox2D(boundingBox);
        }

        public static BoundingBox2D Create(IBoundingBox2D b1, IBoundingBox2D b2)
        {
            return new BoundingBox2D(
                Math.Min(b1.MinX, b2.MinX),
                Math.Min(b1.MinY, b2.MinY),
                Math.Max(b1.MaxX, b2.MaxX),
                Math.Max(b1.MaxY, b2.MaxY)
            );
        }

        public static BoundingBox2D Create(params IBoundingBox2D[] boundingBoxesList)
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var boundingBox in boundingBoxesList)
            {
                if (!flag)
                {
                    result = boundingBox.GetMutableBoundingBox();

                    flag = true;
                    continue;
                }

                result.ExpandToInclude(boundingBox);
            }

            return result.GetBoundingBox();
        }

        public static BoundingBox2D Create(IEnumerable<IBoundingBox2D> boundingBoxesList)
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var boundingBox in boundingBoxesList)
            {
                if (!flag)
                {
                    result = boundingBox.GetMutableBoundingBox();

                    flag = true;
                    continue;
                }

                result.ExpandToInclude(boundingBox);
            }

            return result.GetBoundingBox();
        }

        public static BoundingBox2D CreateFromIntersection(IBoundingBox2D b1, IBoundingBox2D b2)
        {
            return new BoundingBox2D(
                Math.Max(b1.MinX, b2.MinX),
                Math.Max(b1.MinY, b2.MinY),
                Math.Min(b1.MaxX, b2.MaxX),
                Math.Min(b1.MaxY, b2.MaxY)
            );
        }


        public static BoundingBox2D Create(IFiniteGeometricShape2D geometricObject)
        {
            return geometricObject.GetBoundingBox();
        }

        public static BoundingBox2D Create(params IFiniteGeometricShape2D[] geometricObjectsList)
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var geometricObject in geometricObjectsList)
            {
                if (!flag)
                {
                    result = geometricObject.GetMutableBoundingBox();

                    flag = true;
                    continue;
                }

                result.ExpandToInclude(geometricObject.GetBoundingBox());
            }

            return result.GetBoundingBox();
        }

        //public static BoundingBox2D Create(IEnumerable<IGeometricObject2D> geometricObjectsList)
        //{
        //    var result = new MutableBoundingBox2D();

        //    var flag = false;
        //    foreach (var geometricObject in geometricObjectsList)
        //    {
        //        if (!flag)
        //        {
        //            result = geometricObject.GetMutableBoundingBox();

        //            flag = true;
        //            continue;
        //        }

        //        result.ExpandToInclude(geometricObject.GetBoundingBox());
        //    }

        //    return result.GetBoundingBox();
        //}

        public static BoundingBox2D Create<T>(IEnumerable<T> geometricObjectsList)
            where T : IFiniteGeometricShape2D
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var geometricObject in geometricObjectsList)
            {
                if (!flag)
                {
                    result = geometricObject.GetMutableBoundingBox();

                    flag = true;
                    continue;
                }

                result.ExpandToInclude(geometricObject.GetBoundingBox());
            }

            return result.GetBoundingBox();
        }



        public double MinX { get; }

        public double MinY { get; }

        public double MidX 
            => 0.5d * (MinX + MaxX);

        public double MaxX { get; }

        public double MaxY { get; }

        public double MidY 
            => 0.5d * (MinY + MaxY);

        public bool IntersectionTestsEnabled { get; set; } 
            = true;

        public bool IsValid()
        {
            return MinX.IsValid() &&
                   MinY.IsValid() &&
                   MaxX.IsValid() &&
                   MaxY.IsValid();
        }


        internal BoundingBox2D(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;

            MaxY = maxY;
            MaxX = maxX;

            Debug.Assert(IsValid());
        }

        internal BoundingBox2D(IBoundingBox2D boundingBox)
        {
            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;

            MaxY = boundingBox.MaxY;
            MaxX = boundingBox.MaxX;

            Debug.Assert(IsValid());
        }


        public IBorderCurve2D MapUsing(IAffineMap2D affineMap)
        {
            throw new NotImplementedException();
        }

        public BoundingBox2D GetBoundingBox()
        {
            return this;
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return new MutableBoundingBox2D(this);
        }
    }
}
