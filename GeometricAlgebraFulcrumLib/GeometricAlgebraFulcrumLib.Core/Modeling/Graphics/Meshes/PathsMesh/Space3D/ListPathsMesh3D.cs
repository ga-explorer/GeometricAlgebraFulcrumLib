using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space3D;

/// <summary>
/// This class represents a mesh constructed from a sequence of paths in 3D.
/// All paths are assumed to have the exact same number of vertices on them.
/// </summary>
public sealed class ListPathsMesh3D : 
    PSeqReadOnlyList1D<IPointsPath3D>, 
    IPathsMesh3D
{
    public int PathPointsCount { get; }

    public int MeshPointsCount 
        => Count * PathPointsCount;


    public ListPathsMesh3D(int verticesPerPath, IReadOnlyList<IPointsPath3D> pathsList)
        : base(pathsList)
    {
        PathPointsCount = verticesPerPath;
    }

    public ListPathsMesh3D(int verticesPerPath, params IPointsPath3D[] pathsArray)
        : base(pathsArray)
    {
        PathPointsCount = verticesPerPath;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}