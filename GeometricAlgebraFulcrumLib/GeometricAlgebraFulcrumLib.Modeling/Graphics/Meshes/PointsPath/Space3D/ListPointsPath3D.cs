using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

/// <summary>
/// A path where points are directly stored in memory as a list
/// </summary>
public sealed class ListPointsPath3D : 
    PSeqReadOnlyList1D<LinFloat64Vector3D>, 
    IPointsPath3D
{
    public ListPointsPath3D(IReadOnlyList<LinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }

    public ListPointsPath3D(params LinFloat64Vector3D[] pointsArray)
        : base(pointsArray)
    {
    }

    public ListPointsPath3D(IEnumerable<LinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }


    public bool IsValid()
    {
        return DataList.Count >= 2 &&
               DataList.All(p => p.IsValid());
    }
        
    public IPointsPath3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> pointMapping)
    {
        return new ListPointsPath3D(this.Select(pointMapping));
    }
}