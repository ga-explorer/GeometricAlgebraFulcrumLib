using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdKVectorRecord<T>(IndexSet Id, XGaKVector<T> KVector) :
    IXGaIdKVectorRecord<T>;