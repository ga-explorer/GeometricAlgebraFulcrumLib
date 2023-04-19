using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GradeIndexBivectorStorageRecord<T>(uint Grade, ulong KvIndex, BivectorStorage<T> Storage) : 
    IRGaGradeKvIndexRecord;