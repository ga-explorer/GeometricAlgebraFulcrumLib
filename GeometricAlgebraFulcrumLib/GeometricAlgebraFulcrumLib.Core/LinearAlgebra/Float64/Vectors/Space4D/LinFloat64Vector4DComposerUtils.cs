using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;

public static class LinFloat64Vector4DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IQuad<Float64Scalar> vector)
    {
        return vector as LinFloat64Vector4D
               ?? LinFloat64Vector4D.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }
}