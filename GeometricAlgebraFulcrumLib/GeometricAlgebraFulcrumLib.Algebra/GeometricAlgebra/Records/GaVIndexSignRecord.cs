using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

public sealed record GaVIndexSignRecord(int VIndex, IntegerSign Sign) :
    IGaVIndexSignRecord;