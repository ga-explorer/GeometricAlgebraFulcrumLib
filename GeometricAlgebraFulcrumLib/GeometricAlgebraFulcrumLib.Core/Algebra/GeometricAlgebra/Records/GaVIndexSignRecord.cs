using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

public sealed record GaVIndexSignRecord(int VIndex, IntegerSign Sign) :
    IGaVIndexSignRecord;