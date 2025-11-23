using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space3D;

/// <summary>
/// This class represents a 3D point inside a points mesh. The class holds data
/// on the point coordinates and index values inside the mesh.
/// </summary>
public sealed class PointsMeshPoint3D : 
    ILinFloat64Vector3D
{
    /// <summary>
    /// The base mesh of this point
    /// </summary>
    public IPointsMesh3D BaseMesh { get; }

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
    public ILinFloat64Vector3D Point 
        => BaseMesh[PointIndex1, PointIndex2];
        
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
            2 => Z,
            _ => Float64Scalar.Zero
        };

    public double Item1 
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;


    internal PointsMeshPoint3D(IPointsMesh3D baseMesh, int pointIndex1, int pointIndex2)
    {
        BaseMesh = baseMesh;
        PointIndex1 = pointIndex1.Mod(baseMesh.Count1);
        PointIndex2 = pointIndex2.Mod(baseMesh.Count2);
    }
    
    public bool IsValid()
    {
        return Point.IsValid();
    }

}