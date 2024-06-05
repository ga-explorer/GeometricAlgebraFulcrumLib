using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Composers;

public static class MeshComposersUtils
{
    public static IPathsMesh3D ComposeParallelogramPathMesh(LinFloat64Vector3D cornerPoint, LinFloat64Vector3D baseVector, LinFloat64Vector3D sideVector, int basePointsCount, int sidePointsCount)
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
                    .Cast<ILinFloat64Vector3D>();

            var path = new ArrayPointsPath3D(basePoints);

            meshPathsList.Add(path);
        }

        return new ListPathsMesh3D(basePointsCount, meshPathsList);
    }
}