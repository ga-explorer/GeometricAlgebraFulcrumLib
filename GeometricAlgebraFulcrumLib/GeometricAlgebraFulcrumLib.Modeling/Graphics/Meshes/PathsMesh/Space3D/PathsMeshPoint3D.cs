using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space3D;

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

    public double X 
        => Point.X;

    public double Y 
        => Point.Y;

    public double Z 
        => Point.Z;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => Float64Scalar.Zero
        };

    public double Item1 
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;


    internal PathsMeshPoint3D(IPathsMesh3D baseMesh, int pathIndex, int pathPointIndex)
    {
        BaseMesh = baseMesh;
        PathIndex = pathIndex.Mod(baseMesh.Count);
        PathPointIndex = pathPointIndex.Mod(baseMesh.PathPointsCount);
    }
    
    public bool IsValid()
    {
        return Point.IsValid();
    }

}