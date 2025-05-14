using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public sealed record XGaIdScalarRecord<T>(IndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;