using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinMultivector3DUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> VectorOp<T>(this ITriplet<Scalar<T>> mv1, ITriplet<Scalar<T>> mv2)
    {
        return LinBivector3D<T>.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
            mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
            mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
        );
    }
    
    
}