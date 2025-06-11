using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Multivector3DUtils
{
    





    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D VectorOp(this ITriplet<Float64Scalar> mv1, ITriplet<Float64Scalar> mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
            mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
            mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
        );
    }
    
    
}