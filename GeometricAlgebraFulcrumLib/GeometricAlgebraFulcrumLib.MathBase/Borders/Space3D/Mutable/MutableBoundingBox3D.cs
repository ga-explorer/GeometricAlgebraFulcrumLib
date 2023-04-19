using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Mutable
{
    /// <summary>
    /// This class holds the information of an Axis-Aligned Bounding Box
    /// </summary>
    public sealed class MutableBoundingBox3D : IBoundingBox3D
    {
        public static MutableBoundingBox3D CreateInfinite()
        {
            return new MutableBoundingBox3D(
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity, 
                double.PositiveInfinity
            );
        }

        public static MutableBoundingBox3D CreateInfiniteX(double y1, double z1, double y2, double z2)
        {
            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }

            if (z1 > z2)
            {
                (z1, z2) = (z2, z1);
            }

            return new MutableBoundingBox3D(
                double.NegativeInfinity, y1, z1, 
                double.PositiveInfinity, y2, z2
            );
        }

        public static MutableBoundingBox3D CreateInfiniteY(double x1, double z1, double x2, double z2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }

            if (z1 > z2)
            {
                (z1, z2) = (z2, z1);
            }

            return new MutableBoundingBox3D(
                x1, double.NegativeInfinity, z1,
                x2, double.PositiveInfinity, z2
            );
        }

        public static MutableBoundingBox3D CreateInfiniteZ(double x1, double y1, double x2, double y2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }

            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }

            return new MutableBoundingBox3D(
                x1, y1, double.NegativeInfinity,
                x2, y2, double.PositiveInfinity
            );
        }

        public static MutableBoundingBox3D CreateInfiniteXy(double z1, double z2)
        {
            if (z1 > z2)
            {
                (z1, z2) = (z2, z1);
            }

            return new MutableBoundingBox3D(
                double.NegativeInfinity, double.NegativeInfinity, z1,
                double.PositiveInfinity, double.PositiveInfinity, z2
            );
        }

        public static MutableBoundingBox3D CreateInfiniteYz(double x1, double x2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }

            return new MutableBoundingBox3D(
                x1, double.NegativeInfinity, double.NegativeInfinity,
                x2, double.PositiveInfinity, double.PositiveInfinity
            );
        }

        public static MutableBoundingBox3D CreateInfiniteXz(double y1, double y2)
        {
            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }

            return new MutableBoundingBox3D(
                double.NegativeInfinity, y1, double.NegativeInfinity,
                double.PositiveInfinity, y2, double.PositiveInfinity
            );
        }


        public static MutableBoundingBox3D CreateAround(double centerX, double centerY, double centerZ, double deltaX, double deltaY, double deltaZ)
        {
            var minX = centerX - deltaX;
            var maxX = centerX + deltaX;
            var minY = centerY - deltaY;
            var maxY = centerY + deltaY;
            var minZ = centerZ - deltaZ;
            var maxZ = centerZ + deltaZ;

            if (deltaX < 0)
            {
                (maxX, minX) = (minX, maxX);
            }

            if (deltaY < 0)
            {
                (maxY, minY) = (minY, maxY);
            }

            if (deltaZ < 0)
            {
                (maxZ, minZ) = (minZ, maxZ);
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static MutableBoundingBox3D CreateAround(IFloat64Tuple3D center, double deltaX, double deltaY, double deltaZ)
        {
            var minX = center.X - deltaX;
            var maxX = center.X + deltaX;
            var minY = center.Y - deltaY;
            var maxY = center.Y + deltaY;
            var minZ = center.Z - deltaZ;
            var maxZ = center.Z + deltaZ;

            if (deltaX < 0)
            {
                (maxX, minX) = (minX, maxX);
            }

            if (deltaY < 0)
            {
                (maxY, minY) = (minY, maxY);
            }

            if (deltaZ < 0)
            {
                (maxZ, minZ) = (minZ, maxZ);
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }


        public static MutableBoundingBox3D CreateFromPoint(double pointX, double pointY, double pointZ)
        {
            return new MutableBoundingBox3D(pointX, pointY, pointZ);
        }

        public static MutableBoundingBox3D CreateFromPoint(IFloat64Tuple3D point)
        {
            return new MutableBoundingBox3D(point.X, point.Y, point.Z);
        }

        public static MutableBoundingBox3D CreateFromPoints(double point1X, double point1Y, double point1Z, double point2X, double point2Y, double point2Z)
        {
            double minX, minY, minZ, maxX, maxY, maxZ;

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

            if (point1Z <= point2Z)
            {
                minZ = point1Z;
                maxZ = point2Z;
            }
            else
            {
                minZ = point2Z;
                maxZ = point1Z;
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static MutableBoundingBox3D CreateFromPoints(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
        {
            double minX, minY, minZ, maxX, maxY, maxZ;

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

            if (point1.Z <= point2.Z)
            {
                minZ = point1.Z;
                maxZ = point2.Z;
            }
            else
            {
                minZ = point2.Z;
                maxZ = point1.Z;
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static MutableBoundingBox3D CreateFromPoints(IFloat64Tuple3D point1, IFloat64Tuple3D point2, IFloat64Tuple3D point3)
        {
            var minX = point1.X;
            var minY = point1.Y;
            var minZ = point1.Z;

            var maxX = point1.X;
            var maxY = point1.Y;
            var maxZ = point1.Z;

            if (minX > point2.X) minX = point2.X;
            if (minX > point3.X) minX = point3.X;

            if (minY > point2.Y) minY = point2.Y;
            if (minY > point3.Y) minY = point3.Y;

            if (minZ > point2.Z) minZ = point2.Z;
            if (minZ > point3.Z) minZ = point3.Z;

            if (maxX < point2.X) maxX = point2.X;
            if (maxX < point3.X) maxX = point3.X;

            if (maxY < point2.Y) maxY = point2.Y;
            if (maxY < point3.Y) maxY = point3.Y;

            if (maxZ < point2.Z) maxZ = point2.Z;
            if (maxZ < point3.Z) maxZ = point3.Z;

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static MutableBoundingBox3D CreateFromPoints(params IFloat64Tuple3D[] pointsList)
        {
            var point1 = pointsList[0];

            var minX = point1.X;
            var minY = point1.Y;
            var minZ = point1.Z;

            var maxX = point1.X;
            var maxY = point1.Y;
            var maxZ = point1.Z;

            foreach (var point in pointsList.Skip(1))
            {
                if (minX > point.X) minX = point.X;
                if (minY > point.Y) minY = point.Y;
                if (minZ > point.Z) minZ = point.Z;

                if (maxX < point.X) maxX = point.X;
                if (maxY < point.Y) maxY = point.Y;
                if (maxZ < point.Z) maxZ = point.Z;
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static MutableBoundingBox3D CreateFromPoints(IEnumerable<IFloat64Tuple3D> pointsList)
        {
            double minX = 0, minY = 0, minZ = 0;
            double maxX = 0, maxY = 0, maxZ = 0;

            var flag = false;
            foreach (var point in pointsList)
            {
                if (!flag)
                {
                    minX = point.X;
                    minY = point.Y;
                    minZ = point.Z;

                    maxX = point.X;
                    maxY = point.Y;
                    maxZ = point.Z;

                    flag = true;
                    continue;
                }

                if (minX > point.X) minX = point.X;
                if (minY > point.Y) minY = point.Y;
                if (minZ > point.Z) minZ = point.Z;

                if (maxX < point.X) maxX = point.X;
                if (maxY < point.Y) maxY = point.Y;
                if (maxZ < point.Z) maxZ = point.Z;
            }

            return new MutableBoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }


        public static MutableBoundingBox3D Create(IBoundingBox3D boundingBox)
        {
            return new MutableBoundingBox3D(boundingBox);
        }

        public static MutableBoundingBox3D Create(IBoundingBox3D b1, IBoundingBox3D b2)
        {
            return new MutableBoundingBox3D(
                Math.Min(b1.MinX, b2.MinX),
                Math.Min(b1.MinY, b2.MinY),
                Math.Min(b1.MinZ, b2.MinZ),
                Math.Max(b1.MaxX, b2.MaxX),
                Math.Max(b1.MaxY, b2.MaxY),
                Math.Max(b1.MaxZ, b2.MaxZ)
            );
        }

        public static MutableBoundingBox3D Create(params IBoundingBox3D[] boundingBoxesList)
        {
            var result = new MutableBoundingBox3D();

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

        public static MutableBoundingBox3D Create(IEnumerable<IBoundingBox3D> boundingBoxesList)
        {
            var result = new MutableBoundingBox3D();

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

        public static MutableBoundingBox3D CreateFromIntersection(IBoundingBox3D b1, IBoundingBox3D b2)
        {
            return new MutableBoundingBox3D(
                Math.Max(b1.MinX, b2.MinX),
                Math.Max(b1.MinY, b2.MinY),
                Math.Max(b1.MinZ, b2.MinZ),
                Math.Min(b1.MaxX, b2.MaxX),
                Math.Min(b1.MaxY, b2.MaxY),
                Math.Min(b1.MaxZ, b2.MaxZ)
            );
        }


        public static MutableBoundingBox3D Create<T>(params T[] geometricObjectsList)
            where T : IFiniteGeometricShape3D
        {
            var result = new MutableBoundingBox3D();

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

        public static MutableBoundingBox3D Create<T>(IEnumerable<T> geometricObjectsList)
            where T : IFiniteGeometricShape3D
        {
            var result = new MutableBoundingBox3D();

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

        public double MinZ { get; private set; }

        public double MaxX { get; private set; }

        public double MaxY { get; private set; }

        public double MaxZ { get; private set; }
        
        public double MidX
            => 0.5d * (MinX + MaxX);
        
        public double MidY
            => 0.5d * (MinY + MaxY);
        
        public double MidZ
            => 0.5d * (MinZ + MaxZ);


        public bool IsValid()
        {
            return !double.IsNaN(MinX) &&
                   !double.IsNaN(MinY) &&
                   !double.IsNaN(MinZ) &&
                   !double.IsNaN(MaxX) &&
                   !double.IsNaN(MaxY) &&
                   !double.IsNaN(MaxZ);
        }

        public bool IntersectionTestsEnabled { get; set; } = true;


        internal MutableBoundingBox3D()
        {
        }

        internal MutableBoundingBox3D(double x, double y, double z)
        {
            MinX = x;
            MinY = y;
            MinZ = z;

            MaxX = x;
            MaxY = y;
            MaxZ = z;

            Debug.Assert(IsValid());
        }

        internal MutableBoundingBox3D(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;

            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;

            Debug.Assert(IsValid());
        }

        internal MutableBoundingBox3D(IBoundingBox3D boundingBox)
        {
            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;
            MinZ = boundingBox.MinZ;

            MaxX = boundingBox.MaxX;
            MaxY = boundingBox.MaxY;
            MaxZ = boundingBox.MaxZ;

            Debug.Assert(IsValid());
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

            if (MaxZ < MinZ)
            {
                (MaxZ, MinZ) = (MinZ, MaxZ);
            }
        }


        public MutableBoundingBox3D SetTo(double pointX, double pointY, double pointZ)
        {
            MinX = pointX;
            MinY = pointY;
            MinZ = pointZ;

            MaxX = pointX;
            MaxY = pointY;
            MaxZ = pointZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D SetTo(IFloat64Tuple3D point)
        {
            MinX = point.X;
            MinY = point.Y;
            MinZ = point.Z;

            MaxX = point.X;
            MaxY = point.Y;
            MaxZ = point.Z;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D SetTo(IBoundingBox3D boundingBox)
        {
            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;
            MinZ = boundingBox.MinZ;

            MaxX = boundingBox.MaxX;
            MaxY = boundingBox.MaxY;
            MaxZ = boundingBox.MaxZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D SetTo(IFiniteGeometricShape3D geometricObject)
        {
            var boundingBox = geometricObject.GetBoundingBox();

            MinX = boundingBox.MinX;
            MinY = boundingBox.MinY;
            MinZ = boundingBox.MinZ;

            MaxX = boundingBox.MaxX;
            MaxY = boundingBox.MaxY;
            MaxZ = boundingBox.MaxZ;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D MoveMidPointTo(IFloat64Tuple3D newMidPoint)
        {
            var deltaX = newMidPoint.X - 0.5d * (MaxX + MinX);
            var deltaY = newMidPoint.Y - 0.5d * (MaxY + MinY);
            var deltaZ = newMidPoint.Z - 0.5d * (MaxZ + MinZ);

            MinX += deltaX;
            MinY += deltaY;
            MinZ += deltaZ;

            MaxX += deltaX;
            MaxY += deltaY;
            MaxZ += deltaZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D MoveMidPointTo(double newMidPointX, double newMidPointY, double newMidPointZ)
        {
            var deltaX = newMidPointX - 0.5d * (MaxX + MinX);
            var deltaY = newMidPointY - 0.5d * (MaxY + MinY);
            var deltaZ = newMidPointZ - 0.5d * (MaxZ + MinZ);

            MinX += deltaX;
            MinY += deltaY;
            MinZ += deltaZ;

            MaxX += deltaX;
            MaxY += deltaY;
            MaxZ += deltaZ;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D MoveBy(IFloat64Tuple3D delta)
        {
            MinX += delta.X;
            MinY += delta.Y;
            MinZ += delta.Z;

            MaxX += delta.X;
            MaxY += delta.Y;
            MaxZ += delta.Z;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D MoveBy(double deltaX, double deltaY, double deltaZ)
        {
            MinX += deltaX;
            MinY += deltaY;
            MinZ += deltaZ;

            MaxX += deltaX;
            MaxY += deltaY;
            MaxZ += deltaZ;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D UpdateSizeBy(double delta)
        {
            MinX = MinX - delta;
            MinY = MinY - delta;
            MinZ = MinZ - delta;

            MaxX = MaxX + delta;
            MaxY = MaxY + delta;
            MaxZ = MaxZ + delta;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D UpdateSizeBy(IFloat64Tuple3D delta)
        {
            MinX = MinX - delta.X;
            MinY = MinY - delta.Y;
            MinZ = MinZ - delta.Z;

            MaxX = MaxX + delta.X;
            MaxY = MaxY + delta.Y;
            MaxZ = MaxZ + delta.Z;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D UpdateSizeBy(double deltaX, double deltaY, double deltaZ)
        {
            MinX = MinX - deltaX;
            MinY = MinY - deltaY;
            MinZ = MinZ - deltaZ;

            MaxX = MaxX + deltaX;
            MaxY = MaxY + deltaY;
            MaxZ = MaxZ + deltaZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D UpdateSizeByFactor(double updateFactor)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);
            var midZ = 0.5d * (MaxZ + MinZ);

            MinX = (MinX - midX) * updateFactor + midX;
            MinY = (MinY - midY) * updateFactor + midY;
            MinZ = (MinZ - midZ) * updateFactor + midZ;

            MaxX = (MaxX - midX) * updateFactor + midX;
            MaxY = (MaxY - midY) * updateFactor + midY;
            MaxZ = (MaxZ - midZ) * updateFactor + midZ;

            //var deltaX = updateFactor * (MaxX - MinX);
            //var deltaY = updateFactor * (MaxY - MinY);
            //var deltaZ = updateFactor * (MaxZ - MinZ);

            //MinX = MinX - deltaX;
            //MinY = MinY - deltaY;
            //MinZ = MinZ - deltaZ;

            //MaxX = MaxX + deltaX;
            //MaxY = MaxY + deltaY;
            //MaxZ = MaxZ + deltaZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D UpdateSizeByFactor(IFloat64Tuple3D updateFactor)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);
            var midZ = 0.5d * (MaxZ + MinZ);

            MinX = (MinX - midX) * updateFactor.X + midX;
            MinY = (MinY - midY) * updateFactor.Y + midY;
            MinZ = (MinZ - midZ) * updateFactor.Z + midZ;

            MaxX = (MaxX - midX) * updateFactor.X + midX;
            MaxY = (MaxY - midY) * updateFactor.Y + midY;
            MaxZ = (MaxZ - midZ) * updateFactor.Z + midZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D UpdateSizeByFactor(double updateFactorX, double updateFactorY, double updateFactorZ)
        {
            var midX = 0.5d * (MaxX + MinX);
            var midY = 0.5d * (MaxY + MinY);
            var midZ = 0.5d * (MaxZ + MinZ);

            MinX = (MinX - midX) * updateFactorX + midX;
            MinY = (MinY - midY) * updateFactorY + midY;
            MinZ = (MinZ - midZ) * updateFactorZ + midZ;

            MaxX = (MaxX - midX) * updateFactorX + midX;
            MaxY = (MaxY - midY) * updateFactorY + midY;
            MaxZ = (MaxZ - midZ) * updateFactorZ + midZ;

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D ExpandToInfinite()
        {
            MinX = double.NegativeInfinity;
            MinY = double.NegativeInfinity;
            MinZ = double.NegativeInfinity;

            MaxX = double.PositiveInfinity;
            MaxY = double.PositiveInfinity;
            MaxZ = double.PositiveInfinity;

            return this;
        }


        public MutableBoundingBox3D ExpandToInclude(double pointX, double pointY, double pointZ)
        {
            if (MinX > pointX) MinX = pointX;
            if (MinY > pointY) MinY = pointY;
            if (MinZ > pointZ) MinZ = pointZ;

            if (MaxX < pointX) MaxX = pointX;
            if (MaxY < pointY) MaxY = pointY;
            if (MaxZ < pointZ) MaxZ = pointZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(IFloat64Tuple3D point)
        {
            if (MinX > point.X) MinX = point.X;
            if (MinY > point.Y) MinY = point.Y;
            if (MinZ > point.Z) MinZ = point.Z;

            if (MaxX < point.X) MaxX = point.X;
            if (MaxY < point.Y) MaxY = point.Y;
            if (MaxZ < point.Z) MaxZ = point.Z;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(params IFloat64Tuple3D[] pointsList)
        {
            foreach (var point in pointsList)
            {
                if (MinX > point.X) MinX = point.X;
                if (MinY > point.Y) MinY = point.Y;
                if (MinZ > point.Z) MinZ = point.Z;

                if (MaxX < point.X) MaxX = point.X;
                if (MaxY < point.Y) MaxY = point.Y;
                if (MaxZ < point.Z) MaxZ = point.Z;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(IEnumerable<IFloat64Tuple3D> pointsList)
        {
            foreach (var point in pointsList)
            {
                if (MinX > point.X) MinX = point.X;
                if (MinY > point.Y) MinY = point.Y;
                if (MinZ > point.Z) MinZ = point.Z;

                if (MaxX < point.X) MaxX = point.X;
                if (MaxY < point.Y) MaxY = point.Y;
                if (MaxZ < point.Z) MaxZ = point.Z;
            }

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D ExpandToInclude(IBoundingBox3D boundingBox)
        {
            if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
            if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
            if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

            if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
            if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(params IBoundingBox3D[] boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
                if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
                if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(IEnumerable<IBoundingBox3D> boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
                if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
                if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;
            }

            ValidateValues();

            return this;
        }


        public MutableBoundingBox3D ExpandToInclude(IFiniteGeometricShape3D geometricObject)
        {
            var boundingBox = geometricObject.GetBoundingBox();

            if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
            if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
            if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

            if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
            if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
            if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(params IFiniteGeometricShape3D[] geometricObjectsList)
        {
            foreach (var geometricObject in geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
                if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
                if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;
            }

            ValidateValues();

            return this;
        }

        public MutableBoundingBox3D ExpandToInclude(IEnumerable<IFiniteGeometricShape3D> geometricObjectsList)
        {
            foreach (var geometricObject in geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                if (MinX > boundingBox.MinX) MinX = boundingBox.MinX;
                if (MinY > boundingBox.MinY) MinY = boundingBox.MinY;
                if (MinZ > boundingBox.MinZ) MinZ = boundingBox.MinZ;

                if (MaxX < boundingBox.MaxX) MaxX = boundingBox.MaxX;
                if (MaxY < boundingBox.MaxY) MaxY = boundingBox.MaxY;
                if (MaxZ < boundingBox.MaxZ) MaxZ = boundingBox.MaxZ;
            }

            ValidateValues();

            return this;
        }
        

        public IBorderSurface3D MapUsing(IAffineMap3D affineMap)
        {
            var oldSide001 = new Float64Tuple3D(MaxX - MinX, 0, 0);
            var oldSide010 = new Float64Tuple3D(0, MaxY - MinY, 0);
            var oldSide100 = new Float64Tuple3D(0, 0, MaxZ - MinZ);

            var newSide001 = affineMap.MapVector(oldSide001).ToTuple3D();
            var newSide010 = affineMap.MapVector(oldSide010).ToTuple3D();
            var newSide100 = affineMap.MapVector(oldSide100).ToTuple3D();

            var newCorner000 = affineMap.MapPoint(this.GetMinCorner()).ToTuple3D();
            var newCorner001 = newCorner000 + newSide001;
            var newCorner010 = newCorner000 + newSide010;
            var newCorner011 = newCorner001 + newSide010;
            var newCorner100 = newCorner000 + newSide100;
            var newCorner101 = newCorner100 + newSide001;
            var newCorner110 = newCorner100 + newSide010;
            var newCorner111 = newCorner110 + newSide001;

            var newBox = CreateFromPoints(
                newCorner000, 
                newCorner001,
                newCorner010,
                newCorner011,
                newCorner100,
                newCorner101,
                newCorner110,
                newCorner111
            );

            return newBox;
        }

        public BoundingBox3D GetBoundingBox()
        {
            return new BoundingBox3D(this);
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            return new MutableBoundingBox3D(this);
        }
    }
}
