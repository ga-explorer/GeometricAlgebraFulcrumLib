using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinMultivector2DUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> VectorOp<T>(this IPair<Scalar<T>> mv1, IPair<Scalar<T>> mv2)
    {
        return LinBivector2D<T>.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1
        );
    }


    
}