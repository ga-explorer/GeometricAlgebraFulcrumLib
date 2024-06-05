using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdBivectorRecord(ulong Id, RGaFloat64Bivector Bivector) :
    IRGaIdBivectorRecord;
