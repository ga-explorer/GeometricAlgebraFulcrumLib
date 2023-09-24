using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairScalarRecord<T>(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, T Scalar) : 
    IXGaBasisPairScalarRecord<T>;