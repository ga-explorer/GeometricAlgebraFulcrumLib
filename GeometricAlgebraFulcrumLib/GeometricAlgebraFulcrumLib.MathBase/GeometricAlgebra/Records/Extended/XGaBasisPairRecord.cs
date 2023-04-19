using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaBasisPairRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2) :
    IXGaBasisPairRecord;