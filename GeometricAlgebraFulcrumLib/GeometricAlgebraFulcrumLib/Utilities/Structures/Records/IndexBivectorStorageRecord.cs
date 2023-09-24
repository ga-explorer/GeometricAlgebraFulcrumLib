using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record IndexBivectorStorageRecord<T>(ulong KvIndex, BivectorStorage<T> Storage) : 
    IRGaKvIndexRecord;