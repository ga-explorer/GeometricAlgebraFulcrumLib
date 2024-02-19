using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

/// <summary>
/// A path where points are directly stored in memory as a list
/// </summary>
public sealed class ListPointsPath3D : 
    PSeqReadOnlyList1D<IFloat64Vector3D>, 
    IPointsPath3D
{
    public ListPointsPath3D(IReadOnlyList<IFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }

    public ListPointsPath3D(params IFloat64Vector3D[] pointsArray)
        : base(pointsArray)
    {
    }

    public ListPointsPath3D(IEnumerable<IFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }


    public bool IsValid()
    {
        return DataList.Count >= 2 &&
               DataList.All(p => p.IsValid());
    }
        
    public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
    {
        return new ListPointsPath3D(this.Select(pointMapping));
    }
}