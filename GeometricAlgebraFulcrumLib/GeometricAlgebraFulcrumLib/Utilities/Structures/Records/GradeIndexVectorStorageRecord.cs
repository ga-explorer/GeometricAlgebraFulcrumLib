using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GradeIndexVectorStorageRecord<T>(uint Grade, ulong KvIndex, VectorStorage<T> Storage) : 
    IRGaGradeKvIndexRecord;