using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdKVectorRecord(ulong Id, RGaFloat64KVector KVector) :
    IRGaIdKVectorRecord;
