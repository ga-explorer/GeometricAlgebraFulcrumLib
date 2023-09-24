namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records
{
    public sealed record RGaKvIndexScalarRecord(ulong KvIndex, double Scalar) :
        IRGaKvIndexScalarRecord<double>;

    public sealed record RGaKvIndexScalarRecord<T>(ulong KvIndex, T Scalar) :
        IRGaKvIndexScalarRecord<T>;
}