using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdBivectorRecord<T>(IndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;