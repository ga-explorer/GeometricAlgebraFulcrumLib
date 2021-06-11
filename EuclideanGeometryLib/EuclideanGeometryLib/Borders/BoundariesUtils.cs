using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.Borders.Space1D;
using EuclideanGeometryLib.Borders.Space1D.Immutable;
using EuclideanGeometryLib.Borders.Space2D;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;
using EuclideanGeometryLib.Borders.Space3D;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace EuclideanGeometryLib.Borders
{
    public static class BordersUtils
    {

        public static bool IsFinite(this IBoundingBox1D boundingBox)
            => !double.IsInfinity(boundingBox.MinValue) &&
               !double.IsInfinity(boundingBox.MaxValue);

        public static double GetMidValue(this IBoundingBox1D boundingBox)
            => 0.5 * (boundingBox.MinValue + boundingBox.MaxValue);

        public static double GetLength(this IBoundingBox1D boundingBox)
            => (boundingBox.MaxValue - boundingBox.MinValue);

        public static double GetValueOffset(this IBoundingBox1D boundingBox, double value)
        {
            return (value - boundingBox.MinValue) / (boundingBox.MaxValue - boundingBox.MinValue);
        }

        public static BoundingBox1D[] GetSubdivisions(this IBoundingBox1D boundingBox, int valueDivisions)
        {
            var length = boundingBox.GetLength() / valueDivisions;

            var minValues =
                Enumerable
                    .Range(0, valueDivisions)
                    .Select(i => i * length + boundingBox.MinValue)
                    .ToArray();

            var maxValues =
                minValues
                    .Select(v => v + length)
                    .ToArray();

            var divisions = new BoundingBox1D[valueDivisions];

            for (var i = 0; i < valueDivisions; i++)
                divisions[i] = new BoundingBox1D(minValues[i], maxValues[i]);

            return divisions;
        }


        public static bool Contains(this IBoundingBox1D boundingBox, double value, bool useMargins = false)
        {
            if (useMargins)
                return
                    (value >= boundingBox.MinValue - Float64Utils.DefaultAccuracyPositive) &&
                    (value <= boundingBox.MaxValue + Float64Utils.DefaultAccuracyPositive);

            return
                (value >= boundingBox.MinValue) &&
                (value <= boundingBox.MaxValue);
        }

        public static bool Contains(this IBoundingBox1D boundingBox, IBoundingBox1D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MinValue >= boundingBox.MinValue - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxValue <= boundingBox.MaxValue + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MinValue >= boundingBox.MinValue) &&
                (box.MaxValue <= boundingBox.MaxValue);
        }

        public static bool ContainsUpperExclusive(this IBoundingBox1D boundingBox, double value, bool useMargins = false)
        {
            if (useMargins)
                return
                    (value >= boundingBox.MinValue - Float64Utils.DefaultAccuracyPositive) &&
                    (value < boundingBox.MaxValue + Float64Utils.DefaultAccuracyPositive);

            return
                (value >= boundingBox.MinValue) &&
                (value < boundingBox.MaxValue);
        }

        public static bool Overlaps(this IBoundingBox1D boundingBox, IBoundingBox1D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MaxValue >= boundingBox.MinValue - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinValue <= boundingBox.MaxValue + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MaxValue >= boundingBox.MinValue) &&
                (box.MinValue <= boundingBox.MaxValue);
        }


        public static Tuple2D GetCorner(this IBoundingBox2D boundingBox, bool maxCorner)
            => maxCorner
                ? new Tuple2D(boundingBox.MaxX, boundingBox.MaxY)
                : new Tuple2D(boundingBox.MinX, boundingBox.MinY);

        public static double GetMidX(this IBoundingBox2D boundingBox)
            => 0.5 * (boundingBox.MinX + boundingBox.MaxX);

        public static double GetMidY(this IBoundingBox2D boundingBox)
            => 0.5 * (boundingBox.MinY + boundingBox.MaxY);

        public static double GetLengthX(this IBoundingBox2D boundingBox)
            => (boundingBox.MaxX - boundingBox.MinX);

        public static double GetLengthY(this IBoundingBox2D boundingBox)
            => (boundingBox.MaxY - boundingBox.MinY);

        public static BoundingBox1D GetRangeX(this IBoundingBox2D boundingBox)
            => new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

        public static BoundingBox1D GetRangeY(this IBoundingBox2D boundingBox)
            => new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

        public static int GetLongestSideIndex(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) >= (boundingBox.MaxY - boundingBox.MinY))
                ? 0 : 1;

        public static double GetLongestSideMinValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) >= (boundingBox.MaxY - boundingBox.MinY))
                ? boundingBox.MinX : boundingBox.MinY;

        public static double GetLongestSideMaxValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) >= (boundingBox.MaxY - boundingBox.MinY))
                ? boundingBox.MaxX : boundingBox.MaxY;

        public static double GetLongestSideMidValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) >= (boundingBox.MaxY - boundingBox.MinY))
                ? 0.5 * (boundingBox.MinX + boundingBox.MaxX)
                : 0.5 * (boundingBox.MinY + boundingBox.MaxY);

        public static double GetLongestSideLength(this IBoundingBox2D boundingBox)
            => Math.Max(
                boundingBox.MaxX - boundingBox.MinX,
                boundingBox.MaxY - boundingBox.MinY
            );

        public static BoundingBox1D GetLongestSideRange(this IBoundingBox2D boundingBox) 
            => (boundingBox.MaxX - boundingBox.MinX) >= (boundingBox.MaxY - boundingBox.MinY)
                ? new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX)
                : new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

        public static double GetShortestSideMinValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) <= (boundingBox.MaxY - boundingBox.MinY))
                ? boundingBox.MinX : boundingBox.MinY;

        public static double GetShortestSideMaxValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) <= (boundingBox.MaxY - boundingBox.MinY))
                ? boundingBox.MaxX : boundingBox.MaxY;

        public static double GetShortestSideMidValue(this IBoundingBox2D boundingBox)
            => ((boundingBox.MaxX - boundingBox.MinX) <= (boundingBox.MaxY - boundingBox.MinY))
                ? 0.5 * (boundingBox.MinX + boundingBox.MaxX)
                : 0.5 * (boundingBox.MinY + boundingBox.MaxY);

        public static double GetShortestSideLength(this IBoundingBox2D boundingBox)
            => Math.Min(
                boundingBox.MaxX - boundingBox.MinX, 
                boundingBox.MaxY - boundingBox.MinY
            );

        public static BoundingBox1D GetShortestSideRange(this IBoundingBox2D boundingBox)
            => (boundingBox.MaxX - boundingBox.MinX) <= (boundingBox.MaxY - boundingBox.MinY)
                ? new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX)
                : new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

        public static Tuple2D GetMinCorner(this IBoundingBox2D boundingBox)
            => new Tuple2D(boundingBox.MinX, boundingBox.MinY);

        public static Tuple2D GetMaxCorner(this IBoundingBox2D boundingBox)
            => new Tuple2D(boundingBox.MaxX, boundingBox.MaxY);

        public static Tuple2D GetMidPoint(this IBoundingBox2D boundingBox)
            => new Tuple2D(
                0.5 * (boundingBox.MinX + boundingBox.MaxX),
                0.5 * (boundingBox.MinY + boundingBox.MaxY)
            );

        public static Tuple2D GetSideLengths(this IBoundingBox2D boundingBox)
            => new Tuple2D(
                boundingBox.MaxX - boundingBox.MinX,
                boundingBox.MaxY - boundingBox.MinY
            );

        public static Tuple2D GetDiagonalVector(this IBoundingBox2D boundingBox)
            => new Tuple2D(
                boundingBox.MaxX - boundingBox.MinX,
                boundingBox.MaxY - boundingBox.MinY
            );

        public static double GetDiagonalLength(this IBoundingBox2D boundingBox)
        {
            var x = boundingBox.MaxX - boundingBox.MinX;
            var y = boundingBox.MaxY - boundingBox.MinY;

            return Math.Sqrt(x * x + y * y);
        }

        public static double GetBorderLength(this IBoundingBox2D boundingBox)
        {
            var dx = boundingBox.MaxX - boundingBox.MinX;
            var dy = boundingBox.MaxY - boundingBox.MinY;

            return 2.0d * (dx + dy);
        }

        public static double GetArea(this IBoundingBox2D boundingBox)
            => (boundingBox.MaxX - boundingBox.MinX) * 
               (boundingBox.MaxY - boundingBox.MinY);

        public static BoundingSphere2D GetBoundingSphere(this IBoundingBox2D boundingBox)
        {
            var cX = 0.5 * (boundingBox.MinX + boundingBox.MaxX);
            var cY = 0.5 * (boundingBox.MinY + boundingBox.MaxY);
            var r = 0.0d;

            if (
                cX >= boundingBox.MinX && cX <= boundingBox.MaxX &&
                cY >= boundingBox.MinY && cY <= boundingBox.MaxY
            )
                r = Math.Sqrt(
                    (boundingBox.MaxX - cX) * (boundingBox.MaxX - cX) +
                    (boundingBox.MaxY - cY) * (boundingBox.MaxY - cY)
                );

            return BoundingSphere2D.Create(cX, cY, r);
        }

        /// <summary>
        /// Given an index between 0 and 3 this returnes one corner of the box
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="cornerIndex"></param>
        /// <returns></returns>
        public static Tuple2D GetCorner(this IBoundingBox2D boundingBox, int cornerIndex)
        {
            cornerIndex = cornerIndex.Mod(4);

            if (cornerIndex == 0)
                return new Tuple2D(boundingBox.MinX, boundingBox.MinY);

            if (cornerIndex == 1)
                return new Tuple2D(boundingBox.MaxX, boundingBox.MinY);

            if (cornerIndex == 2)
                return new Tuple2D(boundingBox.MinX, boundingBox.MaxY);

            return new Tuple2D(boundingBox.MaxX, boundingBox.MaxY);
        }

        public static double GetSideMinValue(this IBoundingBox2D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(2);

            if (axisIndex == 0)
                return boundingBox.MinX;

            return boundingBox.MinY;
        }

        public static double GetSideMaxValue(this IBoundingBox2D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(2);

            if (axisIndex == 0)
                return boundingBox.MaxX;

            return boundingBox.MaxY;
        }

        public static double GetSideMidValue(this IBoundingBox2D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(2);

            if (axisIndex == 0)
                return 0.5 * (boundingBox.MinX + boundingBox.MaxX);

            return 0.5 * (boundingBox.MinY + boundingBox.MaxY);
        }

        public static double GetSideLength(this IBoundingBox2D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(2);

            if (axisIndex == 0)
                return boundingBox.MaxX - boundingBox.MinX;

            return boundingBox.MaxY - boundingBox.MinY;
        }

        public static BoundingBox1D GetSideRange(this IBoundingBox2D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(2);

            if (axisIndex == 0)
                return new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

            return new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);
        }

        public static Tuple2D GetSideDirection(this MutableBoundingBox2D boundingBox, int cornerIndex1, int cornerIndex2)
        {
            var point1 = boundingBox.GetCorner(cornerIndex1);
            var point2 = boundingBox.GetCorner(cornerIndex2);

            return new Tuple2D(
                point2.X - point1.X,
                point2.Y - point1.Y
            );
        }

        public static Tuple2D GetPointOffset(this IBoundingBox2D boundingBox, ITuple2D point)
        {
            var oX = point.X - boundingBox.MinX;
            var oY = point.Y - boundingBox.MinY;

            if (boundingBox.MaxX > boundingBox.MinX)
                oX = oX / (boundingBox.MaxX - boundingBox.MinX);

            if (boundingBox.MaxY > boundingBox.MinY)
                oY = oY / (boundingBox.MaxY - boundingBox.MinY);

            return new Tuple2D(oX, oY);
        }

        public static bool Contains(this IBoundingBox2D boundingBox, ITuple2D point, bool useMargins = false)
        {
            if (useMargins)
                return
                    (point.X >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (point.X <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive);

            return
                (point.X >= boundingBox.MinX) &&
                (point.X <= boundingBox.MaxX) &&
                (point.Y >= boundingBox.MinY) &&
                (point.Y <= boundingBox.MaxY);
        }

        public static bool Contains(this IBoundingBox2D boundingBox, double pointX, double pointY, bool useMargins = false)
        {
            if (useMargins)
                return
                    (pointX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (pointX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (pointY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (pointY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive);

            return
                (pointX >= boundingBox.MinX) &&
                (pointX <= boundingBox.MaxX) &&
                (pointY >= boundingBox.MinY) &&
                (pointY <= boundingBox.MaxY);
        }

        public static bool Contains(this IBoundingBox2D boundingBox, IBoundingBox2D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MinX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MinX >= boundingBox.MinX) &&
                (box.MaxX <= boundingBox.MaxX) &&
                (box.MinY >= boundingBox.MinY) &&
                (box.MaxY <= boundingBox.MaxY);
        }

        public static bool ContainsUpperExclusive(this IBoundingBox2D boundingBox, ITuple2D point, bool useMargins = false)
        {
            if (useMargins)
                return
                    (point.X >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (point.X < boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y < boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive);

            return
                (point.X >= boundingBox.MinX) &&
                (point.X < boundingBox.MaxX) &&
                (point.Y >= boundingBox.MinY) &&
                (point.Y < boundingBox.MaxY);
        }

        public static bool Overlaps(this IBoundingBox2D boundingBox, IBoundingBox2D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MaxX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MaxX >= boundingBox.MinX) &&
                (box.MinX <= boundingBox.MaxX) &&
                (box.MaxY >= boundingBox.MinY) &&
                (box.MinY <= boundingBox.MaxY);
        }

        public static BoundingBox2D[,] GetSubdivisions(this IBoundingBox2D boundingBox, int xDivisions, int yDivisions)
        {
            var xLength = boundingBox.GetLengthX() / xDivisions;
            var yLength = boundingBox.GetLengthY() / yDivisions;

            var minXValues = 
                Enumerable
                    .Range(0, xDivisions)
                    .Select(i => i * xLength + boundingBox.MinX)
                    .ToArray();

            var maxXValues = 
                minXValues
                    .Select(v => v + xLength)
                    .ToArray();

            var minYValues = 
                Enumerable
                    .Range(0, yDivisions)
                    .Select(i => i * yLength + boundingBox.MinY)
                    .ToArray();

            var maxYValues = 
                minYValues
                    .Select(v => v + yLength)
                    .ToArray();

            var divisions = new BoundingBox2D[xDivisions, yDivisions];

            for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
                divisions[ix, iy] =
                    BoundingBox2D.Create(
                        minXValues[ix],
                        minYValues[iy],
                        maxXValues[ix],
                        maxYValues[iy]
                    );

            return divisions;

        }

        public static LineSegment2D ClipLine(this IBoundingBox2D boundingBox, ILine2D line)
        {
            var tMin = double.NegativeInfinity;
            var tMax = double.PositiveInfinity;

            //Compute intersection parameters of ray with Y slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionX;
                var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
                var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionY;
                var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
                var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            return new LineSegment2D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY
            );
        }

        public static LineSegment2D ClipLine(this IBoundingBox2D boundingBox, ILine2D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
        {
            var tMin = lineParamMinValue;
            var tMax = lineParamMaxValue;

            //Compute intersection parameters of ray with Y slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionX;
                var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
                var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionY;
                var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
                var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            return new LineSegment2D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY
            );
        }


        public static Tuple3D GetCorner(this IBoundingBox3D boundingBox, bool maxCorner)
            => maxCorner
                ? new Tuple3D(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MaxZ)
                : new Tuple3D(boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ);

        public static double GetMidX(this IBoundingBox3D boundingBox)
            => 0.5 * (boundingBox.MinX + boundingBox.MaxX);

        public static double GetMidY(this IBoundingBox3D boundingBox)
            => 0.5 * (boundingBox.MinY + boundingBox.MaxY);

        public static double GetMidZ(this IBoundingBox3D boundingBox)
            => 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);

        public static double GetLengthX(this IBoundingBox3D boundingBox)
            => (boundingBox.MaxX - boundingBox.MinX);

        public static double GetLengthY(this IBoundingBox3D boundingBox)
            => (boundingBox.MaxY - boundingBox.MinY);

        public static double GetLengthZ(this IBoundingBox3D boundingBox)
            => (boundingBox.MaxZ - boundingBox.MinZ);

        public static BoundingBox1D GetRangeX(this IBoundingBox3D boundingBox)
            => new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

        public static BoundingBox1D GetRangeY(this IBoundingBox3D boundingBox)
            => new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

        public static BoundingBox1D GetRangeZ(this IBoundingBox3D boundingBox)
            => new BoundingBox1D(boundingBox.MinZ, boundingBox.MaxZ);

        public static BoundingBox2D GetRangeXy(this IBoundingBox3D boundingBox)
            => new BoundingBox2D(
                boundingBox.MinX,
                boundingBox.MinY,
                boundingBox.MaxX,
                boundingBox.MaxY
            );

        public static BoundingBox2D GetRangeYz(this IBoundingBox3D boundingBox)
            => new BoundingBox2D(
                boundingBox.MinY,
                boundingBox.MinZ,
                boundingBox.MaxY,
                boundingBox.MaxZ
            );

        public static BoundingBox2D GetRangeZx(this IBoundingBox3D boundingBox)
            => new BoundingBox2D(
                boundingBox.MinZ,
                boundingBox.MinX,
                boundingBox.MaxZ,
                boundingBox.MaxX
            );

        public static BoundingBox2D GetBoundingBox2D<T>(this IEnumerable<T> objectsList)
            where T : IFiniteGeometricShape2D
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var geometricObject in objectsList)
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

        public static BoundingBox3D GetBoundingBox3D<T>(this IEnumerable<T> objectsList)
            where T : IFiniteGeometricShape3D
        {
            var result = new MutableBoundingBox3D();

            var flag = false;
            foreach (var geometricObject in objectsList)
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

        public static Tuple2D GetPointAt(this IBoundingBox2D boundingBox, double tx, double ty)
            => new Tuple2D(
                (1.0d - tx) * boundingBox.MinX + tx * boundingBox.MaxX,
                (1.0d - ty) * boundingBox.MinY + ty * boundingBox.MaxY
            );

        public static Tuple2D GetPointAt(this IBoundingBox2D boundingBox, ITuple2D tVector)
            => new Tuple2D(
                (1.0d - tVector.X) * boundingBox.MinX + tVector.X * boundingBox.MaxX,
                (1.0d - tVector.Y) * boundingBox.MinY + tVector.Y * boundingBox.MaxY
            );


        public static int GetLongestSideIndex(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? 0 : 2)
                : (lengthY >= lengthZ ? 1 : 2);
        }

        public static double GetLongestSideMinValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? boundingBox.MinX : boundingBox.MinZ)
                : (lengthY >= lengthZ ? boundingBox.MinY : boundingBox.MinZ);
        }

        public static double GetLongestSideMaxValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? boundingBox.MaxX : boundingBox.MaxZ)
                : (lengthY >= lengthZ ? boundingBox.MaxY : boundingBox.MaxZ);
        }

        public static double GetLongestSideMidValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? boundingBox.GetMidX() : boundingBox.GetMidZ())
                : (lengthY >= lengthZ ? boundingBox.GetMidY() : boundingBox.GetMidZ());
        }

        public static double GetLongestSideLength(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? lengthX : lengthZ)
                : (lengthY >= lengthZ ? lengthY : lengthZ);
        }

        public static BoundingBox1D GetLongestSideRange(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            var axisIndex = (lengthX >= lengthY)
                ? (lengthX >= lengthZ ? 0 : 2)
                : (lengthY >= lengthZ ? 1 : 2);

            if (axisIndex == 0)
                return new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

            if (axisIndex == 1)
                return new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

            return new BoundingBox1D(boundingBox.MinZ, boundingBox.MaxZ);
        }

        public static int GetShortestSideIndex(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? 0 : 2)
                : (lengthY <= lengthZ ? 1 : 2);
        }

        public static double GetShortestSideMinValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? boundingBox.MinX : boundingBox.MinZ)
                : (lengthY <= lengthZ ? boundingBox.MinY : boundingBox.MinZ);
        }

        public static double GetShortestSideMaxValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? boundingBox.MaxX : boundingBox.MaxZ)
                : (lengthY <= lengthZ ? boundingBox.MaxY : boundingBox.MaxZ);
        }

        public static double GetShortestSideMidValue(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? boundingBox.GetMidX() : boundingBox.GetMidZ())
                : (lengthY <= lengthZ ? boundingBox.GetMidY() : boundingBox.GetMidZ());
        }

        public static double GetShortestSideLength(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            return (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? lengthX : lengthZ)
                : (lengthY <= lengthZ ? lengthY : lengthZ);
        }

        public static BoundingBox1D GetShortestSideRange(this IBoundingBox3D boundingBox)
        {
            var lengthX = boundingBox.MaxX - boundingBox.MinX;
            var lengthY = boundingBox.MaxY - boundingBox.MinY;
            var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

            var axisIndex = (lengthX <= lengthY)
                ? (lengthX <= lengthZ ? 0 : 2)
                : (lengthY <= lengthZ ? 1 : 2);

            if (axisIndex == 0)
                return new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

            if (axisIndex == 1)
                return new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

            return new BoundingBox1D(boundingBox.MinZ, boundingBox.MaxZ);
        }

        public static Tuple3D GetMinCorner(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                boundingBox.MinX, 
                boundingBox.MinY,
                boundingBox.MinZ
            );

        public static Tuple3D GetMaxCorner(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                boundingBox.MaxX, 
                boundingBox.MaxY,
                boundingBox.MaxZ
            );

        public static Tuple3D GetMidPoint(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                0.5 * (boundingBox.MinX + boundingBox.MaxX),
                0.5 * (boundingBox.MinY + boundingBox.MaxY),
                0.5 * (boundingBox.MinZ + boundingBox.MaxZ)
            );

        public static Tuple3D GetSideLengths(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                boundingBox.MaxX - boundingBox.MinX,
                boundingBox.MaxY - boundingBox.MinY,
                boundingBox.MaxZ - boundingBox.MinZ
            );

        public static Tuple3D GetSideHalfLengths(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                0.5 * (boundingBox.MaxX - boundingBox.MinX),
                0.5 * (boundingBox.MaxY - boundingBox.MinY),
                0.5 * (boundingBox.MaxZ - boundingBox.MinZ)
            );

        public static Tuple3D GetDiagonalVector(this IBoundingBox3D boundingBox)
            => new Tuple3D(
                boundingBox.MaxX - boundingBox.MinX,
                boundingBox.MaxY - boundingBox.MinY,
                boundingBox.MaxZ - boundingBox.MinZ
            );

        public static double GetDiagonalLength(this IBoundingBox3D boundingBox)
        {
            var x = boundingBox.MaxX - boundingBox.MinX;
            var y = boundingBox.MaxY - boundingBox.MinY;
            var z = boundingBox.MaxZ - boundingBox.MinZ;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double GetSurfaceArea(this IBoundingBox3D boundingBox)
        {
            var dx = boundingBox.MaxX - boundingBox.MinX;
            var dy = boundingBox.MaxY - boundingBox.MinY;
            var dz = boundingBox.MaxZ - boundingBox.MinZ;

            return 2.0d * (dx * dy + dy * dz + dz * dx);
        }

        public static double GetVolume(this IBoundingBox3D boundingBox)
            => (boundingBox.MaxX - boundingBox.MinX) *
               (boundingBox.MaxY - boundingBox.MinY) *
               (boundingBox.MaxZ - boundingBox.MinZ);

        public static BoundingSphere3D GetBoundingSphere(this IBoundingBox3D boundingBox)
        {
            var cX = 0.5 * (boundingBox.MinX + boundingBox.MaxX);
            var cY = 0.5 * (boundingBox.MinY + boundingBox.MaxY);
            var cZ = 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);
            var r = 0.0d;

            if (
                cX >= boundingBox.MinX && cX <= boundingBox.MaxX &&
                cY >= boundingBox.MinY && cY <= boundingBox.MaxY &&
                cZ >= boundingBox.MinZ && cZ <= boundingBox.MaxZ
            )
                r = Math.Sqrt(
                    (boundingBox.MaxX - cX) * (boundingBox.MaxX - cX) +
                    (boundingBox.MaxY - cY) * (boundingBox.MaxY - cY) +
                    (boundingBox.MaxZ - cZ) * (boundingBox.MaxZ - cZ)
                );

            return new BoundingSphere3D(cX, cY, cZ, r);
        }

        /// <summary>
        /// Given an index between 0 and 7 this returnes one corner of the box
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="cornerIndex"></param>
        /// <returns></returns>
        public static Tuple3D GetCorner(this IBoundingBox3D boundingBox, int cornerIndex)
        {
            cornerIndex = cornerIndex.Mod(8);

            if (cornerIndex == 0)
                return new Tuple3D(boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ);

            if (cornerIndex == 1)
                return new Tuple3D(boundingBox.MaxX, boundingBox.MinY, boundingBox.MinZ);

            if (cornerIndex == 2)
                return new Tuple3D(boundingBox.MinX, boundingBox.MaxY, boundingBox.MinZ);

            if (cornerIndex == 3)
                return new Tuple3D(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MinZ);

            if (cornerIndex == 4)
                return new Tuple3D(boundingBox.MinX, boundingBox.MinY, boundingBox.MaxZ);

            if (cornerIndex == 5)
                return new Tuple3D(boundingBox.MaxX, boundingBox.MinY, boundingBox.MaxZ);

            if (cornerIndex == 6)
                return new Tuple3D(boundingBox.MinX, boundingBox.MaxY, boundingBox.MaxZ);

            return new Tuple3D(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MaxZ);
        }

        public static double GetSideMinValue(this IBoundingBox3D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(3);

            if (axisIndex == 0)
                return boundingBox.MinX;

            if (axisIndex == 1)
                return boundingBox.MinY;

            return boundingBox.MinZ;
        }

        public static double GetSideMaxValue(this IBoundingBox3D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(3);

            if (axisIndex == 0)
                return boundingBox.MaxX;

            if (axisIndex == 1)
                return boundingBox.MaxY;

            return boundingBox.MaxZ;
        }

        public static double GetSideMidValue(this IBoundingBox3D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(3);

            if (axisIndex == 0)
                return 0.5 * (boundingBox.MinX + boundingBox.MaxX);

            if (axisIndex == 1)
                return 0.5 * (boundingBox.MinY + boundingBox.MaxY);

            return 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);
        }

        public static double GetSideLength(this IBoundingBox3D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(3);

            if (axisIndex == 0)
                return boundingBox.MaxX - boundingBox.MinX;

            if (axisIndex == 1)
                return boundingBox.MaxY - boundingBox.MinY;

            return boundingBox.MaxZ - boundingBox.MinZ;
        }

        public static BoundingBox1D GetSideRange(this IBoundingBox3D boundingBox, int axisIndex)
        {
            axisIndex = axisIndex.Mod(3);

            if (axisIndex == 0)
                return new BoundingBox1D(boundingBox.MinX, boundingBox.MaxX);

            if (axisIndex == 1)
                return new BoundingBox1D(boundingBox.MinY, boundingBox.MaxY);

            return new BoundingBox1D(boundingBox.MinZ, boundingBox.MaxZ);
        }

        public static Tuple3D GetSideDirection(this IBoundingBox3D boundingBox, int cornerIndex1, int cornerIndex2)
        {
            var point1 = boundingBox.GetCorner(cornerIndex1);
            var point2 = boundingBox.GetCorner(cornerIndex2);

            return new Tuple3D(
                point2.X - point1.X,
                point2.Y - point1.Y,
                point2.Z - point1.Z
            );
        }

        public static Tuple3D GetPointOffset(this IBoundingBox3D boundingBox, ITuple3D point)
        {
            var oX = point.X - boundingBox.MinX;
            var oY = point.Y - boundingBox.MinY;
            var oZ = point.Z - boundingBox.MinZ;

            if (boundingBox.MaxX > boundingBox.MinX)
                oX = oX / (boundingBox.MaxX - boundingBox.MinX);

            if (boundingBox.MaxY > boundingBox.MinY)
                oY = oY / (boundingBox.MaxY - boundingBox.MinY);

            if (boundingBox.MaxZ > boundingBox.MinZ)
                oZ = oZ / (boundingBox.MaxZ - boundingBox.MinZ);

            return new Tuple3D(oX, oY, oZ);
        }

        public static bool Contains(this IBoundingBox3D boundingBox, ITuple3D point, bool useMargins = false)
        {
            if (useMargins)
                return
                    (point.X >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (point.X <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Z >= boundingBox.MinZ - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Z <= boundingBox.MaxZ + Float64Utils.DefaultAccuracyPositive);

            return
                (point.X >= boundingBox.MinX) &&
                (point.X <= boundingBox.MaxX) &&
                (point.Y >= boundingBox.MinY) &&
                (point.Y <= boundingBox.MaxY) &&
                (point.Z >= boundingBox.MinY) &&
                (point.Z <= boundingBox.MaxY);
        }

        public static bool Contains(this IBoundingBox3D boundingBox, double pointX, double pointY, double pointZ, bool useMargins = false)
        {
            if (useMargins)
                return
                    (pointX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (pointX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (pointY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (pointY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive) &&
                    (pointZ >= boundingBox.MinZ - Float64Utils.DefaultAccuracyPositive) &&
                    (pointZ <= boundingBox.MaxZ + Float64Utils.DefaultAccuracyPositive);

            return
                (pointX >= boundingBox.MinX) &&
                (pointX <= boundingBox.MaxX) &&
                (pointY >= boundingBox.MinY) &&
                (pointY <= boundingBox.MaxY) &&
                (pointZ >= boundingBox.MinZ) &&
                (pointZ <= boundingBox.MaxZ);
        }

        public static bool Contains(this IBoundingBox3D boundingBox, IBoundingBox3D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MinX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinZ >= boundingBox.MinZ - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxZ <= boundingBox.MaxZ + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MinX >= boundingBox.MinX) &&
                (box.MaxX <= boundingBox.MaxX) &&
                (box.MinY >= boundingBox.MinY) &&
                (box.MaxY <= boundingBox.MaxY) &&
                (box.MinZ >= boundingBox.MinZ) &&
                (box.MaxZ <= boundingBox.MaxZ);
        }

        public static bool ContainsUpperExclusive(this IBoundingBox3D boundingBox, ITuple3D point, bool useMargins = false)
        {
            if (useMargins)
                return
                    (point.X >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (point.X < boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y < boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y >= boundingBox.MinZ - Float64Utils.DefaultAccuracyPositive) &&
                    (point.Y < boundingBox.MaxZ + Float64Utils.DefaultAccuracyPositive);

            return
                (point.X >= boundingBox.MinX) &&
                (point.X < boundingBox.MaxX) &&
                (point.Y >= boundingBox.MinY) &&
                (point.Y < boundingBox.MaxY) &&
                (point.Z >= boundingBox.MinZ) &&
                (point.Z < boundingBox.MaxZ);
        }

        public static bool Overlaps(this IBoundingBox3D boundingBox, IBoundingBox3D box, bool useMargins = false)
        {
            if (useMargins)
                return
                    (box.MaxX >= boundingBox.MinX - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinX <= boundingBox.MaxX + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxY >= boundingBox.MinY - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinY <= boundingBox.MaxY + Float64Utils.DefaultAccuracyPositive) &&
                    (box.MaxZ >= boundingBox.MinZ - Float64Utils.DefaultAccuracyPositive) &&
                    (box.MinZ <= boundingBox.MaxZ + Float64Utils.DefaultAccuracyPositive);

            return
                (box.MaxX >= boundingBox.MinX) &&
                (box.MinX <= boundingBox.MaxX) &&
                (box.MaxY >= boundingBox.MinY) &&
                (box.MinY <= boundingBox.MaxY) &&
                (box.MaxZ >= boundingBox.MinZ) &&
                (box.MinZ <= boundingBox.MaxZ);
        }

        public static BoundingBox3D[,,] GetSubdivisions(this IBoundingBox3D boundingBox, int xDivisions, int yDivisions, int zDivisions)
        {
            var xLength = boundingBox.GetLengthX() / xDivisions;
            var yLength = boundingBox.GetLengthY() / yDivisions;
            var zLength = boundingBox.GetLengthZ() / zDivisions;

            var minXValues = 
                Enumerable
                    .Range(0, xDivisions)
                    .Select(i => i * xLength + boundingBox.MinX)
                    .ToArray();

            var maxXValues = minXValues.Select(v => v + xLength).ToArray();

            var minYValues = 
                Enumerable
                    .Range(0, yDivisions)
                    .Select(i => i * yLength + boundingBox.MinY)
                    .ToArray();

            var maxYValues = minYValues.Select(v => v + yLength).ToArray();

            var minZValues = 
                Enumerable
                    .Range(0, zDivisions)
                    .Select(i => i * zLength + boundingBox.MinZ)
                    .ToArray();

            var maxZValues = minZValues.Select(v => v + zLength).ToArray();

            var divisions = new BoundingBox3D[xDivisions, yDivisions, zDivisions];

            for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
            for (var iz = 0; iz < zDivisions; iz++)
                divisions[ix, iy, iz] =
                    BoundingBox3D.CreateFromPoints(
                        minXValues[ix],
                        minYValues[iy],
                        minZValues[iz],
                        maxXValues[ix],
                        maxYValues[iy],
                        maxZValues[iz]
                    );

            return divisions;
        }

        public static LineSegment3D ClipLine(this IBoundingBox3D boundingBox, ILine3D line)
        {
            var tMin = double.NegativeInfinity;
            var tMax = double.PositiveInfinity;

            //Compute intersection parameters of ray with Y-Z slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionX;
                var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
                var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X-Z slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionY;
                var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
                var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X-Y slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionZ;
                var tSlap1 = (boundingBox.MinZ - line.OriginZ) * invRayDir;
                var tSlap2 = (boundingBox.MaxZ - line.OriginZ) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            return new LineSegment3D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginZ + tMin * line.DirectionZ,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY,
                line.OriginZ + tMax * line.DirectionZ
            );
        }

        public static LineSegment3D ClipLine(this IBoundingBox3D boundingBox, ILine3D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
        {
            var tMin = lineParamMinValue;
            var tMax = lineParamMaxValue;

            //Compute intersection parameters of ray with Y-Z slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionX;
                var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
                var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X-Z slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionY;
                var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
                var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            //Compute intersection parameters of ray with X-Y slaps
            {
                // Update interval for _i_th bounding box slab
                var invRayDir = 1.0d / line.DirectionZ;
                var tSlap1 = (boundingBox.MinZ - line.OriginZ) * invRayDir;
                var tSlap2 = (boundingBox.MaxZ - line.OriginZ) * invRayDir;

                // Update parametric interval from slab intersection t values
                if (tSlap1 > tSlap2)
                {
                    var s = tSlap1;
                    tSlap1 = tSlap2;
                    tSlap2 = s;
                }

                // Update tFar to ensure robust ray-bounds intersection
                tSlap2 *= 1 + 2 * Float64Utils.Gamma3;
                tMin = tSlap1 > tMin ? tSlap1 : tMin;
                tMax = tSlap2 < tMax ? tSlap2 : tMax;

                if (tMin > tMax)
                    return null;
            }

            return new LineSegment3D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginZ + tMin * line.DirectionZ,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY,
                line.OriginZ + tMax * line.DirectionZ
            );
        }

        public static MutableBoundingBox2D GetMutableBoundingBox2D<T>(this IEnumerable<T> objectsList)
            where T : IFiniteGeometricShape2D
        {
            var result = new MutableBoundingBox2D();

            var flag = false;
            foreach (var geometricObject in objectsList)
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

        public static MutableBoundingBox3D GetMutableBoundingBox3D<T>(this IEnumerable<T> objectsList)
            where T : IFiniteGeometricShape3D
        {
            var result = new MutableBoundingBox3D();

            var flag = false;
            foreach (var geometricObject in objectsList)
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

        public static Tuple3D GetPointAt(this IBoundingBox3D boundingBox, double tx, double ty, double tz)
            => new Tuple3D(
                (1.0d - tx) * boundingBox.MinX + tx * boundingBox.MaxX,
                (1.0d - ty) * boundingBox.MinY + ty * boundingBox.MaxY,
                (1.0d - tz) * boundingBox.MinZ + tz * boundingBox.MaxZ
            );

        public static Tuple3D GetPointAt(this IBoundingBox3D boundingBox, ITuple3D tVector)
            => new Tuple3D(
                (1.0d - tVector.X) * boundingBox.MinX + tVector.X * boundingBox.MaxX,
                (1.0d - tVector.Y) * boundingBox.MinY + tVector.Y * boundingBox.MaxY,
                (1.0d - tVector.Z) * boundingBox.MinZ + tVector.Z * boundingBox.MaxZ
            );

        public static IEnumerable<double> GetComponents(this IBoundingBox1D boundingBox)
        {
            yield return boundingBox.MinValue;
            yield return boundingBox.MaxValue;
        }

        public static IEnumerable<double> GetComponents(this IBoundingBox2D boundingBox, bool byCorner = true)
        {
            if (byCorner)
            {
                yield return boundingBox.MinX;
                yield return boundingBox.MinY;

                yield return boundingBox.MaxX;
                yield return boundingBox.MaxY;
            }
            else
            {
                yield return boundingBox.MinX;
                yield return boundingBox.MaxX;

                yield return boundingBox.MinY;
                yield return boundingBox.MaxY;
            }
        }

        public static IEnumerable<double> GetComponents(this IBoundingBox3D boundingBox, bool byCorner = true)
        {
            if (byCorner)
            {
                yield return boundingBox.MinX;
                yield return boundingBox.MinY;
                yield return boundingBox.MinZ;

                yield return boundingBox.MaxX;
                yield return boundingBox.MaxY;
                yield return boundingBox.MaxZ;
            }
            else
            {
                yield return boundingBox.MinX;
                yield return boundingBox.MaxX;

                yield return boundingBox.MinY;
                yield return boundingBox.MaxY;

                yield return boundingBox.MinZ;
                yield return boundingBox.MaxZ;
            }
        }
    }
}
