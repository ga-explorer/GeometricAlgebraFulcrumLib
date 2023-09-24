using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaIdKVectorStorageRecord<T>(ulong Id, KVectorStorage<T> Storage) : 
    IRGaIdRecord;