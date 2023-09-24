using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdBivectorRecord(ulong Id, RGaFloat64Bivector Bivector) :
    IRGaIdBivectorRecord;
