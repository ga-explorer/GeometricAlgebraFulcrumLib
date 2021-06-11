using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Composers
{
    public class GraphicsTrianglesListGeometryComposer3D
    {
        private readonly List<IGraphicsVertex3D> _verticesList 
            = new List<IGraphicsVertex3D>();


        public GraphicsVertexNormalComputationMethod NormalComputationMethod
            => GenerateNormals
                ? GraphicsVertexNormalComputationMethod.AverageNormals
                : GraphicsVertexNormalComputationMethod.None;
        
        public double DistanceEpsilon { get; set; }
            = 1e-7d;

        public bool AllowSmallTriangles { get; set; }

        public bool GenerateTextureUVs { get; set; }

        public bool GenerateColors { get; set; }

        public bool GenerateNormals { get; set; }

        public bool ReverseNormals { get; set; }

        public IEnumerable<IGraphicsVertex3D> Vertices
            => _verticesList;

        public IEnumerable<ITuple3D> VertexPoints
            => _verticesList
                .Select(p => p.Point);

        public IEnumerable<ITuple2D> VertexTextureUVs
            => _verticesList
                .Select(p => p.TextureUv);

        public IEnumerable<Color> VertexColors
            => _verticesList
                .Select(p => p.Color);

        public IEnumerable<IGraphicsNormal3D> VertexNormals
            => _verticesList
                .Select(p => p.Normal);

        public IEnumerable<int> TriangleVertexIndices
            => Enumerable
                .Range(0, _verticesList.Count);

        public IEnumerable<IGraphicsVertex3D> TriangleVertices
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i]);

        public IEnumerable<ITuple3D> TriangleVertexPoints
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i].Point);

        public IEnumerable<ITuple2D> TriangleVertexTextureUVs
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i].TextureUv);

        public IEnumerable<Color> TriangleVertexColors
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i].Color);

        public IEnumerable<IGraphicsNormal3D> TriangleVertexNormals
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i].Normal);

        public int VerticesCount
            => _verticesList.Count;

        
        public GraphicsTrianglesListGeometryComposer3D()
        {
        }


        public GraphicsTrianglesListGeometryComposer3D Clear()
        {
            _verticesList.Clear();

            return this;
        }


        private IGraphicsVertex3D CreateVertexFromPoint(ITuple3D point)
        {
            var vertexIndex = _verticesList.Count;

            if (GenerateNormals)
            {
                if (GenerateColors)
                    return new GraphicsNormalColoredVertex3D(vertexIndex, point);

                else if (GenerateTextureUVs)
                    return new GraphicsNormalTexturedVertex3D(vertexIndex, point);

                else
                    return new GraphicsNormalVertex3D(vertexIndex, point);
            }
            else
            {
                if (GenerateColors)
                    return new GraphicsColoredVertex3D(vertexIndex, point);

                else if (GenerateTextureUVs)
                    return new GraphicsTexturedVertex3D(vertexIndex, point);

                else
                    return new GraphicsVertex3D(vertexIndex, point);
            }
        }

        private IGraphicsVertex3D CreateVertexFromVertex(IGraphicsVertex3D vertex)
        {
            var vertexIndex = _verticesList.Count;

            if (GenerateNormals)
            {
                if (GenerateColors)
                    return new GraphicsNormalColoredVertex3D(vertexIndex, vertex);

                else if (GenerateTextureUVs)
                    return new GraphicsNormalTexturedVertex3D(vertexIndex, vertex);

                else
                    return new GraphicsNormalVertex3D(vertexIndex, vertex);
            }
            else
            {
                if (GenerateColors)
                    return new GraphicsColoredVertex3D(vertexIndex, vertex);

                else if (GenerateTextureUVs)
                    return new GraphicsTexturedVertex3D(vertexIndex, vertex);

                else
                    return new GraphicsVertex3D(vertexIndex, vertex);
            }
        }

        private IGraphicsVertex3D AddVertexFromPoint(ITuple3D point)
        {
            var storedVertex = CreateVertexFromPoint(point);

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertexFromPoint(double x, double y, double z)
        {
            var storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertexFromVertex(IGraphicsVertex3D vertex)
        {
            var storedVertex = CreateVertexFromVertex(vertex);

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private bool StoreTriangle(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2, IGraphicsVertex3D vertex3)
        {
            //Make sure all side lengths are far enough from zero
            if (!AllowSmallTriangles)
            {
                if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
                if (vertex3.GetDistanceToPoint(vertex2) < DistanceEpsilon) return false;
                if (vertex1.GetDistanceToPoint(vertex3) < DistanceEpsilon) return false;
            }

            //Generate vertex normals
            if (GenerateNormals)
            {
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);

                Debug.Assert(!normal.IsInvalid);

                vertex1.Normal.Set(normal);
                vertex2.Normal.Set(normal);
                vertex3.Normal.Set(normal);
            }

            return true;
        }
        

        public bool AddTriangle(ITriangle3D triangle)
        {
            return StoreTriangle(
                AddVertexFromPoint(triangle.GetPoint1()), 
                AddVertexFromPoint(triangle.GetPoint2()), 
                AddVertexFromPoint(triangle.GetPoint3())
            );
        }

        public bool AddTriangleFromPoints(ITuple3D point1, ITuple3D point2, ITuple3D point3)
        {
            return StoreTriangle(
                AddVertexFromPoint(point1),
                AddVertexFromPoint(point2),
                AddVertexFromPoint(point3)
            );
        }

        public bool AddTriangleFromPoints(ITriplet<ITuple3D> points)
        {
            return StoreTriangle(
                AddVertexFromPoint(points.Item1),
                AddVertexFromPoint(points.Item2),
                AddVertexFromPoint(points.Item3)
            );
        }

        public bool AddTriangleFromVertices(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2, IGraphicsVertex3D vertex3)
        {
            return StoreTriangle(
                AddVertexFromVertex(vertex1), 
                AddVertexFromVertex(vertex2), 
                AddVertexFromVertex(vertex3)
            );
        }

        public bool AddTriangleFromVertices(ITriplet<IGraphicsVertex3D> vertices)
        {
            return StoreTriangle(
                AddVertexFromVertex(vertices.Item1), 
                AddVertexFromVertex(vertices.Item2), 
                AddVertexFromVertex(vertices.Item3)
            );
        }


        public GraphicsTrianglesListGeometryComposer3D AddTriangles(IEnumerable<ITriangle3D> trianglesList)
        {
            foreach (var triangle in trianglesList)
            {
                var vertex1 = AddVertexFromPoint(triangle.GetPoint1());
                var vertex2 = AddVertexFromPoint(triangle.GetPoint2());
                var vertex3 = AddVertexFromPoint(triangle.GetPoint3());

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesListGeometryComposer3D AddTrianglesFromPoints(IEnumerable<ITriplet<ITuple3D>> trianglePointsList)
        {
            foreach (var points in trianglePointsList)
            {
                var vertex1 = AddVertexFromPoint(points.Item1);
                var vertex2 = AddVertexFromPoint(points.Item2);
                var vertex3 = AddVertexFromPoint(points.Item3);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesListGeometryComposer3D AddTrianglesFromPoints(IReadOnlyList<ITuple3D> pointsList)
        {
            if (pointsList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < pointsList.Count; i += 3)
            {
                var vertex1 = AddVertexFromPoint(pointsList[i]);
                var vertex2 = AddVertexFromPoint(pointsList[i + 1]);
                var vertex3 = AddVertexFromPoint(pointsList[i + 2]);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesListGeometryComposer3D AddTrianglesFromVertices(IEnumerable<ITriplet<IGraphicsVertex3D>> triangleVerticesList)
        {
            foreach (var vertices in triangleVerticesList)
            {
                var vertex1 = AddVertexFromVertex(vertices.Item1);
                var vertex2 = AddVertexFromVertex(vertices.Item2);
                var vertex3 = AddVertexFromVertex(vertices.Item3);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesListGeometryComposer3D AddTrianglesFromVertices(IReadOnlyList<IGraphicsVertex3D> verticesList)
        {
            if (verticesList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < verticesList.Count; i += 3)
            {
                var vertex1 = AddVertexFromVertex(verticesList[i]);
                var vertex2 = AddVertexFromVertex(verticesList[i + 1]);
                var vertex3 = AddVertexFromVertex(verticesList[i + 2]);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }


        public GraphicsTrianglesListGeometry3D GenerateGeometry()
        {
            var geometry = 
                GraphicsTrianglesListGeometry3D.Create(TriangleVertexPoints);

            if (GenerateTextureUVs)
                geometry.SetVertexUVs(TriangleVertexTextureUVs.ToArray());

            if (GenerateColors)
                geometry.SetVertexColors(TriangleVertexColors.ToArray());

            if (GenerateNormals)
                geometry.SetVertexNormals(TriangleVertexNormals.ToArray());

            return geometry;
        }
    }
}