using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public class ConstantPointsPath2D : 
    PSeqConstant1D<LinFloat64Vector2D>, 
    IPointsPath2D
{
    public ConstantPointsPath2D(int count)
        : base(count, LinFloat64Vector2D.Create((Float64Scalar)0, 0))
    {
    }

    public ConstantPointsPath2D(int count, double x, double y)
        : base(count, LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y))
    {
    }

    public ConstantPointsPath2D(int count, LinFloat64Vector2D value)
        : base(count, value)
    {
    }
        

    public bool IsValid()
    {
        return Count >= 2 && Value.IsValid();
    }

    public IPointsPath2D MapPoints(Func<LinFloat64Vector2D, LinFloat64Vector2D> pointMapping)
    {
        return new ConstantPointsPath2D(Count, pointMapping(Value));
    }
}