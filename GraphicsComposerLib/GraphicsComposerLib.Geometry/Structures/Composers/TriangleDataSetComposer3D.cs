using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Triangles;
using GraphicsComposerLib.Geometry.Primitives.Triangles;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using GraphicsComposerLib.Geometry.Structures.Data;
using GraphicsComposerLib.Geometry.Structures.Faces;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Composers
{
    public class TriangleDataSetComposer3D
    {
        public PointDataSet3D<IGraphicsVertexData3D> PointDataSet { get; }
            = new PointDataSet3D<IGraphicsVertexData3D>();

        public TriangleDataSet3D<IGraphicsFaceData3D> TriangleDataSet { get; }
            = new TriangleDataSet3D<IGraphicsFaceData3D>();


        public double DistanceEpsilon { get; set; }
            = 1e-7d;

        public bool AllowSmallTriangles { get; set; }

        public bool GenerateTextureUVs { get; set; }

        public bool GenerateColors { get; set; }

        public bool GenerateNormals { get; set; }

        public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
            = GrVertexNormalComputationMethod.WeightedNormals;

        public bool ReverseNormals { get; set; }

        public IEnumerable<PointData3D<IGraphicsVertexData3D>> Vertices
            => PointDataSet;

        public IEnumerable<ITuple3D> VertexPoints
            => PointDataSet;

        public IEnumerable<ITuple2D> VertexTextureUVs
            => PointDataSet.Select(p => p.DataValue.TextureUv);

        public IEnumerable<Color> VertexColors
            => PointDataSet.Select(p => p.DataValue.Color);

        public IEnumerable<GrNormal3D> VertexNormals
            => PointDataSet.Select(p => p.DataValue.Normal);

        public IEnumerable<Triplet<int>> TriangleVertexIndices 
            => TriangleDataSet.Triangles;

        public IEnumerable<Triplet<IGraphicsVertexData3D>> TriangleVertices
            => TriangleDataSet.Triangles.Select(t => 
                new Triplet<IGraphicsVertexData3D>(
                    PointDataSet[t.Item1].DataValue,
                    PointDataSet[t.Item2].DataValue,
                    PointDataSet[t.Item3].DataValue
                )
            );
        
        public IEnumerable<Triplet<ITuple3D>> TriangleVertexPoints
            => TriangleDataSet.Triangles.Select(t => 
                new Triplet<ITuple3D>(
                    PointDataSet[t.Item1],
                    PointDataSet[t.Item2],
                    PointDataSet[t.Item3]
                )
            );

        public IEnumerable<Triplet<ITuple2D>> TriangleVertexTextureUVs
            => TriangleDataSet.Triangles.Select(t => 
                new Triplet<ITuple2D>(
                    PointDataSet[t.Item1].DataValue.TextureUv,
                    PointDataSet[t.Item2].DataValue.TextureUv,
                    PointDataSet[t.Item3].DataValue.TextureUv
                )
            );
        
        public IEnumerable<Triplet<Color>> TriangleVertexColors
            => TriangleDataSet.Triangles.Select(t => 
                new Triplet<Color>(
                    PointDataSet[t.Item1].DataValue.Color,
                    PointDataSet[t.Item2].DataValue.Color,
                    PointDataSet[t.Item3].DataValue.Color
                )
            );

        public IEnumerable<Triplet<GrNormal3D>> TriangleVertexNormals
            => TriangleDataSet.Triangles.Select(t => 
                new Triplet<GrNormal3D>(
                    PointDataSet[t.Item1].DataValue.Normal,
                    PointDataSet[t.Item2].DataValue.Normal,
                    PointDataSet[t.Item3].DataValue.Normal
                )
            );

        public int VerticesCount
            => PointDataSet.Count;
        
        public int TrianglesCount
            => TriangleDataSet.Count;


        public TriangleDataSetComposer3D()
        {
        }


        private IGraphicsVertexData3D CreateVertexData()
        {
            if (GenerateNormals)
            {
                if (GenerateColors)
                    return new GraphicsNormalColorVertexData3D();

                if (GenerateTextureUVs)
                    return new GraphicsNormalTextureVertexData3D();

                return new GraphicsNormalVertexData3D();
            }

            if (GenerateColors)
                return new GraphicsColorVertexData3D();

            if (GenerateTextureUVs)
                return new GraphicsTextureVertexData3D();

            return GraphicsVoidVertexData3D.DefaultData;
        }

        private IGraphicsVertexData3D CreateVertexData(IGraphicsVertexData3D vertexData)
        {
            if (GenerateNormals)
            {
                if (GenerateColors)
                    return new GraphicsNormalColorVertexData3D(vertexData);

                if (GenerateTextureUVs)
                    return new GraphicsNormalTextureVertexData3D(vertexData);

                return new GraphicsNormalVertexData3D(vertexData);
            }

            if (GenerateColors)
                return new GraphicsColorVertexData3D(vertexData);

            if (GenerateTextureUVs)
                return new GraphicsTextureVertexData3D(vertexData);

            return GraphicsVoidVertexData3D.DefaultData;
        }
        
        private void UpdateVertexNormals(PointData3D<IGraphicsVertexData3D> vertex1, PointData3D<IGraphicsVertexData3D> vertex2, PointData3D<IGraphicsVertexData3D> vertex3)
        {
            if (NormalComputationMethod == GrVertexNormalComputationMethod.WeightedNormals)
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
                    normal.IsValid()
                );

                // Update normals of triangle vertices.
                // See here for more information:
                // http://www.bytehazard.com/articles/vertnorm.html
                // normal.MakeUnitVector();
                vertex1.DataValue.Normal.Update(angle1 * normal);
                vertex2.DataValue.Normal.Update(angle2 * normal);
                vertex3.DataValue.Normal.Update(angle3 * normal);

                return;
            }
            
            if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageUnitNormals)
            {
                //Find triangle unit normal
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);
                
                //For debugging only
                Debug.Assert(normal.IsValid());

                vertex1.DataValue.Normal.Update(normal);
                vertex2.DataValue.Normal.Update(normal);
                vertex3.DataValue.Normal.Update(normal);

                return;
            }
            
            if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageNormals)
            {
                //Find triangle normal, not unit but full normal vector
                var normal = 
                    ReverseNormals
                        ? VectorAlgebraUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                        : VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(normal.IsValid());

                vertex1.DataValue.Normal.Update(normal);
                vertex2.DataValue.Normal.Update(normal);
                vertex3.DataValue.Normal.Update(normal);

                return;
            }
        }

        private bool StoreTriangle(PointData3D<IGraphicsVertexData3D> vertex1, PointData3D<IGraphicsVertexData3D> vertex2, PointData3D<IGraphicsVertexData3D> vertex3)
        {
            // Duplicate triangles are not allowed
            if (TriangleDataSet.ContainsTriangle(vertex1.PointIndex, vertex2.PointIndex, vertex3.PointIndex))
                return false;

            // Make sure all side lengths are far enough from zero
            if (!AllowSmallTriangles)
            {
                if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
                if (vertex3.GetDistanceToPoint(vertex2) < DistanceEpsilon) return false;
                if (vertex1.GetDistanceToPoint(vertex3) < DistanceEpsilon) return false;
            }

            // Add triangle vertex indices to indices list
            TriangleDataSet.AddTriangle(vertex1.PointIndex, vertex2.PointIndex, vertex3.PointIndex, GraphicsVoidFaceData3D.DefaultData);

            // Update vertex normals
            if (GenerateNormals)
                UpdateVertexNormals(vertex1, vertex2, vertex3);

            return true;
        }


        public TriangleDataSetComposer3D Clear()
        {
            PointDataSet.Clear();
            TriangleDataSet.Clear();

            return this;
        }

        public TriangleDataSetComposer3D BeginBatch()
        {
            PointDataSet.BeginBatch();

            return this;
        }

        public TriangleDataSetComposer3D EndBatch()
        {
            if (GenerateNormals)
                PointDataSet.EndBatch(dataValue => dataValue.Normal.MakeUnit());
            else
                PointDataSet.EndBatch();

            return this;
        }


        public PointData3D<IGraphicsVertexData3D> AddVertex(double x, double y, double z)
        {
            return PointDataSet.TryGetPointData(x, y, z, out var pointData) 
                ? pointData 
                : PointDataSet.AddPoint(x, y, z, CreateVertexData());
        }

        public PointData3D<IGraphicsVertexData3D> AddVertex(ITuple3D point)
        {
            return PointDataSet.TryGetPointData(point, out var pointData) 
                ? pointData 
                : PointDataSet.AddPoint(point, CreateVertexData());
        }

        public PointData3D<IGraphicsVertexData3D> AddVertex(PointData3D<IGraphicsVertexData3D> vertex)
        {
            return PointDataSet.TryGetPointData(vertex, out var pointData) 
                ? pointData 
                : PointDataSet.AddPoint(vertex, CreateVertexData(vertex.DataValue));
        }


        public TriangleDataSetComposer3D AddVertices(params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                AddVertex(point);

            return this;
        }

        public TriangleDataSetComposer3D AddVertices(IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                AddVertex(point);

            return this;
        }

        public TriangleDataSetComposer3D AddVertices(IEnumerable<PointData3D<IGraphicsVertexData3D>> verticesList)
        {
            foreach (var point in verticesList)
                AddVertex(point);

            return this;
        }


        public bool AddTriangle(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex3 == vertexIndex1)
                return false;

            return StoreTriangle(
                PointDataSet[vertexIndex1], 
                PointDataSet[vertexIndex2], 
                PointDataSet[vertexIndex3]
            );
        }

        public bool AddTriangle(ITriplet<int> vertexIndices)
        {
            if (vertexIndices.Item1 == vertexIndices.Item2 || vertexIndices.Item2 == vertexIndices.Item3 || vertexIndices.Item3 == vertexIndices.Item1)
                return false;

            return StoreTriangle(
                PointDataSet[vertexIndices.Item1], 
                PointDataSet[vertexIndices.Item2], 
                PointDataSet[vertexIndices.Item3]
            );
        }

        public bool AddTriangle(ITuple3D point1, ITuple3D point2, ITuple3D point3)
        {
            return StoreTriangle(
                AddVertex(point1), 
                AddVertex(point2), 
                AddVertex(point3)
            );
        }

        public bool AddTriangle(ITriplet<ITuple3D> points)
        {
            return StoreTriangle(
                AddVertex(points.Item1), 
                AddVertex(points.Item2), 
                AddVertex(points.Item3)
            );
        }

        public bool AddTriangle(PointData3D<IGraphicsVertexData3D> vertex1, PointData3D<IGraphicsVertexData3D> vertex2, PointData3D<IGraphicsVertexData3D> vertex3)
        {
            return StoreTriangle(
                AddVertex(vertex1), 
                AddVertex(vertex2), 
                AddVertex(vertex3)
            );
        }

        public bool AddTriangle(ITriplet<PointData3D<IGraphicsVertexData3D>> vertices)
        {
            return StoreTriangle(
                AddVertex(vertices.Item1), 
                AddVertex(vertices.Item2), 
                AddVertex(vertices.Item3)
            );
        }

        public bool AddTriangle(ITriangle3D triangle)
        {
            return StoreTriangle(
                AddVertex(triangle.GetPoint1()), 
                AddVertex(triangle.GetPoint2()), 
                AddVertex(triangle.GetPoint3())
            );
        }


        public TriangleDataSetComposer3D AddTriangles(IEnumerable<ITriangle3D> trianglesList)
        {
            foreach (var triangle in trianglesList)
            {
                var vertex1 = AddVertex(triangle.GetPoint1());
                var vertex2 = AddVertex(triangle.GetPoint2());
                var vertex3 = AddVertex(triangle.GetPoint3());

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IEnumerable<ITriplet<int>> triangleIndicesList)
        {
            foreach (var indices in triangleIndicesList)
            {
                StoreTriangle(
                    PointDataSet[indices.Item1], 
                    PointDataSet[indices.Item2], 
                    PointDataSet[indices.Item3]
                );
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IReadOnlyList<int> indicesList)
        {
            if (indicesList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < indicesList.Count; i += 3)
            {
                StoreTriangle(
                    PointDataSet[indicesList[i]], 
                    PointDataSet[indicesList[i + 1]], 
                    PointDataSet[indicesList[i + 2]]
                );
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IReadOnlyList<ITuple3D> pointsList)
        {
            if (pointsList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < pointsList.Count; i += 3)
            {
                StoreTriangle(
                    AddVertex(pointsList[i]), 
                    AddVertex(pointsList[i + 1]), 
                    AddVertex(pointsList[i + 2])
                );
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IEnumerable<ITriplet<ITuple3D>> trianglePointsList)
        {
            foreach (var points in trianglePointsList)
            {
                StoreTriangle(
                    AddVertex(points.Item1), 
                    AddVertex(points.Item2), 
                    AddVertex(points.Item3)
                );
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IReadOnlyList<PointData3D<IGraphicsVertexData3D>> verticesList)
        {
            if (verticesList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < verticesList.Count; i += 3)
            {
                StoreTriangle(
                    AddVertex(verticesList[i]), 
                    AddVertex(verticesList[i + 1]), 
                    AddVertex(verticesList[i + 2])
                );
            }

            return this;
        }

        public TriangleDataSetComposer3D AddTriangles(IEnumerable<ITriplet<PointData3D<IGraphicsVertexData3D>>> triangleVerticesList)
        {
            foreach (var vertices in triangleVerticesList)
            {
                StoreTriangle(
                    AddVertex(vertices.Item1), 
                    AddVertex(vertices.Item2), 
                    AddVertex(vertices.Item3)
                );
            }

            return this;
        }


        public GrTriangleGeometry3D GenerateGeometry()
        {
            var geometry = 
                GrTriangleGeometry3D.Create(
                    VertexPoints, 
                    TriangleDataSet.GetIndicesArray()
                );
            
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