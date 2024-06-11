using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;

public static class LinVector4DComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToVector4D<T>(this IQuad<Scalar<T>> vector)
    {
        return vector as LinVector4D<T>
               ?? LinVector4D<T>.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }

}