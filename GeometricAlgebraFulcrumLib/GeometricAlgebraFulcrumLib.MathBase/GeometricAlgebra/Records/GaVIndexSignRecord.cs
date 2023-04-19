using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records;

public sealed record GaVIndexSignRecord(int VIndex, IntegerSign Sign) :
    IGaVIndexSignRecord;