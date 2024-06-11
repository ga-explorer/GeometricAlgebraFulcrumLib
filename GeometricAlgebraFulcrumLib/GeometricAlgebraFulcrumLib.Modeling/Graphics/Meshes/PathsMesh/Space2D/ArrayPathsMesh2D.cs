using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space2D;

/// <summary>
/// This class represents an array of 2D point paths
/// </summary>
public sealed class ArrayPathsMesh2D : 
    PSeqArray1D<IPointsPath2D>, 
    IPathsMesh2D
{
    public int PathPointsCount { get; }

    public int MeshPointsCount
        => Count * PathPointsCount;


    public ArrayPathsMesh2D(int verticesPerPath, int pathsCount)
        : base(pathsCount)
    {
        PathPointsCount = verticesPerPath;
    }

    public ArrayPathsMesh2D(int verticesPerPath, params IPointsPath2D[] pathsArray)
        : base(pathsArray)
    {
        PathPointsCount = verticesPerPath;
    }

    public ArrayPathsMesh2D(int verticesPerPath, IEnumerable<IPointsPath2D> pathsList)
        : base(pathsList)
    {
        PathPointsCount = verticesPerPath;
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
}