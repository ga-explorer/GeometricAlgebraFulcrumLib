using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaIdBivectorRecord(IIndexSet Id, XGaFloat64Bivector Bivector) :
    IXGaIdBivectorRecord;

public sealed record XGaIdBivectorRecord<T>(IIndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;