using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GradeIndexVectorStorageRecord<T>(uint Grade, ulong KvIndex, VectorStorage<T> Storage) : 
    IRGaGradeKvIndexRecord;