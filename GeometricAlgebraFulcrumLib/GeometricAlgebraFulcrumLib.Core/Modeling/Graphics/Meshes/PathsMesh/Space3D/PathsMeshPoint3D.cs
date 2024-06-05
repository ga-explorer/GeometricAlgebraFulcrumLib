using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space3D;

/// <summary>
/// This class represents a 3D point inside a paths mesh. The class holds data
/// on the point coordinates and index values inside the mesh.
/// </summary>
public sealed class PathsMeshPoint3D : ILinFloat64Vector3D
{
    /// <summary>
    /// The base mesh of this point
    /// </summary>
    public IPathsMesh3D BaseMesh { get; }

    /// <summary>
    /// The path index of this point inside its base mesh
    /// </summary>
    public int PathIndex { get; }

    /// <summary>
    /// The point index of this point inside its base path
    /// </summary>
    public int PathPointIndex { get; }

    /// <summary>
    /// The point index of this point inside its base mesh
    /// </summary>
    public int MeshPointIndex 
        => PathPointIndex + PathIndex * BaseMesh.PathPointsCount;

    /// <summary>
    /// The path index and path point index of this point
    /// </summary>
    public Pair<int> PointIndexPair 
        => new Pair<int>(PathIndex, PathPointIndex);

    /// <summary>
    /// The point coordinates of this point
    /// </summary>
    public ILinFloat64Vector3D Point 
        => BaseMesh[PathIndex][PathPointIndex];
        
    public int VSpaceDimensions 
        => 3;

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Z 
        => Point.Z;

    public Float64Scalar Item1 
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public bool IsValid() => Point.IsValid();


    internal PathsMeshPoint3D(IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
    {
        BaseMesh = baseMesh;
        PathIndex = pathIndex.Mod(baseMesh.Count);
        PathPointIndex = pathPointIndex.Mod(baseMesh.PathPointsCount);
    }
}