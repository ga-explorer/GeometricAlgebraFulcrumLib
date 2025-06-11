using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

public static class LinFloat64VectorTermComposerUtils
{
    
    public static LinFloat64VectorTerm CreateZeroLinTerm()
    {
        return new LinFloat64VectorTerm(
            0.ToLinBasisVector(),
            0d
        );
    }

    
    public static LinFloat64VectorTerm CreateZeroLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            0d
        );
    }

    
    public static LinFloat64VectorTerm CreatePositiveLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            1d
        );
    }

    
    public static LinFloat64VectorTerm CreateNegativeLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            -1d
        );
    }

    
    public static LinFloat64VectorTerm CreateLinTerm(this int index, double scalar)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            scalar
        );
    }


}