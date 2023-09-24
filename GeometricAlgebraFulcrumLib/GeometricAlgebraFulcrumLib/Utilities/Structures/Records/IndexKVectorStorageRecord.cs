﻿using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record IndexKVectorStorageRecord<T>(ulong KvIndex, KVectorStorage<T> Storage) : 
    IRGaKvIndexRecord;