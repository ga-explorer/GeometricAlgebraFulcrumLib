using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public sealed record IndexPairRecord(ulong Index1, ulong Index2) : IIndexPairRecord;

    public sealed record IndexScalarRecord<T>(ulong Index, T Scalar) : IIndexScalarRecord<T>;

    public sealed record IndexPairScalarRecord<T>(ulong Index1, ulong Index2, T Scalar) : IIndexPairScalarRecord<T>;

    public sealed record IndexLinVectorStorageRecord<T>(ulong Index, ILinVectorStorage<T> Storage) : IIndexRecord;

    public sealed record IndexPairLinVectorStorageRecord<T>(ulong Index1, ulong Index2, ILinVectorStorage<T> Storage) : IIndexPairRecord;

    public sealed record GradeIndexRecord(uint Grade, ulong Index) : IGradeIndexRecord;

    public sealed record GradeIndexPairRecord(uint Grade, ulong Index1, ulong Index2) : IGradeIndexPairRecord;

    public sealed record GradeIndexScalarRecord<T>(uint Grade, ulong Index, T Scalar) : IGradeIndexScalarRecord<T>;
    
    public sealed record GradeIndexPairScalarRecord<T>(uint Grade, ulong Index1, ulong Index2, T Scalar) : IGradeIndexPairScalarRecord<T>;
    
    public sealed record GradeScalarRecord<T>(uint Grade, T Scalar) : IGradeScalarRecord<T>;

    public sealed record GradeLinVectorStorageRecord<T>(uint Grade, ILinVectorStorage<T> Storage) : IGradeRecord;

    public sealed record GradeIndexLinVectorStorageRecord<T>(uint Grade, ulong Index, ILinVectorStorage<T> Storage) : IGradeIndexRecord;

    public sealed record GradeLinVectorStorageScalarRecord<T>(uint Grade, ILinVectorStorage<T> Storage, T Scalar) : IGradeScalarRecord<T>;

    public sealed record GradeLinMatrixStorageRecord<T>(uint Grade, ILinMatrixStorage<T> Storage) : IGradeRecord;

    public sealed record GradeLinMatrixStorageScalarRecord<T>(uint Grade, ILinMatrixStorage<T> Storage, T Scalar) : IGradeScalarRecord<T>;

    public sealed record LinVectorStorageScalarRecord<T>(ILinVectorStorage<T> Storage, T Scalar) : IScalarRecord<T>;

    public sealed record LinVectorGradedStorageScalarRecord<T>(ILinVectorGradedStorage<T> Storage, T Scalar) : IScalarRecord<T>;

    public sealed record LinMatrixStorageScalarRecord<T>(ILinMatrixStorage<T> Storage, T Scalar) : IScalarRecord<T>;

    public sealed record LinMatrixGradedStorageScalarRecord<T>(ILinMatrixGradedStorage<T> Storage, T Scalar) : IScalarRecord<T>;

    public sealed record IdVectorStorageRecord<T>(ulong Id, VectorStorage<T> Storage) : IIdRecord;

    public sealed record IdBivectorStorageRecord<T>(ulong Id, BivectorStorage<T> Storage) : IIdRecord;

    public sealed record IdKVectorStorageRecord<T>(ulong Id, KVectorStorage<T> Storage) : IIdRecord;

    public sealed record IdMultivectorStorageRecord<T>(ulong Id, IMultivectorStorage<T> Storage) : IIdRecord;
    
    public sealed record IdVectorRecord<T>(ulong Id, GaVector<T> Vector) : IIdRecord;
    
    public sealed record IdBivectorRecord<T>(ulong Id, GaBivector<T> Bivector) : IIdRecord;
    
    public sealed record IdKVectorRecord<T>(ulong Id, GaKVector<T> KVector) : IIdRecord;
    
    public sealed record IdMultivectorRecord<T>(ulong Id, GaMultivector<T> Multivector) : IIdRecord;

    public sealed record IndexVectorStorageRecord<T>(ulong Index, VectorStorage<T> Storage) : IIndexRecord;

    public sealed record IndexVectorRecord<T>(ulong Index, GaVector<T> Vector) : IIndexRecord;

    public sealed record IndexBivectorStorageRecord<T>(ulong Index, BivectorStorage<T> Storage) : IIndexRecord;

    public sealed record IndexBivectorRecord<T>(ulong Index, GaBivector<T> Bivector) : IIndexRecord;

    public sealed record IndexKVectorStorageRecord<T>(ulong Index, KVectorStorage<T> Storage) : IIndexRecord;

    public sealed record IndexKVectorRecord<T>(ulong Index, GaKVector<T> KVector) : IIndexRecord;

    public sealed record GradeIndexVectorStorageRecord<T>(uint Grade, ulong Index, VectorStorage<T> Storage) : IGradeIndexRecord;

    public sealed record GradeIndexBivectorStorageRecord<T>(uint Grade, ulong Index, BivectorStorage<T> Storage) : IGradeIndexRecord;

    public sealed record GradeIndexKVectorStorageRecord<T>(uint Grade, ulong Index, KVectorStorage<T> Storage) : IGradeIndexRecord;
}