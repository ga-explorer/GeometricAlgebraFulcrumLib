using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records
{
    public sealed record XGaIdScalarRecord<T>(IIndexSet Id, T Scalar) :
        IXGaIdScalarRecord<T>;
}