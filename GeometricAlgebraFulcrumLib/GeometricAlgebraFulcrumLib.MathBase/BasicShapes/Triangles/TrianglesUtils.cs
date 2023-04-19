using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Planes.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles
{
    public static class TrianglesUtils
    {
        #region Triangles in 2D
        public static Float64Tuple2D GetPoint1(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(triangle.Point1X, triangle.Point1Y);
        }

        public static Float64Tuple2D GetPoint2(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(triangle.Point2X, triangle.Point2Y);
        }

        public static Float64Tuple2D GetPoint3(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(triangle.Point3X, triangle.Point3Y);
        }

        public static Float64Tuple2D[] GetEndPoints(this ITriangle2D lineSegment)
        {
            return new[]
            {
                new Float64Tuple2D(
                    lineSegment.Point1X,
                    lineSegment.Point1Y
                ),
                new Float64Tuple2D(
                    lineSegment.Point2X,
                    lineSegment.Point2Y
                ),
                new Float64Tuple2D(
                    lineSegment.Point3X,
                    lineSegment.Point3Y
                )
            };
        }

        public static IEnumerable<Float64Tuple2D> GetEndPoints(this IEnumerable<ITriangle2D> trianglesList)
        {
            return trianglesList.SelectMany(t => t.GetEndPoints());
        }

        public static Triangle3D ToTriangle(this ITriangle3D triangle)
        {
            return new Triangle3D(
                triangle.Point1X, triangle.Point1Y, triangle.Point1Z,
                triangle.Point2X, triangle.Point2Y, triangle.Point2Z,
                triangle.Point3X, triangle.Point3Y, triangle.Point3Z
            );
        }

        public static Triangle3D ToTriangleReverse(this ITriangle3D triangle)
        {
            return new Triangle3D(
                triangle.Point3X, triangle.Point3Y, triangle.Point3Z,
                triangle.Point2X, triangle.Point2Y, triangle.Point2Z,
                triangle.Point1X, triangle.Point1Y, triangle.Point1Z
            );
        }

        public static Float64Tuple2D GetDirection12(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y
            );
        }

        public static Float64Tuple2D GetDirection21(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y
            );
        }

        public static Float64Tuple2D GetDirection23(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y
            );
        }

        public static Float64Tuple2D GetDirection32(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y
            );
        }

        public static Float64Tuple2D GetDirection31(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y
            );
        }

        public static Float64Tuple2D GetDirection13(this ITriangle2D triangle)
        {
            return new Float64Tuple2D(
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y
            );
        }

        public static LineSegment2D GetLineSegment12(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point1X, triangle.Point1Y,
                triangle.Point2X, triangle.Point2Y
            );
        }

        public static LineSegment2D GetLineSegment21(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point2X, triangle.Point2Y,
                triangle.Point1X, triangle.Point1Y
            );
        }

        public static LineSegment2D GetLineSegment13(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point1X, triangle.Point1Y,
                triangle.Point3X, triangle.Point3Y
            );
        }

        public static LineSegment2D GetLineSegment31(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point3X, triangle.Point3Y,
                triangle.Point1X, triangle.Point1Y
            );
        }

        public static LineSegment2D GetLineSegment23(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point2X, triangle.Point2Y,
                triangle.Point3X, triangle.Point3Y
            );
        }

        public static LineSegment2D GetLineSegment32(this ITriangle2D triangle)
        {
            return new LineSegment2D(
                triangle.Point3X, triangle.Point3Y,
                triangle.Point2X, triangle.Point2Y
            );
        }

        public static Line2D GetLine12(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y
            );
        }

        public static Line2D GetLine21(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y
            );
        }

        public static Line2D GetLine23(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y
            );
        }

        public static Line2D GetLine32(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y
            );
        }

        public static Line2D GetLine31(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y
            );
        }

        public static Line2D GetLine13(this ITriangle2D triangle)
        {
            return new Line2D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y
            );
        }

        public static LimitedLine2D GetLimitedLine12(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y,
                0, 1
            );
        }

        public static LimitedLine2D GetLimitedLine21(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y,
                0, 1
            );
        }

        public static LimitedLine2D GetLimitedLine13(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y,
                0, 1
            );
        }

        public static LimitedLine2D GetLimitedLine31(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y,
                0, 1
            );
        }

        public static LimitedLine2D GetLimitedLine23(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y,
                0, 1
            );
        }

        public static LimitedLine2D GetLimitedLine32(this ITriangle2D triangle)
        {
            return new LimitedLine2D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y,
                0, 1
            );
        }

        public static Float64Tuple2D GetInnerPointAt(this ITriangle2D triangle, double d1, double d2, double d3)
        {
            var dInv = 1 / (d1 + d2 + d3);
            var w1 = d1 * dInv;
            var w2 = d2 * dInv;
            var w3 = d3 * dInv;

            return new Float64Tuple2D(
                w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
                w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y
            );
        }

        public static Float64Tuple2D GetInnerPointAt(this ITriangle2D triangle, IFloat64Tuple3D dTuple)
        {
            var dInv = 1 / (dTuple.X + dTuple.Y + dTuple.Z);
            var w1 = dTuple.X * dInv;
            var w2 = dTuple.Y * dInv;
            var w3 = dTuple.Z * dInv;

            return new Float64Tuple2D(
                w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
                w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y
            );
        }

        public static IEnumerable<Float64Tuple2D> GetInnerPointsAt(this ITriangle2D triangle, IEnumerable<IFloat64Tuple3D> parametersList)
        {
            return parametersList.Select(
                p => triangle.GetInnerPointAt(p.X, p.Y, p.Z)
            );
        }

        public static Float64Tuple2D GetPointAt(this ITriangle2D triangle, double w1, double w2)
        {
            var w3 = 1.0d - (w1 + w2);

            return new Float64Tuple2D(
                w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
                w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y
            );
        }

        public static IEnumerable<Float64Tuple2D> GetPointsAt(this ITriangle2D triangle, IEnumerable<Float64Tuple2D> parametersList)
        {
            return parametersList.Select(
                p => triangle.GetPointAt(p.X, p.Y)
            );
        }

        public static Triangle2D GetTriangleAt(this ITriangle2D triangle, Float64Tuple2D w1, Float64Tuple2D w2, Float64Tuple2D w3)
        {
            var point1 = triangle.GetPointAt(w1.X, w1.Y);
            var point2 = triangle.GetPointAt(w2.X, w2.Y);
            var point3 = triangle.GetPointAt(w3.X, w3.Y);

            return new Triangle2D(
                point1.X, point1.Y,
                point2.X, point2.Y,
                point3.X, point3.Y
            );
        }
        #endregion

        #region Triangles in 3D
        public static Float64Tuple3D GetPoint1(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(triangle.Point1X, triangle.Point1Y, triangle.Point1Z);
        }

        public static Float64Tuple3D GetPoint2(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(triangle.Point2X, triangle.Point2Y, triangle.Point2Z);
        }

        public static Float64Tuple3D GetPoint3(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(triangle.Point3X, triangle.Point3Y, triangle.Point3Z);
        }

        public static Float64Tuple3D[] GetEndPoints(this ITriangle3D lineSegment)
        {
            return new[]
            {
                new Float64Tuple3D(
                    lineSegment.Point1X,
                    lineSegment.Point1Y,
                    lineSegment.Point1Z
                ),
                new Float64Tuple3D(
                    lineSegment.Point2X,
                    lineSegment.Point2Y,
                    lineSegment.Point2Z
                ),
                new Float64Tuple3D(
                    lineSegment.Point3X,
                    lineSegment.Point3Y,
                    lineSegment.Point3Z
                )
            };
        }

        public static IEnumerable<Float64Tuple3D> GetEndPoints(this IEnumerable<ITriangle3D> trianglesList)
        {
            return trianglesList.SelectMany(t => t.GetEndPoints());
        }

        public static Float64Tuple3D GetDirection12(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y,
                triangle.Point2Z - triangle.Point1Z
            );
        }

        public static Float64Tuple3D GetDirection21(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y,
                triangle.Point1Z - triangle.Point2Z
            );
        }

        public static Float64Tuple3D GetDirection23(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y,
                triangle.Point3Z - triangle.Point2Z
            );
        }

        public static Float64Tuple3D GetDirection32(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y,
                triangle.Point2Z - triangle.Point3Z
            );
        }

        public static Float64Tuple3D GetDirection31(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y,
                triangle.Point1Z - triangle.Point3Z
            );
        }

        public static Float64Tuple3D GetDirection13(this ITriangle3D triangle)
        {
            return new Float64Tuple3D(
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y,
                triangle.Point3Z - triangle.Point1Z
            );
        }

        public static LineSegment3D GetSide12(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z
            );
        }

        public static LineSegment3D GetSide21(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z
            );
        }

        public static LineSegment3D GetSide13(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z
            );
        }

        public static LineSegment3D GetSide31(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z
            );
        }

        public static LineSegment3D GetSide23(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z
            );
        }

        public static LineSegment3D GetSide32(this ITriangle3D triangle)
        {
            return new LineSegment3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z
            );
        }

        public static LineSegment3D[] GetSides(this ITriangle3D triangle)
        {
            return new[]
            {
                triangle.GetSide12(),
                triangle.GetSide23(),
                triangle.GetSide31()
            };
        }

        public static IEnumerable<LineSegment3D> GetSides(this IEnumerable<ITriangle3D> trianglesList)
        {
            return trianglesList.SelectMany(t => t.GetSides());
        }

        public static Line3D GetLine12(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y,
                triangle.Point2Z - triangle.Point1Z
            );
        }

        public static Line3D GetLine21(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y,
                triangle.Point1Z - triangle.Point2Z
            );
        }

        public static Line3D GetLine23(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y,
                triangle.Point3Z - triangle.Point2Z
            );
        }

        public static Line3D GetLine32(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y,
                triangle.Point2Z - triangle.Point3Z
            );
        }

        public static Line3D GetLine31(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y,
                triangle.Point1Z - triangle.Point3Z
            );
        }

        public static Line3D GetLine13(this ITriangle3D triangle)
        {
            return new Line3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y,
                triangle.Point3Z - triangle.Point1Z
            );
        }

        public static Line3D[] GetLines(this ITriangle3D triangle)
        {
            return new[]
            {
                triangle.GetLine12(),
                triangle.GetLine23(),
                triangle.GetLine31()
            };
        }

        public static IEnumerable<Line3D> GetLines(this IEnumerable<ITriangle3D> trianglesList)
        {
            return trianglesList.SelectMany(t => t.GetLines());
        }

        public static LimitedLine3D GetLimitedLine12(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point2X - triangle.Point1X,
                triangle.Point2Y - triangle.Point1Y,
                triangle.Point2Z - triangle.Point1Z,
                0, 1
            );
        }

        public static LimitedLine3D GetLimitedLine21(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point1X - triangle.Point2X,
                triangle.Point1Y - triangle.Point2Y,
                triangle.Point1Z - triangle.Point2Z,
                0, 1
            );
        }

        public static LimitedLine3D GetLimitedLine23(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point2X,
                triangle.Point2Y,
                triangle.Point2Z,
                triangle.Point3X - triangle.Point2X,
                triangle.Point3Y - triangle.Point2Y,
                triangle.Point3Z - triangle.Point2Z,
                0, 1
            );
        }

        public static LimitedLine3D GetLimitedLine32(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point2X - triangle.Point3X,
                triangle.Point2Y - triangle.Point3Y,
                triangle.Point2Z - triangle.Point3Z,
                0, 1
            );
        }

        public static LimitedLine3D GetLimitedLine31(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point3X,
                triangle.Point3Y,
                triangle.Point3Z,
                triangle.Point1X - triangle.Point3X,
                triangle.Point1Y - triangle.Point3Y,
                triangle.Point1Z - triangle.Point3Z,
                0, 1
            );
        }

        public static LimitedLine3D GetLimitedLine13(this ITriangle3D triangle)
        {
            return new LimitedLine3D(
                triangle.Point1X,
                triangle.Point1Y,
                triangle.Point1Z,
                triangle.Point3X - triangle.Point1X,
                triangle.Point3Y - triangle.Point1Y,
                triangle.Point3Z - triangle.Point1Z,
                0, 1
            );
        }

        public static LimitedLine3D[] GetLimitedLines(this ITriangle3D triangle)
        {
            return new[]
            {
                triangle.GetLimitedLine12(),
                triangle.GetLimitedLine23(),
                triangle.GetLimitedLine31()
            };
        }

        public static IEnumerable<LimitedLine3D> GetLimitedLines(this IEnumerable<ITriangle3D> trianglesList)
        {
            return trianglesList.SelectMany(t => t.GetLimitedLines());
        }

        public static Float64Tuple3D GetNormal(this ITriangle3D triangle)
        {
            return triangle
                .GetDirection12()
                .VectorCross(
                    triangle.GetDirection23()
                );
        }

        public static Float64Tuple3D GetUnitNormal(this ITriangle3D triangle)
        {
            return triangle
                .GetDirection12()
                .VectorUnitCross(
                    triangle.GetDirection23()
                );
        }

        public static Float64Tuple3D GetPointAt(this ITriangle3D triangle, double d1, double d2, double d3)
        {
            var d = 1 / (d1 + d2 + d3);
            var w1 = d1 * d;
            var w2 = d2 * d;
            var w3 = d3 * d;

            return new Float64Tuple3D(
                w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
                w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y,
                w1 * triangle.Point1Z + w2 * triangle.Point2Z + w3 * triangle.Point3Z
            );
        }

        public static Float64Tuple3D GetPointAt(this ITriangle3D triangle, double w1, double w2)
        {
            var w3 = 1.0d - (w1 + w2);

            return new Float64Tuple3D(
                w1 * triangle.Point1X + w2 * triangle.Point2X + w3 * triangle.Point3X,
                w1 * triangle.Point1Y + w2 * triangle.Point2Y + w3 * triangle.Point3Y,
                w1 * triangle.Point1Z + w2 * triangle.Point2Z + w3 * triangle.Point3Z
            );
        }

        public static IEnumerable<Float64Tuple3D> GetPointsAt(this ITriangle3D triangle, IEnumerable<Float64Tuple3D> parametersList)
        {
            return parametersList.Select(p => triangle.GetPointAt(p.X, p.Y));
        }

        public static Triangle3D GetTriangleAt(this ITriangle3D triangle, Float64Tuple3D w1, Float64Tuple3D w2, Float64Tuple3D w3)
        {
            var point1 = triangle.GetPointAt(w1.X, w1.Y);
            var point2 = triangle.GetPointAt(w2.X, w2.Y);
            var point3 = triangle.GetPointAt(w3.X, w3.Y);

            return new Triangle3D(
                point1.X, point1.Y, point1.Z,
                point2.X, point2.Y, point2.Z,
                point3.X, point3.Y, point3.Z
            );
        }

        public static Plane3D ToPlane(this ITriangle3D triangle)
        {
            return new Plane3D(
                triangle.GetPoint1(),
                triangle.GetDirection12(),
                triangle.GetDirection23()
            );
        }
        #endregion

    }
}
