using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed record IndexPairRecord(ulong Index1, ulong Index2);

    public sealed record IndexScalarRecord<T>(ulong Index, T Scalar);

    public sealed record IndexPairScalarRecord<T>(ulong Index1, ulong Index2, T Scalar);

    public sealed record GradeIndexRecord(uint Grade, ulong Index);

    public sealed record GradeIndexPairRecord(uint Grade, ulong Key1, ulong Key2);

    public sealed record GradeIndexScalarRecord<T>(uint Grade, ulong Index, T Scalar);
    
    public sealed record GradeIndexPairScalarRecord<T>(uint Grade, ulong Index1, ulong Index2, T Scalar);
    
    public sealed record GradeScalarRecord<T>(uint Grade, T Scalar);

    public sealed record GradeVectorStorageRecord<T>(uint Grade, ILaVectorEvenStorage<T> Storage);

    public sealed record GradeVectorStorageScalarRecord<T>(uint Grade, ILaVectorEvenStorage<T> Storage, T Scalar);

    public sealed record GradeArrayStorageRecord<T>(uint Grade, ILaMatrixEvenStorage<T> Storage);

    public sealed record GradeArrayStorageScalarRecord<T>(uint Grade, ILaMatrixEvenStorage<T> Storage, T Scalar);

    public sealed record VectorEvenStorageScalarRecord<T>(ILaVectorEvenStorage<T> Storage, T Scalar);

    public sealed record VectorGradedStorageScalarRecord<T>(ILaVectorGradedStorage<T> Storage, T Scalar);

    public sealed record ArrayEvenStorageScalarRecord<T>(ILaMatrixEvenStorage<T> Storage, T Scalar);

    public sealed record ArrayGradedStorageScalarRecord<T>(ILaMatrixGradedStorage<T> Storage, T Scalar);
}