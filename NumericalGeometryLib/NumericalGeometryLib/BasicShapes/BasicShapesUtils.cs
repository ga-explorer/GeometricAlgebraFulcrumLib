using System;
using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicMath.Maps.Space2D;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using NumericalGeometryLib.BasicShapes.Triangles;
using NumericalGeometryLib.BasicShapes.Triangles.Immutable;

namespace NumericalGeometryLib.BasicShapes
{
    public static class BasicShapesUtils
    {
        public static IEnumerable<IFloat64Tuple2D> GetRegularPolygonPoints(int sidesCount, double centerX, double centerY, double radius, double offsetAngle = 0, bool reverseOrder = false)
        {
            if (sidesCount < 3)
                throw new InvalidOperationException();

            var angleStep = 2 * Math.PI / sidesCount;

            var result = Enumerable
                .Range(0, sidesCount)
                .Select(i => offsetAngle + i * angleStep)
                .Select(a => (IFloat64Tuple2D)new Float64Tuple2D(
                    centerX + radius * Math.Cos(a),
                    centerY + radius * Math.Sin(a)
                ));

            return reverseOrder ? result.Reverse() : result;
        }

        public static IEnumerable<IFloat64Tuple2D> GetRegularPolygonPoints(int sidesCount, IFloat64Tuple2D center, double radius, double offsetAngle = 0, bool reverseOrder = false)
        {
            if (sidesCount < 3)
                throw new InvalidOperationException();

            var angleStep = 2 * Math.PI / sidesCount;

            var result = Enumerable
                .Range(0, sidesCount)
                .Select(i => offsetAngle + i * angleStep)
                .Select(a => (IFloat64Tuple2D)new Float64Tuple2D(
                    center.X + radius * Math.Cos(a),
                    center.Y + radius * Math.Sin(a)
                ));

            return reverseOrder ? result.Reverse() : result;
        }

        #region Affine Maps
        public static Line2D MapLine(this IAffineMap2D linearMap, ILine2D line)
        {
            return Line2D.Create(
                linearMap.MapPoint(line.GetOrigin()),
                linearMap.MapVector(line.GetDirection())
            );
        }

        public static Line3D MapLine(this IAffineMap3D linearMap, ILine3D line)
        {
            return new Line3D(
                linearMap.MapPoint(line.GetOrigin()),
                linearMap.MapVector(line.GetDirection())
            );
        }

        public static LineSegment2D MapLineSegment(this IAffineMap2D linearMap, ILineSegment2D lineSegment)
        {
            return LineSegment2D.Create(
                linearMap.MapPoint(lineSegment.GetPoint1()),
                linearMap.MapPoint(lineSegment.GetPoint2())
            );
        }

        public static LineSegment3D MapLineSegment(this IAffineMap3D linearMap, ILineSegment3D lineSegment)
        {
            return LineSegment3D.Create(
                linearMap.MapPoint(lineSegment.GetPoint1()),
                linearMap.MapPoint(lineSegment.GetPoint2())
            );
        }

        public static Triangle2D MapTriangle(this IAffineMap2D linearMap, ITriangle2D triangle)
        {
            return Triangle2D.Create(
                linearMap.MapPoint(triangle.GetPoint1()),
                linearMap.MapPoint(triangle.GetPoint2()),
                linearMap.MapPoint(triangle.GetPoint3())
            );
        }

        public static Triangle3D MapTriangle(this IAffineMap3D linearMap, ITriangle3D triangle)
        {
            return Triangle3D.Create(
                linearMap.MapPoint(triangle.GetPoint1()),
                linearMap.MapPoint(triangle.GetPoint2()),
                linearMap.MapPoint(triangle.GetPoint3())
            );
        }
        #endregion
    }
}
