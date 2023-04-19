using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GradeIndexKVectorStorageRecord<T>(uint Grade, ulong KvIndex, KVectorStorage<T> Storage) : 
    IRGaGradeKvIndexRecord;