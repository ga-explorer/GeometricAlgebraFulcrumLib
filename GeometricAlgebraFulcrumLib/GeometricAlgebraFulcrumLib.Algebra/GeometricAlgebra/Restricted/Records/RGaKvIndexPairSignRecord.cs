using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaKvIndexPairSignRecord(ulong KvIndex1, ulong KvIndex2, IntegerSign Sign) :
    IRGaKvIndexPairSignRecord;