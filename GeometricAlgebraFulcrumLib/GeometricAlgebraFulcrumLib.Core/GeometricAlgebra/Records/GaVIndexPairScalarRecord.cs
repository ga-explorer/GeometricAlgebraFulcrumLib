namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public sealed record GaVIndexPairScalarRecord(int VIndex1, int VIndex2, double Scalar) :
    IGaVIndexPairScalarRecord<double>;

public sealed record GaVIndexPairScalarRecord<T>(int VIndex1, int VIndex2, T Scalar) :
    IGaVIndexPairScalarRecord<T>;