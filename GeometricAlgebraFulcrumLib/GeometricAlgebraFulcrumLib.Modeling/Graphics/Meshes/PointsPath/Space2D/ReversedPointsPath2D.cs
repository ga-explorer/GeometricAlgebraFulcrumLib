using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public class ReversedPointsPath2D : 
    PSeqReverse1D<LinFloat64Vector2D>, 
    IPointsPath2D
{
    public IPointsPath2D BasePath { get; }


    public ReversedPointsPath2D(IPointsPath2D basePath)
        : base(basePath)
    {
        BasePath = basePath;
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath2D MapPoints(Func<LinFloat64Vector2D, LinFloat64Vector2D> pointMapping)
    {
        return new ReversedPointsPath2D(
            BasePath.MapPoints(pointMapping)
        );
    }
}