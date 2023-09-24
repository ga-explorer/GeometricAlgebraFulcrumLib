using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaIdKVectorRecord(IIndexSet Id, XGaFloat64KVector KVector) :
    IXGaIdKVectorRecord;
