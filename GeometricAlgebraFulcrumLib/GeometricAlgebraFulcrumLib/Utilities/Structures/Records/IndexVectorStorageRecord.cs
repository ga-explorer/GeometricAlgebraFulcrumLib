using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record IndexVectorStorageRecord<T>(ulong KvIndex, VectorStorage<T> Storage) : 
    IRGaKvIndexRecord;