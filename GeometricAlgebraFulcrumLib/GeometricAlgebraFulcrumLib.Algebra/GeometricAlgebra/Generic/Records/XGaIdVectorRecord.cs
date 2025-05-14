using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public sealed record XGaIdVectorRecord<T>(IndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;