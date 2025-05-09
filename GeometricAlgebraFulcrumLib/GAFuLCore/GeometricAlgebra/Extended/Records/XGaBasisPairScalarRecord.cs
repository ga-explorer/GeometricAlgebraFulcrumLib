using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairScalarRecord<T>(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, T Scalar) : 
    IXGaBasisPairScalarRecord<T>;