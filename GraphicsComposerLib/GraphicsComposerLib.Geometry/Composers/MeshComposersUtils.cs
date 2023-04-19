using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Composers
{
    public static class MeshComposersUtils
    {
        public static IPathsMesh3D ComposeParallelogramPathMesh(Float64Tuple3D cornerPoint, Float64Tuple3D baseVector, Float64Tuple3D sideVector, int basePointsCount, int sidePointsCount)
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
                        .Cast<IFloat64Tuple3D>();

                var path = new ArrayPointsPath3D(basePoints);

                meshPathsList.Add(path);
            }

            return new ListPathsMesh3D(basePointsCount, meshPathsList);
        }
    }
}
