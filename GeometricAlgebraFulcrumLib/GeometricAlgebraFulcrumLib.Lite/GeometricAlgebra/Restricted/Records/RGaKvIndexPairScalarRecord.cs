namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaKvIndexPairScalarRecord<T>(ulong KvIndex1, ulong KvIndex2, T Scalar) :
    IRGaKvIndexPairScalarRecord<T>;

public sealed record RGaKvIndexPairScalarRecord(ulong KvIndex1, ulong KvIndex2, double Scalar) :
    IRGaKvIndexPairScalarRecord<double>;