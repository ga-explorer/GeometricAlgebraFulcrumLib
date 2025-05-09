using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdVectorRecord(ulong Id, RGaFloat64Vector Vector) :
    IRGaIdVectorRecord;
