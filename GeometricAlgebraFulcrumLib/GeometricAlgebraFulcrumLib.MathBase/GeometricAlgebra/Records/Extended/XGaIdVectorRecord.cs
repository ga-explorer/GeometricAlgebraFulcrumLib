using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaIdVectorRecord(IIndexSet Id, XGaFloat64Vector Vector) :
    IXGaIdVectorRecord;

public sealed record XGaIdVectorRecord<T>(IIndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;