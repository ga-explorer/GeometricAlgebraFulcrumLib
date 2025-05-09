using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdKVectorRecord<T>(ulong Id, RGaKVector<T> KVector) :
    IRGaIdKVectorRecord<T>;