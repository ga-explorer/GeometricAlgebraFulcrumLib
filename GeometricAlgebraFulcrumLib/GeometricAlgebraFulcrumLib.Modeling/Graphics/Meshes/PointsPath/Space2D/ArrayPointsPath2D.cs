using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

/// <summary>
/// A path where points are directly stored in memory as an array
/// </summary>
public sealed class ArrayPointsPath2D : 
    PSeqArray1D<LinFloat64Vector2D>, 
    IPointsPath2D
{
    public ArrayPointsPath2D(int pointsCount)
        : base(pointsCount)
    {
    }

    public ArrayPointsPath2D(params LinFloat64Vector2D[] pointsArray)
        : base(pointsArray)
    {
    }
    
    public ArrayPointsPath2D(IEnumerable<LinFloat64Vector2D> pointsList)
        : base(pointsList)
    {
    }
    

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<LinFloat64Vector2D, LinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}