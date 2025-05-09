using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2) :
    IXGaBasisPairRecord;