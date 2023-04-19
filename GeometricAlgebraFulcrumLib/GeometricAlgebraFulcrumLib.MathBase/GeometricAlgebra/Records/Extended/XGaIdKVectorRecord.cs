using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaIdKVectorRecord(IIndexSet Id, XGaFloat64KVector KVector) :
    IXGaIdKVectorRecord;

public sealed record XGaIdKVectorRecord<T>(IIndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;