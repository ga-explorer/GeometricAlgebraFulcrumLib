using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdScalarRecord<T>(IndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;