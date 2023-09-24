using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

public sealed record GaVIndexSignRecord(int VIndex, IntegerSign Sign) :
    IGaVIndexSignRecord;