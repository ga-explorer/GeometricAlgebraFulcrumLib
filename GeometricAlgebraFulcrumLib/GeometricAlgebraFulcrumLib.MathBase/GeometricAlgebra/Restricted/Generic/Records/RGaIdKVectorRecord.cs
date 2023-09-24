using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdKVectorRecord<T>(ulong Id, RGaKVector<T> KVector) :
    IRGaIdKVectorRecord<T>;