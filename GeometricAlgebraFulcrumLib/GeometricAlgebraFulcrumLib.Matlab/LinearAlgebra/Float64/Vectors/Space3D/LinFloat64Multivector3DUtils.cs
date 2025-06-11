using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Multivector3DUtils
{
    





    
    public static LinFloat64Bivector3D VectorOp(this ITriplet<double> mv1, ITriplet<double> mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
            mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
            mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
        );
    }
    
    
}