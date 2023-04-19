using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaIdKVectorRecord(ulong Id, RGaFloat64KVector KVector) :
    IRGaIdKVectorRecord;

public sealed record RGaIdKVectorRecord<T>(ulong Id, RGaKVector<T> KVector) :
    IRGaIdKVectorRecord<T>;