using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaIdBivectorRecord(IIndexSet Id, XGaFloat64Bivector Bivector) :
    IXGaIdBivectorRecord;
