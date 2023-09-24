using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2) :
    IXGaBasisPairRecord;