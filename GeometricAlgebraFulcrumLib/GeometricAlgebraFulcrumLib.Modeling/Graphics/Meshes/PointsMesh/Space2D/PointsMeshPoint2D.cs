using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space2D;

/// <summary>
/// This class represents a 2D point inside a points mesh. The class holds data
/// on the point coordinates and index values inside the mesh.
/// </summary>
public sealed class PointsMeshPoint2D : ILinFloat64Vector2D
{
    /// <summary>
    /// The base mesh of this point
    /// </summary>
    public IPointsMesh2D BaseMesh { get; }

    /// <summary>
    /// The first index of this point inside its base mesh
    /// </summary>
    public int PointIndex1 { get; }

    /// <summary>
    /// The second index of this point inside its base path
    /// </summary>
    public int PointIndex2 { get; }

    /// <summary>
    /// The global point index of this point inside its base mesh
    /// </summary>
    public int MeshPointIndex 
        => PointIndex1 + PointIndex2 * BaseMesh.Count1;

    /// <summary>
    /// The path index and path point index of this point
    /// </summary>
    public Pair<int> PointIndexPair 
        => new Pair<int>(PointIndex1, PointIndex2);

    /// <summary>
    /// The point coordinates of this point
    /// </summary>
    public ILinFloat64Vector2D Point 
        => BaseMesh[PointIndex1, PointIndex2];
        
    public int VSpaceDimensions 
        => 2;

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Item1 
        => X;

    public Float64Scalar Item2
        => Y;

    public bool IsValid() => Point.IsValid();


    internal PointsMeshPoint2D(IPointsMesh2D baseMesh, int pointIndex1, int pointIndex2)
    {
        BaseMesh = baseMesh;
        PointIndex1 = pointIndex1.Mod(baseMesh.Count1);
        PointIndex2 = pointIndex2.Mod(baseMesh.Count2);
    }

}