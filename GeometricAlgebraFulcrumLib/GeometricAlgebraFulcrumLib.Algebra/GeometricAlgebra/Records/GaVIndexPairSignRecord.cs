using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

public sealed record GaVIndexPairSignRecord(int VIndex1, int VIndex2, IntegerSign Sign) :
    IGaVIndexPairSignRecord;