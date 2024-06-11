using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdScalarRecord(IIndexSet Id, double Scalar) :
    IXGaIdScalarRecord<double>;

public sealed record XGaIdScalarRecord<T>(IIndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;