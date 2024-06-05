using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaKvIndexPairSignRecord(ulong KvIndex1, ulong KvIndex2, IntegerSign Sign) :
    IRGaKvIndexPairSignRecord;