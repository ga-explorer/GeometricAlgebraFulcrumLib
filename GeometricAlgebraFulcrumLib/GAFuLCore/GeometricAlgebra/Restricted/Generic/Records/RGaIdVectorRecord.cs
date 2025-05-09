using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdVectorRecord<T>(ulong Id, RGaVector<T> Vector) :
    IRGaIdVectorRecord<T>;