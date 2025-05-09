using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2) :
    IXGaBasisPairRecord;