using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdKVectorRecord(ulong Id, RGaFloat64KVector KVector) :
    IRGaIdKVectorRecord;
