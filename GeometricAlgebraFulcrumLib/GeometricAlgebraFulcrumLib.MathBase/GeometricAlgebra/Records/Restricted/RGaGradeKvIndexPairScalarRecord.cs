namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted
{
    public sealed record RGaGradeKvIndexPairScalarRecord<T>(uint Grade, ulong KvIndex1, ulong KvIndex2, T Scalar) :
        IRGaGradeKvIndexPairScalarRecord<T>;
}