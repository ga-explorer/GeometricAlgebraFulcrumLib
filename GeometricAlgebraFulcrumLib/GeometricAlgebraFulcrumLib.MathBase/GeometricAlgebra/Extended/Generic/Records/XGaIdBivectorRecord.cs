using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdBivectorRecord<T>(IIndexSet Id, XGaBivector<T> Bivector) :
    IXGaIdBivectorRecord<T>;