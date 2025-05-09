using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaKvIndexPairSignRecord(ulong KvIndex1, ulong KvIndex2, IntegerSign Sign) :
    IRGaKvIndexPairSignRecord;