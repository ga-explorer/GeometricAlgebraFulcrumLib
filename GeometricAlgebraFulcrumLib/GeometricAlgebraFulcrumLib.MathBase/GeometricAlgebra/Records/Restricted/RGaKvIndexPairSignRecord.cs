using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaKvIndexPairSignRecord(ulong KvIndex1, ulong KvIndex2, IntegerSign Sign) :
    IRGaKvIndexPairSignRecord;