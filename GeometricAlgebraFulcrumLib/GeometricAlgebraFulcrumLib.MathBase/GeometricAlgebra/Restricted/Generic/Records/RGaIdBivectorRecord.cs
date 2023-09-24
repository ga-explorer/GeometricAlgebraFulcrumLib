using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdBivectorRecord<T>(ulong Id, RGaBivector<T> Bivector) :
    IRGaIdBivectorRecord<T>;