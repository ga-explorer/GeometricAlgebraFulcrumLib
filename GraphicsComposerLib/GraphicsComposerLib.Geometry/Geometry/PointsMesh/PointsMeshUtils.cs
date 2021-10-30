using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;
using EuclideanGeometryLib.GraphicsGeometry.Composers;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Textures;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;
using GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space2D;
using GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh
{
    public static class PointsMeshUtils
    {
        public static int GetMeshPointIndex(this IPointsMesh3D baseMesh, int pointIndex1, int pointIndex2)
        {
            pointIndex1 = pointIndex1.Mod(baseMesh.Count1);
            pointIndex2 = pointIndex2.Mod(baseMesh.Count2);

            return pointIndex1 + pointIndex2 * baseMesh.Count1;
        }

        public static Pair<int> GetPointIndexPair(this IPointsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.Count);
            var pointIndex1 = meshPointIndex % baseMesh.Count1;
            var pointIndex2 = (meshPointIndex - pointIndex1) / baseMesh.Count1;

            return new Pair<int>(pointIndex1, pointIndex2);
        }


        public static ITuple3D GetPoint(this IPointsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.Count);
            var pointIndex2 = meshPointIndex % baseMesh.Count1;
            var pointIndex1 = (meshPointIndex - pointIndex2) / baseMesh.Count1;

            return baseMesh[pointIndex1, pointIndex2];
        }

        public static ITuple3D GetPoint(this IPointsMesh3D baseMesh, int pointIndex1, int pointIndex2)
        {
            return baseMesh[pointIndex1, pointIndex2];
        }

        public static PointsMeshPoint3D GetMeshPoint(this IPointsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.Count);
            var pointIndex2 = meshPointIndex % baseMesh.Count1;
            var pointIndex1 = (meshPointIndex - pointIndex2) / baseMesh.Count1;

            return new PointsMeshPoint3D(baseMesh, pointIndex1, pointIndex2);
        }

        public static PointsMeshPoint3D GetMeshPoint(this IPointsMesh3D baseMesh, int pointIndex1, int pointIndex2)
        {
            return new PointsMeshPoint3D(baseMesh, pointIndex1, pointIndex2);
        }

        public static IGraphicsVertex3D GetVertex(this IPointsMesh3D baseMesh, int meshPointIndex, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.Count);
            var pointIndex2 = meshPointIndex % baseMesh.Count1;
            var pointIndex1 = (meshPointIndex - pointIndex2) / baseMesh.Count1;

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pointIndex1, pointIndex2],
                textureCoordinatesGrid[pointIndex1, pointIndex2]
            );
        }

        public static IGraphicsVertex3D GetVertex(this IPointsMesh3D baseMesh, int pointIndex1, int pointIndex2, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            var meshPointIndex = 
                baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2);

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pointIndex1, pointIndex2],
                textureCoordinatesGrid[pointIndex1, pointIndex2]
            );
        }


        public static IEnumerable<PointsMeshPoint2D> GetMeshPoints(this IPointsMesh2D baseMesh)
        {
            for (var pointIndex1 = 0; pointIndex1 < baseMesh.Count1; pointIndex1++)
            for (var pointIndex2 = 0; pointIndex2 < baseMesh.Count2; pointIndex2++)
                yield return new PointsMeshPoint2D(
                    baseMesh,
                    pointIndex1,
                    pointIndex2
                );
        }
        
        public static IEnumerable<PointsMeshPoint3D> GetMeshPoints(this IPointsMesh3D baseMesh)
        {
            for (var pointIndex1 = 0; pointIndex1 < baseMesh.Count1; pointIndex1++)
            for (var pointIndex2 = 0; pointIndex2 < baseMesh.Count2; pointIndex2++)
                yield return new PointsMeshPoint3D(
                    baseMesh,
                    pointIndex1,
                    pointIndex2
                );
        }

        public static IEnumerable<ITuple3D> GetPoints(this IPointsMesh3D mesh)
        {
            return mesh;
        }

        public static IEnumerable<IGraphicsVertex3D> GetVertices(this IPointsMesh3D mesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return mesh
                .GetMeshPoints()
                .Select(t => new GraphicsTexturedVertex3D(
                    t.MeshPointIndex,
                    t.Point,
                    textureCoordinatesGrid[t.PointIndex1, t.PointIndex2]
                ));
        }

        
        public static IEnumerable<IQuad<ITuple3D>> GetPointQuads(this IPointsMesh3D baseMesh)
        {
            for (var pointIndex1 = 1; pointIndex1 < baseMesh.Count1; pointIndex1++)
            for (var pointIndex2 = 1; pointIndex2 < baseMesh.Count2; pointIndex2++)
                yield return new Quad<ITuple3D>(
                    baseMesh.GetPoint(pointIndex1 - 1, pointIndex2 - 1),
                    baseMesh.GetPoint(pointIndex1 - 1, pointIndex2),
                    baseMesh.GetPoint(pointIndex1, pointIndex2 - 1),
                    baseMesh.GetPoint(pointIndex1, pointIndex2)
                );
        }
        
        public static IEnumerable<IQuad<PointsMeshPoint3D>> GetMeshPointQuads(this IPointsMesh3D baseMesh)
        {
            for (var pointIndex1 = 1; pointIndex1 < baseMesh.Count1; pointIndex1++)
            for (var pointIndex2 = 1; pointIndex2 < baseMesh.Count2; pointIndex2++)
                yield return new Quad<PointsMeshPoint3D>(
                    baseMesh.GetMeshPoint(pointIndex1 - 1, pointIndex2 - 1),
                    baseMesh.GetMeshPoint(pointIndex1 - 1, pointIndex2),
                    baseMesh.GetMeshPoint(pointIndex1, pointIndex2 - 1),
                    baseMesh.GetMeshPoint(pointIndex1, pointIndex2)
                );
        }
        
        public static IEnumerable<IQuad<int>> GetMeshPointIndexQuads(this IPointsMesh3D baseMesh)
        {
            for (var pointIndex1 = 1; pointIndex1 < baseMesh.Count1; pointIndex1++)
            for (var pointIndex2 = 1; pointIndex2 < baseMesh.Count2; pointIndex2++)
                yield return new Quad<int>(
                    baseMesh.GetMeshPointIndex(pointIndex1 - 1, pointIndex2 - 1),
                    baseMesh.GetMeshPointIndex(pointIndex1 - 1, pointIndex2),
                    baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2 - 1),
                    baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2)
                );
        }

        public static IEnumerable<IQuad<IGraphicsVertex3D>> GetVertexQuads(this IPointsMesh3D baseMesh, GraphicsTextureCoordinatesGrid texturesCoordinatesGrid)
        {
            var textureURange = texturesCoordinatesGrid.TextureURange;
            var textureVRange = texturesCoordinatesGrid.TextureVRange;

            for (var pointIndex1 = 1; pointIndex1 < baseMesh.Count1; pointIndex1++)
            {
                var i1 = pointIndex1 - 1;
                var i2 = pointIndex1;

                var u1 = textureURange[i1];
                var u2 = textureURange[i2];

                for (var pointIndex2 = 1; pointIndex2 < baseMesh.Count2; pointIndex2++)
                {
                    var j1 = pointIndex2 - 1;
                    var j2 = pointIndex2;

                    var v1 = textureVRange[j1];
                    var v2 = textureVRange[j2];

                    var texturedPoint1 = new GraphicsTexturedVertex3D(
                        baseMesh.GetMeshPointIndex(i1, j1),
                        baseMesh.GetPoint(i1, j1),
                        new Tuple2D(u1, v1)
                    );

                    var texturedPoint2 = new GraphicsTexturedVertex3D(
                        baseMesh.GetMeshPointIndex(i1, j2),
                        baseMesh.GetPoint(i1, j2),
                        new Tuple2D(u1, v2)
                    );

                    var texturedPoint3 = new GraphicsTexturedVertex3D(
                        baseMesh.GetMeshPointIndex(i2, j1),
                        baseMesh.GetPoint(i2, j1),
                        new Tuple2D(u2, v1)
                    );

                    var texturedPoint4 = new GraphicsTexturedVertex3D(
                        baseMesh.GetMeshPointIndex(i2, j2),
                        baseMesh.GetPoint(i2, j2),
                        new Tuple2D(u2, v2)
                    );

                    yield return new Quad<GraphicsTexturedVertex3D>(
                        texturedPoint1,
                        texturedPoint2,
                        texturedPoint3,
                        texturedPoint4
                    );
                }
            }
        }

        
        public static PartialPointsMesh3D Close(this IPointsMesh3D baseMesh, bool close1, bool close2)
        {
            var count1 = close1 ? (baseMesh.Count1 + 1) : (baseMesh.Count1);
            var count2 = close2 ? (baseMesh.Count2 + 1) : (baseMesh.Count2);

            var range1 = new IndexMapRange1D(count1);
            var range2 = new IndexMapRange1D(count2);

            return new PartialPointsMesh3D(baseMesh, range1, range2);
        }

        public static PartialPointsMesh3D Reverse(this IPointsMesh3D baseMesh, bool reverse1, bool reverse2)
        {
            var range1 = new IndexMapRange1D(baseMesh.Count1);
            var range2 = new IndexMapRange1D(baseMesh.Count2);

            if (reverse1) range1.ReverseDirection();
            if (reverse2) range2.ReverseDirection();

            return new PartialPointsMesh3D(baseMesh, range1, range2);
        }

        
        public static IEnumerable<Pair<int>> GetLineVerticesIndices(this IPointsMesh2D baseMesh, bool generateDimension1 = true, bool generateDimension2 = true)
        {
            if (generateDimension1)
            {
                for (var index1 = 0; index1 < baseMesh.Count1; index1++)
                for (var index22 = 1; index22 < baseMesh.Count2; index22++)
                {
                    var index21 = index22 - 1;

                    yield return new Pair<int>(
                        baseMesh.GetItemIndex(index1, index21),
                        baseMesh.GetItemIndex(index1, index22)
                    );
                }
            }

            if (generateDimension2)
            {
                for (var index2 = 0; index2 < baseMesh.Count2; index2++)
                for (var index12 = 1; index12 < baseMesh.Count1; index12++)
                {
                    var index11 = index12 - 1;

                    yield return new Pair<int>(
                        baseMesh.GetItemIndex(index11, index2),
                        baseMesh.GetItemIndex(index12, index2)
                    );
                }
            }
        }

        public static IEnumerable<Pair<int>> GetLineVerticesIndices(this IPointsMesh3D baseMesh, bool generateDimension1 = true, bool generateDimension2 = true)
        {
            if (generateDimension1)
            {
                for (var index1 = 0; index1 < baseMesh.Count1; index1++)
                for (var index22 = 1; index22 < baseMesh.Count2; index22++)
                {
                    var index21 = index22 - 1;

                    yield return new Pair<int>(
                        baseMesh.GetItemIndex(index1, index21),
                        baseMesh.GetItemIndex(index1, index22)
                    );
                }
            }

            if (generateDimension2)
            {
                for (var index2 = 0; index2 < baseMesh.Count2; index2++)
                for (var index12 = 1; index12 < baseMesh.Count1; index12++)
                {
                    var index11 = index12 - 1;

                    yield return new Pair<int>(
                        baseMesh.GetItemIndex(index11, index2),
                        baseMesh.GetItemIndex(index12, index2)
                    );
                }
            }
        }


        public static IEnumerable<ILineSegment2D> GetLines(this IPointsMesh2D baseMesh, bool generateDimension1 = true, bool generateDimension2 = true)
        {
            if (generateDimension1)
            {
                for (var index1 = 0; index1 < baseMesh.Count1; index1++)
                for (var index22 = 1; index22 < baseMesh.Count2; index22++)
                {
                    var index21 = index22 - 1;

                    yield return LineSegment2D.Create(
                        baseMesh[index1, index21],
                        baseMesh[index1, index22]
                    );
                }
            }

            if (generateDimension2)
            {
                for (var index2 = 0; index2 < baseMesh.Count2; index2++)
                for (var index12 = 1; index12 < baseMesh.Count1; index12++)
                {
                    var index11 = index12 - 1;

                    yield return LineSegment2D.Create(
                        baseMesh[index11, index2],
                        baseMesh[index12, index2]
                    );
                }
            }
        }

        public static IEnumerable<ILineSegment3D> GetLines(this IPointsMesh3D baseMesh, bool generateDimension1 = true, bool generateDimension2 = true)
        {
            if (generateDimension1)
            {
                for (var index1 = 0; index1 < baseMesh.Count1; index1++)
                for (var index22 = 1; index22 < baseMesh.Count2; index22++)
                {
                    var index21 = index22 - 1;

                    yield return LineSegment3D.Create(
                        baseMesh[index1, index21],
                        baseMesh[index1, index22]
                    );
                }
            }

            if (generateDimension2)
            {
                for (var index2 = 0; index2 < baseMesh.Count2; index2++)
                for (var index12 = 1; index12 < baseMesh.Count1; index12++)
                {
                    var index11 = index12 - 1;

                    yield return LineSegment3D.Create(
                        baseMesh[index11, index2],
                        baseMesh[index12, index2]
                    );
                }
            }
        }

        public static IEnumerable<ITriangle3D> GetTriangles(this IPointsMesh3D baseMesh)
        {
            foreach (var pointQuad in baseMesh.GetPointQuads())
            {
                yield return Triangle3D.Create(
                    pointQuad.Item3,
                    pointQuad.Item1,
                    pointQuad.Item4
                );

                yield return Triangle3D.Create(
                    pointQuad.Item2,
                    pointQuad.Item4,
                    pointQuad.Item1
                );
            }
        }

        public static IEnumerable<ITriplet<int>> GetTriangleVertexIndices(this IPointsMesh3D baseMesh)
        {
            foreach (var indexQuad in baseMesh.GetMeshPointIndexQuads())
            {
                yield return new Triplet<int>(
                    indexQuad.Item3,
                    indexQuad.Item1,
                    indexQuad.Item4
                );

                yield return new Triplet<int>(
                    indexQuad.Item2,
                    indexQuad.Item4,
                    indexQuad.Item1
                );
            }
        }

        public static IEnumerable<ITriplet<IGraphicsVertex3D>> GetTriangleVertices(this IPointsMesh3D baseMesh, GraphicsTextureCoordinatesGrid texturesCoordinatesGrid)
        {
            foreach (var indexQuad in baseMesh.GetVertexQuads(texturesCoordinatesGrid))
            {
                yield return new Triplet<IGraphicsVertex3D>(
                    indexQuad.Item3,
                    indexQuad.Item1,
                    indexQuad.Item4
                );

                yield return new Triplet<IGraphicsVertex3D>(
                    indexQuad.Item2,
                    indexQuad.Item4,
                    indexQuad.Item1
                );
            }
        }


        public static GraphicsTextureCoordinatesGrid GetTextureCoordinatesGrid(this IPointsMesh3D baseMesh)
        {
            var textureURange = new PSeqLinearDouble1D(
                0, 
                1, 
                baseMesh.Count1
            );

            var textureVRange = new PSeqLinearDouble1D(
                0, 
                1, 
                baseMesh.Count2
            );

            return new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }

        public static GraphicsTextureCoordinatesGrid GetTextureCoordinatesGrid(this IPointsMesh3D baseMesh, double firstUValue, double lastUValue, double firstVValue, double lastVValue)
        {
            var textureURange = new PSeqLinearDouble1D(
                firstUValue, 
                lastUValue, 
                baseMesh.Count1
            );

            var textureVRange = new PSeqLinearDouble1D(
                firstVValue, 
                lastVValue, 
                baseMesh.Count2
            );
            
            return new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }

        public static TexturedPointsMesh3D ToTexturedMesh(this IPointsMesh3D baseMesh)
        {
            return new TexturedPointsMesh3D(
                baseMesh,
                0,
                1,
                0,
                1
            );
        }

        public static TexturedPointsMesh3D ToTexturedMesh(this IPointsMesh3D baseMesh, double firstUValue, double lastUValue, double firstVValue, double lastVValue)
        {
            return new TexturedPointsMesh3D(
                baseMesh,
                firstUValue,
                lastUValue,
                firstVValue,
                lastVValue
            );
        }


        public static GraphicsLinesGeometry3D GetGraphicsLinesGeometry(this IPointsMesh3D baseMesh, bool generateFromPaths, bool generateFromPathPoints)
        {
            var geometry = 
                GraphicsLinesGeometry3D.Create(baseMesh.GetPoints());

            if (generateFromPaths)
            {
                for (var pointIndex1 = 0; pointIndex1 < baseMesh.Count1; pointIndex1++)
                for (var pointIndex2 = 1; pointIndex2 < baseMesh.Count2; pointIndex2++)
                    geometry.AppendLine(
                        baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2 - 1),
                        baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2)
                    );
            }

            if (generateFromPathPoints)
            {
                for (var pointIndex2 = 0; pointIndex2 < baseMesh.Count2; pointIndex2++)
                for (var pointIndex1 = 1; pointIndex1 < baseMesh.Count1; pointIndex1++)
                    geometry.AppendLine(
                        baseMesh.GetMeshPointIndex(pointIndex1 - 1, pointIndex2),
                        baseMesh.GetMeshPointIndex(pointIndex1, pointIndex2)
                    );
            }

            return geometry;
        }

        public static GraphicsTrianglesGeometry3D GetGraphicsTrianglesGeometry(this IPointsMesh3D baseMesh, GraphicsVertexNormalComputationMethod normalComputationMethod, bool reverseNormals)
        {
            var pointsList =
                baseMesh.GetPoints();

            var geometry = 
                GraphicsTrianglesGeometry3D.Create(pointsList);

            geometry.NormalComputationMethod = normalComputationMethod;

            geometry.AppendTriangles(
                baseMesh.GetTriangleVertexIndices()
            );

            geometry.ComputeVertexNormals(reverseNormals);

            return geometry;
        }

        public static GraphicsTrianglesGeometry3D GetGraphicsTrianglesGeometry(this IPointsMesh3D baseMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid, GraphicsVertexNormalComputationMethod normalComputationMethod, bool reverseNormals)
        {
            var verticesList = 
                baseMesh.GetVertices(textureCoordinatesGrid).ToArray();

            var geometry = 
                GraphicsTrianglesGeometry3D.Create(
                    verticesList,
                    verticesList[0].DataKind
                );

            geometry.NormalComputationMethod = normalComputationMethod;

            geometry.AppendTriangles(
                baseMesh.GetTriangleVertexIndices()
            );

            geometry.ComputeVertexNormals(reverseNormals);

            return geometry;
        }
        

        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, IPointsMesh3D pointsMesh)
        {
            return composer.AddTriangles(
                pointsMesh.GetTriangles()
            );
        }

        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, IPointsMesh3D mesh, double firstTextureU, double lastTextureU, double firstTextureV, double lastTextureV)
        {
            var textureURange =
                PeriodicSequenceUtils.CreateLinearDoubleSequence(
                    firstTextureU, 
                    lastTextureU, 
                    mesh.Count1
                );

            var textureVRange =
                PeriodicSequenceUtils.CreateLinearDoubleSequence(
                    firstTextureV, 
                    lastTextureV, 
                    mesh.Count2
                );

            //TODO: Correct this to add texture information
            composer.AddTriangles(
                mesh.GetTriangles()
            );

            return composer;
        }

        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, IPointsMesh3D pointsMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return composer.AddTrianglesFromVertices(
                pointsMesh.GetTriangleVertices(textureCoordinatesGrid)
            );
        }

        public static GraphicsTrianglesGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesGeometryComposer3D composer, IPointsMesh3D pointsMesh)
        {
            return composer.AddTriangles(
                pointsMesh.GetTriangles()
            );
        }

        public static GraphicsTrianglesGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesGeometryComposer3D composer, IPointsMesh3D pointsMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return composer.AddTrianglesFromVertices(
                pointsMesh.GetTriangleVertices(textureCoordinatesGrid)
            );
        }
    }
}
