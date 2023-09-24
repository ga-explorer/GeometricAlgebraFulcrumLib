using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaIdBivectorStorageRecord<T>(ulong Id, BivectorStorage<T> Storage) : 
    IRGaIdRecord;