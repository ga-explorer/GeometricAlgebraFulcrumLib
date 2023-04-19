using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaBasisPairScalarRecord<T>(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, T Scalar) : 
    IRGaBasisPairScalarRecord<T>;