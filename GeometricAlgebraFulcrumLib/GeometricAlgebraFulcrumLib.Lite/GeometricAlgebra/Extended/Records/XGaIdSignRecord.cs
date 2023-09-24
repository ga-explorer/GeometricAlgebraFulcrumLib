using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaIdSignRecord(IIndexSet Id, IntegerSign Sign) :
    IXGaIdSignRecord;