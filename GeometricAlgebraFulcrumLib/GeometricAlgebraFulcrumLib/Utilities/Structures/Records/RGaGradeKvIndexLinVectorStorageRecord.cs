using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaGradeKvIndexLinVectorStorageRecord<T>(uint Grade, ulong KvIndex, ILinVectorStorage<T> Storage) : 
    IRGaGradeKvIndexRecord;