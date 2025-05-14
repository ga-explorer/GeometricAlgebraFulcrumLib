using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public sealed record XGaIdBivectorRecord<T>(IndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;