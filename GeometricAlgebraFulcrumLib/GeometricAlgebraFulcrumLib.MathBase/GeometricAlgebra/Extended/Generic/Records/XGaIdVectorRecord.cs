using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records;

public sealed record XGaIdVectorRecord<T>(IIndexSet Id, XGaVector<T> Vector) :
    IXGaIdVectorRecord<T>;