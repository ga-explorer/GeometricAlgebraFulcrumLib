using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space3D;

/// <summary>
/// This class represents a mesh from an interpolation of two 3D point paths
/// </summary>
public sealed class LerpPathsMesh3D 
    : IPathsMesh3D
{
    public IPointsPath3D Path1 { get; }

    public IPointsPath3D Path2 { get; }

    public int Count { get; }

    public IPointsPath3D this[int index]
    {
        get
        {
            var paramValue = index.Mod(Count) / (double)(Count - 1);

            return new LerpPointsPath3D(this, paramValue);
        }
    }

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;

    public int PathPointsCount 
        => Path1.Count;

    public int MeshPointsCount 
        => PathPointsCount * Count;


    public LerpPathsMesh3D(IPointsPath3D firstPath, IPointsPath3D lastPath, int pathsCount)
    {
        if (ReferenceEquals(firstPath, null))
            throw new ArgumentNullException(nameof(firstPath));

        if (ReferenceEquals(lastPath, null))
            throw new ArgumentNullException(nameof(lastPath));

        if (firstPath.Count != lastPath.Count)
            throw new ArgumentException("Paths points count don't match");

        Path1 = firstPath;
        Path2 = lastPath;
        Count = pathsCount;
    }


    public IEnumerator<IPointsPath3D> GetEnumerator()
    {
        var delta = 1.0d / (Count - 1);
        for (var index = 0; index < Count; index++)
            yield return new LerpPointsPath3D(
                this,
                index * delta
            );
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