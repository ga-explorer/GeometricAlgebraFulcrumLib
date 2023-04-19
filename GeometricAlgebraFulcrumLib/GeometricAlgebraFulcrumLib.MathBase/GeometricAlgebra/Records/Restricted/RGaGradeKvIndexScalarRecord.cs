namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted
{
    public sealed record RGaGradeKvIndexScalarRecord<T>(uint Grade, ulong KvIndex, T Scalar) :
        IRGaGradeKvIndexScalarRecord<T>;
}