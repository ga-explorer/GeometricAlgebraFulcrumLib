using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdBivectorRecord(ulong Id, RGaFloat64Bivector Bivector) :
    IRGaIdBivectorRecord;
