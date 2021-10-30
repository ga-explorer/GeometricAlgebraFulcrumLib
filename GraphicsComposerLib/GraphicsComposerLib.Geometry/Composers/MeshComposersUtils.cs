using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Composers
{
    public static class MeshComposersUtils
    {
        public static IPathsMesh3D ComposeParallelogramPathMesh(Tuple3D cornerPoint, Tuple3D baseVector, Tuple3D sideVector, int basePointsCount, int sidePointsCount)
        {
            var meshPathsList = new List<IPointsPath3D>(basePointsCount);

            var sidePoints = 
                sidePointsCount
                    .GetRegularSamples(0.0d, 1.0d)
                    .Lerp(cornerPoint, cornerPoint + sideVector);

            foreach (var pathFirstPoint in sidePoints)
            {
                var basePoints = 
                    basePointsCount
                        .GetRegularSamples(0.0d, 1.0d)
                        .Lerp(pathFirstPoint, pathFirstPoint + baseVector)
                        .Cast<ITuple3D>();

                var path = new ArrayPointsPath3D(basePoints);

                meshPathsList.Add(path);
            }

            return new ListPathsMesh3D(basePointsCount, meshPathsList);
        }
    }
}
