using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Multivector2DUtils
{
    
    public static LinFloat64Bivector2D VectorOp(this IPair<double> mv1, IPair<double> mv2)
    {
        return LinFloat64Bivector2D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1
        );
    }
    
}