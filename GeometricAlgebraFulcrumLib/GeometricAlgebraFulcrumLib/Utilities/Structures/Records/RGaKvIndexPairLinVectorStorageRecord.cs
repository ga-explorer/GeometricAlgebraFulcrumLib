using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record RGaKvIndexPairLinVectorStorageRecord<T>(ulong KvIndex1, ulong KvIndex2, ILinVectorStorage<T> Storage) : 
    IRGaKvIndexPairRecord;