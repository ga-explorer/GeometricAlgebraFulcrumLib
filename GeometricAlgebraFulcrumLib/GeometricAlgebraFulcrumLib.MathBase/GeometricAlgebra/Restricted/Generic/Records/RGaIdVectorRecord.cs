using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdVectorRecord<T>(ulong Id, RGaVector<T> Vector) :
    IRGaIdVectorRecord<T>;