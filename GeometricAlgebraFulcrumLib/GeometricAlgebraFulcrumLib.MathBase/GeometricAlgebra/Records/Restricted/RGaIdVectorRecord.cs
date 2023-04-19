using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaIdVectorRecord(ulong Id, RGaFloat64Vector Vector) :
    IRGaIdVectorRecord;

public sealed record RGaIdVectorRecord<T>(ulong Id, RGaVector<T> Vector) :
    IRGaIdVectorRecord<T>;