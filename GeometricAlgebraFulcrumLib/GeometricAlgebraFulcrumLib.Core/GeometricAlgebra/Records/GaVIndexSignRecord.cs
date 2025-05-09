using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public sealed record GaVIndexSignRecord(int VIndex, IntegerSign Sign) :
    IGaVIndexSignRecord;