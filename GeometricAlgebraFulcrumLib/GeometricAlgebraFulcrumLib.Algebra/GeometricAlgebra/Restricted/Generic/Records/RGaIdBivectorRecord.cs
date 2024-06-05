using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdBivectorRecord<T>(ulong Id, RGaBivector<T> Bivector) :
    IRGaIdBivectorRecord<T>;