using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaIdSignRecord(ulong Id, IntegerSign Sign) :
    IRGaIdSignRecord;