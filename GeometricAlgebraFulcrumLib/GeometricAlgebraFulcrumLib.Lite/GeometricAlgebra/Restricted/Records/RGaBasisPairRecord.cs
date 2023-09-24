using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2) :
    IRGaBasisPairRecord;