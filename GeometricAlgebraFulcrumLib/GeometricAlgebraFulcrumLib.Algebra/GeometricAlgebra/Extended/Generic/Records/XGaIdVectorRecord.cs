using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdVectorRecord<T>(IIndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;