using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairScalarRecord<T>(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, T Scalar) : 
    IRGaBasisPairScalarRecord<T>;