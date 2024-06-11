using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

/// <summary>
/// A path where points are directly stored in memory as a list
/// </summary>
public sealed class ListPointsPath2D
    : PSeqReadOnlyList1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public ListPointsPath2D(params ILinFloat64Vector2D[] pointsArray)
        : base(pointsArray)
    {
    }

    public ListPointsPath2D(IReadOnlyList<ILinFloat64Vector2D> pointsArray)
        : base(pointsArray)
    {
    }

    public ListPointsPath2D(IEnumerable<ILinFloat64Vector2D> pointsList)
        : base(pointsList)
    {
    }
        

    public bool IsValid()
    {
        return DataList.Count >= 2 &&
               DataList.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ListPointsPath2D(this.Select(pointMapping));
    }
}