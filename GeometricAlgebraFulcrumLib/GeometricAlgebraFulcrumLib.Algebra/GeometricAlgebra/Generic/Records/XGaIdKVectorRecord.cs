using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public sealed record XGaIdKVectorRecord<T>(IndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;