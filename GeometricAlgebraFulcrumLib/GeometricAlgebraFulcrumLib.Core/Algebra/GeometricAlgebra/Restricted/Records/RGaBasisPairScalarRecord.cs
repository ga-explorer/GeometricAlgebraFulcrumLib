using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairScalarRecord<T>(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, T Scalar) : 
    IRGaBasisPairScalarRecord<T>;