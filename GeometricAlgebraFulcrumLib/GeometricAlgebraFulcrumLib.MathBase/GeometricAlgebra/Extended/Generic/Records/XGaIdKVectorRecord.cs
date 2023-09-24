using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdKVectorRecord<T>(IIndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;