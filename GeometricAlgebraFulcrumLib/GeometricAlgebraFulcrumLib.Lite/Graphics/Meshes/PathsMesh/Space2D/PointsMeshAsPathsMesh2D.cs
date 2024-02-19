using System.Collections;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space2D;

/// <summary>
/// This class represents a paths mesh composed from a base 2D points mesh
/// </summary>
public sealed class PointsMeshAsPathsMesh2D
    : IPathsMesh2D
{
    public IPointsMesh2D BasePointsMesh { get; }

    public int Count
        => BasePointsMesh.Count2;

    public int PathPointsCount
        => BasePointsMesh.Count1;

    public int MeshPointsCount
        => Count * PathPointsCount;

    public IPointsPath2D this[int index]
        => new PointsMeshSlicePointsPath2D(BasePointsMesh, 0, index);

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PointsMeshAsPathsMesh2D(IPointsMesh2D basePointsMesh)
    {
        BasePointsMesh = basePointsMesh;
    }


    public IEnumerator<IPointsPath2D> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}