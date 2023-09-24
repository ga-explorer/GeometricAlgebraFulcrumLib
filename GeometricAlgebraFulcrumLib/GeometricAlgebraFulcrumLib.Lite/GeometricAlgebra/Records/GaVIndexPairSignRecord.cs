using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

public sealed record GaVIndexPairSignRecord(int VIndex1, int VIndex2, IntegerSign Sign) :
    IGaVIndexPairSignRecord;