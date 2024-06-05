using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2) :
    IRGaBasisPairRecord;