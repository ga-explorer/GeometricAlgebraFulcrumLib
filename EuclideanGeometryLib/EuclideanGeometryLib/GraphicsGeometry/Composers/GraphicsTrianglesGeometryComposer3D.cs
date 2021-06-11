using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Composers
{
    /// <summary>
    /// This class can be used to incrementally build a graphics triangle geometry
    /// in 3D
    /// </summary>
    public class GraphicsTrianglesGeometryComposer3D
    {
        private readonly Dictionary3Keys<double, IGraphicsVertex3D> _verticesTable
            = new Dictionary3Keys<double, IGraphicsVertex3D>();

        private readonly List<IGraphicsVertex3D> _verticesList 
            = new List<IGraphicsVertex3D>();

        private readonly List<int> _indicesList
            = new List<int>();


        public double DistanceEpsilon { get; set; }
            = 1e-7d;

        public bool AllowDuplicatePoints { get; private set; }

        public bool AllowSmallTriangles { get; set; }

        public bool GenerateTextureUVs { get; set; }

        public bool GenerateColors { get; set; }

        public bool GenerateNormals { get; set; }

        public GraphicsVertexNormalComputationMethod NormalComputationMethod { get; set; }
            = GraphicsVertexNormalComputationMethod.WeightedNormals;

        public bool ReverseNormals { get; set; }

        public IEnumerable<IGraphicsVertex3D> Vertices
            => _verticesList;

        public IEnumerable<ITuple3D> VertexPoints
            => _verticesList.Select(p => p.Point);

        public IEnumerable<ITuple2D> VertexTextureUVs
            => _verticesList.Select(p => p.TextureUv);

        public IEnumerable<Color> VertexColors
            => _verticesList.Select(p => p.Color);

        public IEnumerable<IGraphicsNormal3D> VertexNormals
            => _verticesList.Select(p => p.Normal);

        public IEnumerable<int> TriangleVertexIndices
            => _indicesList;

        public IEnumerable<IGraphicsVertex3D> TriangleVertices
            => _indicesList.Select(i => _verticesList[i]);

        public IEnumerable<ITuple3D> TriangleVertexPoints
            => _indicesList.Select(i => _verticesList[i].Point);

        public IEnumerable<ITuple2D> TriangleVertexTextureUVs
            => _indicesList.Select(i => _verticesList[i].TextureUv);

        public IEnumerable<IGraphicsNormal3D> TriangleVertexNormals
            => _indicesList.Select(i => _verticesList[i].Normal);

        public int VerticesCount
            => _verticesList.Count;


        public GraphicsTrianglesGeometryComposer3D()
        {
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

        private void UpdateVertexNormals(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2, IGraphicsVertex3D vertex3)
        {
            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.WeightedNormals)
            {
                //Find inner angles of triangle
                var angle1 = vertex1.GetPointsAngle(vertex3, vertex2);
                var angle2 = vertex2.GetPointsAngle(vertex1, vertex3);
                var angle3 = vertex3.GetPointsAngle(vertex2, vertex1);

                //Find triangle normal, not unit but full normal vector
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                // For debugging only
                Debug.Assert(
                    !double.IsNaN(angle1) &&
                    !double.IsNaN(angle2) &&
                    !double.IsNaN(angle3) &&
                    normal.IsValid
                );

                // Update normals of triangle vertices.
                // See here for more information:
                // http://www.bytehazard.com/articles/vertnorm.html
                // normal.MakeUnitVector();
                vertex1.Normal.Update(angle1 * normal);
                vertex2.Normal.Update(angle2 * normal);
                vertex3.Normal.Update(angle3 * normal);

                return;
            }
            
            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.AverageUnitNormals)
            {
                //Find triangle unit normal
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);
                
                //For debugging only
                Debug.Assert(!normal.IsInvalid);

                vertex1.Normal.Update(normal);
                vertex2.Normal.Update(normal);
                vertex3.Normal.Update(normal);

                return;
            }
            
            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.AverageNormals)
            {
                //Find triangle normal, not unit but full normal vector
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(!normal.IsInvalid);

                vertex1.Normal.Update(normal);
                vertex2.Normal.Update(normal);
                vertex3.Normal.Update(normal);

                return;
            }
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

            //Add triangle vertex indices to indices list
            _indicesList.Add(vertex1.Index);
            _indicesList.Add(vertex2.Index);
            _indicesList.Add(vertex3.Index);

            //Update vertex normals
            if (GenerateNormals)
                UpdateVertexNormals(vertex1, vertex2, vertex3);

            return true;
        }


        public GraphicsTrianglesGeometryComposer3D Clear()
        {
            _verticesTable.Clear();
            _verticesList.Clear();
            _indicesList.Clear();

            return this;
        }


        public GraphicsTrianglesGeometryComposer3D BeginBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = false;

            return this;
        }

        public GraphicsTrianglesGeometryComposer3D EndBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = true;

            if (GenerateNormals)
                foreach (var vertex in Vertices)
                    vertex.Normal.MakeUnit();

            return this;
        }


        public IGraphicsVertex3D AddVertexFromPoint(double x, double y, double z)
        {
            IGraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        public IGraphicsVertex3D AddVertexFromPoint(ITuple3D point)
        {
            IGraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromPoint(point);

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromPoint(point);

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        public IGraphicsVertex3D AddVertexFromVertex(IGraphicsVertex3D vertex)
        {
            IGraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromVertex(vertex);

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            var x = vertex.X;
            var y = vertex.Y;
            var z = vertex.Z;

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromVertex(vertex);

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }


        public GraphicsTrianglesGeometryComposer3D AddVerticesFromPoints(params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GraphicsTrianglesGeometryComposer3D AddVerticesFromPoints(IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GraphicsTrianglesGeometryComposer3D AddVerticesFromVertices(IEnumerable<IGraphicsVertex3D> verticesList)
        {
            foreach (var point in verticesList)
                AddVertexFromVertex(point);

            return this;
        }


        public bool AddTriangleFromIndices(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex3 == vertexIndex1)
                return false;

            return StoreTriangle(
                _verticesList[vertexIndex1], 
                _verticesList[vertexIndex2], 
                _verticesList[vertexIndex3]
            );
        }

        public bool AddTriangleFromIndices(ITriplet<int> vertexIndices)
        {
            if (vertexIndices.Item1 == vertexIndices.Item2 || vertexIndices.Item2 == vertexIndices.Item3 || vertexIndices.Item3 == vertexIndices.Item1)
                return false;

            return StoreTriangle(
                _verticesList[vertexIndices.Item1], 
                _verticesList[vertexIndices.Item2], 
                _verticesList[vertexIndices.Item3]
            );
        }

        public bool AddTriangleFromPoints(ITuple3D point1, ITuple3D point2, ITuple3D point3)
        {
            return StoreTriangle(
                AddVertexFromPoint(point1.X, point1.Y, point1.Z), 
                AddVertexFromPoint(point2.X, point2.Y, point2.Z), 
                AddVertexFromPoint(point3.X, point3.Y, point3.Z)
            );
        }

        public bool AddTriangleFromPoints(ITriplet<ITuple3D> points)
        {
            return StoreTriangle(
                AddVertexFromPoint(points.Item1.X, points.Item1.Y, points.Item1.Z), 
                AddVertexFromPoint(points.Item2.X, points.Item2.Y, points.Item2.Z), 
                AddVertexFromPoint(points.Item3.X, points.Item3.Y, points.Item3.Z)
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

        public bool AddTriangle(ITriangle3D triangle)
        {
            return StoreTriangle(
                AddVertexFromPoint(triangle.GetPoint1()), 
                AddVertexFromPoint(triangle.GetPoint2()), 
                AddVertexFromPoint(triangle.GetPoint3())
            );
        }


        public GraphicsTrianglesGeometryComposer3D AddTriangles(IEnumerable<ITriangle3D> trianglesList)
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

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromIndices(IEnumerable<ITriplet<int>> triangleIndicesList)
        {
            foreach (var indices in triangleIndicesList)
            {
                var vertex1 = _verticesList[indices.Item1];
                var vertex2 = _verticesList[indices.Item2];
                var vertex3 = _verticesList[indices.Item3];

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromIndices(IReadOnlyList<int> indicesList)
        {
            if (indicesList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < indicesList.Count; i += 3)
            {
                var vertex1 = _verticesList[indicesList[i]];
                var vertex2 = _verticesList[indicesList[i + 1]];
                var vertex3 = _verticesList[indicesList[i + 2]];

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromPoints(IReadOnlyList<ITuple3D> pointsList)
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

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromPoints(IEnumerable<ITriplet<ITuple3D>> trianglePointsList)
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

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromVertices(IReadOnlyList<IGraphicsVertex3D> verticesList)
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

        public GraphicsTrianglesGeometryComposer3D AddTrianglesFromVertices(IEnumerable<ITriplet<IGraphicsVertex3D>> triangleVerticesList)
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


        public GraphicsTrianglesGeometry3D GenerateGeometry()
        {
            var geometry = 
                GraphicsTrianglesGeometry3D.Create(VertexPoints, _indicesList);
            
            if (GenerateTextureUVs)
                geometry.SetVertexUVs(VertexTextureUVs.ToArray());

            if (GenerateColors)
                geometry.SetVertexColors(VertexColors.ToArray());

            if (GenerateNormals)
            {
                EndBatch();

                geometry.SetVertexNormals(VertexNormals.ToArray());
            }

            return geometry;
        }
    }
}
