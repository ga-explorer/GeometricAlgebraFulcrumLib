using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaKvIndexLinVectorStorageRecord<T>(ulong KvIndex, ILinVectorStorage<T> Storage) : 
    IRGaKvIndexRecord;