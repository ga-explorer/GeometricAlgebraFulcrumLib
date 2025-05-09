using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdVectorRecord<T>(ulong Id, RGaVector<T> Vector) :
    IRGaIdVectorRecord<T>;