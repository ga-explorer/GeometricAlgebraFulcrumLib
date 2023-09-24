using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdVectorRecord(ulong Id, RGaFloat64Vector Vector) :
    IRGaIdVectorRecord;
