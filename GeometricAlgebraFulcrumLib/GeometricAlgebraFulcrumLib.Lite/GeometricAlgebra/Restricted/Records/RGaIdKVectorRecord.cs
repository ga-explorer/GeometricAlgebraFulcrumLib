using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdKVectorRecord(ulong Id, RGaFloat64KVector KVector) :
    IRGaIdKVectorRecord;
