using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2) :
    IXGaBasisPairRecord;