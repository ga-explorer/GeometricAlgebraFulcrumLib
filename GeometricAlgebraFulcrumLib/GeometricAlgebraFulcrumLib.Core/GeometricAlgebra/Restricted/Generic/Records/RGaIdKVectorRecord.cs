using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdKVectorRecord<T>(ulong Id, RGaKVector<T> KVector) :
    IRGaIdKVectorRecord<T>;