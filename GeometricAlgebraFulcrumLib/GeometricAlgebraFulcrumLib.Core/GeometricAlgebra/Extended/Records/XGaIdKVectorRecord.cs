using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaIdKVectorRecord(IndexSet Id, XGaFloat64KVector KVector) :
    IXGaIdKVectorRecord;
