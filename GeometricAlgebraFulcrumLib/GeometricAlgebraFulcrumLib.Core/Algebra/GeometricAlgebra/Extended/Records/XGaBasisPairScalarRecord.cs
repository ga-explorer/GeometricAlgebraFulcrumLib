using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairScalarRecord<T>(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, T Scalar) : 
    IXGaBasisPairScalarRecord<T>;