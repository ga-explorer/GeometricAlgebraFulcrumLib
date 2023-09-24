using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaIdVectorStorageRecord<T>(ulong Id, VectorStorage<T> Storage) : 
    IRGaIdRecord;