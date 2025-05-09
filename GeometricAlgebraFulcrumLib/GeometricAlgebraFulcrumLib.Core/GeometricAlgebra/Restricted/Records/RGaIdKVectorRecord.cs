using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdKVectorRecord(ulong Id, RGaFloat64KVector KVector) :
    IRGaIdKVectorRecord;
