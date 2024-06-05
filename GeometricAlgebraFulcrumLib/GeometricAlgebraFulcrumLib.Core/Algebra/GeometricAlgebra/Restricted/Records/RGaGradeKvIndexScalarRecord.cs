namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaGradeKvIndexScalarRecord<T>(uint Grade, ulong KvIndex, T Scalar) :
    IRGaGradeKvIndexScalarRecord<T>;