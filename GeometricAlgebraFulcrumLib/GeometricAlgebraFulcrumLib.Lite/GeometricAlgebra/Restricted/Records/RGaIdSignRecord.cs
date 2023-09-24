using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdSignRecord(ulong Id, IntegerSign Sign) :
    IRGaIdSignRecord;