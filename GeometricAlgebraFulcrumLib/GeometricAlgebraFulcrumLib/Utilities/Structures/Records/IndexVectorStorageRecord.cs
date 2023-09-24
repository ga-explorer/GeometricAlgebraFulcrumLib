using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record IndexVectorStorageRecord<T>(ulong KvIndex, VectorStorage<T> Storage) : 
    IRGaKvIndexRecord;