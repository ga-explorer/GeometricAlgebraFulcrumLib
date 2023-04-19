using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaIdSignRecord(ulong Id, IntegerSign Sign) :
    IRGaIdSignRecord;