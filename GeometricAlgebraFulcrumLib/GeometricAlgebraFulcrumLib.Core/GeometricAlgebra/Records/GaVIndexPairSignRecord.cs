using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public sealed record GaVIndexPairSignRecord(int VIndex1, int VIndex2, IntegerSign Sign) :
    IGaVIndexPairSignRecord;