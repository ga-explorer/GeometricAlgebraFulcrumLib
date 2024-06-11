using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space3D;

/// <summary>
/// This class represents an array of 3D point paths
/// </summary>
public sealed class ArrayPathsMesh3D : 
    PSeqArray1D<IPointsPath3D>, 
    IPathsMesh3D
{
    public int PathPointsCount { get; }

    public int MeshPointsCount 
        => Count * PathPointsCount;


    public ArrayPathsMesh3D(int verticesPerPath, int pathsCount)
        : base(pathsCount)
    {
        PathPointsCount = verticesPerPath;
    }

    public ArrayPathsMesh3D(int verticesPerPath, params IPointsPath3D[] pathsArray)
        : base(pathsArray)
    {
        PathPointsCount = verticesPerPath;
    }

    public ArrayPathsMesh3D(int verticesPerPath, IEnumerable<IPointsPath3D> pathsList)
        : base(pathsList)
    {
        PathPointsCount = verticesPerPath;
    }

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
}