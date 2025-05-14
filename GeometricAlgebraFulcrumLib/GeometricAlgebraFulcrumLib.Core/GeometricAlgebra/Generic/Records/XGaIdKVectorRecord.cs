using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public sealed record XGaIdKVectorRecord<T>(IndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;