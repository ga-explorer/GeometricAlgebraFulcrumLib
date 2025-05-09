using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdKVectorRecord<T>(IIndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;