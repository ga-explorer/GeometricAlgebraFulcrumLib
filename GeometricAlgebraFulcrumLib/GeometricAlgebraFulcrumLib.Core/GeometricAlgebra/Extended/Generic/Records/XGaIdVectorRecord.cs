using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdVectorRecord<T>(IndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;