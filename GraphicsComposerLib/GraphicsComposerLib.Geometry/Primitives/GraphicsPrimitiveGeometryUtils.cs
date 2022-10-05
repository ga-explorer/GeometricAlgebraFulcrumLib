using System;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using NumericalGeometryLib.BasicShapes.Triangles;
using NumericalGeometryLib.BasicShapes.Triangles.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Lines;
using GraphicsComposerLib.Geometry.Primitives.Points;
using GraphicsComposerLib.Geometry.Primitives.Triangles;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public static class GraphicsPrimitiveGeometryUtils
    {
        private static readonly string[] GraphicsPrimitiveTypeNames =
        {
            "points",
            "lines",
            "line-loop",
            "line-strip",
            "triangles",
            "triangle-strip",
            "triangle-fan"
        };

        public static string GetName(this GraphicsPrimitiveType3D primitiveType)
        {
            return GraphicsPrimitiveTypeNames[(int) primitiveType];
        }


        public static bool IsValidTriangleIndexTriplet(this ITriplet<int> triplet, bool isOrdered)
        {
            var index1 = triplet.Item1;
            var index2 = triplet.Item2;
            var index3 = triplet.Item3;

            return isOrdered
                ? index1 >= 0 && index1 < index2 && index1 < index3
                : index1 >= 0 && index1 != index2 && index2 != index3 && index3 != index1;
        }

        public static bool IsValidTriangleIndexTriplet(this ITriplet<int> triplet)
        {
            var index1 = triplet.Item1;
            var index2 = triplet.Item2;
            var index3 = triplet.Item3;

            return index1 >= 0 && index1 < index2 && index1 < index3;
        }

        /// <summary>
        /// A valid index triplet always start with the smallest index
        /// while retaining the winding order of the indices
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="index3"></param>
        /// <returns></returns>
        public static Triplet<int> GetValidTriangleIndexTriplet(int index1, int index2, int index3)
        {
            if (index1 < 0 || index2 < 0 || index3 < 0)
                throw new IndexOutOfRangeException();

            if (index1 == index2 || index2 == index3 || index3 == index1)
                throw new InvalidOperationException();

            if (index1 < index2)
            {
                if (index1 < index3)
                    return new Triplet<int>(index1, index2, index3);
            }
            else
            {
                if (index2 < index3)
                    return new Triplet<int>(index2, index3, index1);
            }

            return new Triplet<int>(index3, index1, index2);
        }

        /// <summary>
        /// A proper index triplet always start with the smallest index
        /// while retaining the winding order of the indices
        /// </summary>
        /// <param name="triplet"></param>
        /// <returns></returns>
        public static Triplet<int> GetValidTriangleIndexTriplet(this ITriplet<int> triplet)
        {
            var index1 = triplet.Item1;
            var index2 = triplet.Item2;
            var index3 = triplet.Item3;

            if (index1 < 0 || index2 < 0 || index3 < 0)
                throw new IndexOutOfRangeException();

            if (index1 == index2 || index2 == index3 || index3 == index1)
                throw new InvalidOperationException();

            if (index1 < index2)
            {
                if (index1 < index3)
                    return new Triplet<int>(index1, index2, index3);
            }
            else
            {
                if (index2 < index3)
                    return new Triplet<int>(index2, index3, index1);
            }

            return new Triplet<int>(index3, index1, index2);
        }

        public static GrLineGeometryComposer3D ToLineGeometryComposer(this IEnumerable<ITuple3D> pointsList, bool closedFlag = false)
        {
            var lineMesh = new GrLineGeometryComposer3D();

            lineMesh.AddPolyLine(pointsList, closedFlag);

            return lineMesh;
        }

        public static GrPointGeometry3D ToGraphicsPointsGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrPointGeometry3D.Create(pointsList);
        }

        public static GrPointGeometry3D ToGraphicsPointsGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrPointGeometry3D.Create(pointsList);
        }

        public static GrPointSoupGeometry3D ToGraphicsPointsListGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrPointSoupGeometry3D.Create(pointsList);
        }

        public static GrPointSoupGeometry3D ToGraphicsPointsListGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrPointSoupGeometry3D.Create(pointsList);
        }

        public static GrLineLoopGeometry3D ToGraphicsLineLoopGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrLineLoopGeometry3D.Create(pointsList);
        }

        public static GrLineLoopGeometry3D ToGraphicsLineLoopGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrLineLoopGeometry3D.Create(pointsList);
        }

        public static GrLineStripGeometry3D ToGraphicsLineStripGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrLineStripGeometry3D.Create(pointsList);
        }

        public static GrLineStripGeometry3D ToGraphicsLineStripGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrLineStripGeometry3D.Create(pointsList);
        }

        public static GrLineGeometry3D ToGraphicsLinesGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrLineGeometry3D.Create(pointsList);
        }

        public static GrLineGeometry3D ToGraphicsLinesGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrLineGeometry3D.Create(pointsList);
        }

        public static GrLineGeometry3D ToGraphicsLinesGeometry(this IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            return GrLineGeometry3D.Create(lineSegmentsList);
        }

        public static GrLineSoupGeometry3D ToGraphicsLinesListGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrLineSoupGeometry3D.Create(pointsList);
        }

        public static GrLineSoupGeometry3D ToGraphicsLinesListGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrLineSoupGeometry3D.Create(pointsList);
        }

        public static GrLineSoupGeometry3D ToGraphicsLinesListGeometry(this IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            return GrLineSoupGeometry3D.Create(lineSegmentsList);
        }

        public static GrTriangleFanGeometry3D ToGraphicsTriangleFanGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrTriangleFanGeometry3D.Create(pointsList);
        }

        public static GrTriangleFanGeometry3D ToGraphicsTriangleFanGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrTriangleFanGeometry3D.Create(pointsList);
        }

        public static GrTriangleStripGeometry3D ToGraphicsTriangleStripGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrTriangleStripGeometry3D.Create(pointsList);
        }

        public static GrTriangleStripGeometry3D ToGraphicsTriangleStripGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrTriangleStripGeometry3D.Create(pointsList);
        }

        public static GrTriangleGeometry3D ToGraphicsTrianglesGeometry(this IReadOnlyList<ITuple3D> pointsList)
        {
            return GrTriangleGeometry3D.Create(pointsList);
        }

        public static GrTriangleGeometry3D ToGraphicsTrianglesGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrTriangleGeometry3D.Create(pointsList);
        }

        public static GrTriangleGeometry3D ToGraphicsTrianglesGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
        {
            return GrTriangleGeometry3D.Create(trianglesList, reversePoints);
        }

        public static GrTriangleSoupGeometry3D ToGraphicsTrianglesListGeometry(this IEnumerable<ITuple3D> pointsList)
        {
            return GrTriangleSoupGeometry3D.Create(pointsList);
        }

        public static GrTriangleSoupGeometry3D ToGraphicsTrianglesListGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
        {
            return GrTriangleSoupGeometry3D.Create(trianglesList, reversePoints);
        }


        public static Tuple3D GetDisplacedPoint(this IGraphicsSurfaceLocalFrame3D vertex, double t)
        {
            return new Tuple3D(
                vertex.Point.X + t * vertex.Normal.X,
                vertex.Point.Y + t * vertex.Normal.Y,
                vertex.Point.Z + t * vertex.Normal.Z
            );
        }
        
        public static LineSegment3D GetDisplacedLineSegment(this IGraphicsSurfaceLocalFrame3D vertex, double t1, double t2)
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

        public static IEnumerable<ILineSegment3D> GetNormalLines(this IGraphicsTriangleGeometry3D geometry, double t2)
        {
            for (var i = 0; i < geometry.VertexCount; i++)
            {
                var point = geometry.GetGeometryPoint(i);
                var normal = geometry.GetVertexNormal(i);

                var p2 = point.GetPointInDirection(normal, t2);

                yield return LineSegment3D.Create(point, p2);
            }
        }

        public static IEnumerable<ILineSegment3D> GetNormalLines(this IGraphicsTriangleGeometry3D geometry, double t1, double t2)
        {
            for (var i = 0; i < geometry.VertexCount; i++)
            {
                var point = geometry.GetGeometryPoint(i);
                var normal = geometry.GetVertexNormal(i);

                var p1 = point.GetPointInDirection(normal, t1);
                var p2 = point.GetPointInDirection(normal, t2);

                yield return LineSegment3D.Create(p1, p2);
            }
        }

        public static IEnumerable<ITriangle3D> GetDisplacedTriangles(this IGraphicsTriangleGeometry3D geometry, double t)
        {
            foreach (var triangleIndices in geometry.TriangleVertexIndices)
            {
                var (i1, i2, i3) = triangleIndices.ToTuple();

                var p1 = geometry
                        .GetGeometryPoint(i1)
                        .GetPointInDirection(geometry.GetVertexNormal(i1), t);

                var p2 = geometry
                    .GetGeometryPoint(i2)
                    .GetPointInDirection(geometry.GetVertexNormal(i2), t);

                var p3 = geometry
                    .GetGeometryPoint(i3)
                    .GetPointInDirection(geometry.GetVertexNormal(i3), t);

                yield return Triangle3D.Create(p1, p2, p3);
            }
        }

        public static IEnumerable<ILineSegment3D> GetDisplacedTriangleEdges(this IGraphicsTriangleGeometry3D geometry, double t)
        {
            foreach (var triangleIndices in geometry.TriangleVertexIndices)
            {
                var (i1, i2, i3) = triangleIndices.ToTuple();

                var p1 = geometry
                    .GetGeometryPoint(i1)
                    .GetPointInDirection(geometry.GetVertexNormal(i1), t);

                var p2 = geometry
                    .GetGeometryPoint(i2)
                    .GetPointInDirection(geometry.GetVertexNormal(i2), t);

                var p3 = geometry
                    .GetGeometryPoint(i3)
                    .GetPointInDirection(geometry.GetVertexNormal(i3), t);

                yield return LineSegment3D.Create(p1, p2);
                yield return LineSegment3D.Create(p2, p3);
                yield return LineSegment3D.Create(p3, p1);
            }
        }

        public static IEnumerable<ILineSegment3D> GetTriangleEdges(this IGraphicsTriangleGeometry3D geometry)
        {
            foreach (var triangleIndices in geometry.TriangleVertexIndices)
            {
                var (i1, i2, i3) = triangleIndices.ToTuple();

                var p1 = geometry.GetGeometryPoint(i1);
                var p2 = geometry.GetGeometryPoint(i2);
                var p3 = geometry.GetGeometryPoint(i3);

                yield return LineSegment3D.Create(p1, p2);
                yield return LineSegment3D.Create(p2, p3);
                yield return LineSegment3D.Create(p3, p1);
            }
        }
        
        
    }
}