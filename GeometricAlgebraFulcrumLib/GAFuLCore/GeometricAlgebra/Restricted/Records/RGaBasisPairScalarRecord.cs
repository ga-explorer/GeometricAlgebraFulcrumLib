using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairScalarRecord<T>(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, T Scalar) : 
    IRGaBasisPairScalarRecord<T>;