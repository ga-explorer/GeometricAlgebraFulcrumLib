using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaBasisPairScalarRecord<T>(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, T Scalar) : 
    IXGaBasisPairScalarRecord<T>;