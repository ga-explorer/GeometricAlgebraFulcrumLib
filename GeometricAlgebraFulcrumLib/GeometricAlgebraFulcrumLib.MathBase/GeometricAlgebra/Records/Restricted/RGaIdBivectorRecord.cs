using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaIdBivectorRecord(ulong Id, RGaFloat64Bivector Bivector) :
    IRGaIdBivectorRecord;

public sealed record RGaIdBivectorRecord<T>(ulong Id, RGaBivector<T> Bivector) :
    IRGaIdBivectorRecord<T>;