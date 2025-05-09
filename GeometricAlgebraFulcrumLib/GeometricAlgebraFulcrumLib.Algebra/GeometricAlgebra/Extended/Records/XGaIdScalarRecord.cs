using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdScalarRecord(IndexSet Id, double Scalar) :
    IXGaIdScalarRecord<double>;

public sealed record XGaIdScalarRecord<T>(IndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;