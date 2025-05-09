using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2) :
    IRGaBasisPairRecord;