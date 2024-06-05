namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

public sealed record GaVIndexScalarRecord(int VIndex, double Scalar) :
    IGaVIndexScalarRecord<double>;

public sealed record GaVIndexScalarRecord<T>(int VIndex, T Scalar) :
    IGaVIndexScalarRecord<T>;