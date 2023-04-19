using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended
{
    public sealed record XGaIdScalarRecord(IIndexSet Id, double Scalar) :
        IXGaIdScalarRecord<double>;

    public sealed record XGaIdScalarRecord<T>(IIndexSet Id, T Scalar) :
        IXGaIdScalarRecord<T>;
}