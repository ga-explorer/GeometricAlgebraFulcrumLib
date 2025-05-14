using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public sealed record XGaIdBivectorRecord<T>(IndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;