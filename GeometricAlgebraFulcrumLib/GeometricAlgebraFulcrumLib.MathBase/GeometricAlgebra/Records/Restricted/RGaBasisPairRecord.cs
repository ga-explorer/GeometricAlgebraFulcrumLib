using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaBasisPairRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2) :
    IRGaBasisPairRecord;