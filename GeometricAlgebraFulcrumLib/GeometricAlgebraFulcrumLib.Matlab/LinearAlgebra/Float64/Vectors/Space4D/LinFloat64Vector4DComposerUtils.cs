using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public static class LinFloat64Vector4DComposerUtils
{
    
    public static LinFloat64Vector4D ToLinVector4D(this IQuad<double> vector)
    {
        return vector as LinFloat64Vector4D
               ?? LinFloat64Vector4D.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }
}