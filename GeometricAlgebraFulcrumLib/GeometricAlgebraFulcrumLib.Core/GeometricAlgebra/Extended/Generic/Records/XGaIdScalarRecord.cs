using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdScalarRecord<T>(IndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;