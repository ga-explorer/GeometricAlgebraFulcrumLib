using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

public sealed record GaVIndexPairSignRecord(int VIndex1, int VIndex2, IntegerSign Sign) :
    IGaVIndexPairSignRecord;