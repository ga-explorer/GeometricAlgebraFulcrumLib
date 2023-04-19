namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records
{
    public sealed record GaGradeScalarRecord(uint Grade, double Scalar) : 
        IGaGradeScalarRecord<double>;

    public sealed record GaGradeScalarRecord<T>(uint Grade, T Scalar) : 
        IGaGradeScalarRecord<T>;
}