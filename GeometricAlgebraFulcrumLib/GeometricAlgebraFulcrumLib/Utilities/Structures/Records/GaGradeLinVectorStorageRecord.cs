using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaGradeLinVectorStorageRecord<T>(uint Grade, ILinVectorStorage<T> Storage) : 
    IGaGradeRecord;