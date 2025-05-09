using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdBivectorRecord<T>(IIndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;