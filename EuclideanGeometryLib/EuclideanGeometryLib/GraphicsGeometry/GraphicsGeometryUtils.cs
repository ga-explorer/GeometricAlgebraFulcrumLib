using System;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;
using EuclideanGeometryLib.GraphicsGeometry.Constants;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Points;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry
{
    public static class GraphicsGeometryUtils
    {
        public static Tuple2D GetVector2D(this GraphicsAxis2D axis)
        {
            if (axis == GraphicsAxis2D.PositiveX)
                return new Tuple2D(1, 0);

            if (axis == GraphicsAxis2D.NegativeX)
                return new Tuple2D(-1, 0);

            if (axis == GraphicsAxis2D.PositiveY)
                return new Tuple2D(0, 1);

            return new Tuple2D(0, -1);
        }

        public static Tuple3D GetVector3D(this GraphicsAxis2D axis)
        {
            if (axis == GraphicsAxis2D.PositiveX)
                return new Tuple3D(1, 0, 0);

            if (axis == GraphicsAxis2D.NegativeX)
                return new Tuple3D(-1, 0, 0);

            if (axis == GraphicsAxis2D.PositiveY)
                return new Tuple3D(0, 1, 0);

            return new Tuple3D(0, -1, 0);
        }

        public static Tuple3D GetVector3D(this GraphicsAxis3D axis)
        {
            if (axis == GraphicsAxis3D.PositiveX)
                return new Tuple3D(1, 0, 0);

            if (axis == GraphicsAxis3D.NegativeX)
                return new Tuple3D(-1, 0, 0);

            if (axis == GraphicsAxis3D.PositiveY)
                return new Tuple3D(0, 1, 0);

            if (axis == GraphicsAxis3D.NegativeY)
                return new Tuple3D(0, -1, 0);

            if (axis == GraphicsAxis3D.PositiveZ)
                return new Tuple3D(0, 0, 1);

            return new Tuple3D(0, 0, -1);
        }


        public static GraphicsPointsGeometry3D ToGraphicsPointsGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsPointsGeometry3D.Create(pointsList);

        public static GraphicsPointsGeometry3D ToGraphicsPointsGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsPointsGeometry3D.Create(pointsList);

        public static GraphicsPointsListGeometry3D ToGraphicsPointsListGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsPointsListGeometry3D.Create(pointsList);

        public static GraphicsPointsListGeometry3D ToGraphicsPointsListGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsPointsListGeometry3D.Create(pointsList);

        public static GraphicsLineLoopGeometry3D ToGraphicsLineLoopGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsLineLoopGeometry3D.Create(pointsList);

        public static GraphicsLineLoopGeometry3D ToGraphicsLineLoopGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsLineLoopGeometry3D.Create(pointsList);

        public static GraphicsLineStripGeometry3D ToGraphicsLineStripGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsLineStripGeometry3D.Create(pointsList);

        public static GraphicsLineStripGeometry3D ToGraphicsLineStripGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsLineStripGeometry3D.Create(pointsList);

        public static GraphicsLinesGeometry3D ToGraphicsLinesGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsLinesGeometry3D.Create(pointsList);

        public static GraphicsLinesGeometry3D ToGraphicsLinesGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsLinesGeometry3D.Create(pointsList);

        public static GraphicsLinesGeometry3D ToGraphicsLinesGeometry(this IEnumerable<ILineSegment3D> lineSegmentsList)
            => GraphicsLinesGeometry3D.Create(lineSegmentsList);

        public static GraphicsLinesListGeometry3D ToGraphicsLinesListGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsLinesListGeometry3D.Create(pointsList);

        public static GraphicsLinesListGeometry3D ToGraphicsLinesListGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsLinesListGeometry3D.Create(pointsList);

        public static GraphicsLinesListGeometry3D ToGraphicsLinesListGeometry(this IEnumerable<ILineSegment3D> lineSegmentsList)
            => GraphicsLinesListGeometry3D.Create(lineSegmentsList);

        public static GraphicsTriangleFanGeometry3D ToGraphicsTriangleFanGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsTriangleFanGeometry3D.Create(pointsList);

        public static GraphicsTriangleFanGeometry3D ToGraphicsTriangleFanGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsTriangleFanGeometry3D.Create(pointsList);

        public static GraphicsTriangleStripGeometry3D ToGraphicsTriangleStripGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsTriangleStripGeometry3D.Create(pointsList);

        public static GraphicsTriangleStripGeometry3D ToGraphicsTriangleStripGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsTriangleStripGeometry3D.Create(pointsList);

        public static GraphicsTrianglesGeometry3D ToGraphicsTrianglesGeometry(this IReadOnlyList<ITuple3D> pointsList)
            => GraphicsTrianglesGeometry3D.Create(pointsList);

        public static GraphicsTrianglesGeometry3D ToGraphicsTrianglesGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsTrianglesGeometry3D.Create(pointsList);

        public static GraphicsTrianglesGeometry3D ToGraphicsTrianglesGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
            => GraphicsTrianglesGeometry3D.Create(trianglesList, reversePoints);

        public static GraphicsTrianglesListGeometry3D ToGraphicsTrianglesListGeometry(this IEnumerable<ITuple3D> pointsList)
            => GraphicsTrianglesListGeometry3D.Create(pointsList);

        public static GraphicsTrianglesListGeometry3D ToGraphicsTrianglesListGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
            => GraphicsTrianglesListGeometry3D.Create(trianglesList, reversePoints);

        
        public static Tuple3D GetDisplacedPoint(this IGraphicsVertex3D vertex, double t)
        {
            return new Tuple3D(
                vertex.Point.X + t * vertex.Normal.X,
                vertex.Point.Y + t * vertex.Normal.Y,
                vertex.Point.Z + t * vertex.Normal.Z
            );
        }
        
        public static LineSegment3D GetDisplacedLineSegment(this IGraphicsVertex3D vertex, double t1, double t2)
        {
            return LineSegment3D.Create(
                new Tuple3D(
                    vertex.Point.X + t1 * vertex.Normal.X,
                    vertex.Point.Y + t1 * vertex.Normal.Y,
                    vertex.Point.Z + t1 * vertex.Normal.Z
                ),
                new Tuple3D(
                    vertex.Point.X + t2 * vertex.Normal.X,
                    vertex.Point.Y + t2 * vertex.Normal.Y,
                    vertex.Point.Z + t2 * vertex.Normal.Z
                )
            );
        }

        public static IEnumerable<ILineSegment3D> GetNormalLines(this IGraphicsTrianglesGeometry3D geometry, double t2)
        {
            for (var i = 0; i < geometry.VertexPoints.Count; i++)
            {
                var point = geometry.VertexPoints[i];
                var normal = geometry.VertexNormals[i];

                var p2 = point.GetPointInDirection(normal, t2);

                yield return LineSegment3D.Create(point, p2);
            }
        }

        public static IEnumerable<ILineSegment3D> GetNormalLines(this IGraphicsTrianglesGeometry3D geometry, double t1, double t2)
        {
            for (var i = 0; i < geometry.VertexPoints.Count; i++)
            {
                var point = geometry.VertexPoints[i];
                var normal = geometry.VertexNormals[i];

                var p1 = point.GetPointInDirection(normal, t1);
                var p2 = point.GetPointInDirection(normal, t2);

                yield return LineSegment3D.Create(p1, p2);
            }
        }

        public static IEnumerable<ITriangle3D> GetDisplacedTriangles(this IGraphicsTrianglesGeometry3D geometry, double t)
        {
            foreach (var triangleIndices in geometry.TriangleVerticesIndices)
            {
                var (i1, i2, i3) = triangleIndices.ToTuple();

                var p1 = geometry
                        .VertexPoints[i1]
                        .GetPointInDirection(geometry.VertexNormals[i1], t);

                var p2 = geometry
                    .VertexPoints[i2]
                    .GetPointInDirection(geometry.VertexNormals[i2], t);

                var p3 = geometry
                    .VertexPoints[i3]
                    .GetPointInDirection(geometry.VertexNormals[i3], t);

                yield return Triangle3D.Create(p1, p2, p3);
            }
        }

        public static IEnumerable<ILineSegment3D> GetDisplacedTrianglesLines(this IGraphicsTrianglesGeometry3D geometry, double t)
        {
            foreach (var triangleIndices in geometry.TriangleVerticesIndices)
            {
                var (i1, i2, i3) = triangleIndices.ToTuple();

                var p1 = geometry
                    .VertexPoints[i1]
                    .GetPointInDirection(geometry.VertexNormals[i1], t);

                var p2 = geometry
                    .VertexPoints[i2]
                    .GetPointInDirection(geometry.VertexNormals[i2], t);

                var p3 = geometry
                    .VertexPoints[i3]
                    .GetPointInDirection(geometry.VertexNormals[i3], t);

                yield return LineSegment3D.Create(p1, p2);
                yield return LineSegment3D.Create(p2, p3);
                yield return LineSegment3D.Create(p3, p1);
            }
        }
    }
}