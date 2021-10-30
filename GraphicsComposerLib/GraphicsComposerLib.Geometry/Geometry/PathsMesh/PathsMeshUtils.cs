using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
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
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space2D;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh
{
    public static class PathsMeshUtils
    {
        public static int GetMeshPointIndex(this IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
        {
            pathIndex = pathIndex.Mod(baseMesh.Count);
            pathPointIndex = pathPointIndex.Mod(baseMesh.PathPointsCount);

            return pathPointIndex + pathIndex * baseMesh.PathPointsCount;
        }

        public static Pair<int> GetPointIndexPair(this IPathsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.MeshPointsCount);
            var pathIndex = meshPointIndex % baseMesh.Count;
            var pathVertexIndex = (meshPointIndex - pathIndex) / baseMesh.Count;

            return new Pair<int>(pathIndex, pathVertexIndex);
        }

        
        public static ITuple3D GetPoint(this IPathsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.MeshPointsCount);
            var pathPointIndex = meshPointIndex % baseMesh.PathPointsCount;
            var pathIndex = (meshPointIndex - pathPointIndex) / baseMesh.PathPointsCount;

            return baseMesh[pathIndex][pathPointIndex];
        }

        public static ITuple3D GetPoint(this IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
        {
            return baseMesh[pathIndex][pathPointIndex];
        }

        public static PathsMeshPoint3D GetMeshPoint(this IPathsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.MeshPointsCount);
            var pathPointIndex = meshPointIndex % baseMesh.PathPointsCount;
            var pathIndex = (meshPointIndex - pathPointIndex) / baseMesh.PathPointsCount;

            return new PathsMeshPoint3D(baseMesh, pathIndex, pathPointIndex);
        }

        public static PathsMeshPoint3D GetMeshPoint(this IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
        {
            return new PathsMeshPoint3D(baseMesh, pathIndex, pathPointIndex);
        }

        public static IGraphicsVertex3D GetVertex(this IPathsMesh3D baseMesh, int meshPointIndex, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.MeshPointsCount);
            var pathPointIndex = meshPointIndex % baseMesh.PathPointsCount;
            var pathIndex = (meshPointIndex - pathPointIndex) / baseMesh.PathPointsCount;

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pathIndex][pathPointIndex],
                textureCoordinatesGrid[pathIndex, pathPointIndex]
            );
        }

        public static IGraphicsVertex3D GetVertex(this TexturedPathsMesh3D baseMesh, int meshPointIndex)
        {
            meshPointIndex = meshPointIndex.Mod(baseMesh.MeshPointsCount);
            var pathPointIndex = meshPointIndex % baseMesh.PathPointsCount;
            var pathIndex = (meshPointIndex - pathPointIndex) / baseMesh.PathPointsCount;

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pathIndex][pathPointIndex],
                baseMesh.TextureCoordinatesGrid[pathIndex, pathPointIndex]
            );
        }

        public static IGraphicsVertex3D GetVertex(this IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            var meshPointIndex = 
                baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex);

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pathIndex][pathPointIndex],
                textureCoordinatesGrid[pathIndex, pathPointIndex]
            );
        }

        public static IGraphicsVertex3D GetVertex(this TexturedPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
        {
            var meshPointIndex = 
                baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex);

            return new GraphicsTexturedVertex3D(
                meshPointIndex,
                baseMesh[pathIndex][pathPointIndex],
                baseMesh.TextureCoordinatesGrid[pathIndex, pathPointIndex]
            );
        }


        public static IEnumerable<PathsMeshPoint2D> GetMeshPoints(this IPathsMesh2D baseMesh)
        {
            var pathIndex = 0;
            foreach (var path in baseMesh)
            {
                var pathPointIndex = 0;
                foreach (var pathPoint in path)
                {
                    yield return new PathsMeshPoint2D(
                        baseMesh,
                        pathIndex, 
                        pathPointIndex
                    );

                    pathPointIndex++;
                }

                pathIndex++;
            }
        }
        
        public static IEnumerable<PathsMeshPoint3D> GetMeshPoints(this IPathsMesh3D baseMesh)
        {
            var pathIndex = 0;
            foreach (var path in baseMesh)
            {
                var pathPointIndex = 0;
                foreach (var pathPoint in path)
                {
                    yield return new PathsMeshPoint3D(
                        baseMesh,
                        pathIndex, 
                        pathPointIndex
                    );

                    pathPointIndex++;
                }

                pathIndex++;
            }
        }

        public static IEnumerable<ITuple3D> GetPoints(this IPathsMesh3D mesh)
        {
            return mesh.SelectMany(p => p);
        }

        public static IEnumerable<IGraphicsVertex3D> GetVertices(this IPathsMesh3D mesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return mesh
                .GetMeshPoints()
                .Select(t => new GraphicsTexturedVertex3D(
                    t.MeshPointIndex,
                    t.Point,
                    textureCoordinatesGrid[t.PathIndex, t.PathPointIndex]
                ));
        }

        public static IEnumerable<IGraphicsVertex3D> GetVertices(this TexturedPathsMesh3D mesh)
        {
            return mesh
                .GetMeshPoints()
                .Select(t => new GraphicsTexturedVertex3D(
                    t.MeshPointIndex,
                    t.Point,
                    mesh.TextureCoordinatesGrid[t.PathIndex, t.PathPointIndex]
                ));
        }


        public static Pair<IPointsPath3D> GetPathPairAt(this IPathsMesh3D mesh, int pathIndex1, int pathIndex2)
        {
            return new Pair<IPointsPath3D>(
                mesh[pathIndex1], 
                mesh[pathIndex2]
            );
        }

        public static Pair<IPointsPath3D> GetPathPairAt(this IPathsMesh3D mesh, Pair<int> pathIndexPair)
        {
            return new Pair<IPointsPath3D>(
                mesh[pathIndexPair.Item1],
                mesh[pathIndexPair.Item2]
            );
        }


        public static Pair<ITuple3D> GetPointPair(this IPathsMesh3D mesh, int pathIndex1, int pathIndex2, int pathPointIndex)
        {
            return new Pair<ITuple3D>(
                mesh[pathIndex1][pathPointIndex],
                mesh[pathIndex2][pathPointIndex]
            );
        }

        public static Pair<ITuple3D> GetPointPair(this IPathsMesh3D mesh, int pathIndex1, int pathIndex2, int pathPointIndex1, int pathPointIndex2)
        {
            return new Pair<ITuple3D>(
                mesh[pathIndex1][pathPointIndex1],
                mesh[pathIndex2][pathPointIndex2]
            );
        }

        public static Pair<ITuple3D> GetPointPair(this IPathsMesh3D mesh, Pair<int> pathIndexPair, int pathPointIndex)
        {
            return new Pair<ITuple3D>(
                mesh[pathIndexPair.Item1][pathPointIndex],
                mesh[pathIndexPair.Item2][pathPointIndex]
            );
        }

        public static Pair<ITuple3D> GetPointPair(this IPathsMesh3D mesh, int pathIndex, Pair<int> pathPointIndexPair)
        {
            return mesh[pathIndex].GetPointsPair(pathPointIndexPair);
        }


        public static IEnumerable<IQuad<ITuple3D>> GetPointQuads(this IPathsMesh3D baseMesh)
        {
            for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
            for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                yield return new Quad<ITuple3D>(
                    baseMesh.GetPoint(pathIndex - 1, pathPointIndex - 1),
                    baseMesh.GetPoint(pathIndex - 1, pathPointIndex),
                    baseMesh.GetPoint(pathIndex, pathPointIndex - 1),
                    baseMesh.GetPoint(pathIndex, pathPointIndex)
                );
        }
        
        public static IEnumerable<IQuad<PathsMeshPoint3D>> GetMeshPointQuads(this IPathsMesh3D baseMesh)
        {
            for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
            for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                yield return new Quad<PathsMeshPoint3D>(
                    baseMesh.GetMeshPoint(pathIndex - 1, pathPointIndex - 1),
                    baseMesh.GetMeshPoint(pathIndex - 1, pathPointIndex),
                    baseMesh.GetMeshPoint(pathIndex, pathPointIndex - 1),
                    baseMesh.GetMeshPoint(pathIndex, pathPointIndex)
                );
        }
        
        public static IEnumerable<IQuad<int>> GetMeshPointIndexQuads(this IPathsMesh3D baseMesh)
        {
            for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
            for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                yield return new Quad<int>(
                    baseMesh.GetMeshPointIndex(pathIndex - 1, pathPointIndex - 1),
                    baseMesh.GetMeshPointIndex(pathIndex - 1, pathPointIndex),
                    baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex - 1),
                    baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex)
                );
        }

        public static IEnumerable<IQuad<IGraphicsVertex3D>> GetVertexQuads(this IPathsMesh3D baseMesh, GraphicsTextureCoordinatesGrid texturesCoordinatesGrid)
        {
            var textureURange = texturesCoordinatesGrid.TextureURange;
            var textureVRange = texturesCoordinatesGrid.TextureVRange;

            for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
            {
                var i1 = pathIndex - 1;
                var i2 = pathIndex;

                var u1 = textureURange[i1];
                var u2 = textureURange[i2];

                for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                {
                    var j1 = pathPointIndex - 1;
                    var j2 = pathPointIndex;

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

        public static IEnumerable<IQuad<IGraphicsVertex3D>> GetVertexQuads(this TexturedPathsMesh3D baseMesh)
        {
            var textureURange = baseMesh.TextureURange;
            var textureVRange = baseMesh.TextureVRange;

            for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
            {
                var i1 = pathIndex - 1;
                var i2 = pathIndex;

                var u1 = textureURange[i1];
                var u2 = textureURange[i2];

                for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                {
                    var j1 = pathPointIndex - 1;
                    var j2 = pathPointIndex;

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

        
        public static IEnumerable<PartialPathsMesh3D> GetPathStrips(this IPathsMesh3D baseMesh)
        {
            for (var pathIndex2 = 1; pathIndex2 < baseMesh.Count; pathIndex2++)
                yield return new PartialPathsMesh3D(
                    baseMesh, 
                    pathIndex2 - 1, 
                    2
                );
        }

        public static IEnumerable<ILineSegment3D> GetLines(this IPathsMesh3D baseMesh, bool generateFromPaths, bool generateFromPathPoints)
        {
            if (generateFromPaths)
            {
                foreach (var path in baseMesh)
                    for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                        yield return LineSegment3D.Create(
                            path[pathPointIndex - 1], 
                            path[pathPointIndex]
                        );
            }

            if (generateFromPathPoints)
            {
                for (var pathPointIndex = 0; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
                    yield return LineSegment3D.Create(
                        baseMesh[pathIndex - 1][pathPointIndex],
                        baseMesh[pathIndex][pathPointIndex]
                    );
            }
        }

        public static IEnumerable<ITriangle3D> GetTriangles(this IPathsMesh3D baseMesh, bool reversePoints = false)
        {
            if (reversePoints)
            {
                foreach (var pointQuad in baseMesh.GetPointQuads())
                {
                    yield return Triangle3D.Create(
                        pointQuad.Item4,
                        pointQuad.Item1,
                        pointQuad.Item3
                    );

                    yield return Triangle3D.Create(
                        pointQuad.Item1,
                        pointQuad.Item4,
                        pointQuad.Item2
                    );
                }
            }
            else
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
        }

        public static IEnumerable<ITriplet<int>> GetTriangleVertexIndices(this IPathsMesh3D baseMesh)
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

        public static IEnumerable<ITriplet<IGraphicsVertex3D>> GetTriangleVertices(this IPathsMesh3D baseMesh, GraphicsTextureCoordinatesGrid texturesCoordinatesGrid)
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

        public static IEnumerable<ITriplet<IGraphicsVertex3D>> GetTriangleVertices(this TexturedPathsMesh3D baseMesh)
        {
            foreach (var indexQuad in baseMesh.GetVertexQuads())
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


        public static GraphicsTextureCoordinatesGrid GetTextureCoordinatesGrid(this IPathsMesh3D baseMesh)
        {
            var textureURange = new PSeqLinearDouble1D(
                0, 
                1, 
                baseMesh.Count
            );

            var textureVRange = new PSeqLinearDouble1D(
                0, 
                1, 
                baseMesh.PathPointsCount
            );

            return new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }

        public static GraphicsTextureCoordinatesGrid GetTextureCoordinatesGrid(this IPathsMesh3D baseMesh, double firstUValue, double lastUValue, double firstVValue, double lastVValue)
        {
            var textureURange = new PSeqLinearDouble1D(
                firstUValue, 
                lastUValue, 
                baseMesh.Count
            );

            var textureVRange = new PSeqLinearDouble1D(
                firstVValue, 
                lastVValue, 
                baseMesh.PathPointsCount
            );
            
            return new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }

        public static TexturedPathsMesh3D ToTexturedMesh(this IPathsMesh3D baseMesh)
        {
            return new TexturedPathsMesh3D(
                baseMesh,
                0,
                1,
                0,
                1
            );
        }

        public static TexturedPathsMesh3D ToTexturedMesh(this IPathsMesh3D baseMesh, double firstUValue, double lastUValue, double firstVValue, double lastVValue)
        {
            return new TexturedPathsMesh3D(
                baseMesh,
                firstUValue,
                lastUValue,
                firstVValue,
                lastVValue
            );
        }


        public static GraphicsLinesGeometry3D GetGraphicsLinesGeometry(this IPathsMesh3D baseMesh, bool generateFromPaths, bool generateFromPathPoints)
        {
            var geometry = 
                GraphicsLinesGeometry3D.Create(baseMesh.GetPoints());

            if (generateFromPaths)
            {
                for (var pathIndex = 0; pathIndex < baseMesh.Count; pathIndex++)
                for (var pathPointIndex = 1; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                    geometry.AppendLine(
                        baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex - 1),
                        baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex)
                    );
            }

            if (generateFromPathPoints)
            {
                for (var pathPointIndex = 0; pathPointIndex < baseMesh.PathPointsCount; pathPointIndex++)
                for (var pathIndex = 1; pathIndex < baseMesh.Count; pathIndex++)
                    geometry.AppendLine(
                        baseMesh.GetMeshPointIndex(pathIndex - 1, pathPointIndex),
                        baseMesh.GetMeshPointIndex(pathIndex, pathPointIndex)
                    );
            }

            return geometry;
        }

        public static GraphicsTrianglesGeometry3D GetGraphicsTrianglesGeometry(this IPathsMesh3D baseMesh, GraphicsVertexNormalComputationMethod normalComputationMethod, bool reverseNormals)
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

        public static GraphicsTrianglesGeometry3D GetGraphicsTrianglesGeometry(this IPathsMesh3D baseMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid, GraphicsVertexNormalComputationMethod normalComputationMethod, bool reverseNormals)
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

        public static GraphicsTrianglesGeometry3D GetGraphicsTrianglesGeometry(this TexturedPathsMesh3D baseMesh, GraphicsVertexNormalComputationMethod normalComputationMethod, bool reverseNormals)
        {
            var verticesList = 
                baseMesh.GetVertices().ToArray();

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


        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, IPathsMesh3D pathsMesh)
        {
            return composer.AddTriangles(
                pathsMesh.GetTriangles()
            );
        }

        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, IPathsMesh3D pathsMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return composer.AddTrianglesFromVertices(
                pathsMesh.GetTriangleVertices(textureCoordinatesGrid)
            );
        }

        public static GraphicsTrianglesListGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesListGeometryComposer3D composer, TexturedPathsMesh3D pathsMesh)
        {
            return composer.AddTrianglesFromVertices(
                pathsMesh.GetTriangleVertices()
            );
        }
        
        public static GraphicsTrianglesGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesGeometryComposer3D composer, IPathsMesh3D pathsMesh)
        {
            return composer.AddTriangles(
                pathsMesh.GetTriangles()
            );
        }

        public static GraphicsTrianglesGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesGeometryComposer3D composer, IPathsMesh3D pathsMesh, GraphicsTextureCoordinatesGrid textureCoordinatesGrid)
        {
            return composer.AddTrianglesFromVertices(
                pathsMesh.GetTriangleVertices(textureCoordinatesGrid)
            );
        }

        public static GraphicsTrianglesGeometryComposer3D AddTrianglesFromMesh(this GraphicsTrianglesGeometryComposer3D composer, TexturedPathsMesh3D pathsMesh)
        {
            return composer.AddTrianglesFromVertices(
                pathsMesh.GetTriangleVertices()
            );
        }
    }
}
