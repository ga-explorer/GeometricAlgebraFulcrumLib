namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted
{
    public sealed record RGaKvIndexScalarRecord(ulong KvIndex, double Scalar) :
        IRGaKvIndexScalarRecord<double>;

    public sealed record RGaKvIndexScalarRecord<T>(ulong KvIndex, T Scalar) :
        IRGaKvIndexScalarRecord<T>;
}