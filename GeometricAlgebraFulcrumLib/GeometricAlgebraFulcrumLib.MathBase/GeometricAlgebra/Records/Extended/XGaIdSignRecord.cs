using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaIdSignRecord(IIndexSet Id, IntegerSign Sign) :
    IXGaIdSignRecord;