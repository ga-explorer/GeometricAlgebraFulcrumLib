using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Mutable
{
    public sealed class MutableBoundingBox2D : IBoundingBox2D
    {
        public static MutableBoundingBox2D CreateInfinite()
        {
            return new MutableBoundingBox2D();
        }

        public static MutableBoundingBox2D CreateInfiniteX(double y1, double y2)
        {
            return y1 >= y2
                ? new MutableBoundingBox2D(double.NegativeInfinity, y1, double.PositiveInfinity, y2)
                : new MutableBoundingBox2D(double.NegativeInfinity, y2, double.PositiveInfinity, y1);
        }

        public static MutableBoundingBox2D CreateInfiniteY(double x1, double x2)
        {
            return x1 >= x2
                ? new MutableBoundingBox2D(x1, double.NegativeInfinity, x2, double.PositiveInfinity)
                : new MutableBoundingBox2D(x2, double.NegativeInfinity, x1, double.PositiveInfinity);
        }


        public static MutableBoundingBox2D CreateFromPoint(double pointX, double pointY)
        {
            return new MutableBoundingBox2D(pointX, pointY);
        }

        public static MutableBoundingBox2D CreateFromPoint(IFloat64Vector2D point)
        {
            return new MutableBoundingBox2D(point.X, point.Y);
        }

        public static MutableBoundingBox2D CreateFromPoints(double point1X, double point1Y, double point2X, double point2Y)
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }

        public static MutableBoundingBox2D CreateFromPoints(IFloat64Vector2D point1, IFloat64Vector2D point2)
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }

        public static MutableBoundingBox2D CreateFromPoints(IFloat64Vector2D point1, IFloat64Vector2D point2, IFloat64Vector2D point3)
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }

        public static MutableBoundingBox2D CreateFromPoints(params IFloat64Vector2D[] pointsList)
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }

        public static MutableBoundingBox2D CreateFromPoints(IEnumerable<IFloat64Vector2D> pointsList)
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

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }


        public static MutableBoundingBox2D CreateAround(double centerX, double centerY, double deltaX, double deltaY)
        {
            var minX = centerX - deltaX;
            var maxX = centerX + deltaX;
            var minY = centerY - deltaY;
            var maxY = centerY + deltaY;

            if (deltaX < 0)
            {
                (maxX, minX) = (minX, maxX);
            }

            if (deltaY < 0)
            {
                (maxY, minY) = (minY, maxY);
            }

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }

        public static MutableBoundingBox2D CreateAround(IFloat64Vector2D center, double deltaX, double deltaY)
        {
            var minX = center.X - deltaX;
            var maxX = center.X + deltaX;
            var minY = center.Y - deltaY;
            var maxY = center.Y + deltaY;

            if (deltaX < 0)
            {
                (maxX, minX) = (minX, maxX);
            }

            if (deltaY < 0)
            {
                (maxY, minY) = (minY, maxY);
            }

            return new MutableBoundingBox2D(minX, minY, maxX, maxY);
        }


        public static MutableBoundingBox2D Create(IBoundingBox2D boundingBox)
        {
            return new MutableBoundingBox2D(boundingBox);
        }

        public static MutableBoundingBox2D Create(IBoundingBox2D b1, IBoundingBox2D b2)
        {
            return new MutableBoundingBox2D(
                Math.Min(b1.MinX, b2.MinX),
                Math.Min(b1.MinY, b2.MinY),
                Math.Max(b1.MaxX, b2.MaxX),
                Math.Max(b1.MaxY, b2.MaxY)
            );
        }

        public static MutableBoundingBox2D Create(params IBoundingBox2D[] boundingBoxesList)
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

            return result;
        }

        public static MutableBoundingBox2D Create(IEnumerable<IBoundingBox2D> boundingBoxesList)
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

            return result;
        }

        public static MutableBoundingBox2D CreateFromIntersection(IBoundingBox2D b1, IBoundingBox2D b2)
        {
            return new MutableBoundingBox2D(
                Math.Max(b1.MinX, b2.MinX),
                Math.Max(b1.MinY, b2.MinY),
                Math.Min(b1.MaxX, b2.MaxX),
                Math.Min(b1.MaxY, b2.MaxY)
            );
        }


        public static MutableBoundingBox2D Create<T>(params T[] geometricObjectsList)
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

            return result;
        }

        public static MutableBoundingBox2D Create<T>(IEnumerable<T> geometricObjectsList)
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

            return result;
        }


        public double MinX { get; private set; }

        public double MinY { get; private set; }

        public double MaxX { get; private set; }

        public double MaxY { get; private set; }

        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid()
        {
            return !double.IsNaN(MinX) &&
                   !double.IsNaN(MinY) &&
                   !double.IsNaN(MaxX) &&
                   !double.IsNaN(MaxY);
        }


        internal MutableBoundingBox2D()
        {
            MinX = double.NegativeInfinity;
            MinY = double.NegativeInfinity;

            MaxX = double.PositiveInfinity;
            MaxY = double.PositiveInfinity;
        }

        internal MutableBoundingBox2D(double x, double y)
        {
            MinX = x;
            MinY = y;

            MaxX = x;
            MaxY = y;

            ValidateValues();
        }

        internal MutableBoundingBox2D(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;

            MaxX = maxX;
            MaxY = maxY;

            ValidateValues();
        }

        internal MutableBoundingBox2D(IBoundingBox2D boundingBox)
        {
            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;

            MaxY = boundingBox.MaxY;
            MaxX = boundingBox.MaxX;

            ValidateValues();
        }


        private void ValidateValues()
        {
            Debug.Assert(IsValid());

            if (MaxX < MinX)
            {
                (MaxX, MinX) = (MinX, MaxX);
            }

            if (MaxY < MinY)
            {
                (MaxY, MinY) = (MinY, MaxY);
            }
        }


        public MutableBoundingBox2D SetTo(double pointX, double pointY)
        {
            MinX = pointX;
            MinY = pointY;

            MaxX = pointX;
            MaxY = pointY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D SetTo(IFloat64Vector2D point)
        {
            MinX = point.X;
            MinY = point.Y;

            MaxX = point.X;
            MaxY = point.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D SetTo(IBoundingBox2D boundingBox)
        {
            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;

            MaxX = boundingBox.MaxX;
            MaxY = boundingBox.MaxY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D SetTo(IFiniteGeometricShape2D geometricObject)
        {
            var boundingBox = geometricObject.GetBoundingBox();

            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;

            MaxX = boundingBox.MaxX;
            MaxY = boundingBox.MaxY;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D MoveMidPointTo(IFloat64Vector2D newMidPoint)
        {
            var deltaX = newMidPoint.X - 0.5d * (MaxX + MinX);
            var deltaY = newMidPoint.Y - 0.5d * (MaxY + MinY);

            MinX += deltaX;
            MinY += deltaY;

            MaxX += deltaX;
            MaxY += deltaY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D MoveMidPointTo(double newMidPointX, double newMidPointY, double newMidPointZ)
        {
            var deltaX = newMidPointX - 0.5d * (MaxX + MinX);
            var deltaY = newMidPointY - 0.5d * (MaxY + MinY);

            MinX += deltaX;
            MinY += deltaY;

            MaxX += deltaX;
            MaxY += deltaY;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D MoveBy(IFloat64Vector2D delta)
        {
            MinX += delta.X;
            MinY += delta.Y;

            MaxX += delta.X;
            MaxY += delta.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D MoveBy(double deltaX, double deltaY, double deltaZ)
        {
            MinX += deltaX;
            MinY += deltaY;

            MaxX += deltaX;
            MaxY += deltaY;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D UpdateSizeBy(double delta)
        {
            MinX = MinX - delta;
            MinY = MinY - delta;

            MaxX = MaxX + delta;
            MaxY = MaxY + delta;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D UpdateSizeBy(double deltaX, double deltaY)
        {
            MinX = MinX - deltaX;
            MinY = MinY - deltaY;

            MaxX = MaxX + deltaX;
            MaxY = MaxY + deltaY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D UpdateSizeBy(IFloat64Vector2D delta)
        {
            MinX = MinX - delta.X;
            MinY = MinY - delta.Y;

            MaxX = MaxX + delta.X;
            MaxY = MaxY + delta.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D UpdateSizeByFactor(double updateFactor)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);

            MinX = (MinX - midX) * updateFactor + midX;
            MinY = (MinY - midY) * updateFactor + midY;

            MaxX = (MaxX - midX) * updateFactor + midX;
            MaxY = (MaxY - midY) * updateFactor + midY;

            //var deltaX = updateFactor * (MaxX - MinX);
            //var deltaY = updateFactor * (MaxY - MinY);

            //MinX = MinX - deltaX;
            //MinY = MinY - deltaY;

            //MaxX = MaxX + deltaX;
            //MaxY = MaxY + deltaY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D UpdateSizeByFactor(IFloat64Vector2D updateFactor)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);

            MinX = (MinX - midX) * updateFactor.X + midX;
            MinY = (MinY - midY) * updateFactor.Y + midY;

            MaxX = (MaxX - midX) * updateFactor.X + midX;
            MaxY = (MaxY - midY) * updateFactor.Y + midY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D UpdateSizeByFactor(double updateFactorX, double updateFactorY)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);

            MinX = (MinX - midX) * updateFactorX + midX;
            MinY = (MinY - midY) * updateFactorY + midY;

            MaxX = (MaxX - midX) * updateFactorX + midX;
            MaxY = (MaxY - midY) * updateFactorY + midY;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D ExpandToInfinite()
        {
            MinX = double.NegativeInfinity;
            MinY = double.NegativeInfinity;

            MaxX = double.PositiveInfinity;
            MaxY = double.PositiveInfinity;

            return this;
        }


        public MutableBoundingBox2D ExpandToInclude(double pointX, double pointY)
        {
            if (MinX > pointX) MinX = pointX;
            if (MinY > pointY) MinY = pointY;

            if (MaxX < pointX) MaxX = pointX;
            if (MaxY < pointY) MaxY = pointY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(IFloat64Vector2D point)
        {
            if (MinX > point.X) MinX = point.X;
            if (MinY > point.Y) MinY = point.Y;

            if (MaxX < point.X) MaxX = point.X;
            if (MaxY < point.Y) MaxY = point.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(params IFloat64Vector2D[] pointsList)
        {
            foreach (var point in pointsList)
            {
                if (MinX > point.X) MinX = point.X;
                if (MinY > point.Y) MinY = point.Y;

                if (MaxX < point.X) MaxX = point.X;
                if (MaxY < point.Y) MaxY = point.Y;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(IEnumerable<IFloat64Vector2D> pointsList)
        {
            foreach (var point in pointsList)
            {
                if (MinX > point.X) MinX = point.X;
                if (MinY > point.Y) MinY = point.Y;

                if (MaxX < point.X) MaxX = point.X;
                if (MaxY < point.Y) MaxY = point.Y;
            }

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D ExpandToInclude(IBoundingBox2D boundingBox)
        {
            if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
            if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

            if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
            if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(params IBoundingBox2D[] boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(IEnumerable<IBoundingBox2D> boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            }

            ValidateValues();

            return this;
        }


        public MutableBoundingBox2D ExpandToInclude(IFiniteGeometricShape2D geometricObject)
        {
            var boundingBox = geometricObject.GetBoundingBox();

            if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
            if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

            if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
            if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(params IFiniteGeometricShape2D[] geometricObjectsList)
        {
            foreach (var geometricObject in geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox2D ExpandToInclude(IEnumerable<IFiniteGeometricShape2D> geometricObjectsList)
        {
            foreach (var geometricObject in geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            }

            ValidateValues();

            return this;
        }


        public IBorderCurve2D MapUsing(IAffineMap2D affineMap)
        {
            var oldSide01 = Float64Vector2D.Create((Float64Scalar)(MaxX - MinX), 0);
            var oldSide10 = Float64Vector2D.Create(0, (Float64Scalar)(MaxY - MinY));

            var newSide01 = Float64Vector2DComposerUtils.ToVector2D((IPair<double>)affineMap.MapVector(oldSide01));
            var newSide10 = Float64Vector2DComposerUtils.ToVector2D((IPair<double>)affineMap.MapVector(oldSide10));

            var newCorner00 = Float64Vector2DComposerUtils.ToVector2D((IPair<double>)affineMap.MapPoint(this.GetMinCorner()));
            var newCorner01 = newCorner00 + newSide01;
            var newCorner10 = newCorner00 + newSide10;
            var newCorner11 = newCorner01 + newSide10;

            return CreateFromPoints(
                newCorner00, 
                newCorner01, 
                newCorner10,
                newCorner11
            );
        }

        public BoundingBox2D GetBoundingBox()
        {
            return new BoundingBox2D(this);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return new MutableBoundingBox2D(this);
        }
    }
}
