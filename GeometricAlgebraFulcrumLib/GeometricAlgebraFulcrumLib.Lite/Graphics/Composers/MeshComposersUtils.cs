using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Composers
{
    public static class MeshComposersUtils
    {
        public static IPathsMesh3D ComposeParallelogramPathMesh(Float64Vector3D cornerPoint, Float64Vector3D baseVector, Float64Vector3D sideVector, int basePointsCount, int sidePointsCount)
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
                        .Cast<IFloat64Vector3D>();

                var path = new ArrayPointsPath3D(basePoints);

                meshPathsList.Add(path);
            }

            return new ListPathsMesh3D(basePointsCount, meshPathsList);
        }
    }
}
