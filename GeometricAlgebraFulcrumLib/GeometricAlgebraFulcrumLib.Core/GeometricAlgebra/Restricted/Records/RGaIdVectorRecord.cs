using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdVectorRecord(ulong Id, RGaFloat64Vector Vector) :
    IRGaIdVectorRecord;
