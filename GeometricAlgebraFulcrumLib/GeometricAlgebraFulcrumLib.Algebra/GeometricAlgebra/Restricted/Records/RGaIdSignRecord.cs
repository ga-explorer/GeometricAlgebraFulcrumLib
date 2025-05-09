using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdSignRecord(ulong Id, IntegerSign Sign) :
    IRGaIdSignRecord;