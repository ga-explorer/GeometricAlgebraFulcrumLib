namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaGradeKvIndexScalarRecord<T>(uint Grade, ulong KvIndex, T Scalar) :
    IRGaGradeKvIndexScalarRecord<T>;