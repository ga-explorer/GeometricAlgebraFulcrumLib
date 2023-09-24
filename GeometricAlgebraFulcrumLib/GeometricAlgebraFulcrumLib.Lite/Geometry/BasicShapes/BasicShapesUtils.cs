using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Mutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes
{
    public static class BasicShapesUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableBoundingBox3D MapBoundingBox(this IAffineMap3D affineMap, MutableBoundingBox3D boundingBox)
        {
            return (MutableBoundingBox3D) boundingBox.MapUsing(affineMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IIntersectable> GetIntersectables<T>(this IEnumerable<T> geometricObjectsList)
            where T : IFiniteGeometricShape2D
        {
            return geometricObjectsList
                .Cast<IIntersectable>()
                .Where(g => !ReferenceEquals(g, null));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IIntersectable> GetIntersectables(this IEnumerable<IFiniteGeometricShape3D> geometricObjectsList)
        {
            return geometricObjectsList
                .Cast<IIntersectable>()
                .Where(g => !ReferenceEquals(g, null));
        }

        public static IEnumerable<IFloat64Vector2D> GetRegularPolygonPoints(int sidesCount, double centerX, double centerY, double radius, double offsetAngle = 0, bool reverseOrder = false)
        {
            if (sidesCount < 3)
                throw new InvalidOperationException();

            var angleStep = 2 * Math.PI / sidesCount;

            var result = Enumerable
                .Range(0, sidesCount)
                .Select(i => offsetAngle + i * angleStep)
                .Select(a => (IFloat64Vector2D)Float64Vector2D.Create((Float64Scalar)(centerX + radius * Math.Cos(a)),
                    (Float64Scalar)(centerY + radius * Math.Sin(a))));

            return reverseOrder ? result.Reverse() : result;
        }

        public static IEnumerable<IFloat64Vector2D> GetRegularPolygonPoints(int sidesCount, IFloat64Vector2D center, double radius, double offsetAngle = 0, bool reverseOrder = false)
        {
            if (sidesCount < 3)
                throw new InvalidOperationException();

            var angleStep = 2 * Math.PI / sidesCount;

            var result = Enumerable
                .Range(0, sidesCount)
                .Select(i => offsetAngle + i * angleStep)
                .Select(a => (IFloat64Vector2D)Float64Vector2D.Create(center.X + radius * Math.Cos(a),
                    center.Y + radius * Math.Sin(a)));

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
