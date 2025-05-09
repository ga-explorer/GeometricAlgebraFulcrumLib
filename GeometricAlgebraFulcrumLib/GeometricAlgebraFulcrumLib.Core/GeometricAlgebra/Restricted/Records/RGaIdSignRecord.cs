using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdSignRecord(ulong Id, IntegerSign Sign) :
    IRGaIdSignRecord;