using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public sealed record XGaIdVectorRecord<T>(IndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;